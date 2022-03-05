using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Diagnostics;
using System.Drawing.Drawing2D;

namespace WellaTodo
{
    public partial class CalendarForm : Form, IView, IModelObserver
    {
        public event ViewHandler<IView> View_Changed_Event;

        static readonly int CALENDAR_HEADER_HEIGHT = 50;
        static readonly int CALENDAR_WEEK_HEIGHT = 30;
        static readonly int CALENDAR_TASK_TEXT_HEIGHT = 15;
        static readonly int CALENDAR_DAY_TEXT_HEIGHT = 20;

        static readonly string FONT_NAME = "돋움";
        static readonly float FONT_SIZE_TITLE = 24.0f;
        static readonly float FONT_SIZE_TEXT = 14.0f;
        static readonly float FONT_SIZE_SMALL = 8.0f;

        static readonly Color PSEUDO_BACK_COLOR = Color.White;
        static readonly Color PSEUDO_HIGHLIGHT_COLOR = Color.LightCyan;
        static readonly Color PSEUDO_SELECTED_COLOR = Color.Cyan;
        static readonly Color PSEUDO_TEXTBOX_BACK_COLOR = Color.LightCyan;

        MainController m_Controller;

        Panel panel_Calendar = new Panel();
        Label labelCurrentDate = new Label();
        Button buttonToday = new Button();
        Button buttonPrevMonth = new Button();
        Button buttonNextMonth = new Button();
        FlowLayoutPanel[] dayPanel = new FlowLayoutPanel[42];
        DateTime m_dtValue = DateTime.Now;
        ToolTip m_TaskToolTip = new ToolTip();
        TaskEditForm taskEditForm = new TaskEditForm();

        int m_Find_Result_Day;
        Calendar_Item m_Find_Result_Item;

        public CalendarForm()
        {
            InitializeComponent();
        }

        public void SetController(MainController controller)
        {
            m_Controller = controller;
        }

        // --------------------------------------------------
        // Form 이벤트
        // --------------------------------------------------
        private void CalendarForm_Load(object sender, EventArgs e)
        {
            // 달력창
            panel_Calendar.Name = "panel_Calendar";
            panel_Calendar.Dock = DockStyle.Fill;
            panel_Calendar.Paint += new PaintEventHandler(CalendarForm_Paint);
            panel_Calendar.Resize += new EventHandler(CalendarForm_Resize);
            panel_Calendar.Location = new Point(0, 0);
            panel_Calendar.Size = new Size(Width, Height);
            panel_Calendar.BackColor = Color.White;
            Controls.Add(panel_Calendar);

            //Console.WriteLine("Initiate_Calendar");
            buttonToday.Size = new Size(50, 30);
            buttonToday.Location = new Point(20, 10);
            buttonToday.Click += new EventHandler(buttonToday_Click);
            buttonToday.Text = "오늘";
            panel_Calendar.Controls.Add(buttonToday);

            buttonPrevMonth.Size = new Size(50, 30);
            buttonPrevMonth.Location = new Point(80, 10);
            buttonPrevMonth.Click += new EventHandler(buttonPrevMonth_Click);
            buttonPrevMonth.Text = "이전달";
            panel_Calendar.Controls.Add(buttonPrevMonth);

            buttonNextMonth.Size = new Size(50, 30);
            buttonNextMonth.Location = new Point(140, 10);
            buttonNextMonth.Click += new EventHandler(buttonNextMonth_Click);
            buttonNextMonth.Text = "다음달";
            panel_Calendar.Controls.Add(buttonNextMonth);

            labelCurrentDate.Font = new Font(FONT_NAME, FONT_SIZE_TITLE);
            labelCurrentDate.AutoSize = true;
            labelCurrentDate.Size = new Size(200, CALENDAR_HEADER_HEIGHT);
            labelCurrentDate.Location = new Point(200, 10);
            labelCurrentDate.Text = "2020년 1월";
            panel_Calendar.Controls.Add(labelCurrentDate);

            for (int i = 0; i < 42; i++)
            {
                dayPanel[i] = new FlowLayoutPanel();
                dayPanel[i].Size = new Size(1, 1);
                dayPanel[i].BackColor = PSEUDO_BACK_COLOR;
                dayPanel[i].MouseDoubleClick += new MouseEventHandler(DayPanel_MouseDoubleClick);
                dayPanel[i].AllowDrop = true;
                dayPanel[i].DragEnter += new DragEventHandler(DayPanel_DragEnter);
                dayPanel[i].DragDrop += new DragEventHandler(DayPanel_DragDrop);
                dayPanel[i].DragOver += new DragEventHandler(DayPanel_DragOver);
                dayPanel[i].DragLeave += new EventHandler(DayPanel_DragLeave);
                panel_Calendar.Controls.Add(dayPanel[i]);
            }

            SetDate(m_dtValue); // 현재 날짜로 달력 열기
        }

