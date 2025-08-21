using KupaKuper_DeviceSever.Server;

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
        }

        private void button2_Click(object sender, EventArgs e)
        {
            server.ChangeLanguage("zh-cn");
        }
    }
}
