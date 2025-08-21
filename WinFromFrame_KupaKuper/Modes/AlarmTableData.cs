namespace WinFromFrame_KupaKuper.Modes
{
    /// <summary>
    /// 报警表格数据结构
    /// </summary>
    /// <param name="Number"></param>
    /// <param name="AlarmId"></param>
    /// <param name="AlarmType"></param>
    /// <param name="AlarmText"></param>
    /// <param name="StationText"></param>
    /// <param name="TriggerTime"></param>
    /// <param name="AlarmValue"></param>
    /// <param name="AlarmTime"></param>
    public record AlarmTableData
    (
        string Number,
        string AlarmId,
        string AlarmType,
        string AlarmText,
        string StationText,
        string TriggerTime,
        string AlarmValue,
        string AlarmTime
    );

    /// <summary>
    /// 报警数量统计结构
    /// </summary>
    /// <param name="Alarm"></param>
    /// <param name="Info"></param>
    public record AlarmNumber
    (
        string Time,
        int Alarm,
        int Info
    );

    /// <summary>
    /// 报警统计结构
    /// </summary>
    /// <param name="DicAlarm"></param>
    /// <param name="DicInfo"></param>
    public record AlarmStatistics
    (
        Dictionary<string,int> DicAlarm,
        Dictionary<string,int> DicInfo
    );
}
