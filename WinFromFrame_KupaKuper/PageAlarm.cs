using KupaKuper_DeviceSever.Server;

using KupaKuper_HMI_Config.DeviceConfig.BaseType;

using WinFromFrame_KupaKuper.ViewModes;

namespace WinFromFrame_KupaKuper
{
    public partial class PageAlarm : Form
    {
        private DeviceSystemServer server;
        private AlarmDataViewMode? alarmDataViewMode;
        private AlarmViewMode? alarmViewMode;
        public PageAlarm(DeviceSystemServer _server)
        {
            InitializeComponent();
            this.server = _server;
        }
        private void PageAlarm_Load(object sender, EventArgs e)
        {
            alarmDataViewMode = new AlarmDataViewMode(server)
            {
                UpdataView = () =>
                {
                     ShowAlarmData();
                }
            };
            alarmViewMode = new AlarmViewMode(server) 
            { 
                UpdataView = () =>
                {
                    ShowAlarmView();
                }
            };
            alarmDataViewMode.SelectedDate = DateTime.Now;
            alarmViewMode?.OnInitialized();
        }

        private void ShowAlarmData()
        {
            if (alarmDataViewMode?.logEntries == null) return;
            
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
            foreach (var alarm in alarmDataViewMode.logEntries)
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
                Value = alarmDataViewMode.SelectedDate
            };

            Button searchButton = new Button
            {
                Location = new Point(380, 10),
                Size = new Size(80, 25),
                Text = "搜索"
            };

            searchBox.TextChanged += (sender, e) =>
            {
                alarmDataViewMode.SearchCurrentData(searchBox.Text);
                ShowAlarmData();
            };

            datePicker.ValueChanged += (sender, e) =>
            {
                alarmDataViewMode.SelectedDate = datePicker.Value;
            };

            searchButton.Click += (sender, e) =>
            {
                alarmDataViewMode.SearchCurrentData(searchBox.Text);
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
            if (alarmViewMode?.alarmModes == null) return;

            AlarmBox.Invoke(() =>
            {
                // 清除实时报警页面现有控件
                AlarmBox.Panel1.Controls.Clear();
                AlarmBox.Panel2.Controls.Clear();

                // 实时报警列表
                Panel currentAlarmPanel = new Panel
                {
                    Dock = DockStyle.Fill,
                    AutoScroll = true,
                    BackColor = Color.White
                };

                int yPos = 10;
                foreach (var alarm in alarmViewMode.alarmModes)
                {
                    Panel alarmCard = CreateAlarmCard(alarm, yPos);
                    currentAlarmPanel.Controls.Add(alarmCard);
                    yPos += 90; // 卡片高度 + 间距
                }

                // 报警统计信息
                Panel statsPanel = new Panel
                {
                    Dock = DockStyle.Fill,
                    AutoScroll = true,
                    BackColor = Color.FromArgb(245, 245, 245)
                };

                // 添加统计信息
                Label statsTitle = new Label
                {
                    Text = "报警统计",
                    Font = new Font("Microsoft YaHei UI", 14, FontStyle.Bold),
                    Location = new Point(10, 10),
                    Size = new Size(200, 30),
                    ForeColor = Color.FromArgb(64, 158, 219)
                };

                statsPanel.Controls.Add(statsTitle);

                // 显示报警统计排序
                int statYPos = 50;
                foreach (var (alarmType, count) in alarmViewMode.AlarmValues)
                {
                    Label statLabel = new Label
                    {
                        Text = $"{alarmType}: {count}次",
                        Font = new Font("Microsoft YaHei UI", 10),
                        Location = new Point(10, statYPos),
                        Size = new Size(300, 25)
                    };
                    statsPanel.Controls.Add(statLabel);
                    statYPos += 30;
                }

                AlarmBox.Panel1.Controls.Add(currentAlarmPanel);
                AlarmBox.Panel2.Controls.Add(statsPanel);
            });
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
                    alarmViewMode?.OnInitialized();
                    break;
                case 1:
                    alarmDataViewMode?.OnInitialized();
                    break;
            }
        }
        private void PageTable_Deselecting(object sender, TabControlCancelEventArgs e)
        {
            switch (e.TabPageIndex)
            {
                case 0:
                    alarmViewMode?.Dispose();
                    break;
                case 1:
                    alarmDataViewMode?.Dispose();
                    break;
            }
        }
    }
}
