using KupaKuper_DeviceSever.Server;

using System.Diagnostics;

using WinFromFrame_KupaKuper.Modes;

namespace WinFromFrame_KupaKuper.ViewModes
{
    public class AlarmDataViewMode:BaseViewMode
    {
        private IDeviceSystemServer _Server;
        /// <summary>
        /// 当前显示的日志条目
        /// </summary>
        public List<AlarmTableData>? logEntries = new();
        /// <summary>
        /// 当前显示的表头条目
        /// </summary>
        public AlarmTableData? logEntriesHeaders;
        /// <summary>
        /// 当前文件的所有数据条目
        /// </summary>
        public (AlarmTableData?, List<AlarmTableData>) LogDatas = new(null, new());

        public override required Action UpdataView { get; set; }

        public AlarmDataViewMode(IDeviceSystemServer _Server) : base(_Server)
        {
            this._Server = _Server;
        }
        /// <summary>
        /// 选择的日期
        /// </summary>
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set
            {
                _selectedDate = value;
                LoadLogForSelectedDate();
            }
        }
        private DateTime _selectedDate = DateTime.Now;

        public string searchText = "";

        public override void OnInitialized()
        {
            base.OnInitialized();
            LoadLogForSelectedDate();
        }

        /// <summary>
        /// 加载选定日期的日志文件
        /// </summary>
        public void LoadLogForSelectedDate()
        {
            try
            {
                string[]? logtxt = _Server.CurrentDevice.ReadAlarmLogAsync(_Server.Language, SelectedDate).Result;
                if (logtxt == null) return;
                SetAlarmData(logtxt);
                UpdataView();
            }
            catch (Exception ex)
            {
                _Server.CurrentDevice.AddDeviceLogItem($"加载日志文件错误: {ex.Message}");
            }
        }

        public override void LanguageChanged(string language)
        {
            LoadLogForSelectedDate();
        }

        /// <summary>
        /// 离开页面时需要关闭的请求等
        /// </summary>
        public override void RemovePageQuest()
        {
            base.RemovePageQuest();
        }
        /// <summary>
        /// 读取报警log内容并解析成报警数据模型
        /// </summary>
        /// <param name="logtxt"></param>
        public void SetAlarmData(string[] logtxt)
        {
            if (logtxt.Length < 1) {logEntries = null; return; }
            LogDatas = ParseAlarmLog(logtxt);
            logEntriesHeaders = LogDatas.Item1;
            logEntries = LogDatas.Item2;
        }
        /// <summary>
        /// 解析报警log为报警类型
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public (AlarmTableData?, List<AlarmTableData>) ParseAlarmLog(string[] log)
        {
            List<AlarmTableData> alarmModes = new();

            if (log.Length < 1) return (null, alarmModes);

            // 解析表头
            var strings = log[0].Split(',');
            AlarmTableData headers = new("header", strings[0], strings[1], strings[2], strings[3], strings[4], strings[5], strings[6]);

            // 解析报警数据
            for (int i = 1; i < log.Length; i++)
            {
                var fields = log[i].Split(',');
                if (fields.Length < 7 || fields[5] != false.ToString())
                    continue;

                alarmModes.Insert(0, new AlarmTableData
                (
                    $"alarm{i}",
                    fields[0],
                    fields[1],
                    fields[2],
                    fields[3],
                    fields[4],
                    fields[5],
                    fields[6]
                ));
            }

            return (headers, alarmModes);
        }

        public void SearchCurrentData(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                // 搜索文本为空时显示所有数据
                logEntries = LogDatas.Item2?.ToList();
                return;
            }

            // 使用逗号分割搜索文本，得到关键词组（OR关系）
            var keywordGroups = searchText.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                                         .Select(group => group.Trim())
                                         .Where(group => !string.IsNullOrWhiteSpace(group))
                                         .ToList();

            // 如果没有有效关键词组，显示所有数据
            if (!keywordGroups.Any())
            {
                logEntries = LogDatas.Item2?.ToList();
                return;
            }

            // 对每个关键词组进行处理
            logEntries = LogDatas.Item2?.Where(entry =>
                // 匹配任意一个关键词组（OR关系）
                keywordGroups.Any(group =>
                {
                    // 将关键词组按空格分割，得到需要同时匹配的关键词（AND关系）
                    var andKeywords = group.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                                         .Select(k => k.ToLowerInvariant())
                                         .ToList();

                    // 如果关键词组为空，跳过
                    if (andKeywords.Count == 0)
                        return false;

                    // 检查该条目中是否包含该关键词组的所有关键词（AND关系）
                    return andKeywords.All(keyword =>
                        entry.Number.Contains(keyword, StringComparison.InvariantCultureIgnoreCase) ||
                        entry.AlarmId.Contains(keyword, StringComparison.InvariantCultureIgnoreCase) ||
                        entry.StationText.Contains(keyword, StringComparison.InvariantCultureIgnoreCase) ||
                        entry.AlarmType.Contains(keyword, StringComparison.InvariantCultureIgnoreCase) ||
                        entry.AlarmText.Contains(keyword, StringComparison.InvariantCultureIgnoreCase) ||
                        entry.TriggerTime.Contains(keyword, StringComparison.InvariantCultureIgnoreCase)
                    );
                })
            ).ToList();
        }
    }
}
