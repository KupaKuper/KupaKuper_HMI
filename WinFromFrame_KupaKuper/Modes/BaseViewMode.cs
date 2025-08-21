using KupaKuper_DeviceSever.Server;

namespace WinFromFrame_KupaKuper.Modes
{
    public abstract class BaseViewMode(IDeviceSystemServer _Server)
    {
        /// <summary>
        /// 界面加载前需要执行的方法
        /// </summary>
        public virtual void OnInitialized()
        {
            _Server.LanguageChanged += LanguageChanged;
            _Server.DeviceChanged += _Server_DeviceChanged; ;
            _Server.BeforeDeviceChanged += _Server_BeforeDeviceChanged;
            _Server.UserChanged += _Server_UserChanged;
            _Server.ThemeChanged += _Server_ThemeChanged;
            _Server_DeviceChanged();
        }

        public virtual void _Server_ThemeChanged(bool obj)
        {
            
        }

        public virtual void _Server_UserChanged(IDeviceSystemServer.UserType user)
        {
            
        }

        /// <summary>
        /// 设备切换后执行的方法
        /// </summary>
        public virtual void _Server_DeviceChanged()
        {
            
        }
        /// <summary>
        /// 设备切换前执行的方法
        /// </summary>
        public virtual void _Server_BeforeDeviceChanged()
        {
            
        }
        /// <summary>
        /// 页面更新事件
        /// </summary>
        public abstract Action UpdataView { get; set; }
        /// <summary>
        /// 语言切换事件
        /// </summary>
        /// <param name="language"></param>
        public virtual void LanguageChanged(string language)
        {
            UpdataView();
        }
        /// <summary>
        /// 页面关闭前需要执行的方法
        /// </summary>
        public virtual void Dispose()
        {
            _Server.LanguageChanged -= LanguageChanged;
            _Server.DeviceChanged -= _Server_DeviceChanged; ;
            _Server.BeforeDeviceChanged -= _Server_BeforeDeviceChanged;
            _Server.UserChanged -= _Server_UserChanged;
            _Server.ThemeChanged -= _Server_ThemeChanged;
            RemovePageQuest();
        }
        /// <summary>
        /// 离开页面时需要执行的其它事件
        /// </summary>
        public virtual void RemovePageQuest()
        {

        }
    }
}
