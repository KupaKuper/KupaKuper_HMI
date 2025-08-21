namespace WinFromFrame_KupaKuper.Modes
{
    public class GantyrConfig
    {
        /// <summary>
        /// 龙门x方向轴名称
        /// </summary>
        public string Axis_X {  get; set; } = string.Empty;
        /// <summary>
        /// 龙门y方向轴名称
        /// </summary>
        public string Axis_Y { get; set; } = string.Empty;
        /// <summary>
        /// 龙门z方向轴名称
        /// </summary>
        public string? Axis_Z { get; set; } = string.Empty;
        /// <summary>
        /// 龙门点位名称和对应到每个轴的定位编号
        /// </summary>
        public Dictionary<string, string> GantryPositions { get; set; } = new();

    }
}
