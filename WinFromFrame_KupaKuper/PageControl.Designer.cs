namespace WinFromFrame_KupaKuper
{
    partial class PageControl
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
            tabPage_IO = new TabPage();
            IoBox = new FlowLayoutPanel();
            but_Next = new Button();
            but_Previous = new Button();
            txt_PageNumber = new Label();
            but_Output = new Button();
            but_Input = new Button();
            tabPage_Cylinder = new TabPage();
            CylinderBox = new FlowLayoutPanel();
            CylinderNameBox = new FlowLayoutPanel();
            tabPage_Axis = new TabPage();
            tabPage_Parameter = new TabPage();
            PageTable.SuspendLayout();
            tabPage_IO.SuspendLayout();
            tabPage_Cylinder.SuspendLayout();
            SuspendLayout();
            // 
            // PageTable
            // 
            PageTable.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            PageTable.Controls.Add(tabPage_IO);
            PageTable.Controls.Add(tabPage_Cylinder);
            PageTable.Controls.Add(tabPage_Axis);
            PageTable.Controls.Add(tabPage_Parameter);
            PageTable.Location = new Point(4, 3);
            PageTable.Name = "PageTable";
            PageTable.SelectedIndex = 0;
            PageTable.Size = new Size(1167, 595);
            PageTable.TabIndex = 0;
            PageTable.Selected += PageTable_Selected;
            PageTable.Deselecting += PageTable_Deselecting;
            // 
            // tabPage_IO
            // 
            tabPage_IO.Controls.Add(IoBox);
            tabPage_IO.Controls.Add(but_Next);
            tabPage_IO.Controls.Add(but_Previous);
            tabPage_IO.Controls.Add(txt_PageNumber);
            tabPage_IO.Controls.Add(but_Output);
            tabPage_IO.Controls.Add(but_Input);
            tabPage_IO.Location = new Point(4, 26);
            tabPage_IO.Name = "tabPage_IO";
            tabPage_IO.Padding = new Padding(3);
            tabPage_IO.Size = new Size(1159, 565);
            tabPage_IO.TabIndex = 0;
            tabPage_IO.Text = "IO监控";
            tabPage_IO.UseVisualStyleBackColor = true;
            // 
            // IoBox
            // 
            IoBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            IoBox.AutoScroll = true;
            IoBox.Location = new Point(6, 48);
            IoBox.Name = "IoBox";
            IoBox.Size = new Size(1147, 511);
            IoBox.TabIndex = 5;
            // 
            // but_Next
            // 
            but_Next.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            but_Next.Location = new Point(1118, 6);
            but_Next.Name = "but_Next";
            but_Next.Size = new Size(35, 35);
            but_Next.TabIndex = 4;
            but_Next.Text = "▶";
            but_Next.UseVisualStyleBackColor = true;
            but_Next.Click += but_Next_Click;
            // 
            // but_Previous
            // 
            but_Previous.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            but_Previous.Location = new Point(1037, 6);
            but_Previous.Name = "but_Previous";
            but_Previous.Size = new Size(35, 35);
            but_Previous.TabIndex = 3;
            but_Previous.Text = "◀";
            but_Previous.UseVisualStyleBackColor = true;
            but_Previous.Click += but_Previous_Click;
            // 
            // txt_PageNumber
            // 
            txt_PageNumber.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txt_PageNumber.AutoSize = true;
            txt_PageNumber.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Bold, GraphicsUnit.Point, 134);
            txt_PageNumber.Location = new Point(1078, 13);
            txt_PageNumber.Name = "txt_PageNumber";
            txt_PageNumber.Size = new Size(34, 19);
            txt_PageNumber.TabIndex = 2;
            txt_PageNumber.Text = "1/1";
            // 
            // but_Output
            // 
            but_Output.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            but_Output.Location = new Point(926, 6);
            but_Output.Name = "but_Output";
            but_Output.Size = new Size(105, 36);
            but_Output.TabIndex = 1;
            but_Output.Text = "输出IO";
            but_Output.UseVisualStyleBackColor = true;
            but_Output.Click += but_Output_Click;
            // 
            // but_Input
            // 
            but_Input.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            but_Input.Location = new Point(815, 6);
            but_Input.Name = "but_Input";
            but_Input.Size = new Size(105, 36);
            but_Input.TabIndex = 0;
            but_Input.Text = "输入IO";
            but_Input.UseVisualStyleBackColor = true;
            but_Input.Click += but_Input_Click;
            // 
            // tabPage_Cylinder
            // 
            tabPage_Cylinder.Controls.Add(CylinderBox);
            tabPage_Cylinder.Controls.Add(CylinderNameBox);
            tabPage_Cylinder.Location = new Point(4, 26);
            tabPage_Cylinder.Name = "tabPage_Cylinder";
            tabPage_Cylinder.Padding = new Padding(3);
            tabPage_Cylinder.Size = new Size(1159, 565);
            tabPage_Cylinder.TabIndex = 1;
            tabPage_Cylinder.Text = "气缸手动";
            tabPage_Cylinder.UseVisualStyleBackColor = true;
            // 
            // CylinderBox
            // 
            CylinderBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            CylinderBox.AutoScroll = true;
            CylinderBox.Location = new Point(214, 6);
            CylinderBox.Name = "CylinderBox";
            CylinderBox.Size = new Size(939, 553);
            CylinderBox.TabIndex = 1;
            // 
            // CylinderNameBox
            // 
            CylinderNameBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            CylinderNameBox.AutoScroll = true;
            CylinderNameBox.Location = new Point(6, 6);
            CylinderNameBox.Name = "CylinderNameBox";
            CylinderNameBox.Size = new Size(202, 553);
            CylinderNameBox.TabIndex = 0;
            // 
            // tabPage_Axis
            // 
            tabPage_Axis.Location = new Point(4, 26);
            tabPage_Axis.Name = "tabPage_Axis";
            tabPage_Axis.Padding = new Padding(3);
            tabPage_Axis.Size = new Size(1159, 565);
            tabPage_Axis.TabIndex = 2;
            tabPage_Axis.Text = "伺服手动";
            tabPage_Axis.UseVisualStyleBackColor = true;
            // 
            // tabPage_Parameter
            // 
            tabPage_Parameter.Location = new Point(4, 26);
            tabPage_Parameter.Name = "tabPage_Parameter";
            tabPage_Parameter.Padding = new Padding(3);
            tabPage_Parameter.Size = new Size(1159, 565);
            tabPage_Parameter.TabIndex = 3;
            tabPage_Parameter.Text = "参数配置";
            tabPage_Parameter.UseVisualStyleBackColor = true;
            // 
            // PageControl
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1174, 601);
            Controls.Add(PageTable);
            Name = "PageControl";
            Text = "PageControl";
            Load += PageControl_Load;
            PageTable.ResumeLayout(false);
            tabPage_IO.ResumeLayout(false);
            tabPage_IO.PerformLayout();
            tabPage_Cylinder.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl PageTable;
        private TabPage tabPage_IO;
        private TabPage tabPage_Cylinder;
        private TabPage tabPage_Axis;
        private TabPage tabPage_Parameter;
        private Button but_Next;
        private Button but_Previous;
        private Label txt_PageNumber;
        private Button but_Output;
        private Button but_Input;
        private FlowLayoutPanel IoBox;
        private FlowLayoutPanel CylinderBox;
        private FlowLayoutPanel CylinderNameBox;
    }
}