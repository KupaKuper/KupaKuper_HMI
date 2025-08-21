using KupaKuper_DeviceSever.Server;

using KupaKuper_HMI_Config.DeviceConfig.BaseType;

using System.Collections.ObjectModel;

using WinFromFrame_KupaKuper.Help.GlobalParameters;
using WinFromFrame_KupaKuper.Modes;

namespace WinFromFrame_KupaKuper.ViewModes
{
    public class IoViewMode : BaseViewMode
    {
        private IDeviceSystemServer _Server;
        public IoViewMode(IDeviceSystemServer _Server) : base(_Server)
        {
            this._Server = _Server;
        }
        public ObservableCollection<Io> IoModes = new();
        public readonly object _ioDataLock = new object();
        public int CurrentPage = 1;  // 当前页码
        public int TotalPages = 1;   // 总页数
        public int PageIoCount = GetGlobalParameter.AppGlobalParameter.PageIoCount;//每页显示的数量
        public int RowIoCount = GetGlobalParameter.AppGlobalParameter.RowIoCount; //每行显示的数量

        public override required Action UpdataView { get; set; }
        /// <summary>
        /// 需要重构设备切换或页面加载时的数据读取
        /// </summary>
        public override void _Server_DeviceChanged()
        {
            base._Server_DeviceChanged();
            _Server.CurrentDevice.IoModes.TryGetValue(_Server.CurrentDeviceRember.IoType.ToString(), out var iOModes);
            IoModes = new ObservableCollection<Io>(iOModes ?? Enumerable.Empty<Io>());
            TotalPages = (IoModes.Count - 1) / PageIoCount + 1;
            _Server.CurrentDevice.RequestUpdataIo(_Server.ServerID, _Server.CurrentDeviceRember.IoType.ToString());
            UpdataView();
        }
        /// <summary>
        /// 页面加载默认执行UpdateDevice,重构以执行其它的事件
        /// </summary>
        public override void OnInitialized()
        {
            base.OnInitialized();
        }
        /// <summary>
        /// 页面关闭时执行的额外事件
        /// </summary>
        public override void RemovePageQuest()
        {
            base.RemovePageQuest();
            _Server.CurrentDevice.RemoveUpdataIo(_Server.ServerID, _Server.CurrentDeviceRember.IoType.ToString());
        }
        /// <summary>
        /// 类型切换方法
        /// </summary>
        /// <param name="ioType"></param>
        public void ToggleIoType(DeviceRember.IoTypeName ioType)
        {
            _Server.CurrentDevice.RemoveUpdataIo(_Server.ServerID, _Server.CurrentDeviceRember.IoType.ToString());
            if (_Server.CurrentDevice.IoModes.TryGetValue(ioType.ToString(), out var iOModes))
            {
                IoModes = IoModes = new ObservableCollection<Io>(iOModes ?? Enumerable.Empty<Io>());
                _Server.CurrentDeviceRember.IoType = ioType;
                TotalPages = (IoModes.Count - 1) / PageIoCount + 1;
                CurrentPage = 1;
            }
            _Server.CurrentDevice.RequestUpdataIo(_Server.ServerID, _Server.CurrentDeviceRember.IoType.ToString());
            UpdataView();
        }

        /// <summary>
        /// 上一页
        /// </summary>
        public void PreviousPage()
        {
            if (CurrentPage > 1)
            {
                CurrentPage--;
                _Server.CurrentDeviceRember.IoIndex = CurrentPage;
                UpdataView();
            }
        }

        /// <summary>
        /// 下一页
        /// </summary>
        public void NextPage()
        {
            if (CurrentPage < TotalPages)
            {
                CurrentPage++;
                _Server.CurrentDeviceRember.IoIndex = CurrentPage;
                UpdataView();
            }
        }
    }
}
