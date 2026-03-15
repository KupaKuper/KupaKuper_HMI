using KupaKuper_DeviceSever.Server;

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

            searchBox.TextChanged += (sender, e) =>
            {
                alarmDataViewModel.SearchCurrentData(searchBox.Text);
                ShowAlarmData();
            };

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
                // 更新图表统计信息
                UpdateChartStatsPanel(chartStatsPanel);
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
            
            // 创建上下分割容器
            SplitContainer chartSplitContainer = new SplitContainer
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Horizontal,
                SplitterDistance = 300, // 上半部分高度
                FixedPanel = FixedPanel.Panel1
            };
            
            // 上半部分：柱状图显示每小时报警次数
            CreateHourlyBarChart(chartSplitContainer.Panel1);
            
            // 下半部分：左右两个饼图
            CreatePieCharts(chartSplitContainer.Panel2);
            
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
                Size = new Size(300, 25),
                ForeColor = Color.FromArgb(64, 158, 219)
            };
            
            // 创建柱状图
            Chart barChart = new Chart
            {
                Location = new Point(10, 40),
                Size = new Size(container.Width - 30, container.Height - 60),
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
            
            // 添加示例数据（实际应从alarmViewModel获取）
            for (int hour = 0; hour < 24; hour++)
            {
                // 这里应该从alarmViewModel.AlarmNumberData获取实际数据
                int count = new Random().Next(0, 10); // 示例数据
                series.Points.AddXY($"{hour}-{hour+1}", count);
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
            
            // 左半部分：报警持续时间前5饼图
            CreateDurationPieChart(pieSplitContainer.Panel1, "报警持续时间前5");
            
            // 右半部分：报警次数前5饼图
            CreateCountPieChart(pieSplitContainer.Panel2, "报警次数前5");
            
            container.Controls.Add(pieSplitContainer);
        }
        
        private void CreateDurationPieChart(Panel container, string title)
        {
            // 创建饼图标题
            Label chartTitle = new Label
            {
                Text = title,
                Font = new Font("Microsoft YaHei UI", 10, FontStyle.Bold),
                Location = new Point(10, 10),
                Size = new Size(container.Width - 20, 20),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.FromArgb(64, 158, 219)
            };
            
            // 创建饼图
            Chart pieChart = new Chart
            {
                Location = new Point(10, 40),
                Size = new Size(container.Width - 30, container.Height - 60),
                BackColor = Color.White
            };
            
            // 创建图表区域
            ChartArea chartArea = new ChartArea("PieArea");
            pieChart.ChartAreas.Add(chartArea);
            
            // 创建数据系列
            Series series = new Series("持续时间");
            series.ChartType = SeriesChartType.Pie;
            
            // 添加示例数据（实际应从alarmViewModel获取）
            string[] alarmTypes = {"设备故障", "传感器异常", "通讯中断", "参数超限", "其他"};
            Color[] colors = {Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.Blue};
            
            for (int i = 0; i < 5; i++)
            {
                // 这里应该从实际数据获取报警持续时间
                double duration = new Random().Next(1, 100); // 示例数据
                DataPoint point = new DataPoint(0, duration);
                point.AxisLabel = alarmTypes[i];
                point.Color = colors[i];
                point.LegendText = alarmTypes[i];
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
        
        private void CreateCountPieChart(Panel container, string title)
        {
            // 创建饼图标题
            Label chartTitle = new Label
            {
                Text = title,
                Font = new Font("Microsoft YaHei UI", 10, FontStyle.Bold),
                Location = new Point(10, 10),
                Size = new Size(container.Width - 20, 20),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.FromArgb(64, 158, 219)
            };
            
            // 创建饼图
            Chart pieChart = new Chart
            {
                Location = new Point(10, 40),
                Size = new Size(container.Width - 30, container.Height - 60),
                BackColor = Color.White
            };
            
            // 创建图表区域
            ChartArea chartArea = new ChartArea("PieArea");
            pieChart.ChartAreas.Add(chartArea);
            
            // 创建数据系列
            Series series = new Series("报警次数");
            series.ChartType = SeriesChartType.Pie;
            
            // 添加示例数据（实际应从alarmViewModel.AlarmValues获取）
            if (alarmViewModel?.AlarmValues != null && alarmViewModel.AlarmValues.Count > 0)
            {
                int countToShow = Math.Min(5, alarmViewModel.AlarmValues.Count);
                Color[] colors = {Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.Blue};
                
                for (int i = 0; i < countToShow; i++)
                {
                    var (alarmType, count) = alarmViewModel.AlarmValues[i];
                    DataPoint point = new DataPoint(0, count);
                    point.AxisLabel = alarmType;
                    point.Color = colors[i % colors.Length];
                    point.LegendText = alarmType;
                    series.Points.Add(point);
                }
            }
            else
            {
                // 示例数据
                string[] alarmTypes = {"设备故障", "传感器异常", "通讯中断", "参数超限", "其他"};
                Color[] colors = {Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.Blue};
                
                for (int i = 0; i < 5; i++)
                {
                    int count = new Random().Next(1, 20); // 示例数据
                    DataPoint point = new DataPoint(0, count);
                    point.AxisLabel = alarmTypes[i];
                    point.Color = colors[i];
                    point.LegendText = alarmTypes[i];
                    series.Points.Add(point);
                }
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
