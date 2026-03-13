using KupaKuper_DeviceSever.Server;

using KupaKuper_HMI_Config.DeviceConfig.BaseType;

using WinFromFrame_KupaKuper.Help;
using WinFromFrame_KupaKuper.ViewModes;
namespace WinFromFrame_KupaKuper
{
    public partial class PageControl : Form
    {
        private DeviceSystemServer server;
        private IoViewMode? IOviewMode;
        private CylinderViewMode? CylinderViewMode;
        private AxisViewMode? AxisViewMode;
        private OtherViewMode? OtherViewMode;
        public PageControl(DeviceSystemServer _server)
        {
            InitializeComponent();
            this.server = _server;
        }

        private void PageControl_Load(object sender, EventArgs e)
        {
            IOviewMode = new IoViewMode(server)
            {
                UpdataView = () =>
                {
                    // 初始化IO卡片池（如果尚未初始化）
                    InitializeIoCardPool();
                    ShowIo();
                    txt_PageNumber.Text = $"{IOviewMode?.CurrentPage}/{IOviewMode?.TotalPages}";
                }
            };
            CylinderViewMode = new CylinderViewMode(server)
            {
                UpdataView = () =>
                {
                    showCylinder();
                }
            };
            AxisViewMode = new AxisViewMode(server)
            {
                UpdataView = () =>
                {
                    showAxis();
                }
            };
            OtherViewMode = new OtherViewMode(server)
            {
                UpdataView = () =>
                {
                    showOther();
                }
            };
            IOviewMode.OnInitialized();
        }
        private void ShowIo()
        {
            if (IOviewMode == null)
            {
                MessageBox.Show("IO视图模式未初始化。");
                return;
            }
            if (IOviewMode.IoModes.Count == 0)
            {
                MessageBox.Show("没有可显示的IO数据。");
                return;
            }

            // 清除现有控件
            IoBox.Controls.Clear();

            // 设置容器布局为流式布局
            IoBox.SuspendLayout();
            IoBox.AutoScroll = true;

            int itemsPerRow = 4; // 每行显示的IO数量
            int padding = 10;    // 内边距
            int itemWidth = (IoBox.ClientSize.Width - 10 - padding * (itemsPerRow + 1)) / itemsPerRow; // 每个IO项的宽度
            int itemHeight = 50; // 每个IO项的高度

            // 获取当前页的IO数据
            var currentPageIos = IOviewMode.IoModes.Skip((IOviewMode.CurrentPage - 1) * IOviewMode.PageIoCount).Take(IOviewMode.PageIoCount).ToList();
            int iosToShow = Math.Min(currentPageIos.Count, MAX_IO_CARDS);

            // 隐藏所有IO卡片
            foreach (var ioCard in ioCardPool)
            {
                ioCard.Visible = false;
                ioCard.Tag = null; // 清除之前的绑定
            }

            // 显示需要的IO卡片并绑定数据
            for (int i = 0; i < iosToShow; i++)
            {
                var io = currentPageIos[i];
                var ioPanel = ioCardPool[i];
                
                // 绑定IO数据到卡片
                BindIoToCard(io, ioPanel);
                
                // 计算位置
                int currentRow = i / itemsPerRow;
                int currentCol = i % itemsPerRow;
                
                // 设置IO卡片位置和大小
                ioPanel.Size = new Size(itemWidth, itemHeight);
                ioPanel.Location = new Point(padding + currentCol * (itemWidth + padding), padding + currentRow * (itemHeight + padding));
                ioPanel.Visible = true;
                ioPanel.Tag = io;
                
                if (!IoBox.Controls.Contains(ioPanel))
                {
                    IoBox.Controls.Add(ioPanel);
                }
            }

            IoBox.ResumeLayout();
        }

        /// <summary>
        /// 将IO数据绑定到卡片
        /// </summary>
        private void BindIoToCard(Io io, Panel ioPanel)
        {
            // 更新卡片内容
            Label? nameLabel = ioPanel.Controls.Find("nameLabel", true).FirstOrDefault() as Label;
            if (nameLabel != null) nameLabel.Text = io.NameText;

            Panel? statusIndicator = ioPanel.Controls.Find("statusIndicator", true).FirstOrDefault() as Panel;
            if (statusIndicator != null)
            {
                statusIndicator.BackColor = io.IoVar.Value ? Color.FromArgb(76, 175, 80) : Color.FromArgb(200, 200, 200);
            }

            // 绑定属性更改事件
            io.AnyPropertyChanged += (sender, e) =>
            {
                ioPanel.Invoke((MethodInvoker)delegate
                {
                    UpdateIoCardVisualState(io, ioPanel);
                });
            };

            // 初始更新一次状态
            UpdateIoCardVisualState(io, ioPanel);
        }

