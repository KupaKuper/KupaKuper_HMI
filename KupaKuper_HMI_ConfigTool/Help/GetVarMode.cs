using DocumentFormat.OpenXml.Vml.Spreadsheet;

using KupaKuper_HMI_Config.DeviceConfig.BaseType;

namespace KupaKuper_HMI_ConfigTool.Help
{
    public static class GetVarMode
    {
        public static string VarFirstName = string.Empty;
        public static Dictionary<string, string>? DicVarAddress;
        public static WriteReadVariable ToWR(PlcModel plcModel, string varinfo, string VarName, VarMode varMode)
        {
            PlcVar plcVar = GetVar(plcModel, varinfo, VarName, varMode);
            WriteReadVariable Var = new();
            Var.PlcVarName = plcVar.PlcVarName;
            Var.PlcVarMode = plcVar.PlcVarMode;
            Var.PlcVarAddress = plcVar.PlcVarAddress;
            Var.VarInfo = plcVar.VarInfo;
            return Var;
        }
        public static WriteOnlyVariable ToWO(PlcModel plcModel, string varinfo, string VarName, VarMode varMode)
        {
            PlcVar plcVar = GetVar(plcModel, varinfo, VarName, varMode);
            WriteOnlyVariable Var = new();
            Var.PlcVarName = plcVar.PlcVarName;
            Var.PlcVarMode = plcVar.PlcVarMode;
            Var.PlcVarAddress = plcVar.PlcVarAddress;
            Var.VarInfo = plcVar.VarInfo;
            return Var;
        }
        public static ReadOnlyVariable ToRO(PlcModel plcModel, string varinfo, string VarName, VarMode varMode)
        {
            PlcVar plcVar = GetVar(plcModel, varinfo, VarName, varMode);
            ReadOnlyVariable Var = new();
            Var.PlcVarName = plcVar.PlcVarName;
            Var.PlcVarMode = plcVar.PlcVarMode;
            Var.PlcVarAddress = plcVar.PlcVarAddress;
            Var.VarInfo = plcVar.VarInfo;
            return Var;
        }
        public static PlcVar GetVar(PlcModel plcModel, string varinfo, string VarName, VarMode varMode)
        {
            switch (plcModel)
            {
                case PlcModel.ModbusTcp:
                    if (DicVarAddress == null)
                    {
                        throw new ArgumentNullException(nameof(DicVarAddress), "DicVarAddress cannot be null for ModbusTcp model.");
                    }
                    return GetVar(DicVarAddress, varinfo, VarName, varMode);
                case PlcModel.OpcUa:
                    return GetVar(VarFirstName, varinfo, VarName, varMode);
                default:
                    throw new ArgumentException($"Unsupported PLC model: {plcModel}", nameof(plcModel));
            }
        }

        private static PlcVar GetVar(string VarFirstName, string varinfo, string VarName, VarMode varMode)
        {
            return new()
            {
                VarInfo = varinfo,
                PlcVarName = VarName,
                PlcVarAddress = VarFirstName + VarName,
                PlcVarMode = varMode
            };
        }

        private static PlcVar GetVar(Dictionary<string, string> DicVarAddress, string varinfo, string VarName, VarMode varMode)
        {
            if (!DicVarAddress.TryGetValue(VarName, out string? VarFirstName))
            {
                VarFirstName = string.Empty;
            }
            return new()
            {
                VarInfo = varinfo,
                PlcVarName = VarName,
                PlcVarAddress = VarFirstName,
                PlcVarMode = varMode
            };
        }

        /// <summary>
        /// 使用文件选择器打开 CSV 文件并读取键值对填充到 DicVarAddress
        /// </summary>
        /// <returns>操作成功返回 true，失败返回 false</returns>
        public static bool LoadCsvToDicVarAddress()
        {
            // 创建文件选择器实例
            OpenFileDialog openFileDialog = new()
            {
                Filter = "CSV 文件 (*.csv)|*.csv",
                Title = "选择 CSV 文件"
            };

            // 显示文件选择对话框
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // 初始化字典
                    DicVarAddress = new Dictionary<string, string>();

                    // 读取 CSV 文件内容
                    string[] lines = System.IO.File.ReadAllLines(openFileDialog.FileName);

                    // 逐行解析 CSV 文件
                    foreach (string line in lines)
                    {
                        // 按逗号分割每行内容
                        string[] parts = line.Split(',');
                        if (parts.Length >= 2)
                        {
                            // 去除前后空白字符后添加到字典
                            string key = parts[0].Trim();
                            string value = parts[1].Trim();
                            DicVarAddress[key] = value;
                        }
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    // 可根据需求添加日志记录或错误提示
                    System.Diagnostics.Debug.WriteLine("读取 CSV 文件时出错: " + ex.Message);
                    return false;
                }
            }

            return false;
        }
        /// <summary>
        /// 参数类型显示互换
        /// </summary>
        /// <param name="ParameterMode"></param>
        /// <returns></returns>
        public static string GetParameterMode(string ParameterMode)
        {
            switch (ParameterMode)
            {
                case "默认":
                    return "None";
                case "复归型":
                    return "Revert";
                case "置位型":
                    return "Position";
                case "选择型":
                    return "Choose";
                case "None":
                    return "默认";
                case "Revert":
                    return "复归型";
                case "Position":
                    return "置位型";
                case "Choose":
                    return "选择型";
            }
            return ParameterMode;
        }
        public static string GetPopupsMode(string PopupsMode)
        {
            switch (PopupsMode)
            {
                case "Display":
                    return "确认弹窗";
                case "Confirmation":
                    return "判断弹窗";
                case "Input":
                    return "输入弹窗";
                case "Choose":
                    return "选择弹窗";
                case "script":
                    return "脚本";
                case "确认弹窗":
                    return "Display";
                case "判断弹窗":
                    return "Confirmation";
                case "输入弹窗":
                    return "Input";
                case "选择弹窗":
                    return "Choose";
                case "脚本":
                    return "script";
            }
            return PopupsMode;
        }
    }
    public static class GetVarMode<T> where T : struct
    {
        public static WriteReadVariable<T> ToWR(PlcModel plcModel, string varinfo, string VarName, VarMode varMode)
        {
            PlcVar plcVar = GetVarMode.GetVar(plcModel, varinfo, VarName, varMode);
            WriteReadVariable<T> Var = new();
            Var.PlcVarName = plcVar.PlcVarName;
            Var.PlcVarMode = plcVar.PlcVarMode;
            Var.PlcVarAddress = plcVar.PlcVarAddress;
            Var.VarInfo = plcVar.VarInfo;
            return Var;
        }
        public static ReadOnlyVariable<T> ToRO(PlcModel plcModel, string varinfo, string VarName, VarMode varMode)
        {
            PlcVar plcVar = GetVarMode.GetVar(plcModel, varinfo, VarName, varMode);
            ReadOnlyVariable<T> Var = new();
            Var.PlcVarName = plcVar.PlcVarName;
            Var.PlcVarMode = plcVar.PlcVarMode;
            Var.PlcVarAddress = plcVar.PlcVarAddress;
            Var.VarInfo = plcVar.VarInfo;
            return Var;
        }
    }

}
