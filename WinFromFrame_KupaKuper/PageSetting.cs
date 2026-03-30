using KupaKuper_HMI_DeviceSever.Server;

namespace WinFromFrame_KupaKuper
{
    public partial class PageSetting : Form
    {
        DeviceSystemServer server;
        public PageSetting(DeviceSystemServer _server)
        {
            InitializeComponent();
            this.server = _server;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            server.ChangeLanguage("en-us");
            server.Language = "en-us";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            server.ChangeLanguage("zh-cn");
            server.Language = "zh-cn";
        }
    }
}
