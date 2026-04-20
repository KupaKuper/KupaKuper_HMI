using ClosedXML.Excel;
using KupaKuper_HMI_Config.DeviceConfig;
using System.IO;

namespace KupaKuper_HMI_ConfigTool.Help
{
    /// <summary>
    /// Excel文件操作类，用于读取和写入Excel文件
    /// </summary>
    public class XlsxOperate
    {
        /// <summary>
        /// Excel工作簿对象
        /// </summary>
        private XLWorkbook package;

        /// <summary>
        /// 构造函数，初始化Excel操作对象
        /// </summary>
        /// <param name="path">Excel文件路径</param>
        public XlsxOperate(string path)
        {
            package = new (path);
        }
        
        /// <summary>
        /// 获取指定工作表的数据
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>工作表数据，每行数据为一个字符串数组</returns>
        public List<string[]> GetXlsxSheetData(string sheetName)
        {
            List<string[]> rows = new List<string[]>();
            
            // 获取指定工作表
            var worksheet = package.Worksheets.FirstOrDefault(ws => ws.Name == sheetName);
            
            if (worksheet != null)
            {
                // 获取工作表的行数和列数
                int rowCount = worksheet.LastRowUsed()!.RowNumber();
                int colCount = worksheet.LastColumnUsed()!.ColumnNumber();
                
                // 遍历所有行
                for (int row = 1; row <= rowCount; row++)
                {
                    string[] rowData = new string[colCount];
                    
                    // 遍历所有列
                    for (int col = 1; col <= colCount; col++)
                    {
                        // 获取单元格值并转换为字符串
                        rowData[col - 1] = worksheet.Cell(row, col).GetString();
                    }
                    
                    rows.Add(rowData);
                }
            }
            return rows;
        }

        /// <summary>
        /// 将设备数据写入Excel文件
        /// </summary>
        /// <param name="DeviceData">设备数据，键为工作表名称，值为工作表数据</param>
        /// <param name="path">保存路径</param>
        public static void WriteXlsx(Dictionary<string, List<string[]>> DeviceData, string path)
        {
            string ConfigTemplate = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "KupaKuper_HMI", "ConfigTemplate.xlsx");
            
            // 从模板创建新工作簿
            using (var workbook = new XLWorkbook(ConfigTemplate))
            {
                // 遍历每个工作表数据
                foreach (var sheetData in DeviceData)
                {
                    string sheetName = sheetData.Key;
                    List<string[]> rows = sheetData.Value;
                    
                    // 获取或创建工作表
                    IXLWorksheet worksheet;
                    if (workbook.Worksheets.Contains(sheetName))
                    {
                        worksheet = workbook.Worksheets.Worksheet(sheetName);
                    }
                    else
                    {
                        worksheet = workbook.Worksheets.Add(sheetName);
                    }
                    
                    // 写入数据
                    for (int row = 0; row < rows.Count; row++)
                    {
                        string[] rowData = rows[row];
                        for (int col = 0; col < rowData.Length; col++)
                        {
                            worksheet.Cell(row + 1, col + 1).Value = rowData[col];
                        }
                    }
                }
                
                // 保存到指定路径（允许覆盖）
                workbook.SaveAs(path);
            }
        }

        /// <summary>
        /// 复制模板文件到指定地址
        /// </summary>
        /// <param name="destinationPath">目标路径</param>
        /// <param name="overwrite">是否覆盖已存在的文件</param>
        /// <exception cref="FileNotFoundException">当模板文件不存在时抛出</exception>
        /// <exception cref="IOException">当复制失败时抛出</exception>
        public static void CopyTemplateFile(string destinationPath, bool overwrite = true)
        {
            string ConfigTemplate = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "KupaKuper_HMI", "ConfigTemplate.xlsx");

            // 检查模板文件是否存在
            if (!File.Exists(ConfigTemplate))
            {
                throw new FileNotFoundException($"模板文件不存在: {ConfigTemplate}");
            }

            // 确保目标目录存在
            string destinationDirectory = Path.GetDirectoryName(destinationPath);
            if (!string.IsNullOrEmpty(destinationDirectory) && !Directory.Exists(destinationDirectory))
            {
                Directory.CreateDirectory(destinationDirectory);
            }

            // 复制文件
            try
            {
                File.Copy(ConfigTemplate, destinationPath, overwrite);
            }
            catch (Exception ex)
            {
                throw new IOException($"复制模板文件失败: {ex.Message}", ex);
            }
        }
    }
}
