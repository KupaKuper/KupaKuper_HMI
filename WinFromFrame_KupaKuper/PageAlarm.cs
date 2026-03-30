using KupaKuper_HMI_DeviceSever.Server;

using KupaKuper_HMI_Config.DeviceConfig.BaseType;

using System.Windows.Forms.DataVisualization.Charting;

using WinFromFrame_KupaKuper.ViewModels;

namespace WinFromFrame_KupaKuper
{
    public partial class PageAlarm : Form
    {
        private DeviceSystemServer server;
        private AlarmDataViewModel? alarmDataViewModel;
        private AlarmViewModel? alarmViewModel;
        
        // 报警卡片池 - 预实例化20个卡片
        private List<Panel> alarmCardPool = new List<Panel>();
        private const int ALARM_CARD_POOL_SIZE = 20;
        
        public PageAlarm(DeviceSystemServer _server)
        {
            InitializeComponent();
            this.server = _server;
            InitializeAlarmCardPool();
        }
        
        private void InitializeAlarmCardPool()
        {
            for (int i = 0; i < ALARM_CARD_POOL_SIZE; i++)
            {
                Panel alarmCard = CreateAlarmCardTemplate();
                alarmCard.Visible = false; // 初始隐藏
                alarmCardPool.Add(alarmCard);
            }
        }
        
        private Panel CreateAlarmCardTemplate()
        {
            Panel alarmCard = new Panel
            {
                Size = new Size(300, 80),
                BackColor = Color.FromArgb(255, 200, 200),
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(5)
            };

            Label alarmTypeLabel = new Label
            {
                Font = new Font("Microsoft YaHei UI", 10, FontStyle.Bold),
                Location = new Point(5, 5),
                Size = new Size(290, 20),
                ForeColor = Color.Red
            };

            Label alarmTextLabel = new Label
            {
                Font = new Font("Microsoft YaHei UI", 9),
                Location = new Point(5, 30),
                Size = new Size(290, 20)
            };

            Label timeLabel = new Label
            {
                Font = new Font("Microsoft YaHei UI", 8),
                Location = new Point(5, 55),
                Size = new Size(290, 20),
                ForeColor = Color.Gray
            };

            alarmCard.Controls.Add(alarmTypeLabel);
            alarmCard.Controls.Add(alarmTextLabel);
            alarmCard.Controls.Add(timeLabel);

            return alarmCard;
        }
        private void PageAlarm_Load(object sender, EventArgs e)
        {
            alarmDataViewModel = new AlarmDataViewModel(server)
            {
                UpdataView = () =>
                {
                     ShowAlarmData();
                }
            };
            alarmViewModel = new AlarmViewModel(server) 
            {
                UpdataView = () =>
                {
                    ShowAlarmView();
                }
            };
            alarmDataViewModel.SelectedDate = DateTime.Now;
            alarmViewModel?.OnInitialized();
            
            // 页面加载时更新一次图表
            Panel chartStatsPanel = GetOrCreateChartStatsPanel();
            UpdateChartStatsPanel(chartStatsPanel);
        }

        private void ShowAlarmData()
        {
            if (alarmDataViewModel?.logEntries == null) return;
            
            // 清除历史报警页面现有控件
            tabPage_HistoryAlarm.Controls.Clear();
            
            // 创建历史报警表格
            DataGridView historyAlarmGrid = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                RowHeadersVisible = false,
                BackgroundColor = Color.White
            };

            // 添加列
            historyAlarmGrid.Columns.Add("Number", "报警序号");
            historyAlarmGrid.Columns.Add("AlarmId", "报警ID");
            historyAlarmGrid.Columns.Add("AlarmType", "报警类型");
            historyAlarmGrid.Columns.Add("AlarmText", "报警内容");
            historyAlarmGrid.Columns.Add("StationText", "报警工站");
            historyAlarmGrid.Columns.Add("TriggerTime", "触发时间");
            historyAlarmGrid.Columns.Add("AlarmValue", "报警值");
            historyAlarmGrid.Columns.Add("AlarmTime", "报警时间");