        private void CalendarForm_Paint(object sender, PaintEventArgs e)
        {
            //Console.WriteLine("CalendarForm_Paint");
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            Font font = new Font(FONT_NAME, FONT_SIZE_TEXT, FontStyle.Bold);
            Pen pen = new Pen(Color.Black, 1.0f);
            SolidBrush brush = new SolidBrush(Color.Black);

            int x = 0;
            int y = 0;
            int w = panel_Calendar.Size.Width;
            int h = panel_Calendar.Size.Height;
            g.FillRectangle(new SolidBrush(Color.White), x - 1, y - 1, w + 1, h + 1);
            g.DrawRectangle(new Pen(Color.Black, 1.0f), x, y, w - 1, h - 1);
            g.DrawRectangle(new Pen(Color.Black, 1.0f), x, y, w - 1, CALENDAR_HEADER_HEIGHT);
            g.DrawRectangle(new Pen(Color.Black, 1.0f), x, y + CALENDAR_HEADER_HEIGHT, w - 1, CALENDAR_WEEK_HEIGHT);

            // Week 표시
            int num_WeeksInMonth = Calc_NumOfWeekInMonth(m_dtValue);
            int w_gap = (w / 7);
            int h_gap = ((h - CALENDAR_HEADER_HEIGHT - CALENDAR_WEEK_HEIGHT) / num_WeeksInMonth);
            string weekName;
            for (int i = 0; i < 7; i++)
            {
                weekName = System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.AbbreviatedDayNames[i][0].ToString();

                int tx = x + i * w_gap;
                int ty = y + CALENDAR_HEADER_HEIGHT;
                int tw = w_gap;
                int th = CALENDAR_WEEK_HEIGHT;
                RectangleF drawRect = new RectangleF(tx, ty, tw, th);
                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Center;
                stringFormat.LineAlignment = StringAlignment.Center;

                if (i == 0) // 일요일은 RED
                    g.DrawString(weekName, font, new SolidBrush(Color.Red), drawRect, stringFormat);
                else
                    g.DrawString(weekName, font, brush, drawRect, stringFormat);

                if (i == 6)
                    g.DrawRectangle(pen, x, y + CALENDAR_HEADER_HEIGHT, w, h - CALENDAR_HEADER_HEIGHT);
                else
                    g.DrawRectangle(pen, x + i * w_gap, y + CALENDAR_HEADER_HEIGHT, w_gap, h - CALENDAR_HEADER_HEIGHT);
            }

            // Day 표시
            int line_sx = x;
            int line_sy = y + CALENDAR_HEADER_HEIGHT + CALENDAR_WEEK_HEIGHT;
            int pos = 0;
            for (int j = 0; j < num_WeeksInMonth; j++)
            {
                for (int i = 0; i < 7; i++)
                {
                    if (i == 6)
                    {
                        if (j == num_WeeksInMonth - 1)
                            g.DrawRectangle(pen, line_sx, line_sy, w, h);
                        else
                            g.DrawRectangle(pen, line_sx, line_sy + j * h_gap, w, h_gap);
                    }
                    else
                    {
                        if (j == num_WeeksInMonth - 1)
                            g.DrawRectangle(pen, line_sx + i * w_gap, line_sy, w_gap, h);
                        else
                            g.DrawRectangle(pen, line_sx + i * w_gap, line_sy + j * h_gap, w_gap, h_gap);
                    }
                    dayPanel[pos].Location = new Point(line_sx + 1 + i * w_gap, line_sy + 1 + j * h_gap);
                    dayPanel[pos].Size = new Size(w_gap - 2, h_gap - 2);
                    foreach (Control ctr in dayPanel[pos].Controls)  // TASK 폭 조정
                    {
                        ctr.Width = dayPanel[pos].Width;
                    }
                    pos++;
                }
            }
        }

        private void CalendarForm_Resize(object sender, EventArgs e)
        {
            panel_Calendar.Refresh();
        }

