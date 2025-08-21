namespace WinFromFrame_KupaKuper
{
    partial class PageAlarm
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
            PageTable = new TabControl();
            tabPage_CurrentAlarm = new TabPage();
            tabPage_HistoryAlarm = new TabPage();
            PageTable.SuspendLayout();
            SuspendLayout();
            // 
            // PageTable
            // 
            PageTable.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            PageTable.Controls.Add(tabPage_CurrentAlarm);
            PageTable.Controls.Add(tabPage_HistoryAlarm);
            PageTable.Location = new Point(4, 3);
            PageTable.Name = "PageTable";
            PageTable.SelectedIndex = 0;
            PageTable.Size = new Size(1167, 595);
            PageTable.TabIndex = 1;
            // 
            // tabPage_CurrentAlarm
            // 
            tabPage_CurrentAlarm.Location = new Point(4, 26);
            tabPage_CurrentAlarm.Name = "tabPage_CurrentAlarm";
            tabPage_CurrentAlarm.Padding = new Padding(3);
            tabPage_CurrentAlarm.Size = new Size(1159, 565);
            tabPage_CurrentAlarm.TabIndex = 0;
            tabPage_CurrentAlarm.Text = "实时报警";
            tabPage_CurrentAlarm.UseVisualStyleBackColor = true;
            // 
            // tabPage_HistoryAlarm
            // 
            tabPage_HistoryAlarm.Location = new Point(4, 26);
            tabPage_HistoryAlarm.Name = "tabPage_HistoryAlarm";
            tabPage_HistoryAlarm.Padding = new Padding(3);
            tabPage_HistoryAlarm.Size = new Size(1159, 565);
            tabPage_HistoryAlarm.TabIndex = 1;
            tabPage_HistoryAlarm.Text = "历史报警";
            tabPage_HistoryAlarm.UseVisualStyleBackColor = true;
            // 
            // PageAlarm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1174, 601);
            Controls.Add(PageTable);
            Name = "PageAlarm";
            Text = "PageAlarm";
            PageTable.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl PageTable;
        private TabPage tabPage_CurrentAlarm;
        private TabPage tabPage_HistoryAlarm;
    }
}