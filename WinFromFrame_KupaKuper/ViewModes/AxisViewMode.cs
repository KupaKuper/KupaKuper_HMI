using KupaKuper_DeviceSever.Server;

using KupaKuper_HMI_Config.DeviceConfig.BaseType;

using System.Collections.ObjectModel;

using WinFromFrame_KupaKuper.Modes;

namespace WinFromFrame_KupaKuper.ViewModes
{
    public class AxisViewMode : BaseViewMode
    {
        private IDeviceSystemServer _Server;
        public AxisViewMode(IDeviceSystemServer _Server) : base(_Server)
        {
            this._Server = _Server;
        }
        /// <summary>
        /// 轴数据
        /// </summary>
        public ObservableCollection<Axis>? AxesModes;
        /// <summary>
        /// 所有轴的名称
        /// </summary>
        public List<string> AxesGroups = new();
        public override required Action UpdataView { get; set; }
        /// <summary>
        /// 需要重构设备切换或页面加载时的数据读取
        /// </summary>
        public override void _Server_DeviceChanged()
        {
            base._Server_DeviceChanged();
            AxesGroups = _Server.CurrentDevice.AxesName;
            string axisGroupRember = _Server.CurrentDeviceRember.AxisGroup == "" ? AxesGroups[0] : _Server.CurrentDeviceRember.AxisGroup;
            AxesModes = _Server.CurrentDevice.AxesModes[axisGroupRember];
            _Server.CurrentDeviceRember.AxisGroup = axisGroupRember;
            _Server.CurrentDevice.RequestUpdataAxis(_Server.ServerID, _Server.CurrentDeviceRember.AxisGroup);
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
            _Server.CurrentDevice.RemoveUpdataAxis(_Server.ServerID, _Server.CurrentDeviceRember.AxisGroup);
        }
        /// <summary>
        /// 切换分组
        /// </summary>
        /// <param name="group"></param>
        public void SelectGroup(string group)
        {
            _Server.CurrentDevice.RemoveUpdataAxis(_Server.ServerID, _Server.CurrentDeviceRember.AxisGroup);
            if (_Server.CurrentDevice.AxesModes.TryGetValue(group, out ObservableCollection<Axis>? axisModes))
            {
                AxesModes = axisModes;
                _Server.CurrentDeviceRember.AxisGroup = group;
            }
            _Server.CurrentDevice.RequestUpdataAxis(_Server.ServerID, group);
        }
        public override void _Server_UserChanged(IDeviceSystemServer.UserType user)
        {
            UpdataView();
        }
    }
}
