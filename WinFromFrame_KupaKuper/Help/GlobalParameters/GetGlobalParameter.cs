using KupaKuper_DeviceSever.Server;

using KupaKuper_HMI_Config.Help;

namespace WinFromFrame_KupaKuper.Help.GlobalParameters
{
    public static class GetGlobalParameter
    {
        /// <summary>
        /// 获取全局参数
        /// </summary>
        /// <param name="parameterName">参数名称</param>
        /// <returns>参数值</returns>
        public static void GetParameter()
        {
            string AppGlobalParameterPath = Path.Combine(DeviceSystems.AppConfigPath, "AppGlobalParameter.json");
            if (!File.Exists(AppGlobalParameterPath))
            {
                AppGlobalParameter = new AppGlobalParameter();
                JsonFileHelper.SaveJson(AppGlobalParameterPath, AppGlobalParameter);
                return;
            }
            AppGlobalParameter = JsonFileHelper.ReadJson<AppGlobalParameter>(AppGlobalParameterPath);
        }
        /// <summary>
        /// 全局参数
        /// </summary>
        public static AppGlobalParameter AppGlobalParameter { get; private set; } = new();
    }
}
