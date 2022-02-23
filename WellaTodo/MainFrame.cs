//
// copyright honeysoft v0.14 -> v0.7 -> v0.8 -> 0.95
//

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;
using System.Drawing.Drawing2D;

namespace WellaTodo
{
    public delegate void TodoItemList_Event(object sender, EventArgs e);

    public partial class MainFrame : Form, IView, IModelObserver
    {
        public event ViewHandler<IView> View_Changed_Event;

        static readonly string WINDOW_CAPTION = "Wella Todo v0.95";
        static readonly int WINDOW_WIDTH = 1200;
        static readonly int WINDOW_HEIGHT = 700;
        static readonly int MENU_WINDOW_WIDTH = 250;
        static readonly int DETAIL_WINDOW_WIDTH = 260;
        static readonly int DETAIL_WINDOW_X1 = 5;
        static readonly int TASK_HEIGHT = 40;
        static readonly int HEADER_HEIGHT = 50;
        static readonly int TAIL_HEIGHT = 50;
        static readonly int TASK_WIDTH_GAP = 25;
        static readonly int MENU_WIDTH_GAP = 15;
        static readonly int TEXTBOX_HEIGHT_GAP = 42;

        static readonly Image ICON_ACCOUNT = Properties.Resources.outline_manage_accounts_black_24dp;
        static readonly Image ICON_SUNNY = Properties.Resources.outline_wb_sunny_black_24dp;
        static readonly Image ICON_GRADE = Properties.Resources.outline_grade_black_24dp;
        static readonly Image ICON_EVENTNOTE = Properties.Resources.outline_event_note_black_24dp;
        static readonly Image ICON_CHECKCIRCLE = Properties.Resources.outline_check_circle_black_24dp;
        static readonly Image ICON_HOME = Properties.Resources.outline_home_black_24dp;
        static readonly Image ICON_LIST = Properties.Resources.outline_list_black_24dp;

        static readonly Color PSEUDO_BACK_COLOR = Color.White;
        static readonly Color PSEUDO_HIGHLIGHT_COLOR = Color.LightCyan;
        static readonly Color PSEUDO_SELECTED_COLOR = Color.Cyan;
        static readonly Color PSEUDO_TEXTBOX_BACK_COLOR = Color.LightCyan;

        static readonly string FONT_NAME = "돋움";
        static readonly float FONT_SIZE_TITLE = 24.0f;
        static readonly float FONT_SIZE_TEXT = 14.0f;

        public enum MenuList 
        { 
            LOGIN_SETTING_MENU      = 1,
            MYTODAY_MENU            = 2,
            IMPORTANT_MENU          = 3,
            DEADLINE_MENU           = 4,
            COMPLETE_MENU           = 5,
            TODO_ITEM_MENU          = 6,
            RESERVED_MENU           = 7,
            LIST_MENU               = 8
        }

        MainController m_Controller;

        List<Todo_Item> m_Task = new List<Todo_Item>();
        ToolTip m_TaskToolTip = new ToolTip();

        LoginSettingForm loginSettingForm = new LoginSettingForm();
        MemoForm memoForm = new MemoForm();
        OutputForm outputForm = new OutputForm();

        RoundCheckbox roundCheckbox1 = new RoundCheckbox();
        StarCheckbox starCheckbox1 = new StarCheckbox();

        RoundLabel roundLabel1 = new RoundLabel();
        RoundLabel roundLabel2 = new RoundLabel();
        RoundLabel roundLabel3 = new RoundLabel();
        RoundLabel roundLabel4 = new RoundLabel();

        Label createDateLabel = new Label();
        RoundLabel upArrow = new RoundLabel();
        RoundLabel downArrow = new RoundLabel();

        Color COLOR_DETAIL_WINDOW_BACK_COLOR = Color.PapayaWhip;

        bool isActivated = false;
        bool isDetailWindowOpen = false;
        bool isTextbox_Task_Clicked = false;
        bool isTextbox_List_Clicked = false;
        bool isTextbox_Momo_Changed = false;

        Todo_Item m_Pre_Selected_Item;
        Todo_Item m_Selected_Item;
        TwoLineList m_Pre_Selected_Menu;
        TwoLineList m_Selected_Menu;
        MenuList enum_Selected_Menu;

        int m_currentPage = 1;
        int m_thumbsPerPage = 20;

        int m_printCounter = 0;
        int m_printPageNo = 1;

        public MainFrame()
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
        private void MainFrame_Load(object sender, EventArgs e)
        {
            Initiate_View();
            Initiate_MenuList();
            Change_ColorTheme();

            timer1.Interval = 3000;
            timer1.Enabled = true;
        }

        private void MainFrame_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("저장할까요?", WINDOW_CAPTION, MessageBoxButtons.YesNoCancel);
            switch (result)
            {
                case DialogResult.Cancel:
                    e.Cancel = true;
                    return;
                case DialogResult.No:
                    break;
                case DialogResult.Yes:
                    m_Controller.Save_Data_File();
                    break;
            }

            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void MainFrame_Paint(object sender, PaintEventArgs e)
        {
            Update_Display();
        }

        private void MainFrame_Resize(object sender, EventArgs e)
        {
            Update_Display();
        }

        private void MainFrame_Activated(object sender, EventArgs e)
        {
            isActivated = true;
            Update_Display();
        }

        private void MainFrame_Deactivate(object sender, EventArgs e)
        {
            isActivated = false;
        }

        //--------------------------------------------------------------
        // 태스트 및 메뉴리스트 초기화 및 Update Display
        //--------------------------------------------------------------
        private void Initiate_View()
        {
            DateTime dt = DateTime.Now;
            Size = new Size(WINDOW_WIDTH, WINDOW_HEIGHT);
            Text = WINDOW_CAPTION + " [" + dt.ToString("yyyy-MM-dd(ddd) tt h:mm") + "]";

            DoubleBuffered = true;

            splitContainer1.SplitterDistance = MENU_WINDOW_WIDTH;
            splitContainer1.Panel1.BackColor = PSEUDO_BACK_COLOR;
            splitContainer1.Panel1MinSize = 200;
            splitContainer1.Panel2.BackColor = PSEUDO_BACK_COLOR;
            splitContainer1.Panel2MinSize = 300;

            labelUserName.Image = ICON_ACCOUNT;
            labelUserName.ImageAlign = ContentAlignment.MiddleLeft;
            labelUserName.Font = new Font(FONT_NAME, FONT_SIZE_TEXT);
            labelUserName.Text = "      사용자 계정 / 셋팅";
            labelUserName.Location = new Point(0, 0);
            labelUserName.Size = new Size(splitContainer1.Panel1.Width, HEADER_HEIGHT);
            labelUserName.BackColor = PSEUDO_BACK_COLOR;

            textBox_AddList.Font = new Font(FONT_NAME, FONT_SIZE_TEXT);
            textBox_AddList.Location = new Point(10, splitContainer1.Panel1.Height - TEXTBOX_HEIGHT_GAP);
            textBox_AddList.Size = new Size(flowLayoutPanel_Menulist.Width - MENU_WIDTH_GAP, 25);
            textBox_AddList.BackColor = PSEUDO_TEXTBOX_BACK_COLOR;
            textBox_AddList.Text = "+ 새 목록 추가";

            splitContainer2.SplitterDistance = splitContainer2.Width;
            splitContainer2.IsSplitterFixed = true;
            splitContainer2.Location = new Point(0, 0);
            splitContainer2.Size = new Size(splitContainer1.Panel2.Width, splitContainer1.Panel2.Height - TAIL_HEIGHT);
            splitContainer2.Panel1.BackColor = PSEUDO_BACK_COLOR;

            panel_Header.Location = new Point(0, 0);
            panel_Header.Size = new Size(splitContainer2.Panel1.Width, HEADER_HEIGHT);
            panel_Header .BackColor = PSEUDO_HIGHLIGHT_COLOR;

            label_ListName.Image = ICON_HOME;
            label_ListName.ImageAlign = ContentAlignment.MiddleLeft;
            label_ListName.Font = new Font(FONT_NAME, FONT_SIZE_TITLE);
            label_ListName.AutoSize = true;
            label_ListName.Location = new Point(10,10); // font size에 따라 위치 설정할 것
            label_ListName.Size = new Size(panel_Header.Width, HEADER_HEIGHT);
            label_ListName.BackColor = PSEUDO_HIGHLIGHT_COLOR;

            // 태스크 항목
            flowLayoutPanel2.AutoScroll = false;
            flowLayoutPanel2.HorizontalScroll.Maximum = 0;
            flowLayoutPanel2.HorizontalScroll.Enabled = false;
            flowLayoutPanel2.HorizontalScroll.Visible = false;
            flowLayoutPanel2.AutoScroll = true;

            flowLayoutPanel2.MouseWheel += new MouseEventHandler(flowLayoutPanel2_MouseWheel);

            flowLayoutPanel2.Location = new Point(0, HEADER_HEIGHT);
            flowLayoutPanel2.Size = new Size(splitContainer2.Panel1.Width, splitContainer2.Panel1.Height - TAIL_HEIGHT);

            textBox_Task.Font = new Font(FONT_NAME, FONT_SIZE_TEXT);
            textBox_Task.Location = new Point(10, splitContainer1.Panel2.Height - TEXTBOX_HEIGHT_GAP);
            textBox_Task.Size = new Size(splitContainer1.Panel2.Width - 25, 25);
            textBox_Task.BackColor = PSEUDO_TEXTBOX_BACK_COLOR;
            textBox_Task.Text = "+ 작업 추가";

            // 상세창
            roundCheckbox1.MouseEnter += new EventHandler(roundCheckbox1_MouseEnter);
            roundCheckbox1.MouseLeave += new EventHandler(roundCheckbox1_MouseLeave);
            roundCheckbox1.MouseClick += new MouseEventHandler(roundCheckbox1_MouseClick);
            roundCheckbox1.Location = new Point(DETAIL_WINDOW_X1 + 2, 5);
            roundCheckbox1.Size = new Size(25, 25);
            splitContainer2.Panel2.Controls.Add(roundCheckbox1);

            textBox_Title.Location = new Point(DETAIL_WINDOW_X1 + 30, 8);
            textBox_Title.Size = new Size(DETAIL_WINDOW_WIDTH - 78, 25);
            textBox_Title.BackColor = PSEUDO_TEXTBOX_BACK_COLOR;

            starCheckbox1.MouseEnter += new EventHandler(starCheckbox1_MouseEnter);
            starCheckbox1.MouseLeave += new EventHandler(starCheckbox1_MouseLeave);
            starCheckbox1.MouseClick += new MouseEventHandler(starCheckbox1_MouseClick);
            starCheckbox1.Location = new Point(DETAIL_WINDOW_X1 + 215, 5);
            starCheckbox1.Size = new Size(25, 25);
            splitContainer2.Panel2.Controls.Add(starCheckbox1);

            roundLabel1.MouseClick += new MouseEventHandler(roundLabel1_Click);
            roundLabel1.MouseEnter += new EventHandler(roundLabel1_MouseEnter);
            roundLabel1.MouseLeave += new EventHandler(roundLabel1_MouseLeave);
            roundLabel1.Text = "나의 하루에 추가";
            roundLabel1.Location = new Point(DETAIL_WINDOW_X1 + 15, 40);
            roundLabel1.Size = new Size(DETAIL_WINDOW_WIDTH - 45, 30);
            splitContainer2.Panel2.Controls.Add(roundLabel1);

            roundLabel2.MouseClick += new MouseEventHandler(roundLabel2_Click);
            roundLabel2.MouseEnter += new EventHandler(roundLabel2_MouseEnter);
            roundLabel2.MouseLeave += new EventHandler(roundLabel2_MouseLeave);
            roundLabel2.Text = "미리 알림";
            roundLabel2.Location = new Point(DETAIL_WINDOW_X1 + 15, 75);
            roundLabel2.Size = new Size(DETAIL_WINDOW_WIDTH - 45, 30);
            splitContainer2.Panel2.Controls.Add(roundLabel2);

            roundLabel3.MouseClick += new MouseEventHandler(roundLabel3_Click);
            roundLabel3.MouseEnter += new EventHandler(roundLabel3_MouseEnter);
            roundLabel3.MouseLeave += new EventHandler(roundLabel3_MouseLeave);
            roundLabel3.Text = "기한 설정";
            roundLabel3.Location = new Point(DETAIL_WINDOW_X1 + 15, 110);
            roundLabel3.Size = new Size(DETAIL_WINDOW_WIDTH - 45, 30);
            splitContainer2.Panel2.Controls.Add(roundLabel3);

            roundLabel4.MouseClick += new MouseEventHandler(roundLabel4_Click);
            roundLabel4.MouseEnter += new EventHandler(roundLabel4_MouseEnter);
            roundLabel4.MouseLeave += new EventHandler(roundLabel4_MouseLeave);
            roundLabel4.Text = "반복";
            roundLabel4.Location = new Point(DETAIL_WINDOW_X1 + 15, 145);
            roundLabel4.Size = new Size(DETAIL_WINDOW_WIDTH - 45, 30);
            splitContainer2.Panel2.Controls.Add(roundLabel4);

            textBox_Memo.Multiline = true;
            textBox_Memo.Location = new Point(DETAIL_WINDOW_X1 + 5, 185);
            textBox_Memo.Size = new Size(DETAIL_WINDOW_WIDTH - 25, 130);

            createDateLabel.Text = " 생성됨";
            createDateLabel.Location = new Point(DETAIL_WINDOW_X1 + 10, 325);
            createDateLabel.Size = new Size(100, 50);
            splitContainer2.Panel2.Controls.Add(createDateLabel);

            upArrow.Click += new EventHandler(upArrow_Click);
            upArrow.MouseEnter += new EventHandler(upArrow_MouseEnter);
            upArrow.MouseLeave += new EventHandler(upArrow_MouseLeave);
            upArrow.Text = "위로";
            upArrow.Location = new Point(DETAIL_WINDOW_X1 + 115, 320);
            upArrow.Size = new Size(60, 30);
            splitContainer2.Panel2.Controls.Add(upArrow);

            downArrow.Click += new EventHandler(downArrow_Click);
            downArrow.MouseEnter += new EventHandler(downArrow_MouseEnter);
            downArrow.MouseLeave += new EventHandler(downArrow_MouseLeave);
            downArrow.Text = "아래로";
            downArrow.Location = new Point(DETAIL_WINDOW_X1 + 180, 320);
            downArrow.Size = new Size(60, 30);
            splitContainer2.Panel2.Controls.Add(downArrow);

            // 닫기 버튼
            button1.Location = new Point(DETAIL_WINDOW_X1 + 15, 360);
            button1.Size = new Size(75, 25);

            // 삭제 버튼
            button2.Location = new Point(DETAIL_WINDOW_X1 + 160, 360);
            button2.Size = new Size(75, 25);

            Send_Log_Message(">MainFrame::Initiate_View");
        }

        private void Initiate_MenuList()
        {
            TwoLineList divider1 = new TwoLineList();
            TwoLineList twolinelist_Menu1 = new TwoLineList(new Bitmap(ICON_SUNNY), "오늘 할 일", "", "");
            TwoLineList twolinelist_Menu2 = new TwoLineList(new Bitmap(ICON_GRADE), "중요", "", "");
            TwoLineList twolinelist_Menu3 = new TwoLineList(new Bitmap(ICON_EVENTNOTE), "계획된 일정", "", "");
            TwoLineList twolinelist_Menu4 = new TwoLineList(new Bitmap(ICON_CHECKCIRCLE), "완료됨", "", "");
            TwoLineList twolinelist_Menu5 = new TwoLineList(new Bitmap(ICON_HOME), "작업", "", "");
            TwoLineList divider2 = new TwoLineList();
            twolinelist_Menu1.TwoLineList_Click += new TwoLineList_Event(TwoLineList_Click);
            twolinelist_Menu2.TwoLineList_Click += new TwoLineList_Event(TwoLineList_Click);
            twolinelist_Menu3.TwoLineList_Click += new TwoLineList_Event(TwoLineList_Click);
            twolinelist_Menu4.TwoLineList_Click += new TwoLineList_Event(TwoLineList_Click);
            twolinelist_Menu5.TwoLineList_Click += new TwoLineList_Event(TwoLineList_Click);
            twolinelist_Menu5.DragEnter += new DragEventHandler(TwoLineList_DragEnter);
            twolinelist_Menu5.DragDrop += new DragEventHandler(TwoLineList_DragDrop);

            flowLayoutPanel_Menulist.AutoScroll = false;
            flowLayoutPanel_Menulist.HorizontalScroll.Maximum = 0;
            flowLayoutPanel_Menulist.HorizontalScroll.Enabled = false;
            flowLayoutPanel_Menulist.HorizontalScroll.Visible = false;
            flowLayoutPanel_Menulist.AutoScroll = true;

            flowLayoutPanel_Menulist.BackColor = PSEUDO_BACK_COLOR;
            flowLayoutPanel_Menulist.Margin = new Padding(0);
            flowLayoutPanel_Menulist.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel_Menulist.WrapContents = false;
            flowLayoutPanel_Menulist.Width = splitContainer1.SplitterDistance;
            flowLayoutPanel_Menulist.Location = new Point(labelUserName.Location.X, labelUserName.Height);
            flowLayoutPanel_Menulist.Size = new Size(splitContainer1.SplitterDistance, splitContainer1.Panel1.Height - labelUserName.Height - TAIL_HEIGHT);

            flowLayoutPanel_Menulist.Controls.Add(divider1);
            flowLayoutPanel_Menulist.Controls.Add(twolinelist_Menu1);
            flowLayoutPanel_Menulist.Controls.Add(twolinelist_Menu2);
            flowLayoutPanel_Menulist.Controls.Add(twolinelist_Menu3);
            flowLayoutPanel_Menulist.Controls.Add(twolinelist_Menu4);
            flowLayoutPanel_Menulist.Controls.Add(twolinelist_Menu5);
            flowLayoutPanel_Menulist.Controls.Add(divider2);

            foreach (string list_name in m_Controller.Query_ListName()) // 목록 리스트를 등록한다
            {
                if (list_name != "작업")
                {
                    TwoLineList list;
                    list = new TwoLineList(new Bitmap(ICON_LIST), list_name, "", "");
                    list.TwoLineList_Click -= new TwoLineList_Event(TwoLineList_Click);
                    list.TwoLineList_Click += new TwoLineList_Event(TwoLineList_Click);
                    list.DragEnter -= new DragEventHandler(TwoLineList_DragEnter);
                    list.DragEnter += new DragEventHandler(TwoLineList_DragEnter);
                    list.DragDrop -= new DragEventHandler(TwoLineList_DragDrop);
                    list.DragDrop += new DragEventHandler(TwoLineList_DragDrop);

                    flowLayoutPanel_Menulist.Controls.Add(list);  // 판넬에 목록 저장
                }
            }

            foreach (TwoLineList item in flowLayoutPanel_Menulist.Controls) // 폭 조절
            {
                item.Width = flowLayoutPanel_Menulist.VerticalScroll.Visible
                ? flowLayoutPanel_Menulist.Width - 2 - SystemInformation.VerticalScrollBarWidth
                : flowLayoutPanel_Menulist.Width - 2;
                item.IsSelected = false;
            }

            m_Selected_Menu = twolinelist_Menu5; // 최초로 "작업"이 선택됨
            m_Pre_Selected_Menu = m_Selected_Menu;
            m_Selected_Menu.IsSelected = true;
            enum_Selected_Menu = MenuList.TODO_ITEM_MENU;

            Menu_Task(); // 작업이 초기 출력됨

            Update_Task_Width();
            Update_Menu_Metadata();

            Send_Log_Message(">MainFrame::Initiate_MenuList");
        }