        private void CalendarForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        //--------------------------------------------------------------
        // 초기화 및 Update Display
        //--------------------------------------------------------------
        private void SetDate(DateTime dt)
        {
            Send_Log_Message(">CalendarForm::SetDate -> m_dtValue : " + dt.ToLongDateString());

            m_dtValue = dt;

            DateTime startDate = new DateTime(dt.Year, dt.Month, 1, 0, 0, 0);
            int num_DaysInMonth = DateTime.DaysInMonth(dt.Year, dt.Month);
            int num_DayOfWeek = (int)startDate.DayOfWeek;
            DateTime endDate = new DateTime(dt.Year, dt.Month, num_DaysInMonth, 23, 59, 59);

            labelCurrentDate.Text = dt.Year.ToString() + "년 " + dt.Month.ToString() + "월";

            for (int i = 0; i < dayPanel.Length; i++) // eventhandler 제거 및 dayPanel 클리어
            {
                foreach (Control ctr in dayPanel[i].Controls)
                {
                    if (ctr is Calendar_Item)
                    {
                        Calendar_Item list = (Calendar_Item)ctr;
                        list.Calendar_Item_Click -= new Calendar_Item_Event(Calendar_Item_Click);
                    }
                }
                dayPanel[i].Controls.Clear();
            }

            // 날짜 표시 -> 오늘/공휴일 표시할 것
            int preDays = (new int[] { 0, 1, 2, 3, 4, 5, 6 })[(int)startDate.DayOfWeek];
            DateTime curDate = startDate.AddDays(-preDays);
            for (int i = 0; i < dayPanel.Length; i++)
            {
                Calendar_Day label_Day = new Calendar_Day();

                label_Day.Present_Day = curDate;

                if (curDate.Day == 1) // 매월 1일은 월과 함께 표기
                {
                    label_Day.Text = curDate.Month.ToString() + "/" + curDate.Day.ToString();
                }
                else
                {
                    label_Day.Text = curDate.Day.ToString();
                }

                if (curDate.Month == startDate.Month) // 이전달 & 다음달 배경색 변경
                {
                    dayPanel[i].BackColor = Color.White;
                }
                else
                {
                    dayPanel[i].BackColor = Color.LightGray;
                }

                if (curDate == DateTime.Today) // 오늘은 Violet 색상으로 변경
                {
                    label_Day.BackColor = Color.Violet;
                }

                if ((i % 7) == 0) // 일요일은 RED 색상으로 변경
                {
                    label_Day.ForeColor = Color.Red;
                }

                string holiday = IsHoliDay(curDate);
                if (holiday != "평일") // 공휴일은 RED 색상으로 변경
                {
                    label_Day.ForeColor = Color.Red;
                    label_Day.Text = label_Day.Text + " " + holiday;
                }

                label_Day.Font = new Font(FONT_NAME, FONT_SIZE_TEXT, FontStyle.Bold);
                label_Day.Height = CALENDAR_DAY_TEXT_HEIGHT;

                dayPanel[i].Controls.Add(label_Day);

                curDate = curDate.AddDays(1);
            }

            // Task 표시
            curDate = startDate.AddDays(-preDays);
            for (int i = 0; i < dayPanel.Length; i++)
            {
                IEnumerable<CDataCell> dataset = m_Controller.Query_Month_Calendar(curDate);
                foreach (CDataCell dc in dataset)
                {
                    Calendar_Item label_planned = new Calendar_Item(dc);
                    label_planned.Calendar_Item_Click -= new Calendar_Item_Event(Calendar_Item_Click);
                    label_planned.Calendar_Item_Click += new Calendar_Item_Event(Calendar_Item_Click); // event 제거할 것
                    label_planned.Font = dc.DC_complete
                        ? new Font(FONT_NAME, FONT_SIZE_SMALL, FontStyle.Strikeout)
                        : new Font(FONT_NAME, FONT_SIZE_SMALL, FontStyle.Regular);
                    label_planned.BackColor = DateTime.Compare(curDate.Date, DateTime.Today.Date) < 0
                        ? PSEUDO_HIGHLIGHT_COLOR
                        : PSEUDO_SELECTED_COLOR;
                    label_planned.AutoSize = false;

                    label_planned.Width = dayPanel[i].Width;
                    label_planned.Height = CALENDAR_TASK_TEXT_HEIGHT;

                    //m_TaskToolTip.IsBalloon = true;
                    //m_TaskToolTip.ToolTipTitle = "Calendar";
                    //m_TaskToolTip.ToolTipIcon = ToolTipIcon.Info;
                    //m_TaskToolTip.ShowAlways = true;
                    m_TaskToolTip.SetToolTip(label_planned, dc.DC_title);

                    dayPanel[i].Controls.Add(label_planned);
                }
                curDate = curDate.AddDays(1);
            }

            panel_Calendar.Refresh(); // refresh 해야함
        }

        //--------------------------------------------------------------
        // 처리 메서드
        //--------------------------------------------------------------
        private int Calc_NumOfWeekInMonth(DateTime dt)
        {
            DateTime startDate = new DateTime(dt.Year, dt.Month, 1);
            int num_DaysInMonth = DateTime.DaysInMonth(dt.Year, dt.Month);
            int num_DayOfWeek = (int)startDate.DayOfWeek;

            int result = (num_DaysInMonth + num_DayOfWeek) / 7;
            int mod = (num_DaysInMonth + num_DayOfWeek) % 7;
            return mod == 0 ? result : result + 1;
        }

