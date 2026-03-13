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
                // 获取或创建报警列表面板
                Panel currentAlarmPanel = GetOrCreateAlarmPanel();
                
                // 获取或创建统计信息面板
                Panel statsPanel = GetOrCreateStatsPanel();
                
                int yPos = 10;
                int alarmCount = Math.Min(alarmViewMode.alarmModes.Count, ALARM_CARD_POOL_SIZE);
                
                // 使用预实例化的卡片池 - 直接更新内容，不重新添加控件
                for (int i = 0; i < alarmCount; i++)
                {
                    var alarm = alarmViewMode.alarmModes[i];
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

                // 更新统计信息
                UpdateStatsPanel(statsPanel);
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
        
        private Panel GetOrCreateStatsPanel()
        {
            // 查找现有的统计面板
            foreach (Control control in AlarmBox.Panel2.Controls)
            {
                if (control is Panel panel && panel.Tag?.ToString() == "StatsPanel")
                {
                    return panel;
                }
            }
            
            // 创建新的统计面板
            Panel statsPanel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = Color.FromArgb(245, 245, 245),
                Tag = "StatsPanel" // 标记为统计面板
            };
            
            AlarmBox.Panel2.Controls.Add(statsPanel);
            return statsPanel;
        }
        
        private void UpdateStatsPanel(Panel statsPanel)
        {
            // 清除旧的统计信息（保留标题）
            var controlsToRemove = new List<Control>();
            foreach (Control control in statsPanel.Controls)
            {
                if (control.Tag?.ToString() != "StatsTitle")
                {
                    controlsToRemove.Add(control);
                }
            }
            
            foreach (var control in controlsToRemove)
            {
                statsPanel.Controls.Remove(control);
                control.Dispose();
            }
            
            // 确保有统计标题
            Label? statsTitle = null;
            foreach (Control control in statsPanel.Controls)
            {
                if (control is Label label && label.Tag?.ToString() == "StatsTitle")
                {
                    statsTitle = label;
                    break;
                }
            }
            
            if (statsTitle == null)
            {
                statsTitle = new Label
                {
                    Text = "报警统计",
                    Font = new Font("Microsoft YaHei UI", 14, FontStyle.Bold),
                    Location = new Point(10, 10),
                    Size = new Size(200, 30),
                    ForeColor = Color.FromArgb(64, 158, 219),
                    Tag = "StatsTitle"
                };
                statsPanel.Controls.Add(statsTitle);
            }

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