        private void Update_Display()
        {
            //Rectangle rc = ClientRectangle;
            //Console.WriteLine(">ClientRectangle W[{0}] H[{1}]", rc.Width, rc.Height);
            //Console.WriteLine("splitContainer1.SplitterDistance [{0}]", splitContainer1.SplitterDistance);
            //Console.WriteLine("splitContainer1.Size W[{0}] H[{1}]", splitContainer1.Width, splitContainer1.Height);
            //Console.WriteLine("splitContainer1.Panel1.Width [{0}]", splitContainer1.Panel1.Width);
            //Console.WriteLine("splitContainer1.Panel2.Width [{0}]", splitContainer1.Panel2.Width);
            /*
            if (WindowState == FormWindowState.Maximized)
            {
                splitContainer1.SplitterDistance = MENU_WINDOW_WIDTH;
            }

            if (WindowState == FormWindowState.Normal)
            {
                splitContainer1.SplitterDistance = MENU_WINDOW_WIDTH;
            }
            */
            flowLayoutPanel_Menulist.Width = splitContainer1.SplitterDistance;
            flowLayoutPanel_Menulist.Height = splitContainer1.Panel1.Height - labelUserName.Height - TAIL_HEIGHT;
            //Console.WriteLine("flowLayoutPanel_Menulist.Width [{0}]", flowLayoutPanel_Menulist.Width);

            labelUserName.Width = flowLayoutPanel_Menulist.Width;

            textBox_AddList.Location = new Point(10, splitContainer1.Panel1.Height - TEXTBOX_HEIGHT_GAP);
            textBox_AddList.Size = new Size(flowLayoutPanel_Menulist.Width - MENU_WIDTH_GAP, 25);

            foreach (TwoLineList item in flowLayoutPanel_Menulist.Controls)
            {
                item.Width = flowLayoutPanel_Menulist.VerticalScroll.Visible
                ? flowLayoutPanel_Menulist.Width - 2 - SystemInformation.VerticalScrollBarWidth
                : flowLayoutPanel_Menulist.Width - 2;
            }

            splitContainer2.Size = new Size(splitContainer1.Panel2.Width, splitContainer1.Panel2.Height - TAIL_HEIGHT);

            //Console.WriteLine("splitContainer2.SplitterDistance [{0}]", splitContainer2.SplitterDistance);
            //Console.WriteLine("splitContainer2.Size W[{0}] H[{1}]", splitContainer2.Width, splitContainer2.Height);
            //Console.WriteLine("splitContainer2.Panel1.Width [{0}]", splitContainer2.Panel1.Width);
            //Console.WriteLine("splitContainer2.Panel2.Width [{0}]", splitContainer2.Panel2.Width);
            //Console.WriteLine("flowLayoutPanel2.Width [{0}]", flowLayoutPanel2.Width);

            if (isDetailWindowOpen)
            {
                int width = splitContainer2.Width - DETAIL_WINDOW_WIDTH;
                splitContainer2.SplitterDistance = width < 0 ? 0 : width;
            }

            panel_Header.Size = new Size(splitContainer2.Panel1.Width, HEADER_HEIGHT);
            flowLayoutPanel2.Size = new Size(splitContainer2.Panel1.Width, splitContainer2.Panel1.Height - TAIL_HEIGHT);
            textBox_Task.Location = new Point(10, splitContainer1.Panel2.Height - TEXTBOX_HEIGHT_GAP);
            textBox_Task.Size = new Size(splitContainer1.Panel2.Width - 20, 25);

            Update_Task_Width();
        }

        // ============================================
        // 할일 항목 처리 (추가/삭제/메모편집)
        // ============================================
        private void Task_Add(string text)
        {
            CDataCell dc = new CDataCell();
            DateTime dt = DateTime.Now;

            switch (enum_Selected_Menu)
            {
                case MenuList.MYTODAY_MENU:     // 오늘 할 일 메뉴에서 입력됨
                    dc.DC_listName = "작업";
                    dc.DC_title = text;
                    dc.DC_myToday = true;
                    dt = dt.AddDays(1);
                    dc.DC_myTodayTime = new DateTime(dt.Year, dt.Month, dt.Day, 00, 00, 00);
                    Send_Log_Message("1>MainFrame::Task_Add -> Add Task in MyToday Menu : " + dc.DC_title);
                    m_Controller.Perform_Add_Task(dc);
                    break;
                case MenuList.IMPORTANT_MENU:     // 중요 메뉴에서 입력됨
                    dc.DC_listName = "작업";
                    dc.DC_title = text;
                    dc.DC_important = true;
                    Send_Log_Message("1>MainFrame::Task_Add -> Add Task in Important Menu : " + dc.DC_title);
                    m_Controller.Perform_Add_Task(dc);
                    break;
                case MenuList.DEADLINE_MENU:     // 계획된 일정 메뉴에서 입력됨
                    dc.DC_listName = "작업";
                    dc.DC_title = text;
                    dc.DC_deadlineType = 1;
                    dt = dt.AddDays(1);
                    dc.DC_deadlineTime = new DateTime(dt.Year, dt.Month, dt.Day, 00, 00, 00);
                    Send_Log_Message("1>MainFrame::Task_Add -> Add Task in Planned Menu : " + dc.DC_title);
                    m_Controller.Perform_Add_Task(dc);
                    break;
                case MenuList.COMPLETE_MENU:     // 완료됨 메뉴에서 입력됨 -> 할일 추가 불가함
                    Send_Log_Message("1>MainFrame::Task_Add -> Add Task in Complete Menu -> Can't Add Task!");
                    break;
                default:
                    dc.DC_listName = m_Selected_Menu.PrimaryText;
                    dc.DC_title = text;
                    Send_Log_Message("1>MainFrame::Task_Add -> Add Task in Task or List Menu : " + dc.DC_title);
                    m_Controller.Perform_Add_Task(dc);
                    break;
            }
        }

        private void Task_Delete(CDataCell dc)
        {
            string txt = "항목 삭제? [" + dc.DC_title + "]";
            if (MessageBox.Show(txt, WINDOW_CAPTION, MessageBoxButtons.YesNo) == DialogResult.No) return;

            Send_Log_Message("1>MainFrame::Task_Delete : " + dc.DC_title);
            m_Controller.Perform_Delete_Task(dc);
        }

        private void Edit_Task_Memo()
        {
            memoForm.StartPosition = FormStartPosition.CenterParent;

            memoForm.TextBoxString = textBox_Memo.Text;
            memoForm.Text = m_Selected_Item.TD_DataCell.DC_title;
            memoForm.ShowDialog();

            textBox_Memo.Text = memoForm.TextBoxString;
            textBox_Memo.SelectionStart = textBox_Memo.Text.Length;

            //메모 내용에 변경이 있는지 확인(?)
            m_Selected_Item.TD_DataCell.DC_memo = textBox_Memo.Text;  // 입력 사항에 오류가 있는지 체크할 것

            Send_Log_Message("1>MainFrame::Edit_Task_Memo : " + m_Selected_Item.TD_DataCell.DC_title);
            m_Controller.Perform_Modify_Task_Memo(m_Selected_Item.TD_DataCell);
        }

        // ============================================
        // 목록 항목 처리 (추가/목록이름변경하기)
        // ============================================
        private void Add_List(string txt)
        {
            // 동일 이름의 목록 찾기 -> 발견시 뒷자리 번호 부여
            if (!AddList_Check_ListName(txt))
            {
                MessageBox.Show("목록명이 잘못되었거나 중복된 목록명입니다", WINDOW_CAPTION);
                return;
            }

            Send_Log_Message("1>MainFrame::Add_List -> Add New List Menu : " + txt);
            m_Controller.Perform_Menulist_Add(txt);
        }

        private bool AddList_Check_ListName(string txt)
        {
            if (txt == "오늘 할 일" || txt == "중요" || txt == "계획된 일정" || txt == "완료됨" || txt == "작업")
            {
                Send_Log_Message("Warning>MainFrame::AddList_Check_ListName -> Can't Add MenuList for Reserved Menu!!");
                return false;
            }

            foreach (TwoLineList item in flowLayoutPanel_Menulist.Controls)
            {
                if (item.PrimaryText == txt)
                {
                    Send_Log_Message("Warning>MainFrame::AddList_Check_ListName -> Can't Add MenuList for Same menu name exist!!");
                    return false;
                }
            }

            return true;
        }

        private void Menulist_Rename_Process(object sender, MouseEventArgs e)
        {
            TwoLineList sd = (TwoLineList)sender;

            string source = sd.PrimaryText;
            string target = sd.PrimaryText_Renamed;

            Send_Log_Message("1-3>MainFrame::Menulist_Rename_Process -> Rename from " + source + " to " + target);

            m_Controller.Perform_Menulist_Rename(source, target);
        }

        //--------------------------------------------------------------
        // 할일 항목 폭 맞추기 등 필요한 메서드
        //--------------------------------------------------------------
        private void Update_Task_Width()
        {
            panel_Header.Width = splitContainer2.Panel1.Width;
            foreach (Todo_Item item in flowLayoutPanel2.Controls)
            {
                item.Width = flowLayoutPanel2.Width - TASK_WIDTH_GAP;
                //item.Width = flowLayoutPanel2.VerticalScroll.Visible
                //    ? flowLayoutPanel2.Width - 8 - SystemInformation.VerticalScrollBarWidth
                //    : flowLayoutPanel2.Width - 8;
            }
        }

        private int Calc_SplitterDistance()
        {
            //Send_Log_Message("Form size (WxH) :" + Size.Width +"x" + Size.Height);
            //Send_Log_Message("SplitterDistance-1 :" + splitContainer1.SplitterDistance);
            //Send_Log_Message("SplitterDistance-2 :" + splitContainer2.SplitterDistance);

            int distance;
            if (Size.Width > 800)
            {
                distance = MENU_WINDOW_WIDTH;
            } else
            {
                distance = (int)(MENU_WINDOW_WIDTH * 0.75);
            }
            return distance;
        }

        private void Change_ColorTheme()
        {
            switch (loginSettingForm.ColorTheme)
            {
                case 1: // papayaWhip
                    COLOR_DETAIL_WINDOW_BACK_COLOR = Color.PapayaWhip;

                    break;
                case 2: // white
                    COLOR_DETAIL_WINDOW_BACK_COLOR = Color.White;
                    break;
            }
            // 상세창 컬러 셋팅
            splitContainer2.Panel2.BackColor = COLOR_DETAIL_WINDOW_BACK_COLOR;
            roundCheckbox1.BackColor = COLOR_DETAIL_WINDOW_BACK_COLOR;
            starCheckbox1.BackColor = COLOR_DETAIL_WINDOW_BACK_COLOR;
            roundLabel1.BackColor = COLOR_DETAIL_WINDOW_BACK_COLOR;
            roundLabel2.BackColor = COLOR_DETAIL_WINDOW_BACK_COLOR;
            roundLabel3.BackColor = COLOR_DETAIL_WINDOW_BACK_COLOR;
            roundLabel4.BackColor = COLOR_DETAIL_WINDOW_BACK_COLOR;
            createDateLabel.BackColor = COLOR_DETAIL_WINDOW_BACK_COLOR;
            upArrow.BackColor = COLOR_DETAIL_WINDOW_BACK_COLOR;
            downArrow.BackColor = COLOR_DETAIL_WINDOW_BACK_COLOR;
        }

        
        private void Open_DetailWindow()
        {
            splitContainer2.SplitterDistance = splitContainer2.Width - DETAIL_WINDOW_WIDTH;
            flowLayoutPanel2.Width = splitContainer2.Width - DETAIL_WINDOW_WIDTH;
            label_ListName.Width = splitContainer2.Width - DETAIL_WINDOW_WIDTH;
            isDetailWindowOpen = true;
        }

        private void Close_DetailWindow()
        {
            splitContainer2.SplitterDistance = splitContainer2.Width;
            flowLayoutPanel2.Width = splitContainer2.Width;
            label_ListName.Width = splitContainer2.Width;
            isDetailWindowOpen = false;
        }

        private void Update_Menu_Metadata()
        {
            int cnt;
            foreach (TwoLineList item in flowLayoutPanel_Menulist.Controls)
            {
                switch (item.PrimaryText)
                {
                    case "오늘 할 일":
                        cnt = m_Controller.Query_MyToday().Count();
                        break;
                    case "중요":
                        cnt = m_Controller.Query_Important().Count();
                        break;
                    case "계획된 일정":
                        cnt = m_Controller.Query_Planned().Count();
                        break;
                    case "완료됨":
                        cnt = m_Controller.Query_Complete().Count();
                        break;
                    case "작업":
                        cnt = m_Controller.Query_Task("작업").Count();
                        break;
                    default:
                        cnt = m_Controller.Query_Task(item.PrimaryText).Count();
                        break;
                }

                item.MetadataText = cnt.ToString();
            }
        }

        private void Update_Task_Infomation(CDataCell dc)
        {
            foreach (Todo_Item item in flowLayoutPanel2.Controls)  // dc로 td 찾기
            {
                if (dc.DC_task_ID == item.TD_DataCell.DC_task_ID)
                {
                    item.TD_infomation = MakeInfoTextFromDataCell(dc);
                    item.Refresh();
                    break;
                }
            }
        }

        private string MakeInfoTextFromDataCell(CDataCell dc)
        {
            string infoText = "";

            switch (enum_Selected_Menu)
            {
                case MenuList.MYTODAY_MENU:     // 오늘 할 일 메뉴에서 표시됨
                    infoText += "<" + dc.DC_listName + "> ";
                    break;
                case MenuList.IMPORTANT_MENU:     // 중요 메뉴에서 표시됨
                    infoText += "<" + dc.DC_listName + "> ";
                    break;
                case MenuList.DEADLINE_MENU:     // 계획된 일정 메뉴에서 표시됨
                    infoText += "<" + dc.DC_listName + "> ";
                    break;
                case MenuList.COMPLETE_MENU:     // 완료됨 메뉴에서 표시됨
                    infoText += "<" + dc.DC_listName + "> ";
                    break;
                case MenuList.LOGIN_SETTING_MENU:
                    break;
                case MenuList.TODO_ITEM_MENU:
                    break;
                case MenuList.RESERVED_MENU:
                    break;
                case MenuList.LIST_MENU:
                    break;
                default:
                    break;
            }

            if (dc.DC_myToday) infoText += " [오늘 할일]";

            if (dc.DC_remindType > 0) infoText += " [알람]" + dc.DC_remindTime.ToString("MM/dd(ddd)tthh:mm");

            if (dc.DC_deadlineType > 0) infoText += " [기한]" + dc.DC_deadlineTime.ToString("MM/dd(ddd)tthh:mm");

            switch (dc.DC_repeatType)
            {
                case 1:
                    infoText += " [매일]" + dc.DC_repeatTime.ToString("MM/dd(ddd)tthh:mm");
                    break;
                case 2:
                    infoText += " [평일]" + dc.DC_repeatTime.ToString("MM/dd(ddd)tthh:mm");
                    break;
                case 3:
                    infoText += " [매주]" + dc.DC_repeatTime.ToString("MM/dd(ddd)tthh:mm");
                    break;
                case 4:
                    infoText += " [매월]" + dc.DC_repeatTime.ToString("MM/dd(ddd)tthh:mm");
                    break;
                case 5:
                    infoText += " [매년]" + dc.DC_repeatTime.ToString("yyyy/MM/dd(ddd)tthh:mm");
                    break;
                default:
                    break;
            }

            return infoText;
        }

