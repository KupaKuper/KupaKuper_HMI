namespace WinFromFrame_KupaKuper.Modes
{
    enum EnumDeviceMode
    {
        /// <summary>
        /// 没有状态
        /// </summary>
        none = 0,
        /// <summary>
        /// 自动运行中
        /// </summary>
        autoRunning = 1,
        /// <summary>
        /// 手动运行中
        /// </summary>
        manualRunning = 2,
        /// <summary>
        /// 初始化运行中
        /// </summary>
        initRunning = 3,
        /// <summary>
        /// 初始化完成
        /// </summary>
        initDone = 4,
        /// <summary>
        /// 暂停中
        /// </summary>
        pause = 5,
        /// <summary>
        /// 待料中
        /// </summary>
        waitProduct = 6,
        /// <summary>
        /// 报警中
        /// </summary>
        alarm = 7,
        /// <summary>
        /// 停止中
        /// </summary>
        stop = 8,
        /// <summary>
        /// 设备异常(急停等)
        /// </summary>
        error = 9,
    }
}
