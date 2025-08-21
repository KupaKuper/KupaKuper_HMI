using KupaKuper_DeviceSever.Server;

using KupaKuper_HMI_Config.DeviceConfig.BaseType;
using KupaKuper_HMI_Config.Help;

using System.Collections.ObjectModel;
using System.ComponentModel;

using WinFromFrame_KupaKuper.Modes;

namespace WinFromFrame_KupaKuper.ViewModes
{
    public class GantryViewMode : BaseViewMode
    {
        private IDeviceSystemServer _Server;
        public GantryViewMode(IDeviceSystemServer _Server) : base(_Server)
        {
            this._Server = _Server;
        }

        public override required Action UpdataView { get; set; }
        /// <summary>
        /// 需要重构设备切换或页面加载时的数据读取
        /// </summary>
        public override void _Server_DeviceChanged()
        {
            base._Server_DeviceChanged();
        }
        /// <summary>
        /// 页面加载默认执行UpdateDevice,重构以执行其它的事件
        /// </summary>
        public override void OnInitialized()
        {
            base.OnInitialized();
            GrantyrConfigs = ReadGantryData();
            Disabled = _Server.CurrentUserType < IDeviceSystemServer.UserType.Engineer;
            ShowFirstGantry();
        }
        /// <summary>
        /// 页面关闭时执行的额外事件
        /// </summary>
        public override void RemovePageQuest()
        {
            base.RemovePageQuest();
            if (GantryData != null)
            {
                GantryData.Axis_X.AnyPropertyChanged -= OnIoModePropertyChanged;
                GantryData.Axis_Y.AnyPropertyChanged -= OnIoModePropertyChanged;
                if (GantryData.Axis_Z != null) GantryData.Axis_Z.AnyPropertyChanged -= OnIoModePropertyChanged;
            }
        }

        public override void _Server_UserChanged(IDeviceSystemServer.UserType user)
        {
            Disabled = user < IDeviceSystemServer.UserType.Engineer;
            UpdataView();
        }
        public Dictionary<string, GantyrConfig> GrantyrConfigs { get; set; } = new();
        public GantryData? GantryData { get; set; }
        public List<string> GantryGroups = new List<string>();
        public string CurrentGantryGroup = string.Empty;
        public bool Disabled = true;

