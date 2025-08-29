namespace WinFromFrame_KupaKuper
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            notifyIcon1 = new NotifyIcon(components);
            contextMenuStrip1 = new ContextMenuStrip(components);
            showWindowToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            contextMenuStrip1.SuspendLayout();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            Pagesbut_首页 = new Button();
            Pagesbut_操作 = new Button();
            Pagesbut_图片 = new Button();
            Pagesbut_报警 = new Button();
            Pagesbut_数据 = new Button();
            Pagesbut_设置 = new Button();
            Pagesbut_登入 = new Button();
            Pagesbut_暂停 = new Button();
            Pagesbut_启动 = new Button();
            Pagesbut_复位 = new Button();
            Pagesbut_device = new Button();
            PageBox = new Panel();
            SuspendLayout();
            // 
            // notifyIcon1
            // 
            notifyIcon1.ContextMenuStrip = contextMenuStrip1;
            notifyIcon1.Text = "WinFromFrame_KupaKuper";
            notifyIcon1.Icon = (Icon)resources.GetObject("$this.Icon");
            notifyIcon1.Visible = true;
            notifyIcon1.DoubleClick += notifyIcon1_DoubleClick;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { showWindowToolStripMenuItem, exitToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(125, 48);
            // 
            // showWindowToolStripMenuItem
            // 
            showWindowToolStripMenuItem.Name = "showWindowToolStripMenuItem";
            showWindowToolStripMenuItem.Size = new Size(124, 22);
            showWindowToolStripMenuItem.Text = "显示窗口";
            showWindowToolStripMenuItem.Click += showWindowToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(124, 22);
            exitToolStripMenuItem.Text = "退出";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // Pagesbut_首页
            // 
            Pagesbut_首页.Location = new Point(12, 12);
            Pagesbut_首页.Name = "Pagesbut_首页";
            Pagesbut_首页.Size = new Size(60, 60);
            Pagesbut_首页.TabIndex = 0;
            Pagesbut_首页.Text = "首页";
            Pagesbut_首页.UseVisualStyleBackColor = true;
            Pagesbut_首页.Click += Pagesbut_首页_Click;
            // 
            // Pagesbut_操作
            // 
            Pagesbut_操作.Location = new Point(78, 12);
            Pagesbut_操作.Name = "Pagesbut_操作";
            Pagesbut_操作.Size = new Size(60, 60);
            Pagesbut_操作.TabIndex = 1;
            Pagesbut_操作.Text = "操作";
            Pagesbut_操作.UseVisualStyleBackColor = true;
            Pagesbut_操作.Click += Pagesbut_操作_Click;
            // 
            // Pagesbut_图片
            // 
            Pagesbut_图片.Location = new Point(144, 12);
            Pagesbut_图片.Name = "Pagesbut_图片";
            Pagesbut_图片.Size = new Size(60, 60);
            Pagesbut_图片.TabIndex = 2;
            Pagesbut_图片.Text = "图片";
            Pagesbut_图片.UseVisualStyleBackColor = true;
            Pagesbut_图片.Click += Pagesbut_图片_Click;
            // 
            // Pagesbut_报警
            // 
            Pagesbut_报警.Location = new Point(210, 12);
            Pagesbut_报警.Name = "Pagesbut_报警";
            Pagesbut_报警.Size = new Size(60, 60);
            Pagesbut_报警.TabIndex = 3;
            Pagesbut_报警.Text = "报警";
            Pagesbut_报警.UseVisualStyleBackColor = true;
            Pagesbut_报警.Click += Pagesbut_报警_Click;
            // 
            // Pagesbut_数据
            // 
            Pagesbut_数据.Location = new Point(276, 12);
            Pagesbut_数据.Name = "Pagesbut_数据";
            Pagesbut_数据.Size = new Size(60, 60);
            Pagesbut_数据.TabIndex = 4;
            Pagesbut_数据.Text = "数据";
            Pagesbut_数据.UseVisualStyleBackColor = true;
            Pagesbut_数据.Click += Pagesbut_数据_Click;
            // 
            // Pagesbut_设置
            // 
            Pagesbut_设置.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Pagesbut_设置.Location = new Point(1142, 12);
            Pagesbut_设置.Name = "Pagesbut_设置";
            Pagesbut_设置.Size = new Size(60, 60);
            Pagesbut_设置.TabIndex = 5;
            Pagesbut_设置.Text = "设置";
            Pagesbut_设置.UseVisualStyleBackColor = true;
            Pagesbut_设置.Click += Pagesbut_设置_Click;
            // 
            // Pagesbut_登入
            // 
            Pagesbut_登入.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Pagesbut_登入.Location = new Point(1076, 12);
            Pagesbut_登入.Name = "Pagesbut_登入";
            Pagesbut_登入.Size = new Size(60, 60);
            Pagesbut_登入.TabIndex = 6;
            Pagesbut_登入.Text = "登入";
            Pagesbut_登入.UseVisualStyleBackColor = true;
            Pagesbut_登入.Click += Pagesbut_登入_Click;
            // 
            // Pagesbut_暂停
            // 
            Pagesbut_暂停.Anchor = AnchorStyles.Top;
            Pagesbut_暂停.Location = new Point(776, 12);
            Pagesbut_暂停.Name = "Pagesbut_暂停";
            Pagesbut_暂停.Size = new Size(60, 60);
            Pagesbut_暂停.TabIndex = 7;
            Pagesbut_暂停.Text = "暂停";
            Pagesbut_暂停.UseVisualStyleBackColor = true;
            // 
            // Pagesbut_启动
            // 
            Pagesbut_启动.Anchor = AnchorStyles.Top;
            Pagesbut_启动.Location = new Point(710, 12);
            Pagesbut_启动.Name = "Pagesbut_启动";
            Pagesbut_启动.Size = new Size(60, 60);
            Pagesbut_启动.TabIndex = 8;
            Pagesbut_启动.Text = "启动";
            Pagesbut_启动.UseVisualStyleBackColor = true;
            // 
            // Pagesbut_复位
            // 
            Pagesbut_复位.Anchor = AnchorStyles.Top;
            Pagesbut_复位.Location = new Point(842, 12);
            Pagesbut_复位.Name = "Pagesbut_复位";
            Pagesbut_复位.Size = new Size(60, 60);
            Pagesbut_复位.TabIndex = 9;
            Pagesbut_复位.Text = "复位";
            Pagesbut_复位.UseVisualStyleBackColor = true;
            // 
            // Pagesbut_device
            // 
            Pagesbut_device.Anchor = AnchorStyles.Top;
            Pagesbut_device.Location = new Point(512, 12);
            Pagesbut_device.Name = "Pagesbut_device";
            Pagesbut_device.Size = new Size(192, 60);
            Pagesbut_device.TabIndex = 10;
            Pagesbut_device.Text = "设备状态";
            Pagesbut_device.UseVisualStyleBackColor = true;
            // 
            // PageBox
            // 
            PageBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            PageBox.BackColor = SystemColors.ActiveBorder;
            PageBox.Location = new Point(12, 78);
            PageBox.Name = "PageBox";
            PageBox.Size = new Size(1190, 640);
            PageBox.TabIndex = 11;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1214, 721);
            Controls.Add(PageBox);
            Controls.Add(Pagesbut_device);
            Controls.Add(Pagesbut_复位);
            Controls.Add(Pagesbut_启动);
            Controls.Add(Pagesbut_暂停);
            Controls.Add(Pagesbut_登入);
            Controls.Add(Pagesbut_设置);
            Controls.Add(Pagesbut_数据);
            Controls.Add(Pagesbut_报警);
            Controls.Add(Pagesbut_图片);
            Controls.Add(Pagesbut_操作);
            Controls.Add(Pagesbut_首页);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(890, 550);
            Name = "MainForm";
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MainForm";
            Load += MainForm_Load;
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem showWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private Button Pagesbut_首页;
        private Button Pagesbut_操作;
        private Button Pagesbut_图片;
        private Button Pagesbut_报警;
        private Button Pagesbut_数据;
        private Button Pagesbut_设置;
        private Button Pagesbut_登入;
        private Button Pagesbut_暂停;
        private Button Pagesbut_启动;
        private Button Pagesbut_复位;
        private Button Pagesbut_device;
        private Panel PageBox;
    }
}