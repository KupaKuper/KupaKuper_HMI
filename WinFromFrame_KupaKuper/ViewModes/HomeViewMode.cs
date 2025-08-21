using KupaKuper_DeviceSever.Server;

using WinFromFrame_KupaKuper.Modes;

namespace WinFromFrame_KupaKuper.ViewModes
{
    public class HomeViewMode : BaseViewMode
    {
        private IDeviceSystemServer _Server;
        public HomeViewMode(IDeviceSystemServer _Server) : base(_Server)
        {
            this._Server = _Server;
        }
        public override required Action UpdataView { get; set; }

        public List<string> DeviceModeMessage
        {
            get
            {
                return new()
                {
                    _Server.GetLanguageValue("DeviceMode_0"),
                    _Server.GetLanguageValue("DeviceMode_1"),
                    _Server.GetLanguageValue("DeviceMode_2"),
                    _Server.GetLanguageValue("DeviceMode_3"),
                    _Server.GetLanguageValue("DeviceMode_4"),
                    _Server.GetLanguageValue("DeviceMode_5"),
                    _Server.GetLanguageValue("DeviceMode_6"),
                    _Server.GetLanguageValue("DeviceMode_7"),
                    _Server.GetLanguageValue("DeviceMode_8"),
                    _Server.GetLanguageValue("DeviceMode_9"),
                };
            }
        }
        /// <summary>
        /// 需要重构设备切换或页面加载时的数据读取
        /// </summary>
        public override void _Server_DeviceChanged()
        {
            base._Server_DeviceChanged();
            UpdataView();
        }
        /// <summary>
        /// 页面加载默认执行UpdateDevice,重构以执行其它的事件
        /// </summary>
        public override void OnInitialized()
        {
            base.OnInitialized();
            var first = FaultStatistics;
        }
        /// <summary>
        /// 页面关闭时执行的额外事件
        /// </summary>
        public override void RemovePageQuest()
        {
            base.RemovePageQuest();
        }
        /// <summary>
        /// 用户类型改变时执行的事件
        /// </summary>
        /// <param name="user"></param>
        public override void _Server_UserChanged(IDeviceSystemServer.UserType user)
        {
            base._Server_UserChanged(user);
            UpdataView();
        }

        /// <summary>
        /// 获取设备故障统计数据
        /// </summary>
        public List<StringData> FaultStatistics
        {
            get
            {
                return GetFaultStatistics();
            }
        }

        private List<StringData> GetFaultStatistics()
        {
            float RunningTime = 8.5F;
            float AlarmTime = 1.3F;
            float DownTime = 2F;
            float PauseTime = 4.4F;
            if (_Server.CurrentDevice.Connected)
            {
                RunningTime = _Server.CurrentDevice.Ethernet?.TryRead<float>(_Server.CurrentDevice.DeviceConfig.device.DataConfig.RunningTime.PlcVarAddress, out bool r) ?? 0F;
                AlarmTime = _Server.CurrentDevice.Ethernet?.TryRead<float>(_Server.CurrentDevice.DeviceConfig.device.DataConfig.AlarmTime.PlcVarAddress, out bool f) ?? 0F;
                DownTime = _Server.CurrentDevice.Ethernet?.TryRead<float>(_Server.CurrentDevice.DeviceConfig.device.DataConfig.DownTime.PlcVarAddress, out bool s) ?? 0F;
                PauseTime = _Server.CurrentDevice.Ethernet?.TryRead<float>(_Server.CurrentDevice.DeviceConfig.device.DataConfig.PauseTime.PlcVarAddress, out bool t) ?? 0F;
            }
            return new()
            {
                new(){ Name = "设备运行时间", Value = (decimal)RunningTime },
                new(){ Name = "设备故障时间", Value = (decimal)AlarmTime },
                new(){ Name = "设备待料时间", Value = (decimal)DownTime },
                new(){ Name = "设备暂停时间", Value = (decimal)PauseTime }
            };
        }

        public string GetPointColor(StringData stringData)
        {
            switch (stringData.Name)
            {
                case "设备运行时间":
                    return "#e3001b";
                case "设备故障时间":
                    return "#005ba3";
                case "设备待料时间":
                    return "#ffd500";
                case "设备暂停时间":
                    return "#00783c";
                default:
                    return "#87775d";
            }



        }

        /// <summary>
        /// 获取服务器地址
        /// </summary>
        /// <returns></returns>
        public string GetServerAddress()
        {
            try
            {
                var host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        return $"https://{ip}:7047";
                    }
                }
                return "https://localhost:7047";
            }
            catch
            {
                return "https://localhost:7047";
            }
        }
    }
}
