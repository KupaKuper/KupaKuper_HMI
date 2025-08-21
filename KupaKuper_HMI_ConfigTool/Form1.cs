using KupaKuper_HMI_Config.DeviceConfig;
using KupaKuper_HMI_Config.Help;
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
        public Form1()
        {
            InitializeComponent();
        }
        private void but_OpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel文件|*.xlsx";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                try
                {
                    device = XlsxHelp.ReadXlsx(filePath);
                    AddMessage($"成功读取配置文件: {filePath}\n");
                    AddDeviceMessage(device);
                }
                catch (Exception ex)
                {
                    AddMessage($"读取文件时发生错误: {ex.Message}\n");
                }
            }
        }
        private void butSaveJson_Click(object sender, EventArgs e)
        {
            if (device == null)
            {
                AddMessage("请先加载配置文件\n");
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

                    // 创建配置文件夹
                    string configFolder = Path.Combine(directoryPath, configName);
                    try
                    {
                        Directory.CreateDirectory(configFolder);

                        // 生成字典和语言包
                        string dictPath = Path.Combine(configFolder, DictionaryPath);
                        string outputPath = Path.Combine(configFolder, OutputPath);

                        manager.GenerateDictionaryAsync(device, dictPath).Wait();
                        manager.BuildLanguagePacksAsync(dictPath, outputPath).Wait();

                        // 保存JSON文件
                        string jsonPath = Path.Combine(configFolder, "Config.json");
                        JsonFileHelper.SaveJson(jsonPath, device);
                        AddMessage($"成功保存JSON文件: {jsonPath}\n");

                        AddMessage($"配置文件夹创建成功: {configFolder}\n");
                    }
                    catch (Exception ex)
                    {
                        AddMessage($"保存配置时发生错误: {ex.Message}\n");
                    }
                }
            }
        }
        private void butSaveExcel_Click(object sender, EventArgs e)
        {
            if (device == null)
            {
                AddMessage("请先加载配置文件\n");
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Xlsx格式|*.xlsx";
            saveFileDialog.FileName = "Config";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;
                try
                {
                    XlsxHelp.SaveXlsx(filePath, device);
                    AddMessage($"成功保存Excel文件: {filePath}\n");
                }
                catch (Exception ex)
                {
                    AddMessage($"保存Excel文件时发生错误: {ex.Message}\n");
                }
            }
        }
        public void AddMessage(string message)
        {
            txtMessage.AppendText(message + "\r\n");
        }

        private void but_SaveNewCsv_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Xlsx格式|*.xlsx";
            saveFileDialog.FileName = "Config";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;
                try
                {
                    XlsxHelp.SaveXlsx(filePath, new());
                    AddMessage($"成功生成Excel文件: {filePath}\n");
                }
                catch (Exception ex)
                {
                    AddMessage($"生成Excel文件时发生错误: {ex.Message}\n");
                }
            }
        }

        private void but_OpenJsonFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON文件|*.json";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                try
                {
                    device = JsonFileHelper.ReadJson<Device>(filePath);
                    AddMessage($"成功读取配置文件: {filePath}\n");
                    AddDeviceMessage(device);
                }
                catch (Exception ex)
                {
                    AddMessage($"读取文件时发生错误: {ex.Message}\n");
                }
            }
        }
        private void AddDeviceMessage(Device device)
        {
            AddMessage($"设备名称: {device.DeviceMessage.DeviceName}");
            AddMessage($"设备类型: {device.DeviceMessage.DeviceType.ToString()}");
            AddMessage($"设备地址: {device.DeviceMessage.DeviceAddress}");
            AddMessage($"输入IO数量: {device.IoConfig.InputIoList.Count}");
            AddMessage($"输出IO数量: {device.IoConfig.OutputIoList.Count}");
            AddMessage($"气缸数量: {device.CylindersConfig.CylinderList.Count}");
            AddMessage($"轴数量: {device.AxesConfig.AxisList.Count}");
            AddMessage($"报警数量: {device.AlarmsConfig.AlarmList.Count}");
            AddMessage($"参数数量: {device.ParametersConfig.ParameterList.Count}");
            AddMessage($"循环读取数量: {device.CyclicReadConfig.CyclicReadList.Count}");
        }

        private void but_OnlyJson_Click(object sender, EventArgs e)
        {
            if (device == null)
            {
                AddMessage("请先加载配置文件\n");
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON格式|*.json";
            saveFileDialog.FileName = "Config";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;
                string configFolder = Path.GetDirectoryName(filePath)!;
                try
                {
                    // 生成字典和语言包
                    string dictPath = Path.Combine(configFolder, DictionaryPath);
                    string outputPath = Path.Combine(configFolder, OutputPath);

                    manager.GenerateDictionaryAsync(device, dictPath).Wait();
                    manager.BuildLanguagePacksAsync(dictPath, outputPath).Wait();

                    JsonFileHelper.SaveJson(filePath, device);
                    AddMessage($"成功保存JSON文件: {filePath}\n");
                }
                catch (Exception ex)
                {
                    AddMessage($"保存JSON文件时发生错误: {ex.Message}\n");
                }
            }
        }

        private void but_SaveLanguage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON文件|*.json";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                string configFolder = Path.GetDirectoryName(filePath)!;
                try
                {
                    device = JsonFileHelper.ReadJson<Device>(filePath);
                    // 生成字典和语言包
                    string dictPath = Path.Combine(configFolder, DictionaryPath);
                    string outputPath = Path.Combine(configFolder, OutputPath);

                    manager.GenerateDictionaryAsync(device, dictPath).Wait();
                    manager.BuildLanguagePacksAsync(dictPath, outputPath).Wait();
                    AddMessage($"生成语言包成功: {outputPath}\n");
                }
                catch (Exception ex)
                {
                    AddMessage($"生成语言包时发生错误: {ex.Message}\n");
                }
            }
        }
    }
}