        private bool FindCalendarItem(CDataCell dc)
        {
            for (int i = 0; i < dayPanel.Length; i++)
            {
                foreach (Control ctr in dayPanel[i].Controls)
                {
                    if (ctr is Calendar_Item)
                    {
                        Calendar_Item item = (Calendar_Item)ctr;
                        if (dc.DC_task_ID == item.CD_DataCell.DC_task_ID)
                        {
                            m_Find_Result_Day = i;
                            m_Find_Result_Item = item;
                            return true;
                        }
                    }
                }
            }

            // 없을 경우 신규 아이템 생성후 리턴
            Calendar_Item newItem = new Calendar_Item(dc);
            newItem.Calendar_Item_Click -= new Calendar_Item_Event(Calendar_Item_Click);
            newItem.Calendar_Item_Click += new Calendar_Item_Event(Calendar_Item_Click); // event 제거할 것
            newItem.AutoSize = false;

            newItem.Width = dayPanel[0].Width;
            newItem.Height = CALENDAR_TASK_TEXT_HEIGHT;
            m_TaskToolTip.SetToolTip(newItem, dc.DC_title);

            m_Find_Result_Item = newItem;
            return false;
        }

        private bool IsCurrentPage(DateTime dt)
        {
            DateTime startDate = new DateTime(m_dtValue.Year, m_dtValue.Month, 1, 0, 0, 0);
            int preDays = (new int[] { 0, 1, 2, 3, 4, 5, 6 })[(int)startDate.DayOfWeek];
            startDate = startDate.AddDays(-preDays);
            DateTime endDate = startDate.AddDays(42);

            if (dt.CompareTo(startDate) >= 0 && dt.CompareTo(endDate) <= 0)
            {
                return true;
            }
            return false;
        }

        //--------------------------------------------------------------
        // Model 이벤트
        //--------------------------------------------------------------
        public void Update_View(IModel m, ModelEventArgs e)
        {
            CDataCell dc = e.Item;
            WParam param = e.Param;
            switch (param)
            {
                case WParam.WM_OPEN_DATA:
                    Update_Open_Data();
                    break;
                case WParam.WM_COMPLETE_PROCESS:
                    Update_Complete_Process(dc);
                    break;
                case WParam.WM_IMPORTANT_PROCESS:
                    Update_Important_Process(dc);
                    break;
                case WParam.WM_MODIFY_TASK_TITLE:
                    Update_Modify_Task_Title(dc);
                    break;
                case WParam.WM_MODIFY_TASK_MEMO:
                    break;
                case WParam.WM_TASK_DELETE:
                    Update_Delete_Task(dc);
                    break;
                case WParam.WM_MODIFY_MYTODAY:
                    break;
                case WParam.WM_MODIFY_REMIND:
                    break;
                case WParam.WM_MODIFY_PLANNED:
                    Update_Modify_Planned(dc);
                    break;
                case WParam.WM_MODIFY_REPEAT:
                    break;
                case WParam.WM_MENULIST_RENAME:
                    Update_Menulist_Rename(dc);
                    break;
                case WParam.WM_MENULIST_DELETE:
                    Update_Menulist_Delete(dc);
                    break;
                case WParam.WM_TRANSFER_TASK:
                    Update_Transfer_Task(dc);
                    break;
                case WParam.WM_PLAN_ADD:
                    Update_Add_Plan(dc);
                    break;
                default:
                    break;
            }
        }

        private void Send_Log_Message(string msg)
        {
            try
            {
                View_Changed_Event.Invoke(this, new ViewEventArgs(msg));
            }
            catch (Exception)
            {
                MessageBox.Show("Please enter a valid number");
            }
        }

        private void Update_Open_Data()
        {
            SetDate(m_dtValue);
        }

        private void Update_Complete_Process(CDataCell dc)
        {
            Send_Log_Message("4>CalendarForm::Update_Complete_Process");

            if (FindCalendarItem(dc))
            {
                Send_Log_Message("4>CalendarForm::Update_Complete_Process -> Find matching item : " + dc.DC_title);
                m_Find_Result_Item.Font = (dc.DC_complete || dc.DC_archive)
                                       ? new Font(FONT_NAME, FONT_SIZE_SMALL, FontStyle.Strikeout)
                                       : new Font(FONT_NAME, FONT_SIZE_SMALL, FontStyle.Regular);
            }
        }

        private void Update_Important_Process(CDataCell dc)
        {
            Send_Log_Message("4>CalendarForm::Update_Important_Process" + dc.DC_title);
        }

