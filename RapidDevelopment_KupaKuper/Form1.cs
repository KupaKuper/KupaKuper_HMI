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
            //�״����лᷢ������,��������
            //�״����к���ڸ�Ŀ¼������configTemplate.json�ļ�,Ϊ�������õ�ģ���ļ�
            //�����ʵ������޸�configTemplate.json�ļ���,������Ϊconfig.json
            Config = JsonFileHelper.ReadConfig<ConfigMode>(@"./", "config", true);
            Ethernet = new OpcUaEthernet(Config.IP, Config.HeartbeatAddress);
            Ethernet.ConnectChanged += Ethernet_ConnectChanged;
            SetConfig(Config);
            SetVariable_ChangedAnsy(Config);
            CyclicRead();
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
                // �������������Ա仯ʱ���߼�
                Console.WriteLine($"test_1 changed to: {config.test_1.Value}");
            };
            config.test_3.AnyPropertyChanged += (s, e) =>
            {
                // �������������Ա仯ʱ���߼�
                Console.WriteLine($"test_3 changed to: {config.test_3.Value}");
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
