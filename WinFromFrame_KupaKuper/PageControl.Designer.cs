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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PageControl));
            PageTable = new TabControl();
            tabPage_IO = new TabPage();
            IoBox = new FlowLayoutPanel();
            label23 = new Label();
            but_Next = new Button();
            but_Previous = new Button();
            txt_PageNumber = new Label();
            but_Output = new Button();
            but_Input = new Button();
            tabPage_Cylinder = new TabPage();
            CylinderBox = new FlowLayoutPanel();
            ex_cy_card = new Panel();
            ex_cy_alarmTimePanel = new TableLayoutPanel();
            ex_cy_homeAlarmTimeTextbox = new TextBox();
            ex_cy_workAlarmTimeTextbox = new TextBox();
            ex_cy_alarmTimeTxt = new Label();
            ex_cy_senserTimeLabel = new Label();
            ex_cy_senserTimePanel = new TableLayoutPanel();
            ex_cy_homeDone = new Panel();
            ex_cy_workDone = new Panel();
            ex_cy_homeDoneTimeTextbox = new TextBox();
            ex_cy_workDoneTimeTextbox = new TextBox();
            ex_cy_err = new Label();
            ex_cy_controlPanel = new TableLayoutPanel();
            ex_cy_workButton = new Button();
            ex_cy_workSenser = new Panel();
            ex_cy_homeButton = new Button();
            ex_cy_homeSenser = new Panel();
            ex_cy_name = new Label();
            CylinderNameBox = new FlowLayoutPanel();
            tabPage_Axis = new TabPage();
            splitContainer1 = new SplitContainer();
            AxesNameBox = new FlowLayoutPanel();
            AxisBox = new SplitContainer();
            AxisControlBox = new GroupBox();
            errorIndicator = new Panel();
            label17 = new Label();
            moveDone = new Panel();
            label16 = new Label();
            enableButton = new Button();
            relPosition = new TextBox();
            label15 = new Label();
            remberPosition = new Label();
            label13 = new Label();
            label12 = new Label();
            goRemberPositionButton = new Button();
            teachButton = new Button();
            label11 = new Label();
            label10 = new Label();
            label9 = new Label();
            goRelPositionButton = new Button();
            goAbsPositionButton = new Button();
            homeButton = new Button();
            resetButton = new Button();
            stopButton = new Button();
            jogNButton = new Button();
            jogPButton = new Button();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            negLimit = new Panel();
            label4 = new Label();
            orign = new Panel();
            label3 = new Label();
            posLimit = new Panel();
            label2 = new Label();
            busyIndicator = new Panel();
            label1 = new Label();
            powerIndicator = new Panel();
            positionValueLabel = new Label();
            positionPanel = new FlowLayoutPanel();
            panel1 = new Panel();
            button1 = new Button();
            textBox2 = new TextBox();
            label20 = new Label();
            textBox1 = new TextBox();
            label19 = new Label();
            label18 = new Label();
            label14 = new Label();
            tabPage_Parameter = new TabPage();
            PageTable.SuspendLayout();
            tabPage_IO.SuspendLayout();
            IoBox.SuspendLayout();
            tabPage_Cylinder.SuspendLayout();
            CylinderBox.SuspendLayout();
            ex_cy_card.SuspendLayout();
            ex_cy_alarmTimePanel.SuspendLayout();
            ex_cy_senserTimePanel.SuspendLayout();
            ex_cy_controlPanel.SuspendLayout();
            tabPage_Axis.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)AxisBox).BeginInit();
            AxisBox.Panel1.SuspendLayout();
            AxisBox.Panel2.SuspendLayout();
            AxisBox.SuspendLayout();
            AxisControlBox.SuspendLayout();
            positionPanel.SuspendLayout();
            panel1.SuspendLayout();
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
            IoBox.Controls.Add(label23);
            IoBox.Location = new Point(6, 48);
            IoBox.Name = "IoBox";
            IoBox.Size = new Size(1147, 511);
            IoBox.TabIndex = 5;
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Location = new Point(3, 0);
            label23.Name = "label23";
            label23.Size = new Size(50, 17);
            label23.TabIndex = 0;
            label23.Text = "label23";
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
            CylinderBox.Controls.Add(ex_cy_card);
            CylinderBox.Location = new Point(214, 6);
            CylinderBox.Name = "CylinderBox";
            CylinderBox.Size = new Size(939, 553);
            CylinderBox.TabIndex = 1;
            // 
            // ex_cy_card
            // 
            ex_cy_card.BackColor = Color.Transparent;
            ex_cy_card.BorderStyle = BorderStyle.FixedSingle;
            ex_cy_card.Controls.Add(ex_cy_alarmTimePanel);
            ex_cy_card.Controls.Add(ex_cy_alarmTimeTxt);
            ex_cy_card.Controls.Add(ex_cy_senserTimeLabel);
            ex_cy_card.Controls.Add(ex_cy_senserTimePanel);
            ex_cy_card.Controls.Add(ex_cy_err);
            ex_cy_card.Controls.Add(ex_cy_controlPanel);
            ex_cy_card.Controls.Add(ex_cy_name);
            ex_cy_card.Location = new Point(3, 3);
            ex_cy_card.Name = "ex_cy_card";
            ex_cy_card.Size = new Size(325, 230);
            ex_cy_card.TabIndex = 0;
            // 
            // ex_cy_alarmTimePanel
            // 
            ex_cy_alarmTimePanel.ColumnCount = 2;
            ex_cy_alarmTimePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            ex_cy_alarmTimePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            ex_cy_alarmTimePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            ex_cy_alarmTimePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            ex_cy_alarmTimePanel.Controls.Add(ex_cy_homeAlarmTimeTextbox, 1, 0);
            ex_cy_alarmTimePanel.Controls.Add(ex_cy_workAlarmTimeTextbox, 0, 0);
            ex_cy_alarmTimePanel.Location = new Point(3, 185);
            ex_cy_alarmTimePanel.Name = "ex_cy_alarmTimePanel";
            ex_cy_alarmTimePanel.RowCount = 1;
            ex_cy_alarmTimePanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            ex_cy_alarmTimePanel.Size = new Size(317, 36);
            ex_cy_alarmTimePanel.TabIndex = 43;
            // 
            // ex_cy_homeAlarmTimeTextbox
            // 
            ex_cy_homeAlarmTimeTextbox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ex_cy_homeAlarmTimeTextbox.Font = new Font("Microsoft YaHei UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 134);
            ex_cy_homeAlarmTimeTextbox.Location = new Point(161, 3);
            ex_cy_homeAlarmTimeTextbox.Name = "ex_cy_homeAlarmTimeTextbox";
            ex_cy_homeAlarmTimeTextbox.Size = new Size(153, 32);
            ex_cy_homeAlarmTimeTextbox.TabIndex = 34;
            // 
            // ex_cy_workAlarmTimeTextbox
            // 
            ex_cy_workAlarmTimeTextbox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ex_cy_workAlarmTimeTextbox.Font = new Font("Microsoft YaHei UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 134);
            ex_cy_workAlarmTimeTextbox.Location = new Point(3, 3);
            ex_cy_workAlarmTimeTextbox.Name = "ex_cy_workAlarmTimeTextbox";
            ex_cy_workAlarmTimeTextbox.Size = new Size(152, 32);
            ex_cy_workAlarmTimeTextbox.TabIndex = 33;
            // 
            // ex_cy_alarmTimeTxt
            // 
            ex_cy_alarmTimeTxt.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ex_cy_alarmTimeTxt.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 134);
            ex_cy_alarmTimeTxt.Location = new Point(3, 159);
            ex_cy_alarmTimeTxt.Name = "ex_cy_alarmTimeTxt";
            ex_cy_alarmTimeTxt.Size = new Size(317, 23);
            ex_cy_alarmTimeTxt.TabIndex = 42;
            ex_cy_alarmTimeTxt.Text = "报警触发延时";
            ex_cy_alarmTimeTxt.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // ex_cy_senserTimeLabel
            // 
            ex_cy_senserTimeLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ex_cy_senserTimeLabel.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 134);
            ex_cy_senserTimeLabel.Location = new Point(3, 94);
            ex_cy_senserTimeLabel.Name = "ex_cy_senserTimeLabel";
            ex_cy_senserTimeLabel.Size = new Size(317, 23);
            ex_cy_senserTimeLabel.TabIndex = 41;
            ex_cy_senserTimeLabel.Text = "到位信号延时";
            ex_cy_senserTimeLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // ex_cy_senserTimePanel
            // 
            ex_cy_senserTimePanel.ColumnCount = 4;
            ex_cy_senserTimePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            ex_cy_senserTimePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            ex_cy_senserTimePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            ex_cy_senserTimePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            ex_cy_senserTimePanel.Controls.Add(ex_cy_homeDone, 3, 0);
            ex_cy_senserTimePanel.Controls.Add(ex_cy_workDone, 0, 0);
            ex_cy_senserTimePanel.Controls.Add(ex_cy_homeDoneTimeTextbox, 2, 0);
            ex_cy_senserTimePanel.Controls.Add(ex_cy_workDoneTimeTextbox, 1, 0);
            ex_cy_senserTimePanel.Location = new Point(3, 120);
            ex_cy_senserTimePanel.Name = "ex_cy_senserTimePanel";
            ex_cy_senserTimePanel.RowCount = 1;
            ex_cy_senserTimePanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            ex_cy_senserTimePanel.Size = new Size(316, 36);
            ex_cy_senserTimePanel.TabIndex = 2;
            // 
            // ex_cy_homeDone
            // 
            ex_cy_homeDone.BorderStyle = BorderStyle.FixedSingle;
            ex_cy_homeDone.Dock = DockStyle.Fill;
            ex_cy_homeDone.Location = new Point(286, 3);
            ex_cy_homeDone.Name = "ex_cy_homeDone";
            ex_cy_homeDone.Size = new Size(27, 30);
            ex_cy_homeDone.TabIndex = 36;
            // 
            // ex_cy_workDone
            // 
            ex_cy_workDone.BorderStyle = BorderStyle.FixedSingle;
            ex_cy_workDone.Dock = DockStyle.Fill;
            ex_cy_workDone.Location = new Point(3, 3);
            ex_cy_workDone.Name = "ex_cy_workDone";
            ex_cy_workDone.Size = new Size(25, 30);
            ex_cy_workDone.TabIndex = 35;
            // 
            // ex_cy_homeDoneTimeTextbox
            // 
            ex_cy_homeDoneTimeTextbox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ex_cy_homeDoneTimeTextbox.Font = new Font("Microsoft YaHei UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 134);
            ex_cy_homeDoneTimeTextbox.Location = new Point(160, 3);
            ex_cy_homeDoneTimeTextbox.Name = "ex_cy_homeDoneTimeTextbox";
            ex_cy_homeDoneTimeTextbox.Size = new Size(120, 32);
            ex_cy_homeDoneTimeTextbox.TabIndex = 34;
            // 
            // ex_cy_workDoneTimeTextbox
            // 
            ex_cy_workDoneTimeTextbox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ex_cy_workDoneTimeTextbox.Font = new Font("Microsoft YaHei UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 134);
            ex_cy_workDoneTimeTextbox.Location = new Point(34, 3);
            ex_cy_workDoneTimeTextbox.Name = "ex_cy_workDoneTimeTextbox";
            ex_cy_workDoneTimeTextbox.Size = new Size(120, 32);
            ex_cy_workDoneTimeTextbox.TabIndex = 33;
            // 
            // ex_cy_err
            // 
            ex_cy_err.AutoSize = true;
            ex_cy_err.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 134);
            ex_cy_err.ForeColor = Color.Red;
            ex_cy_err.Location = new Point(3, 0);
            ex_cy_err.Name = "ex_cy_err";
            ex_cy_err.Size = new Size(38, 17);
            ex_cy_err.TabIndex = 40;
            ex_cy_err.Text = "Error";
            // 
            // ex_cy_controlPanel
            // 
            ex_cy_controlPanel.ColumnCount = 4;
            ex_cy_controlPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            ex_cy_controlPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            ex_cy_controlPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            ex_cy_controlPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            ex_cy_controlPanel.Controls.Add(ex_cy_workButton, 1, 0);
            ex_cy_controlPanel.Controls.Add(ex_cy_workSenser, 0, 0);
            ex_cy_controlPanel.Controls.Add(ex_cy_homeButton, 2, 0);
            ex_cy_controlPanel.Controls.Add(ex_cy_homeSenser, 3, 0);
            ex_cy_controlPanel.Location = new Point(3, 31);
            ex_cy_controlPanel.Name = "ex_cy_controlPanel";
            ex_cy_controlPanel.RowCount = 1;
            ex_cy_controlPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            ex_cy_controlPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            ex_cy_controlPanel.Size = new Size(316, 60);
            ex_cy_controlPanel.TabIndex = 1;
            // 
            // ex_cy_workButton
            // 
            ex_cy_workButton.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ex_cy_workButton.BackColor = Color.Transparent;
            ex_cy_workButton.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Bold, GraphicsUnit.Point, 134);
            ex_cy_workButton.Location = new Point(34, 3);
            ex_cy_workButton.Name = "ex_cy_workButton";
            ex_cy_workButton.Size = new Size(120, 54);
            ex_cy_workButton.TabIndex = 21;
            ex_cy_workButton.Text = "伸出";
            ex_cy_workButton.UseVisualStyleBackColor = false;
            // 
            // ex_cy_workSenser
            // 
            ex_cy_workSenser.BorderStyle = BorderStyle.FixedSingle;
            ex_cy_workSenser.Dock = DockStyle.Fill;
            ex_cy_workSenser.Location = new Point(3, 3);
            ex_cy_workSenser.Name = "ex_cy_workSenser";
            ex_cy_workSenser.Size = new Size(25, 54);
            ex_cy_workSenser.TabIndex = 24;
            // 
            // ex_cy_homeButton
            // 
            ex_cy_homeButton.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ex_cy_homeButton.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Bold, GraphicsUnit.Point, 134);
            ex_cy_homeButton.Location = new Point(160, 3);
            ex_cy_homeButton.Name = "ex_cy_homeButton";
            ex_cy_homeButton.Size = new Size(120, 54);
            ex_cy_homeButton.TabIndex = 22;
            ex_cy_homeButton.Text = "缩回";
            ex_cy_homeButton.UseVisualStyleBackColor = true;
            // 
            // ex_cy_homeSenser
            // 
            ex_cy_homeSenser.BorderStyle = BorderStyle.FixedSingle;
            ex_cy_homeSenser.Dock = DockStyle.Fill;
            ex_cy_homeSenser.Location = new Point(286, 3);
            ex_cy_homeSenser.Name = "ex_cy_homeSenser";
            ex_cy_homeSenser.Size = new Size(27, 54);
            ex_cy_homeSenser.TabIndex = 24;
            // 
            // ex_cy_name
            // 
            ex_cy_name.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ex_cy_name.Font = new Font("Microsoft YaHei UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 134);
            ex_cy_name.Location = new Point(3, 0);
            ex_cy_name.Name = "ex_cy_name";
            ex_cy_name.Size = new Size(317, 28);
            ex_cy_name.TabIndex = 0;
            ex_cy_name.Text = "|";
            ex_cy_name.TextAlign = ContentAlignment.MiddleCenter;
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
            tabPage_Axis.Controls.Add(splitContainer1);
            tabPage_Axis.Location = new Point(4, 26);
            tabPage_Axis.Name = "tabPage_Axis";
            tabPage_Axis.Padding = new Padding(3);
            tabPage_Axis.Size = new Size(1159, 565);
            tabPage_Axis.TabIndex = 2;
            tabPage_Axis.Text = "伺服手动";
            tabPage_Axis.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.FixedPanel = FixedPanel.Panel1;
            splitContainer1.Location = new Point(3, 3);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(AxesNameBox);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(AxisBox);
            splitContainer1.Size = new Size(1153, 559);
            splitContainer1.SplitterDistance = 206;
            splitContainer1.TabIndex = 5;
            // 
            // AxesNameBox
            // 
            AxesNameBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            AxesNameBox.AutoScroll = true;
            AxesNameBox.Location = new Point(0, 0);
            AxesNameBox.Name = "AxesNameBox";
            AxesNameBox.Size = new Size(206, 559);
            AxesNameBox.TabIndex = 2;
            // 
            // AxisBox
            // 
            AxisBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            AxisBox.Location = new Point(0, 0);
            AxisBox.Name = "AxisBox";
            AxisBox.Orientation = Orientation.Horizontal;
            // 
            // AxisBox.Panel1
            // 
            AxisBox.Panel1.Controls.Add(AxisControlBox);
            // 
            // AxisBox.Panel2
            // 
            AxisBox.Panel2.Controls.Add(positionPanel);
            AxisBox.Size = new Size(943, 559);
            AxisBox.SplitterDistance = 278;
            AxisBox.TabIndex = 4;
            // 
            // AxisControlBox
            // 
            AxisControlBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            AxisControlBox.Controls.Add(errorIndicator);
            AxisControlBox.Controls.Add(label17);
            AxisControlBox.Controls.Add(moveDone);
            AxisControlBox.Controls.Add(label16);
            AxisControlBox.Controls.Add(enableButton);
            AxisControlBox.Controls.Add(relPosition);
            AxisControlBox.Controls.Add(label15);
            AxisControlBox.Controls.Add(remberPosition);
            AxisControlBox.Controls.Add(label13);
            AxisControlBox.Controls.Add(label12);
            AxisControlBox.Controls.Add(goRemberPositionButton);
            AxisControlBox.Controls.Add(teachButton);
            AxisControlBox.Controls.Add(label11);
            AxisControlBox.Controls.Add(label10);
            AxisControlBox.Controls.Add(label9);
            AxisControlBox.Controls.Add(goRelPositionButton);
            AxisControlBox.Controls.Add(goAbsPositionButton);
            AxisControlBox.Controls.Add(homeButton);
            AxisControlBox.Controls.Add(resetButton);
            AxisControlBox.Controls.Add(stopButton);
            AxisControlBox.Controls.Add(jogNButton);
            AxisControlBox.Controls.Add(jogPButton);
            AxisControlBox.Controls.Add(label8);
            AxisControlBox.Controls.Add(label7);
            AxisControlBox.Controls.Add(label6);
            AxisControlBox.Controls.Add(label5);
            AxisControlBox.Controls.Add(negLimit);
            AxisControlBox.Controls.Add(label4);
            AxisControlBox.Controls.Add(orign);
            AxisControlBox.Controls.Add(label3);
            AxisControlBox.Controls.Add(posLimit);
            AxisControlBox.Controls.Add(label2);
            AxisControlBox.Controls.Add(busyIndicator);
            AxisControlBox.Controls.Add(label1);
            AxisControlBox.Controls.Add(powerIndicator);
            AxisControlBox.Controls.Add(positionValueLabel);
            AxisControlBox.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 134);
            AxisControlBox.Location = new Point(3, 3);
            AxisControlBox.Name = "AxisControlBox";
            AxisControlBox.Size = new Size(937, 272);
            AxisControlBox.TabIndex = 3;
            AxisControlBox.TabStop = false;
            AxisControlBox.Text = "测试界面测试轴1";
            // 
            // errorIndicator
            // 
            errorIndicator.BorderStyle = BorderStyle.FixedSingle;
            errorIndicator.Location = new Point(408, 34);
            errorIndicator.Name = "errorIndicator";
            errorIndicator.Size = new Size(20, 20);
            errorIndicator.TabIndex = 5;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Bold, GraphicsUnit.Point, 134);
            label17.Location = new Point(319, 35);
            label17.Name = "label17";
            label17.Size = new Size(83, 19);
            label17.TabIndex = 35;
            label17.Text = "轴报警状态:";
            label17.TextAlign = ContentAlignment.MiddleRight;
            // 
            // moveDone
            // 
            moveDone.BorderStyle = BorderStyle.FixedSingle;
            moveDone.Location = new Point(83, 118);
            moveDone.Name = "moveDone";
            moveDone.Size = new Size(20, 20);
            moveDone.TabIndex = 3;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Bold, GraphicsUnit.Point, 134);
            label16.Location = new Point(8, 118);
            label16.Name = "label16";
            label16.Size = new Size(69, 19);
            label16.TabIndex = 34;
            label16.Text = "定位完成:";
            label16.TextAlign = ContentAlignment.MiddleRight;
            // 
            // enableButton
            // 
            enableButton.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Bold, GraphicsUnit.Point, 134);
            enableButton.Location = new Point(123, 27);
            enableButton.Name = "enableButton";
            enableButton.Size = new Size(75, 35);
            enableButton.TabIndex = 33;
            enableButton.Text = "使能";
            enableButton.UseVisualStyleBackColor = true;
            // 
            // relPosition
            // 
            relPosition.Anchor = AnchorStyles.Bottom;
            relPosition.Font = new Font("Microsoft YaHei UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 134);
            relPosition.Location = new Point(466, 195);
            relPosition.Name = "relPosition";
            relPosition.Size = new Size(100, 32);
            relPosition.TabIndex = 32;
            // 
            // label15
            // 
            label15.Anchor = AnchorStyles.Bottom;
            label15.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Bold, GraphicsUnit.Point, 134);
            label15.Location = new Point(260, 199);
            label15.Name = "label15";
            label15.Size = new Size(200, 20);
            label15.TabIndex = 31;
            label15.Text = "相对定位距离:";
            label15.TextAlign = ContentAlignment.MiddleRight;
            // 
            // remberPosition
            // 
            remberPosition.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            remberPosition.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 134);
            remberPosition.Location = new Point(81, 239);
            remberPosition.Name = "remberPosition";
            remberPosition.Size = new Size(82, 20);
            remberPosition.TabIndex = 30;
            remberPosition.Text = "9999.999";
            remberPosition.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label13
            // 
            label13.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label13.AutoSize = true;
            label13.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Bold, GraphicsUnit.Point, 134);
            label13.Location = new Point(6, 240);
            label13.Name = "label13";
            label13.Size = new Size(69, 19);
            label13.TabIndex = 29;
            label13.Text = "记忆位置:";
            label13.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            label12.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label12.AutoSize = true;
            label12.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Bold, GraphicsUnit.Point, 134);
            label12.Location = new Point(6, 200);
            label12.Name = "label12";
            label12.Size = new Size(97, 19);
            label12.TabIndex = 28;
            label12.Text = "返回记忆位置:";
            label12.TextAlign = ContentAlignment.MiddleRight;
            // 
            // goRemberPositionButton
            // 
            goRemberPositionButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            goRemberPositionButton.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Bold, GraphicsUnit.Point, 134);
            goRemberPositionButton.Location = new Point(109, 192);
            goRemberPositionButton.Name = "goRemberPositionButton";
            goRemberPositionButton.Size = new Size(75, 35);
            goRemberPositionButton.TabIndex = 27;
            goRemberPositionButton.Text = "回记忆位";
            goRemberPositionButton.UseVisualStyleBackColor = true;
            // 
            // teachButton
            // 
            teachButton.Anchor = AnchorStyles.Bottom;
            teachButton.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Bold, GraphicsUnit.Point, 134);
            teachButton.Location = new Point(466, 233);
            teachButton.Name = "teachButton";
            teachButton.Size = new Size(75, 35);
            teachButton.TabIndex = 26;
            teachButton.Text = "示教";
            teachButton.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            label11.Anchor = AnchorStyles.Bottom;
            label11.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Bold, GraphicsUnit.Point, 134);
            label11.Location = new Point(260, 240);
            label11.Name = "label11";
            label11.Size = new Size(200, 20);
            label11.TabIndex = 25;
            label11.Text = "示教当前点位:";
            label11.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            label10.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            label10.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Bold, GraphicsUnit.Point, 134);
            label10.Location = new Point(547, 199);
            label10.Name = "label10";
            label10.Size = new Size(300, 20);
            label10.TabIndex = 24;
            label10.Text = "轴相对定位:";
            label10.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            label9.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            label9.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Bold, GraphicsUnit.Point, 134);
            label9.Location = new Point(547, 240);
            label9.Name = "label9";
            label9.Size = new Size(300, 20);
            label9.TabIndex = 23;
            label9.Text = "轴绝对定位:";
            label9.TextAlign = ContentAlignment.MiddleRight;
            // 
            // goRelPositionButton
            // 
            goRelPositionButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            goRelPositionButton.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Bold, GraphicsUnit.Point, 134);
            goRelPositionButton.Location = new Point(853, 192);
            goRelPositionButton.Name = "goRelPositionButton";
            goRelPositionButton.Size = new Size(75, 35);
            goRelPositionButton.TabIndex = 22;
            goRelPositionButton.Text = "相对定位";
            goRelPositionButton.UseVisualStyleBackColor = true;
            // 
            // goAbsPositionButton
            // 
            goAbsPositionButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            goAbsPositionButton.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Bold, GraphicsUnit.Point, 134);
            goAbsPositionButton.Location = new Point(853, 233);
            goAbsPositionButton.Name = "goAbsPositionButton";
            goAbsPositionButton.Size = new Size(75, 35);
            goAbsPositionButton.TabIndex = 21;
            goAbsPositionButton.Text = "绝对定位";
            goAbsPositionButton.UseVisualStyleBackColor = true;
            // 
            // homeButton
            // 
            homeButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            homeButton.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Bold, GraphicsUnit.Point, 134);
            homeButton.Location = new Point(853, 110);
            homeButton.Name = "homeButton";
            homeButton.Size = new Size(75, 35);
            homeButton.TabIndex = 20;
            homeButton.Text = "回原";
            homeButton.UseVisualStyleBackColor = true;
            // 
            // resetButton
            // 
            resetButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            resetButton.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Bold, GraphicsUnit.Point, 134);
            resetButton.Location = new Point(853, 69);
            resetButton.Name = "resetButton";
            resetButton.Size = new Size(75, 35);
            resetButton.TabIndex = 19;
            resetButton.Text = "复位";
            resetButton.UseVisualStyleBackColor = true;
            // 
            // stopButton
            // 
            stopButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            stopButton.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Bold, GraphicsUnit.Point, 134);
            stopButton.Location = new Point(853, 28);
            stopButton.Name = "stopButton";
            stopButton.Size = new Size(75, 35);
            stopButton.TabIndex = 18;
            stopButton.Text = "停止";
            stopButton.UseVisualStyleBackColor = true;
            // 
            // jogNButton
            // 
            jogNButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            jogNButton.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Bold, GraphicsUnit.Point, 134);
            jogNButton.Location = new Point(853, 151);
            jogNButton.Name = "jogNButton";
            jogNButton.Size = new Size(75, 35);
            jogNButton.TabIndex = 17;
            jogNButton.Text = "Jog-";
            jogNButton.UseVisualStyleBackColor = true;
            // 
            // jogPButton
            // 
            jogPButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            jogPButton.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Bold, GraphicsUnit.Point, 134);
            jogPButton.Location = new Point(772, 151);
            jogPButton.Name = "jogPButton";
            jogPButton.Size = new Size(75, 35);
            jogPButton.TabIndex = 16;
            jogPButton.Text = "Jog+";
            jogPButton.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label8.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Bold, GraphicsUnit.Point, 134);
            label8.Location = new Point(547, 117);
            label8.Name = "label8";
            label8.Size = new Size(300, 20);
            label8.TabIndex = 15;
            label8.Text = "轴回原点:";
            label8.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label7.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Bold, GraphicsUnit.Point, 134);
            label7.Location = new Point(547, 76);
            label7.Name = "label7";
            label7.Size = new Size(300, 20);
            label7.TabIndex = 14;
            label7.Text = "轴复位:";
            label7.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label6.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Bold, GraphicsUnit.Point, 134);
            label6.Location = new Point(547, 35);
            label6.Name = "label6";
            label6.Size = new Size(300, 20);
            label6.TabIndex = 13;
            label6.Text = "轴停止:";
            label6.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Bold, GraphicsUnit.Point, 134);
            label5.Location = new Point(168, 77);
            label5.Name = "label5";
            label5.Size = new Size(55, 19);
            label5.TabIndex = 11;
            label5.Text = "负极限:";
            // 
            // negLimit
            // 
            negLimit.BorderStyle = BorderStyle.FixedSingle;
            negLimit.Location = new Point(229, 77);
            negLimit.Name = "negLimit";
            negLimit.Size = new Size(20, 20);
            negLimit.TabIndex = 10;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Bold, GraphicsUnit.Point, 134);
            label4.Location = new Point(95, 77);
            label4.Name = "label4";
            label4.Size = new Size(41, 19);
            label4.TabIndex = 9;
            label4.Text = "原点:";
            // 
            // orign
            // 
            orign.BorderStyle = BorderStyle.FixedSingle;
            orign.Location = new Point(142, 77);
            orign.Name = "orign";
            orign.Size = new Size(20, 20);
            orign.TabIndex = 8;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Bold, GraphicsUnit.Point, 134);
            label3.Location = new Point(8, 77);
            label3.Name = "label3";
            label3.Size = new Size(55, 19);
            label3.TabIndex = 7;
            label3.Text = "正极限:";
            // 
            // posLimit
            // 
            posLimit.BorderStyle = BorderStyle.FixedSingle;
            posLimit.Location = new Point(69, 77);
            posLimit.Name = "posLimit";
            posLimit.Size = new Size(20, 20);
            posLimit.TabIndex = 6;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Bold, GraphicsUnit.Point, 134);
            label2.Location = new Point(204, 35);
            label2.Name = "label2";
            label2.Size = new Size(83, 19);
            label2.TabIndex = 5;
            label2.Text = "轴忙碌状态:";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // busyIndicator
            // 
            busyIndicator.BorderStyle = BorderStyle.FixedSingle;
            busyIndicator.Location = new Point(293, 34);
            busyIndicator.Name = "busyIndicator";
            busyIndicator.Size = new Size(20, 20);
            busyIndicator.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Bold, GraphicsUnit.Point, 134);
            label1.Location = new Point(8, 35);
            label1.Name = "label1";
            label1.Size = new Size(83, 19);
            label1.TabIndex = 3;
            label1.Text = "轴使能状态:";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // powerIndicator
            // 
            powerIndicator.BorderStyle = BorderStyle.FixedSingle;
            powerIndicator.Location = new Point(97, 34);
            powerIndicator.Name = "powerIndicator";
            powerIndicator.Size = new Size(20, 20);
            powerIndicator.TabIndex = 2;
            // 
            // positionValueLabel
            // 
            positionValueLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            positionValueLabel.BackColor = Color.White;
            positionValueLabel.Font = new Font("Microsoft YaHei UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 134);
            positionValueLabel.ImageAlign = ContentAlignment.MiddleRight;
            positionValueLabel.Location = new Point(838, 0);
            positionValueLabel.Name = "positionValueLabel";
            positionValueLabel.Size = new Size(90, 23);
            positionValueLabel.TabIndex = 1;
            positionValueLabel.Text = "9999.999";
            positionValueLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // positionPanel
            // 
            positionPanel.AutoScroll = true;
            positionPanel.Controls.Add(panel1);
            positionPanel.Dock = DockStyle.Fill;
            positionPanel.Location = new Point(0, 0);
            positionPanel.Margin = new Padding(3, 3, 10, 3);
            positionPanel.Name = "positionPanel";
            positionPanel.Size = new Size(943, 277);
            positionPanel.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(button1);
            panel1.Controls.Add(textBox2);
            panel1.Controls.Add(label20);
            panel1.Controls.Add(textBox1);
            panel1.Controls.Add(label19);
            panel1.Controls.Add(label18);
            panel1.Controls.Add(label14);
            panel1.Location = new Point(3, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(934, 35);
            panel1.TabIndex = 0;
            // 
            // button1
            // 
            button1.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 134);
            button1.Location = new Point(854, 2);
            button1.Name = "button1";
            button1.Size = new Size(75, 30);
            button1.TabIndex = 6;
            button1.Text = "定位";
            button1.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            textBox2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            textBox2.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Bold, GraphicsUnit.Point, 134);
            textBox2.Location = new Point(748, 5);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(100, 25);
            textBox2.TabIndex = 5;
            textBox2.Text = "9999.999";
            textBox2.TextAlign = HorizontalAlignment.Center;
            // 
            // label20
            // 
            label20.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label20.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 134);
            label20.Location = new Point(683, 0);
            label20.Name = "label20";
            label20.Size = new Size(59, 33);
            label20.TabIndex = 4;
            label20.Text = "速度:";
            label20.TextAlign = ContentAlignment.MiddleRight;
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            textBox1.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Bold, GraphicsUnit.Point, 134);
            textBox1.Location = new Point(577, 4);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 25);
            textBox1.TabIndex = 3;
            textBox1.Text = "9999.999";
            textBox1.TextAlign = HorizontalAlignment.Center;
            // 
            // label19
            // 
            label19.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label19.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 134);
            label19.Location = new Point(512, 1);
            label19.Name = "label19";
            label19.Size = new Size(59, 33);
            label19.TabIndex = 2;
            label19.Text = "位置:";
            label19.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label18
            // 
            label18.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label18.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 134);
            label18.Location = new Point(58, 0);
            label18.Name = "label18";
            label18.Size = new Size(448, 33);
            label18.TabIndex = 1;
            label18.Text = "测试点位测试点位_1";
            label18.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label14
            // 
            label14.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label14.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 134);
            label14.Location = new Point(3, 0);
            label14.Name = "label14";
            label14.Size = new Size(49, 33);
            label14.TabIndex = 0;
            label14.Text = "1";
            label14.TextAlign = ContentAlignment.MiddleCenter;
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
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "PageControl";
            Text = "PageControl";
            Load += PageControl_Load;
            PageTable.ResumeLayout(false);
            tabPage_IO.ResumeLayout(false);
            tabPage_IO.PerformLayout();
            IoBox.ResumeLayout(false);
            IoBox.PerformLayout();
            tabPage_Cylinder.ResumeLayout(false);
            CylinderBox.ResumeLayout(false);
            ex_cy_card.ResumeLayout(false);
            ex_cy_card.PerformLayout();
            ex_cy_alarmTimePanel.ResumeLayout(false);
            ex_cy_alarmTimePanel.PerformLayout();
            ex_cy_senserTimePanel.ResumeLayout(false);
            ex_cy_senserTimePanel.PerformLayout();
            ex_cy_controlPanel.ResumeLayout(false);
            tabPage_Axis.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            AxisBox.Panel1.ResumeLayout(false);
            AxisBox.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)AxisBox).EndInit();
            AxisBox.ResumeLayout(false);
            AxisControlBox.ResumeLayout(false);
            AxisControlBox.PerformLayout();
            positionPanel.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
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
        private FlowLayoutPanel AxesNameBox;
        private GroupBox AxisControlBox;
        private Label positionValueLabel;
        private SplitContainer AxisBox;
        private Panel powerIndicator;
        private Label label1;
        private Label label2;
        private Panel busyIndicator;
        private Label label3;
        private Panel posLimit;
        private Label label8;
        private Label label7;
        private Label label6;
        private Label label5;
        private Panel negLimit;
        private Label label4;
        private Panel orign;
        private Button goAbsPositionButton;
        private Button homeButton;
        private Button resetButton;
        private Button stopButton;
        private Button jogNButton;
        private Button jogPButton;
        private Label label11;
        private Label label10;
        private Label label9;
        private Button goRelPositionButton;
        private Label label12;
        private Button goRemberPositionButton;
        private Button teachButton;
        private Label remberPosition;
        private Label label13;
        private TextBox relPosition;
        private Label label15;
        private Button enableButton;
        private Panel moveDone;
        private Label label16;
        private Label label17;
        private Panel errorIndicator;
        private FlowLayoutPanel positionPanel;
        private Panel panel1;
        private Label label19;
        private Label label18;
        private Label label14;
        private Button button1;
        private TextBox textBox2;
        private Label label20;
        private TextBox textBox1;
        private SplitContainer splitContainer1;
        private Panel ex_cy_card;
        private Label label21;
        private Panel ex_cy_homeSenser;
        private Panel ex_cy_workSenser;
        private Label ex_cy_err;
        private Label ex_cy_name;
        private Panel panel4;
        private Button ex_cy_homeButton;
        private Button ex_cy_workButton;
        private TextBox ex_cy_homeDoneTimeTextbox;
        private TextBox ex_cy_workDoneTimeTextbox;
        private GroupBox groupBox2;
        private TextBox textBox6;
        private TextBox textBox5;
        private Label label22;
        private Panel ex_cy_homeDone;
        private Panel ex_cy_workDone;
        private TableLayoutPanel ex_cy_controlPanel;
        private Label ex_cy_senserTimeLabel;
        private TableLayoutPanel ex_cy_senserTimePanel;
        private TableLayoutPanel ex_cy_alarmTimePanel;
        private TextBox ex_cy_homeAlarmTimeTextbox;
        private TextBox ex_cy_workAlarmTimeTextbox;
        private Label ex_cy_alarmTimeTxt;
        private Label label23;
    }
}