        private void Update_Modify_Task_Title(CDataCell dc)
        {
            if (FindCalendarItem(dc))
            {
                Send_Log_Message("4>CalendarForm::Update_Modify_Task_Title -> Find matching item : " + dc.DC_title);
                m_Find_Result_Item.PrimaryText = dc.DC_title;
            }
            Send_Log_Message("4>CalendarForm::Update_Modify_Task_Title");
        }

        private void Update_Add_Plan(CDataCell dc)
        {
            DateTime dt = dc.DC_deadlineTime;

            if (IsCurrentPage(dt))  // New Task가 현재 화면에 있나?
            {
                Calendar_Item label_planned = new Calendar_Item(dc);
                label_planned.Calendar_Item_Click -= new Calendar_Item_Event(Calendar_Item_Click);
                label_planned.Calendar_Item_Click += new Calendar_Item_Event(Calendar_Item_Click); // event 제거할 것
                label_planned.Font = dc.DC_complete
                    ? new Font(FONT_NAME, FONT_SIZE_SMALL, FontStyle.Strikeout)
                    : new Font(FONT_NAME, FONT_SIZE_SMALL, FontStyle.Regular);
                label_planned.BackColor = DateTime.Compare(dt.Date, DateTime.Today.Date) < 0
                    ? PSEUDO_HIGHLIGHT_COLOR
                    : PSEUDO_SELECTED_COLOR;
                label_planned.AutoSize = false;

                int pos = 0;
                int counter = 0;
                for (int i = 0; i < dayPanel.Length; i++)
                {
                    foreach (Control ctr in dayPanel[i].Controls)
                    {
                        if (ctr is Calendar_Day)
                        {
                            Calendar_Day item = (Calendar_Day)ctr;
                            if (dt.Date == item.Present_Day.Date)
                            {
                                pos = i;
                                counter++;
                            }
                        }
                    }
                }

                if (counter == 0)
                {
                    Send_Log_Message("Warning>CalendarForm::Update_Add_Plan -> Can not Found Item");
                }

                label_planned.Width = dayPanel[pos].Width;
                label_planned.Height = CALENDAR_TASK_TEXT_HEIGHT;
                m_TaskToolTip.SetToolTip(label_planned, dc.DC_title);
                dayPanel[pos].Controls.Add(label_planned);

                Send_Log_Message("4>CalendarForm::Update_Add_Plan -> Current Month Calendar " + dt.ToLongDateString());
            }
        }

        private void Update_Delete_Task(CDataCell dc)
        {
            Send_Log_Message("4>CalendarForm::Update_Delete_Task");

            if (FindCalendarItem(dc))
            {
                Send_Log_Message("4>CalendarForm::Update_Delete_Task -> Find matching item : " + dc.DC_title);
                dayPanel[m_Find_Result_Day].Controls.Remove(m_Find_Result_Item);
            }
        }

        private void Update_Modify_Planned(CDataCell dc)
        {
            Send_Log_Message("4>CalendarForm::Update_Modify_Planned");
            int day = 0;
            DateTime dt = dc.DC_deadlineTime;    // 변경후 날짜

            bool cond_1 = FindCalendarItem(dc);  // 변경전이 현재 화면에 있나?
            bool cond_2 = IsCurrentPage(dt);     // 변경후가 현재 화면에 있나?

            if (cond_2)
            {
                DateTime startDate = new DateTime(m_dtValue.Year, m_dtValue.Month, 1, 0, 0, 0);
                int preDays = (new int[] { 0, 1, 2, 3, 4, 5, 6 })[(int)startDate.DayOfWeek];
                startDate = startDate.AddDays(-preDays);
                day = (dt - startDate).Days;
                //Send_Log_Message("4>CalendarForm::Update_Modify_Planned -> startDay : " + startDate .ToShortDateString());
                //Send_Log_Message("4>CalendarForm::Update_Modify_Planned -> dt : " + dt.ToShortDateString());
                //Send_Log_Message("4>CalendarForm::Update_Modify_Planned -> 날짜차이(42이내) : " + day);
            }

            m_Find_Result_Item.Font = dc.DC_complete
                            ? new Font(FONT_NAME, FONT_SIZE_SMALL, FontStyle.Strikeout)
                            : new Font(FONT_NAME, FONT_SIZE_SMALL, FontStyle.Regular);
            m_Find_Result_Item.BackColor = DateTime.Compare(dc.DC_deadlineTime.Date, DateTime.Today.Date) < 0
                ? PSEUDO_HIGHLIGHT_COLOR
                : PSEUDO_SELECTED_COLOR;

            if (cond_1 == true && cond_2 == true)  // 삭제후 표기
            {
                Send_Log_Message("4>CalendarForm::Update_Modify_Planned -> 삭제후 표기 :" + day);
                dayPanel[m_Find_Result_Day].Controls.Remove(m_Find_Result_Item);  // 변경전 항목 제거
                dayPanel[day].Controls.Add(m_Find_Result_Item);  // 변경된 날짜에 항목을 추가한다
            }
            else if (cond_1 == true && cond_2 == false)  // 삭제
            {
                Send_Log_Message("4>CalendarForm::Update_Modify_Planned -> 삭제");
                dayPanel[m_Find_Result_Day].Controls.Remove(m_Find_Result_Item);  // 변경전 항목 제거
            }
            else if (cond_1 == false && cond_2 == true)  // 표기
            {
                Send_Log_Message("4>CalendarForm::Update_Modify_Planned -> 표기:" + day);
                dayPanel[day].Controls.Add(m_Find_Result_Item);  // 변경된 날짜에 항목을 추가한다
            }
            else
            {
                Send_Log_Message("4>CalendarForm::Update_Modify_Planned -> 변경없음");
            }
        }

