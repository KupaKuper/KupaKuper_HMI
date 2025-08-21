using KupaKuper_DeviceSever.Server;

using WinFromFrame_KupaKuper.Modes;

namespace WinFromFrame_KupaKuper.ViewModes
{
    public class DataRecordViewMode : BaseViewMode
    {
        private IDeviceSystemServer _Server;
        public DataRecordViewMode(IDeviceSystemServer _Server) : base(_Server)
        {
            this._Server = _Server;
        }
        public string CurrentCsvPath { get; set; } = string.Empty;

        public List<string> DataRecords { get; set; } = new List<string>();

        public override required Action UpdataView { get; set; }
        /// <summary>
        /// 需要重构设备切换或页面加载时的数据读取
        /// </summary>
        public override void _Server_DeviceChanged()
        {
            base._Server_DeviceChanged();
            DataRecords = _Server.CurrentDevice.DeviceConfig.device.DataConfig.ReadCsvAddress;
            UpdataView();
        }
        /// <summary>
        /// 页面加载默认执行UpdateDevice,重构以执行其它的事件
        /// </summary>
        public override void OnInitialized()
        {
            base.OnInitialized();
            CurrentCsvPath=DataRecords.First().ToString();
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