            // 设置列样式
            foreach (DataGridViewColumn column in historyAlarmGrid.Columns)
            {
                column.DefaultCellStyle.Font = new Font("Microsoft YaHei UI", 9F);
                column.HeaderCell.Style.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
            }

            // 添加数据
            foreach (var alarm in alarmDataViewModel.logEntries)
            {
                historyAlarmGrid.Rows.Add(
                    alarm.Number,
                    alarm.AlarmId,
                    alarm.AlarmType,
                    alarm.AlarmText,
                    alarm.StationText,
                    alarm.TriggerTime,
                    alarm.AlarmValue,
                    alarm.AlarmTime
                );
            }

            // 添加搜索框和日期选择器
            Panel searchPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 50,
                BackColor = Color.FromArgb(240, 240, 240)
            };

            TextBox searchBox = new TextBox
            {
                Location = new Point(10, 10),
                Size = new Size(200, 25),
                PlaceholderText = "搜索报警内容..."
            };

            DateTimePicker datePicker = new DateTimePicker
            {
                Location = new Point(220, 10),
                Size = new Size(150, 25),
                Value = alarmDataViewModel.SelectedDate
            };

            Button searchButton = new Button
            {
                Location = new Point(380, 10),
                Size = new Size(80, 25),
                Text = "搜索"
            };

            //searchBox.TextChanged += (sender, e) =>
            //{
            //    alarmDataViewModel.SearchCurrentData(searchBox.Text);
            //    ShowAlarmData();
            //};

            datePicker.ValueChanged += (sender, e) =>
            {
                alarmDataViewModel.SelectedDate = datePicker.Value;
            };

            searchButton.Click += (sender, e) =>
            {
                alarmDataViewModel.SearchCurrentData(searchBox.Text);
                ShowAlarmData();
            };

            searchPanel.Controls.Add(searchBox);
            searchPanel.Controls.Add(datePicker);
            searchPanel.Controls.Add(searchButton);

