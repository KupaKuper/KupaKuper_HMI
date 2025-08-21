using KupaKuper_DeviceSever.Server;

using WinFromFrame_KupaKuper.Modes;

namespace WinFromFrame_KupaKuper.ViewModes
{
    public class FaultStatisticsViewMode : BaseViewMode
    {
        private IDeviceSystemServer _Server;
        public FaultStatisticsViewMode(IDeviceSystemServer _Server) : base(_Server)
        {
            this._Server = _Server;
        }

        public override required Action UpdataView { get; set; }
        /// <summary>
        /// 需要重构设备切换或页面加载时的数据读取
        /// </summary>
        public override void _Server_DeviceChanged()
        {
            base._Server_DeviceChanged();
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
        }

        
    }
}