        private void SendDataCellToTodoItem(CDataCell dc)
        {
            if (dc == null)  // Save, Load, Open, Print 명령은 dc가 null 임
            {
                return;
            }

            // 화면에 dc가 있는지 확인후 item.TD_DataCell에 복사한다
            foreach (Todo_Item item in flowLayoutPanel2.Controls)  // dc로 td 찾기
            {
                if (dc.DC_task_ID == item.TD_DataCell.DC_task_ID)
                {
                    item.TD_DataCell = (CDataCell)dc.Clone();
                    break;
                }
            }
        }

        private void SendDataCellToDetailWindow(CDataCell dc)
        {
            textBox_Title.Text = dc.DC_title;
            roundCheckbox1.Checked = dc.DC_complete;
            starCheckbox1.Checked = dc.DC_important;
            textBox_Memo.Text = dc.DC_memo;
            textBox_Memo.SelectionStart = textBox_Memo.Text.Length;

            isTextbox_Momo_Changed = false;

            if (dc.DC_myToday)
            {
                roundLabel1.Text = "나의 하루에 추가됨";
                roundLabel1.BackColor = PSEUDO_SELECTED_COLOR;

            }
            else
            {
                roundLabel1.Text = "나의 하루에 추가";
                roundLabel1.BackColor = COLOR_DETAIL_WINDOW_BACK_COLOR;
            }

            if (dc.DC_remindType > 0)
            {
                roundLabel2.Text = "알림 설정됨";
                roundLabel2.BackColor = PSEUDO_SELECTED_COLOR;
            }
            else
            {
                roundLabel2.Text = "미리 알림";
                roundLabel2.BackColor = COLOR_DETAIL_WINDOW_BACK_COLOR;
            }

            if (dc.DC_deadlineType > 0)
            {
                roundLabel3.Text = "기한 설정됨";
                roundLabel3.BackColor = PSEUDO_SELECTED_COLOR;
            }
            else
            {
                roundLabel3.Text = "기한 설정";
                roundLabel3.BackColor = COLOR_DETAIL_WINDOW_BACK_COLOR;
            }

            if (dc.DC_repeatType > 0)
            {
                roundLabel4.Text = "반복 설정됨";
                roundLabel4.BackColor = PSEUDO_SELECTED_COLOR;
            }
            else
            {
                roundLabel4.Text = "반복";
                roundLabel4.BackColor = COLOR_DETAIL_WINDOW_BACK_COLOR;
            }

            createDateLabel.Text = dc.DC_dateCreated.ToString("yyyy-MM-dd(ddd)\r\n") + "생성됨[" + dc.DC_task_ID + "]";
        }

        // ------------------------------------------------------------------
        // 알람 체크  -> controller로 이동할 것
        // ------------------------------------------------------------------
        private void AlarmCheck()
        {
            /*
            bool alarm = false;
            int result;
            string txt;
            DateTime dt = DateTime.Now;
            DateTime tt;

            IEnumerable<CDataCell> dataset = m_Controller.Query_All_Task();
            int pos = 0;
            foreach (CDataCell data in dataset)
            {
                // 오늘할 일 체크
                if (data.DC_myToday) //if (!data.DC_complete && data.DC_myToday)
                {
                    tt = data.DC_myTodayTime;
                    result = DateTime.Compare(dt, tt);
                    
                    if (result > 0) // 날짜 지남
                    {
                        AlarmForm alarmForm = new AlarmForm();
                        alarmForm.StartPosition = FormStartPosition.Manual;
                        alarmForm.Location = new Point(Location.X + (Width - alarmForm.Width) / 2, Location.Y + (Height - alarmForm.Height) / 2);
                        alarmForm.Show();

                        txt = "오늘 할 일 싯점 도래 <" + data.DC_listName + ">  [" + pos.ToString() + "] " + data.DC_title + "\r\n";
                        alarmForm.TextBoxString = txt;
                        txt = tt.ToString("yyyy-MM-dd HH:mm:ss") + "- TARGET" + "\r\n";
                        alarmForm.TextBoxString = txt;
                        txt = dt.ToString("yyyy-MM-dd HH:mm:ss") + "- Now" + "\r\n";
                        alarmForm.TextBoxString = txt;

                        data.DC_myToday = false;
                        data.DC_myTodayTime = default;
                        Update_Task_Infomation(data);
                        alarm = true;
                    }
                }

                // 미리 알림 체크
                if (data.DC_remindType > 0)
                {
                    tt = data.DC_remindTime;
                    result = DateTime.Compare(dt, tt);

                    if (result > 0) // 날짜 지남
                    {
                        AlarmForm alarmForm = new AlarmForm();
                        alarmForm.StartPosition = FormStartPosition.Manual;
                        alarmForm.Location = new Point(Location.X + (Width - alarmForm.Width) / 2, Location.Y + (Height - alarmForm.Height) / 2);
                        alarmForm.Show();

                        txt = "미리 알림 싯점 도래 <" + data.DC_listName + ">  [" + pos.ToString() + "] " + data.DC_title + "\r\n";
                        alarmForm.TextBoxString = txt;
                        txt = tt.ToString("yyyy-MM-dd HH:mm:ss") + "- TARGET" + "\r\n";
                        alarmForm.TextBoxString = txt;
                        txt = dt.ToString("yyyy-MM-dd HH:mm:ss") + "- Now" + "\r\n";
                        alarmForm.TextBoxString = txt;

                        data.DC_remindType = 0;
                        data.DC_remindTime = default;
                        Update_Task_Infomation(data);
                        alarm = true;
                    }
                }

                // 기한 설정 체크
                if (data.DC_deadlineType > 0)  // if (data.DC_deadlineType > 0)
                {
                    tt = data.DC_deadlineTime;
                    result = DateTime.Compare(dt, tt);

                    if (result > 0) // 날짜 지남
                    {
                        if (data.DC_deadlineType != 5)
                        {
                            AlarmForm alarmForm = new AlarmForm();
                            alarmForm.StartPosition = FormStartPosition.Manual;
                            alarmForm.Location = new Point(Location.X + (Width - alarmForm.Width) / 2, Location.Y + (Height - alarmForm.Height) / 2);
                            alarmForm.Show();

                            txt = "기한 설정 싯점 도래 <" + data.DC_listName + ">  [" + pos.ToString() + "] " + data.DC_title + "\r\n";
                            alarmForm.TextBoxString = txt;
                            txt = tt.ToString("yyyy-MM-dd HH:mm:ss") + "- TARGET" + "\r\n";
                            alarmForm.TextBoxString = txt;
                            txt = dt.ToString("yyyy-MM-dd HH:mm:ss") + "- Now" + "\r\n";
                            alarmForm.TextBoxString = txt;

                            data.DC_deadlineType = 5; // 알람처리 완료
                            //data.DC_deadlineTime = default;
                            Update_Task_Infomation(data);
                            alarm = true;
                        }
                    }
                }

                // 반복 체크
                if (data.DC_repeatType > 0)
                {
                    tt = data.DC_repeatTime;
                    result = DateTime.Compare(dt, tt);

                    if (result > 0) // 날짜 지남
                    {
                        AlarmForm alarmForm = new AlarmForm();
                        alarmForm.StartPosition = FormStartPosition.Manual;
                        alarmForm.Location = new Point(Location.X + (Width - alarmForm.Width) / 2, Location.Y + (Height - alarmForm.Height) / 2);
                        alarmForm.Show();

                        txt = "반복 싯점 도래 <" + data.DC_listName + ">  [" + pos.ToString() + "] " + data.DC_title + "\r\n";
                        alarmForm.TextBoxString = txt;
                        txt = tt.ToString("yyyy-MM-dd HH:mm:ss") + "- TARGET" + "\r\n";
                        alarmForm.TextBoxString = txt;
                        txt = dt.ToString("yyyy-MM-dd HH:mm:ss") + "- Now" + "\r\n";
                        alarmForm.TextBoxString = txt;

                        switch (data.DC_repeatType) // 반복 재설정
                        {
                            case 1:
                                Repeat_EveryDay(data);
                                break;
                            case 2:
                                Repeat_WorkingDay(data);
                                break;
                            case 3:
                                Repeat_EveryWeek(data);
                                break;
                            case 4:
                                Repeat_EveryMonth(data);
                                break;
                            case 5:
                                Repeat_EveryYear(data);
                                break;
                        }
                        alarm = true;
                    }
                }
                pos++;
            }

            if (!alarm) return;

            foreach (TwoLineList item in flowLayoutPanel_Menulist.Controls)
            {
                if (item.IsSelected)
                {
                    switch (item.PrimaryText)
                    {
                        case "오늘 할 일":
                            enum_Selected_Menu = MenuList.MYTODAY_MENU;
                            Menu_MyToday();
                            break;
                        case "중요":
                            enum_Selected_Menu = MenuList.IMPORTANT_MENU;
                            Menu_Important();
                            break;
                        case "계획된 일정":
                            enum_Selected_Menu = MenuList.DEADLINE_MENU;
                            Menu_Planned();
                            break;
                        case "완료됨":
                            enum_Selected_Menu = MenuList.COMPLETE_MENU;
                            Menu_Completed();
                            break;
                        case "작업":
                            enum_Selected_Menu = MenuList.TODO_ITEM_MENU;
                            Menu_Task();
                            break;
                        default:
                            enum_Selected_Menu = MenuList.LIST_MENU;
                            Menu_List(item);
                            break;
                    }
                }
            }
            Update_Task_Width();
            Update_Menu_Metadata();
            */
        }

        private void Display_Data()
        {
            int pos;
            string txt;

            if (outputForm.Visible)
            {
                pos = 0;
                foreach (CDataCell task in m_Controller.Query_All_Task())
                {
                    txt = ">myTaskItems : [" + task.DC_task_ID + "] " + task.DC_listName + "--" + task.DC_title + "\r\n";
                    outputForm.TextBoxString = txt;
                    pos++;
                }

                foreach (string list in m_Controller.Query_ListName())
                {
                    txt = ">myListNames : " + list + "\r\n";
                    outputForm.TextBoxString = txt;
                }
            }
        }

        //--------------------------------------------------------------
        // Model 이벤트
        //--------------------------------------------------------------
        public void ModelObserver_Event_method(IModel m, ModelEventArgs e)
        {
            //Console.WriteLine(">MainFrame::ModelObserver_Event_method");
            // Model에서 온 데이타로 View를 업데이트
        }

        public void Update_View(IModel m, ModelEventArgs e)
        {
            CDataCell dc = e.Item;
            WParam param = e.Param;

            SendDataCellToTodoItem(dc); // 변경된 내용을 현재 화면 아이템의 DataCell 내용을 변경한다

            switch (param)
            {
                case WParam.WM_LOAD_DATA:
                    Update_Load_Data();
                    break;
                case WParam.WM_SAVE_DATA:
                    Update_Save_Data();
                    break;
                case WParam.WM_OPEN_DATA:
                    Update_Open_Data();
                    break;
                case WParam.WM_PRINT_DATA:
                    Update_Print_Data();
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
                    Update_Modify_Task_Memo(dc);
                    break;
                case WParam.WM_TASK_ADD:
                    Update_Add_Task(dc);
                    break;
                case WParam.WM_TASK_DELETE:
                    Update_Delete_Task(dc);
                    break;
                case WParam.WM_TASK_MOVE_TO:
                    Update_Task_Move_To(dc);
                    break;
                case WParam.WM_TASK_MOVE_UP:
                    Update_Task_Move_Up(dc);
                    break;
                case WParam.WM_TASK_MOVE_DOWN:
                    Update_Task_Move_Down(dc);
                    break;
                case WParam.WM_MODIFY_MYTODAY:
                    Update_Modify_MyToday(dc);
                    break;
                case WParam.WM_MODIFY_REMIND:
                    Update_Modify_Remind(dc);
                    break;
                case WParam.WM_MODIFY_PLANNED:
                    Update_Modify_Planned(dc);
                    break;
                case WParam.WM_MODIFY_REPEAT:
                    Update_Modify_Repeat(dc);
                    break;
                case WParam.WM_MENULIST_ADD:
                    Update_Menulist_Add(dc);
                    break;
                case WParam.WM_MENULIST_RENAME:
                    Update_Menulist_Rename(dc);
                    break;
                case WParam.WM_MENULIST_DELETE:
                    Update_Menulist_Delete(dc);
                    break;
                case WParam.WM_MENULIST_UP:
                    Update_Menulist_Up(dc);
                    break;
                case WParam.WM_MENULIST_DOWN:
                    Update_Menulist_Down(dc);
                    break;
                case WParam.WM_TRANSFER_TASK:
                    Update_Transfer_Task(dc);
                    break;
                case WParam.WM_PLAN_ADD:
                    Update_Add_Task(dc);
                    break;
                case WParam.WM_MEMO_ADD:
                    Update_Add_Task(dc);
                    break;
                case WParam.WM_MEMO_MOVE_TO:
                    Update_Memo_Move_To(dc);
                    break;
                case WParam.WM_CONVERT_NOTEPAD:
                    Update_Convert_NotePad(dc);
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
                MessageBox.Show("Send_Log_Message -> Error");
            }
        }

        private void Update_Load_Data()
        {
            Send_Log_Message("4>MainFrame::Update_Load_Data -> Data Loading Completed!!");
        }

        private void Update_Save_Data()
        {
            Send_Log_Message("4>MainFrame::Update_Save_Data -> Data Saving Completed!!");
        }

        private void Update_Open_Data()
        {
            // 목록 리스트 해제
            flowLayoutPanel_Menulist.Controls.Clear();

            Initiate_MenuList();

            Send_Log_Message("4>MainFrame::Update_Open_Data -> Data Open Completed!!");
        }

        private void Update_Print_Data()
        {
            if (!isActivated)
            {
                Send_Log_Message("4>MainForm::Update_Print_Data -> this form is not activated!!");
                return;
            }

            printPreviewDialog1.StartPosition = FormStartPosition.CenterParent;
            printPreviewDialog1.Document = printDocument1;
            //printPreviewDialog1.ClientSize = new Size(this.Width, this.Height);
            printPreviewDialog1.MinimumSize = new Size(800, 600);
            printPreviewDialog1.UseAntiAlias = true;

            if (printPreviewDialog1.ShowDialog() == DialogResult.Cancel)
            {
                m_printCounter = 0;
                m_printPageNo = 1;
            }

            /*
            printDialog1.Document = printDocument1;
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
            */
            Send_Log_Message("4>MainForm::Update_Print_Data");
        }

        private void Update_Menulist_Up(CDataCell dc)
        {
            int pos = 0;
            foreach (TwoLineList item in flowLayoutPanel_Menulist.Controls)
            {
                if (m_Selected_Menu.PrimaryText == item.PrimaryText)
                {
                    flowLayoutPanel_Menulist.Controls.SetChildIndex(item, pos - 1);
                    break;
                }
                pos++;
            }

            Update_Task_Width();
            Update_Menu_Metadata();

            Send_Log_Message("4>MainFrame::OnMenuListUp_Click -> MenuList move Up Completed!!");
        }

        private void Update_Menulist_Down(CDataCell dc)
        {
            int pos = 0;
            foreach (TwoLineList item in flowLayoutPanel_Menulist.Controls)
            {
                if (m_Selected_Menu.PrimaryText == item.PrimaryText)
                {
                    flowLayoutPanel_Menulist.Controls.SetChildIndex(item, pos + 1);
                    break;
                }
                pos++;
            }

            Update_Task_Width();
            Update_Menu_Metadata();

            Send_Log_Message("4>MainFrame::Update_Menulist_Down -> MenuList move Down Completed!!");
        }

        private void Update_Menulist_Rename(CDataCell dc)
        {
            string source = dc.DC_title;
            string target = dc.DC_listName;

            foreach (TwoLineList list in flowLayoutPanel_Menulist.Controls)
            {
                if (source == list.PrimaryText)
                {
                    list.PrimaryText = target;
                    list.Refresh();

                    Send_Log_Message("4>MainFrame::Menulist_Rename_Process -> Rename Completed from " + source + " to " + target);

                    Menu_List(list);  // 목록 리스트 다시 표시
                    break;
                }
            }

            Update_Task_Width();
            Update_Menu_Metadata();
        }

        private void Update_Menulist_Delete(CDataCell dc)
        {
            string target = dc.DC_listName;
            foreach (TwoLineList list in flowLayoutPanel_Menulist.Controls)
            {
                if (list.PrimaryText == target)
                {
                    Send_Log_Message("4>MainFrame::Update_Menulist_Delete -> m_ListName Deleted is : " + target + "-" + list.PrimaryText);

                    list.TwoLineList_Click -= new TwoLineList_Event(TwoLineList_Click);
                    list.DragEnter -= new DragEventHandler(TwoLineList_DragEnter);
                    list.DragDrop -= new DragEventHandler(TwoLineList_DragDrop);
                    flowLayoutPanel_Menulist.Controls.Remove(list); // 리스트 제거
                    list.Dispose();

                    break;
                }
                else
                {
                    if (!list.IsDivider)
                    {
                        m_Pre_Selected_Menu = list;
                        Send_Log_Message("4>MainFrame::Update_Menulist_Delete -> m_Pre_Selected_Menu is " + list.PrimaryText);
                    }
                }
            }

            m_Selected_Menu = m_Pre_Selected_Menu;
            m_Selected_Menu.IsSelected = true;

            Menu_List(m_Selected_Menu);

            Update_Task_Width();
            Update_Menu_Metadata();

            Send_Log_Message("4>MainFrame::Update_Menulist_Delete -> Delete MenuList is Complete!!");
        }

