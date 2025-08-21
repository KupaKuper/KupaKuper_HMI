using KupaKuper_DeviceSever.Server;

using KupaKuper_HMI_Config.DeviceConfig.BaseType;

using System.Collections.ObjectModel;

using WinFromFrame_KupaKuper.Help.GlobalParameters;
using WinFromFrame_KupaKuper.Modes;

namespace WinFromFrame_KupaKuper.ViewModes
{
    public class CylinderViewMode : BaseViewMode
    {
        private IDeviceSystemServer _Server;
        public CylinderViewMode(IDeviceSystemServer _Server) : base(_Server)
        {
            this._Server = _Server;
        }
        public ObservableCollection<Cylinder>? CylinderModes;
        public List<string> CylinderGroups = new();
        public int CurrentPage = 1;
        public int TotalPages = 1;
        public int ItemsPerPage = GetGlobalParameter.AppGlobalParameter.PageCylinderCount;

        public override required Action UpdataView { get; set; }
        /// <summary>
        /// 需要重构设备切换或页面加载时的数据读取
        /// </summary>
        public override void _Server_DeviceChanged()
        {
            base._Server_DeviceChanged();
            CylinderGroups = _Server.CurrentDevice.CylindersName;
            string cylinderGroupRember = _Server.CurrentDeviceRember.CylinderGroup == "" ? CylinderGroups[0] : _Server.CurrentDeviceRember.CylinderGroup;
            CylinderModes = _Server.CurrentDevice.CylinderModes[cylinderGroupRember];
            _Server.CurrentDeviceRember.CylinderGroup = cylinderGroupRember;
            CurrentPage = _Server.CurrentDeviceRember.CylinderIndex;
            TotalPages = (CylinderModes.Count - 1) / ItemsPerPage + 1;
            _Server.CurrentDevice.RequestUpdataCylinders(_Server.ServerID, cylinderGroupRember);
            UpdataView();
        }
        /// <summary>
        /// 页面加载默认执行UpdateDevice,重构以执行其它的事件
        /// </summary>
        public override void OnInitialized()
        {
            base.OnInitialized();
        }
        /// <summary>
        /// 页面关闭时执行的额外事件
        /// </summary>
        public override void RemovePageQuest()
        {
            base.RemovePageQuest();
            _Server.CurrentDevice.RemoveUpdataCylinders(_Server.ServerID, _Server.CurrentDeviceRember.CylinderGroup);

        }
        /// <summary>
        /// 切换分组
        /// </summary>
        /// <param name="group"></param>
        public void SelectGroup(string group)
        {
            _Server.CurrentDevice.RemoveUpdataCylinders(_Server.ServerID, _Server.CurrentDeviceRember.CylinderGroup);
            if (_Server.CurrentDevice.CylinderModes.TryGetValue(group, out ObservableCollection<Cylinder>? cylinderModes))
            {
                CylinderModes = cylinderModes;
                _Server.CurrentDeviceRember.CylinderGroup = group;
                TotalPages = (CylinderModes.Count - 1) / ItemsPerPage + 1;
                CurrentPage = 1;
            }
            _Server.CurrentDevice.RequestUpdataCylinders(_Server.ServerID, group);
        }
        /// <summary>
        /// 上一页
        /// </summary>
        public void PreviousPage()
        {
            if (CurrentPage > 1)
            {
                CurrentPage--;
                _Server.CurrentDeviceRember.CylinderIndex = CurrentPage;
                UpdataView();
            }
        }
        /// <summary>
        /// 下一页
        /// </summary>
        public void NextPage()
        {
            if (CurrentPage < TotalPages)
            {
                CurrentPage++;
                _Server.CurrentDeviceRember.CylinderIndex = CurrentPage;
                UpdataView();
            }
        }
        public override void _Server_UserChanged(IDeviceSystemServer.UserType user)
        {
            UpdataView();
        }
    }
}
