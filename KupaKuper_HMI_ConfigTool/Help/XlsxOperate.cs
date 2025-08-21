using ClosedXML.Excel;
namespace KupaKuper_HMI_ConfigTool.Help
{
    public class XlsxOperate
    {
        private XLWorkbook package;
        public XlsxOperate(string path)
        {
            package = new (path);
        }
        
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

        public static void WriteXlsx(Dictionary<string, List<string[]>> DeviceData, string path)
        {
            string ConfigTemplate = @".\ConfigTemplate.xlsx";
            
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
    }
}