        private void Update_Menulist_Add(CDataCell dc)
        {
            TwoLineList list = new TwoLineList(new Bitmap(ICON_LIST), dc.DC_listName, "", "");

            list.TwoLineList_Click -= new TwoLineList_Event(TwoLineList_Click);
            list.TwoLineList_Click += new TwoLineList_Event(TwoLineList_Click);
            list.DragEnter -= new DragEventHandler(TwoLineList_DragEnter);
            list.DragEnter += new DragEventHandler(TwoLineList_DragEnter);
            list.DragDrop -= new DragEventHandler(TwoLineList_DragDrop);
            list.DragDrop += new DragEventHandler(TwoLineList_DragDrop);

            flowLayoutPanel_Menulist.Controls.Add(list); // 판넬 컨렉션에 저장

            foreach (TwoLineList item in flowLayoutPanel_Menulist.Controls)  // 폭 설정
            {
                item.Width = flowLayoutPanel_Menulist.VerticalScroll.Visible
                ? flowLayoutPanel_Menulist.Width - 2 - SystemInformation.VerticalScrollBarWidth
                : flowLayoutPanel_Menulist.Width - 2;

                item.IsSelected = false;
            }

            m_Selected_Menu = list;
            m_Selected_Menu.IsSelected = true;
            m_Pre_Selected_Menu = m_Selected_Menu;
            enum_Selected_Menu = MenuList.LIST_MENU;

            Menu_List(m_Selected_Menu);
            Update_Menu_Metadata();

            Send_Log_Message("4>MainFrame::Update_Menulist_Add -> Add New List Menu : " + m_Selected_Menu.PrimaryText);
        }

        public void Update_Add_Task(CDataCell dc)
        {
            string msg = "";
            switch (enum_Selected_Menu)
            {
                case MenuList.MYTODAY_MENU: // 오늘 할 일 메뉴에서 입력됨
                    msg = "MYTODAY_MENU";
                    break;
                case MenuList.IMPORTANT_MENU: // 중요 메뉴에서 입력됨
                    msg = "IMPORTANT_MENU";
                    break;
                case MenuList.DEADLINE_MENU: // 계획된 일정 메뉴에서 입력됨
                    msg = "PLANNED_MENU";
                    break;
                default:
                    if (dc.DC_listName != m_Selected_Menu.PrimaryText) // 보이지 않는 다른 메뉴
                    {
                        Send_Log_Message("4>MainFrame::Update_Add_Task -> Anothor Menu List");
                        Update_Menu_Metadata();
                        return;
                    }
                    else
                    {
                        msg = "Current Menu List"; // 현재 메뉴에서 생성됨
                    }
                    break;
            }

            Todo_Item item = new Todo_Item(dc);  // Task 생성

            m_Task.Insert(0, item);

            flowLayoutPanel2.Controls.Add(item);
            flowLayoutPanel2.Controls.SetChildIndex(item, 0);
            item.UserControl_Click -= new TodoItemList_Event(TodoItem_UserControl_Click);
            item.UserControl_Click += new TodoItemList_Event(TodoItem_UserControl_Click);
            item.DragEnter -= new DragEventHandler(TodoItem_DragEnter);
            item.DragEnter += new DragEventHandler(TodoItem_DragEnter);
            item.DragDrop -= new DragEventHandler(TodoItem_DragDrop);
            item.DragDrop += new DragEventHandler(TodoItem_DragDrop);

            //Console.WriteLine("Update_Add_Task1 " + dc.DC_title);
            //Console.WriteLine("Update_Add_Task2 " + item.TD_title);
            //Console.WriteLine("Update_Add_Task3 " + item.TD_DataCell.DC_title);

            flowLayoutPanel2.VerticalScroll.Value = 0;

            Close_DetailWindow();
            Update_Task_Width();
            Update_Task_Infomation(dc);
            Update_Menu_Metadata();

            Send_Log_Message("4>MainFrame::Update_Add_Task : " + msg + " [" + item.TD_DataCell.DC_task_ID + "]" + item.TD_title);
        }

        public void Update_Delete_Task(CDataCell dc)
        {
            labelUserName.Focus();  // 레이아웃 유지용 포커싱

            int counter = 0;
            foreach (Todo_Item item in flowLayoutPanel2.Controls)  // dc로 td 찾기
            {
                if (dc.DC_task_ID == item.TD_DataCell.DC_task_ID)
                {
                    m_Task.Remove(item);  //m_Task 에서 항목 삭제해야함, 

                    item.UserControl_Click -= new TodoItemList_Event(TodoItem_UserControl_Click);
                    item.DragEnter -= new DragEventHandler(TodoItem_DragEnter);
                    item.DragDrop -= new DragEventHandler(TodoItem_DragDrop);
                    flowLayoutPanel2.Controls.Remove(item);
                    item.Dispose();
                    Send_Log_Message("4>MainFrame::Update_Delete_Task -> Delete Todo_Item : [" + item.TD_DataCell.DC_task_ID + "]" + item.TD_title);

                    int pos = m_currentPage * m_thumbsPerPage;
                    if (pos <= m_Task.Count)  // 20개 항목 이상시는 1개 더 땡겨와야됨 (?)
                    {
                        Send_Log_Message("4>MainFrame::Update_Delete_Task -> 20개 항목 이상으로 밑에서 1개 더 땡김");
                        flowLayoutPanel2.Controls.Add(m_Task[pos - 1]);
                        m_Task[pos - 1].Width = flowLayoutPanel2.Width - TASK_WIDTH_GAP;
                    }

                    counter++;
                    break;
                }
            }

            if (counter == 0)
            {
                Send_Log_Message("Warning>MainFrame::Update_Delete_Task -> No matching Data!!");
            }

            Close_DetailWindow();
            Update_Task_Width();
            Update_Task_Infomation(dc);
            Update_Menu_Metadata();

            Send_Log_Message("4>MainFrame::Update_Delete_Task -> Delete Completed!");
        }

        private void Update_Complete_Process(CDataCell dc)
        {
            Send_Log_Message("4>MainFrame::Update_Complete_Process");

            labelUserName.Focus();  // 레이아웃 유지용 포커싱

            foreach (Todo_Item item in flowLayoutPanel2.Controls)  // dc로 td 찾기
            {
                if (dc.DC_task_ID == item.TD_DataCell.DC_task_ID)
                {
                    item.TD_complete = dc.DC_complete;
                    /*
                    Console.WriteLine("4>MainFrame::Update_Complete_Process -> m_Task.Count : " + m_Task.Count);
                    Console.WriteLine("4>MainFrame::Update_Complete_Process -> m_currentPage : " + m_currentPage);
                    if (m_Task.Count < m_thumbsPerPage) Console.WriteLine("1:true"); else Console.WriteLine("1:false");
                    if (m_currentPage >= ((m_Task.Count / m_thumbsPerPage) + 1)) Console.WriteLine("2:true"); else Console.WriteLine("2:false");
                    if (m_Task.Count == m_currentPage * m_thumbsPerPage) Console.WriteLine("3:true"); else Console.WriteLine("3:false");
                    */
                    if (item.TD_complete)
                    {
                        if (m_Task.Count < m_thumbsPerPage                                  // 한 페이지 보다 작을 경우
                            || m_currentPage >= ((m_Task.Count / m_thumbsPerPage) + 1)      // 전체 페이지가 출력된 경우
                            || m_Task.Count == m_currentPage * m_thumbsPerPage)             // 딱 맞게 20개 단위로 출력된 경우
                        {
                            if (enum_Selected_Menu == MenuList.TODO_ITEM_MENU || enum_Selected_Menu == MenuList.LIST_MENU)
                            {
                                flowLayoutPanel2.Controls.SetChildIndex(item, flowLayoutPanel2.Controls.Count);
                                Send_Log_Message("4>MainFrame::Update_Complete_Process -> 완료 처리 / 일반항목 / 맨밑으로 (잔여 항목 없음)");
                            }
                            else
                            {
                                flowLayoutPanel2.Controls.Remove(item);
                                Send_Log_Message("4>MainFrame::Update_Complete_Process -> 완료 처리 / 특수항목 / 제거함 (잔여 항목 없음)");
                            }
                        }
                        else
                        {
                            flowLayoutPanel2.Controls.Remove(item);  // 전체 페이지가 출력 안된 경우는 한개만 추가함
                            if (enum_Selected_Menu == MenuList.TODO_ITEM_MENU || enum_Selected_Menu == MenuList.LIST_MENU)
                            {
                                flowLayoutPanel2.Controls.Add(m_Task[m_currentPage * m_thumbsPerPage]);
                                Send_Log_Message("4>MainFrame::Update_Complete_Process -> 완료 처리 / 일반항목 / 제거후 1개 땡김 (잔여 항목 있음)");
                            }
                            else
                            {
                                Send_Log_Message("4>MainFrame::Update_Complete_Process -> 완료 처리 / 특수항목 / 제거함 (잔여 항목 있음)");
                            }
                        }

                        int pos = m_Task.IndexOf(item);
                        Todo_Item td = m_Task[pos]; //추출
                        m_Task.RemoveAt(pos); //삭제
                        if (enum_Selected_Menu == MenuList.TODO_ITEM_MENU || enum_Selected_Menu == MenuList.LIST_MENU)
                        {
                            m_Task.Insert(m_Task.Count, td); //삽입
                        }
                    }
                    else
                    {
                        if (m_Task.Count < m_thumbsPerPage                                  // 한 페이지 보다 작을 경우
                            || m_currentPage >= ((m_Task.Count / m_thumbsPerPage) + 1)      // 전체 페이지가 출력된 경우
                            || m_Task.Count == m_currentPage * m_thumbsPerPage)             // 딱 맞게 20개 단위로 출력된 경우
                        {
                            if (enum_Selected_Menu == MenuList.TODO_ITEM_MENU || enum_Selected_Menu == MenuList.LIST_MENU)
                            {
                                flowLayoutPanel2.Controls.SetChildIndex(item, 0);
                                Send_Log_Message("4>MainFrame::Update_Complete_Process -> 완료 해제 / 일반항목 / 맨위로 (잔여 항목 없음)");
                            }
                            else
                            {
                                flowLayoutPanel2.Controls.Remove(item);
                                Send_Log_Message("4>MainFrame::Update_Complete_Process -> 완료 해제 / 특수항목 / 제거함 (잔여 항목 없음)");
                            }
                        }
                        else
                        {
                            if (enum_Selected_Menu == MenuList.TODO_ITEM_MENU || enum_Selected_Menu == MenuList.LIST_MENU)
                            {
                                flowLayoutPanel2.Controls.SetChildIndex(item, 0);
                                Send_Log_Message("4>MainFrame::Update_Complete_Process -> 완료 해제 / 일반항목 / 맨위로 (잔여 항목 있음)");
                            }
                            else
                            {
                                flowLayoutPanel2.Controls.Remove(item);
                                flowLayoutPanel2.Controls.Add(m_Task[m_currentPage * m_thumbsPerPage]);
                                Send_Log_Message("4>MainFrame::Update_Complete_Process -> 완료 해제 / 특수항목 / 제거후 1개 땡김 (잔여 항목 있음)");
                            }
                        }

                        int pos = m_Task.IndexOf(item);
                        Todo_Item td = m_Task[pos]; //추출
                        m_Task.RemoveAt(pos); //삭제
                        if (enum_Selected_Menu == MenuList.TODO_ITEM_MENU || enum_Selected_Menu == MenuList.LIST_MENU)
                        {
                            m_Task.Insert(0, td); //삽입
                        }

                    }
                    SendDataCellToDetailWindow(dc);
                    item.Refresh();
                    break;
                }
            }

            Update_Task_Width();
            Update_Menu_Metadata();

            Send_Log_Message("4>MainFrame::Update_Complete_Process -> Completed!");
        }

        private void Update_Important_Process(CDataCell dc)
        {
            //m_Controller.Notify_Log_Message("4>MainFrame::Update_Important_Process");
            labelUserName.Focus();  // 레이아웃 유지용 포커싱

            foreach (Todo_Item item in flowLayoutPanel2.Controls)  // dc로 td 찾기
            {
                if (dc.DC_task_ID == item.TD_DataCell.DC_task_ID)
                {
                    item.TD_complete = dc.DC_complete;
                    item.TD_important = dc.DC_important;
                    if ((item.TD_important == true) && (item.TD_complete == false))  // 중요 & 미완료시 맨위로 이동
                    {
                        if (enum_Selected_Menu != MenuList.IMPORTANT_MENU)
                        {
                            flowLayoutPanel2.Controls.SetChildIndex(item, 0);
                            flowLayoutPanel2.VerticalScroll.Value = 0;
                            Send_Log_Message("4>MainFrame::Update_Important_Process -> 중요설정 (미완료시) / 일반메뉴 / 맨위로 이동");
                        }
                        else
                        {
                            Send_Log_Message("4>MainFrame::Update_Important_Process -> 중요설정 (미완료시) / 중요메뉴 (해당없음)");
                        }

                        int pos = m_Task.IndexOf(item);
                        Todo_Item td = m_Task[pos]; //추출
                        m_Task.RemoveAt(pos); //삭제
                        m_Task.Insert(0, td); //삽입
                    }
                    else
                    {
                        if (enum_Selected_Menu != MenuList.IMPORTANT_MENU)
                        {
                            Send_Log_Message("4>MainFrame::Update_Important_Process -> 중요설정or해제 / 일반메뉴 / 그대로 ");
                        }
                        else
                        {
                            flowLayoutPanel2.Controls.Remove(item);
                            Send_Log_Message("4>MainFrame::Update_Important_Process -> 중요설정or해제 / 중요메뉴 / 삭제됨");
                        }
                    }
                    SendDataCellToDetailWindow(dc);
                    item.Refresh();
                    break;
                }
            }

            Update_Task_Width();
            Update_Menu_Metadata();

            Send_Log_Message("4>MainFrame::Update_Important_Process -> Completed!");
        }

        private void Update_Convert_NotePad(CDataCell dc)
        {
            foreach (Todo_Item item in flowLayoutPanel2.Controls)  // dc로 td 찾기
            {
                if (dc.DC_task_ID == item.TD_DataCell.DC_task_ID)
                {
                    item.TD_DataCell.DC_notepad = dc.DC_notepad;
                    item.Refresh();
                }
            }

            Send_Log_Message("4>MainFrame::Update_Convert_NotePad -> Completed!!" + dc.DC_title + "-" + dc.DC_notepad);
        }

        private void Update_Transfer_Task(CDataCell dc)
        {
            labelUserName.Focus();  // 레이아웃 유지용 포커싱

            foreach (Todo_Item item in flowLayoutPanel2.Controls)  // dc로 td 찾기
            {
                if (dc.DC_task_ID == item.TD_DataCell.DC_task_ID)
                {
                    m_Task.Remove(item); //삭제
                    flowLayoutPanel2.Controls.Remove(item); // 제거
                }
            }

            Update_Menu_Metadata();

            Send_Log_Message("4>MainFrame::Update_Transfer_Task -> Transfer Item is Completed!!");
        }

        private void Update_Modify_Task_Title(CDataCell dc)
        {
            foreach (Todo_Item item in flowLayoutPanel2.Controls)  // dc로 td 찾기
            {
                if (dc.DC_task_ID == item.TD_DataCell.DC_task_ID)
                {
                    item.TD_title = dc.DC_title;
                    SendDataCellToDetailWindow(dc);
                    item.Refresh();
                    break;
                }
            }

            Send_Log_Message("4>MainFrame::Update_Modify_Task_Title : " + dc.DC_title);
        }

        private void Update_Modify_Task_Memo(CDataCell dc)
        {
            foreach (Todo_Item item in flowLayoutPanel2.Controls)  // dc로 td 찾기
            {
                if (dc.DC_task_ID == item.TD_DataCell.DC_task_ID)
                {
                    SendDataCellToDetailWindow(dc);
                    item.TD_DataCell.DC_memo = dc.DC_memo;
                    m_TaskToolTip.SetToolTip(item, item.TD_DataCell.DC_memo);
                    item.Refresh();
                    break;
                }
            }

            Send_Log_Message("4>MainFrame::Update_Modify_Task_Memo : " + dc.DC_title);
        }

        private void Update_Modify_MyToday(CDataCell dc)
        {
            if (dc.DC_myToday)
            {
                roundLabel1.Text = "나의 하루에 추가됨";
                roundLabel1.BackColor = PSEUDO_SELECTED_COLOR;
            }
            else
            {
                roundLabel1.Text = "나의 하루에 추가";
                roundLabel1.BackColor = COLOR_DETAIL_WINDOW_BACK_COLOR;
            }

            if (enum_Selected_Menu == MenuList.MYTODAY_MENU) Menu_MyToday();  // 나의하루 메뉴에서 실행시

            Update_Task_Infomation(dc);
            Update_Menu_Metadata();

            Send_Log_Message("4>MainFrame::Update_Modify_MyToday -> MyToday Process Completed!!");
        }

        private void Update_Modify_Remind(CDataCell dc)
        {
            if (dc.DC_remindType > 0)
            {
                roundLabel2.Text = "알림 설정됨";
                roundLabel2.BackColor = PSEUDO_SELECTED_COLOR;
            }
            else
            {
                roundLabel2.Text = "미리 알림";
                roundLabel2.BackColor = COLOR_DETAIL_WINDOW_BACK_COLOR;
            }

            Update_Task_Infomation(dc);
            Update_Menu_Metadata();

            Send_Log_Message("4>MainFrame::Update_Modify_Remind -> Modify Remind Completed!!");
        }

