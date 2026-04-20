namespace KupaKuper_HMI_ConfigTool
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panelMain = new Panel();
            lblTitle = new Label();
            grpFileOperation = new GroupBox();
            but_SaveNewCsv = new Button();
            but_OpenCsvFile = new Button();
            but_OpenJsonFile = new Button();
            grpDeviceGeneration = new GroupBox();
            but_SaveExcel = new Button();
            but_SaveDevice = new Button();
            grpOtherTools = new GroupBox();
            but_OnlyJson = new Button();
            but_SaveLanguage = new Button();
            panelRight = new Panel();
            lblDeviceInfo = new Label();
            panelDeviceInfo = new Panel();
            lblMessageTitle = new Label();
            panelMessage = new Panel();
            txtMessage = new TextBox();
            panelMain.SuspendLayout();
            grpFileOperation.SuspendLayout();
            grpDeviceGeneration.SuspendLayout();
            grpOtherTools.SuspendLayout();
            panelRight.SuspendLayout();
            panelMessage.SuspendLayout();
            SuspendLayout();
            // 
            // panelMain
            // 
            panelMain.BackColor = Color.FromArgb(240, 240, 240);
            panelMain.Controls.Add(lblTitle);
            panelMain.Controls.Add(grpFileOperation);
            panelMain.Controls.Add(grpDeviceGeneration);
            panelMain.Controls.Add(grpOtherTools);
            panelMain.Controls.Add(panelRight);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(0, 0);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(900, 550);
            panelMain.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.BackColor = Color.FromArgb(64, 158, 219);
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Font = new Font("Microsoft YaHei UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(0, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(900, 34);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "KupaKuper HMI 配置工具";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // grpFileOperation
            // 
            grpFileOperation.BackColor = Color.White;
            grpFileOperation.Controls.Add(but_SaveNewCsv);
            grpFileOperation.Controls.Add(but_OpenCsvFile);
            grpFileOperation.Controls.Add(but_OpenJsonFile);
            grpFileOperation.Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Bold);
            grpFileOperation.ForeColor = Color.FromArgb(64, 64, 64);
            grpFileOperation.Location = new Point(20, 37);
            grpFileOperation.Name = "grpFileOperation";
            grpFileOperation.Size = new Size(220, 200);
            grpFileOperation.TabIndex = 1;
            grpFileOperation.TabStop = false;
            grpFileOperation.Text = "📁 文件操作";
            // 
            // but_SaveNewCsv
            // 
            but_SaveNewCsv.BackColor = Color.FromArgb(76, 175, 80);
            but_SaveNewCsv.FlatAppearance.BorderSize = 0;
            but_SaveNewCsv.FlatStyle = FlatStyle.Flat;
            but_SaveNewCsv.Font = new Font("Microsoft YaHei UI", 9F);
            but_SaveNewCsv.ForeColor = Color.White;
            but_SaveNewCsv.Location = new Point(15, 30);
            but_SaveNewCsv.Name = "but_SaveNewCsv";
            but_SaveNewCsv.Size = new Size(190, 35);
            but_SaveNewCsv.TabIndex = 0;
            but_SaveNewCsv.Text = "🆕 新建配置表";
            but_SaveNewCsv.UseVisualStyleBackColor = false;
            but_SaveNewCsv.Click += but_SaveNewCsv_Click;
            // 
            // but_OpenCsvFile
            // 
            but_OpenCsvFile.BackColor = Color.FromArgb(64, 158, 219);
            but_OpenCsvFile.FlatAppearance.BorderSize = 0;
            but_OpenCsvFile.FlatStyle = FlatStyle.Flat;
            but_OpenCsvFile.Font = new Font("Microsoft YaHei UI", 9F);
            but_OpenCsvFile.ForeColor = Color.White;
            but_OpenCsvFile.Location = new Point(15, 70);
            but_OpenCsvFile.Name = "but_OpenCsvFile";
            but_OpenCsvFile.Size = new Size(190, 35);
            but_OpenCsvFile.TabIndex = 1;
            but_OpenCsvFile.Text = "📂 读取Excel配置";
            but_OpenCsvFile.UseVisualStyleBackColor = false;
            but_OpenCsvFile.Click += but_OpenFile_Click;
            // 
            // but_OpenJsonFile
            // 
            but_OpenJsonFile.BackColor = Color.FromArgb(64, 158, 219);
            but_OpenJsonFile.FlatAppearance.BorderSize = 0;
            but_OpenJsonFile.FlatStyle = FlatStyle.Flat;
            but_OpenJsonFile.Font = new Font("Microsoft YaHei UI", 9F);
            but_OpenJsonFile.ForeColor = Color.White;
            but_OpenJsonFile.Location = new Point(15, 110);
            but_OpenJsonFile.Name = "but_OpenJsonFile";
            but_OpenJsonFile.Size = new Size(190, 35);
            but_OpenJsonFile.TabIndex = 2;
            but_OpenJsonFile.Text = "📄 读取Json配置";
            but_OpenJsonFile.UseVisualStyleBackColor = false;
            but_OpenJsonFile.Click += but_OpenJsonFile_Click;
            // 
            // grpDeviceGeneration
            // 
            grpDeviceGeneration.BackColor = Color.White;
            grpDeviceGeneration.Controls.Add(but_SaveExcel);
            grpDeviceGeneration.Controls.Add(but_SaveDevice);
            grpDeviceGeneration.Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Bold);
            grpDeviceGeneration.ForeColor = Color.FromArgb(64, 64, 64);
            grpDeviceGeneration.Location = new Point(20, 243);
            grpDeviceGeneration.Name = "grpDeviceGeneration";
            grpDeviceGeneration.Size = new Size(220, 150);
            grpDeviceGeneration.TabIndex = 2;
            grpDeviceGeneration.TabStop = false;
            grpDeviceGeneration.Text = "⚙️ 设备生成";
            // 
            // but_SaveExcel
            // 
            but_SaveExcel.BackColor = Color.FromArgb(255, 152, 0);
            but_SaveExcel.FlatAppearance.BorderSize = 0;
            but_SaveExcel.FlatStyle = FlatStyle.Flat;
            but_SaveExcel.Font = new Font("Microsoft YaHei UI", 9F);
            but_SaveExcel.ForeColor = Color.White;
            but_SaveExcel.Location = new Point(15, 30);
            but_SaveExcel.Name = "but_SaveExcel";
            but_SaveExcel.Size = new Size(190, 35);
            but_SaveExcel.TabIndex = 3;
            but_SaveExcel.Text = "💾 保存为Excel";
            but_SaveExcel.UseVisualStyleBackColor = false;
            but_SaveExcel.Click += butSaveExcel_Click;
            // 
            // but_SaveDevice
            // 
            but_SaveDevice.BackColor = Color.FromArgb(156, 39, 176);
            but_SaveDevice.FlatAppearance.BorderSize = 0;
            but_SaveDevice.FlatStyle = FlatStyle.Flat;
            but_SaveDevice.Font = new Font("Microsoft YaHei UI", 9F);
            but_SaveDevice.ForeColor = Color.White;
            but_SaveDevice.Location = new Point(15, 70);
            but_SaveDevice.Name = "but_SaveDevice";
            but_SaveDevice.Size = new Size(190, 35);
            but_SaveDevice.TabIndex = 4;
            but_SaveDevice.Text = "🚀 生成设备文件";
            but_SaveDevice.UseVisualStyleBackColor = false;
            but_SaveDevice.Click += butSaveJson_Click;
            // 
            // grpOtherTools
            // 
            grpOtherTools.BackColor = Color.White;
            grpOtherTools.Controls.Add(but_OnlyJson);
            grpOtherTools.Controls.Add(but_SaveLanguage);
            grpOtherTools.Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Bold);
            grpOtherTools.ForeColor = Color.FromArgb(64, 64, 64);
            grpOtherTools.Location = new Point(20, 399);
            grpOtherTools.Name = "grpOtherTools";
            grpOtherTools.Size = new Size(220, 80);
            grpOtherTools.TabIndex = 3;
            grpOtherTools.TabStop = false;
            grpOtherTools.Text = "🔧 其他工具";
            // 
            // but_OnlyJson
            // 
            but_OnlyJson.BackColor = Color.FromArgb(255, 152, 0);
            but_OnlyJson.FlatAppearance.BorderSize = 0;
            but_OnlyJson.FlatStyle = FlatStyle.Flat;
            but_OnlyJson.Font = new Font("Microsoft YaHei UI", 9F);
            but_OnlyJson.ForeColor = Color.White;
            but_OnlyJson.Location = new Point(15, 30);
            but_OnlyJson.Name = "but_OnlyJson";
            but_OnlyJson.Size = new Size(190, 35);
            but_OnlyJson.TabIndex = 5;
            but_OnlyJson.Text = "📋 仅保存Json";
            but_OnlyJson.UseVisualStyleBackColor = false;
            but_OnlyJson.Click += but_OnlyJson_Click;
            // 
            // but_SaveLanguage
            // 
            but_SaveLanguage.BackColor = Color.FromArgb(0, 150, 136);
            but_SaveLanguage.FlatAppearance.BorderSize = 0;
            but_SaveLanguage.FlatStyle = FlatStyle.Flat;
            but_SaveLanguage.Font = new Font("Microsoft YaHei UI", 9F);
            but_SaveLanguage.ForeColor = Color.White;
            but_SaveLanguage.Location = new Point(15, 30);
            but_SaveLanguage.Name = "but_SaveLanguage";
            but_SaveLanguage.Size = new Size(190, 35);
            but_SaveLanguage.TabIndex = 6;
            but_SaveLanguage.Text = "🌐 仅生成语言包";
            but_SaveLanguage.UseVisualStyleBackColor = false;
            but_SaveLanguage.Click += but_SaveLanguage_Click;
            // 
            // panelRight
            // 
            panelRight.BackColor = Color.White;
            panelRight.Controls.Add(lblDeviceInfo);
            panelRight.Controls.Add(panelDeviceInfo);
            panelRight.Controls.Add(lblMessageTitle);
            panelRight.Controls.Add(panelMessage);
            panelRight.Location = new Point(260, 37);
            panelRight.Name = "panelRight";
            panelRight.Size = new Size(620, 501);
            panelRight.TabIndex = 4;
            // 
            // lblDeviceInfo
            // 
            lblDeviceInfo.AutoSize = true;
            lblDeviceInfo.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Bold);
            lblDeviceInfo.ForeColor = Color.FromArgb(64, 158, 219);
            lblDeviceInfo.Location = new Point(14, 0);
            lblDeviceInfo.Name = "lblDeviceInfo";
            lblDeviceInfo.Size = new Size(74, 22);
            lblDeviceInfo.TabIndex = 5;
            lblDeviceInfo.Text = "设备信息";
            // 
            // panelDeviceInfo
            // 
            panelDeviceInfo.BackColor = Color.Transparent;
            panelDeviceInfo.Location = new Point(14, 25);
            panelDeviceInfo.Name = "panelDeviceInfo";
            panelDeviceInfo.Size = new Size(591, 175);
            panelDeviceInfo.TabIndex = 7;
            // 
            // lblMessageTitle
            // 
            lblMessageTitle.AutoSize = true;
            lblMessageTitle.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Bold);
            lblMessageTitle.ForeColor = Color.FromArgb(64, 158, 219);
            lblMessageTitle.Location = new Point(15, 206);
            lblMessageTitle.Name = "lblMessageTitle";
            lblMessageTitle.Size = new Size(74, 22);
            lblMessageTitle.TabIndex = 6;
            lblMessageTitle.Text = "操作日志";
            // 
            // panelMessage
            // 
            panelMessage.BackColor = Color.FromArgb(250, 250, 250);
            panelMessage.BorderStyle = BorderStyle.FixedSingle;
            panelMessage.Controls.Add(txtMessage);
            panelMessage.Location = new Point(15, 231);
            panelMessage.Name = "panelMessage";
            panelMessage.Size = new Size(590, 267);
            panelMessage.TabIndex = 8;
            // 
            // txtMessage
            // 
            txtMessage.BackColor = Color.FromArgb(30, 30, 30);
            txtMessage.BorderStyle = BorderStyle.None;
            txtMessage.Cursor = Cursors.IBeam;
            txtMessage.Dock = DockStyle.Fill;
            txtMessage.Font = new Font("Consolas", 10F);
            txtMessage.ForeColor = Color.Lime;
            txtMessage.ImeMode = ImeMode.Off;
            txtMessage.Location = new Point(0, 0);
            txtMessage.Multiline = true;
            txtMessage.Name = "txtMessage";
            txtMessage.ReadOnly = true;
            txtMessage.ScrollBars = ScrollBars.Vertical;
            txtMessage.Size = new Size(588, 265);
            txtMessage.TabIndex = 9;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 550);
            Controls.Add(panelMain);
            MinimumSize = new Size(900, 550);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "KupaKuper HMI 配置工具";
            panelMain.ResumeLayout(false);
            grpFileOperation.ResumeLayout(false);
            grpDeviceGeneration.ResumeLayout(false);
            grpOtherTools.ResumeLayout(false);
            panelRight.ResumeLayout(false);
            panelRight.PerformLayout();
            panelMessage.ResumeLayout(false);
            panelMessage.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelMain;
        private GroupBox grpFileOperation;
        private GroupBox grpDeviceGeneration;
        private GroupBox grpOtherTools;
        private Panel panelRight;
        private Label lblTitle;
        private Label lblDeviceInfo;
        private Label lblMessageTitle;
        private Panel panelDeviceInfo;
        private Panel panelMessage;
        private Button but_OpenCsvFile;
        private TextBox txtMessage;
        private Button but_SaveExcel;
        private Button but_SaveDevice;
        private Button but_SaveNewCsv;
        private Button but_OpenJsonFile;
        private Button but_OnlyJson;
        private Button but_SaveLanguage;
    }
}