        /// <summary>
        /// 保存龙门数据到本地
        /// </summary>
        public void SaveGantryData()
        {
            try
            {
                string LoginDataPath = Path.Combine(_Server.CurrentDevice.DeviceSetting.ConfigPath, "GantryConfig.json");

                JsonFileHelper.SaveJson(LoginDataPath, GrantyrConfigs);
            }
            catch (Exception ex)
            {
                _Server.CurrentDevice.AddDeviceLogItem($"龙门数据保存失败:{ex.Message}");
                _Server.Toast_ShowError(_Server.CurrentDeviecName, "龙门数据保存失败", null);
            }
        }
        /// <summary>
        /// 读取本地保存的龙门数据
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, GantyrConfig> ReadGantryData()
        {
            try
            {
                string LoginDataPath = Path.Combine(_Server.CurrentDevice.DeviceSetting.ConfigPath, "GantryConfig.json");

                // 检查文件是否存在
                if (!File.Exists(LoginDataPath))
                {
                    // 文件不存在时返回空
                    return new();
                }

                // 读取数据
                var userData= JsonFileHelper.ReadJson<Dictionary<string, GantyrConfig>>(LoginDataPath);
                // 确保返回非空
                return userData ?? new();
            }
            catch (Exception ex)
            {
                _Server.CurrentDevice.AddDeviceLogItem($"读取龙门数据时出错:{ex.Message}");
                _Server.Toast_ShowError(_Server.CurrentDeviecName, "读取龙门数据时出错", null);
                return new();
            }
        }
        public void ShowFirstGantry()
        {
            GantryGroups = GrantyrConfigs.Keys.ToList();
            if (GantryGroups.Count > 0 && CurrentGantryGroup==string.Empty)
            {
                CurrentGantryGroup = GantryGroups.First();
                SelectGroup(CurrentGantryGroup);
            }
        }
        /// <summary>
        /// 切换龙门显示
        /// </summary>
        /// <param name="group"></param>
        public void SelectGroup(string group)
        {
            if (string.IsNullOrEmpty(group)) return;
            if (GrantyrConfigs.TryGetValue(group, out GantyrConfig? config))
            {
                _Server.CurrentDevice.RemoveUpdataAll(_Server.ServerID);
                if (GantryData != null)
                {
                    GantryData.Axis_X.AnyPropertyChanged -= OnIoModePropertyChanged;
                    GantryData.Axis_Y.AnyPropertyChanged -= OnIoModePropertyChanged;
                    if (GantryData.Axis_Z != null) GantryData.Axis_Z.AnyPropertyChanged -= OnIoModePropertyChanged;
                }
                if (config == null) return;
                if (!_Server.CurrentDevice.AxesModes.TryGetValue(config!.Axis_X, out var axis_x)) return;
                if (!_Server.CurrentDevice.AxesModes.TryGetValue(config!.Axis_Y, out var axis_y)) return;
                ObservableCollection<Axis>? axis_z = null;
                if (config.Axis_Z == null ? false : !_Server.CurrentDevice.AxesModes.TryGetValue(config!.Axis_Z, out axis_z)) return;
                _Server.CurrentDevice.RequestUpdataAxis(_Server.ServerID, config.Axis_X);
                _Server.CurrentDevice.RequestUpdataAxis(_Server.ServerID, config.Axis_Y);
                if (config.Axis_Z != null) _Server.CurrentDevice.RequestUpdataAxis(_Server.ServerID, config.Axis_Z);
                GantryData = new(axis_x.First(), axis_y.First(), axis_z == null ? null : axis_z.First());
                GantryData.GantryPositions = config.GantryPositions;
                GantryData.Axis_X.AnyPropertyChanged += OnIoModePropertyChanged;
                GantryData.Axis_Y.AnyPropertyChanged += OnIoModePropertyChanged;
                if (GantryData.Axis_Z != null) GantryData.Axis_Z.AnyPropertyChanged += OnIoModePropertyChanged;
                CurrentGantryGroup = group;
            }
        }
        private void OnIoModePropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            UpdataView();
        }
        /// <summary>
        /// 添加龙门配置
        /// </summary>
        /// <param name="name"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void AddGantryData(string name, string x, string y, string? z)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(x) || string.IsNullOrEmpty(y)) return;
            GrantyrConfigs.TryAdd(name, new()
            {
                Axis_X = x,
                Axis_Y = y,
                Axis_Z = z
            });
            SaveGantryData();
            ShowFirstGantry();
        }
        /// <summary>
        /// 删除龙门配置
        /// </summary>
        /// <param name="gantryKey"></param>
        public void RemoveGantryData(string gantryKey)
        {
            if (string.IsNullOrEmpty(gantryKey)) return;
            if (GrantyrConfigs.Remove(gantryKey))
            {
                SaveGantryData();
                if (CurrentGantryGroup == gantryKey)
                {
                    CurrentGantryGroup = string.Empty;
                    ShowFirstGantry();
                }
            }
        }
        /// <summary>
        /// 添加龙门点位
        /// </summary>
        /// <param name="gantryKey"></param>
        /// <param name="name"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void AddGantryPositionData(string gantryKey, string name, int x, int y, int? z)
        {
            if (string.IsNullOrEmpty(name)) return;
            if (GrantyrConfigs.TryGetValue(gantryKey, out var gantyrConfig))
            {
                string positionValue = $"{x},{y},{(z==null?"null":z)}";
                gantyrConfig.GantryPositions.TryAdd(name, positionValue);
                SaveGantryData();
            }
        }
        /// <summary>
        /// 删除龙门点位数据
        /// </summary>
        /// <param name="gantryKey"></param>
        /// <param name="positionName"></param>
        public void RemoveGantryPositionData(string gantryKey, string positionName)
        {
            if (string.IsNullOrEmpty(gantryKey) || string.IsNullOrEmpty(positionName)) return;
            if (GrantyrConfigs.TryGetValue(gantryKey, out var gantyrConfig))
            {
                if (gantyrConfig.GantryPositions.Remove(positionName))
                {
                    SaveGantryData();
                }
            }
        }
    }
}