        private void Update_Modify_Planned(CDataCell dc)
        {
            if (dc.DC_deadlineType > 0)
            {
                roundLabel3.Text = "기한 설정됨";
                roundLabel3.BackColor = PSEUDO_SELECTED_COLOR;
                Send_Log_Message("4>MainFrame::Update_Modify_Planned -> 기한 설정됨");
            }
            else
            {
                roundLabel3.Text = "기한 설정";
                roundLabel3.BackColor = COLOR_DETAIL_WINDOW_BACK_COLOR;
                Send_Log_Message("4>MainFrame::Update_Modify_Planned -> 기한 해제됨");
            }

            if (enum_Selected_Menu == MenuList.DEADLINE_MENU) Menu_Planned(); // 기한 설정 메뉴에서 실행

            Update_Task_Infomation(dc);
            Update_Menu_Metadata();

            Send_Log_Message("4>MainFrame::Update_Modify_Planned -> Modify Planned Completed!!");
        }

        private void Update_Modify_Repeat(CDataCell dc)
        {
            if (dc.DC_repeatType > 0)
            {
                roundLabel4.Text = "반복 설정됨";
                roundLabel4.BackColor = PSEUDO_SELECTED_COLOR;
            }
            else
            {
                roundLabel4.Text = "반복";
                roundLabel4.BackColor = COLOR_DETAIL_WINDOW_BACK_COLOR;
            }

            Update_Task_Infomation(dc);
            Update_Menu_Metadata();

            Send_Log_Message("4>MainFrame::Update_Modify_Repeat -> Modify Planned Completed!!");
        }

        private void Update_Task_Move_Up(CDataCell dc)
        {
            int pos = 0;
            int counter = 0;
            foreach (Todo_Item item in flowLayoutPanel2.Controls)  // dc로 td 찾기
            {
                if (dc.DC_task_ID == item.TD_DataCell.DC_task_ID)
                {
                    if (pos == 0)
                    {
                        Send_Log_Message("Warning>MainFrame::Update_Task_Move_Up -> Top position, Can't move up");
                        break;
                    }
                    counter++;
                    flowLayoutPanel2.Controls.SetChildIndex(item, pos - 1);
                    break;
                }
                pos++;
            }
            if (counter == 0)
            {
                Send_Log_Message("Warning>MainFrame::Update_Task_Move_Up -> There is no matching item!");
            }

            Send_Log_Message("4>MainFrame::Update_Task_Move_Up -> Move Up Completed! " + dc.DC_title);
        }

        private void Update_Task_Move_Down(CDataCell dc)
        {
            int pos = 0;
            int counter = 0;
            foreach (Todo_Item item in flowLayoutPanel2.Controls)  // dc로 td 찾기
            {
                if (dc.DC_task_ID == item.TD_DataCell.DC_task_ID)
                {
                    if (pos == (flowLayoutPanel2.Controls.Count - 1))
                    {
                        Send_Log_Message("Warning>MainFrame::Update_Task_Move_Down -> Bottom position, Can't move down");
                        break;
                    }
                    counter++;
                    flowLayoutPanel2.Controls.SetChildIndex(item, pos + 1);
                    break;
                }
                pos++;
            }

            if (counter == 0)
            {
                Send_Log_Message("Warning>MainFrame::Update_Task_Move_Down -> There is no matching item!");
            }

            Send_Log_Message("4>MainFrame::Update_Task_Move_Down -> Move Down Completed! " + dc.DC_title);
        }

        private void Update_Task_Move_To(CDataCell dc)
        {
            int pos = 0;
            foreach (Todo_Item item in flowLayoutPanel2.Controls)  // dc로 td 찾기
            {
                if (dc.DC_task_ID == item.TD_DataCell.DC_task_ID)
                {
                    flowLayoutPanel2.Controls.SetChildIndex(m_Selected_Item, pos);

                    break;
                }
                pos++;
            }

            Send_Log_Message("4>MainFrame::Update_Task_Move_To");
        }

        private void Update_Memo_Move_To(CDataCell dc)
        {
            int counter = 0;
            foreach (Todo_Item item in flowLayoutPanel2.Controls)  // dc로 td 찾기
            {
                if (dc.DC_task_ID == item.TD_DataCell.DC_task_ID)
                {
                    Display_Selected_Menu();

                    counter++;
                    break;
                }
            }

            if (counter == 0)
            {
                Send_Log_Message("Warning>MainFrame::Update_Task_Move_Up -> There is no matching item!");
            }

            Send_Log_Message("4>MainFrame::Update_Memo_Move_To -> Completed!");

        }

        //--------------------------------------------------------------
        // 메뉴 리스트를 클릭했을때 처리
        //--------------------------------------------------------------
        private void TwoLineList_Click(object sender, EventArgs e)
        {
            TwoLineList sd = (TwoLineList)sender;
            MouseEventArgs me = (MouseEventArgs)e;

            if (m_Pre_Selected_Menu == null) m_Pre_Selected_Menu = sd;

            if (!m_Pre_Selected_Menu.Equals(sd))
            {
                m_Pre_Selected_Menu.IsSelected = false;
            }

            m_Selected_Menu = sd;
            m_Selected_Menu.IsSelected = true;
            m_Pre_Selected_Menu = m_Selected_Menu;

            Close_DetailWindow();

            switch (me.Button)
            {
                case MouseButtons.Left:
                    Send_Log_Message(">MainFrame::TwoLineList_Click -> Left Button : " + m_Selected_Menu.PrimaryText);
                    Display_Selected_Menu();
                    break;
                case MouseButtons.Right:
                    Send_Log_Message(">MainFrame::TwoLineList_Click -> Right Button : " + m_Selected_Menu.PrimaryText);
                    Display_Selected_Menu();
                    MenuList_Right_Click_ContextMenu();  // 컨텍스트 메뉴
                    break;
                case MouseButtons.Middle:
                    Send_Log_Message(">MainFrame::TwoLineList_Click -> Middle Button : " + m_Selected_Menu.PrimaryText);
                    Menulist_Rename_Process(sender, me);  // 목록명 변경되어 실행됨
                    break;
            }
        }

        private void Display_Selected_Menu()
        {
            Send_Log_Message(">MainFrame::Display_Selected_Menu -> Display Task of Selected Menu : " + m_Selected_Menu.PrimaryText);

            switch (m_Selected_Menu.PrimaryText)
            {
                case "오늘 할 일":
                    Menu_MyToday();
                    break;
                case "중요":
                    Menu_Important();
                    break;
                case "계획된 일정":
                    Menu_Planned();
                    break;
                case "완료됨":
                    Menu_Completed();
                    break;
                case "작업":
                    Menu_Task();
                    break;
                default:
                    Menu_List(m_Selected_Menu);
                    break;
            }
        }

        private void MenuList_Right_Click_ContextMenu()
        {
            ContextMenu menuListContextMenu = new ContextMenu();
            menuListContextMenu.Popup += new EventHandler(OnMenuListPopupEvent);

            MenuItem saveData = new MenuItem("데이터 저장", new EventHandler(OnSaveDataMenu_Click));
            MenuItem displayData = new MenuItem("데이터 표시", new EventHandler(OnDisplayDataMenu_Click));
            MenuItem printData = new MenuItem("데이터 인쇄", new EventHandler(OnPrintDataMenu_Click));
            MenuItem moveListUp = new MenuItem("목록 위로 이동", new EventHandler(OnMenuListUp_Click));
            MenuItem moveListDown = new MenuItem("목록 아래로 이동", new EventHandler(OnMenuListDown_Click));
            MenuItem renameList = new MenuItem("목록 이름바꾸기", new EventHandler(OnRenameMenuList_Click));
            MenuItem deleteList = new MenuItem("목록 삭제", new EventHandler(OnDeleteMenuList_Click));

            menuListContextMenu.MenuItems.Add(saveData);
            menuListContextMenu.MenuItems.Add(displayData);
            menuListContextMenu.MenuItems.Add(printData);
            menuListContextMenu.MenuItems.Add("-");
            menuListContextMenu.MenuItems.Add(moveListUp);
            menuListContextMenu.MenuItems.Add(moveListDown);
            menuListContextMenu.MenuItems.Add("-");
            menuListContextMenu.MenuItems.Add(renameList);
            menuListContextMenu.MenuItems.Add(deleteList);

            if (enum_Selected_Menu == MenuList.LIST_MENU)
            {
                moveListUp.Enabled = true;
                moveListDown.Enabled = true;
                renameList.Enabled = true;
                deleteList.Enabled = true;
            }
            else
            {
                moveListUp.Enabled = false;
                moveListDown.Enabled = false;
                renameList.Enabled = false;
                deleteList.Enabled = false;
            }

            Point cursor = PointToClient(Cursor.Position);
            menuListContextMenu.Show(this, cursor);
        }

        private void OnMenuListPopupEvent(object sender, EventArgs e)
        {

        }

        private void OnSaveDataMenu_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("저장할까요?", WINDOW_CAPTION, MessageBoxButtons.YesNoCancel);
            switch (result)
            {
                case DialogResult.Cancel:
                    return;
                case DialogResult.No:
                    break;
                case DialogResult.Yes:
                    m_Controller.Save_Data_File();
                    break;
            }
        }

        private void OnDisplayDataMenu_Click(object sender, EventArgs e)
        {
            outputForm.StartPosition = FormStartPosition.CenterParent;

            if (outputForm.Visible)
                outputForm.Hide();
            else
                outputForm.Show();

            Display_Data();
        }

        private void OnPrintDataMenu_Click(object sender, EventArgs e)
        {
            m_Controller.Print_Data_File();
        }

        private void OnMenuListUp_Click(object sender, EventArgs e)
        {
            Send_Log_Message("1>MainFrame::OnMenuListUp_Click -> Menulist UP : " + m_Selected_Menu.PrimaryText);

            m_Controller.Perform_Menulist_Up(m_Selected_Menu.PrimaryText);
        }

        private void OnMenuListDown_Click(object sender, EventArgs e)
        {
            Send_Log_Message("1>MainFrame::OnMenuListDown_Click -> Menulist DOWN : " + m_Selected_Menu.PrimaryText);

            m_Controller.Perform_Menulist_Down(m_Selected_Menu.PrimaryText);
        }

        private void OnRenameMenuList_Click(object sender, EventArgs e)
        {
            Send_Log_Message("1-1>MainFrame::OnRenameMenuList_Click -> Show TextBox for Rename Menu List");

            m_Selected_Menu.Rename_Process();
        }

