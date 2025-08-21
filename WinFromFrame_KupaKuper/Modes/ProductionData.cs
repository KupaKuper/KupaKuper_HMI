namespace WinFromFrame_KupaKuper.Modes
{
    /// <summary>
    /// 每小时产能数据模型
    /// </summary>
    public class ProductionData
    {
        /// <summary>
        /// 生产时间段
        /// </summary>
        public string? DateTime { get; set; }
        /// <summary>
        /// 生产总数
        /// </summary>
        public decimal Capacity { get; set; }
        /// <summary>
        /// OK的数量
        /// </summary>
        public int OKCount { get; set; }
        /// <summary>
        /// NG的数量
        /// </summary>
        public int NGCount { get; set; }
        /// <summary>
        /// 良率
        /// </summary>
        public double Yield { get; set; }
        /// <summary>
        /// 是否为白班或晚班
        /// </summary>
        public bool? Shift { get; set; }
    }
    /// <summary>
    /// 每天的产能数据模型
    /// </summary>
    public class DayProductionData
    {
        /// <summary>
        /// 日期
        /// </summary>
        public string? DateTime { get; set; }
        /// <summary>
        /// 当天生产总和
        /// </summary>
        public decimal SumCapacity { get; set; }
        /// <summary>
        /// 当天生产OK总和
        /// </summary>
        public int SumOKCount { get; set; }
        /// <summary>
        /// 当天生产NG总和
        /// </summary>
        public int SumNGCount { get; set; }
        /// <summary>
        /// 当天生产良率
        /// </summary>
        public double SumYield { get; set; }
        /// <summary>
        /// 每小时的生产数据
        /// </summary>
        public List<ProductionData> ProductionDatas { get; set; } = new List<ProductionData>();
    }
}
