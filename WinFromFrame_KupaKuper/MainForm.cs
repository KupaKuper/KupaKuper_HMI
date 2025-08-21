using KupaKuper_DeviceSever.Server;

using System.Drawing.Text;

namespace WinFromFrame_KupaKuper
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// 设备服务器
        /// </summary>
        public DeviceSystemServer _server;
        /// <summary>
        /// 字体加载类
        /// </summary>
        private PrivateFontCollection _fontCollection = new PrivateFontCollection();
        public MainForm(IDeviceSystemServer server)
        {
            InitializeComponent();
            this._server = (DeviceSystemServer)server;
            // 加载字体文件
            string fontPath = Path.Combine(Application.StartupPath, "Resources", "iconfont.ttf");

            if (File.Exists(fontPath))
            {
                _fontCollection.AddFontFile(fontPath);
            }
            else
            {
                MessageBox.Show("字体文件未找到！");
            }
            _server.ChangeLanguage("zh-cn");
            InitializeIconfont();
        }
        private void InitializeIconfont()
        {
            float fontSize = 26f; // 设置字体大小
            Color backcolor = Color.FromArgb(105, 183, 247); // 设置按钮背景颜色
            // 设置按钮字体和图标
            Pagesbut_首页.Font = new Font(_fontCollection.Families[0], fontSize);
            Pagesbut_首页.Text = "\uf895"; // 替换为你的图标 Unicode
            Pagesbut_首页.BackColor = backcolor; // 设置按钮背景颜色
            Pagesbut_操作.Font = new Font(_fontCollection.Families[0], fontSize);
            Pagesbut_操作.Text = "\uec1b"; // 替换为你的图标 Unicode
            Pagesbut_操作.BackColor = backcolor; // 设置按钮背景颜色
            Pagesbut_图片.Font = new Font(_fontCollection.Families[0], fontSize);
            Pagesbut_图片.Text = "\ue8c7"; // 替换为你的图标 Unicode
            Pagesbut_图片.BackColor = backcolor; // 设置按钮背景颜色
            Pagesbut_报警.Font = new Font(_fontCollection.Families[0], fontSize);
            Pagesbut_报警.Text = "\ue608"; // 替换为你的图标 Unicode
            Pagesbut_报警.BackColor = backcolor; // 设置按钮背景颜色
            Pagesbut_数据.Font = new Font(_fontCollection.Families[0], fontSize);
            Pagesbut_数据.Text = "\uf89b"; // 替换为你的图标 Unicode
            Pagesbut_数据.BackColor = backcolor; // 设置按钮背景颜色
            Pagesbut_启动.Font = new Font(_fontCollection.Families[0], fontSize);
            Pagesbut_启动.Text = "\ue610"; // 替换为你的图标 Unicode
            Pagesbut_启动.BackColor = backcolor; // 设置按钮背景颜色
            Pagesbut_暂停.Font = new Font(_fontCollection.Families[0], fontSize);
            Pagesbut_暂停.Text = "\ue60f"; // 替换为你的图标 Unicode
            Pagesbut_暂停.BackColor = backcolor; // 设置按钮背景颜色
            Pagesbut_复位.Font = new Font(_fontCollection.Families[0], fontSize);
            Pagesbut_复位.Text = "\ue611"; // 替换为你的图标 Unicode
            Pagesbut_复位.BackColor = backcolor; // 设置按钮背景颜色
            Pagesbut_登入.Font = new Font(_fontCollection.Families[0], fontSize);
            Pagesbut_登入.Text = "\uf89d"; // 替换为你的图标 Unicode
            Pagesbut_登入.BackColor = backcolor; // 设置按钮背景颜色
            Pagesbut_设置.Font = new Font(_fontCollection.Families[0], fontSize);
            Pagesbut_设置.Text = "\uf89c"; // 替换为你的图标 Unicode
            Pagesbut_设置.BackColor = backcolor; // 设置按钮背景颜色
            _server.ConnectedChanged += (b) =>
            {
                Pagesbut_device.BackColor = b ? Color.FromArgb(105, 183, 247) : Color.FromArgb(255, 128, 128);
            };
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            PageHome child = new(_server);
            child.Size = PageBox.Size; // 设置子窗体大小与Panel一致
            child.TopLevel = false; // 关键！取消顶级窗体属性
            child.FormBorderStyle = FormBorderStyle.None; // 去掉边框
            child.Dock = DockStyle.Fill; // 填充Panel
            // 清空Panel并添加子窗体
            PageBox.Controls.Clear();
            PageBox.Controls.Add(child);
            child.Show(); // 显示子窗体
        }
        #region 页面按钮事件
        private void Pagesbut_首页_Click(object sender, EventArgs e)
        {
            PageHome child = new(_server);
            child.Size = PageBox.Size; // 设置子窗体大小与Panel一致
            child.TopLevel = false; // 关键！取消顶级窗体属性
            child.FormBorderStyle = FormBorderStyle.None; // 去掉边框
            child.Dock = DockStyle.Fill; // 填充Panel
            // 清空Panel并添加子窗体
            PageBox.Controls.Clear();
            PageBox.Controls.Add(child);
            child.Show(); // 显示子窗体
        }

        private void Pagesbut_操作_Click(object sender, EventArgs e)
        {
            PageControl child = new(_server);
            child.Size = PageBox.Size; // 设置子窗体大小与Panel一致
            child.TopLevel = false; // 关键！取消顶级窗体属性
            child.FormBorderStyle = FormBorderStyle.None; // 去掉边框
            child.Dock = DockStyle.Fill; // 填充Panel
            // 清空Panel并添加子窗体
            PageBox.Controls.Clear();
            PageBox.Controls.Add(child);
            child.Show(); // 显示子窗体
        }
        private void Pagesbut_图片_Click(object sender, EventArgs e)
        {
            PageImage child = new(_server);
            child.Size = PageBox.Size; // 设置子窗体大小与Panel一致
            child.TopLevel = false; // 关键！取消顶级窗体属性
            child.FormBorderStyle = FormBorderStyle.None; // 去掉边框
            child.Dock = DockStyle.Fill; // 填充Panel
            // 清空Panel并添加子窗体
            PageBox.Controls.Clear();
            PageBox.Controls.Add(child);
            child.Show(); // 显示子窗体
        }

        private void Pagesbut_报警_Click(object sender, EventArgs e)
        {
            PageAlarm child = new(_server);
            child.Size = PageBox.Size; // 设置子窗体大小与Panel一致
            child.TopLevel = false; // 关键！取消顶级窗体属性
            child.FormBorderStyle = FormBorderStyle.None; // 去掉边框
            child.Dock = DockStyle.Fill; // 填充Panel
            // 清空Panel并添加子窗体
            PageBox.Controls.Clear();
            PageBox.Controls.Add(child);
            child.Show(); // 显示子窗体
        }

        private void Pagesbut_数据_Click(object sender, EventArgs e)
        {
            PageData child = new(_server);
            child.Size = PageBox.Size; // 设置子窗体大小与Panel一致
            child.TopLevel = false; // 关键！取消顶级窗体属性
            child.FormBorderStyle = FormBorderStyle.None; // 去掉边框
            child.Dock = DockStyle.Fill; // 填充Panel
            // 清空Panel并添加子窗体
            PageBox.Controls.Clear();
            PageBox.Controls.Add(child);
            child.Show(); // 显示子窗体
        }

        private void Pagesbut_登入_Click(object sender, EventArgs e)
        {
            PageLogin child = new(_server);
            child.Size = PageBox.Size; // 设置子窗体大小与Panel一致
            child.TopLevel = false; // 关键！取消顶级窗体属性
            child.FormBorderStyle = FormBorderStyle.None; // 去掉边框
            child.Dock = DockStyle.Fill; // 填充Panel
            // 清空Panel并添加子窗体
            PageBox.Controls.Clear();
            PageBox.Controls.Add(child);
            child.Show(); // 显示子窗体
        }

        private void Pagesbut_设置_Click(object sender, EventArgs e)
        {
            PageSetting child = new(_server);
            child.Size = PageBox.Size; // 设置子窗体大小与Panel一致
            child.TopLevel = false; // 关键！取消顶级窗体属性
            child.FormBorderStyle = FormBorderStyle.None; // 去掉边框
            child.Dock = DockStyle.Fill; // 填充Panel
            // 清空Panel并添加子窗体
            PageBox.Controls.Clear();
            PageBox.Controls.Add(child);
            child.Show(); // 显示子窗体
        }
        #endregion

    }
}
