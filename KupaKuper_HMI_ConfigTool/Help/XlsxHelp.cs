using KupaKuper_HMI_Config.DeviceConfig;
using KupaKuper_HMI_Config.DeviceConfig.BaseType;

namespace KupaKuper_HMI_ConfigTool.Help
{
    public static class XlsxHelp
    {
        private static PlcModel plcModel;
        private static string AlarmLogPath = "";
        /// <summary>
        /// 读取指定的xlsx文件转换成Device类型
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        public static Device ReadXlsx(string path)
        {
            ArgumentNullException.ThrowIfNull(path);
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"配置文件不存在: {path}");
            }
            Dictionary<string, List<string[]>> DeviceData = new()
            {
                {"Device",new() },
                {"System",new() },
                {"Io",new() },
                {"Axis",new() },
                {"Cylinder",new() },
                {"Alarm",new() },
                {"Data",new() },
                {"Parameters",new() },
                {"CyclicRead",new() }
            };
            XlsxOperate xlsxOperate = new(path);
            foreach (var item in DeviceData.Keys)
            {
                DeviceData[item] = xlsxOperate.GetXlsxSheetData(item);
            }
            Device newDevice = new()
            {
                DeviceMessage = GetDeviceMessage(DeviceData["Device"]),
                SystemConfig = GetSystemConfig(DeviceData["System"]),
                IoConfig = GetIoLConfig(DeviceData["Io"]),
                AxesConfig = GetAxisConfig(DeviceData["Axis"]),
                CylindersConfig = GetCylinderConfig(DeviceData["Cylinder"]),
                AlarmsConfig = GetAlarmConfig(DeviceData["Alarm"]),
                DataConfig = GetDataConfig(DeviceData["Data"]),
                ParametersConfig = GetParameterListConfig(DeviceData["Parameters"]),
                CyclicReadConfig = GetCyclicReadConfig(DeviceData["CyclicRead"])
            };
            newDevice.AlarmsConfig.AlarmFilePath = AlarmLogPath;
            return newDevice;
        }
        /// <summary>
        /// 将指定Device类型转换成xlsx文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="model"></param>
        public static void SaveXlsx(string path, Device model)
        {
            Dictionary<string, List<string[]>> DeviceData = new()
            {
                {"Device",SetDeviceMessage(model.DeviceMessage) },
                {"System",SetSystemConfig(model.SystemConfig) },
                {"Io",SetIoLConfig(model.IoConfig) },
                {"Axis",SetAxisConfig(model.AxesConfig) },
                {"Cylinder",SetCylinderConfig(model.CylindersConfig) },
                {"Alarm",SetAlarmConfig(model.AlarmsConfig) },
                {"Data",SetDataConfig(model.DataConfig) },
                {"Parameters",SetParameterListConfig(model.ParametersConfig) },
                {"CyclicRead",SetCyclicReadConfig(model.CyclicReadConfig) }
            };
            XlsxOperate.WriteXlsx(DeviceData, path);
        }
        #region 读取xlsx的方法
        private static DeviceMessage GetDeviceMessage(List<string[]> Data)
        {
            DeviceMessage deviceMessage = new();
            deviceMessage.DeviceName = Data[1][1];
            deviceMessage.DeviceType = plcModel = Enum.Parse<PlcModel>(Data[2][1]);
            deviceMessage.DeviceAddress = Data[3][1];
            deviceMessage.DeviceVarFirstName = GetVarMode.VarFirstName = Data[4][1];
            deviceMessage.HeartbeatAddress = GetVarMode<bool>.ToWR(plcModel, Data[5][0], Data[5][1], Mode(Data[5][2]));
            return deviceMessage;
        }
        private static SystemConfig GetSystemConfig(List<string[]> Data)
        {
            SystemConfig systemConfig = new();
            systemConfig.Start = GetVarMode<bool>.ToWR(plcModel,"启动控制变量", Data[1][1], Mode(Data[1][2]));
            systemConfig.Reset = GetVarMode<bool>.ToWR(plcModel, "复位控制变量", Data[2][1], Mode(Data[2][2]));
            systemConfig.Pause = GetVarMode<bool>.ToWR(plcModel, "暂停控制变量", Data[3][1], Mode(Data[3][2]));
            systemConfig.Mode = GetVarMode<Int16>.ToRO(plcModel,"设备状态读取变量", Data[4][1], Mode(Data[4][2]));
            return systemConfig;
        }
        private static IoConfig GetIoLConfig(List<string[]> Data)
        {
            IoConfig ioLConfig = new();
            ioLConfig.InputIoList.Clear();
            ioLConfig.OutputIoList.Clear();
            Data.RemoveAt(0);
            foreach (var item in Data)
            {
                if (item.Length > 1)
                {
                    ioLConfig.InputIoList.Add(new()
                    {
                        Name = new() {DefaultText = item[0] },
                        IoVar = GetVarMode<bool>.ToRO(plcModel,"输入IO点", item[1], Mode("Bool"))
                    });
                }
                if (item.Length > 3)
                {
                    ioLConfig.OutputIoList.Add(new()
                    {
                        Name = new() { DefaultText = item[2] },
                        IoVar = GetVarMode<bool>.ToRO(plcModel,"输出IO点", item[3], Mode("Bool"))
                    });
                }
            }
            return ioLConfig;
        }
        private static AxisConfig GetAxisConfig(List<string[]> Data)
        {
            AxisConfig axisConfig = new();
            axisConfig.AxisList.Clear();
            int index = -1;
            bool IsPosition = false;
            List<(List<string[]>, List<string[]>)> axisdata = new();
            foreach (var item in Data)
            {
                if (item[0].Contains("轴显示名称"))
                {
                    IsPosition = false;
                    axisdata.Add(new(new(), new()));
                    index++;
                }
                if (item[0].Contains("轴点位编号"))
                {
                    IsPosition = true;
                }
                if (IsPosition)
                {
                    axisdata[index].Item2.Add(item);
                }
                else
                {
                    axisdata[index].Item1.Add(item);
                }
            }
            foreach (var item in axisdata)
            {
                List<string[]> axisControl = item.Item1;
                List<string[]> axisPosition = item.Item2;
                axisPosition.RemoveAt(0);
                var newaxis = new Axis()
                {
                    AxisControl = new()
                    {
                        Name = new() { DefaultText = axisControl[0][1] },
                        MoveAbs = GetVarMode.ToWO(plcModel,"绝对定位触发变量", axisControl[0][4], Mode(axisControl[0][5])),
                        MaxVelocity = float.Parse(axisControl[1][1]),
                        MoveRel = GetVarMode.ToWO(plcModel,"相对定位触发变量", axisControl[1][4], Mode(axisControl[1][5])),
                        CurrentPosition = GetVarMode<float>.ToRO(plcModel,"当前位置显示变量", axisControl[2][1], Mode(axisControl[2][2])),
                        RelativePosition = GetVarMode<float>.ToWR(plcModel,"相对定位距离设定变量", axisControl[2][4], Mode(axisControl[2][5])),
                        Power = GetVarMode<bool>.ToRO(plcModel,"轴使能状态变量", axisControl[3][1], Mode(axisControl[3][2])),
                        MoveMemory = GetVarMode.ToWO(plcModel,"到记忆为触发变量", axisControl[3][4], Mode(axisControl[3][5])),
                        Busy = GetVarMode<bool>.ToRO(plcModel,"忙碌状态变量", axisControl[4][1], Mode(axisControl[4][2])),
                        MemoryPosition = GetVarMode<float>.ToRO(plcModel,"记忆位置显示变量", axisControl[4][4], Mode(axisControl[4][5])),
                        OpenPower = GetVarMode.ToWO(plcModel,"使能切换变量", axisControl[5][1], Mode(axisControl[5][2])),
                        Error = GetVarMode<bool>.ToRO(plcModel,"报错状态变量", axisControl[5][4], Mode(axisControl[5][5])),
                        JogP = GetVarMode.ToWO(plcModel,"Jog正触发变量", axisControl[6][1], Mode(axisControl[6][2])),
                        AbsNumber = GetVarMode<Int16>.ToWR(plcModel,"绝对定位编号变量", axisControl[6][4], Mode(axisControl[6][5])),
                        JogN = GetVarMode.ToWO(plcModel,"Jog负触发变量", axisControl[7][1], Mode(axisControl[7][2])),
                        JogVelocity = GetVarMode<float>.ToWR(plcModel,"点动速度设置变量", axisControl[7][4], Mode(axisControl[7][5])),
                        Stop = GetVarMode.ToWO(plcModel,"停止变量", axisControl[8][1], Mode(axisControl[8][2])),
                        PosLimit = GetVarMode<bool>.ToRO(plcModel, "正极限状态变量", axisControl[8][4], Mode(axisControl[8][5])),
                        GoHome = GetVarMode.ToWO(plcModel,"回原点触发变量", axisControl[9][1], Mode(axisControl[9][2])),
                        NegLimit = GetVarMode<bool>.ToRO(plcModel, "负极限状态变量", axisControl[9][4], Mode(axisControl[9][5])),
                        HomeDown = GetVarMode<bool>.ToRO(plcModel,"回原点完成状态变量", axisControl[10][1], Mode(axisControl[10][2])),
                        Origin = GetVarMode<bool>.ToRO(plcModel, "原点状态变量", axisControl[10][4], Mode(axisControl[10][5])),
                        Reset = GetVarMode.ToWO(plcModel,"复位触发变量", axisControl[11][1], Mode(axisControl[11][2])),
                        MovAbsDone = GetVarMode<bool>.ToRO(plcModel, "绝对定位完成状态变量", axisControl[11][4], Mode(axisControl[11][5])),
                        Teach = GetVarMode.ToWO(plcModel,"示教触发变量", axisControl[12][1], Mode(axisControl[12][2])),
                        MovRelDone = GetVarMode<bool>.ToRO(plcModel, "相对定位完成状态变量", axisControl[12][4], Mode(axisControl[12][5]))
                    },
                    ListPosition = new()
                };
                foreach (var p in axisPosition)
                {
                    newaxis.ListPosition.Add(new()
                    {
                        PositionNo = int.Parse(p[0]),
                        PositionVar = GetVarMode<float>.ToWR(plcModel,"点位位置变量", p[1], Mode(p[2])),
                        Name = new() { DefaultText = p[3] },
                        VelocityVar = GetVarMode<float>.ToWR(plcModel,"点位速度变量", p[4], Mode(p[5]))
                    });
                }
                axisConfig.AxisList.Add(newaxis);
            }
            return axisConfig;
        }
        private static CylinderConfig GetCylinderConfig(List<string[]> Data)
        {
            CylinderConfig cylinderConfig = new();
            Data.RemoveAt(0);
            cylinderConfig.CylinderList.Clear();
            foreach (var item in Data)
            {
                cylinderConfig.CylinderList.Add(new()
                {
                    Name = new() { DefaultText = item[0] },
                    Station = new() { DefaultText = item[1] },
                    HomeButtonName = new() { DefaultText = item[2] },
                    WorkButtonName = new() { DefaultText = item[3] },
                    Home = GetVarMode<bool>.ToWR(plcModel,"气缸缩回控制变量", item[4], VarMode.Bool),
                    HomeInput= GetVarMode<bool>.ToRO(plcModel,"气缸缩回磁簧信号变量", item[5], VarMode.Bool),
                    HomeDown= GetVarMode<bool>.ToRO(plcModel, "气缸缩回Done信号变量", item[6], VarMode.Bool),
                    Work = GetVarMode<bool>.ToWR(plcModel,"气缸伸出控制变量", item[7], VarMode.Bool),
                    WorkInput = GetVarMode<bool>.ToRO(plcModel, "气缸伸出磁簧信号变量", item[8], VarMode.Bool),
                    WorkDown = GetVarMode<bool>.ToRO(plcModel,"气缸伸出Done信号变量", item[9], VarMode.Bool),
                    HomeLock= GetVarMode<bool>.ToRO(plcModel, "气缸缩回Lock变量", item[10], VarMode.Bool),
                    WorkLock= GetVarMode<bool>.ToRO(plcModel, "气缸伸出Lock变量", item[11], VarMode.Bool),
                    Error= GetVarMode<bool>.ToRO(plcModel, "气缸报警状态变量", item[12], VarMode.Bool)
                });
            }
            return cylinderConfig;
        }
        private static AlarmConfig GetAlarmConfig(List<string[]> Data)
        {
            AlarmConfig alarmConfig = new();
            Data.RemoveAt(0);
            alarmConfig.AlarmList.Clear();
            alarmConfig.InfoList.Clear();
            foreach (var item in Data)
            {
                if (item[1] == "Alarm")
                {
                    alarmConfig.AlarmList.Add(new()
                    {
                        Trigger = GetVarMode<bool>.ToRO(plcModel,"报警触发变量", item[0], VarMode.Bool),
                        Message = new() { DefaultText = item[2] },
                        Station = new() { DefaultText = item[3] },
                        ErrorId = item[4]
                    });
                }
                if (item[1] == "Info")
                {
                    alarmConfig.InfoList.Add(new()
                    {
                        Trigger = GetVarMode<bool>.ToRO(plcModel, "报警触发变量", item[0], VarMode.Bool),
                        Message = new() { DefaultText = item[2] },
                        Station = new() { DefaultText = item[3] },
                        ErrorId = item[4]
                    });
                }
            }
            return alarmConfig;
        }
        private static DataConfig GetDataConfig(List<string[]> Data)
        {
            DataConfig dataConfig = new();
            dataConfig.ReadCsvAddress.Clear();
            dataConfig.ProductionNumber = GetVarMode.ToRO(plcModel,"设备生产总数量", Data[0][1], Mode(Data[0][2]));
            dataConfig.NgNumber = GetVarMode.ToRO(plcModel,"设备生产NG数量", Data[1][1], Mode(Data[1][2]));
            dataConfig.RunningTime = GetVarMode<bool>.ToRO(plcModel, "设备运行时长", Data[2][1], Mode(Data[2][2]));
            dataConfig.PauseTime = GetVarMode<bool>.ToRO(plcModel, "设备暂停时长", Data[3][1], Mode(Data[3][2]));
            dataConfig.AlarmTime = GetVarMode<bool>.ToRO(plcModel, "设备报警时长", Data[4][1], Mode(Data[4][2]));
            dataConfig.DownTime = GetVarMode<bool>.ToRO(plcModel, "设备待料时长", Data[5][1], Mode(Data[5][2]));
            dataConfig.DailyProductAdr = Data[6][1];
            dataConfig.PictureAdr = Data[7][1];
            AlarmLogPath = Data[8][1];
            for (int i = 9; i < Data.Count; i++)
            {
                dataConfig.ReadCsvAddress.Add(Data[i][1]);
            }
            return dataConfig;
        }
        private static ParametersConfig GetParameterListConfig(List<string[]> Data)
        {
            ParametersConfig parameterListConfig = new();
            Data.RemoveAt(0);
            parameterListConfig.ParameterList.Clear();
            foreach (var item in Data)
            {
                parameterListConfig.ParameterList.Add(new()
                {
                    Name = new() { DefaultText = item[0] },
                    GroupName = new() { DefaultText = item[1] },
                    PlcVar = GetVarMode.ToWR(plcModel,"参数变量", item[2], Mode(item[3])),
                    OperateMode = Enum.Parse<ParameterOperateMode>(GetVarMode.GetParameterMode(item[4])),
                    OperateData= (item[5] != null) ? item[5] : string.Empty
                });
            }
            return parameterListConfig;
        }
        private static CyclicReadConfig GetCyclicReadConfig(List<string[]> Data)
        {
            CyclicReadConfig cyclicReadConfig = new();
            Data.RemoveAt(0);
            cyclicReadConfig.CyclicReadList.Clear();
            foreach (var item in Data)
            {
                cyclicReadConfig.CyclicReadList.Add(new()
                {
                    Key = item[0],
                    PlcVar = GetVarMode.ToWR(plcModel,"常驻变量", item[1], Mode(item[2])),
                    IsPopups = bool.Parse(item[3]),
                    PopupsTriggerCond = item[4]
                });
            }
            return cyclicReadConfig;
        }
        
        private static VarMode Mode(string mode)
        {
            return Enum.Parse<VarMode>(mode);
        }
        #endregion
        #region 写入xlsx的方法
        private static List<string[]> SetDeviceMessage(DeviceMessage Data)
        {
            List<string[]> Write = new()
            {
                new[]{ "参数介绍", "参数内容", "变量类型" },
                new[]{ "设备名称:",Data.DeviceName },
                new[]{"设备通讯类型:",Data.DeviceType.ToString()},
                new[]{ "PLC通讯址:", Data.DeviceAddress},
                new[]{ "变量地址格式(前缀):", Data.DeviceVarFirstName},
                new[]{ "检测通讯连接状态的心跳监测变量:",Data.HeartbeatAddress.PlcVarName,Data.HeartbeatAddress.PlcVarMode.ToString() }
            };
            return Write;
        }
        private static List<string[]> SetSystemConfig(SystemConfig Data)
        {
            List<string[]> Write = new()
            {
                new[]{ "设备控制产数","变量名称","变量类型" },
                new[]{"设备启动变量:",Data.Start.PlcVarName,Data.Start.PlcVarMode.ToString()},
                new[]{"设备复位变量:",Data.Reset.PlcVarName,Data.Reset.PlcVarMode.ToString()},
                new[]{"设备暂停变量:",Data.Pause.PlcVarName,Data.Pause.PlcVarMode.ToString()},
                new[]{"设备状态变量:",Data.Mode.PlcVarName,Data.Mode.PlcVarMode.ToString()}
            };
            return Write;
        }
        private static List<string[]> SetIoLConfig(IoConfig Data)
        {
            List<string[]> Write = new()
            {
                new[]{"输入IO点显示名称","输入IO点变量名","输出IO点显示名称","输出IO点变量名"}
            };
            for (int i = 0; i < Math.Max(Data.InputIoList.Count,Data.OutputIoList.Count); i++)
            {
                string[] strings =
                {
                    i<Data.InputIoList.Count? Data.InputIoList[i].Name.DefaultText:"",
                    i<Data.InputIoList.Count? Data.InputIoList[i].IoVar.PlcVarName:"",
                    i<Data.OutputIoList.Count? Data.OutputIoList[i].Name.DefaultText:"",
                    i<Data.OutputIoList.Count? Data.OutputIoList[i].IoVar.PlcVarName:""
                };
                Write.Add(strings);
            }
            return Write;
        }
        private static List<string[]> SetAxisConfig(AxisConfig Data)
        {
            List<string[]> Write = new();
            foreach (var item in Data.AxisList)
            {
                AxisControl control = item.AxisControl;
                List<AxisPosition> position = item.ListPosition;
                Write.AddRange(new List<string[]>
                {
                    new[]{"轴显示名称:", control.Name.DefaultText,"","轴绝对定位触发变量:", control.MoveAbs.PlcVarName, control.MoveAbs.PlcVarMode.ToString()},
                    new[]{ "轴最大速度:",control.MaxVelocity.ToString(),"", "轴相对定位触发变量:",control.MoveRel.PlcVarName,control.MoveRel.PlcVarMode.ToString() },
                    new[]{"轴当前位置变量:",control.CurrentPosition.PlcVarName,control.CurrentPosition.PlcVarMode.ToString(),"轴相对定位距离设定变量:",control.RelativePosition.PlcVarName,control.RelativePosition.PlcVarMode.ToString()},
                    new[]{"轴使能状态变量:",control.Power.PlcVarName,control.Power.PlcVarMode.ToString(),"轴到记忆位触发变量:",control.MoveRel.PlcVarName,control.MoveRel.PlcVarMode.ToString()},
                    new[]{"轴忙碌状态变量:", control.Busy.PlcVarName, control.Busy.PlcVarMode.ToString(), "轴记忆位位置变量:",control.MemoryPosition.PlcVarName,control.MemoryPosition.PlcVarMode.ToString(), },
                    new[]{"轴使能切换变量:", control.OpenPower.PlcVarName, control.OpenPower.PlcVarMode.ToString(), "轴报错状态变量:",control.Error.PlcVarName,control.Error.PlcVarMode.ToString(), },
                    new[]{"轴JogP变量:", control.JogP.PlcVarName, control.JogP.PlcVarMode.ToString(), "轴绝对定位编号变量:",control.AbsNumber.PlcVarName,control.AbsNumber.PlcVarMode.ToString(), },
                    new[]{"轴JogN变量:", control.JogN.PlcVarName, control.JogN.PlcVarMode.ToString(), "轴点动速度变量:",control.JogVelocity.PlcVarName,control.JogVelocity.PlcVarMode.ToString(), },
                    new[]{"轴停止变量:", control.Stop.PlcVarName, control.Stop.PlcVarMode.ToString(), "轴正限位状态变量:",control.PosLimit.PlcVarName,control.PosLimit.PlcVarMode.ToString(), },
                    new[]{"轴回原点变量:", control.GoHome.PlcVarName, control.GoHome.PlcVarMode.ToString(), "轴负限位状态变量:",control.NegLimit.PlcVarName,control.NegLimit.PlcVarMode.ToString(), },
                    new[]{"轴回原点完成变量:", control.HomeDown.PlcVarName, control.HomeDown.PlcVarMode.ToString(), "轴原点状态变量:",control.Origin.PlcVarName,control.Origin.PlcVarMode.ToString(), },
                    new[]{"轴复位变量:", control.Reset.PlcVarName, control.Reset.PlcVarMode.ToString(), "轴绝对定位完成变量:",control.MovAbsDone.PlcVarName,control.MovAbsDone.PlcVarMode.ToString(), },
                    new[]{"轴示教变量:", control.Teach.PlcVarName, control.Teach.PlcVarMode.ToString(), "轴相对定位完成变量:",control.MovRelDone.PlcVarName,control.MovRelDone.PlcVarMode.ToString(), },
                    new[]{ "轴点位编号","轴点位位置变量","变量类型","轴点位显示名称","轴点位速度变量","变量类型" }
                });
                foreach (var p in position)
                {
                    Write.Add(new string[]
                    {
                        p.PositionNo.ToString(),
                        p.PositionVar.PlcVarName,
                        p.PositionVar.PlcVarMode.ToString(),
                        p.Name.DefaultText,
                        p.VelocityVar.PlcVarName,
                        p.VelocityVar.PlcVarMode.ToString()
                    });
                }
            }
            return Write;
        }
        private static List<string[]> SetCylinderConfig(CylinderConfig Data)
        {
            List<string[]> Write = new()
            {
                new[]{ "缩回到位信号变量","伸出控制变量","伸出磁簧输入信号变量","伸出到位信号变量","缩回保护锁变量","伸出保护锁变量","气缸报警变量" }
            };
            foreach (var item in Data.CylinderList)
            {
                Write.Add(new string[]
                {
                    item.Name.DefaultText,
                    item.Station.DefaultText,
                    item.HomeButtonName.DefaultText,
                    item.WorkButtonName.DefaultText,
                    item.Home.PlcVarName,
                    item.HomeInput.PlcVarName,
                    item.HomeDown.PlcVarName,
                    item.Work.PlcVarName,
                    item.WorkInput.PlcVarName,
                    item.WorkDown.PlcVarName,
                    item.HomeLock.PlcVarName,
                    item.WorkLock.PlcVarName,
                    item.Error.PlcVarName
                });
            }
            return Write;
        }
        private static List<string[]> SetAlarmConfig(AlarmConfig Data)
        {
            AlarmLogPath = Data.AlarmFilePath;
            List<string[]> Write = new()
            {
                new[]{ "报警触发变量","报警类型","报警显示信息","所属工站","报警ID" }
            };
            foreach (var item in Data.AlarmList)
            {
                Write.Add(new string[]
                {
                    item.Trigger.PlcVarName,
                    "Alarm",
                    item.Message.DefaultText,
                    item.Station.DefaultText,
                    item.ErrorId
                });
            }
            foreach (var item in Data.InfoList)
            {
                Write.Add(new string[]
                {
                    item.Trigger.PlcVarName,
                    "Info",
                    item.Message.DefaultText,
                    item.Station.DefaultText,
                    item.ErrorId
                });
            }
            return Write;
        }
        private static List<string[]> SetDataConfig(DataConfig Data)
        {
            List<string[]> Write = new()
            {
                new[]{"设备生产总数读取变量:",Data.ProductionNumber.PlcVarName,Data.ProductionNumber.PlcVarMode.ToString()},
                new[]{ "设备生产总NG数量:",Data.NgNumber.PlcVarName,Data.NgNumber.PlcVarMode.ToString() },
                new[]{"设备运行时长:",Data.RunningTime.PlcVarName,Data.RunningTime.PlcVarMode.ToString()},
                new[]{"设备暂停时长:",Data.PauseTime.PlcVarName,Data.PauseTime.PlcVarMode.ToString()},
                new[]{"设备报警时长:",Data.AlarmTime.PlcVarName,Data.AlarmTime.PlcVarMode.ToString()},
                new[]{"设备待料时长:",Data.DownTime.PlcVarName,Data.DownTime.PlcVarMode.ToString()},
                new[]{ "设备历史数据保存文件地址:",Data.DailyProductAdr },
                new[]{ "设备图片保存文件地址:",Data.PictureAdr },
                new[]{ "设备报警log保存文件地址:",AlarmLogPath }
            };
            if (Data.ReadCsvAddress.Count > 0)
            {
                bool first = false;

                foreach (var item in Data.ReadCsvAddress)
                {
                    if (!first)
                    {
                        Write.Add(new[] { "其它需要读取的文件地址表:", item });
                        first = true;
                    }
                    else
                    {
                        Write.Add(new[] { "", item });
                    }
                }
            }
            return Write;
        }
        private static List<string[]> SetParameterListConfig(ParametersConfig Data)
        {
            List<string[]> Write = new()
            {
                new[]{ "参数显示名称","参数所属分组","参数读取变量","变量类型", "操作模式", "设置" }
            };
            foreach (var item in Data.ParameterList)
            {
                Write.Add(new string[]
                {
                    item.Name.DefaultText,
                    item.GroupName.DefaultText,
                    item.PlcVar.PlcVarName,
                    item.PlcVar.PlcVarMode.ToString(),
                    GetVarMode.GetParameterMode(item.OperateMode.ToString()),
                    item.OperateData,
                });
            }
            return Write;
        }
        private static List<string[]> SetCyclicReadConfig(CyclicReadConfig Data)
        {
            List<string[]> Write = new()
            {
                new[]{ "常驻变量获取Key值(不能重复)","变量名","变量类型","是否显示弹窗","弹窗触发值" }
            };
            foreach (var item in Data.CyclicReadList)
            {
                Write.Add(new string[]
                {
                    item.Key,
                    item.PlcVar.PlcVarName,
                    item.PlcVar.PlcVarMode.ToString(),
                    item.IsPopups.ToString(),
                    item.PopupsTriggerCond
                });
            }
            return Write;
        }
        #endregion
    }

}
