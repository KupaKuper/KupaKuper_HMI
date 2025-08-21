namespace WinFromFrame_KupaKuper
{
    partial class PageImage
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
            tabPage_Image = new TabPage();
            PageTable.SuspendLayout();
            SuspendLayout();
            // 
            // PageTable
            // 
            PageTable.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            PageTable.Controls.Add(tabPage_Image);
            PageTable.Location = new Point(4, 3);
            PageTable.Name = "PageTable";
            PageTable.SelectedIndex = 0;
            PageTable.Size = new Size(1167, 595);
            PageTable.TabIndex = 1;
            // 
            // tabPage_Image
            // 
            tabPage_Image.Location = new Point(4, 26);
            tabPage_Image.Name = "tabPage_Image";
            tabPage_Image.Padding = new Padding(3);
            tabPage_Image.Size = new Size(1159, 565);
            tabPage_Image.TabIndex = 0;
            tabPage_Image.Text = "图像显示";
            tabPage_Image.UseVisualStyleBackColor = true;
            // 
            // PageImage
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1174, 601);
            Controls.Add(PageTable);
            Name = "PageImage";
            Text = "PageImage";
            PageTable.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl PageTable;
        private TabPage tabPage_Image;
    }
}