        /// <summary>
        /// 更新IO卡片视觉状态
        /// </summary>
        private void UpdateIoCardVisualState(Io io, Panel ioPanel)
        {
            Panel? statusIndicator = ioPanel.Controls.Find("statusIndicator", true).FirstOrDefault() as Panel;
            if (statusIndicator != null)
            {
                statusIndicator.BackColor = io.IoVar.Value ? Color.FromArgb(76, 175, 80) : Color.FromArgb(200, 200, 200);
            }
        }

        private void showCylinder()
        {
            if (CylinderViewMode == null)
            {
                MessageBox.Show("气缸视图模式未初始化。");
                return;
            }
            if (CylinderViewMode.CylinderModes?.Count == 0)
            {
                MessageBox.Show("没有可显示的气缸数据。");
                return;
            }

            // 清除现有控件
            CylinderNameBox.Controls.Clear();

            // 添加气缸分组选择按钮
            if (CylinderViewMode.CylinderGroups != null && CylinderViewMode.CylinderGroups.Count > 0)
            {
                foreach (var cylinderGroup in CylinderViewMode.CylinderGroups)
                {
                    Button groupButton = new Button
                    {
                        Text = server.GetLanguageValue(cylinderGroup),
                        Tag = cylinderGroup,
                        Size = new Size(CylinderNameBox.Width - 20, 30),
                        Anchor = AnchorStyles.Left,
                        Margin = new Padding(5),
                        BackColor = server.CurrentDeviceRember.CylinderGroup == cylinderGroup ? MainForm.backcolor : Color.White,
                        FlatStyle = FlatStyle.Flat
                    };
                    groupButton.FlatAppearance.BorderSize = 1;
                    groupButton.Click += (sender, e) =>
                    {
                        CylinderViewMode.SelectGroup(cylinderGroup);
                        ShowCylinderCard(); // 重新加载气缸显示
                    };
                    CylinderNameBox.Controls.Add(groupButton);
                }
            }

            // 设置容器布局
            CylinderBox.AutoScroll = true;
            CylinderBox.Padding = new Padding(10);
            
            // 初始化卡片池（如果尚未初始化）
            InitializeCylinderCardPool();
            ShowCylinderCard();
        }

        private int currentPositionPage = 1;
        private int positionsPerPage = 10;
        
        // 气缸卡片池相关字段
        private List<Panel> cylinderCardPool = new List<Panel>();
        private bool isCylinderCardPoolInitialized = false;
        private const int MAX_CYLINDER_CARDS = 20; // 预实例化最大卡片数量，略大于单页最大值16
        
        // IO卡片池相关字段
        private List<Panel> ioCardPool = new List<Panel>();
        private bool isIoCardPoolInitialized = false;
        private const int MAX_IO_CARDS = 60; // 预实例化最大IO卡片数量，略大于单页最大值48

        /// <summary>
        /// 初始化气缸卡片池
        /// </summary>
        private void InitializeCylinderCardPool()
        {
            if (isCylinderCardPoolInitialized) return;
            
            for (int i = 0; i < MAX_CYLINDER_CARDS; i++)
            {
                Panel cardPanel = CreateCylinderCardTemplate();
                cylinderCardPool.Add(cardPanel);
            }
            
            isCylinderCardPoolInitialized = true;
        }
        
        private ControlHelp ControlHelp_cylinder= new ControlHelp();
        /// <summary>
        /// 创建气缸卡片模板（不绑定具体气缸数据）
        /// </summary>
        private Panel CreateCylinderCardTemplate()
        {
            

            Panel cardPanel = new Panel
            {
                Size = new Size(325, 230),
                BackColor = Color.FromArgb(240,240,240),
                BorderStyle = BorderStyle.None,
                Margin = new Padding(10),
                Visible = false, // 初始隐藏
            };
            ControlHelp_cylinder.SetControlRoundedRegion(cardPanel, 15);

            // 气缸名称标签
            Label nameLabel = new Label
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                Font = new Font("Microsoft YaHei UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 134),
                Location = new Point(2, 2),
                BorderStyle= BorderStyle.None,
                Size = new Size(321, 26),
                TabIndex = 0,
                Text = "气缸名称",
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.FromArgb(64, 158, 219),
                ForeColor = Color.White,
                Name = "nameLabel"
            };
            ControlHelp_cylinder.SetControlRoundedRegion(nameLabel, 15);

            // 气缸错误状态标签
            Label errLabel = new Label
            {
                AutoSize = true,
                Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 134),
                ForeColor = Color.White,
                Location = new Point(0, 0),
                Size = new Size(38, 17),
                Text = "Error",
                BackColor = Color.FromArgb(220, 53, 69),
                Visible = false,
                Name = "errLabel"
            };

