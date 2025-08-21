using KupaKuper_DeviceSever.Server;

using WinFromFrame_KupaKuper.Modes;

namespace WinFromFrame_KupaKuper.ViewModes
{
    public class VisionImangViewMode : BaseViewMode
    {
        private IDeviceSystemServer _Server;
        public VisionImangViewMode(IDeviceSystemServer _Server) : base(_Server)
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
            GetImages();
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
        }
        /// <summary>
        /// 默认加载的图片地址
        /// </summary>
        public string CurrentShowImage { get; set; } = string.Empty;
        /// <summary>
        /// 默认加载的图片名称
        /// </summary>
        public string CurrentImagePath { get; set; } = string.Empty;
        /// <summary>
        /// 默认加载的图片名称
        /// </summary>
        public string CurrentImageName { get; set; } = string.Empty;
        /// <summary>
        /// 默认加载的图片创建时间
        /// </summary>
        public string CurrentImageCreationTime { get; set; } = string.Empty;
        /// <summary>
        /// 默认加载的图片大小
        /// </summary>
        public string CurrentImageSize { get; set; } = string.Empty;
        /// <summary>
        /// 文件夹内所有的图片地址
        /// </summary>
        public List<string> ImageList { get; set; } = new List<string>();
        /// <summary>
        /// 获取图片文件夹内的图片
        /// </summary>
        public void GetImages()
        {
            var imageFileAdr = _Server.CurrentDevice.DeviceConfig.device.DataConfig.PictureAdr;
            if (string.IsNullOrEmpty(imageFileAdr))
                return;

            try
            {
                // 获取源图片并排序
                var imageFiles = Directory.EnumerateFiles(imageFileAdr, "*.*", SearchOption.TopDirectoryOnly)
                    .Where(file =>
                    {
                        var ext = Path.GetExtension(file).ToLower();
                        return ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".bmp";
                    })
                    .Select(file => new FileInfo(file))
                    .OrderByDescending(f => f.LastWriteTime)
                    .Take(100)
                    .Select(f => f.FullName)
                    .ToList();

                // 更新图片列表
                List<string> _ImageList = new List<string>();
                var oldList = Interlocked.Exchange(ref _ImageList, imageFiles);
                ImageList = _ImageList;
                oldList?.Clear();

                CurrentShowImage = imageFiles.FirstOrDefault() ?? string.Empty;
            }
            catch (Exception ex)
            {
                _Server.CurrentDevice.AddDeviceLogItem($"更新图片列表时出错: {ex.Message}");
                ImageList = new List<string>();
                CurrentShowImage = string.Empty;
            }
        }
        /// <summary>
        /// 根据图片地址更新当前显示图片的属性
        /// </summary>
        /// <param name="imagePath">图片路径</param>
        public void UpdateImageProperties(string imagePath)
        {
            if (string.IsNullOrEmpty(imagePath))
            {
                CurrentShowImage = string.Empty;
                CurrentImageName = string.Empty;
                CurrentImageCreationTime = string.Empty;
                CurrentImageSize = string.Empty;
                CurrentImagePath = string.Empty;
                return;
            }

            try
            {
                var fileInfo = new FileInfo(imagePath);
                CurrentShowImage = imagePath;
                CurrentImagePath = imagePath;
                CurrentImageName = fileInfo.Name;
                CurrentImageCreationTime = fileInfo.CreationTime.ToString("yyyy-MM-dd HH:mm:ss");
                CurrentImageSize = FormatFileSize(fileInfo.Length);
            }
            catch (Exception ex)
            {
                _Server.CurrentDevice.AddDeviceLogItem($"更新图片属性时出错: {ex.Message}");
            }
        }

        private string FormatFileSize(long length)
        {
            return length switch
            {
                < 1024 => $"{length} B",
                < 1024 * 1024 => $"{length / 1024.0:F2} KB",
                < 1024 * 1024 * 1024 => $"{length / (1024.0 * 1024):F2} MB",
                _ => $"{length / (1024.0 * 1024 * 1024):F2} GB"
            };
        }
    }
}
