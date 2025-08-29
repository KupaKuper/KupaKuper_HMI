namespace WinFromFrame_KupaKuper
{
    partial class PageData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PageData));
            PageTable = new TabControl();
            tabPage_DataRecord = new TabPage();
            tabPage_Gantry = new TabPage();
            PageTable.SuspendLayout();
            SuspendLayout();
            // 
            // PageTable
            // 
            PageTable.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            PageTable.Controls.Add(tabPage_DataRecord);
            PageTable.Controls.Add(tabPage_Gantry);
            PageTable.Location = new Point(4, 3);
            PageTable.Name = "PageTable";
            PageTable.SelectedIndex = 0;
            PageTable.Size = new Size(1167, 595);
            PageTable.TabIndex = 1;
            // 
            // tabPage_DataRecord
            // 
            tabPage_DataRecord.Location = new Point(4, 26);
            tabPage_DataRecord.Name = "tabPage_DataRecord";
            tabPage_DataRecord.Padding = new Padding(3);
            tabPage_DataRecord.Size = new Size(1159, 565);
            tabPage_DataRecord.TabIndex = 0;
            tabPage_DataRecord.Text = "生产数据";
            tabPage_DataRecord.UseVisualStyleBackColor = true;
            // 
            // tabPage_Gantry
            // 
            tabPage_Gantry.Location = new Point(4, 26);
            tabPage_Gantry.Name = "tabPage_Gantry";
            tabPage_Gantry.Padding = new Padding(3);
            tabPage_Gantry.Size = new Size(1159, 565);
            tabPage_Gantry.TabIndex = 1;
            tabPage_Gantry.Text = "设备数据";
            tabPage_Gantry.UseVisualStyleBackColor = true;
            // 
            // PageData
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1174, 601);
            Controls.Add(PageTable);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "PageData";
            Text = "PageData";
            PageTable.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl PageTable;
        private TabPage tabPage_DataRecord;
        private TabPage tabPage_Gantry;
    }
}