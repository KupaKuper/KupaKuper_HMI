using KupaKuper_EthernetWrapper.Ethernet;

using KupaKuper_HelpClass;

using KupaKuper_HMI_Config.DeviceConfig.BaseType;

using System.Reflection;

namespace RapidDevelopment_KupaKuper
{
    /// <summary>
    /// ��������:
    /// 1.����ʵ�������޸�ConfigMode.cs�еı�������
    /// 2.���г���,����ʵ������޸ĸ�Ŀ¼�µ�configTemplate.json�ļ���,������Ϊconfig.json
    /// 3.��ɽ������
    /// 4.����ʵ�������޸��Զ����¼��еĴ���
    /// ע��:Ĭ��ʹ��OPC UAЭ�����ͨѶ,�����Ҫʹ������Э��,��ο�KupaKuper_EthernetWrapper��Ŀ�е�����Э��������޸�
    /// </summary>
    public partial class MainFrom : Form
    {
        public MainFrom()
        {
            InitializeComponent();
        }
        #region ģ����ش���(����Ҫ�޸ĵĴ���)
        public BaseEthernet Ethernet { get; set; }
        public ConfigMode Config { get; set; }
        public List<IReadVariable> Variables { get; set; } = new();
        // ��д�ر��¼���ʵ����С����ϵͳ����
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // ȡ��Ĭ�Ϲر���Ϊ
            e.Cancel = true;
            // ��С�����ڵ�ϵͳ����
            this.WindowState = FormWindowState.Minimized;
            // ��С��ʱ������������ʾ
            this.ShowInTaskbar = false;
            // ��ʾϵͳ����ͼ����ʾ
            notifyIcon1.ShowBalloonTip(1000, "RapidDevelopment_KupaKuper", "��������С����ϵͳ���̣�˫��ͼ��ָ�", ToolTipIcon.Info);
        }

        // ˫��ϵͳ����ͼ��ָ�����
        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            // �ָ�����
            this.WindowState = FormWindowState.Normal;
            // �ָ�����������ʾ
            this.ShowInTaskbar = true;
            // �����
            this.Activate();
        }

        // �Ҽ��˵���ʾ����
        private void showWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            this.Activate();
        }

        // �Ҽ��˵��˳�����
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // ȡ������ͼ����ʾ
            notifyIcon1.Visible = false;
            // ������������˳�
            Application.Exit();
        }
        /// <summary>
        ///�״����лᷢ������,��������
        ///�״����к���ڸ�Ŀ¼������configTemplate.json�ļ�,Ϊ�������õ�ģ���ļ�
        ///�����ʵ������޸�configTemplate.json�ļ���,������Ϊconfig.json
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
        /// ���ڼ����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainFrom_Load(object sender, EventArgs e)
        {
            InitConfig();
        }

        #region �Զ����¼�(��Ҫ�޸ĵĴ���)
        /// <summary>
        /// ����״̬�仯�¼�(�Զ���)
        /// </summary>
        /// <param name="e"></param>
        private void Ethernet_ConnectChanged(bool e)
        {

        }
        /// <summary>
        /// ���ÿɶ�������ֵ�仯�¼�(�Զ���)
        /// </summary>
        /// <param name="config"></param>
        private void SetVariable_ChangedAnsy(ConfigMode config)
        {
            config.test_1.AnyPropertyChanged += (s, e) =>
            {
                // ����������ֵ�仯ʱ���߼�
                Console.WriteLine($"test_1 changed to: {config.test_1.Value}");
                this.Invoke(() =>
                {
                    //label1.Text = config.test_1.Value.ToString();
                });
            };
            config.test_3.AnyPropertyChanged += (s, e) =>
            {
                // ����������ֵ�仯ʱ���߼�
                Console.WriteLine($"test_3 changed to: {config.test_3.Value}");
                this.Invoke(() =>
                {
                    //label2.Text = config.test_3.Value.ToString();
                });
            };
        }
        #endregion
        #region ģ�����(����Ҫ�޸ĵĴ���)
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="config"></param>
        private void SetConfig(ConfigMode config)
        {
            var testconfig = (ConfigMode)config;
            // ����t����������
            foreach (var property in testconfig.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var propertyType = property.PropertyType;

                // ������������Ƿ�ΪĿ������
                if (propertyType.BaseType == typeof(PlcVar) || propertyType.BaseType?.BaseType == typeof(PlcVar))
                {
                    var variable = property.GetValue(testconfig);
                    if (variable == null) continue;
                    // ���ݱ������������ض�����
                    if (variable is WriteReadVariable wrVar)
                    {
                        // ���ÿɶ���д��������
                        wrVar.SetValue += (value) =>
                        {
                            // ��������������ֵ���߼�
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
                        // ����ֻд��������
                        woVar.SetValue += (value) =>
                        {
                            // ��������������ֵ���߼�
                            Ethernet.TryWrite(woVar.PlcVarAddress, value);
                        };
                    }
                }
            }
        }
        /// <summary>
        /// ��ȡ������ѭ������
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
        /// ���PLCд������ʱ,����ͨ����������ģ���е�SetCommand������ʵ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            //ע��д����������ͱ����PLC�б�������������һ��
            Config.test_2.SetCommand.Execute(true);
        }
    }
}