            tabPage_HistoryAlarm.Controls.Add(historyAlarmGrid);
            tabPage_HistoryAlarm.Controls.Add(searchPanel);
        }

        private void ShowAlarmView()
        {
            if (alarmViewModel?.alarmModels == null) return;
            // 获取或创建报警列表面板
            Panel currentAlarmPanel = GetOrCreateAlarmPanel();

            // 获取或创建图表统计面板（上下分割）
            Panel chartStatsPanel = GetOrCreateChartStatsPanel();

            AlarmBox.Invoke(() =>
            {
                int yPos = 10;
                int alarmCount = Math.Min(alarmViewModel.alarmModels.Count, ALARM_CARD_POOL_SIZE);
                
                // 使用预实例化的卡片池 - 直接更新内容，不重新添加控件
                for (int i = 0; i < alarmCount; i++)
                {
                    var alarm = alarmViewModel.alarmModels[i];
                    Panel alarmCard = GetAlarmCardFromPool(i, alarm, yPos);
                    
                    // 如果卡片不在面板中，则添加；否则直接更新位置和内容
                    if (!currentAlarmPanel.Controls.Contains(alarmCard))
                    {
                        currentAlarmPanel.Controls.Add(alarmCard);
                    }
                    
                    yPos += 90; // 卡片高度 + 间距
                }

                // 隐藏未使用的卡片
                for (int i = alarmCount; i < ALARM_CARD_POOL_SIZE; i++)
                {
                    alarmCardPool[i].Visible = false;
                }
                // 不再自动更新图表，只在页面加载时更新一次，然后通过手动刷新按钮更新
            });
            
        }
        
        private Panel GetOrCreateAlarmPanel()
        {
            // 查找现有的报警面板
            foreach (Control control in AlarmBox.Panel1.Controls)
            {
                if (control is Panel panel && panel.Tag?.ToString() == "AlarmPanel")
                {
                    return panel;
                }
            }
            
            // 创建新的报警面板
            Panel currentAlarmPanel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = Color.White,
                Tag = "AlarmPanel" // 标记为报警面板
            };
            
            AlarmBox.Panel1.Controls.Add(currentAlarmPanel);
            return currentAlarmPanel;
        }
        
        private Panel GetOrCreateChartStatsPanel()
        {
            // 查找现有的图表统计面板
            foreach (Control control in AlarmBox.Panel2.Controls)
            {
                if (control is Panel panel && panel.Tag?.ToString() == "ChartStatsPanel")
                {
                    return panel;
                }
            }
            
            // 创建新的图表统计面板（上下分割）
            Panel chartStatsPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(245, 245, 245),
                Tag = "ChartStatsPanel" // 标记为图表统计面板
            };
            
            AlarmBox.Panel2.Controls.Add(chartStatsPanel);
            return chartStatsPanel;
        }
        
        private void UpdateChartStatsPanel(Panel chartStatsPanel)
        {
            // 清除旧的图表控件
            chartStatsPanel.Controls.Clear();
            
            // 创建顶部控制面板
            Panel topPanel = new Panel
            {
                Anchor= AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                Dock = DockStyle.Top,
                Height = 30,
                RightToLeft= RightToLeft.Yes,
                BackColor = Color.White
            };
            
            Button refreshButton = new Button
            {
                Text = "刷新图表",
                Location = new Point(10, 8),
                Size = new Size(100, 24),
                BackColor = Color.FromArgb(64, 158, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            refreshButton.FlatAppearance.BorderSize = 0;
            refreshButton.Click += (sender, e) =>
            {
                // 手动刷新图表
                UpdateChartStatsPanel(chartStatsPanel);
            };
            
            topPanel.Controls.Add(refreshButton);
            
            // 创建上下分割容器
            SplitContainer chartSplitContainer = new SplitContainer
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Horizontal,
                FixedPanel = FixedPanel.None
            };
            
            // 上半部分：柱状图显示每小时报警次数
            CreateHourlyBarChart(chartSplitContainer.Panel1);
            
            // 下半部分：左右两个饼图
            CreatePieCharts(chartSplitContainer.Panel2);
            
            chartStatsPanel.Controls.Add(topPanel);
            chartStatsPanel.Controls.Add(chartSplitContainer);
        }
        
        private void CreateHourlyBarChart(Panel container)
        {
            // 创建柱状图标题
            Label chartTitle = new Label
            {
                Text = "当班每小时报警次数",
                Font = new Font("Microsoft YaHei UI", 12, FontStyle.Bold),
                Location = new Point(10, 10),
                BackColor= Color.White,
                TextAlign= ContentAlignment.MiddleLeft,
                Size = new Size(300, 25),
                ForeColor = Color.FromArgb(64, 158, 219)
            };
            
            // 创建柱状图
            Chart barChart = new Chart
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(10, 40, 10, 10),
                BackColor = Color.White
            };
            
            // 创建图表区域
            ChartArea chartArea = new ChartArea("MainArea");
            chartArea.AxisX.Title = "小时";
            chartArea.AxisY.Title = "报警次数";
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisY.MajorGrid.Enabled = false;
            barChart.ChartAreas.Add(chartArea);
            
            // 创建数据系列
            Series series = new Series("报警次数");
            series.ChartType = SeriesChartType.Column;
            series.Color = Color.FromArgb(64, 158, 219);
            
            // 从alarmViewModel获取实际数据，显示前5个报警次数最频繁的时间段
            if (alarmViewModel?.AlarmNumberData != null && alarmViewModel.AlarmNumberData.Count > 0)
            {
                foreach (var hourData in alarmViewModel.AlarmNumberData)
                {
                    series.Points.AddXY(hourData.Time, hourData.Alarm);
                }
            }
            else
            {
                // 如果没有数据，显示空状态
                series.Points.AddXY("无数据", 0);
            }
            
            barChart.Series.Add(series);
            
            container.Controls.Add(chartTitle);
            container.Controls.Add(barChart);
        }
        
        private void CreatePieCharts(Panel container)
        {
            // 创建左右分割容器
            SplitContainer pieSplitContainer = new SplitContainer
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Vertical,
                SplitterDistance = container.Width / 2
            };

            // 左半部分：报警频率前5
            CreateDurationPieChart_1(pieSplitContainer.Panel1, "报警频率前5");
            
            // 右半部分：报警次数前5饼图
            CreateDurationPieChart_2(pieSplitContainer.Panel2, "报警次数前5");
            
            container.Controls.Add(pieSplitContainer);
        }
        
        private void CreateDurationPieChart_1(Panel container, string title)
        {
            // 创建饼图标题
            Label chartTitle = new Label
            {
                Text = title,
                Font = new Font("Microsoft YaHei UI", 10, FontStyle.Bold),
                Location = new Point(10, 10),
                BackColor=Color.White,
                TextAlign = ContentAlignment.MiddleLeft,
                ForeColor = Color.FromArgb(64, 158, 219)
            };
            
            // 创建饼图
            Chart pieChart = new Chart
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(10, 40, 10, 10),
                BackColor = Color.White
            };
            
            // 创建图表区域
            ChartArea chartArea = new ChartArea("PieArea");
            pieChart.ChartAreas.Add(chartArea);
            
            // 创建数据系列
            Series series = new Series("报警次数");
            series.ChartType = SeriesChartType.Pie;
            
            // 从alarmViewModel获取实际数据，显示报警次数最多的5个报警项
            if (alarmViewModel?.AlarmValues != null && alarmViewModel.AlarmValues.Count > 0)
            {
                Color[] colors = {Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.Blue, Color.Purple, Color.Pink, Color.Cyan, Color.Brown, Color.Gray};
                
                var top5Alarms = alarmViewModel.AlarmNumberData.OrderByDescending(x => x.Alarm).Take(5).ToList();

                for (int i = 0; i < top5Alarms.Count; i++)
                {
                    var alarmItem = top5Alarms[i];
                    DataPoint point = new DataPoint(0, alarmItem.Alarm);
                    point.AxisLabel = alarmItem.Time;
                    point.Color = colors[i % colors.Length];
                    point.LegendText = $"{alarmItem.Time}: {alarmItem.Alarm}";
                    series.Points.Add(point);
                }
            }
            else
            {
                // 如果没有数据，显示空状态
                DataPoint point = new DataPoint(0, 1);
                point.AxisLabel = "无数据";
                point.Color = Color.Gray;
                point.LegendText = "无数据";
                series.Points.Add(point);
            }
            
            pieChart.Series.Add(series);
            
            // 添加图例
            Legend legend = new Legend("PieLegend");
            legend.Docking = Docking.Bottom;
            pieChart.Legends.Add(legend);
            
            container.Controls.Add(chartTitle);
            container.Controls.Add(pieChart);
        }

        private void CreateDurationPieChart_2(Panel container, string title)
        {
            // 创建饼图标题
            Label chartTitle = new Label
            {
                Text = title,
                Font = new Font("Microsoft YaHei UI", 10, FontStyle.Bold),
                Location = new Point(10, 10),
                BackColor = Color.White,
                TextAlign = ContentAlignment.MiddleLeft,
                ForeColor = Color.FromArgb(64, 158, 219)
            };

            // 创建饼图
            Chart pieChart = new Chart
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(10, 40, 10, 10),
                BackColor = Color.White
            };

            // 创建图表区域
            ChartArea chartArea = new ChartArea("PieArea");
            pieChart.ChartAreas.Add(chartArea);

            // 创建数据系列
            Series series = new Series("报警次数");
            series.ChartType = SeriesChartType.Pie;

            // 从alarmViewModel获取实际数据，显示报警次数最多的5个报警项
            if (alarmViewModel?.AlarmValues != null && alarmViewModel.AlarmValues.Count > 0)
            {
                Color[] colors = { Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.Blue, Color.Purple, Color.Pink, Color.Cyan, Color.Brown, Color.Gray };

                var top5Alarms = alarmViewModel.AlarmValues.Take(5).ToList();

                for (int i = 0; i < top5Alarms.Count; i++)
                {
                    var alarmItem = top5Alarms[i];
                    DataPoint point = new DataPoint(0, alarmItem.Item2);
                    point.AxisLabel = alarmItem.Item1;
                    point.Color = colors[i % colors.Length];
                    point.LegendText = $"{alarmItem.Item1}: {alarmItem.Item2}";
                    series.Points.Add(point);
                }
            }
            else
            {
                // 如果没有数据，显示空状态
                DataPoint point = new DataPoint(0, 1);
                point.AxisLabel = "无数据";
                point.Color = Color.Gray;
                point.LegendText = "无数据";
                series.Points.Add(point);
            }

            pieChart.Series.Add(series);

            // 添加图例
            Legend legend = new Legend("PieLegend");
            legend.Docking = Docking.Bottom;
            pieChart.Legends.Add(legend);

            container.Controls.Add(chartTitle);
            container.Controls.Add(pieChart);
        }


        private Panel GetAlarmCardFromPool(int index, Alarm alarm, int yPos)
        {
            if (index >= alarmCardPool.Count) return CreateAlarmCard(alarm, yPos);
            
            Panel alarmCard = alarmCardPool[index];
            UpdateAlarmCardContent(alarmCard, alarm, yPos);
            alarmCard.Visible = true;
            return alarmCard;
        }
        
        private void UpdateAlarmCardContent(Panel alarmCard, Alarm alarm, int yPos)
        {
            alarmCard.Location = new Point(10, yPos);
            
            // 更新卡片内容
            if (alarmCard.Controls.Count >= 3)
            {
                Label alarmTypeLabel = (Label)alarmCard.Controls[0];
                Label alarmTextLabel = (Label)alarmCard.Controls[1];
                Label timeLabel = (Label)alarmCard.Controls[2];
                
                alarmTypeLabel.Text = alarm.AlarmType.ToString();
                alarmTextLabel.Text = alarm.AlarmText;
                timeLabel.Text = $"触发时间: {DateTime.Now:HH:mm:ss}";
            }
        }

        private Panel CreateAlarmCard(Alarm alarm, int yPos)
        {
            Panel alarmCard = new Panel
            {
                Size = new Size(300, 80),
                Location = new Point(10, yPos),
                BackColor = Color.FromArgb(255, 200, 200), // 默认红色背景表示报警
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(5)
            };

            Label alarmTypeLabel = new Label
            {
                Text = alarm.AlarmType.ToString(),
                Font = new Font("Microsoft YaHei UI", 10, FontStyle.Bold),
                Location = new Point(5, 5),
                Size = new Size(290, 20),
                ForeColor = Color.Red
            };

            Label alarmTextLabel = new Label
            {
                Text = alarm.AlarmText,
                Font = new Font("Microsoft YaHei UI", 9),
                Location = new Point(5, 30),
                Size = new Size(290, 20)
            };

            Label timeLabel = new Label
            {
                Text = $"触发时间: {DateTime.Now:HH:mm:ss}",
                Font = new Font("Microsoft YaHei UI", 8),
                Location = new Point(5, 55),
                Size = new Size(290, 20),
                ForeColor = Color.Gray
            };

            alarmCard.Controls.Add(alarmTypeLabel);
            alarmCard.Controls.Add(alarmTextLabel);
            alarmCard.Controls.Add(timeLabel);

            return alarmCard;
        }
        private void PageTable_Selected(object sender, TabControlEventArgs e)
        {
            switch (e.TabPageIndex)
            {
                case 0:
                    alarmViewModel?.OnInitialized();
                    break;
                case 1:
                    alarmDataViewModel?.OnInitialized();
                    break;
            }
        }
        private void PageTable_Deselecting(object sender, TabControlCancelEventArgs e)
        {
            switch (e.TabPageIndex)
            {
                case 0:
                    alarmViewModel?.Dispose();
                    break;
                case 1:
                    alarmDataViewModel?.Dispose();
                    break;
            }
        }
    }
}