        private void Update_Menulist_Rename(CDataCell dc)
        {

        }

        private void Update_Menulist_Delete(CDataCell dc)
        {
            Send_Log_Message("4>CalendarForm::Update_Menulist_Delete -> SetDate(m_dtValue)");
            SetDate(m_dtValue);
        }

        private void Update_Transfer_Task(CDataCell dc)
        {

        }

        //--------------------------------------------------------------
        // 사용자 입력 처리
        //--------------------------------------------------------------
        private void DayPanel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            FlowLayoutPanel dayPanel = (FlowLayoutPanel)sender;
            DateTime dt = DateTime.Now;

            foreach (Control ctr in dayPanel.Controls)
            {
                if (ctr is Calendar_Day)
                {
                    Calendar_Day item = (Calendar_Day)ctr;
                    dt = item.Present_Day;
                }
            }

            taskEditForm.StartPosition = FormStartPosition.CenterParent;

            taskEditForm.TE_DataCell = new CDataCell();
            taskEditForm.TE_DataCell.DC_deadlineType = 4;
            taskEditForm.TE_DataCell.DC_deadlineTime = new DateTime(dt.Year, dt.Month, dt.Day, 22, 00, 00);

            taskEditForm.IsNewTask = true;

            taskEditForm.ShowDialog();

            if (taskEditForm.IsCreated)
            {
                Send_Log_Message("1>CalendarForm::DayPanel_MouseDoubleClick -> New Task Created : " + taskEditForm.TE_DataCell.DC_title
                    + "[" + taskEditForm.TE_DataCell.DC_deadlineTime.ToLongDateString() + "]");

                m_Controller.Perform_Add_Plan(taskEditForm.TE_DataCell);
            }
            else
            {
                Send_Log_Message("1>CalendarForm::DayPanel_MouseDoubleClick -> Create New Task is Canceled!!");
            }

            taskEditForm.IsNewTask = false;
            taskEditForm.IsCreated = false;
        }

