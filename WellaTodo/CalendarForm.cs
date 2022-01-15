using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        public CalendarForm()
        {
            InitializeComponent();
        }

        public void SetController(MainController controller)
        {
            m_Controller = controller;
        }

        public void Update_View(IModel m, ModelEventArgs e)
        {
            Console.WriteLine("4>CalendarForm::Update_View -> SetDate(m_dtValue)");
            SetDate(m_dtValue);

            CDataCell dc = e.Item;
            WParam param = e.Param;
            switch (param)
            {
                case WParam.WM_COMPLETE_PROCESS:
                    break;
                case WParam.WM_IMPORTANT_PROCESS:
                    break;
                case WParam.WM_MODIFY_TASK_TITLE:
                    break;
                case WParam.WM_MODIFY_TASK_MEMO:
                    break;
                case WParam.WM_TASK_ADD:
                    break;
                case WParam.WM_TASK_DELETE:
                    break;
                case WParam.WM_MODIFY_MYTODAY:
                    break;
                case WParam.WM_MODIFY_REMIND:
                    break;
                case WParam.WM_MODIFY_PLANNED:
                    break;
                case WParam.WM_MODIFY_REPEAT:
                    break;
                default:
                    break;
            }
        }

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
                dayPanel[i].BackColor = Color.White;
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
                    foreach (Control ctr in dayPanel[pos].Controls) ctr.Width = dayPanel[pos].Width;  // TASK 폭 조정
                    pos++;
                }
            }
        }

        private void CalendarForm_Resize(object sender, EventArgs e)
        {
            //Console.WriteLine("panel_Calendar_Resize");
            panel_Calendar.Refresh();
        }

        private void SetDate(DateTime dt)
        {
            //Console.WriteLine("SetDate");
            m_dtValue = dt;
            DateTime startDate = new DateTime(dt.Year, dt.Month, 1, 0, 0, 0);
            int num_DaysInMonth = DateTime.DaysInMonth(dt.Year, dt.Month);
            int num_DayOfWeek = (int)startDate.DayOfWeek;
            DateTime endDate = new DateTime(dt.Year, dt.Month, num_DaysInMonth, 23, 59, 59);

            labelCurrentDate.Text = dt.Year.ToString() + "년 " + dt.Month.ToString() + "월";

            for (int i = 0; i < 42; i++) // eventhandler 제거 및 dayPanel 클리어
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

            // 날짜 표시
            int preDays = (new int[] { 0, 1, 2, 3, 4, 5, 6 })[(int)startDate.DayOfWeek];
            DateTime curDate = startDate.AddDays(-preDays);
            for (int i = 0; i < dayPanel.Length; i++)
            {
                Label label_Day = new Label();
                if (curDate.Day == 1) // 매월 1일 표기
                    label_Day.Text = curDate.Month.ToString() + "/" + curDate.Day.ToString();
                else
                    label_Day.Text = curDate.Day.ToString();
                if (curDate.Month == startDate.Month) // 이전달 & 다음달 배경색
                    dayPanel[i].BackColor = Color.White;
                else
                    dayPanel[i].BackColor = Color.LightGray;
                label_Day.Font = new Font(FONT_NAME, FONT_SIZE_TEXT, FontStyle.Bold);
                label_Day.Height = CALENDAR_DAY_TEXT_HEIGHT;
                if (curDate == DateTime.Today) label_Day.BackColor = Color.Violet; // 오늘은 BLUE
                if ((i % 7) == 0) label_Day.ForeColor = Color.Red;  // 일요일은 RED
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
                    m_TaskToolTip.SetToolTip(label_planned, dc.DC_title);
                    dayPanel[i].Controls.Add(label_planned);
                }
                curDate = curDate.AddDays(1);
            }

            panel_Calendar.Refresh(); // refresh 해야함
        }

        private int Calc_NumOfWeekInMonth(DateTime dt)
        {
            DateTime startDate = new DateTime(dt.Year, dt.Month, 1);
            int num_DaysInMonth = DateTime.DaysInMonth(dt.Year, dt.Month);
            int num_DayOfWeek = (int)startDate.DayOfWeek;

            int result = (num_DaysInMonth + num_DayOfWeek) / 7;
            int mod = (num_DaysInMonth + num_DayOfWeek) % 7;
            return mod == 0 ? result : result + 1;
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

            //taskEditForm.StartPosition = FormStartPosition.Manual;
            //taskEditForm.Location = new Point(Location.X + (Width - taskEditForm.Width) / 2, Location.Y + (Height - taskEditForm.Height) / 2);

            IEnumerable<CDataCell> dataset = m_Controller.Query_Task_Calendar(sd.CD_DataCell);
            CDataCell dc = dataset.First();

            taskEditForm.TE_DataCell = (CDataCell)dc.Clone();

            taskEditForm.ShowDialog();

            dc = taskEditForm.TE_DataCell;

            if (taskEditForm.IsCompleteChanged)
            {
                Console.WriteLine("1>CalendarForm::Calendar_Item_Click -> Complete : " + dc.DC_complete);
                m_Controller.Perform_Complete_Process(dc);
                taskEditForm.IsCompleteChanged = false;
            }

            if (taskEditForm.IsImportantChanged)
            {
                Console.WriteLine("1>CalendarForm::Calendar_Item_Click -> Important : " + dc.DC_important);
                m_Controller.Perform_Important_Process(dc);
                taskEditForm.IsImportantChanged = false;
            }

            if (taskEditForm.IsTitleChanged)
            {
                Console.WriteLine("1>CalendarForm::Calendar_Item_Click -> Title : " + dc.DC_title);
                m_Controller.Perform_Modify_Task_Title(dc);
                taskEditForm.IsTitleChanged = false;
            }

            if (taskEditForm.IsMemoChanged)
            {
                Console.WriteLine("1>CalendarForm::Calendar_Item_Click -> Memo : " + dc.DC_title);
                m_Controller.Perform_Modify_Task_Memo(dc);
                taskEditForm.IsMemoChanged = false;
            }

            if (taskEditForm.IsDeleted) // 목록 삭제
            {
                Console.WriteLine("1>CalendarForm::Calendar_Item_Click -> Task Delete");
                m_Controller.Perform_Delete_Task(dc);
                taskEditForm.IsDeleted = false;
            }
        }
    }
}
