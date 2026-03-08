using KupaKuper_DeviceSever.Server;

using KupaKuper_HMI_Config.DeviceConfig.BaseType;

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
            int currentRow = 0;
            int currentCol = 0;

            foreach (var io in IOviewMode.IoModes.Skip((IOviewMode.CurrentPage - 1) * IOviewMode.PageIoCount).Take(IOviewMode.PageIoCount).ToList())
            {
                // 创建IO项容器
                Panel ioPanel = new Panel
                {
                    Size = new Size(itemWidth, itemHeight),
                    Location = new Point(padding + currentCol * (itemWidth + padding), padding + currentRow * (itemHeight + padding)),
                    BorderStyle = BorderStyle.FixedSingle,
                    BackColor = Color.White,
                    Margin = new Padding(5),
                    Tag = io,
                    Cursor = Cursors.Hand
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

                // 创建IO名称标签
                Label nameLabel = new Label
                {
                    Text = io.NameText,
                    Font = new Font("Segoe UI", 12, FontStyle.Regular),
                    AutoSize = false,
                    Size = new Size(itemWidth - 40, 25),
                    Location = new Point(10, 10),
                    TextAlign = ContentAlignment.MiddleLeft
                };

                // 创建IO状态指示器
                Panel statusIndicator = new Panel
                {
                    Size = new Size(20, 20),
                    Location = new Point(itemWidth - 30, 15),
                    BorderStyle = BorderStyle.FixedSingle,
                    BackColor = io.IoVar.Value ? Color.FromArgb(76, 175, 80) : Color.FromArgb(200, 200, 200),
                };

                // 添加属性更改事件
                io.AnyPropertyChanged += (sender, e) =>
                {
                    statusIndicator.Invoke((MethodInvoker)delegate
                    {
                        statusIndicator.BackColor = io.IoVar.Value ? Color.FromArgb(76, 175, 80) : Color.FromArgb(200, 200, 200);
                    });
                };

                // 添加控件到容器
                ioPanel.Controls.Add(nameLabel);
                ioPanel.Controls.Add(statusIndicator);
                IoBox.Controls.Add(ioPanel);

                // 更新位置
                currentCol++;
                if (currentCol >= itemsPerRow)
                {
                    currentCol = 0;
                    currentRow++;
                }
            }

            IoBox.ResumeLayout();
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
            ShowCylinderCard();
        }

        private int currentPositionPage = 1;
        private int positionsPerPage = 10;

        /// <summary>
        /// 显示气缸数据，并添加气缸控制按钮事件处理
        /// </summary>
        private void ShowCylinderCard()
        {
            CylinderBox.Controls.Clear();
            ShowCylinderChangeButton();

            int cardMargin = 2;

            foreach (var cylinder in CylinderViewMode?.CylinderModes!)
            {
                // 创建卡片容器
                Panel cardPanel = new Panel
                {
                    Size = new Size(324, 200),
                    BackColor = Color.White,
                    BorderStyle = BorderStyle.FixedSingle,
                    Margin = new Padding(cardMargin),
                    Tag = cylinder
                };

                // 添加阴影效果
                cardPanel.Paint += (sender, e) =>
                {
                    using (Pen pen = new Pen(Color.LightGray, 1))
                    {
                        e.Graphics.DrawRectangle(pen, 0, 0, cardPanel.Width - 1, cardPanel.Height - 1);
                    }
                };

                // 气缸名称标签
                Label nameLabel = new Label
                {
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                    Font = new Font("Microsoft YaHei UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 134),
                    Location = new Point(3, 0),
                    Size = new Size(316, 28),
                    TabIndex = 0,
                    Text = cylinder.NameText,
                    TextAlign = ContentAlignment.MiddleCenter
                };

                //气缸错误状态标签
                Label errLabel = new Label
                {
                    AutoSize = true,
                    Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 134),
                    ForeColor = Color.Red,
                    Location = new Point(3, 0),
                    Size = new Size(38, 17),
                    Text = "Error",
                };

                // 伸出按钮
                Button extendButton = new Button
                {
                    Anchor = AnchorStyles.Top | AnchorStyles.Right,
                    Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Bold, GraphicsUnit.Point, 134),
                    Location = new Point(183, 31),
                    Size = new Size(100, 35),
                    Text = cylinder.WorkText,
                    UseVisualStyleBackColor = true,
                };
                extendButton.FlatAppearance.BorderSize = 0;
                extendButton.Click += (sender, e) =>
                {
                    cylinder.Work.ISetValueCommand?.Execute(true);
                };

                // 缩回按钮
                Button retractButton = new Button
                {
                    Anchor = AnchorStyles.Top | AnchorStyles.Right,
                    Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Bold, GraphicsUnit.Point, 134),
                    Location = new Point(39, 31),
                    Size = new Size(100, 35),
                    Text = cylinder.HomeText,
                    UseVisualStyleBackColor = true,
                };
                retractButton.FlatAppearance.BorderSize = 0;
                retractButton.Click += (sender, e) =>
                {
                    cylinder.Home.ISetValueCommand?.Execute(true);
                };

                //伸出到位磁簧信号
                Panel workSensor = new Panel
                {
                    Size = new Size(20, 20),
                    Location = new Point(289, 38),
                    BorderStyle = BorderStyle.FixedSingle,
                    BackColor = cylinder.WorkInput.Value ? Color.Green : Color.Gray,
                };

                //缩回到位磁簧信号
                Panel homeSensor = new Panel
                {
                    Size = new Size(20, 20),
                    Location = new Point(10, 38),
                    BorderStyle = BorderStyle.FixedSingle,
                    BackColor = cylinder.HomeInput.Value ? Color.Green : Color.Gray,
                };

                //到位信号延时容器
                GroupBox sensorDoneTimeBox = new GroupBox
                {
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                    Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 134),
                    Location = new Point(3, 72),
                    Size = new Size(316, 57),
                    TabStop = false,
                    Text = "到位信号延时",
                };
                // 伸出到位延时输入框
                TextBox workDelayInput = new TextBox
                {
                    Font = new Font("Microsoft YaHei UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 134),
                    Location = new Point(180, 19),
                    Size = new Size(100, 32),
                };
                // 缩回到位延时输入框
                TextBox homeDelayInput = new TextBox
                {
                    Anchor = AnchorStyles.Top | AnchorStyles.Right,
                    Font = new Font("Microsoft YaHei UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 134),
                    Location = new Point(36, 19),
                    Size = new Size(100, 32),
                };
                // 伸出到位信号
                Panel workDone=new Panel
                {
                    Size = new Size(20, 20),
                    Location = new Point(286, 25),
                    BorderStyle = BorderStyle.FixedSingle,
                    BackColor = cylinder.WorkDone.Value ? Color.Green : Color.Gray,
                };
                // 缩回到位信号
                Panel homeDone = new Panel
                {
                    Anchor = AnchorStyles.Top | AnchorStyles.Right,
                    Size = new Size(20, 20),
                    Location = new Point(10, 25),
                    BorderStyle = BorderStyle.FixedSingle,
                    BackColor = cylinder.HomeDone.Value ? Color.Green : Color.Gray,
                };
                sensorDoneTimeBox.Controls.Add(workDelayInput);
                sensorDoneTimeBox.Controls.Add(homeDelayInput);
                sensorDoneTimeBox.Controls.Add(workDone);
                sensorDoneTimeBox.Controls.Add(homeDone);

                // 报警信号延时容器
                GroupBox sensorErrTimeBox = new GroupBox
                {
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                    Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 134),
                    Location = new Point(3, 140),
                    Size = new Size(316, 57),
                    TabStop = false,
                    Text = "报警触发延时",
                };
                // 伸出报警延时输入框
                TextBox workErrDelayInput = new TextBox
                {
                    Font = new Font("Microsoft YaHei UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 134),
                    Location = new Point(180, 19),
                    Size = new Size(120, 32),
                };
                // 缩回报警延时输入框
                TextBox homeErrDelayInput = new TextBox
                {
                    Anchor = AnchorStyles.Top | AnchorStyles.Right,
                    Font = new Font("Microsoft YaHei UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 134),
                    Location = new Point(16, 19),
                    Size = new Size(120, 32),
                };
                sensorErrTimeBox.Controls.Add(workErrDelayInput);
                sensorErrTimeBox.Controls.Add(homeErrDelayInput);

                // 将控件添加到卡片面板
                cardPanel.Controls.Add(errLabel);
                cardPanel.Controls.Add(nameLabel);
                cardPanel.Controls.Add(extendButton);
                cardPanel.Controls.Add(retractButton);
                cardPanel.Controls.Add(workSensor);
                cardPanel.Controls.Add(homeSensor);
                cardPanel.Controls.Add(sensorDoneTimeBox);
                cardPanel.Controls.Add(sensorErrTimeBox);

                // 添加属性更改事件
                cylinder.AnyPropertyChanged+=(sender, e) =>
                {
                    cardPanel.Invoke((MethodInvoker)delegate
                    {
                        workSensor.BackColor = cylinder.WorkInput.Value ? Color.Green : Color.Gray;
                        homeSensor.BackColor = cylinder.HomeInput.Value ? Color.Green : Color.Gray;
                        workDone.BackColor = cylinder.WorkDone.Value ? Color.Green : Color.Gray;
                        homeDone.BackColor = cylinder.HomeDone.Value ? Color.Green : Color.Gray;
                        errLabel.Visible = cylinder.Error.Value;
                    });
                };
                // 将卡片添加到容器
                CylinderBox.Controls.Add(cardPanel);
            }
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