            // 控制按钮面板
            TableLayoutPanel controlPanel = new TableLayoutPanel
            {
                ColumnCount = 4,
                ColumnStyles = {
                    new ColumnStyle(SizeType.Percent, 10F),
                    new ColumnStyle(SizeType.Percent, 40F),
                    new ColumnStyle(SizeType.Percent, 40F),
                    new ColumnStyle(SizeType.Percent, 10F)
                },
                Location = new Point(3, 31),
                Name = "controlPanel",
                RowCount = 1,
                RowStyles = { new RowStyle(SizeType.Percent, 100F) },
                Size = new Size(316, 60),
                TabIndex = 1
            };

            // 伸出到位磁簧信号
            Panel workSensor = new Panel
            {
                BorderStyle = BorderStyle.FixedSingle,
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(206, 212, 218),
                Name = "workSensor"
            };

            // 伸出按钮
            Button extendButton = new Button
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Bold, GraphicsUnit.Point, 134),
                Location = new Point(34, 3),
                Size = new Size(120, 54),
                Text = "伸出",
                FlatStyle = FlatStyle.Standard,
                FlatAppearance = { BorderSize = 0 },
                Name = "extendButton"
            };

            // 缩回按钮
            Button retractButton = new Button
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Bold, GraphicsUnit.Point, 134),
                Location = new Point(160, 3),
                Size = new Size(120, 54),
                Text = "缩回",
                FlatStyle = FlatStyle.Standard,
                FlatAppearance = { BorderSize = 0 },
                Name = "retractButton"
            };

            // 缩回到位磁簧信号
            Panel homeSensor = new Panel
            {
                BorderStyle = BorderStyle.FixedSingle,
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(206, 212, 218),
                Name = "homeSensor"
            };

            controlPanel.Controls.Add(workSensor, 0, 0);
            controlPanel.Controls.Add(extendButton, 1, 0);
            controlPanel.Controls.Add(retractButton, 2, 0);
            controlPanel.Controls.Add(homeSensor, 3, 0);

            // 到位信号延时标签
            Label sensorDoneTimeLabel = new Label
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 134),
                Location = new Point(3, 94),
                Size = new Size(317, 23),
                Text = "到位信号延时",
                TextAlign = ContentAlignment.MiddleLeft,
                Name = "sensorDoneTimeLabel"
            };

            // 到位信号延时面板
            TableLayoutPanel sensorDoneTimePanel = new TableLayoutPanel
            {
                ColumnCount = 4,
                ColumnStyles = {
                    new ColumnStyle(SizeType.Percent, 10F),
                    new ColumnStyle(SizeType.Percent, 40F),
                    new ColumnStyle(SizeType.Percent, 40F),
                    new ColumnStyle(SizeType.Percent, 10F)
                },
                Location = new Point(3, 120),
                Name = "sensorDoneTimePanel",
                RowCount = 1,
                RowStyles = { new RowStyle(SizeType.Percent, 100F) },
                Size = new Size(316, 36),
                TabIndex = 2
            };

            // 伸出到位信号
            Panel workDone = new Panel
            {
                BorderStyle = BorderStyle.FixedSingle,
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(206, 212, 218),
                Name = "workDone"
            };

            // 伸出到位延时输入框
            TextBox workDelayInput = new TextBox
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                Font = new Font("Microsoft YaHei UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 134),
                Location = new Point(34, 3),
                Size = new Size(120, 32),
                TextAlign = HorizontalAlignment.Center,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Name = "workDelayInput"
            };

            // 缩回到位延时输入框
            TextBox homeDelayInput = new TextBox
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                Font = new Font("Microsoft YaHei UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 134),
                Location = new Point(160, 3),
                Size = new Size(120, 32),
                TextAlign = HorizontalAlignment.Center,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Name = "homeDelayInput"
            };

            // 缩回到位信号
            Panel homeDone = new Panel
            {
                BorderStyle = BorderStyle.FixedSingle,
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(206, 212, 218),
                Name = "homeDone"
            };

            sensorDoneTimePanel.Controls.Add(workDone, 0, 0);
            sensorDoneTimePanel.Controls.Add(workDelayInput, 1, 0);
            sensorDoneTimePanel.Controls.Add(homeDelayInput, 2, 0);
            sensorDoneTimePanel.Controls.Add(homeDone, 3, 0);

            // 报警触发延时标签
            Label sensorErrTimeLabel = new Label
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 134),
                Location = new Point(3, 159),
                Size = new Size(317, 23),
                Text = "报警触发延时",
                TextAlign = ContentAlignment.MiddleLeft,
                Name = "sensorErrTimeLabel"
            };

            // 报警触发延时面板
            TableLayoutPanel sensorErrTimePanel = new TableLayoutPanel
            {
                ColumnCount = 2,
                ColumnStyles = {
                    new ColumnStyle(SizeType.Percent, 50F),
                    new ColumnStyle(SizeType.Percent, 50F)
                },
                Location = new Point(3, 185),
                Name = "sensorErrTimePanel",
                RowCount = 1,
                RowStyles = { new RowStyle(SizeType.Percent, 100F) },
                Size = new Size(317, 36),
                TabIndex = 3
            };

            // 伸出报警延时输入框
            TextBox workErrDelayInput = new TextBox
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                Font = new Font("Microsoft YaHei UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 134),
                Location = new Point(3, 3),
                Size = new Size(152, 32),
                TextAlign = HorizontalAlignment.Center,
                Name = "workErrDelayInput"
            };

            // 缩回报警延时输入框
            TextBox homeErrDelayInput = new TextBox
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                Font = new Font("Microsoft YaHei UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 134),
                Location = new Point(161, 3),
                Size = new Size(153, 32),
                TextAlign = HorizontalAlignment.Center,
                Name = "homeErrDelayInput"
            };

            sensorErrTimePanel.Controls.Add(workErrDelayInput, 0, 0);
            sensorErrTimePanel.Controls.Add(homeErrDelayInput, 1, 0);

            // 将控件添加到卡片面板
            cardPanel.Controls.Add(errLabel);
            cardPanel.Controls.Add(nameLabel);
            cardPanel.Controls.Add(controlPanel);
            cardPanel.Controls.Add(sensorDoneTimeLabel);
            cardPanel.Controls.Add(sensorDoneTimePanel);
            cardPanel.Controls.Add(sensorErrTimeLabel);
            cardPanel.Controls.Add(sensorErrTimePanel);

            return cardPanel;
        }

        /// <summary>
        /// 初始化IO卡片池
        /// </summary>
        private void InitializeIoCardPool()
        {
            if (isIoCardPoolInitialized) return;
            
            for (int i = 0; i < MAX_IO_CARDS; i++)
            {
                Panel ioPanel = CreateIoCardTemplate();
                ioCardPool.Add(ioPanel);
            }
            
            isIoCardPoolInitialized = true;
        }

        /// <summary>
        /// 创建IO卡片模板（不绑定具体IO数据）
        /// </summary>
        private Panel CreateIoCardTemplate()
        {
            Panel ioPanel = new Panel
            {
                Size = new Size(200, 50),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(5),
                Cursor = Cursors.Hand,
                Visible = false // 初始隐藏
            };

            // 添加悬停效果
            ioPanel.MouseEnter += (sender, e) =>
            {
                ((Panel)sender!).BackColor = Color.FromArgb(245, 245, 245);
            };

            ioPanel.MouseLeave += (sender, e) =>
            {
                ((Panel)sender!).BackColor = Color.White;
            };

            // IO名称标签
            Label nameLabel = new Label
            {
                Text = "IO名称",
                Font = new Font("Segoe UI", 12, FontStyle.Regular),
                AutoSize = false,
                Size = new Size(160, 25),
                Location = new Point(10, 10),
                TextAlign = ContentAlignment.MiddleLeft,
                Name = "nameLabel"
            };

            // IO状态指示器
            Panel statusIndicator = new Panel
            {
                Size = new Size(20, 20),
                Location = new Point(ioPanel.Width-30, 15),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.FromArgb(200, 200, 200),
                Name = "statusIndicator"
            };

            // 将控件添加到IO面板
            ioPanel.Controls.Add(nameLabel);
            ioPanel.Controls.Add(statusIndicator);

            return ioPanel;
        }

        /// <summary>
        /// 显示气缸数据，使用预实例化的卡片池
        /// </summary>
        private void ShowCylinderCard()
        {
            CylinderBox.Controls.Clear();
            ShowCylinderChangeButton();

            if (CylinderViewMode?.CylinderModes == null) return;

            int cylinderCount = CylinderViewMode.CylinderModes.Count;
            int cardsToShow = Math.Min(cylinderCount, MAX_CYLINDER_CARDS);

            // 隐藏所有卡片
            foreach (var card in cylinderCardPool)
            {
                card.Visible = false;
                card.Tag = null; // 清除之前的绑定
            }

            // 显示需要的卡片并绑定数据
            for (int i = 0; i < cardsToShow; i++)
            {
                var cylinder = CylinderViewMode.CylinderModes[i];
                var cardPanel = cylinderCardPool[i];
                
                // 绑定气缸数据到卡片
                BindCylinderToCard(cylinder, cardPanel);
                
                // 设置卡片位置
                int row = i / 2; // 每行显示2个卡片
                int col = i % 2;
                int margin = 10;
                int cardWidth = 325;
                int cardHeight = 230;
                
                cardPanel.Location = new Point(margin + col * (cardWidth + margin), margin + row * (cardHeight + margin));
                cardPanel.Visible = true;
                cardPanel.Tag = cylinder;
                
                if (!CylinderBox.Controls.Contains(cardPanel))
                {
                    CylinderBox.Controls.Add(cardPanel);
                }
            }
        }

        /// <summary>
        /// 将气缸数据绑定到卡片
        /// </summary>
        private void BindCylinderToCard(Cylinder cylinder, Panel cardPanel)
        {
            // 清除之前的事件绑定
            ClearCardEventBindings(cardPanel);

            // 更新卡片内容
            Label? nameLabel = cardPanel.Controls.Find("nameLabel", true).FirstOrDefault() as Label;
            if (nameLabel != null) nameLabel.Text = cylinder.NameText;

            Label? errLabel = cardPanel.Controls.Find("errLabel", true).FirstOrDefault() as Label;
            if (errLabel != null) errLabel.Visible = cylinder.Error.Value;

            // 更新按钮文本
            Button? extendButton = cardPanel.Controls.Find("extendButton", true).FirstOrDefault() as Button;
            if (extendButton != null)
            {
                extendButton.Text = cylinder.WorkText;
                extendButton.Click += (sender, e) =>
                {
                    cylinder.Work.ISetValueCommand?.Execute(true);
                };
            }

            Button? retractButton = cardPanel.Controls.Find("retractButton", true).FirstOrDefault() as Button;
            if (retractButton != null)
            {
                retractButton.Text = cylinder.HomeText;
                retractButton.Click += (sender, e) =>
                {
                    cylinder.Home.ISetValueCommand?.Execute(true);
                };
            }

            // 绑定属性更改事件
            cylinder.AnyPropertyChanged += (sender, e) =>
            {
                cardPanel.Invoke((MethodInvoker)delegate
                {
                    UpdateCardVisualState(cylinder, cardPanel);
                });
            };

            // 初始更新一次状态
            UpdateCardVisualState(cylinder, cardPanel);
        }

        /// <summary>
        /// 更新卡片视觉状态
        /// </summary>
        private void UpdateCardVisualState(Cylinder cylinder, Panel cardPanel)
        {
            Panel? workSensor = cardPanel.Controls.Find("workSensor", true).FirstOrDefault() as Panel;
            if (workSensor != null) workSensor.BackColor = cylinder.WorkInput.Value ? Color.FromArgb(40, 167, 69) : Color.FromArgb(206, 212, 218);

            Panel? homeSensor = cardPanel.Controls.Find("homeSensor", true).FirstOrDefault() as Panel;
            if (homeSensor != null) homeSensor.BackColor = cylinder.HomeInput.Value ? Color.FromArgb(40, 167, 69) : Color.FromArgb(206, 212, 218);

            Panel? workDone = cardPanel.Controls.Find("workDone", true).FirstOrDefault() as Panel;
            if (workDone != null) workDone.BackColor = cylinder.WorkDone.Value ? Color.FromArgb(40, 167, 69) : Color.FromArgb(206, 212, 218);

            Panel? homeDone = cardPanel.Controls.Find("homeDone", true).FirstOrDefault() as Panel;
            if (homeDone != null) homeDone.BackColor = cylinder.HomeDone.Value ? Color.FromArgb(40, 167, 69) : Color.FromArgb(206, 212, 218);

            Label? errLabel = cardPanel.Controls.Find("errLabel", true).FirstOrDefault() as Label;
            if (errLabel != null) errLabel.Visible = cylinder.Error.Value;
        }

        /// <summary>
        /// 清除卡片的事件绑定
        /// </summary>
        private void ClearCardEventBindings(Panel cardPanel)
        {
            // 由于按钮事件是每次绑定的，不需要特殊清除
            // 新的绑定会覆盖旧的绑定
        }

        /// <summary>
        /// 显示气缸选择按钮高亮
        /// </summary>
        private void ShowCylinderChangeButton()
        {
            foreach (var item in CylinderNameBox.Controls)
            {
                if (item is Button button)
                {
                    button.BackColor = server.CurrentDeviceRember.CylinderGroup == button.Tag?.ToString() ? MainForm.backcolor : Color.White;
                }
            }
        }
        /// <summary>
        /// 显示轴数据，并添加轴控制按钮事件处理
        /// </summary>
        private void showAxis()
        {
            if (AxisViewMode == null)
            {
                MessageBox.Show("轴视图模式未初始化。");
                return;
            }
            if (AxisViewMode.AxesModes?.Count == 0)
            {
                MessageBox.Show("没有可显示的轴数据。");
                return;
            }

            // 清除现有控件
            AxesNameBox.Controls.Clear();

            // 添加轴分组选择按钮
            if (AxisViewMode.AxesGroups != null && AxisViewMode.AxesGroups.Count > 0)
            {
                foreach (var axisGroup in AxisViewMode.AxesGroups)
                {
                    Button groupButton = new Button
                    {
                        Text = server.GetLanguageValue(axisGroup),
                        Tag = axisGroup,
                        Size = new Size(AxesNameBox.Width - 30, 30),
                        Anchor = AnchorStyles.Left,
                        Margin = new Padding(5),
                        BackColor = server.CurrentDeviceRember.AxisGroup == axisGroup ? MainForm.backcolor : Color.White,
                        FlatStyle = FlatStyle.Flat
                    };
                    groupButton.FlatAppearance.BorderSize = 1;
                    groupButton.Click += (sender, e) =>
                    {
                        AxisViewMode.SelectGroup(axisGroup);
                        currentPositionPage = 1; // 重置页码
                        var axis = AxisViewMode.AxesModes?.First();
                        showAxisDetail(axis); // 重新加载轴显示
                    };
                    AxesNameBox.Controls.Add(groupButton);
                }
                showAxisDetail(AxisViewMode.AxesModes?.First()); // 重新加载轴显示
            }
        }
        public void showAxisDetail(Axis? axis)
        {
            if (axis == null)
            {
                MessageBox.Show("无法获取轴数据。");
                return;
            }
            AxisControlBox.Text = axis.NameText;
            // Jog正向按钮（复归型）
            jogPButton.MouseDown += (sender, e) =>
            {
                axis.AxisControl.JogP.ISetValueCommand?.Execute(true);
            };
            jogPButton.MouseUp += (sender, e) =>
            {
                axis.AxisControl.JogP.ISetValueCommand?.Execute(false);
            };

            // Jog反向按钮（复归型）
            jogNButton.MouseDown += (sender, e) =>
            {
                axis.AxisControl.JogN.ISetValueCommand?.Execute(true);
            };
            jogNButton.MouseUp += (sender, e) =>
            {
                axis.AxisControl.JogN.ISetValueCommand?.Execute(false);
            };

            // 停止按钮（按下即触发）
            stopButton.Click += (sender, e) =>
            {
                axis.AxisControl.Stop.ISetValueCommand?.Execute(true);
                // 短暂延迟后复位
                Task.Delay(100).ContinueWith(t =>
                {
                    axis.AxisControl.Stop.ISetValueCommand?.Execute(false);
                });
            };

            // 回原点按钮
            homeButton.Click += (sender, e) =>
            {
                axis.AxisControl.GoHome.ISetValueCommand?.Execute(true);
                // 短暂延迟后复位
                Task.Delay(100).ContinueWith(t =>
                {
                    axis.AxisControl.GoHome.ISetValueCommand?.Execute(false);
                });
            };

            // 使能按钮
            enableButton.Click += (sender, e) =>
            {
                axis.AxisControl.OpenPower.ISetValueCommand?.Execute(true);
                // 短暂延迟后复位
                Task.Delay(100).ContinueWith(t =>
                {
                    axis.AxisControl.OpenPower.ISetValueCommand?.Execute(false);
                });
            };

            // 复位按钮
            resetButton.Click += (sender, e) =>
            {
                axis.AxisControl.Reset.ISetValueCommand?.Execute(true);
                // 短暂延迟后复位
                Task.Delay(100).ContinueWith(t =>
                {
                    axis.AxisControl.Reset.ISetValueCommand?.Execute(false);
                });
            };
            // 绝对定位
            goAbsPositionButton.Click += (sender, e) => 
            {
                axis.AxisControl.MoveAbs.ISetValueCommand?.Execute(true);
                // 短暂延迟后复位
                Task.Delay(100).ContinueWith(t =>
                {
                    axis.AxisControl.MoveAbs.ISetValueCommand?.Execute(false);
                });
            };
            // 相对定位
            goRelPositionButton.Click += (sender, e) =>
            {
                axis.AxisControl.MoveRel.ISetValueCommand?.Execute(true);
                // 短暂延迟后复位
                Task.Delay(100).ContinueWith(t =>
                {
                    axis.AxisControl.MoveRel.ISetValueCommand?.Execute(false);
                });
            };
            // 示教按钮
            teachButton.Click += (sender, e) =>
            {
                axis.AxisControl.Teach.ISetValueCommand?.Execute(true);
                // 短暂延迟后复位
                Task.Delay(100).ContinueWith(t =>
                {
                    axis.AxisControl.Teach.ISetValueCommand?.Execute(false);
                });
            };
            // 相对位置输入框
            relPosition.TextChanged += (sender, e) =>
            {
                if (double.TryParse(relPosition.Text, out double newValue))
                {
                    axis.AxisControl.RelativePosition.SetCommand.Execute(newValue);
                }
            };
            ShowAxisPosition(axis);
            // 添加属性更改事件处理
            axis.AnyPropertyChanged += PageControl_AnyPropertyChanged;
        }

        public void ShowAxisPosition(Axis? axis)
        {
            if (axis == null)
            {
                MessageBox.Show("无法获取轴数据。");
                return;
            }
            positionPanel.Controls.Clear();
            ShowAxisChangeButton();

            // 只设置Dock属性，移除Anchor属性避免冲突
            positionPanel.Dock = DockStyle.Fill;

            foreach (var pos in axis.ListPosition.Skip((currentPositionPage - 1) * positionsPerPage).Take(positionsPerPage))
            {
                Panel posItemPanel = new Panel
                {
                    BorderStyle = BorderStyle.None,
                    Dock = DockStyle.Top,
                    Width = 930, // 考虑滚动条宽度
                    Height = 35,
                    TabIndex = 0,
                };

                Label posNoLabel = new Label
                {
                    Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left,
                    Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 134),
                    Location = new Point(3, 0),
                    Size = new Size(49, 33),
                    TabIndex = 0,
                    Text = pos.PositionNo.ToString(),
                    TextAlign = ContentAlignment.MiddleCenter,
                };
                posItemPanel.Controls.Add(posNoLabel);

                Label posNameLabel = new Label
                {
                    Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                    Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 134),
                    Location = new Point(58, 0),
                    Size = new Size(440, 33),
                    TabIndex = 1,
                    Text = pos.NameText,
                    TextAlign = ContentAlignment.MiddleCenter,
                    BorderStyle = BorderStyle.FixedSingle,
                };
                posNameLabel.Click += (sender, e) =>
                {
                    axis.AxisControl.AbsNumber.Value = (short)pos.PositionNo;
                    ShowAxisPositionNo(axis);
                };
                posItemPanel.Controls.Add(posNameLabel);

                Label label_postxt = new Label
                {
                    Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right,
                    Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 134),
                    Location = new Point(512, 1),
                    Size = new Size(59, 33),
                    TabIndex = 2,
                    Text = "位置:",
                    TextAlign = ContentAlignment.MiddleRight,
                };
                posItemPanel.Controls.Add(label_postxt);

                TextBox posValueLabel = new TextBox
                {
                    Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right,
                    Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Bold, GraphicsUnit.Point, 134),
                    Location = new Point(577, 4),
                    Size = new Size(100, 25),
                    TabIndex = 3,
                    Text = pos.PositionVar.Value.ToString("F2"),
                    TextAlign = HorizontalAlignment.Center,
                };
                posValueLabel.TextChanged+=(sender, e) =>
                {
                    if (double.TryParse(posValueLabel.Text, out double newValue))
                    {
                        pos.PositionVar.SetCommand.Execute(newValue);
                    }
                };
                posItemPanel.Controls.Add(posValueLabel);

                Label label_velocitytxt = new Label
                {
                    Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right,
                    Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 134),
                    Location = new Point(683, 0),
                    Size = new Size(59, 33),
                    TabIndex = 4,
                    Text = "速度:",
                    TextAlign = ContentAlignment.MiddleRight,
                };
                posItemPanel.Controls.Add(label_velocitytxt);

                TextBox posVelocityLabel = new TextBox
                {
                    Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right,
                    Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Bold, GraphicsUnit.Point, 134),
                    Location = new Point(748, 4),
                    Size = new Size(100, 25),
                    Text = pos.VelocityVar.Value.ToString("F0"),
                    TabIndex = 5,
                    TextAlign = HorizontalAlignment.Center,
                };
                posVelocityLabel.TextChanged+=(sender, e) =>
                {
                    if (double.TryParse(posVelocityLabel.Text, out double newValue))
                    {
                        pos.VelocityVar.SetCommand.Execute(newValue);
                    }
                };
                posItemPanel.Controls.Add(posVelocityLabel);

                Button goToButton = new Button
                {
                    Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right,
                    Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 134),
                    Location = new Point(854, 2),
                    Size = new Size(70, 30),
                    TabIndex = 6,
                    Text = "定位",
                    UseVisualStyleBackColor = true,
                };
                goToButton.FlatAppearance.BorderSize = 0;
                goToButton.MouseDown += (sender, e) =>
                {
                    axis.AxisControl.AbsNumber.Value = (short)pos.PositionNo;
                    axis.AxisControl.MoveAbs.ISetValueCommand?.Execute(true);
                };
                goToButton.MouseUp += (sender, e) =>
                {
                    axis.AxisControl.AbsNumber.Value = (short)pos.PositionNo;
                    axis.AxisControl.MoveAbs.ISetValueCommand?.Execute(false);
                };
                posItemPanel.Controls.Add(goToButton);

                positionPanel.Controls.Add(posItemPanel);
            }
            ShowAxisPositionNo(axis);
        }
        /// <summary>
        /// 显示轴选择按钮高亮
        /// </summary>
        /// <param name="axis"></param>
        public void ShowAxisChangeButton()
        {
            foreach (var item in AxesNameBox.Controls)
            {
                if(item is Button button)
                {
                    button.BackColor = server.CurrentDeviceRember.AxisGroup == button.Tag?.ToString() ? MainForm.backcolor : Color.White;
                }
            }
        }
        /// <summary>
        /// 显示轴位置编号，并高亮当前选中位置
        /// </summary>
        /// <param name="axis"></param>
        public void ShowAxisPositionNo(Axis axis)
        {
            foreach (var item in positionPanel.Controls)
            {
                if (item is Panel panel)
                {
                    foreach (var item1 in panel.Controls)
                    {
                        if (item1 is Label label)
                        {
                            panel.BackColor = label.Text == axis.AxisControl.AbsNumber.Value.ToString() ? MainForm.backcolor : Color.Transparent;
                            break;
                        }
                    }
                }
            }
        }

        private void PageControl_AnyPropertyChanged(object arg1, System.ComponentModel.PropertyChangedEventArgs arg2)
        {
            // 轴属性更改事件处理
            Axis? axis = AxisViewMode?.AxesModes?.First();
            if (axis != null && AxisBox.InvokeRequired)
            {
                AxisBox.Invoke((MethodInvoker)delegate
                {
                    try
                    {
                        // 更新轴状态
                        positionValueLabel.Text = axis.AxisControl.CurrentPosition.Value.ToString("F2");
                        powerIndicator.BackColor = axis.AxisControl.Power.Value ? Color.Green : Color.Gray;
                        errorIndicator.BackColor = axis.AxisControl.Error.Value ? Color.Red : Color.Gray;
                        busyIndicator.BackColor = axis.AxisControl.Busy.Value ? Color.Green : Color.Gray;
                        posLimit.BackColor = axis.AxisControl.PosLimit.Value ? Color.Green : Color.Gray;
                        negLimit.BackColor = axis.AxisControl.NegLimit.Value ? Color.Green : Color.Gray;
                        orign.BackColor = axis.AxisControl.Origin.Value ? Color.Green : Color.Gray;
                        moveDone.BackColor = axis.AxisControl.MovAbsDone.Value ? Color.Green : Color.Gray;
                        remberPosition.Text = axis.AxisControl.MemoryPosition.Value.ToString();
                        ShowAxisPositionNo(axis);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("更新轴状态时出错: " + ex.Message);
                    }
                });
            }
        }

        private void showOther()
        {

        }

        private void but_Input_Click(object sender, EventArgs e)
        {
            if (server.CurrentDeviceRember.IoType != DeviceRember.IoTypeName.input)
                IOviewMode?.ToggleIoType(DeviceRember.IoTypeName.input);
        }

        private void but_Output_Click(object sender, EventArgs e)
        {
            if (server.CurrentDeviceRember.IoType != DeviceRember.IoTypeName.output)
                IOviewMode?.ToggleIoType(DeviceRember.IoTypeName.output);
        }

        private void but_Next_Click(object sender, EventArgs e)
        {
            IOviewMode?.NextPage();
        }

        private void but_Previous_Click(object sender, EventArgs e)
        {
            IOviewMode?.PreviousPage();
        }

        private void PageTable_Deselecting(object sender, TabControlCancelEventArgs e)
        {
            switch (e.TabPageIndex)
            {
                case 0:
                    IOviewMode?.Dispose();
                    break;
                case 1:
                    CylinderViewMode?.Dispose();
                    break;
                case 2:
                    AxisViewMode?.Dispose();
                    break;
                case 3:
                    OtherViewMode?.Dispose();
                    break;
            }
        }

        private void PageTable_Selected(object sender, TabControlEventArgs e)
        {
            switch (e.TabPageIndex)
            {
                case 0:
                    IOviewMode?.OnInitialized();
                    break;
                case 1:
                    CylinderViewMode?.OnInitialized();
                    break;
                case 2:
                    AxisViewMode?.OnInitialized();
                    break;
                case 3:
                    OtherViewMode?.OnInitialized();
                    break;
            }
        }
    }
}