        private void buttonToday_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            SetDate(dt);
        }

        private void buttonPrevMonth_Click(object sender, EventArgs e)
        {
            DateTime dt = m_dtValue.AddMonths(-1);
            SetDate(dt);
        }

        private void buttonNextMonth_Click(object sender, EventArgs e)
        {
            DateTime dt = m_dtValue.AddMonths(1);
            SetDate(dt);
        }

        private void Calendar_Item_Click(object sender, EventArgs e)
        {
            Calendar_Item sd = (Calendar_Item)sender;

            taskEditForm.StartPosition = FormStartPosition.CenterParent;

            IEnumerable<CDataCell> dataset = m_Controller.Query_Task_Calendar(sd.CD_DataCell);
            CDataCell dc = dataset.First();

            taskEditForm.TE_DataCell = (CDataCell)dc.Clone();

            taskEditForm.ShowDialog();

            dc = taskEditForm.TE_DataCell;

            if (taskEditForm.IsCompleteChanged)
            {
                Send_Log_Message("1>CalendarForm::Calendar_Item_Click -> Complete Changed : " + dc.DC_complete);
                m_Controller.Perform_Complete_Process(dc);
                taskEditForm.IsCompleteChanged = false;
            }

            if (taskEditForm.IsImportantChanged)
            {
                Send_Log_Message("1>CalendarForm::Calendar_Item_Click -> Important Changed : " + dc.DC_important);
                m_Controller.Perform_Important_Process(dc);
                taskEditForm.IsImportantChanged = false;
            }

            if (taskEditForm.IsTitleChanged)
            {
                Send_Log_Message("1>CalendarForm::Calendar_Item_Click -> Title Changed : " + dc.DC_title);
                m_Controller.Perform_Modify_Task_Title(dc);
                taskEditForm.IsTitleChanged = false;
            }

            if (taskEditForm.IsMemoChanged)
            {
                Send_Log_Message("1>CalendarForm::Calendar_Item_Click -> Memo Changed : " + dc.DC_title);
                m_Controller.Perform_Modify_Task_Memo(dc);
                taskEditForm.IsMemoChanged = false;
            }

            if (taskEditForm.IsDeleted) // 목록 삭제
            {
                Send_Log_Message("1>CalendarForm::Calendar_Item_Click -> Task Delete");
                m_Controller.Perform_Delete_Task(dc);
                taskEditForm.IsDeleted = false;
            }

            if (taskEditForm.IsPlannedChanged) // 기한 설정 변경
            {
                dc.DC_deadlineType = 4;

                Send_Log_Message("1>CalendarForm::Calendar_Item_Click -> Planned Changed");
                m_Controller.Perform_Modify_Planned(dc);
                taskEditForm.IsPlannedChanged = false;
            }

            if (taskEditForm.IsPlannedDeleted) // 기한 설정 해제
            {
                dc.DC_deadlineType = 0;
                dc.DC_deadlineTime = default;

                Send_Log_Message("1>CalendarForm::Calendar_Item_Click -> Planned Deleted");
                m_Controller.Perform_Modify_Planned(dc);
                taskEditForm.IsPlannedDeleted = false;
            }
        }

        //--------------------------------------------------------------
        // 드래그 앤 드롭 처리
        //--------------------------------------------------------------
        private void DayPanel_DragOver(object sender, DragEventArgs e)
        {
            FlowLayoutPanel dp = (FlowLayoutPanel )sender;
            dp.BackColor = PSEUDO_HIGHLIGHT_COLOR;
        }

        private void DayPanel_DragLeave(object sender, EventArgs e)
        {
            FlowLayoutPanel dp = (FlowLayoutPanel)sender;
            dp.BackColor = PSEUDO_BACK_COLOR;
        }

        private void DayPanel_DragEnter(object sender, DragEventArgs e)
        {
            //Console.WriteLine("DayPanel_DragEnter");
            if (e.Data.GetDataPresent(typeof(Todo_Item)))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else if(e.Data.GetDataPresent(typeof(Calendar_Item)))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void DayPanel_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(Todo_Item)))
            {
                Todo_Item item = e.Data.GetData(typeof(Todo_Item)) as Todo_Item;
                //Console.WriteLine("DayPanel_DragDrop -> source : " + item.TD_title);
                Point p = panel_Calendar.PointToClient(new Point(e.X, e.Y));
                FlowLayoutPanel dp = (FlowLayoutPanel)panel_Calendar.GetChildAtPoint(p);

                Calendar_Day planned_day = null;
                foreach (Control ctr in dp.Controls)
                {
                    if (ctr is Calendar_Day)
                    {
                        planned_day = (Calendar_Day)ctr;
                        break;
                    }
                }
                dp.BackColor = PSEUDO_BACK_COLOR;

                Send_Log_Message("1>CalendarForm::DayPanel_DragDrop -> Create New Task at DragDrop Selected Day");

                DateTime dt = planned_day.Present_Day;
                dt = new DateTime(dt.Year, dt.Month, dt.Day, 22, 00, 00);

                item.TD_DataCell.DC_deadlineType = 4;
                item.TD_DataCell.DC_deadlineTime = dt;

                m_Controller.Perform_Modify_Planned(item.TD_DataCell);
            }
            else if (e.Data.GetDataPresent(typeof(Calendar_Item)))
            {
                Calendar_Item item = e.Data.GetData(typeof(Calendar_Item)) as Calendar_Item;
                //Console.WriteLine("DayPanel_DragDrop -> source : " + item.PrimaryText);
                Point p = panel_Calendar.PointToClient(new Point(e.X, e.Y));
                FlowLayoutPanel dp = (FlowLayoutPanel)panel_Calendar.GetChildAtPoint(p);

                Calendar_Day planned_day = null;
                foreach (Control ctr in dp.Controls)
                {
                    if (ctr is Calendar_Day)
                    {
                        planned_day = (Calendar_Day)ctr;
                        break;
                    }
                }
                dp.BackColor = PSEUDO_BACK_COLOR;

                Send_Log_Message("1>CalendarForm::DayPanel_DragDrop -> Move Task at DragDrop Selected Day");

                DateTime dt = planned_day.Present_Day;
                dt = new DateTime(dt.Year, dt.Month, dt.Day, 22, 00, 00);

                item.CD_DataCell.DC_deadlineType = 4;
                item.CD_DataCell.DC_deadlineTime = dt;

                m_Controller.Perform_Modify_Planned(item.CD_DataCell);
            }
            else
            {
                
            }
        }

        // --------------------------------------------------
        // 양력을 음력 변환
        // --------------------------------------------------
        private DateTime ConvertSolarToLunar(int year, int month, int day)
        {
            int LeapMonth;
            int LanarYear, LanarMonth, LanarDay;
            bool isLeapMonth = false;
            DateTime dt = new DateTime(year, month, day);

            System.Globalization.KoreanLunisolarCalendar LanarCalendar = new System.Globalization.KoreanLunisolarCalendar();

            LanarYear = LanarCalendar.GetYear(dt);
            LanarMonth = LanarCalendar.GetMonth(dt);
            LanarDay = LanarCalendar.GetDayOfMonth(dt);
            if (LanarCalendar.GetMonthsInYear(LanarYear) > 12) //1년이 12이상이면 윤달이 있음..
            {
                isLeapMonth = LanarCalendar.IsLeapMonth(LanarYear, LanarMonth); //윤월인지
                LeapMonth = LanarCalendar.GetLeapMonth(LanarYear); //년도의 윤달이 몇월인지?
                if (LanarMonth >= LeapMonth) LanarMonth--; //달이 윤월보다 같거나 크면 -1을 함 즉 윤8은->9 이기때문
            }
            return new DateTime(int.Parse(LanarYear.ToString()), int.Parse(LanarMonth.ToString()), int.Parse(LanarDay.ToString()));
        }

        // --------------------------------------------------
        // 음력을 양력 변환
        // --------------------------------------------------
        private DateTime ConvertLunarToSolar(int LanarYear, int LanarMonth, int LanarDay)
        {
            System.Globalization.KoreanLunisolarCalendar LanarCalendar = new System.Globalization.KoreanLunisolarCalendar();

            bool isLeapMonth = LanarCalendar.IsLeapMonth(LanarYear, LanarMonth);
            int LeapMonth;

            if (LanarCalendar.GetMonthsInYear(LanarYear) > 12)
            {
                LeapMonth = LanarCalendar.GetLeapMonth(LanarYear);
                if (isLeapMonth) LanarMonth++;
                if (LanarMonth > LeapMonth) LanarMonth++;
            }

            //LanarCalendar은 마지막 날짜가 매달 다르기 때문에 예외 뜨면 그날 맨 마지막 날로 지정 -> 오류수정할 것
            try
            {
                LanarCalendar.ToDateTime(LanarYear, LanarMonth, LanarDay, 0, 0, 0, 0);
            }
            catch
            {
                return LanarCalendar.ToDateTime(LanarYear, LanarMonth, LanarCalendar.GetDaysInMonth(LanarYear, LanarMonth), 0, 0, 0, 0);
            }
            
            return LanarCalendar.ToDateTime(LanarYear, LanarMonth, LanarDay, 0, 0, 0, 0);
        }

        // --------------------------------------------------
        // 공휴일 Check
        // --------------------------------------------------
        private string IsHoliDay(DateTime dt)
        {
            string dayName = "평일";
            int year = dt.Year;
            int month = dt.Month;
            int day = dt.Day;

            // 양력 공휴일
            if (month == 1 && day == 1) return "신정";
            if (month == 3 && day == 1) return "삼일절";
            if (month == 5 && day == 5) return "어린이날";
            if (month == 6 && day == 6) return "현충일";
            if (month == 8 && day == 15) return "광복절";
            if (month == 10 && day == 3) return "개천절";
            if (month == 10 && day == 9) return "한글날";
            if (month == 12 && day == 25) return "크리스마스";

            // 음력 공휴일
            DateTime solar;
            solar = ConvertLunarToSolar(year-1, 12, 29);  //  <-- 에러 부분임
            if (month == solar.Month && day == solar.Day) return "구정";
            solar = ConvertLunarToSolar(year, 1, 1);
            if (month == solar.Month && day == solar.Day) return "구정";
            solar = ConvertLunarToSolar(year, 1, 2);
            if (month == solar.Month && day == solar.Day) return "구정";
            solar = ConvertLunarToSolar(year, 4, 8);
            if (month == solar.Month && day == solar.Day) return "석가탄신일";
            solar = ConvertLunarToSolar(year, 8, 14);
            if (month == solar.Month && day == solar.Day) return "추석";
            solar = ConvertLunarToSolar(year, 8, 15);
            if (month == solar.Month && day == solar.Day) return "추석";
            solar = ConvertLunarToSolar(year, 8, 16);
            if (month == solar.Month && day == solar.Day) return "추석";

            return dayName;
        }
    }
}
