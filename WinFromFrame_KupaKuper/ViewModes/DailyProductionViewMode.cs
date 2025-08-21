using KupaKuper_DeviceSever.Server;

using WinFromFrame_KupaKuper.Help;
using WinFromFrame_KupaKuper.Modes;

namespace WinFromFrame_KupaKuper.ViewModes
{
    public class DailyProductionViewMode : BaseViewMode
    {
        public List<ProductionData> allProductionData = new();
        public List<ProductionData> currentProductionData = new();
        public string currentShift = "白班";

        public bool IsReadData = false;
        private IDeviceSystemServer _Server;

        public override required Action UpdataView { get; set; }

        public DailyProductionViewMode(IDeviceSystemServer _Server) : base(_Server)
        {
            this._Server = _Server;
        }
        public override void _Server_DeviceChanged()
        {
            // 设备切换时读取数据
            base._Server_DeviceChanged();
            string UphDataPath = Path.Combine(_Server.CurrentDevice.DeviceConfig.device.DataConfig.DailyProductAdr, $"UPH\\{DateTime.Now.ToString("yyyyMMdd")}.csv");
            ReadCsvData(UphDataPath);
            UpdateChartData(currentShift);
            UpdataView();
        }
        public override void RemovePageQuest()
        {
            base.RemovePageQuest();
            allProductionData.Clear();
        }
        /// <summary>
        /// 读取CSV文件数据
        /// </summary>
        public void ReadCsvData(string csvPath)
        {
            allProductionData.Clear();
            allProductionData = ReadProductDataHelp.GetUphData(csvPath)?.ProductionDatas ?? [];
        }

        /// <summary>
        /// 更新图表数据
        /// </summary>
        public void UpdateChartData(string shiftType)
        {
            IEnumerable<ProductionData> filteredData;

            switch (shiftType)
            {
                case "白班":
                    filteredData = allProductionData.Where(d => d.Shift == true);

                    break;
                case "晚班":
                    filteredData = allProductionData.Where(d => d.Shift == false);
                    break;
                default:
                    filteredData = allProductionData;
                    break;
            }
            currentProductionData = filteredData.ToList();
            IsReadData = true;
        }
    }
}
