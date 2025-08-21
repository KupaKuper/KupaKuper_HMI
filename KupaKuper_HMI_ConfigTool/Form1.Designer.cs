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
            but_OpenCsvFile = new Button();
            txtMessage = new TextBox();
            but_SaveExcel = new Button();
            but_SaveDevice = new Button();
            but_SaveNewCsv = new Button();
            but_OpenJsonFile = new Button();
            but_OnlyJson = new Button();
            but_SaveLanguage = new Button();
            SuspendLayout();
            // 
            // but_OpenCsvFile
            // 
            but_OpenCsvFile.Location = new Point(79, 131);
            but_OpenCsvFile.Name = "but_OpenCsvFile";
            but_OpenCsvFile.Size = new Size(93, 23);
            but_OpenCsvFile.TabIndex = 0;
            but_OpenCsvFile.Text = "读取配置表";
            but_OpenCsvFile.UseVisualStyleBackColor = true;
            but_OpenCsvFile.Click += but_OpenFile_Click;
            // 
            // txtMessage
            // 
            txtMessage.BackColor = SystemColors.ActiveCaptionText;
            txtMessage.Cursor = Cursors.IBeam;
            txtMessage.ForeColor = SystemColors.Window;
            txtMessage.ImeMode = ImeMode.Off;
            txtMessage.Location = new Point(265, 12);
            txtMessage.Multiline = true;
            txtMessage.Name = "txtMessage";
            txtMessage.ReadOnly = true;
            txtMessage.ScrollBars = ScrollBars.Horizontal;
            txtMessage.Size = new Size(523, 426);
            txtMessage.TabIndex = 1;
            // 
            // but_SaveExcel
            // 
            but_SaveExcel.Location = new Point(79, 270);
            but_SaveExcel.Name = "but_SaveExcel";
            but_SaveExcel.Size = new Size(93, 23);
            but_SaveExcel.TabIndex = 2;
            but_SaveExcel.Text = "保存为Excel";
            but_SaveExcel.UseVisualStyleBackColor = true;
            but_SaveExcel.Click += butSaveExcel_Click;
            // 
            // but_SaveDevice
            // 
            but_SaveDevice.Location = new Point(79, 241);
            but_SaveDevice.Name = "but_SaveDevice";
            but_SaveDevice.Size = new Size(93, 23);
            but_SaveDevice.TabIndex = 3;
            but_SaveDevice.Text = "生成设备文件";
            but_SaveDevice.UseVisualStyleBackColor = true;
            but_SaveDevice.Click += butSaveJson_Click;
            // 
            // but_SaveNewCsv
            // 
            but_SaveNewCsv.Location = new Point(79, 102);
            but_SaveNewCsv.Name = "but_SaveNewCsv";
            but_SaveNewCsv.Size = new Size(93, 23);
            but_SaveNewCsv.TabIndex = 4;
            but_SaveNewCsv.Text = "新建配置表";
            but_SaveNewCsv.UseVisualStyleBackColor = true;
            but_SaveNewCsv.Click += but_SaveNewCsv_Click;
            // 
            // but_OpenJsonFile
            // 
            but_OpenJsonFile.Location = new Point(79, 160);
            but_OpenJsonFile.Name = "but_OpenJsonFile";
            but_OpenJsonFile.Size = new Size(93, 23);
            but_OpenJsonFile.TabIndex = 5;
            but_OpenJsonFile.Text = "读取Json配置";
            but_OpenJsonFile.UseVisualStyleBackColor = true;
            but_OpenJsonFile.Click += but_OpenJsonFile_Click;
            // 
            // but_OnlyJson
            // 
            but_OnlyJson.Location = new Point(79, 299);
            but_OnlyJson.Name = "but_OnlyJson";
            but_OnlyJson.Size = new Size(93, 23);
            but_OnlyJson.TabIndex = 6;
            but_OnlyJson.Text = "仅保存Json";
            but_OnlyJson.UseVisualStyleBackColor = true;
            but_OnlyJson.Click += but_OnlyJson_Click;
            // 
            // but_SaveLanguage
            // 
            but_SaveLanguage.Location = new Point(79, 328);
            but_SaveLanguage.Name = "but_SaveLanguage";
            but_SaveLanguage.Size = new Size(93, 23);
            but_SaveLanguage.TabIndex = 7;
            but_SaveLanguage.Text = "仅生成语言包";
            but_SaveLanguage.UseVisualStyleBackColor = true;
            but_SaveLanguage.Click += but_SaveLanguage_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(but_SaveLanguage);
            Controls.Add(but_OnlyJson);
            Controls.Add(but_OpenJsonFile);
            Controls.Add(but_SaveNewCsv);
            Controls.Add(but_SaveDevice);
            Controls.Add(but_SaveExcel);
            Controls.Add(txtMessage);
            Controls.Add(but_OpenCsvFile);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

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
