using KupaKuper_DeviceSever.Server;

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
                        statusIndicator.BackColor = io.IoVar.Value? Color.FromArgb(76, 175, 80) : Color.FromArgb(200, 200, 200);
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
            CylinderBox.Controls.Clear();
            CylinderNameBox.Controls.Clear();

            // 添加气缸分组选择按钮
            if (CylinderViewMode.CylinderGroups != null && CylinderViewMode.CylinderGroups.Count > 0)
            {
                foreach (var cylinderGroup in CylinderViewMode.CylinderGroups)
                {
                    Button groupButton = new Button
                    {
                        Text = server.GetLanguageValue(cylinderGroup),
                        Size = new Size(CylinderNameBox.Width - 20, 30),
                        Anchor = AnchorStyles.Left,
                        Margin = new Padding(5),
                        BackColor = server.CurrentDeviceRember.CylinderGroup == cylinderGroup ? Color.LightBlue : Color.White,
                        FlatStyle = FlatStyle.Flat
                    };
                    groupButton.FlatAppearance.BorderSize = 1;
                    groupButton.Click += (sender, e) => {
                        CylinderViewMode.SelectGroup(cylinderGroup);
                        showCylinder(); // 重新加载气缸显示
                    };
                    CylinderNameBox.Controls.Add(groupButton);
                }
            }

            // 设置容器布局
            CylinderBox.AutoScroll = true;
            CylinderBox.Padding = new Padding(10);

            int cardMargin = 15;
            int cardWidth = CylinderBox.Width / 2 - cardMargin*2;
            int cardHeight = 180;
            int columns = Math.Max(1, CylinderBox.Width / (cardWidth + cardMargin));
            int x = 0;
            int y = 0;

            foreach (var cylinder in CylinderViewMode.CylinderModes!)
            {
                // 创建卡片容器
                Panel cardPanel = new Panel
                {
                    Size = new Size(cardWidth, cardHeight),
                    Location = new Point(x, y),
                    BackColor = Color.White,
                    BorderStyle = BorderStyle.FixedSingle,
                    Margin = new Padding(5,0,5,10),
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
                    Text = cylinder.NameText,
                    Font = new Font("Segoe UI", 12, FontStyle.Bold),
                    AutoSize = false,
                    Size = new Size(cardWidth - 20, 25),
                    Location = new Point(10, 10),
                    TextAlign = ContentAlignment.MiddleLeft
                };

                // 伸出按钮
                Button extendButton = new Button
                {
                    Text = "伸出",
                    Size = new Size(60, 30),
                    Location = new Point(10, 45),
                    BackColor = Color.FromArgb(25, 118, 210),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat
                };
                extendButton.FlatAppearance.BorderSize = 0;
                extendButton.Click += (sender, e) => {
                    cylinder.Work.ISetValueCommand?.Execute(true);
                };

                // 缩回按钮
                Button retractButton = new Button
                {
                    Text = "缩回",
                    Size = new Size(60, 30),
                    Location = new Point(80, 45),
                    BackColor = Color.FromArgb(25, 118, 210),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat
                };
                retractButton.FlatAppearance.BorderSize = 0;
                retractButton.Click += (sender, e) => {
                    cylinder.Home.ISetValueCommand?.Execute(true);
                };

                // 伸出到位状态指示器
                Panel extendPositionIndicator = new Panel
                {
                    Size = new Size(15, 15),
                    Location = new Point(10, 90),
                    BackColor = cylinder.WorkInput.Value ? Color.Green : Color.Gray,
                    BorderStyle = BorderStyle.FixedSingle
                };

                Label extendPositionLabel = new Label
                {
                    Text = "伸出到位",
                    Font = new Font("Segoe UI", 9),
                    AutoSize = true,
                    Location = new Point(30, 90)
                };

                // 缩回到位状态指示器
                Panel retractPositionIndicator = new Panel
                {
                    Size = new Size(15, 15),
                    Location = new Point(100, 90),
                    BackColor = cylinder.HomeInput.Value ? Color.Green : Color.Gray,
                    BorderStyle = BorderStyle.FixedSingle
                };

                Label retractPositionLabel = new Label
                {
                    Text = "缩回到位",
                    Font = new Font("Segoe UI", 9),
                    AutoSize = true,
                    Location = new Point(120, 90)
                };

                // 报警状态指示器
                Panel alarmIndicator = new Panel
                {
                    Size = new Size(15, 15),
                    Location = new Point(10, 115),
                    BackColor = cylinder.Error.Value ? Color.Red : Color.Gray,
                    BorderStyle = BorderStyle.FixedSingle
                };

                Label alarmLabel = new Label
                {
                    Text = "报警状态",
                    Font = new Font("Segoe UI", 9),
                    AutoSize = true,
                    Location = new Point(30, 115)
                };

                // 伸出锁定状态指示器
                Panel extendLockIndicator = new Panel
                {
                    Size = new Size(15, 15),
                    Location = new Point(10, 140),
                    BackColor = cylinder.WorkLock.Value ? Color.Orange : Color.Gray,
                    BorderStyle = BorderStyle.FixedSingle
                };

                Label extendLockLabel = new Label
                {
                    Text = "伸出锁定",
                    Font = new Font("Segoe UI", 9),
                    AutoSize = true,
                    Location = new Point(30, 140)
                };

                // 缩回锁定状态指示器
                Panel retractLockIndicator = new Panel
                {
                    Size = new Size(15, 15),
                    Location = new Point(100, 140),
                    BackColor = cylinder.HomeLock.Value ? Color.Orange : Color.Gray,
                    BorderStyle = BorderStyle.FixedSingle
                };

                Label retractLockLabel = new Label
                {
                    Text = "缩回锁定",
                    Font = new Font("Segoe UI", 9),
                    AutoSize = true,
                    Location = new Point(120, 140)
                };

                // 添加属性更改事件处理
                cylinder.AnyPropertyChanged += (sender, e) =>
                {
                    if (cardPanel.InvokeRequired)
                    {
                        cardPanel.Invoke((MethodInvoker)delegate
                        {
                            extendPositionIndicator.BackColor = cylinder.WorkInput.Value ? Color.Green : Color.Gray;
                            retractPositionIndicator.BackColor = cylinder.HomeInput.Value ? Color.Green : Color.Gray;
                            alarmIndicator.BackColor = cylinder.Error.Value ? Color.Red : Color.Gray;
                            extendLockIndicator.BackColor = cylinder.WorkLock.Value ? Color.Orange : Color.Gray;
                            retractLockIndicator.BackColor = cylinder.HomeLock.Value ? Color.Orange : Color.Gray;
                        });
                    }
                    else
                    {
                        extendPositionIndicator.BackColor = cylinder.WorkInput.Value ? Color.Green : Color.Gray;
                        retractPositionIndicator.BackColor = cylinder.HomeInput.Value ? Color.Green : Color.Gray;
                        alarmIndicator.BackColor = cylinder.Error.Value ? Color.Red : Color.Gray;
                        extendLockIndicator.BackColor = cylinder.WorkLock.Value ? Color.Orange : Color.Gray;
                        retractLockIndicator.BackColor = cylinder.HomeLock.Value ? Color.Orange : Color.Gray;
                    }
                };

                // 将控件添加到卡片面板
                cardPanel.Controls.AddRange(new Control[] {
                    nameLabel,
                    extendButton,
                    retractButton,
                    extendPositionIndicator,
                    extendPositionLabel,
                    retractPositionIndicator,
                    retractPositionLabel,
                    alarmIndicator,
                    alarmLabel,
                    extendLockIndicator,
                    extendLockLabel,
                    retractLockIndicator,
                    retractLockLabel
                });

                // 将卡片添加到容器
                CylinderBox.Controls.Add(cardPanel);

                // 更新位置
                x += cardWidth + cardMargin;
                if (x + cardWidth > CylinderBox.Width)
                {
                    x = cardMargin;
                    y += cardHeight + cardMargin;
                }
            }
        }

        private void showAxis()
        {
            AxisViewMode.AxesModes.First().AnyPropertyChanged += PageControl_AnyPropertyChanged;
        }

        private void PageControl_AnyPropertyChanged(object arg1, System.ComponentModel.PropertyChangedEventArgs arg2)
        {
            //throw new NotImplementedException();
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
