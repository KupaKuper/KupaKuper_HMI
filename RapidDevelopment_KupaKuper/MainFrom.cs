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
    public partial class MainFrom : Form
    {
        public MainFrom()
        {
            InitializeComponent();
        }
        #region 模板相关代码(不需要修改的代码)
        public BaseEthernet Ethernet { get; set; }
        public ConfigMode Config { get; set; }
        public List<IReadVariable> Variables { get; set; } = new();
        // 重写关闭事件，实现最小化到系统托盘
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // 取消默认关闭行为
            e.Cancel = true;
            // 最小化窗口到系统托盘
            this.WindowState = FormWindowState.Minimized;
            // 最小化时不在任务栏显示
            this.ShowInTaskbar = false;
            // 显示系统托盘图标提示
            notifyIcon1.ShowBalloonTip(1000, "RapidDevelopment_KupaKuper", "程序已最小化到系统托盘，双击图标恢复", ToolTipIcon.Info);
        }

        // 双击系统托盘图标恢复窗口
        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            // 恢复窗口
            this.WindowState = FormWindowState.Normal;
            // 恢复在任务栏显示
            this.ShowInTaskbar = true;
            // 激活窗口
            this.Activate();
        }

        // 右键菜单显示窗口
        private void showWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            this.Activate();
        }

        // 右键菜单退出程序
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 取消托盘图标显示
            notifyIcon1.Visible = false;
            // 允许程序真正退出
            Application.Exit();
        }
        /// <summary>
        ///首次运行会发生报错,正常现象
        ///首次运行后会在根目录下生成configTemplate.json文件,为个人配置的模板文件
        ///请根据实际情况修改configTemplate.json文件后,重命名为config.json
        /// </summary>
        private void InitConfig()
        {           
            Config = JsonFileHelper.ReadConfig<ConfigMode>(@"./", "config", true);
            Ethernet = new OpcUaEthernet(Config.IP, Config.HeartbeatAddress);
            Ethernet.ConnectChanged += Ethernet_ConnectChanged;
            SetConfig(Config);
            SetVariable_ChangedAnsy(Config);
            CyclicRead();
        }
        #endregion
        /// <summary>
        /// 窗口加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainFrom_Load(object sender, EventArgs e)
        {
            InitConfig();
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
                // 这里可以添加值变化时的逻辑
                Console.WriteLine($"test_1 changed to: {config.test_1.Value}");
                this.Invoke(() =>
                {
                    //label1.Text = config.test_1.Value.ToString();
                });
            };
            config.test_3.AnyPropertyChanged += (s, e) =>
            {
                // 这里可以添加值变化时的逻辑
                Console.WriteLine($"test_3 changed to: {config.test_3.Value}");
                this.Invoke(() =>
                {
                    //label2.Text = config.test_3.Value.ToString();
                });
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
