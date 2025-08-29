using KupaKuper_HMI_Config.DeviceConfig.BaseType;

namespace RapidDevelopment_KupaKuper
{
    /// <summary>
    /// 配置需要的变量类型模型,根据实际需求进行修改
    /// </summary>
    public class ConfigMode: BaseMode
    {
        public ReadOnlyVariable<bool> test_1 { get; set; } = new();
        public WriteOnlyVariable test_2 { get; set; } = new();
        public WriteReadVariable<int> test_3 { get; set; } = new();
    }
}
