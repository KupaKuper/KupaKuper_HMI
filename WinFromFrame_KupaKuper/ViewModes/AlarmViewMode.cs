using KupaKuper_DeviceSever.Server;

using KupaKuper_HMI_Config.DeviceConfig.BaseType;

using System.Collections.ObjectModel;

using WinFromFrame_KupaKuper.Modes;

namespace WinFromFrame_KupaKuper.ViewModes
{
    public class AlarmViewMode : BaseViewMode
    {
        private IDeviceSystemServer _Server;
        public AlarmViewMode(IDeviceSystemServer _Server) : base(_Server)
        {
            this._Server = _Server;
        }

        public override required Action UpdataView { get; set; }
        /// <summary>
        /// 当前报警项
        /// </summary>
        public ObservableCollection<Alarm> alarmModes = new();
        /// <summary>
        /// 当天的报警数量统计
        /// </summary>
        public List<AlarmNumber> AlarmNumberData = new();
        /// <summary>
        /// 当天的报警统计
        /// </summary>
        public AlarmStatistics AlarmStatisticsData { get; set; }
        /// <summary>
        /// 报警统计排序
        /// </summary>
        public List<(string,int)> AlarmValues=new();    

        public override void OnInitialized()
        {
            base.OnInitialized();
        }
        public override void _Server_DeviceChanged()
        {
            base._Server_DeviceChanged();
            alarmModes = _Server.CurrentDevice.CurrentAlarmModes;
            _Server.CurrentDevice.AlarmTrigger += UpdataAlarm;
            LoadLogForSelectedDate();
            UpdataView();
        }
        public override void _Server_BeforeDeviceChanged()
        {
            base._Server_BeforeDeviceChanged();
            _Server.CurrentDevice.AlarmTrigger -= UpdataAlarm;
        }
        public override void Dispose()
        {
            _Server.CurrentDevice.AlarmTrigger -= UpdataAlarm;
            base.Dispose();
        }

        public override void LanguageChanged(string language)
        {
            base.LanguageChanged(language);
            LoadLogForSelectedDate();
        }

        private void UpdataAlarm(string arg1, Alarm mode)
        {
            UpdataView();
        }

        /// <summary>
        /// 加载选定日期的日志文件
        /// </summary>
        public void LoadLogForSelectedDate()
        {
            try
            {
                string[]? logtxt = _Server.CurrentDevice.ReadAlarmLogAsync(_Server.Language, DateTime.Today).Result;
                if (logtxt == null) return;
                SetAlarmData(logtxt);
            }
            catch (Exception ex)
            {
                _Server.CurrentDevice.AddDeviceLogItem($"加载日志文件错误: {ex.Message}");
            }
        }

        /// <summary>
        /// 读取报警log内容并解析成报警数据模型
        /// </summary>
        /// <param name="logtxt"></param>
        public void SetAlarmData(string[] logtxt)
        {
            var TodyAlarmData = ParseAlarmLog(logtxt);
            AlarmStatisticsData = TodyAlarmData.Item1;
            AlarmNumberData = TodyAlarmData.Item2;
            // 统计报警排序并取前10
            AlarmValues = AlarmStatisticsData.DicAlarm
                .OrderByDescending(x => x.Value)
                .Take(10)
                .Select(x => (x.Key, x.Value))
                .ToList();
        }
        /// <summary>
        /// 解析报警log为报警类型
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public (AlarmStatistics, List<AlarmNumber>) ParseAlarmLog(string[] log)
        {
            var alarmStats = new Dictionary<string, int>();
            var infoStats = new Dictionary<string, int>();
            var hourlyStats = new (int alarms, int infos)[24];

            if (log.Length < 1)
                return (new AlarmStatistics(alarmStats, infoStats), new List<AlarmNumber>());

            Parallel.For(1, log.Length, i =>
            {
                var fields = log[i].Split(',');
                if (fields.Length < 6 || fields[5].ToLower() != "true") 
                    return;

                string alarmId = fields[2];
                bool isAlarm = fields[1].ToLower() == "alarm";
                
                if (DateTime.TryParse(fields[4], out DateTime alarmTime))
                {
                    int hour = alarmTime.Hour;
                    
                    lock (hourlyStats)
                    {
                        if (isAlarm)
                        {
                            hourlyStats[hour].alarms++;
                            
                            lock (alarmStats)
                            {
                                alarmStats.TryGetValue(alarmId, out int count);
                                alarmStats[alarmId] = count + 1;
                            }
                        }
                        else
                        {
                            hourlyStats[hour].infos++;
                            
                            lock (infoStats)
                            {
                                infoStats.TryGetValue(alarmId, out int count);
                                infoStats[alarmId] = count + 1;
                            }
                        }
                    }
                }
            });

            var alarmNumber = hourlyStats
                .Select((x, i) => new AlarmNumber($"{i}-{i+1}", x.alarms, x.infos))
                .ToList();

            return (new AlarmStatistics(alarmStats, infoStats), alarmNumber);
        }

    }
}
