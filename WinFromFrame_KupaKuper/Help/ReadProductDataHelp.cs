using KupaKuper_EthernetWrapper.Help;

using WinFromFrame_KupaKuper.Modes;

namespace WinFromFrame_KupaKuper.Help
{
    public static class ReadProductDataHelp
    {
        /// <summary>
        /// 读取UPH数据(专用)
        /// </summary>
        /// <param name="csvPath"></param>
        /// <returns></returns>
        public static DayProductionData? GetUphData(string csvPath)
        {
            DayProductionData dayProductionData = new();
            List<ProductionData> newProductionDatas = new();
            if (!File.Exists(csvPath))
            {
                // 处理文件不存在的情况
                return null;
            }

            try
            {
                var lines = File.ReadAllLines(csvPath, EncodingHelp.GetEncoding("GBK"));
                if (lines.Length <= 1) return null; // 跳过表头

                foreach (var line in lines.Skip(1))
                {
                    var parts = line.Split(',');
                    if (parts.Length != 6) continue;

                    if (decimal.TryParse(parts[1], out var capacity) &&
                        int.TryParse(parts[2], out var okCount) &&
                        int.TryParse(parts[3], out var ngCount) &&
                        double.TryParse(parts[4], out var yieldValue))
                    {
                        var dateTime = parts[0];
                        if (dateTime == "总计")
                        {
                            dayProductionData.DateTime = csvPath.Split('\\').Last().Replace(".csv", "");
                            dayProductionData.SumCapacity = capacity;
                            dayProductionData.SumOKCount = okCount;
                            dayProductionData.SumNGCount = ngCount;
                            dayProductionData.SumYield = yieldValue;
                        }
                        else
                        {
                            newProductionDatas.Add(new ProductionData
                            {
                                DateTime = dateTime,
                                Capacity = capacity,
                                OKCount = okCount,
                                NGCount = ngCount,
                                Yield = yieldValue,
                                Shift = bool.TryParse(parts[5], out var shift) ? shift : null
                            });
                        }
                    }
                }
                dayProductionData.ProductionDatas = newProductionDatas;
            }
            catch (OperationCanceledException)
            {
                return null; // 忽略取消操作异常
            }
            return dayProductionData;
        }

