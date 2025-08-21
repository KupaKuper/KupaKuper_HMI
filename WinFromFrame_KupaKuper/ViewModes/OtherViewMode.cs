using KupaKuper_DeviceSever.Server;

using KupaKuper_HMI_Config.DeviceConfig.BaseType;

using System.Collections.ObjectModel;

using WinFromFrame_KupaKuper.Modes;

namespace WinFromFrame_KupaKuper.ViewModes
{
    public class OtherViewMode : BaseViewMode
    {
        private IDeviceSystemServer _Server;
        public OtherViewMode(IDeviceSystemServer _Server) : base(_Server)
        {
            this._Server = _Server;
        }
        public ObservableCollection<Parameter>? ParameterModes;
        public List<string> ParameterGroups = new();
        public override required Action UpdataView { get; set; }
        /// <summary>
        /// 需要重构设备切换或页面加载时的数据读取
        /// </summary>
        public override void _Server_DeviceChanged()
        {
            base._Server_DeviceChanged();
            ParameterGroups = _Server.CurrentDevice.ParametersName;
            string parameterGroupRember = _Server.CurrentDeviceRember.ParameterGroup == "" ? ParameterGroups[0] : _Server.CurrentDeviceRember.ParameterGroup;
            ParameterModes = _Server.CurrentDevice.ParameterModes[parameterGroupRember];
            _Server.CurrentDeviceRember.ParameterGroup = parameterGroupRember;
            _Server.CurrentDevice.RequestUpdataParameters(_Server.ServerID, _Server.CurrentDeviceRember.ParameterGroup);
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
            _Server.CurrentDevice.RemoveUpdataParameters(_Server.ServerID, _Server.CurrentDeviceRember.ParameterGroup);
        }
        /// <summary>
        /// 切换分组
        /// </summary>
        /// <param name="group"></param>
        public void SelectGroup(string group)
        {
            _Server.CurrentDevice.RemoveUpdataParameters(_Server.ServerID, _Server.CurrentDeviceRember.ParameterGroup);
            if (_Server.CurrentDevice.ParameterModes.TryGetValue(group, out ObservableCollection<Parameter>? parameterModes))
            {
                ParameterModes = parameterModes;
                _Server.CurrentDeviceRember.ParameterGroup = group;
            }
            _Server.CurrentDevice.RequestUpdataParameters(_Server.ServerID, group);
        }

        public override void _Server_UserChanged(IDeviceSystemServer.UserType user)
        {
            UpdataView();
        }
    }
}
