using KupaKuper_HelpClass;

using KupaKuper_HMI_Config.DeviceConfig;
using KupaKuper_HMI_Config.Services;

using KupaKuper_HMI_ConfigTool.Help;

namespace KupaKuper_HMI_ConfigTool
{
    public partial class Form1 : Form
    {
        private Device? device;
        private LocalizationManager manager = new();

        /// <summary>
        /// 字库CSV文件路径
        /// </summary>
        public string DictionaryPath { get; set; } = @"Localization\dictionary.csv";

        /// <summary>
        /// 语言包输出目录
        /// </summary>
        public string OutputPath { get; set; } = @"Localization\Packages\";

        /// <summary>
        /// 设备信息标签列表
        /// </summary>
        private List<Label> deviceInfoLabels = new();

        public Form1()
        {
            InitializeComponent();
            InitializeDeviceInfoPanel();
        }

        /// <summary>
        /// 初始化设备信息面板
        /// </summary>
        private void InitializeDeviceInfoPanel()
        {
            string[] labels = {
                "设备名称", "设备类型", "设备地址",
                "输入IO数量", "输出IO数量", "气缸数量",
                "轴数量", "报警数量", "参数数量", "循环读取数量"
            };

            for (int i = 0; i < labels.Length; i++)
            {
                Label lblName = new Label
                {
                    Text = labels[i] + ":",
                    Font = new Font("Microsoft YaHei UI", 9F),
                    ForeColor = Color.FromArgb(100, 100, 100),
                    Location = new Point(10+(i/6)*325, 10 + ((i % 6) * 22)),
                    Size = new Size(85, 20),
                    TextAlign = ContentAlignment.MiddleRight
                };

                Label lblValue = new Label
                {
                    Name = $"lblValue_{i}",
                    Text = "-",
                    Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(64, 158, 219),
                    Location = new Point(95+(i/6)*325, 10 + ((i % 6) * 22)),
                    Size = new Size(200, 20),
                    TextAlign = ContentAlignment.MiddleLeft
                };

                deviceInfoLabels.Add(lblValue);
                panelDeviceInfo.Controls.Add(lblName);
                panelDeviceInfo.Controls.Add(lblValue);
            }
        }

        /// <summary>
        /// 读取Excel配置文件
        /// </summary>
        private void but_OpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Excel文件|*.xlsx"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                try
                {
                    device = XlsxHelp.ReadXlsx(filePath);
                    AddMessage($"[成功] 读取配置文件: {filePath}");
                    AddDeviceMessage(device);
                }
                catch (Exception ex)
                {
                    AddMessage($"[错误] 读取文件失败: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// 生成设备文件（JSON + 字典 + 语言包）
        /// </summary>
        private void butSaveJson_Click(object sender, EventArgs e)
        {
            if (device == null)
            {
                AddMessage("[警告] 请先加载配置文件");
                return;
            }

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.FileName = device.DeviceMessage.DeviceName;
                saveFileDialog.Filter = "";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string directoryPath = Path.GetDirectoryName(saveFileDialog.FileName)!;
                    string configName = Path.GetFileNameWithoutExtension(saveFileDialog.FileName);

                    string configFolder = Path.Combine(directoryPath, configName);
                    try
                    {
                        Directory.CreateDirectory(configFolder);

                        string dictPath = Path.Combine(configFolder, DictionaryPath);
                        string outputPath = Path.Combine(configFolder, OutputPath);

                        manager.GenerateDictionaryAsync(device, dictPath).Wait();
                        manager.BuildLanguagePacksAsync(dictPath, outputPath).Wait();

                        string jsonPath = Path.Combine(configFolder, "Config.json");
                        JsonFileHelper.SaveJson(jsonPath, device);

                        AddMessage($"[成功] JSON文件已保存: {jsonPath}");
                        AddMessage($"[成功] 配置文件夹已创建: {configFolder}");
                    }
                    catch (Exception ex)
                    {
                        AddMessage($"[错误] 保存配置失败: {ex.Message}");
                    }
                }
            }
        }

