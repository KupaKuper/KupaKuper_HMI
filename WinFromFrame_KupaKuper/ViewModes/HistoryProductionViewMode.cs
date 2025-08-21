using KupaKuper_DeviceSever.Server;

using WinFromFrame_KupaKuper.Help;
using WinFromFrame_KupaKuper.Modes;

namespace WinFromFrame_KupaKuper.ViewModes
{
    public class HistoryProductionViewMode : BaseViewMode
    {
        private IDeviceSystemServer _Server;
        public HistoryProductionViewMode(IDeviceSystemServer _Server) : base(_Server)
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
            UpdateDateTime();
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

        public DateTime StartDateTime { get; set; } = DateTime.Now.AddDays(-8);
        public DateTime EndDateTime { get; set; } = DateTime.Now.AddDays(-1);
        public List<DayProductionData> dayProductionDatas { get; set; } = new();
        public DayProductionData? CurrentProductData { get; set; }
        public string ApexChartKey = Guid.NewGuid().ToString();

        public void UpdateDateTime()
        {
            if (StartDateTime > EndDateTime) return;
            dayProductionDatas.Clear();
            for (DateTime date = StartDateTime; date <= EndDateTime; date = date.AddDays(1))
            {
                string dateString = date.ToString("yyyyMMdd");
                string csvPath = Path.Combine(_Server.CurrentDevice.DeviceConfig.device.DataConfig.DailyProductAdr, $"UPH\\{dateString}.csv");
                DayProductionData? dayProductionData = ReadProductDataHelp.GetUphData(csvPath);
                if (dayProductionData != null) dayProductionDatas.Add(dayProductionData);
            }
            ApexChartKey = Guid.NewGuid().ToString();
        }
    }
}