        private void OnDeleteMenuList_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("목록을 삭제할까요?", WINDOW_CAPTION, MessageBoxButtons.YesNo) == DialogResult.No) return;

            Send_Log_Message("1>MainFrame::OnDeleteMenuList_Click -> m_ListName Delete : " + m_Selected_Menu.PrimaryText);
            
            m_Controller.Perform_Menulist_Delete(m_Selected_Menu.PrimaryText);
        }

        //--------------------------------------------------------------
        // 메뉴 Query 처리
        //--------------------------------------------------------------
        private void Menu_MyToday()
        {
            upArrow.Visible = false;
            downArrow.Visible = false;
            textBox_Task.Visible = true;

            label_ListName.Image = ICON_SUNNY;
            label_ListName.Text = "   " + "오늘 할 일";

            enum_Selected_Menu = MenuList.MYTODAY_MENU;

            Send_Log_Message(">MainFrame::Menu_MyToday -> Query & Display MyToday Menu : " + m_Selected_Menu.PrimaryText);
            Add_Task_To_Panel_Paging(m_Controller.Query_MyToday());
        }

        private void Menu_Important()
        {
            upArrow.Visible = false;
            downArrow.Visible = false;
            textBox_Task.Visible = true;

            label_ListName.Image = ICON_GRADE;
            label_ListName.Text = "   " + "중요";

            enum_Selected_Menu = MenuList.IMPORTANT_MENU;

            Send_Log_Message(">MainFrame::Menu_Important -> Query & Display Important Menu : " + m_Selected_Menu.PrimaryText);
            Add_Task_To_Panel_Paging(m_Controller.Query_Important());
        }

        private void Menu_Planned()
        {
            upArrow.Visible = false;
            downArrow.Visible = false;
            textBox_Task.Visible = true;

            label_ListName.Image = ICON_EVENTNOTE;
            label_ListName.Text = "   " + "계획된 일정";

            enum_Selected_Menu = MenuList.DEADLINE_MENU;

            Send_Log_Message(">MainFrame::Menu_Planned -> Query & Display Planned Menu : " + m_Selected_Menu.PrimaryText);
            Add_Task_To_Panel_Paging(m_Controller.Query_Planned());
        }

        private void Menu_Completed()
        {
            upArrow.Visible = false;
            downArrow.Visible = false;
            textBox_Task.Visible = false;

            label_ListName.Image = ICON_CHECKCIRCLE;
            label_ListName.Text = "   " + "완료됨";

            enum_Selected_Menu = MenuList.COMPLETE_MENU;

            Send_Log_Message(">MainFrame::Menu_Completed -> Query & Display Complete Menu : " + m_Selected_Menu.PrimaryText);
            Add_Task_To_Panel_Paging(m_Controller.Query_Complete());
        }

        private void Menu_Task()
        {
            upArrow.Visible = true;
            downArrow.Visible = true;
            textBox_Task.Visible = true;

            label_ListName.Image = ICON_HOME;
            label_ListName.Text = "   " + "작업";

            enum_Selected_Menu = MenuList.TODO_ITEM_MENU;

            Send_Log_Message(">MainFrame::Menu_Task -> Query & Display Task Menu : " + m_Selected_Menu.PrimaryText);
            Add_Task_To_Panel_Paging(m_Controller.Query_Task("작업"));
        }

        private void Menu_List(TwoLineList list)
        {
            upArrow.Visible = true;
            downArrow.Visible = true;
            textBox_Task.Visible = true;

            label_ListName.Image = ICON_LIST;
            label_ListName.Text = "   " + list.PrimaryText;

            enum_Selected_Menu = MenuList.LIST_MENU;

            Send_Log_Message(">MainFrame::Menu_List -> Query & Display List Menu : " + m_Selected_Menu.PrimaryText);
            Add_Task_To_Panel_Paging(m_Controller.Query_Task(m_Selected_Menu.PrimaryText));
        }

        private void Add_Task_To_Panel_Paging(IEnumerable<CDataCell> dataset)
        {
            for (int i = 0; i < m_Task.Count; i++) // eventhandler 제거 및 m_Task 클리어
            {
                m_Task[i].UserControl_Click -= new TodoItemList_Event(TodoItem_UserControl_Click);
                m_Task[i].DragEnter -= new DragEventHandler(TodoItem_DragEnter);
                m_Task[i].DragDrop -= new DragEventHandler(TodoItem_DragDrop);
                //m_Task[i].Dispose();
            }
            m_Task.Clear();

            foreach (CDataCell data in dataset)  // Todo_Item 생성 및 m_Task에 저장
            {
                Todo_Item item = new Todo_Item(data);

                //Console.WriteLine("TodoItem 1 " + data.DC_title);
                //Console.WriteLine("TodoItem 2 " + item.TD_title);
                //Console.WriteLine("TodoItem 3 " + item.TD_DataCell.DC_title);

                item.UserControl_Click -= new TodoItemList_Event(TodoItem_UserControl_Click);
                item.UserControl_Click += new TodoItemList_Event(TodoItem_UserControl_Click); // 이벤트 재구독 확인할 것
                item.DragEnter -= new DragEventHandler(TodoItem_DragEnter);
                item.DragEnter += new DragEventHandler(TodoItem_DragEnter);
                item.DragDrop -= new DragEventHandler(TodoItem_DragDrop);
                item.DragDrop += new DragEventHandler(TodoItem_DragDrop);

                item.TD_infomation = MakeInfoTextFromDataCell(data);
                m_TaskToolTip.SetToolTip(item, item.TD_DataCell.DC_memo);
                m_Task.Add(item);
            }

            m_currentPage = 1;
            flowLayoutPanel2.SuspendLayout();
            flowLayoutPanel2.Controls.Clear();
            for (int i = 0; i < m_thumbsPerPage; i++)  // 한 페이지당 20개 표기, 첫페이지 표시
            {
                if (i == m_Task.Count) break;
                flowLayoutPanel2.Controls.Add(m_Task[i]);
                m_Task[i].Width = flowLayoutPanel2.Width - TASK_WIDTH_GAP;
            }
            flowLayoutPanel2.ResumeLayout();

            // 화면이 길어 20개 길이가 부족시 한페이지 더 추가함 -> 40개 이상은 불가
            if (((flowLayoutPanel2.Height / TASK_HEIGHT) > m_thumbsPerPage) && (m_Task.Count > m_thumbsPerPage))  
            {
                for (int i = 0; i < m_thumbsPerPage; i++)
                {
                    if (m_currentPage * m_thumbsPerPage + i >= m_Task.Count) break;
                    flowLayoutPanel2.Controls.Add(m_Task[m_currentPage * m_thumbsPerPage + i]);
                    m_Task[m_currentPage * m_thumbsPerPage + i].Width = flowLayoutPanel2.Width - TASK_WIDTH_GAP;
                }
                m_currentPage++;
            }

            if (flowLayoutPanel2.Controls.Count < 1) return;

            // 화면 맨위 항목을 선택한다
            Todo_Item sd = null;

            foreach (Todo_Item item in flowLayoutPanel2.Controls)
            {
                if (item is Todo_Item)
                {
                    sd = item;
                    break;
                }
            }

            if (m_Pre_Selected_Item == null) m_Pre_Selected_Item = sd;

            if (m_Pre_Selected_Item.Equals(sd))
            {

            }
            else
            {
                m_Pre_Selected_Item.IsItemSelected = false;
            }
            //Console.WriteLine("sd : " + sd.TD_DataCell.DC_title);
            m_Pre_Selected_Item = sd;
            m_Selected_Item = sd;
            m_Selected_Item.IsItemSelected = true;

            SendDataCellToDetailWindow(m_Selected_Item.TD_DataCell);
        }

        private void Add_Task_To_Panel_ScrollDown()
        {
            flowLayoutPanel2.Focus();
            int max = flowLayoutPanel2.VerticalScroll.Maximum - flowLayoutPanel2.VerticalScroll.LargeChange;
            if (flowLayoutPanel2.VerticalScroll.Value <= max) return;
            if (m_currentPage >= ((m_Task.Count / m_thumbsPerPage) + 1)) return;
            for (int i = 0; i < m_thumbsPerPage; i++)
            {
                if (m_currentPage * m_thumbsPerPage + i >= m_Task.Count) break;
                flowLayoutPanel2.Controls.Add(m_Task[m_currentPage * m_thumbsPerPage + i]);
                m_Task[m_currentPage * m_thumbsPerPage + i].Width = flowLayoutPanel2.Width - TASK_WIDTH_GAP;
            }
            m_currentPage++;
        }

        private void flowLayoutPanel2_MouseWheel(object sender, MouseEventArgs e)
        {
            Add_Task_To_Panel_ScrollDown();
        }

        private void flowLayoutPanel2_Scroll(object sender, ScrollEventArgs e)
        {
            Add_Task_To_Panel_ScrollDown();
        }

        private void textBox_AddList_Enter(object sender, EventArgs e)
        {
            if (!isTextbox_List_Clicked) textBox_AddList.Text = "";
            isTextbox_List_Clicked = true;
        }

        private void textBox_AddList_Leave(object sender, EventArgs e)
        {
            textBox_AddList.Text = "+ 새 목록 추가";
            isTextbox_List_Clicked = false;
        }

        private void textBox_AddList_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = false;
                e.SuppressKeyPress = false;

                if (textBox_AddList.Text.Trim().Length == 0) return;

                Send_Log_Message("1>MainFrame::textBox_AddList_KeyUp -> Add List!! " + textBox_AddList.Text);

                Add_List(textBox_AddList.Text);

                textBox_AddList.Text = "";
            }
        }

        private void textBox_AddList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void textBox_AddList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu textboxMenu = new ContextMenu();
                MenuItem copyMenu = new MenuItem("복사", new EventHandler(OnCopyMenu_textBox_AddList_Click));
                MenuItem cutMenu = new MenuItem("잘라내기", new EventHandler(OnCutMenu_textBox_AddList_Click));
                MenuItem pasteMenu = new MenuItem("붙여넣기", new EventHandler(OnPasteMenu_textBox_AddList_Click));

                textboxMenu.Popup += new EventHandler(OnPopupEvent_textBox_AddList);
                textboxMenu.MenuItems.Add(copyMenu);
                textboxMenu.MenuItems.Add(cutMenu);
                textboxMenu.MenuItems.Add(pasteMenu);
                textBox_AddList.ContextMenu = textboxMenu;

                textBox_AddList.ContextMenu.Show(textBox_AddList, new Point(e.X, e.Y));
            }
        }

        private void OnPopupEvent_textBox_AddList(object sender, EventArgs e)
        {
            ContextMenu ctm = (ContextMenu)sender;
            ctm.MenuItems[0].Enabled = textBox_AddList.SelectedText.Length != 0; // copy
            ctm.MenuItems[1].Enabled = textBox_AddList.SelectedText.Length != 0; // cut
            ctm.MenuItems[2].Enabled = Clipboard.ContainsText(); // paste
        }
        private void OnCopyMenu_textBox_AddList_Click(object sender, EventArgs e) { textBox_AddList.Copy(); }
        private void OnCutMenu_textBox_AddList_Click(object sender, EventArgs e) { textBox_AddList.Cut(); }
        private void OnPasteMenu_textBox_AddList_Click(object sender, EventArgs e) { textBox_AddList.Paste(); }

        //--------------------------------------------------------------
        // 할일 항목을 클릭했을때 처리
        //--------------------------------------------------------------
        private void TodoItem_UserControl_Click(object sender, EventArgs e)
        {
            Todo_Item sd = (Todo_Item)sender;

            MouseEventArgs me = (MouseEventArgs)e;

            if (m_Pre_Selected_Item == null) m_Pre_Selected_Item = sd;

            if (m_Pre_Selected_Item.Equals(sd))
            {
                if (!(sd.IsCompleteClicked || sd.IsImportantClicked))
                {
                    if (me.Button == MouseButtons.Left) 
                    { 
                        if (isDetailWindowOpen) Close_DetailWindow(); else Open_DetailWindow(); 
                    }
                }
            }
            else
            {
                m_Pre_Selected_Item.IsItemSelected = false;
            }

            m_Pre_Selected_Item = sd;
            m_Selected_Item = sd;
            m_Selected_Item.IsItemSelected = true;

            m_Controller.Send_DataCell(sd.TD_DataCell);

            Send_Log_Message(">MainFrame::TodoItem_UserControl_Click : " + m_Selected_Item.TD_title);

            //Console.WriteLine("TodoItem_UserControl_Click1 " + sd.TD_title);
            //Console.WriteLine("TodoItem_UserControl_Click2 " + m_Selected_Item.TD_title);
            //Console.WriteLine("TodoItem_UserControl_Click3 " + sd.TD_DataCell.DC_title);
            //Console.WriteLine("TodoItem_UserControl_Click4 " + m_Selected_Item.TD_DataCell.DC_title);

            SendDataCellToDetailWindow(m_Selected_Item.TD_DataCell);
            Update_Task_Width();

            switch (me.Button)
            {
                case MouseButtons.Left:
                    if (sd.IsCompleteClicked) //완료됨 클릭시
                    {
                        sd.TD_DataCell.DC_complete = sd.TD_complete;

                        Send_Log_Message("1>MainFrame::TodoItem_UserControl_Click -> Complete :" + sd.TD_complete);
                        m_Controller.Perform_Complete_Process(sd.TD_DataCell);

                        sd.IsCompleteClicked = false;
                    }
                    if (sd.IsImportantClicked)  // 중요 항목 클릭시
                    {
                        sd.TD_DataCell.DC_important = sd.TD_important;

                        Send_Log_Message("1>MainFrame::TodoItem_UserControl_Click -> Important :" + sd.TD_important);
                        m_Controller.Perform_Important_Process(sd.TD_DataCell);

                        sd.IsImportantClicked = false;
                    }
                    if (sd.TD_DataCell.DC_notepad)
                    {
                        Send_Log_Message("1>MainFrame::TodoItem_UserControl_Click -> Transfer RTF Data :" + sd.TD_title);
                        m_Controller.Perform_Transfer_RTF_Data(sd.TD_DataCell);
                    }
                    break;
                case MouseButtons.Right:
                    Task_ContextMenu();
                    break;
            }
        }

        private void Task_ContextMenu()
        {
            ContextMenu todoItemContextMenu = new ContextMenu();
            todoItemContextMenu.Popup += new EventHandler(OnTodoItemPopupEvent);

            MenuItem myTodayItem = new MenuItem("나의 하루에 추가", new EventHandler(OnMyToday_Click)); // 재활용
            MenuItem importantItem = new MenuItem("중요로 표시", new EventHandler(OnImportantMenuItem_Click));
            MenuItem completeItem = new MenuItem("완료됨으로 표시", new EventHandler(OnCompleteMenuItem_Click));
            MenuItem todayDeadlineItem = new MenuItem("오늘까지", new EventHandler(OnTodayDeadline_Click)); // 재활용
            MenuItem tomorrowDeadlineItem = new MenuItem("내일까지", new EventHandler(OnTomorrowDeadline_Click)); // 재활용
            MenuItem selectDayItem = new MenuItem("날짜 선택", new EventHandler(OnSelectDeadline_Click)); // 재활용
            MenuItem deleteDeadlineItem = new MenuItem("기한 제거", new EventHandler(OnDeleteDeadline_Click)); // 재활용
            MenuItem menuEditItem = new MenuItem("메모 확장", new EventHandler(OnMemoEditMenuItem_Click));
            MenuItem notepadItem = new MenuItem("노트패트로 전환", new EventHandler(OnNotePadMenuItem_Click));
            MenuItem moveItem = new MenuItem("항목 이동");
            MenuItem deleteItem = new MenuItem("항목 삭제", new EventHandler(OnDeleteItem_Click));

            todoItemContextMenu.MenuItems.Add(myTodayItem);
            todoItemContextMenu.MenuItems.Add(importantItem);
            todoItemContextMenu.MenuItems.Add(completeItem);
            todoItemContextMenu.MenuItems.Add("-");
            todoItemContextMenu.MenuItems.Add(todayDeadlineItem);
            todoItemContextMenu.MenuItems.Add(tomorrowDeadlineItem);
            todoItemContextMenu.MenuItems.Add(selectDayItem);
            todoItemContextMenu.MenuItems.Add(deleteDeadlineItem);
            todoItemContextMenu.MenuItems.Add("-");
            todoItemContextMenu.MenuItems.Add(menuEditItem);
            todoItemContextMenu.MenuItems.Add(notepadItem);
            todoItemContextMenu.MenuItems.Add("-");
            todoItemContextMenu.MenuItems.Add(moveItem);
            todoItemContextMenu.MenuItems.Add(deleteItem);

            foreach (string list in m_Controller.Query_ListName())
            {
                moveItem.MenuItems.Add(new MenuItem(list, new EventHandler(OnTransferItem_Click)));
            }

            Point cursor = PointToClient(Cursor.Position);
            todoItemContextMenu.Show(this, cursor);
        }

        private void OnTodoItemPopupEvent(object sender, EventArgs e)
        {
            ContextMenu ctm = (ContextMenu)sender;
            
            ctm.MenuItems[0].Text = m_Selected_Item.TD_DataCell.DC_myToday ? "나의 하루에서 제거" : "나의 하루에 추가";
            ctm.MenuItems[1].Text = m_Selected_Item.TD_DataCell.DC_important ? "중요도 제거" : "중요로 표시";
            ctm.MenuItems[2].Text = m_Selected_Item.TD_DataCell.DC_complete ? "완료되지 않음으로 표시" : "완료됨으로 표시";
            ctm.MenuItems[4].Enabled = m_Selected_Item.TD_DataCell.DC_deadlineType != 1;
            ctm.MenuItems[5].Enabled = m_Selected_Item.TD_DataCell.DC_deadlineType != 2;
            ctm.MenuItems[7].Enabled = m_Selected_Item.TD_DataCell.DC_deadlineType > 0;

            ctm.MenuItems[10].Text = m_Selected_Item.TD_DataCell.DC_notepad ? "할 일로 전환" : "노트패드로 전환";
        }

        private void OnMyToday_Click(object sender, EventArgs e)
        {
            m_Controller.Perform_MyToday_Process(m_Selected_Item.TD_DataCell);
        }

        private void OnCompleteMenuItem_Click(object sender, EventArgs e)
        {
            roundCheckbox1.Checked = !roundCheckbox1.Checked;

            m_Selected_Item.TD_complete = roundCheckbox1.Checked;
            m_Selected_Item.TD_DataCell.DC_complete = roundCheckbox1.Checked;

            Send_Log_Message("1>MainFrame::OnCompleteMenuItem_Click -> Complete :" + m_Selected_Item.TD_DataCell.DC_complete);
            m_Controller.Perform_Complete_Process(m_Selected_Item.TD_DataCell);

            Update_Menu_Metadata();
        }

        private void OnImportantMenuItem_Click(object sender, EventArgs e)
        {
            starCheckbox1.Checked = !starCheckbox1.Checked;

            m_Selected_Item.TD_important = starCheckbox1.Checked;
            m_Selected_Item.TD_DataCell.DC_important = starCheckbox1.Checked;
            Send_Log_Message("1>MainFrame::OnImportantMenuItem_Click -> Important :" + m_Selected_Item.TD_DataCell.DC_important);
            m_Controller.Perform_Important_Process(m_Selected_Item.TD_DataCell);

            Update_Menu_Metadata();
        }

        private void OnMemoEditMenuItem_Click(object sender, EventArgs e)
        {
            Edit_Task_Memo();
        }

        private void OnNotePadMenuItem_Click(object sender, EventArgs e)
        {
            Send_Log_Message("1>MainFrame::OnNotePadMenuItem_Click : " + m_Selected_Item.TD_DataCell.DC_title);
            m_Controller.Perform_Convert_NotePad(m_Selected_Item.TD_DataCell);
        }

        private void OnTransferItem_Click(object sender, EventArgs e)
        {
            MenuItem list = (MenuItem)sender;

            if (list.Text == m_Selected_Menu.PrimaryText)
            {
                Send_Log_Message("Warning>MainFrame::OnTransferItem_Click -> Can't transfer item as same list");
                return;
            }

            Send_Log_Message("1>MainFrame::OnTransferItem_Click -> Transfer Item Click!! : from " + m_Selected_Item.TD_DataCell.DC_listName + " to " + list.Text);
            
            m_Controller.Perform_Trasnfer_Task(m_Selected_Item.TD_DataCell, list.Text);
        }

        private void OnDeleteItem_Click(object sender, EventArgs e)
        {
            Send_Log_Message("1>MainFrame::OnDeleteItem_Click -> Delete Task : " + m_Selected_Item.TD_DataCell.DC_title);

            Task_Delete(m_Selected_Item.TD_DataCell);
        }

        // -----------------------------------------------------------
        // 할일 입력 처리 부분
        // -----------------------------------------------------------
        private void textBox_Task_Enter(object sender, EventArgs e)
        {
            if (!isTextbox_Task_Clicked) textBox_Task.Text = "";
            isTextbox_Task_Clicked = true;
        }

        private void textBox_Task_Leave(object sender, EventArgs e)
        {
            textBox_Task.Text = "+ 작업 추가";
            isTextbox_Task_Clicked = false;
        }

        private void textBox_Task_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void textBox_Task_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = false;
                e.SuppressKeyPress = false;

                if (textBox_Task.Text.Trim().Length == 0) return;
                Task_Add(textBox_Task.Text);  // 입력 사항에 오류 및 특수문자("&")가 있는지 체크할 것
                textBox_Task.Text = "";
            }
        }

        private void textBox_Task_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu textboxMenu = new ContextMenu();
                MenuItem copyMenu = new MenuItem("복사", new EventHandler(OnCopyMenu_textBox_Task_Click));
                MenuItem cutMenu = new MenuItem("잘라내기", new EventHandler(OnCutMenu_textBox_Task_Click));
                MenuItem pasteMenu = new MenuItem("붙여넣기", new EventHandler(OnPasteMenu_textBox_Task_Click));

                textboxMenu.Popup += new EventHandler(OnPopupEvent_textBox_Task);
                textboxMenu.MenuItems.Add(copyMenu);
                textboxMenu.MenuItems.Add(cutMenu);
                textboxMenu.MenuItems.Add(pasteMenu);
                textBox_Task.ContextMenu = textboxMenu;

                textBox_Task.ContextMenu.Show(textBox_Task, new Point(e.X, e.Y));
            }
        }

        private void OnPopupEvent_textBox_Task(object sender, EventArgs e)
        {
            ContextMenu ctm = (ContextMenu)sender;
            ctm.MenuItems[0].Enabled = textBox_Task.SelectedText.Length != 0; // copy
            ctm.MenuItems[1].Enabled = textBox_Task.SelectedText.Length != 0; // cut
            ctm.MenuItems[2].Enabled = Clipboard.ContainsText(); // paste
        }
        private void OnCopyMenu_textBox_Task_Click(object sender, EventArgs e) { textBox_Task.Copy(); }
        private void OnCutMenu_textBox_Task_Click(object sender, EventArgs e) { textBox_Task.Cut(); }
        private void OnPasteMenu_textBox_Task_Click(object sender, EventArgs e) { textBox_Task.Paste(); }

        // --------------------------------------------------------------------------
        // 상세창 처리 부분 (제목 / 완료 / 중요 / 메모 / 위아래 / 닫기 / 삭제)
        // --------------------------------------------------------------------------
        private void textBox_Title_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void textBox_Title_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = false;
                e.SuppressKeyPress = false;

                if (textBox_Title.Text.Trim().Length == 0)
                {
                    textBox_Title.Text = m_Selected_Item.TD_DataCell.DC_title;
                    return;
                }

                m_Selected_Item.TD_DataCell.DC_title = textBox_Title.Text;  // 입력 사항에 오류가 있는지 체크할 것

                Send_Log_Message("1>MainFrame::textBox_Title_KeyUp -> Title :" + m_Selected_Item.TD_DataCell.DC_title);
                m_Controller.Perform_Modify_Task_Title(m_Selected_Item.TD_DataCell);
            }
        }

        private void textBox_Title_Leave(object sender, EventArgs e)
        {
            if (textBox_Title.Text.Trim().Length == 0)
            {
                textBox_Title.Text = m_Selected_Item.TD_DataCell.DC_title;
                return;
            }

            m_Selected_Item.TD_DataCell.DC_title = textBox_Title.Text;  // 입력 사항에 오류가 있는지 체크할 것

            Send_Log_Message("1>MainFrame::textBox_Title_Leave -> Title :" + m_Selected_Item.TD_DataCell.DC_title);
            m_Controller.Perform_Modify_Task_Title(m_Selected_Item.TD_DataCell);
        }

        private void textBox_Title_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu textboxMenu = new ContextMenu();
                MenuItem copyMenu = new MenuItem("복사", new EventHandler(OnCopyMenu_textBox_Title_Click));
                MenuItem cutMenu = new MenuItem("잘라내기", new EventHandler(OnCutMenu_textBox_Title_Click));
                MenuItem pasteMenu = new MenuItem("붙여넣기", new EventHandler(OnPasteMenu_textBox_Title_Click));

                textboxMenu.Popup += new EventHandler(OnPopupEvent_textBox_Title);
                textboxMenu.MenuItems.Add(copyMenu);
                textboxMenu.MenuItems.Add(cutMenu);
                textboxMenu.MenuItems.Add(pasteMenu);
                textBox_Title.ContextMenu = textboxMenu;

                textBox_Title.ContextMenu.Show(textBox_Title, new Point(e.X, e.Y));
            }
        }

        private void OnPopupEvent_textBox_Title(object sender, EventArgs e)
        {
            ContextMenu ctm = (ContextMenu)sender;
            ctm.MenuItems[0].Enabled = textBox_Title.SelectedText.Length != 0; // copy
            ctm.MenuItems[1].Enabled = textBox_Title.SelectedText.Length != 0; // cut
            ctm.MenuItems[2].Enabled = Clipboard.ContainsText(); // paste
        }

        private void OnCopyMenu_textBox_Title_Click(object sender, EventArgs e) { textBox_Title.Copy(); }
        private void OnCutMenu_textBox_Title_Click(object sender, EventArgs e) { textBox_Title.Cut(); }
        private void OnPasteMenu_textBox_Title_Click(object sender, EventArgs e) { textBox_Title.Paste(); }

        // 상세창 메모 커서 벗어남
        private void textBox_Memo_Leave(object sender, EventArgs e)
        {
            //메모 내용에 변경이 있는지 확인(?)
            if (isTextbox_Momo_Changed) 
            { 
                isTextbox_Momo_Changed = false; 
            }
            else 
            {
                return;
            }

            m_Selected_Item.TD_DataCell.DC_memo = textBox_Memo.Text;  // 입력 사항에 오류가 있는지 체크할 것

            Send_Log_Message("1>MainFrame::textBox_Memo_Leave -> Changed Memo!!");

            m_Controller.Perform_Modify_Task_Memo(m_Selected_Item.TD_DataCell);
        }

        private void textBox_Memo_TextChanged(object sender, EventArgs e)
        {
            isTextbox_Momo_Changed = true;
        }

        // 상세창 메모 컨텍스트 메뉴
        private void textBox_Memo_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu textboxMenu = new ContextMenu();
                MenuItem extendMemo = new MenuItem("메모확장", new EventHandler(OnExtendMemo_textBox_Memo_Click));
                MenuItem copyMenu = new MenuItem("복사", new EventHandler(OnCopyMenu_textBox_Memo_Click));
                MenuItem cutMenu = new MenuItem("잘라내기", new EventHandler(OnCutMenu_textBox_Memo_Click));
                MenuItem pasteMenu = new MenuItem("붙여넣기", new EventHandler(OnPasteMenu_textBox_Memo_Click));
                MenuItem selectAllMenu = new MenuItem("전체 선택", new EventHandler(OnSelectAllMenu_textBox_Memo_Click));
                MenuItem undoMenu = new MenuItem("실행 취소", new EventHandler(OnUndoMenu_textBox_Memo_Click));

                textboxMenu.Popup += new EventHandler(OnPopupEvent_textBox_Memo);
                textboxMenu.MenuItems.Add(extendMemo);
                textboxMenu.MenuItems.Add("-");
                textboxMenu.MenuItems.Add(copyMenu);
                textboxMenu.MenuItems.Add(cutMenu);
                textboxMenu.MenuItems.Add(pasteMenu);
                textboxMenu.MenuItems.Add(selectAllMenu);
                textboxMenu.MenuItems.Add("-");
                textboxMenu.MenuItems.Add(undoMenu);
                textBox_Memo.ContextMenu = textboxMenu;

                textBox_Memo.ContextMenu.Show(textBox_Memo, new Point(e.X, e.Y));
            }
        }

        private void OnPopupEvent_textBox_Memo(object sender, EventArgs e)
        {
            ContextMenu ctm = (ContextMenu)sender;

            ctm.MenuItems[2].Enabled = textBox_Memo.SelectedText.Length != 0; // copy
            ctm.MenuItems[3].Enabled = textBox_Memo.SelectedText.Length != 0; // cut
            ctm.MenuItems[4].Enabled = Clipboard.ContainsText(); // paste
            ctm.MenuItems[5].Enabled = textBox_Memo.Text.Length != 0; // selectAll
            ctm.MenuItems[7].Enabled = textBox_Memo.CanUndo; // undo
        }

        private void OnExtendMemo_textBox_Memo_Click(object sender, EventArgs e)
        {
            Edit_Task_Memo();
        }

        private void OnCopyMenu_textBox_Memo_Click(object sender, EventArgs e) { textBox_Memo.Copy(); }
        private void OnCutMenu_textBox_Memo_Click(object sender, EventArgs e) { textBox_Memo.Cut(); }
        private void OnPasteMenu_textBox_Memo_Click(object sender, EventArgs e) { textBox_Memo.Paste(); }
        private void OnSelectAllMenu_textBox_Memo_Click(object sender, EventArgs e) { textBox_Memo.SelectAll(); }
        private void OnUndoMenu_textBox_Memo_Click(object sender, EventArgs e) { textBox_Memo.Undo(); }

        // 상세창 닫기 버튼
        private void button1_Click(object sender, EventArgs e)
        {
            if (isDetailWindowOpen) Close_DetailWindow();

            Update_Task_Width();
        }

        // 상세창 삭제 버튼
        private void button2_Click_1(object sender, EventArgs e)
        {
            Send_Log_Message("1>MainFrame::button2_Click_1 -> Delete Task");

            Task_Delete(m_Selected_Item.TD_DataCell);
        }

        // -------------------------------------------------
        // 상세창 완료 체크시
        // -------------------------------------------------
        private void roundCheckbox1_MouseClick(object sender, EventArgs e)
        {
            m_Selected_Item.TD_complete = roundCheckbox1.Checked;
            m_Selected_Item.TD_DataCell.DC_complete = roundCheckbox1.Checked;

            Send_Log_Message("1>MainFrame::roundCheckbox1_MouseClick -> Complete :" + m_Selected_Item.TD_DataCell.DC_complete);
            m_Controller.Perform_Complete_Process(m_Selected_Item.TD_DataCell);
        }

        private void roundCheckbox1_MouseEnter(object sender, EventArgs e)
        {
            roundCheckbox1.BackColor = COLOR_DETAIL_WINDOW_BACK_COLOR;
        }

        private void roundCheckbox1_MouseLeave(object sender, EventArgs e)
        {
            roundCheckbox1.BackColor = COLOR_DETAIL_WINDOW_BACK_COLOR;
        }

        // -------------------------------------------------
        // 상세창 중요 체크시
        // -------------------------------------------------
        private void starCheckbox1_MouseClick(object sender, EventArgs e)
        {
            m_Selected_Item.TD_important = starCheckbox1.Checked;
            m_Selected_Item.TD_DataCell.DC_important = starCheckbox1.Checked;

            Send_Log_Message("1>MainFrame::starCheckbox1_MouseClick -> Important :" + m_Selected_Item.TD_DataCell.DC_important);
            m_Controller.Perform_Important_Process(m_Selected_Item.TD_DataCell);
        }

        private void starCheckbox1_MouseEnter(object sender, EventArgs e)
        {
            starCheckbox1.BackColor = COLOR_DETAIL_WINDOW_BACK_COLOR;
        }

        private void starCheckbox1_MouseLeave(object sender, EventArgs e)
        {
            starCheckbox1.BackColor = COLOR_DETAIL_WINDOW_BACK_COLOR;
        }

        // -------------------------------------------------
        // 상세창 - 나의 하루에 추가 메뉴
        // -------------------------------------------------
        private void roundLabel1_Click(object sender, MouseEventArgs e)
        {
            Send_Log_Message("1>MainFrame::roundLabel1_Click -> MyToday Process!!");

            m_Controller.Perform_MyToday_Process(m_Selected_Item.TD_DataCell);
        }

        private void roundLabel1_MouseEnter(object sender, EventArgs e)
        {
            roundLabel1.BackColor = PSEUDO_HIGHLIGHT_COLOR;
        }

        private void roundLabel1_MouseLeave(object sender, EventArgs e)
        {
            roundLabel1.BackColor = m_Selected_Item.TD_DataCell.DC_myToday ? PSEUDO_SELECTED_COLOR : COLOR_DETAIL_WINDOW_BACK_COLOR;
        }

        // -------------------------------------------------
        // 상세창 - 미리 알림
        // -------------------------------------------------
        private void roundLabel2_MouseEnter(object sender, EventArgs e)
        {
            roundLabel2.BackColor = PSEUDO_HIGHLIGHT_COLOR;
        }

        private void roundLabel2_MouseLeave(object sender, EventArgs e)
        {
            roundLabel2.BackColor = m_Selected_Item.TD_DataCell.DC_remindType > 0 ? PSEUDO_SELECTED_COLOR : COLOR_DETAIL_WINDOW_BACK_COLOR;
        }

        private void roundLabel2_Click(object sender, MouseEventArgs e)
        {
            roundLabel2.Focus();
            if (e.Button != MouseButtons.Left) return;

            ContextMenu remindMenu = new ContextMenu();
            MenuItem todayRemind = new MenuItem("오늘 나중에", new EventHandler(OnTodayRemind_Click));
            MenuItem tomorrowRemind = new MenuItem("내일", new EventHandler(OnTomorrowRemind_Click));
            MenuItem nextWeekRemind = new MenuItem("다음 주", new EventHandler(OnNextWeekRemind_Click));
            MenuItem selectRemind = new MenuItem("날짜 및 시간 선택", new EventHandler(OnSelectRemind_Click));
            MenuItem deleteRemind = new MenuItem("미리 알림 제거", new EventHandler(OnDeleteRemind_Click));
            remindMenu.MenuItems.Add(todayRemind);
            remindMenu.MenuItems.Add(tomorrowRemind);
            remindMenu.MenuItems.Add(nextWeekRemind);
            remindMenu.MenuItems.Add(selectRemind);
            remindMenu.MenuItems.Add(deleteRemind);

            int px = splitContainer1.SplitterDistance + splitContainer2.SplitterDistance + 60;
            int py = 107;
            remindMenu.Show(this, new Point(px, py));
        }

        private void OnTodayRemind_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            dt = dt.Minute < 30 ? dt.AddHours(3) : dt.AddHours(4);

            m_Selected_Item.TD_DataCell.DC_remindType = 1;
            m_Selected_Item.TD_DataCell.DC_remindTime = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, 00, 00);

            Send_Log_Message("1>MainFrame::OnTodayRemind_Click -> Modify Today Remind!!");
            m_Controller.Perform_Modify_Remind(m_Selected_Item.TD_DataCell);
        }

        private void OnTomorrowRemind_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            dt = dt.AddDays(1);

            m_Selected_Item.TD_DataCell.DC_remindType = 2;
            m_Selected_Item.TD_DataCell.DC_remindTime = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, 00, 00);

            Send_Log_Message("1>MainFrame::OnTomorrowRemind_Click -> Modify Tomorrow Remind!!");
            m_Controller.Perform_Modify_Remind(m_Selected_Item.TD_DataCell);
        }

        private void OnNextWeekRemind_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            dt = dt.AddDays(8-(int)dt.DayOfWeek);

            m_Selected_Item.TD_DataCell.DC_remindType = 3;
            m_Selected_Item.TD_DataCell.DC_remindTime = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, 00, 00);

            Send_Log_Message("1>MainFrame::OnNextWeekRemind_Click -> Modify NextWeek Remind!!");
            m_Controller.Perform_Modify_Remind(m_Selected_Item.TD_DataCell);
        }

        private void OnSelectRemind_Click(object sender, EventArgs e)
        {
            DateTimePickerForm calendar = new DateTimePickerForm();

            calendar.ShowDialog();

            if (!calendar.IsSelected || calendar.SelectedDateTime == default)
            {
                Send_Log_Message("1>MainFrame::OnSelectRemind_Click -> Modify Remind Canceled");
                return;
            }
            calendar.IsSelected = false;

            m_Selected_Item.TD_DataCell.DC_remindType = 4;
            m_Selected_Item.TD_DataCell.DC_remindTime = calendar.SelectedDateTime;

            Send_Log_Message("1>MainFrame::OnSelectRemind_Click -> Modify Selected Day Remind!!");
            m_Controller.Perform_Modify_Remind(m_Selected_Item.TD_DataCell);
        }

        private void OnDeleteRemind_Click(object sender, EventArgs e)
        {
            m_Selected_Item.TD_DataCell.DC_remindType = 0;
            m_Selected_Item.TD_DataCell.DC_remindTime = default;

            Send_Log_Message("1>MainFrame::OnSelectRemind_Click -> Delete Remind!!");
            m_Controller.Perform_Modify_Remind(m_Selected_Item.TD_DataCell);
        }

        // -------------------------------------------------
        // 상세창 - 기한 설정 메뉴
        // -------------------------------------------------
        private void roundLabel3_MouseEnter(object sender, EventArgs e)
        {
            roundLabel3.BackColor = PSEUDO_HIGHLIGHT_COLOR;
        }

        private void roundLabel3_MouseLeave(object sender, EventArgs e)
        {
            roundLabel3.BackColor = m_Selected_Item.TD_DataCell.DC_deadlineType > 0 ? PSEUDO_SELECTED_COLOR : COLOR_DETAIL_WINDOW_BACK_COLOR;
        }

        private void roundLabel3_Click(object sender, MouseEventArgs e)
        {
            roundLabel3.Focus();
            if (e.Button != MouseButtons.Left) return;

            ContextMenu deadlineMenu = new ContextMenu();
            MenuItem todayDeadline = new MenuItem("오늘", new EventHandler(OnTodayDeadline_Click));
            MenuItem tomorrowDeadline = new MenuItem("내일", new EventHandler(OnTomorrowDeadline_Click));
            MenuItem nextWeekDeadline = new MenuItem("다음주", new EventHandler(OnNextWeekDeadline_Click));
            MenuItem selectDeadline = new MenuItem("날짜 선택", new EventHandler(OnSelectDeadline_Click));
            MenuItem deleteDeadline = new MenuItem("기한 설정 제거", new EventHandler(OnDeleteDeadline_Click));
            deadlineMenu.MenuItems.Add(todayDeadline);
            deadlineMenu.MenuItems.Add(tomorrowDeadline);
            deadlineMenu.MenuItems.Add(nextWeekDeadline);
            deadlineMenu.MenuItems.Add(selectDeadline);
            deadlineMenu.MenuItems.Add(deleteDeadline);

            int px = splitContainer1.SplitterDistance + splitContainer2.SplitterDistance + 60;
            int py = 142;
            deadlineMenu.Show(this, new Point(px, py));
        }

        private void OnTodayDeadline_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;

            m_Selected_Item.TD_DataCell.DC_deadlineType = 1;
            m_Selected_Item.TD_DataCell.DC_deadlineTime = new DateTime(dt.Year, dt.Month, dt.Day, 22, 00, 00);

            Send_Log_Message("1>MainFrame::OnTodayDeadline_Click -> Modify Today Planned!!");
            m_Controller.Perform_Modify_Planned(m_Selected_Item.TD_DataCell);
        }

        private void OnTomorrowDeadline_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            dt = dt.AddDays(1);

            m_Selected_Item.TD_DataCell.DC_deadlineType = 2;
            m_Selected_Item.TD_DataCell.DC_deadlineTime = new DateTime(dt.Year, dt.Month, dt.Day, 22, 00, 00);

            Send_Log_Message("1>MainFrame::OnTomorrowDeadline_Click -> Modify Tomorrow Planned!!");
            m_Controller.Perform_Modify_Planned(m_Selected_Item.TD_DataCell);
        }

        private void OnNextWeekDeadline_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            dt = dt.AddDays(8 - (int)dt.DayOfWeek);

            m_Selected_Item.TD_DataCell.DC_deadlineType = 3;
            m_Selected_Item.TD_DataCell.DC_deadlineTime = new DateTime(dt.Year, dt.Month, dt.Day, 22, 00, 00);

            Send_Log_Message("1>MainFrame::OnNextWeekDeadline_Click -> Modify NextWeek Planned!!");
            m_Controller.Perform_Modify_Planned(m_Selected_Item.TD_DataCell);
        }

        private void OnSelectDeadline_Click(object sender, EventArgs e)
        {
            DateTimePickerForm calendar = new DateTimePickerForm();
            calendar.ShowDialog();

            DateTime dt = calendar.SelectedDateTime;

            if (!calendar.IsSelected || calendar.SelectedDateTime == default)
            {
                Send_Log_Message("1>MainFrame::OnSelectDeadline_Click -> Modify Planned Canceled");
                return;
            }

            if (dt.Hour == 0 && dt.Minute == 0 && dt.Second == 0) // 시간을 입력하지 않을때
            {
                dt = new DateTime(dt.Year, dt.Month, dt.Day, 22, 00, 00);
            }

            calendar.IsSelected = false;

            m_Selected_Item.TD_DataCell.DC_deadlineType = 4;
            m_Selected_Item.TD_DataCell.DC_deadlineTime = dt;

            Send_Log_Message("1>MainFrame::OnSelectDeadline_Click -> Modify Selected Day Planned!!");
            m_Controller.Perform_Modify_Planned(m_Selected_Item.TD_DataCell);
        }

        private void OnDeleteDeadline_Click(object sender, EventArgs e)
        {
            m_Selected_Item.TD_DataCell.DC_deadlineType = 0;
            m_Selected_Item.TD_DataCell.DC_deadlineTime = default;

            Send_Log_Message("1>MainFrame::OnDeleteDeadline_Click -> Delete Planned");
            m_Controller.Perform_Modify_Planned(m_Selected_Item.TD_DataCell);

            if (m_Selected_Item.TD_DataCell.DC_repeatType > 0) // 반복이 되어 있을때
            {
                Send_Log_Message("1>MainFrame::OnDeleteDeadline_Click -> There is Repeat, it is Deleted");
                OnDeleteRepeat_Click(this, new EventArgs());
            }
        }

        // -------------------------------------------------
        // 상세창 - 반복 메뉴
        // -------------------------------------------------
        private void roundLabel4_MouseEnter(object sender, EventArgs e)
        {
            roundLabel4.BackColor = PSEUDO_HIGHLIGHT_COLOR;
        }

        private void roundLabel4_MouseLeave(object sender, EventArgs e)
        {
            roundLabel4.BackColor = m_Selected_Item.TD_DataCell.DC_repeatType > 0 ? PSEUDO_SELECTED_COLOR : COLOR_DETAIL_WINDOW_BACK_COLOR;
        }

        private void roundLabel4_Click(object sender, MouseEventArgs e)
        {
            roundLabel4.Focus();
            if (e.Button != MouseButtons.Left) return;

            ContextMenu repeatMenu = new ContextMenu();
            MenuItem everyDayRepeat = new MenuItem("매일", new EventHandler(OnEveryDayRepeat_Click));
            MenuItem workingDayRepeat = new MenuItem("평일", new EventHandler(OnWorkingDayRepeat_Click));
            MenuItem everyWeekRepeat = new MenuItem("매주", new EventHandler(OnEveryWeekRepeat_Click));
            MenuItem everyMonthRepeat = new MenuItem("매월", new EventHandler(OnEveryMonthRepeat_Click));
            MenuItem everyYearRepeat = new MenuItem("매년", new EventHandler(OnEveryYearRepeat_Click));
            MenuItem deleteRepeat = new MenuItem("반복 제거", new EventHandler(OnDeleteRepeat_Click));
            repeatMenu.MenuItems.Add(everyDayRepeat);
            repeatMenu.MenuItems.Add(workingDayRepeat);
            repeatMenu.MenuItems.Add(everyWeekRepeat);
            repeatMenu.MenuItems.Add(everyMonthRepeat);
            repeatMenu.MenuItems.Add(everyYearRepeat);
            repeatMenu.MenuItems.Add(deleteRepeat);

            int px = splitContainer1.SplitterDistance + splitContainer2.SplitterDistance + 60;
            int py = 177;
            repeatMenu.Show(this, new Point(px, py));
        }

        private void OnEveryDayRepeat_Click(object sender, EventArgs e)
        {
            Repeat_EveryDay(m_Selected_Item.TD_DataCell);
        }

        private void Repeat_EveryDay(CDataCell data)
        {
            DateTime dt = DateTime.Now;
            dt = dt.AddDays(1);
            m_Controller.Perform_Modify_Repeat(data, 1, new DateTime(dt.Year, dt.Month, dt.Day, 09, 00, 00));
            if (data.DC_deadlineType == 0) // 기한설정이 되어 있지 않을때 오늘까지
            {
                OnTodayDeadline_Click(this, new EventArgs());
            }
        }

        private void OnWorkingDayRepeat_Click(object sender, EventArgs e)
        {
            Repeat_WorkingDay(m_Selected_Item.TD_DataCell);
        }

        private void Repeat_WorkingDay(CDataCell data)
        {
            DateTime dt = DateTime.Now;
            DayOfWeek dw = dt.DayOfWeek;
            switch (dw)
            {
                case DayOfWeek.Monday:
                    dt = dt.AddDays(1);
                    break;
                case DayOfWeek.Tuesday:
                    dt = dt.AddDays(1);
                    break;
                case DayOfWeek.Wednesday:
                    dt = dt.AddDays(1);
                    break;
                case DayOfWeek.Thursday:
                    dt = dt.AddDays(1);
                    break;
                case DayOfWeek.Friday:
                    dt = dt.AddDays(3);
                    break;
                case DayOfWeek.Saturday:
                    dt = dt.AddDays(2);
                    break;
                case DayOfWeek.Sunday:
                    dt = dt.AddDays(1);
                    break;
            }
            m_Controller.Perform_Modify_Repeat(data, 2, new DateTime(dt.Year, dt.Month, dt.Day, 09, 00, 00));
            if (data.DC_deadlineType == 0) // 기한설정이 되어 있지 않을때 오늘까지
            {
                OnTodayDeadline_Click(this, new EventArgs());
            }
        }

        private void OnEveryWeekRepeat_Click(object sender, EventArgs e)
        {
            Repeat_EveryWeek(m_Selected_Item.TD_DataCell);
        }

        private void Repeat_EveryWeek(CDataCell data)
        {
            DateTime dt = DateTime.Now;
            dt = dt.AddDays(7);
            m_Controller.Perform_Modify_Repeat(data, 3, new DateTime(dt.Year, dt.Month, dt.Day, 09, 00, 00));
            if (data.DC_deadlineType == 0) // 기한설정이 되어 있지 않을때 오늘까지
            {
                OnTodayDeadline_Click(this, new EventArgs());
            }
        }

        private void OnEveryMonthRepeat_Click(object sender, EventArgs e)
        {
            Repeat_EveryMonth(m_Selected_Item.TD_DataCell);
        }

        private void Repeat_EveryMonth(CDataCell data)
        {
            DateTime dt = DateTime.Now;
            dt = dt.AddMonths(1); // 매달 말일 계산 필요 - 28/29/30/31일 경우
            m_Controller.Perform_Modify_Repeat(data, 4, new DateTime(dt.Year, dt.Month, dt.Day, 09, 00, 00));
            if (data.DC_deadlineType == 0) // 기한설정이 되어 있지 않을때 오늘까지
            {
                OnTodayDeadline_Click(this, new EventArgs());
            }
        }

        private void OnEveryYearRepeat_Click(object sender, EventArgs e)
        {
            Repeat_EveryYear(m_Selected_Item.TD_DataCell);
        }

        private void Repeat_EveryYear(CDataCell data)
        {
            DateTime dt = DateTime.Now;
            dt = dt.AddYears(1);  // 윤년 계산 필요 2월29일
            m_Controller.Perform_Modify_Repeat(data, 5, new DateTime(dt.Year, dt.Month, dt.Day, 09, 00, 00));
            if (data.DC_deadlineType == 0) // 기한설정이 되어 있지 않을때 오늘까지
            {
                OnTodayDeadline_Click(this, new EventArgs());
            }
        }

        private void OnDeleteRepeat_Click(object sender, EventArgs e)
        {
            m_Controller.Perform_Modify_Repeat(m_Selected_Item.TD_DataCell, 0, default);
        }

        // -------------------------------------------------------
        // 상세창 - upArrow
        // -------------------------------------------------------
        private void upArrow_MouseEnter(object sender, EventArgs e)
        {
            upArrow.BackColor = PSEUDO_HIGHLIGHT_COLOR;
        }

        private void upArrow_MouseLeave(object sender, EventArgs e)
        {
            upArrow.BackColor = COLOR_DETAIL_WINDOW_BACK_COLOR;
        }

        private void upArrow_Click(object sender, EventArgs e)
        {
            upArrow.Focus();
            Send_Log_Message("1>MainFrame::upArrow_Click -> Task Move Up!");
            m_Controller.Perform_Task_Move_Up(m_Selected_Item.TD_DataCell);
        }

        // -------------------------------------------------------
        // 상세창 - downArrow
        // -------------------------------------------------------
        private void downArrow_MouseEnter(object sender, EventArgs e)
        {
            downArrow.BackColor = PSEUDO_HIGHLIGHT_COLOR;
        }

        private void downArrow_MouseLeave(object sender, EventArgs e)
        {
            downArrow.BackColor = COLOR_DETAIL_WINDOW_BACK_COLOR;
        }

        private void downArrow_Click(object sender, EventArgs e)
        {
            downArrow.Focus();
            Send_Log_Message("1>MainFrame::downArrow_Click -> Task Move Down!");
            m_Controller.Perform_Task_Move_Down(m_Selected_Item.TD_DataCell);
        }

        // -------------------------------------------------------------
        // 스프릿컨테이너 이벤트
        //-------------------------------------------------------------
        private void splitContainer1_MouseDown(object sender, MouseEventArgs e)
        {
            splitContainer1.IsSplitterFixed = true;
        }

        private void splitContainer1_MouseUp(object sender, MouseEventArgs e)
        {
            splitContainer1.IsSplitterFixed = false;
        }

        private void splitContainer1_MouseMove(object sender, MouseEventArgs e)
        {
            if (splitContainer1.IsSplitterFixed)
            {
                if (e.Button.Equals(MouseButtons.Left))
                {
                    if (splitContainer1.Orientation.Equals(Orientation.Vertical))
                    {
                        if (e.X > 0 && e.X < (splitContainer1.Width))
                        {
                            splitContainer1.SplitterDistance = e.X;
                            Update_Display();
                        }
                    }
                }
                else
                {
                    splitContainer1.IsSplitterFixed = false;
                }
            }
        }

        //--------------------------------------------------------------
        // 계정 및 옵션 처리
        //--------------------------------------------------------------
        private void labelUserName_MouseEnter(object sender, EventArgs e)
        {
            labelUserName.BackColor = PSEUDO_HIGHLIGHT_COLOR;
        }

        private void labelUserName_MouseLeave(object sender, EventArgs e)
        {
            labelUserName.BackColor = PSEUDO_BACK_COLOR;
        }

        private void labelUserName_MouseClick(object sender, MouseEventArgs e)
        {
            loginSettingForm.StartPosition = FormStartPosition.CenterParent;
            loginSettingForm.ShowDialog();

            Change_ColorTheme();

            if (loginSettingForm.UserName.Length != 0)
            {
                labelUserName.Text = "      사용자 (" + loginSettingForm.UserName + ")";
            }
        }

        //--------------------------------------------------------------
        // 태스크 드래그 앤 드롭 - Target
        //--------------------------------------------------------------
        private void TodoItem_DragEnter(object sender, DragEventArgs e)
        {
            //Console.WriteLine("TodoItem_DragEnter");
            if (e.Data.GetDataPresent(typeof(Todo_Item)))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void TodoItem_DragDrop(object sender, DragEventArgs e)
        {
            Todo_Item data = null;
            if (e.Data.GetDataPresent(typeof(Todo_Item)))
            {
                data = e.Data.GetData(typeof(Todo_Item)) as Todo_Item;
                //Console.WriteLine("TodoItem_DragDrop -> source : " + data.TD_title);
            }
            
            Point p = flowLayoutPanel2.PointToClient(new Point(e.X, e.Y));
            Todo_Item item = (Todo_Item)flowLayoutPanel2.GetChildAtPoint(p);

            if (data.TD_title == item.TD_title)
            {
                //Console.WriteLine("TodoItem_DragDrop -> Same task can't move");
                return;
            }

            if (item.TD_complete)
            {
                //Console.WriteLine("TodoItem_DragDrop -> Can't move over Complete Task");
                return;
            }
            
            Send_Log_Message("1>MainFrame::TodoItem_DragDrop -> Source : "
                             + data.TD_DataCell.DC_title
                             + " Target : "
                             + item.TD_DataCell.DC_title);
            m_Controller.Perform_Task_Move_To(data.TD_DataCell, item.TD_DataCell);
        }

        //--------------------------------------------------------------
        // 메뉴 리스트 드래그 앤 드롭 - Target
        //--------------------------------------------------------------
        private void TwoLineList_DragEnter(object sender, DragEventArgs e)
        {
            //Console.WriteLine("TwoLineList_DragEnter");
            if (e.Data.GetDataPresent(typeof(Todo_Item)))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void TwoLineList_DragDrop(object sender, DragEventArgs e)
        {
            //Console.WriteLine("TwoLineList_DragDrop");
            if (e.Data.GetDataPresent(typeof(Todo_Item)))
            {
                var item = e.Data.GetData(typeof(Todo_Item)) as Todo_Item;
                //Console.WriteLine("TwoLineList_DragDrop -> source : " + item.TD_title);
            }
            TwoLineList sd = (TwoLineList)sender;
            //Console.WriteLine("TwoLineList_DragDrop -> target : " + sd.PrimaryText);

            if (sd.PrimaryText == m_Selected_Menu.PrimaryText)
            {
                Send_Log_Message("Warning>MainFrame::TwoLineList_DragDrop -> Can't transfer item as same list");
                return;
            }

            Send_Log_Message("1>MainFrame::TwoLineList_DragDrop -> Transfer Item Click!! : from "
                             + m_Selected_Item.TD_DataCell.DC_listName
                             + " to "
                             + sd.PrimaryText);

            m_Controller.Perform_Trasnfer_Task(m_Selected_Item.TD_DataCell, sd.PrimaryText);
        }

        // ---------------------------------------------------------------------------
        // 인쇄하기
        // ---------------------------------------------------------------------------
        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            m_printCounter = 0;
            m_printPageNo = 1;
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int start_X = 10;
            int start_Y = 140;
            int lineHeight = 30;
            //int printLineNo = 0;

            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;

            //e.PageSettings.PaperSize = new System.Drawing.Printing.PaperSize("A4", 850, 1100);
            float pageWidth = e.PageSettings.PrintableArea.Width;
            float pageHeight = e.PageSettings.PrintableArea.Height - 50;
            Font font = new Font("Arial", 14, FontStyle.Regular);
            float fontHeight = font.GetHeight();

            if (m_printPageNo == 1)
            {
                //RectangleF drawRect = new RectangleF((float)(startWidth + width), (float)startHeight, (float)width1, avgHeight);
                //e.Graphics.DrawRectangle(Pens.Black, (float)(startWidth + width), (float)startHeight, (float)width1, avgHeight);
                //e.Graphics.DrawString("목록 및 할일 인쇄", new Font("Arial", 20, FontStyle.Bold), Brushes.Black, drawRect, sf);
                
                e.Graphics.DrawString("목록 및 할일 인쇄", new Font("Arial", 20, FontStyle.Bold), Brushes.Black, 10, 40);
                e.Graphics.DrawString("인쇄일 : " + DateTime.Now.ToString("yyyy/MM/dd"), new Font("Arial", 13), Brushes.Black, 10, 80);
                e.Graphics.DrawString("페이지번호 : " + m_printPageNo, new Font("Arial", 13), Brushes.Black, 10, 100);

                e.Graphics.DrawString(m_Selected_Menu.PrimaryText, new Font("Arial", 16, FontStyle.Bold), Brushes.Black, start_X, start_Y);
                start_X += 50;
                start_Y += lineHeight;
            }

            for (int i = m_printCounter; i < m_Task.Count; i++)
            {
                e.Graphics.DrawString(m_Task[i].TD_title, new Font("Arial", 14, FontStyle.Regular), Brushes.Black, start_X, start_Y);
                m_printCounter++;

                /*
                start_Y += lineHeight;
                printLineNo++;
                if (printLineNo % 35 == 0) // 한페이지당 35줄 인쇄
                {
                    e.HasMorePages = true;
                    m_printPageNo++;
                    return;
                }
                else
                {
                    e.HasMorePages = false;
                }
                */

                start_Y += (int)fontHeight;
                if (start_Y >= pageHeight)
                {
                    e.HasMorePages = true;
                    m_printPageNo++;
                    return;
                }
                else
                {
                    e.HasMorePages = false;
                }
            }
        }

        // ---------------------------------------------------------------------------
        // 타이머 처리
        // ---------------------------------------------------------------------------
        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            Text = WINDOW_CAPTION + " [" + dt.ToString("yyyy-MM-dd(ddd)") + "]";

            AlarmCheck();

            timer1.Interval = 60000;
        }

        private void Delay(int ms)
        {
            DateTime dateTimeNow = DateTime.Now;
            TimeSpan duration = new TimeSpan(0, 0, 0, 0, ms);
            DateTime dateTimeAdd = dateTimeNow.Add(duration);
            while (dateTimeAdd >= dateTimeNow)
            {
                System.Windows.Forms.Application.DoEvents();
                dateTimeNow = DateTime.Now;
            }
            return;
        }
    }
}

