namespace WinFromFrame_KupaKuper.Help.GlobalParameters
{
    public class AppGlobalParameter
    {
        /// <summary>
        /// light主题颜色
        /// </summary>
        public string LightThemeBackgroundColor { get; set; } = "#ffffff";
        /// <summary>
        /// dark主题颜色
        /// </summary>
        public string DarkThemeBackgroundColor { get; set; } = "#3b3b3b";
        /// <summary>
        /// light主题文字颜色
        /// </summary>
        public string LightThemeTextColor { get; set; } = "#000000";
        /// <summary>
        /// dark主题文字颜色
        /// </summary>
        public string DarkThemeTextColor { get; set; } = "#ffffff";
        /// <summary>
        /// light主题主要颜色
        /// </summary>
        public string LightThemePrimaryColor { get; set; } = "#69b7f7";
        /// <summary>
        /// dark主题主要颜色
        /// </summary>
        public string DarkThemePrimaryColor { get; set; } = "#5d85d5";
        /// <summary>
        /// light主题阴影颜色
        /// </summary>
        public string LightThemeShadowColor { get; set; } = "#dedede";
        /// <summary>
        /// dark主题阴影颜色
        /// </summary>
        public string DarkThemeShadowColor { get; set; } = "#282828";
        /// <summary>
        /// 页面滚动间隔时间(s)
        /// </summary>
        public double ScrollIntervalS { get; set; } = 10;
        /// <summary>
        /// 每页显示的IO数量
        /// </summary>
        public int PageIoCount { get; set; } = 48;
        /// <summary>
        /// 每行显示的Io数量
        /// </summary>
        public int RowIoCount { get; set; } = 4;
        /// <summary>
        /// 每页显示的气缸数量
        /// </summary>
        public int PageCylinderCount { get; set; } = 16;
        /// <summary>
        /// 设置对应CSV文件显示列表时的列宽调整
        /// </summary>
        public Dictionary<string, List<string>> CsvColumnWidthsSetting { get; set; } = new()
        {
            {string.Empty, new(){"1,100px"} } // 默认空设置
        };

    }
}
