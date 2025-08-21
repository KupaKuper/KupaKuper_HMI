namespace WinFromFrame_KupaKuper
{
    partial class PageHome
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
            tabPage_Home = new TabPage();
            PageTable.SuspendLayout();
            SuspendLayout();
            // 
            // PageTable
            // 
            PageTable.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            PageTable.Controls.Add(tabPage_Home);
            PageTable.Location = new Point(4, 3);
            PageTable.Name = "PageTable";
            PageTable.SelectedIndex = 0;
            PageTable.Size = new Size(1167, 595);
            PageTable.TabIndex = 1;
            // 
            // tabPage_Home
            // 
            tabPage_Home.Location = new Point(4, 26);
            tabPage_Home.Name = "tabPage_Home";
            tabPage_Home.Padding = new Padding(3);
            tabPage_Home.Size = new Size(1159, 565);
            tabPage_Home.TabIndex = 0;
            tabPage_Home.Text = "首页";
            tabPage_Home.UseVisualStyleBackColor = true;
            // 
            // PageHome
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1174, 601);
            Controls.Add(PageTable);
            Name = "PageHome";
            Text = "PageControl";
            Load += PageControl_Load;
            PageTable.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl PageTable;
        private TabPage tabPage_Home;
    }
}