        /// <summary>
        /// 保存为Excel文件
        /// </summary>
        private void butSaveExcel_Click(object sender, EventArgs e)
        {
            if (device == null)
            {
                AddMessage("[警告] 请先加载配置文件");
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Xlsx格式|*.xlsx",
                FileName = "Config"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;
                try
                {
                    XlsxHelp.SaveXlsx(filePath, device);
                    AddMessage($"[成功] Excel文件已保存: {filePath}");
                }
                catch (Exception ex)
                {
                    AddMessage($"[错误] 保存Excel失败: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// 添加消息到日志文本框
        /// </summary>
        /// <param name="message">消息内容</param>
        public void AddMessage(string message)
        {
            string timestamp = DateTime.Now.ToString("HH:mm:ss");
            txtMessage.AppendText($"[{timestamp}] {message}\r\n");
            txtMessage.SelectionStart = txtMessage.Text.Length;
            txtMessage.ScrollToCaret();
        }

        /// <summary>
        /// 生成新的Excel配置文件
        /// </summary>
        private void but_SaveNewCsv_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Xlsx格式|*.xlsx",
                FileName = "Config"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;
                try
                {
                    XlsxHelp.CopyTemplate(filePath);
                    AddMessage($"[成功] 新建Excel文件: {filePath}");
                }
                catch (Exception ex)
                {
                    AddMessage($"[错误] 生成Excel失败: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// 读取JSON配置文件
        /// </summary>
        private void but_OpenJsonFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "JSON文件|*.json"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                try
                {
                    device = JsonFileHelper.ReadJson<Device>(filePath);
                    AddMessage($"[成功] 读取配置文件: {filePath}");
                    AddDeviceMessage(device);
                }
                catch (Exception ex)
                {
                    AddMessage($"[错误] 读取JSON失败: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// 显示设备信息到面板
        /// </summary>
        /// <param name="device">设备对象</param>
        private void AddDeviceMessage(Device device)
        {
            if (deviceInfoLabels.Count < 10) return;

            deviceInfoLabels[0].Text = device.DeviceMessage.DeviceName;
            deviceInfoLabels[1].Text = device.DeviceMessage.DeviceType.ToString();
            deviceInfoLabels[2].Text = device.DeviceMessage.DeviceAddress;
            deviceInfoLabels[3].Text = device.IoConfig.InputIoList.Count.ToString();
            deviceInfoLabels[4].Text = device.IoConfig.OutputIoList.Count.ToString();
            deviceInfoLabels[5].Text = device.CylindersConfig.CylinderList.Count.ToString();
            deviceInfoLabels[6].Text = device.AxesConfig.AxisList.Count.ToString();
            deviceInfoLabels[7].Text = device.AlarmsConfig.AlarmList.Count.ToString();
            deviceInfoLabels[8].Text = device.ParametersConfig.ParameterList.Count.ToString();
            deviceInfoLabels[9].Text = device.CyclicReadConfig.CyclicReadList.Count.ToString();

            AddMessage($"[信息] 设备信息已更新");
        }

        /// <summary>
        /// 仅保存JSON文件
        /// </summary>
        private void but_OnlyJson_Click(object sender, EventArgs e)
        {
            if (device == null)
            {
                AddMessage("[警告] 请先加载配置文件");
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "JSON格式|*.json",
                FileName = "Config"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;
                string configFolder = Path.GetDirectoryName(filePath)!;

                try
                {
                    string dictPath = Path.Combine(configFolder, DictionaryPath);
                    string outputPath = Path.Combine(configFolder, OutputPath);

                    manager.GenerateDictionaryAsync(device, dictPath).Wait();
                    manager.BuildLanguagePacksAsync(dictPath, outputPath).Wait();

                    JsonFileHelper.SaveJson(filePath, device);
                    AddMessage($"[成功] JSON文件已保存: {filePath}");
                }
                catch (Exception ex)
                {
                    AddMessage($"[错误] 保存JSON失败: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// 生成语言包
        /// </summary>
        private void but_SaveLanguage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "JSON文件|*.json"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                string configFolder = Path.GetDirectoryName(filePath)!;

                try
                {
                    device = JsonFileHelper.ReadJson<Device>(filePath);

                    string dictPath = Path.Combine(configFolder, DictionaryPath);
                    string outputPath = Path.Combine(configFolder, OutputPath);

                    manager.GenerateDictionaryAsync(device, dictPath).Wait();
                    manager.BuildLanguagePacksAsync(dictPath, outputPath).Wait();

                    AddMessage($"[成功] 语言包已生成: {outputPath}");
                }
                catch (Exception ex)
                {
                    AddMessage($"[错误] 生成语言包失败: {ex.Message}");
                }
            }
        }
    }
}