        /// <summary>
        /// 读取csv时的转换失败处理策略
        /// </summary>
        public enum ReservedCategories
        {
            /// <summary>
            /// 跳过转换失败的属性，继续处理其他属性
            /// </summary>
            Attribute,
            /// <summary>
            /// 跳过转换失败的整行数据
            /// </summary>
            Row,
            /// <summary>
            /// 只要有转换失败就终止读取，返回false
            /// </summary>
            Node
        }
        /// <summary>
        /// 从CSV文件读取数据并转换为指定类型的列表
        /// </summary>
        /// <typeparam name="T">目标数据模型类型</typeparam>
        /// <param name="filePath">CSV文件路径</param>
        /// <param name="separator">字段分隔符，默认为逗号(',')</param>
        /// <param name="encoding">文件编码，默认为GBK</param>
        /// <param name="reservedCategory">数据转换失败处理策略，默认为Attribute</param>
        /// <returns>转换后的对象列表</returns>
        /// <exception cref="FileNotFoundException">当指定文件不存在时抛出</exception>
        /// <exception cref="ArgumentException">当文件路径为空或无效时抛出</exception>
        public static List<T> ReadCsv<T>(string csvPath, char separator = ',', ReservedCategories reserved = ReservedCategories.Attribute) where T : class, new()
        {
            var (_, data) = ReadCsvWithHeaders<T>(csvPath, separator, reserved);
            return data;
        }
        /// <summary>
        /// 从CSV文件读取数据，返回包含表头和转换后数据的元组
        /// </summary>
        /// <typeparam name="T">目标数据模型类型</typeparam>
        /// <param name="filePath">CSV文件路径</param>
        /// <param name="separator">字段分隔符，默认为逗号(',')</param>
        /// <param name="encoding">文件编码，默认为GBK</param>
        /// <param name="reservedCategory">数据转换失败处理策略，默认为Attribute</param>
        /// <returns>包含表头列表和数据列表的元组</returns>
        /// <exception cref="FileNotFoundException">当指定文件不存在时抛出</exception>
        /// <exception cref="ArgumentException">当文件路径为空或无效时抛出</exception>
        public static (List<string> Headers, List<T> Data) ReadCsvWithHeaders<T>(string csvPath, char separator = ',', ReservedCategories reserved = ReservedCategories.Attribute) where T : class, new()
        {
            var headers = new List<string>();
            var dataList = new List<T>();

            if (!File.Exists(csvPath))
            {
                return (headers, dataList);
            }

            try
            {
                var lines = File.ReadAllLines(csvPath, EncodingHelp.GetEncoding("GBK"));
                if (lines.Length == 0) return (headers, dataList);

                // 解析表头
                headers = lines[0].Split(separator).Select(h => h.Trim()).ToList();
                if (lines.Length <= 1) return (headers, dataList);

                // 获取目标类型的属性信息
                var properties = typeof(T).GetProperties();

                // 处理数据行
                foreach (var line in lines.Skip(1))
                {
                    var parts = line.Split(separator).Select(p => p.Trim()).ToArray();
                    var item = new T();
                    bool isRowValid = true;

                    for (int i = 0; i < headers.Count && i < parts.Length; i++)
                    {
                        var property = properties.FirstOrDefault(p =>
                            string.Equals(p.Name, headers[i], StringComparison.OrdinalIgnoreCase));

                        if (property != null && !string.IsNullOrEmpty(parts[i]))
                        {
                            try
                            {
                                // 尝试转换数据类型
                                var value = Convert.ChangeType(parts[i], property.PropertyType);
                                property.SetValue(item, value);
                            }
                            catch
                            {
                                switch (reserved)
                                {
                                    case ReservedCategories.Attribute:
                                        // 跳过转换失败的属性
                                        break;
                                    case ReservedCategories.Row:
                                        // 标记当前行为无效
                                        isRowValid = false;
                                        break;
                                    case ReservedCategories.Node:
                                        // 只要有转换失败就终止并返回
                                        return (headers, new List<T>());
                                    default:
                                        break;
                                }
                            }
                        }
                    }

                    if (isRowValid)
                    {
                        dataList.Add(item);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // 忽略取消操作异常
            }
            catch (Exception)
            {
                if (reserved == ReservedCategories.Node)
                {
                    return (headers, new List<T>());
                }
            }

            return (headers, dataList);
        }
        /// <summary>
        /// 安全地从CSV文件读取数据，通过out参数返回结果并捕获异常
        /// </summary>
        /// <typeparam name="T">目标数据模型类型</typeparam>
        /// <param name="filePath">CSV文件路径</param>
        /// <param name="result">输出参数，包含表头列表和数据列表的元组</param>
        /// <param name="errorMessage">输出参数，当读取失败时返回错误信息</param>
        /// <param name="separator">字段分隔符，默认为逗号(',')</param>
        /// <param name="encoding">文件编码，默认为GBK</param>
        /// <param name="reservedCategory">数据转换失败处理策略，默认为Attribute</param>
        /// <returns>读取成功返回true，失败返回false</returns>
        public static bool TryReadCsvWithHeaders<T>(string csvPath, out List<string> headers, out List<T> data, char separator = ',', ReservedCategories reserved = ReservedCategories.Attribute) where T : class, new()
        {
            headers = new List<string>();
            data = new List<T>();

            try
            {
                var result = ReadCsvWithHeaders<T>(csvPath, separator, reserved);
                headers = result.Headers;
                data = result.Data;
                return reserved != ReservedCategories.Node || data.Any();
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 异步从CSV文件读取数据，返回包含表头和转换后数据的元组
        /// </summary>
        /// <typeparam name="T">目标数据模型类型</typeparam>
        /// <param name="filePath">CSV文件路径</param>
        /// <param name="separator">字段分隔符，默认为逗号(',')</param>
        /// <param name="encoding">文件编码，默认为GBK</param>
        /// <param name="reservedCategory">数据转换失败处理策略，默认为Attribute</param>
        /// <returns>包含表头列表和数据列表的元组的异步任务</returns>
        /// <exception cref="FileNotFoundException">当指定文件不存在时抛出</exception>
        /// <exception cref="ArgumentException">当文件路径为空或无效时抛出</exception>
        /// <exception cref="OperationCanceledException">当异步操作被取消时抛出</exception>
        public static async Task<(List<string> Headers, List<T> Data)> AsyncReadCsvWithHeaders<T>(string csvPath, char separator = ',', ReservedCategories reserved = ReservedCategories.Attribute) where T : class, new()
        {
            var headers = new List<string>();
            var dataList = new List<T>();

            if (!File.Exists(csvPath))
            {
                return (headers, dataList);
            }

            try
            {
                var lines = await File.ReadAllLinesAsync(csvPath, EncodingHelp.GetEncoding("GBK")).ConfigureAwait(false);
                if (lines.Length == 0) return (headers, dataList);

                // 解析表头
                headers = lines[0].Split(separator).Select(h => h.Trim()).ToList();
                if (lines.Length <= 1) return (headers, dataList);

                // 获取目标类型的属性信息
                var properties = typeof(T).GetProperties();

                // 处理数据行
                foreach (var line in lines.Skip(1))
                {
                    var parts = line.Split(separator).Select(p => p.Trim()).ToArray();
                    var item = new T();
                    bool isRowValid = true;

                    for (int i = 0; i < headers.Count && i < parts.Length; i++)
                    {
                        var property = properties.FirstOrDefault(p =>
                            string.Equals(p.Name, headers[i], StringComparison.OrdinalIgnoreCase));

                        if (property != null && !string.IsNullOrEmpty(parts[i]))
                        {
                            try
                            {
                                // 尝试转换数据类型
                                var value = Convert.ChangeType(parts[i], property.PropertyType);
                                property.SetValue(item, value);
                            }
                            catch
                            {
                                switch (reserved)
                                {
                                    case ReservedCategories.Attribute:
                                        // 跳过转换失败的属性
                                        break;
                                    case ReservedCategories.Row:
                                        // 标记当前行为无效
                                        isRowValid = false;
                                        break;
                                    case ReservedCategories.Node:
                                        // 只要有转换失败就终止并返回
                                        return (headers, new List<T>());
                                    default:
                                        break;
                                }
                            }
                        }
                    }

                    if (isRowValid)
                    {
                        dataList.Add(item);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // 忽略取消操作异常
            }
            catch (Exception)
            {
                if (reserved == ReservedCategories.Node)
                {
                    return (headers, new List<T>());
                }
            }

            return (headers, dataList);
        }
    }
}