/*
 M   : 월. 10 이하는 한자리
MM  : 2자리 월
MMM : 축약형 월 이름 (예: APR)
d   : 일. 10 이하는 한자리
dd  : 2자리 일자
ddd : 축약형 요일 이름 (예: Mon)
yy  : 2자리 연도 
yyyy: 4자리 연도 
h   : 시간 (12시간, 10 이하 한자리)
hh  : 2자리 시간 (12시간)
H   : 시간 (24시간, 10 이하 한자리)
HH  : 2자리 시간 (24시간)
m   : 분 (10 이하 한자리)
mm  : 2자리 분
s   : 초 (10 이하 한자리)
ss  : 2자리 초
tt  : AM / PM

public enum DayOfWeek
{
    Sunday = 0,
    Monday = 1,
    Tuesday = 2,
    Wednesday = 3,
    Thursday = 4,
    Friday = 5,
    Saturday = 6
}

*/

/*
FlowPanel.Items.Clear()
for(int i = (currentPage-1) * thumbsPerPage; i < (currentPage * thumbsPerPage) - 1; i++)
   FlowPanel.Controls.Add(Pedits[i])
*/

//Stopwatch sw = new Stopwatch();
//sw.Start();
//sw.Stop();
//Console.WriteLine(sw.ElapsedMilliseconds.ToString() + "ms - nor");
//sw.Reset();