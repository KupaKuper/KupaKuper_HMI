using KupaKuper_EthernetWrapper.Ethernet;

using KupaKuper_HelpClass;

using KupaKuper_HMI_Config.DeviceConfig.BaseType;

using System.Reflection;

namespace RapidDevelopment_KupaKuper
{
    /// <summary>
    /// 开发步骤:
    /// 1.根据实际需求修改ConfigMode.cs中的变量类型
    /// 2.运行程序,根据实际情况修改根目录下的configTemplate.json文件后,重命名为config.json
    /// 3.完成界面设计
    /// 4.根据实际需求修改自定义事件中的代码
    /// 注意:默认使用OPC UA协议进行通讯,如果需要使用其他协议,请参考KupaKuper_EthernetWrapper项目中的其他协议类进行修改
    /// </summary>
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public BaseEthernet Ethernet { get; set; }
        public ConfigMode Config { get; set; }
        public List<IReadVariable> Variables { get; set; } = new();
        private void Form1_Load(object sender, EventArgs e)
        {
            //首次运行会发生报错,正常现象
            //首次运行后会在根目录下生成configTemplate.json文件,为个人配置的模板文件
            //请根据实际情况修改configTemplate.json文件后,重命名为config.json
            Config = JsonFileHelper.ReadConfig<ConfigMode>(@"./", "config", true);
            Ethernet = new OpcUaEthernet(Config.IP, Config.HeartbeatAddress);
            Ethernet.ConnectChanged += Ethernet_ConnectChanged;
            SetConfig(Config);
            SetVariable_ChangedAnsy(Config);
            CyclicRead();
        }

        #region 自定义事件(需要修改的代码)
        /// <summary>
        /// 连接状态变化事件(自定义)
        /// </summary>
        /// <param name="e"></param>
        private void Ethernet_ConnectChanged(bool e)
        {

        }
        /// <summary>
        /// 配置可读变量的值变化事件(自定义)
        /// </summary>
        /// <param name="config"></param>
        private void SetVariable_ChangedAnsy(ConfigMode config)
        {
            config.test_1.AnyPropertyChanged += (s, e) =>
            {
                // 这里可以添加属性变化时的逻辑
                Console.WriteLine($"test_1 changed to: {config.test_1.Value}");
            };
            config.test_3.AnyPropertyChanged += (s, e) =>
            {
                // 这里可以添加属性变化时的逻辑
                Console.WriteLine($"test_3 changed to: {config.test_3.Value}");
            };
        }
        #endregion
        #region 模板代码(不需要修改的代码)
        /// <summary>
        /// 变量配置
        /// </summary>
        /// <param name="config"></param>
        private void SetConfig(ConfigMode config)
        {
            var testconfig = (ConfigMode)config;
            // 遍历t的所有属性
            foreach (var property in testconfig.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var propertyType = property.PropertyType;

                // 检查属性类型是否为目标类型
                if (propertyType.BaseType == typeof(PlcVar) || propertyType.BaseType?.BaseType == typeof(PlcVar))
                {
                    var variable = property.GetValue(testconfig);
                    if (variable == null) continue;
                    // 根据变量类型设置特定属性
                    if (variable is WriteReadVariable wrVar)
                    {
                        // 设置可读可写变量属性
                        wrVar.SetValue += (value) =>
                        {
                            // 这里可以添加设置值的逻辑
                            Ethernet.TryWrite(wrVar.PlcVarAddress, value);
                        };
                        Variables.Add(wrVar);
                    }
                    else if (variable is ReadOnlyVariable roVar)
                    {
                        Variables.Add(roVar);
                    }
                    else if (variable is WriteOnlyVariable woVar)
                    {
                        // 设置只写变量属性
                        woVar.SetValue += (value) =>
                        {
                            // 这里可以添加设置值的逻辑
                            Ethernet.TryWrite(woVar.PlcVarAddress, value);
                        };
                    }
                }
            }
        }
        /// <summary>
        /// 读取变量的循环任务
        /// </summary>
        private void CyclicRead()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    foreach (var item in Variables)
                    {
                        if (item is WriteReadVariable wr)
                        {
                            if (Ethernet.TryRead(wr.PlcVarAddress, out var value))
                            {
                                if (wr.Value != value && value != null) wr.Value = value;
                            }
                        }
                        else if (item is ReadOnlyVariable ro)
                        {
                            if (Ethernet.TryRead(ro.PlcVarAddress, out var value))
                            {
                                if (ro.Value != value && value != null) ro.Value = value;
                            }
                        }
                    }
                    Thread.Sleep(10);
                }
            });
        }
        #endregion

        /// <summary>
        /// 想给PLC写入数据时,可以通过调用配置模型中的SetCommand命令来实现
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            //注意写入的数据类型必须和PLC中变量的数据类型一致
            Config.test_2.SetCommand.Execute(true);
        }
    }
}
