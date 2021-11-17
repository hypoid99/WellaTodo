// copyright honeysoft v0.14 -> v0.7 -> v0.8

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

namespace WellaTodo
{
    public delegate void TwoLineList_Event(object sender, EventArgs e);
    public delegate void TodoItemList_Event(object sender, EventArgs e);
    
    public partial class MainFrame : Form, IView, IModelObserver
    {
        public event ViewHandler<IView> Changed_View_Event;

        static readonly string WINDOW_CAPTION = "Wella Todo v0.8";
        static readonly int WINDOW_WIDTH = 1000;
        static readonly int WINDOW_HEIGHT = 500;
        static readonly int MENU_WINDOW_WIDTH = 200;
        static readonly int DETAIL_WINDOW_WIDTH = 260;
        static readonly int DETAIL_WINDOW_X1 = 5;
        static readonly int HEADER_HEIGHT = 50;
        static readonly int TAIL_HEIGHT = 50;
        static readonly int TASK_WIDTH_GAP = 25;

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

        static readonly string FONT_NAME = "맑은고딕";
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
        List<CDataCell> m_Data = new List<CDataCell>();
        List<TwoLineList> m_ListName = new List<TwoLineList>();
        List<string> m_listName_Data = new List<string>();
        List<Todo_Item> m_Task = new List<Todo_Item>();

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

        bool isDetailWindowOpen = false;
        bool isTextboxClicked = false;
        bool isTextbox_AddList_Clicked = false;

        int m_selected_position;
        int m_selected_menu = (int)MenuList.TODO_ITEM_MENU; // 초기 작업 메뉴 설정
        string m_selected_listname;
        int m_currentPage = 1;
        int m_thumbsPerPage = 20;

        public MainFrame()
        {
            InitializeComponent();

            DoubleBuffered = true;
        }

        public void SetController(MainController controller)
        {
            m_Controller = controller;
        }

        private void MainFrame_Load(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            Size = new Size(WINDOW_WIDTH, WINDOW_HEIGHT);
            Text = WINDOW_CAPTION + " [" + dt.ToString("yyyy-MM-dd(ddd) tt h:mm") + "]";

            m_Data = m_Controller.Get_Model().GetDataCollection();

            Load_Data_File();

            Initiate_View();
            SetColorTheme();
            Initiate_MenuList();

            timer1.Interval = 3000;
            timer1.Enabled = true;
        }

        private void MainFrame_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("저장할까요?", WINDOW_CAPTION, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Save_Data_File();
            }
        }

        private void MainFrame_Paint(object sender, PaintEventArgs e)
        {
            Repaint();
        }

        private void MainFrame_Resize(object sender, EventArgs e)
        {
            Repaint();
        }

        private void Initiate_View()
        {
            splitContainer1.SplitterDistance = MENU_WINDOW_WIDTH;
            splitContainer1.Panel1MinSize = 100;
            splitContainer1.Panel2MinSize = 200;
            splitContainer1.Panel1.BackColor = PSEUDO_BACK_COLOR;
            splitContainer1.Panel2.BackColor = PSEUDO_BACK_COLOR;

            labelUserName.Image = ICON_ACCOUNT;
            labelUserName.ImageAlign = ContentAlignment.MiddleLeft;
            labelUserName.Font = new Font(FONT_NAME, FONT_SIZE_TEXT);
            labelUserName.Text = "      사용자 계정 / 셋팅";
            labelUserName.Location = new Point(0, 0);
            labelUserName.Size = new Size(splitContainer1.Panel1.Width, HEADER_HEIGHT);
            labelUserName.BackColor = PSEUDO_BACK_COLOR;

            textBox_AddList.Location = new Point(10, splitContainer1.Panel1.Height - 35);
            textBox_AddList.Size = new Size(flowLayoutPanel_Menulist.Width - 15, 25);
            textBox_AddList.BackColor = PSEUDO_TEXTBOX_BACK_COLOR;
            textBox_AddList.Text = "+ 새 목록 추가";

            splitContainer2.SplitterDistance = splitContainer2.Width;
            splitContainer2.IsSplitterFixed = true;
            splitContainer2.Location = new Point(0, 0);
            splitContainer2.Size = new Size(splitContainer1.Panel2.Width, splitContainer1.Panel2.Height - TAIL_HEIGHT);
            splitContainer2.Panel1.BackColor = PSEUDO_BACK_COLOR;

            label_ListName.Image = ICON_HOME;
            label_ListName.ImageAlign = ContentAlignment.MiddleLeft;
            label_ListName.Font = new Font(FONT_NAME, FONT_SIZE_TITLE);
            label_ListName.Location = new Point(0,0);
            label_ListName.Size = new Size(splitContainer2.Panel1.Width, HEADER_HEIGHT);
            label_ListName.BackColor = PSEUDO_HIGHLIGHT_COLOR;

            // 태스크 항목
            flowLayoutPanel2.AutoScroll = false;
            flowLayoutPanel2.HorizontalScroll.Maximum = 0;
            flowLayoutPanel2.HorizontalScroll.Enabled = false;
            flowLayoutPanel2.HorizontalScroll.Visible = false;
            flowLayoutPanel2.AutoScroll = true;

            flowLayoutPanel2.MouseWheel += new MouseEventHandler(flowLayoutPanel2_MouseWheel);

            flowLayoutPanel2.Location = new Point(0, label_ListName.Height);
            flowLayoutPanel2.Size = new Size(splitContainer2.Panel1.Width, splitContainer2.Panel1.Height - TAIL_HEIGHT);

            textBox2.Location = new Point(10, splitContainer1.Panel2.Height - 35);
            textBox2.Size = new Size(splitContainer1.Panel2.Width - 25, 25);
            textBox2.BackColor = PSEUDO_TEXTBOX_BACK_COLOR;
            textBox2.Text = "+ 작업 추가";

            // 상세창
            roundCheckbox1.CheckedChanged += new EventHandler(roundCheckbox1_CheckedChanged);
            roundCheckbox1.MouseEnter += new EventHandler(roundCheckbox1_MouseEnter);
            roundCheckbox1.MouseLeave += new EventHandler(roundCheckbox1_MouseLeave);
            roundCheckbox1.MouseClick += new MouseEventHandler(roundCheckbox1_MouseClick);
            roundCheckbox1.Location = new Point(DETAIL_WINDOW_X1 + 2, 5);
            roundCheckbox1.Size = new Size(25, 25);
            splitContainer2.Panel2.Controls.Add(roundCheckbox1);

            textBox3.Location = new Point(DETAIL_WINDOW_X1 + 30, 8);
            textBox3.Size = new Size(DETAIL_WINDOW_WIDTH - 78, 25);
            textBox3.BackColor = PSEUDO_TEXTBOX_BACK_COLOR;

            starCheckbox1.CheckedChanged += new EventHandler(starCheckbox1_CheckedChanged);
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

            textBox1.Multiline = true;
            textBox1.Location = new Point(DETAIL_WINDOW_X1 + 5, 185);
            textBox1.Size = new Size(DETAIL_WINDOW_WIDTH - 25, 130);

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
        }

        //--------------------------------------------------------------
        // Repaint
        //--------------------------------------------------------------
        private void Repaint()
        {
            //Rectangle rc = ClientRectangle;
            //Console.WriteLine(">ClientRectangle W[{0}] H[{1}]", rc.Width, rc.Height);
            //Console.WriteLine("splitContainer1.SplitterDistance [{0}]", splitContainer1.SplitterDistance);
            //Console.WriteLine("splitContainer1.Size W[{0}] H[{1}]", splitContainer1.Width, splitContainer1.Height);
            //Console.WriteLine("splitContainer1.Panel1.Width [{0}]", splitContainer1.Panel1.Width);
            //Console.WriteLine("splitContainer1.Panel2.Width [{0}]", splitContainer1.Panel2.Width);

            flowLayoutPanel_Menulist.Width = splitContainer1.SplitterDistance;
            flowLayoutPanel_Menulist.Height = splitContainer1.Panel1.Height - labelUserName.Height - TAIL_HEIGHT;
            //Console.WriteLine("flowLayoutPanel_Menulist.Width [{0}]", flowLayoutPanel_Menulist.Width);

            labelUserName.Width = flowLayoutPanel_Menulist.Width;

            textBox_AddList.Location = new Point(10, splitContainer1.Panel1.Height - 35);
            textBox_AddList.Size = new Size(flowLayoutPanel_Menulist.Width - 15, 25);

            foreach (TwoLineList ctr in flowLayoutPanel_Menulist.Controls)
            {
                ctr.Width = flowLayoutPanel_Menulist.Width - 2;
            }

            splitContainer2.Size = new Size(splitContainer1.Panel2.Width, splitContainer1.Panel2.Height - TAIL_HEIGHT);

            //Console.WriteLine("splitContainer2.SplitterDistance [{0}]", splitContainer2.SplitterDistance);
            //Console.WriteLine("splitContainer2.Size W[{0}] H[{1}]", splitContainer2.Width, splitContainer2.Height);
            //Console.WriteLine("splitContainer2.Panel1.Width [{0}]", splitContainer2.Panel1.Width);
            //Console.WriteLine("splitContainer2.Panel2.Width [{0}]", splitContainer2.Panel2.Width);

            label_ListName.Size = new Size(splitContainer2.Panel1.Width, 50);
            flowLayoutPanel2.Size = new Size(splitContainer2.Panel1.Width, splitContainer2.Panel1.Height - TAIL_HEIGHT);
            //Console.WriteLine("flowLayoutPanel2.Width [{0}]", flowLayoutPanel2.Width);
            textBox2.Location = new Point(10, splitContainer1.Panel2.Height - 35);
            textBox2.Size = new Size(splitContainer1.Panel2.Width - 20, 25);

            if (isDetailWindowOpen)
            {
                int width = splitContainer2.Width - DETAIL_WINDOW_WIDTH;
                splitContainer2.SplitterDistance = width < 0 ? 0 : width;
            }

            Set_TodoItem_Width();
        }

        // -------------------------------------------------------------
        // 스프릿컨테이너-1 이벤트
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
                            Repaint();
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
        // 할일 항목 폭 맞추기 등 필요한 메서드
        //--------------------------------------------------------------
        private void Set_TodoItem_Width()
        {
            foreach (Todo_Item item in flowLayoutPanel2.Controls)
            {
                item.Width = flowLayoutPanel2.Width - TASK_WIDTH_GAP;
                //item.Width = flowLayoutPanel2.VerticalScroll.Visible
                //    ? flowLayoutPanel2.Width - 8 - SystemInformation.VerticalScrollBarWidth
                //    : flowLayoutPanel2.Width - 8;
            }
            Display_Data();
        }

        private void SetColorTheme()
        {
            switch (loginSettingForm.ColorTheme)
            {
                case 1: // white
                    COLOR_DETAIL_WINDOW_BACK_COLOR = Color.White;
                    break;
                case 2: // papayaWhip
                    COLOR_DETAIL_WINDOW_BACK_COLOR = Color.PapayaWhip;
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

        private void OpenDetailWindow()
        {
            splitContainer2.SplitterDistance = splitContainer2.Width - DETAIL_WINDOW_WIDTH;
            flowLayoutPanel2.Width = splitContainer2.Width - DETAIL_WINDOW_WIDTH;
            label_ListName.Width = splitContainer2.Width - DETAIL_WINDOW_WIDTH;
            isDetailWindowOpen = true;
        }

        private void CloseDetailWindow()
        {
            splitContainer2.SplitterDistance = splitContainer2.Width;
            flowLayoutPanel2.Width = splitContainer2.Width;
            label_ListName.Width = splitContainer2.Width;
            isDetailWindowOpen = false;
        }

        private void Display_Data()
        {
            int pos;
            string txt;
            if (outputForm.Visible)
            {
                pos = 0;
                foreach (CDataCell data in m_Data)
                {
                    txt = ">Data DC:[" + pos + "] " + data.DC_listName + "--" + data.DC_title + "\r\n";
                    outputForm.TextBoxString = txt;
                    pos++;
                }
                outputForm.TextBoxString = ">Data Selected Position:[" + m_selected_position + "]--" + m_Data[m_selected_position].DC_title + "\r\n";
                
                foreach (TwoLineList data in m_ListName)
                {
                    txt = ">m_ListName: " + data.ToString () + "\r\n";
                    outputForm.TextBoxString = txt;
                }

                txt = ">Selected Menu : [" + m_selected_menu.ToString() + "]----------------------\r\n";
                outputForm.TextBoxString = txt;
            }
        }

        public void ModelObserver_Event_method(IModel m, ModelEventArgs e)
        {
            Console.WriteLine(">MainFrame::ModelObserver_Event_method");
            // Model에서 온 데이타로 View를 업데이트
        }

        private void Invoke_View_Event()
        {
            Console.WriteLine(">MainFrame::Invoke_View_Event");
            try
            {
                Changed_View_Event.Invoke(this, new ViewEventArgs(1));
            }
            catch (Exception)
            {
                MessageBox.Show("Please enter a valid number", WINDOW_CAPTION);
            }
        }

        //--------------------------------------------------------------
        // 메뉴리스트 초기 데이타 로딩
        //--------------------------------------------------------------
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

            m_ListName.Add(twolinelist_Menu5); // "작업" 리스트를 등록한다
            
            foreach (string list_name in m_listName_Data)
            {
                if (list_name != "작업")
                {
                    TwoLineList list;
                    list = new TwoLineList(new Bitmap(ICON_LIST), list_name, "", "");
                    list.TwoLineList_Click -= new TwoLineList_Event(TwoLineList_Click);
                    list.TwoLineList_Click += new TwoLineList_Event(TwoLineList_Click);

                    m_ListName.Add(list);
                    flowLayoutPanel_Menulist.Controls.Add(list);
                }
            }

            twolinelist_Menu5.IsSelected = true;

            foreach (TwoLineList ctr in flowLayoutPanel_Menulist.Controls)
            {
                ctr.Width = flowLayoutPanel_Menulist.Width - 2;
            }

            // 할일 항목 초기 표시
            Menu_Task();
            Set_TodoItem_Width();
            Update_Metadata();
        }

        //--------------------------------------------------------------
        // 할일 파일 로딩
        //--------------------------------------------------------------
        private void Load_Data_File()
        {
            Stream rs = new FileStream("task.dat", FileMode.Open);
            BinaryFormatter deserializer = new BinaryFormatter();

            List<CDataCell> todo_data = (List<CDataCell>)deserializer.Deserialize(rs);
            foreach (CDataCell dt in todo_data)
            {
                m_Data.Insert(m_Data.Count, dt);
            }
            rs.Close();
            
            Stream rs_list = new FileStream("list.dat", FileMode.Open);
            BinaryFormatter deserializer_list = new BinaryFormatter();
            List<string> list_name = (List<string>)deserializer_list.Deserialize(rs_list);

            m_listName_Data.Clear();
            foreach (string list in list_name)
            {
                m_listName_Data.Add(list);
            }
            rs_list.Close();
            
        }

        //--------------------------------------------------------------
        // 할일 파일 세이브
        //--------------------------------------------------------------
        private void Save_Data_File()
        {
            
            Stream ws = new FileStream("task.dat", FileMode.Create);
            BinaryFormatter serializer = new BinaryFormatter();

            serializer.Serialize(ws, m_Data);
            ws.Close();
           
            Stream ws_list = new FileStream("list.dat", FileMode.Create);
            BinaryFormatter serializer_list = new BinaryFormatter();

            m_listName_Data.Clear();
            for (int i = 0; i < m_ListName.Count; i++)  // 작업 이후 리스트 이름을 저장한다
            {
                TwoLineList item = m_ListName[i];
                m_listName_Data.Add(item.PrimaryText);
            }
            serializer_list.Serialize(ws_list, m_listName_Data);
            ws_list.Close();
            
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

            if (loginSettingForm.IsSaveClose)
            {
                if (MessageBox.Show("저장할까요?", WINDOW_CAPTION, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Save_Data_File();
                }
            }

            SetColorTheme();

            if (loginSettingForm.UserName.Length != 0)
            {
                labelUserName.Text = "      사용자 ("+ loginSettingForm.UserName + ")";
            }

        }


        //--------------------------------------------------------------
        // 메뉴리스트를 클릭했을때 처리
        //--------------------------------------------------------------
        private void TwoLineList_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;

            switch (me.Button)
            {
                case MouseButtons.Left:
                    Menulist_Left_Click(sender, me);
                    break;
                case MouseButtons.Right:
                    Menulist_Right_Click(sender, me);
                    break;
                case MouseButtons.Middle:
                    Menulist_Rename(sender, me);
                    break;
            }
        }

        private void Menulist_Left_Click(object sender, MouseEventArgs e)
        {
            TwoLineList sd = (TwoLineList)sender;

            CloseDetailWindow();
            foreach (TwoLineList item in flowLayoutPanel_Menulist.Controls)
            {
                item.IsSelected = false;
                if (item.Equals(sd))
                {
                    item.IsSelected = true;

                    switch (item.PrimaryText)
                    {
                        case "오늘 할 일":
                            m_selected_menu = (int)MenuList.MYTODAY_MENU;
                            Menu_MyToday();
                            break;
                        case "중요":
                            m_selected_menu = (int)MenuList.IMPORTANT_MENU;
                            Menu_Important();
                            break;
                        case "계획된 일정":
                            m_selected_menu = (int)MenuList.DEADLINE_MENU;
                            Menu_Planned();
                            break;
                        case "완료됨":
                            m_selected_menu = (int)MenuList.COMPLETE_MENU;
                            Menu_Completed();
                            break;
                        case "작업":
                            m_selected_menu = (int)MenuList.TODO_ITEM_MENU;
                            Menu_Task();
                            break;
                        default:
                            m_selected_menu = (int)MenuList.LIST_MENU;
                            Menu_List(item);
                            break;
                    }
                }
            }
            Set_TodoItem_Width();
        }

        private void Menulist_Right_Click(object sender, MouseEventArgs e)
        {
            TwoLineList sd = (TwoLineList)sender;

            foreach (TwoLineList item in flowLayoutPanel_Menulist.Controls)
            {
                item.IsSelected = false;
                if (item.Equals(sd))
                {
                    item.IsSelected = true;

                    switch (item.PrimaryText)
                    {
                        case "오늘 할 일":
                            m_selected_menu = (int)MenuList.MYTODAY_MENU;
                            break;
                        case "중요":
                            m_selected_menu = (int)MenuList.IMPORTANT_MENU;
                            break;
                        case "계획된 일정":
                            m_selected_menu = (int)MenuList.DEADLINE_MENU;
                            break;
                        case "완료됨":
                            m_selected_menu = (int)MenuList.COMPLETE_MENU;
                            break;
                        case "작업":
                            m_selected_menu = (int)MenuList.TODO_ITEM_MENU;
                            m_selected_listname = item.PrimaryText;
                            break;
                        default:
                            m_selected_menu = (int)MenuList.LIST_MENU;
                            m_selected_listname = item.PrimaryText;
                            break;
                    }
                }
            }

            ContextMenu menuListContextMenu = new ContextMenu();
            menuListContextMenu.Popup += new EventHandler(OnMenuListPopupEvent);

            MenuItem saveData = new MenuItem("데이터 저장", new EventHandler(OnSaveDataMenu_Click));
            MenuItem displayData = new MenuItem("데이터 표시", new EventHandler(OnDisplayDataMenu_Click));
            MenuItem renameList = new MenuItem("목록 이름바꾸기", new EventHandler(OnRenameMenuList_Click));
            MenuItem deleteList = new MenuItem("목록 삭제", new EventHandler(OnDeleteMenuList_Click));

            menuListContextMenu.MenuItems.Add(saveData);
            menuListContextMenu.MenuItems.Add(displayData);
            menuListContextMenu.MenuItems.Add("-");
            menuListContextMenu.MenuItems.Add(renameList);
            menuListContextMenu.MenuItems.Add(deleteList);

            int px = Control.MousePosition.X - Location.X;
            int py = Control.MousePosition.Y - Location.Y - 30;
            menuListContextMenu.Show(this, new Point(px, py));
        }

        private void OnMenuListPopupEvent(object sender, EventArgs e)
        {
            ContextMenu ctm = (ContextMenu)sender;

            if (m_selected_menu == (int)MenuList.LIST_MENU)
            {
                ctm.MenuItems[3].Enabled = true;
                ctm.MenuItems[4].Enabled = true;
            }
            else
            {
                ctm.MenuItems[3].Enabled = false;
                ctm.MenuItems[4].Enabled = false;
            }
            
        }

        private void OnSaveDataMenu_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("저장할까요?", WINDOW_CAPTION, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Save_Data_File();
            }
        }

        private void OnDisplayDataMenu_Click(object sender, EventArgs e)
        {
            outputForm.StartPosition = FormStartPosition.Manual;
            outputForm.Location = new Point(Location.X + (Width - outputForm.Width) / 2, Location.Y + (Height - outputForm.Height) / 2);

            if (outputForm.Visible)
                outputForm.Hide();
            else
                outputForm.Show();
            Display_Data();
        }

        private void OnRenameMenuList_Click(object sender, EventArgs e)
        {
            foreach (TwoLineList list in flowLayoutPanel_Menulist.Controls)
            {
                if (list.PrimaryText == m_selected_listname)
                {
                    list.RenamePrimaryText();
                    Menu_List(list);
                    break;
                }
            }
            
        }

        private void Menulist_Rename(object sender, MouseEventArgs e)
        {
            TwoLineList sd = (TwoLineList)sender;

            int pos = 0;
            foreach (CDataCell item in m_Data)
            {
                if (sd.PrimaryText == item.DC_listName)
                {
                    m_Data[pos].DC_listName = sd.PrimaryText_Renamed;
                }
                pos++;
            }
            
            pos = 0;
            for (int i = 0; i < m_listName_Data.Count; i++)
            {
                string item = m_listName_Data[i];
                if (sd.PrimaryText == item)
                {
                    m_listName_Data[pos] = sd.PrimaryText_Renamed;
                }
                pos++;
            }
            
            pos = 0;
            foreach (TwoLineList item in m_ListName)
            {
                if (sd.PrimaryText == item.PrimaryText)
                {
                    m_ListName[pos].PrimaryText = sd.PrimaryText_Renamed;
                }
                pos++;
            }

            Set_TodoItem_Width();
            Update_Metadata();
        }

        private void OnDeleteMenuList_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("목록을 삭제할까요?", WINDOW_CAPTION, MessageBoxButtons.YesNo) == DialogResult.No) return;

            TwoLineList before_list = null;
            foreach (TwoLineList list in flowLayoutPanel_Menulist.Controls)
            {
                if (list.PrimaryText == m_selected_listname)
                {
                    list.TwoLineList_Click -= new TwoLineList_Event(TwoLineList_Click);
                    flowLayoutPanel_Menulist.Controls.Remove(list);
                    list.Dispose();

                    m_ListName.Remove(list);

                    int pos = 0;
                    while (pos < m_Data.Count)
                    {
                        CDataCell dc = m_Data[pos];
                        if (dc.DC_listName == m_selected_listname)
                        {
                            m_Data.RemoveAt(pos);
                        }
                        else
                        {
                            ++pos;
                        }
                    }
                    break;
                }
                else
                {
                    before_list = list;
                }
            }

            Menu_List(before_list);
            Set_TodoItem_Width();
            Update_Metadata();
        }

        //--------------------------------------------------------------
        // 메뉴 처리
        //--------------------------------------------------------------
        private void Menu_MyToday()
        {
            upArrow.Visible = false;
            downArrow.Visible = false;
            label_ListName.Image = ICON_SUNNY;
            label_ListName.Text = "   " + "오늘 할 일";
            m_selected_listname = "오늘 할 일";

            //Add_Task_To_Panel(from CDataCell dt in m_Data where dt.DC_myToday && !dt.DC_complete select dt);
            Add_Task_To_Panel_Paging(from CDataCell dt in m_Data where dt.DC_myToday && !dt.DC_complete select dt);
        }

        private void Menu_Important()
        {
            upArrow.Visible = false;
            downArrow.Visible = false;
            label_ListName.Image = ICON_GRADE;
            label_ListName.Text = "   " + "중요";
            m_selected_listname = "중요";

            //Add_Task_To_Panel(from CDataCell dt in m_Data where dt.DC_important && !dt.DC_complete select dt);
            Add_Task_To_Panel_Paging(from CDataCell dt in m_Data where dt.DC_important && !dt.DC_complete select dt);
        }

        private void Menu_Planned()
        {
            upArrow.Visible = false;
            downArrow.Visible = false;
            label_ListName.Image = ICON_EVENTNOTE;
            label_ListName.Text = "   " + "계획된 일정";
            m_selected_listname = "계획된 일정";

            //Add_Task_To_Panel(from CDataCell dt in m_Data where (dt.DC_myToday || dt.DC_deadlineType > 0 || dt.DC_repeatType > 0) && !dt.DC_complete select dt);
            Add_Task_To_Panel_Paging(from CDataCell dt in m_Data where (dt.DC_myToday || dt.DC_deadlineType > 0 || dt.DC_repeatType > 0) && !dt.DC_complete select dt);
        }

        private void Menu_Completed()
        {
            upArrow.Visible = false;
            downArrow.Visible = false;
            label_ListName.Image = ICON_CHECKCIRCLE;
            label_ListName.Text = "   " + "완료됨";
            m_selected_listname = "완료됨";

            //Add_Task_To_Panel(from CDataCell dt in m_Data where dt.DC_complete == true select dt);
            Add_Task_To_Panel_Paging(from CDataCell dt in m_Data where dt.DC_complete == true select dt);
        }

        private void Menu_Task()
        {
            upArrow.Visible = true;
            downArrow.Visible = true;
            label_ListName.Image = ICON_HOME;
            label_ListName.Text = "   " + "작업";
            m_selected_listname = "작업";

            //Add_Task_To_Panel(from CDataCell dt in m_Data where dt.DC_listName == "작업" select dt);
            Add_Task_To_Panel_Paging(from CDataCell dt in m_Data where dt.DC_listName == "작업" select dt);
        }

        private void Add_Task_To_Panel(IEnumerable<CDataCell> dataset)
        {
            flowLayoutPanel2.Controls.Clear();
            foreach (CDataCell data in dataset)
            {
                Todo_Item item = new Todo_Item(data);
                flowLayoutPanel2.Controls.Add(item);
                item.Width = flowLayoutPanel2.Width - TASK_WIDTH_GAP; 
                Change_TaskInfomationText(data);
                item.UserControl_Click -= new TodoItemList_Event(TodoItem_UserControl_Click);
                item.UserControl_Click += new TodoItemList_Event(TodoItem_UserControl_Click); // 이벤트 재구독 확인할 것
            }
            foreach (Todo_Item item in flowLayoutPanel2.Controls)  // 선택 항목 표기
            {
                if (item.TD_DataCell.Equals(m_Data[m_selected_position]))
                {
                    item.IsItemSelected = true;
                    item.Refresh();
                }
            }
        }

        private void Add_Task_To_Panel_Paging(IEnumerable<CDataCell> dataset)
        {
            m_Task.Clear();
            foreach (CDataCell data in dataset)
            {
                Todo_Item item = new Todo_Item(data);
                item.UserControl_Click -= new TodoItemList_Event(TodoItem_UserControl_Click);
                item.UserControl_Click += new TodoItemList_Event(TodoItem_UserControl_Click); // 이벤트 재구독 확인할 것
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
                Change_TaskInfomationText(m_Task[i].TD_DataCell);
            }
            flowLayoutPanel2.ResumeLayout();

            foreach (Todo_Item item in flowLayoutPanel2.Controls)  // 선택 항목 표기
            {
                if (item.TD_DataCell.Equals(m_Data[m_selected_position]))
                {
                    item.IsItemSelected = true;
                    item.Refresh();
                }
            }
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

        //--------------------------------------------------------------
        // 목록 추가하기 처리
        //--------------------------------------------------------------
        private void Add_NewList(string txt)
        {
            // 동일 이름의 목록 찾기 -> 발견시 뒷자리 번호 부여
            if (!Check_ListName(txt))
            {
                MessageBox.Show("목록명이 잘못되었거나 중복된 목록명입니다", WINDOW_CAPTION);
                return;
            }

            // 신규 목록 등록하기
            string listName = txt;
            TwoLineList list;
            list = new TwoLineList(new Bitmap(ICON_LIST), listName, "", "");
            list.TwoLineList_Click -= new TwoLineList_Event(TwoLineList_Click);
            list.TwoLineList_Click += new TwoLineList_Event(TwoLineList_Click);

            m_ListName.Add(list); // 이름 목록에 저장

            flowLayoutPanel_Menulist.Controls.Add(list); // 판넬 컨렉션에 저장
            foreach (TwoLineList item in flowLayoutPanel_Menulist.Controls)
            {
                item.Width = flowLayoutPanel_Menulist.VerticalScroll.Visible
                ? flowLayoutPanel_Menulist.Width - 2 - SystemInformation.VerticalScrollBarWidth
                : flowLayoutPanel_Menulist.Width - 2;

                item.IsSelected = false;
                if (item.Equals(list))
                {
                    item.IsSelected = true;
                }
            }

            m_selected_menu = (int)MenuList.LIST_MENU;
            Menu_List(list);
            Update_Metadata();
        }

        private bool Check_ListName(string txt)
        {
            if (txt == "오늘 할 일"
                || txt == "중요"
                || txt == "계획된 일정"
                || txt == "완료됨"
                || txt == "작업") return false;

            foreach (TwoLineList item in m_ListName)
            {
                if (item.PrimaryText == txt)
                {
                    return false;
                }
            }
            return true;
        }

        private void textBox_AddList_Enter(object sender, EventArgs e)
        {
            if (!isTextbox_AddList_Clicked) textBox_AddList.Text = "";
            isTextbox_AddList_Clicked = true;
        }

        private void textBox_AddList_Leave(object sender, EventArgs e)
        {
            if (isTextbox_AddList_Clicked)
            {
                if (textBox_AddList.Text.Trim().Length == 0)
                {
                    textBox_AddList.Text = "+ 새 목록 추가";
                    isTextbox_AddList_Clicked = false;
                }
            }
            else
            {
                textBox_AddList.Text = "+ 새 목록 추가";
                isTextbox_AddList_Clicked = false;
            }
        }

        private void textBox_AddList_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = false;
                e.SuppressKeyPress = false;
                if (textBox_AddList.Text.Trim().Length == 0) return;
                Add_NewList(textBox_AddList.Text);
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
        // 목록 리스트 처리
        //--------------------------------------------------------------
        private void Menu_List(TwoLineList list)
        {
            upArrow.Visible = true;
            downArrow.Visible = true;

            label_ListName.Image = ICON_LIST;
            label_ListName.Text = "   "+list.PrimaryText;
            m_selected_listname = list.PrimaryText;

            //Add_Task_To_Panel(from CDataCell dt in m_Data where dt.DC_listName == list.PrimaryText select dt);
            Add_Task_To_Panel_Paging(from CDataCell dt in m_Data where dt.DC_listName == list.PrimaryText select dt);
        }


        //--------------------------------------------------------------
        // 할일 항목 추가 -> 아무 메뉴에서 생성 못하게 할 것
        //--------------------------------------------------------------
        private void Add_Task(string text)
        {
            //m_Controller.performAddItem();
            DateTime dt = DateTime.Now;

            CDataCell dc = new CDataCell(m_selected_listname, text);  // DataCell 생성
            m_Data.Insert(0, dc);
            m_Data[0].DC_dateCreated = dt;

            Todo_Item item = new Todo_Item(dc);  // Task 생성
            flowLayoutPanel2.Controls.Add(item);
            flowLayoutPanel2.Controls.SetChildIndex(item, 0);
            item.UserControl_Click -= new TodoItemList_Event(TodoItem_UserControl_Click);
            item.UserControl_Click += new TodoItemList_Event(TodoItem_UserControl_Click);

            flowLayoutPanel2.VerticalScroll.Value = 0;

            CloseDetailWindow();
            switch (m_selected_menu)
            {
                case (int)MenuList.MYTODAY_MENU:     // 오늘 할 일 메뉴에서 입력됨
                    m_Data[0].DC_listName = "작업";
                    m_Data[0].DC_myToday = true;
                    dt = dt.AddDays(1);
                    m_Data[0].DC_myTodayTime = new DateTime(dt.Year, dt.Month, dt.Day, 00, 00, 00);
                    Menu_MyToday();
                    break;
                case (int)MenuList.IMPORTANT_MENU:     // 중요 메뉴에서 입력됨
                    m_Data[0].DC_listName = "작업";
                    m_Data[0].DC_important = true;
                    item.TD_important = true;
                    Menu_Important();
                    break;
                case (int)MenuList.DEADLINE_MENU:     // 계획된 일정 메뉴에서 입력됨
                    m_Data[0].DC_listName = "작업";
                    m_Data[0].DC_deadlineType = 1;
                    dt = dt.AddDays(1);
                    m_Data[0].DC_deadlineTime = new DateTime(dt.Year, dt.Month, dt.Day, 00, 00, 00);
                    Menu_Planned();
                    break;
                case (int)MenuList.COMPLETE_MENU:     // 완료됨 메뉴에서 입력됨
                    m_Data[0].DC_listName = "작업";
                    m_Data[0].DC_complete = true;
                    item.TD_complete = true;
                    
                    for (int i = 1; i <= m_Data.Count; i++)
                    {
                        if (m_Data[i].DC_listName == m_Data[0].DC_listName)
                        {
                            if (m_Data[i].DC_complete)
                            {
                                dc = m_Data[0]; //추출
                                m_Data.RemoveAt(0); //삭제
                                m_Data.Insert(i-1, dc); //삽입
                                break;
                            }
                        }
                    }
                    
                    Menu_Completed();
                    break;
            }
            Change_TaskInfomationText(m_Data[0]);
            Set_TodoItem_Width();
            Update_Metadata();
        }

        //--------------------------------------------------------------
        // 할일 항목을 클릭했을때 처리
        //--------------------------------------------------------------
        private void TodoItem_UserControl_Click(object sender, EventArgs e)
        {
            Todo_Item sd = (Todo_Item)sender;
            MouseEventArgs me = (MouseEventArgs)e;

            switch (me.Button)
            {
                case MouseButtons.Left:
                    TodoItem_Left_Click(sender, me);
                    break;
                case MouseButtons.Right:
                    TodoItem_Right_Click(sender, me);
                    break;
            }
        }

        private void TodoItem_Left_Click(object sender, MouseEventArgs e)
        {
            int pos = 0;
            Todo_Item sd = (Todo_Item)sender;
            
            foreach (CDataCell data in m_Data) // td로 dc 찾기
            {
                if (data.Equals(sd.TD_DataCell))
                {
                    m_selected_position = pos;  // 클릭한 task의 datacell 위치
                    break;
                }
                pos++;
            }

            pos = 0;
            foreach (Todo_Item item in flowLayoutPanel2.Controls)
            {
                if (item.Equals(sd))
                {
                    //완료됨 클릭시
                    if (item.IsCompleteClicked)
                    {
                        Complete_Process(item);  // td로 dc 변경
                        if (m_selected_menu == (int)MenuList.MYTODAY_MENU) Menu_MyToday(); // 오늘할일 메뉴에서 실행
                        if (m_selected_menu == (int)MenuList.IMPORTANT_MENU) Menu_Important(); // 중요 메뉴에서 실행
                        if (m_selected_menu == (int)MenuList.DEADLINE_MENU) Menu_Planned(); // 계획된 일정에서 실행
                        if (m_selected_menu == (int)MenuList.COMPLETE_MENU) Menu_Completed(); // 완료됨에서 실행
                        break;
                    }

                    // 중요항목 클릭시
                    if (item.IsImportantClicked)
                    {
                        Improtant_Process(item);  // td로 dc 변경
                        if (m_selected_menu == (int)MenuList.IMPORTANT_MENU) Menu_Important(); // 중요 메뉴에서 실행
                        break;
                    }

                    //Todo 아이템 클릭시
                    foreach (CDataCell data in m_Data) // td로 dc 찾기
                    {
                        if (data.Equals(item.TD_DataCell))
                        {
                            if (isDetailWindowOpen && item.IsItemSelected) 
                                CloseDetailWindow(); else OpenDetailWindow();
                            SendDataCellToDetailWindow(data);
                        }
                    }
                }
                pos++;
            }

            foreach (Todo_Item item in flowLayoutPanel2.Controls)
            {
                if (item.IsItemSelected) item.IsItemSelected = false;
            }
            sd.IsItemSelected = true;

            Set_TodoItem_Width();
            Update_Metadata();
        }

        private void Improtant_Process(Todo_Item item)
        {
            int pos = 0;
            foreach (CDataCell data in m_Data)
            {
                if (data.Equals(item.TD_DataCell))
                {
                    if (item.TD_important && !item.TD_complete)
                    {
                        flowLayoutPanel2.Controls.SetChildIndex(item, 0); // 리스트 상위로 이동
                        flowLayoutPanel2.VerticalScroll.Value = 0;

                        data.DC_important = true;

                        CDataCell dc = m_Data[pos]; //추출
                        m_Data.RemoveAt(pos); //삭제
                        m_Data.Insert(0, dc); //삽입

                        m_selected_position = 0;
                    }
                    else if (item.TD_important && item.TD_complete)
                    {
                        data.DC_important = true;
                    }
                    else if (!item.TD_important)
                    {
                        data.DC_important = false;
                    }
                    SendDataCellToDetailWindow(data);
                    break;
                }
                pos++;
            }
        }

        private void Complete_Process(Todo_Item item)
        {
            int pos = 0;
            foreach (CDataCell data in m_Data)
            {
                if (data.Equals(item.TD_DataCell))
                {
                    if (item.TD_complete)
                    {
                        int cnt = flowLayoutPanel2.Controls.Count;
                        flowLayoutPanel2.Controls.SetChildIndex(item, cnt);

                        data.DC_complete = true;

                        CDataCell dc = m_Data[pos]; //추출
                        m_Data.RemoveAt(pos); //삭제
                        m_Data.Insert(m_Data.Count, dc); //삽입

                        m_selected_position = m_Data.Count - 1;
                    }
                    else
                    {
                        flowLayoutPanel2.Controls.SetChildIndex(item, 0);

                        data.DC_complete = false;

                        CDataCell dc = m_Data[pos]; //추출
                        m_Data.RemoveAt(pos); //삭제
                        m_Data.Insert(0, dc); //삽입

                        m_selected_position = 0;
                    }
                    
                    SendDataCellToDetailWindow(data);
                    break;
                }
                pos++;
            }
        }

        private void TodoItem_Right_Click(object sender, MouseEventArgs e)
        {
            Todo_Item sd = (Todo_Item)sender;

            foreach (Todo_Item item in flowLayoutPanel2.Controls)
            {
                if (item.IsItemSelected) item.IsItemSelected = false;
            }
            sd.IsItemSelected = true;

            int pos = 0;
            foreach (CDataCell data in m_Data)  // td로 dc 찾기
            {
                if (data.Equals(sd.TD_DataCell))
                {
                    m_selected_position = pos;
                    SendDataCellToDetailWindow(data);
                    break;
                }
                pos++;
            }

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
            todoItemContextMenu.MenuItems.Add("-");
            todoItemContextMenu.MenuItems.Add(deleteItem);
            int px = Control.MousePosition.X - Location.X;
            int py = Control.MousePosition.Y - Location.Y - 30;
            todoItemContextMenu.Show(this, new Point(px, py));
        }

        private void OnTodoItemPopupEvent(object sender, EventArgs e)
        {
            ContextMenu ctm = (ContextMenu)sender;

            ctm.MenuItems[0].Text = m_Data[m_selected_position].DC_myToday ? "나의 하루에서 제거" : "나의 하루에 추가";
            ctm.MenuItems[1].Text = m_Data[m_selected_position].DC_important ? "중요도 제거" : "중요로 표시";
            ctm.MenuItems[2].Text = m_Data[m_selected_position].DC_complete ? "완료되지 않음으로 표시" : "완료됨으로 표시";
            ctm.MenuItems[4].Enabled = m_Data[m_selected_position].DC_deadlineType != 1;
            ctm.MenuItems[5].Enabled = m_Data[m_selected_position].DC_deadlineType != 2;
            ctm.MenuItems[7].Enabled = m_Data[m_selected_position].DC_deadlineType > 0;
        }

        private void OnImportantMenuItem_Click(object sender, EventArgs e)
        {
            if (m_Data[m_selected_position].DC_important)
            {
                starCheckbox1.Checked = false;
                m_Data[m_selected_position].DC_important = false;
            } else
            {
                starCheckbox1.Checked = true;
                m_Data[m_selected_position].DC_important = true;
            }

            foreach (Todo_Item item in flowLayoutPanel2.Controls)  // dc로 td 찾기
            {
                if (m_Data[m_selected_position].Equals(item.TD_DataCell))
                {
                    item.TD_important = starCheckbox1.Checked;
                    Improtant_Process(item);
                    if (m_selected_menu == (int)MenuList.IMPORTANT_MENU) Menu_Important(); // 중요 메뉴에서 실행
                    break;
                }
            }
            Update_Metadata();
        }

        private void OnCompleteMenuItem_Click(object sender, EventArgs e)
        {
            if (m_Data[m_selected_position].DC_complete)
            {
                roundCheckbox1.Checked = false;
                m_Data[m_selected_position].DC_complete = false;
            }
            else
            {
                roundCheckbox1.Checked = true;
                m_Data[m_selected_position].DC_complete = true;
            }

            foreach (Todo_Item item in flowLayoutPanel2.Controls)  // dc로 td 찾기
            {
                if (m_Data[m_selected_position].Equals(item.TD_DataCell))
                {
                    item.TD_complete = roundCheckbox1.Checked;
                    Complete_Process(item);
                    if (m_selected_menu == (int)MenuList.MYTODAY_MENU) Menu_MyToday(); // 오늘할일 메뉴에서 실행
                    if (m_selected_menu == (int)MenuList.IMPORTANT_MENU) Menu_Important(); // 중요 메뉴에서 실행
                    if (m_selected_menu == (int)MenuList.DEADLINE_MENU) Menu_Planned(); // 계획된 일정에서 실행
                    if (m_selected_menu == (int)MenuList.COMPLETE_MENU) Menu_Completed(); // 완료됨에서 실행
                    break;
                }
            }
            Update_Metadata();
        }

        private void OnMemoEditMenuItem_Click(object sender, EventArgs e)
        {
            memoForm.StartPosition = FormStartPosition.Manual;
            memoForm.Location = new Point(Location.X + (Width - memoForm.Width) / 2, Location.Y + (Height - memoForm.Height) / 2);
            memoForm.TextBoxString = textBox1.Text;
            memoForm.Text = m_Data[m_selected_position].DC_title;
            memoForm.ShowDialog();
            textBox1.Text = memoForm.TextBoxString;
            textBox1.SelectionStart = textBox1.Text.Length;

            m_Data[m_selected_position].DC_memo = textBox1.Text;
        }

        private void OnDeleteItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("항목 삭제?", WINDOW_CAPTION, MessageBoxButtons.YesNo) == DialogResult.No) return;

            CloseDetailWindow();

            foreach (Todo_Item item in flowLayoutPanel2.Controls)  // dc로 td 찾기
            {
                if (m_Data[m_selected_position].Equals(item.TD_DataCell))
                {
                    item.UserControl_Click -= new TodoItemList_Event(TodoItem_UserControl_Click);
                    splitContainer2.Panel1.Controls.Remove(item);
                    item.Dispose();

                    m_Data.Remove(m_Data[m_selected_position]);
                    break;
                }
            }

            Set_TodoItem_Width();
            Update_Metadata();
        }

        private void Update_Metadata()
        {
            int cnt = 0;
            foreach (TwoLineList item in flowLayoutPanel_Menulist.Controls)
            {
                switch (item.PrimaryText)
                {
                    case "오늘 할 일":
                        cnt = (from CDataCell data in m_Data where data.DC_myToday && !data.DC_complete select data).Count();
                        break;
                    case "중요":
                        cnt = (from CDataCell data in m_Data where data.DC_important && !data.DC_complete select data).Count();
                        break;
                    case "계획된 일정":
                        cnt = (from CDataCell data in m_Data
                               where (data.DC_myToday || data.DC_deadlineType > 0 || data.DC_repeatType > 0) && !data.DC_complete
                               select data).Count();
                        break;
                    case "완료됨":
                        cnt = (from CDataCell data in m_Data where data.DC_complete select data).Count();
                        break;
                    case "작업":
                        cnt = (from CDataCell data in m_Data where data.DC_listName == "작업" select data).Count();
                        break;
                    default:
                        cnt = (from CDataCell data in m_Data where data.DC_listName == item.PrimaryText select data).Count();
                        break;
                }
                item.MetadataText = cnt.ToString();
            }
        }

        private void SendDataCellToDetailWindow(CDataCell dc)
        {
            textBox3.Text = dc.DC_title;
            roundCheckbox1.Checked = dc.DC_complete;
            starCheckbox1.Checked = dc.DC_important;
            textBox1.Text = dc.DC_memo;
            textBox1.SelectionStart = textBox1.Text.Length;

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

            createDateLabel.Text = dc.DC_dateCreated.ToString("yyyy-MM-dd(ddd)\r\n") + "생성됨["+m_selected_position+"]";
        }

        // -----------------------------------------------------------
        // 할일 입력 처리 부분
        // -----------------------------------------------------------
        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (!isTextboxClicked) textBox2.Text = "";
            isTextboxClicked = true;
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (isTextboxClicked)
            {
                if (textBox2.Text.Trim().Length == 0)
                {
                    textBox2.Text = "+ 작업 추가";
                    isTextboxClicked = false;
                }
            }
            else
            {
                textBox2.Text = "+ 작업 추가";
                isTextboxClicked = false;
            }
        }

        private void textBox2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu textboxMenu = new ContextMenu();
                MenuItem copyMenu = new MenuItem("복사", new EventHandler(OnCopyMenu_textBox2_Click));
                MenuItem cutMenu = new MenuItem("잘라내기", new EventHandler(OnCutMenu_textBox2_Click));
                MenuItem pasteMenu = new MenuItem("붙여넣기", new EventHandler(OnPasteMenu_textBox2_Click));

                textboxMenu.Popup += new EventHandler(OnPopupEvent_textBox2);
                textboxMenu.MenuItems.Add(copyMenu);
                textboxMenu.MenuItems.Add(cutMenu);
                textboxMenu.MenuItems.Add(pasteMenu);
                textBox2.ContextMenu = textboxMenu;

                textBox2.ContextMenu.Show(textBox2, new Point(e.X, e.Y));
            }
        }

        private void OnPopupEvent_textBox2(object sender, EventArgs e)
        {
            ContextMenu ctm = (ContextMenu)sender;
            ctm.MenuItems[0].Enabled = textBox2.SelectedText.Length != 0; // copy
            ctm.MenuItems[1].Enabled = textBox2.SelectedText.Length != 0; // cut
            ctm.MenuItems[2].Enabled = Clipboard.ContainsText(); // paste
        }
        private void OnCopyMenu_textBox2_Click(object sender, EventArgs e) { textBox2.Copy(); }
        private void OnCutMenu_textBox2_Click(object sender, EventArgs e) { textBox2.Cut(); }
        private void OnPasteMenu_textBox2_Click(object sender, EventArgs e) { textBox2.Paste(); }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = false;
                e.SuppressKeyPress = false;
                if (textBox2.Text.Trim().Length == 0) return;
                Add_Task(textBox2.Text);
                textBox2.Text = "";
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        // --------------------------------------------------------------------------
        // 상세창 처리 부분 (제목 / 완료 / 중요 / 메모 / 위아래 / 닫기 / 삭제)
        // --------------------------------------------------------------------------

        //상세창 제목 입력
        private void textBox3_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu textboxMenu = new ContextMenu();
                MenuItem copyMenu = new MenuItem("복사", new EventHandler(OnCopyMenu_textBox3_Click));
                MenuItem cutMenu = new MenuItem("잘라내기", new EventHandler(OnCutMenu_textBox3_Click));
                MenuItem pasteMenu = new MenuItem("붙여넣기", new EventHandler(OnPasteMenu_textBox3_Click));

                textboxMenu.Popup += new EventHandler(OnPopupEvent_textBox3);
                textboxMenu.MenuItems.Add(copyMenu);
                textboxMenu.MenuItems.Add(cutMenu);
                textboxMenu.MenuItems.Add(pasteMenu);
                textBox3.ContextMenu = textboxMenu;

                textBox3.ContextMenu.Show(textBox3, new Point(e.X, e.Y));
            }
        }

        private void OnPopupEvent_textBox3(object sender, EventArgs e)
        {
            ContextMenu ctm = (ContextMenu)sender;
            ctm.MenuItems[0].Enabled = textBox3.SelectedText.Length != 0; // copy
            ctm.MenuItems[1].Enabled = textBox3.SelectedText.Length != 0; // cut
            ctm.MenuItems[2].Enabled = Clipboard.ContainsText(); // paste
        }

        private void OnCopyMenu_textBox3_Click(object sender, EventArgs e) { textBox3.Copy(); }
        private void OnCutMenu_textBox3_Click(object sender, EventArgs e) { textBox3.Cut(); }
        private void OnPasteMenu_textBox3_Click(object sender, EventArgs e) { textBox3.Paste(); }

        private void textBox3_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = false;
                e.SuppressKeyPress = false;

                if (textBox3.Text.Trim().Length == 0)
                {
                    textBox3.Text = m_Data[m_selected_position].DC_title;
                    return;
                }

                m_Data[m_selected_position].DC_title = textBox3.Text;

                foreach (Todo_Item item in flowLayoutPanel2.Controls)  // dc로 td 찾기
                {
                    if (m_Data[m_selected_position].Equals(item.TD_DataCell))
                    {
                        item.TD_title = textBox3.Text;
                        break;
                    }
                }
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (textBox3.Text.Trim().Length == 0)
            {
                textBox3.Text = m_Data[m_selected_position].DC_title;
                return;
            }

            m_Data[m_selected_position].DC_title = textBox3.Text;

            foreach (Todo_Item item in flowLayoutPanel2.Controls)  // dc로 td 찾기
            {
                if (m_Data[m_selected_position].Equals(item.TD_DataCell))
                {
                    item.TD_title = textBox3.Text;
                    break;
                }
            }
        }

        // 상세창 메모 커서 벗어남
        private void textBox1_Leave(object sender, EventArgs e)
        {
            //메모 내용에 변경이 있는지 확인(?)
            m_Data[m_selected_position].DC_memo = textBox1.Text;
        }

        private void textBox1_MouseLeave(object sender, EventArgs e)
        {
            m_Data[m_selected_position].DC_memo = textBox1.Text;
        }

        // 상세창 메모 컨텍스트 메뉴
        private void textBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu textboxMenu = new ContextMenu();
                MenuItem extendMemo = new MenuItem("메모확장", new EventHandler(OnExtendMemo_textBox1_Click));
                MenuItem copyMenu = new MenuItem("복사", new EventHandler(OnCopyMenu_textBox1_Click));
                MenuItem cutMenu = new MenuItem("잘라내기", new EventHandler(OnCutMenu_textBox1_Click));
                MenuItem pasteMenu = new MenuItem("붙여넣기", new EventHandler(OnPasteMenu_textBox1_Click));
                MenuItem selectAllMenu = new MenuItem("전체 선택", new EventHandler(OnSelectAllMenu_textBox1_Click));
                MenuItem undoMenu = new MenuItem("실행 취소", new EventHandler(OnUndoMenu_textBox1_Click));

                textboxMenu.Popup += new EventHandler(OnPopupEvent_textBox1);
                textboxMenu.MenuItems.Add(extendMemo);
                textboxMenu.MenuItems.Add("-");
                textboxMenu.MenuItems.Add(copyMenu);
                textboxMenu.MenuItems.Add(cutMenu);
                textboxMenu.MenuItems.Add(pasteMenu);
                textboxMenu.MenuItems.Add(selectAllMenu);
                textboxMenu.MenuItems.Add("-");
                textboxMenu.MenuItems.Add(undoMenu);
                textBox1.ContextMenu = textboxMenu;

                textBox1.ContextMenu.Show(textBox1, new Point(e.X, e.Y));
            }
        }

        private void OnPopupEvent_textBox1(object sender, EventArgs e)
        {
            ContextMenu ctm = (ContextMenu)sender;

            ctm.MenuItems[2].Enabled = textBox1.SelectedText.Length != 0; // copy
            ctm.MenuItems[3].Enabled = textBox1.SelectedText.Length != 0; // cut
            ctm.MenuItems[4].Enabled = Clipboard.ContainsText(); // paste
            ctm.MenuItems[5].Enabled = textBox1.Text.Length != 0; // selectAll
            ctm.MenuItems[7].Enabled = textBox1.CanUndo; // undo
        }

        private void OnExtendMemo_textBox1_Click(object sender, EventArgs e)
        {
            memoForm.StartPosition = FormStartPosition.Manual;
            memoForm.Location = new Point(Location.X + (Width - memoForm.Width) / 2, Location.Y + (Height - memoForm.Height) / 2);
            memoForm.TextBoxString = textBox1.Text;
            memoForm.ShowDialog();
            textBox1.Text = memoForm.TextBoxString;
            textBox1.SelectionStart = textBox1.Text.Length;

            m_Data[m_selected_position].DC_memo = textBox1.Text;
        }

        private void OnCopyMenu_textBox1_Click(object sender, EventArgs e) { textBox1.Copy(); }
        private void OnCutMenu_textBox1_Click(object sender, EventArgs e) { textBox1.Cut(); }
        private void OnPasteMenu_textBox1_Click(object sender, EventArgs e) { textBox1.Paste(); }
        private void OnSelectAllMenu_textBox1_Click(object sender, EventArgs e) { textBox1.SelectAll(); }
        private void OnUndoMenu_textBox1_Click(object sender, EventArgs e) { textBox1.Undo(); }

        // 상세창 닫기 버튼
        private void button1_Click(object sender, EventArgs e)
        {
            if (isDetailWindowOpen) CloseDetailWindow();
            Set_TodoItem_Width();
        }

        //상세창 삭제 버튼
        private void button2_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("항목 삭제?", WINDOW_CAPTION, MessageBoxButtons.YesNo) == DialogResult.No) return;

            if (isDetailWindowOpen)
            {
                foreach (Todo_Item item in flowLayoutPanel2.Controls)  // dc로 td 찾기
                {
                    if (m_Data[m_selected_position].Equals(item.TD_DataCell))
                    {
                        item.UserControl_Click -= new TodoItemList_Event(TodoItem_UserControl_Click);
                        splitContainer2.Panel1.Controls.Remove(item);
                        item.Dispose();

                        m_Data.Remove(m_Data[m_selected_position]);
                        break;
                    }
                }
                CloseDetailWindow();
            }
            Set_TodoItem_Width();
        }

        //
        //상세창 완료 체크시
        //
        private void roundCheckbox1_MouseClick(object sender, EventArgs e)
        {
            if (isDetailWindowOpen)
            {
                m_Data[m_selected_position].DC_complete = roundCheckbox1.Checked;

                foreach (Todo_Item item in flowLayoutPanel2.Controls)  // dc로 td 찾기
                {
                    if (m_Data[m_selected_position].Equals(item.TD_DataCell))
                    {
                        item.TD_complete = roundCheckbox1.Checked;
                        Complete_Process(item);
                        if (m_selected_menu == (int)MenuList.MYTODAY_MENU) Menu_MyToday(); // 오늘할일 메뉴에서 실행
                        if (m_selected_menu == (int)MenuList.IMPORTANT_MENU) Menu_Important(); // 중요 메뉴에서 실행
                        if (m_selected_menu == (int)MenuList.DEADLINE_MENU) Menu_Planned(); // 계획된 일정에서 실행
                        if (m_selected_menu == (int)MenuList.COMPLETE_MENU) Menu_Completed(); // 완료됨에서 실행
                        break;
                    }
                }
            }
            Update_Metadata();
        }

        private void roundCheckbox1_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void roundCheckbox1_MouseEnter(object sender, EventArgs e)
        {
            roundCheckbox1.BackColor = COLOR_DETAIL_WINDOW_BACK_COLOR;
        }

        private void roundCheckbox1_MouseLeave(object sender, EventArgs e)
        {
            roundCheckbox1.BackColor = COLOR_DETAIL_WINDOW_BACK_COLOR;
        }

        // 
        //상세창 중요 체크시
        //
        private void starCheckbox1_MouseClick(object sender, EventArgs e)
        {
            if (isDetailWindowOpen)
            {
                m_Data[m_selected_position].DC_important = starCheckbox1.Checked;

                foreach (Todo_Item item in flowLayoutPanel2.Controls)  // dc로 td 찾기
                {
                    if (m_Data[m_selected_position].Equals(item.TD_DataCell))
                    {
                        item.TD_important = starCheckbox1.Checked;
                        Improtant_Process(item);
                        if (m_selected_menu == (int)MenuList.IMPORTANT_MENU) Menu_Important(); // 중요 메뉴에서 실행
                        break;
                    }
                }
            }
            Update_Metadata();
        }

        private void starCheckbox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void starCheckbox1_MouseEnter(object sender, EventArgs e)
        {
            starCheckbox1.BackColor = COLOR_DETAIL_WINDOW_BACK_COLOR;
        }

        private void starCheckbox1_MouseLeave(object sender, EventArgs e)
        {
            starCheckbox1.BackColor = COLOR_DETAIL_WINDOW_BACK_COLOR;
        }

        //
        // 상세창 - 나의 하루에 추가 메뉴
        //
        private void roundLabel1_Click(object sender, MouseEventArgs e)
        {
            roundLabel1.Focus();
            if (e.Button != MouseButtons.Left) return;
            OnMyToday_Click(sender, e);
        }

        private void OnMyToday_Click(object sender, EventArgs e)
        {
            if (m_Data[m_selected_position].DC_myToday)
            {
                m_Data[m_selected_position].DC_myToday = false;  // 해제
                m_Data[m_selected_position].DC_myTodayTime = default;
                roundLabel1.Text = "나의 하루에 추가";
                roundLabel1.BackColor = COLOR_DETAIL_WINDOW_BACK_COLOR;
            }
            else
            {
                m_Data[m_selected_position].DC_myToday = true; // 설정
                DateTime dt = DateTime.Now;

                dt = dt.AddDays(1);

                m_Data[m_selected_position].DC_myTodayTime = new DateTime(dt.Year, dt.Month, dt.Day, 00, 00, 00);
                //Console.WriteLine("나의 하루에 추가 설정됨 : " + m_Data[m_selected_position].DC_myTodayTime.ToString("yyyy-MM-dd HH:mm:ss"));

                roundLabel1.Text = "나의 하루에 추가됨";
                roundLabel1.BackColor = PSEUDO_SELECTED_COLOR;
            }

            Change_TaskInfomationText(m_Data[m_selected_position]);
            Update_Metadata();

            if (m_selected_menu == (int)MenuList.MYTODAY_MENU) Menu_MyToday(); // 나의하루에 추가 메뉴에서 실행
        }

        private void roundLabel1_MouseEnter(object sender, EventArgs e)
        {
            roundLabel1.BackColor = PSEUDO_HIGHLIGHT_COLOR;
        }

        private void roundLabel1_MouseLeave(object sender, EventArgs e)
        {
            roundLabel1.BackColor = m_Data[m_selected_position].DC_myToday ? PSEUDO_SELECTED_COLOR : COLOR_DETAIL_WINDOW_BACK_COLOR;
        }

        //
        // 상세창 - 미리 알림 메뉴
        //
        private void roundLabel2_MouseEnter(object sender, EventArgs e)
        {
            roundLabel2.BackColor = PSEUDO_HIGHLIGHT_COLOR;
        }

        private void roundLabel2_MouseLeave(object sender, EventArgs e)
        {
            roundLabel2.BackColor = m_Data[m_selected_position].DC_remindType > 0 ? PSEUDO_SELECTED_COLOR : COLOR_DETAIL_WINDOW_BACK_COLOR;
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
            m_Data[m_selected_position].DC_remindType = 1;

            DateTime dt = DateTime.Now;
            dt = dt.Minute < 30 ? dt.AddHours(3) : dt.AddHours(4);
            m_Data[m_selected_position].DC_remindTime = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, 0, 0);
            //Console.WriteLine("오늘 나중에 알림 설정됨 : " + m_Data[m_selected_position].DC_remindTime.ToString("yyyy-MM-dd HH:mm:ss"));

            roundLabel2.Text = "알림 설정됨";
            roundLabel2.BackColor = PSEUDO_SELECTED_COLOR;

            Change_TaskInfomationText(m_Data[m_selected_position]);
            Update_Metadata();
        }

        private void OnTomorrowRemind_Click(object sender, EventArgs e)
        {
            m_Data[m_selected_position].DC_remindType = 2;

            DateTime dt = DateTime.Now;
            dt = dt.AddDays(1);
            m_Data[m_selected_position].DC_remindTime = new DateTime(dt.Year, dt.Month, dt.Day, 09, 00, 00);
            //Console.WriteLine("내일 알림 설정됨 : " + m_Data[m_selected_position].DC_remindTime.ToString("yyyy-MM-dd HH:mm:ss"));

            roundLabel2.Text = "알림 설정됨";
            roundLabel2.BackColor = PSEUDO_SELECTED_COLOR;

            Change_TaskInfomationText(m_Data[m_selected_position]);
            Update_Metadata();
        }

        private void OnNextWeekRemind_Click(object sender, EventArgs e)
        {
            m_Data[m_selected_position].DC_remindType = 3;

            DateTime dt = DateTime.Now;
            DayOfWeek dw = dt.DayOfWeek;
            switch (dw)
            {
                case DayOfWeek.Monday:
                    dt = dt.AddDays(7);
                    break;
                case DayOfWeek.Tuesday:
                    dt = dt.AddDays(6);
                    break;
                case DayOfWeek.Wednesday:
                    dt = dt.AddDays(5);
                    break;
                case DayOfWeek.Thursday:
                    dt = dt.AddDays(4);
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
            m_Data[m_selected_position].DC_remindTime = new DateTime(dt.Year, dt.Month, dt.Day, 09, 00, 00);
            //Console.WriteLine("다음주 알림 설정됨 : " + m_Data[m_selected_position].DC_remindTime.ToString("yyyy-MM-dd HH:mm:ss"));

            roundLabel2.Text = "알림 설정됨";
            roundLabel2.BackColor = PSEUDO_SELECTED_COLOR;

            Change_TaskInfomationText(m_Data[m_selected_position]);
            Update_Metadata();
        }

        private void OnSelectRemind_Click(object sender, EventArgs e)
        {
            CarendarForm carendar = new CarendarForm();
            carendar.ShowDialog();
            if (carendar.IsSelected && (carendar.SelectedDateTime != default))
            {
                m_Data[m_selected_position].DC_remindType = 4;
                m_Data[m_selected_position].DC_remindTime = carendar.SelectedDateTime;
                //Console.WriteLine("알림 선택 설정됨 : " + m_Data[m_selected_position].DC_remindTime.ToString("yyyy-MM-dd HH:mm:ss"));
                roundLabel2.Text = "알림 설정됨";
                roundLabel2.BackColor = PSEUDO_SELECTED_COLOR;
                carendar.IsSelected = false;
            }
            else
            {
                m_Data[m_selected_position].DC_remindType = 0;
                m_Data[m_selected_position].DC_remindTime = default;
                roundLabel2.Text = "미리 알림";
                roundLabel2.BackColor = COLOR_DETAIL_WINDOW_BACK_COLOR;
            }

            Change_TaskInfomationText(m_Data[m_selected_position]);
            Update_Metadata();
        }

        private void OnDeleteRemind_Click(object sender, EventArgs e)
        {
            m_Data[m_selected_position].DC_remindType = 0;
            m_Data[m_selected_position].DC_remindTime = default;
            roundLabel2.Text = "미리 알림";
            roundLabel2.BackColor = COLOR_DETAIL_WINDOW_BACK_COLOR;

            Change_TaskInfomationText(m_Data[m_selected_position]);
            Update_Metadata();
        }

        //
        // 상세창 - 기한 설정 메뉴
        //
        private void roundLabel3_MouseEnter(object sender, EventArgs e)
        {
            roundLabel3.BackColor = PSEUDO_HIGHLIGHT_COLOR;
        }

        private void roundLabel3_MouseLeave(object sender, EventArgs e)
        {
            roundLabel3.BackColor = m_Data[m_selected_position].DC_deadlineType > 0 ? PSEUDO_SELECTED_COLOR : COLOR_DETAIL_WINDOW_BACK_COLOR;
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
            m_Data[m_selected_position].DC_deadlineType = 1;

            DateTime dt = DateTime.Now;

            dt = dt.AddDays(1);

            m_Data[m_selected_position].DC_deadlineTime = new DateTime(dt.Year, dt.Month, dt.Day, 00, 00, 00);
            //Console.WriteLine("오늘 기한 설정됨 : " + m_Data[m_selected_position].DC_myTodayTime.ToString("yyyy-MM-dd HH:mm:ss"));

            roundLabel3.Text = "기한 설정됨";
            roundLabel3.BackColor = PSEUDO_SELECTED_COLOR;

            Change_TaskInfomationText(m_Data[m_selected_position]);
            Update_Metadata();
        }

        private void OnTomorrowDeadline_Click(object sender, EventArgs e)
        {
            m_Data[m_selected_position].DC_deadlineType = 2;

            DateTime dt = DateTime.Now;

            dt = dt.AddDays(2);

            m_Data[m_selected_position].DC_deadlineTime = new DateTime(dt.Year, dt.Month, dt.Day, 00, 00, 00);
            //Console.WriteLine("내일 기한 설정됨 : " + m_Data[m_selected_position].DC_deadlineTime.ToString("yyyy-MM-dd HH:mm:ss"));

            roundLabel3.Text = "기한 설정됨";
            roundLabel3.BackColor = PSEUDO_SELECTED_COLOR;

            Change_TaskInfomationText(m_Data[m_selected_position]);
            Update_Metadata();
        }

        private void OnNextWeekDeadline_Click(object sender, EventArgs e)
        {
            m_Data[m_selected_position].DC_deadlineType = 3;

            DateTime dt = DateTime.Now;
            DayOfWeek dw = dt.DayOfWeek;
            switch (dw)
            {
                case DayOfWeek.Monday:
                    dt = dt.AddDays(7);
                    break;
                case DayOfWeek.Tuesday:
                    dt = dt.AddDays(6);
                    break;
                case DayOfWeek.Wednesday:
                    dt = dt.AddDays(5);
                    break;
                case DayOfWeek.Thursday:
                    dt = dt.AddDays(4);
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
            m_Data[m_selected_position].DC_deadlineTime = new DateTime(dt.Year, dt.Month, dt.Day, 00, 00, 00);
            //Console.WriteLine("다음주 기한 설정됨 : " + m_Data[m_selected_position].DC_deadlineTime.ToString("yyyy-MM-dd HH:mm:ss"));

            roundLabel3.Text = "기한 설정됨";
            roundLabel3.BackColor = PSEUDO_SELECTED_COLOR;

            Change_TaskInfomationText(m_Data[m_selected_position]);
            Update_Metadata();
        }

        private void OnSelectDeadline_Click(object sender, EventArgs e)
        {
            CarendarForm carendar = new CarendarForm();
            carendar.ShowDialog();
            if (carendar.IsSelected && (carendar.SelectedDateTime != default))
            {
                m_Data[m_selected_position].DC_deadlineType = 4;
                m_Data[m_selected_position].DC_deadlineTime = carendar.SelectedDateTime;
                //Console.WriteLine("선택 기한 설정됨 : " + m_Data[m_selected_position].DC_deadlineTime.ToString("yyyy-MM-dd HH:mm:ss"));

                roundLabel3.Text = "기한 설정됨";
                roundLabel3.BackColor = PSEUDO_SELECTED_COLOR;
                carendar.IsSelected = false;
            }
            else
            {
                m_Data[m_selected_position].DC_deadlineType = 0;
                m_Data[m_selected_position].DC_deadlineTime = default;
                roundLabel3.Text = "기한 설정";
                roundLabel3.BackColor = COLOR_DETAIL_WINDOW_BACK_COLOR;
            }

            Change_TaskInfomationText(m_Data[m_selected_position]);
            Update_Metadata();
        }

        private void OnDeleteDeadline_Click(object sender, EventArgs e)
        {
            m_Data[m_selected_position].DC_deadlineType = 0;
            m_Data[m_selected_position].DC_deadlineTime = default;
            roundLabel3.Text = "기한 설정";
            roundLabel3.BackColor = COLOR_DETAIL_WINDOW_BACK_COLOR;
            if (m_Data[m_selected_position].DC_repeatType > 0) // 반복이 되어 있을때
            {
                m_Data[m_selected_position].DC_repeatType = 0;
                m_Data[m_selected_position].DC_repeatTime = default;
                roundLabel4.Text = "반복";
                roundLabel4.BackColor = COLOR_DETAIL_WINDOW_BACK_COLOR;
            }

            Change_TaskInfomationText(m_Data[m_selected_position]);
            Update_Metadata();

            if (m_selected_menu == (int)MenuList.DEADLINE_MENU) Menu_Planned(); // 기한 설정 메뉴에서 실행
        }

        //
        // 상세창 - 반복 메뉴
        //
        private void roundLabel4_MouseEnter(object sender, EventArgs e)
        {
            roundLabel4.BackColor = PSEUDO_HIGHLIGHT_COLOR;
        }

        private void roundLabel4_MouseLeave(object sender, EventArgs e)
        {
            roundLabel4.BackColor = m_Data[m_selected_position].DC_repeatType > 0 ? PSEUDO_SELECTED_COLOR : COLOR_DETAIL_WINDOW_BACK_COLOR;
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
            Repeat_EveryDay(m_Data[m_selected_position]);
        }

        private void Repeat_EveryDay(CDataCell data)
        {
            data.DC_repeatType = 1;

            DateTime dt = DateTime.Now;
            dt = dt.AddDays(1);

            data.DC_repeatTime = new DateTime(dt.Year, dt.Month, dt.Day, 09, 00, 00);
            //Console.WriteLine("매일 반복 설정됨 : " + data.DC_repeatTime.ToString("yyyy-MM-dd HH:mm:ss"));

            roundLabel4.Text = "반복 설정됨";
            roundLabel4.BackColor = PSEUDO_SELECTED_COLOR;
            if (data.DC_deadlineType == 0) // 기한설정이 되어 있지 않을때
            {
                data.DC_deadlineType = 1;
                data.DC_deadlineTime = new DateTime(dt.Year, dt.Month, dt.Day, 09, 00, 00);

                roundLabel3.Text = "기한 설정됨";
                roundLabel3.BackColor = PSEUDO_SELECTED_COLOR;
            }

            Change_TaskInfomationText(data);
            Update_Metadata();
        }

        private void OnWorkingDayRepeat_Click(object sender, EventArgs e)
        {
            Repeat_WorkingDay(m_Data[m_selected_position]);
        }

        private void Repeat_WorkingDay(CDataCell data)
        {
            data.DC_repeatType = 2;

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
            data.DC_repeatTime = new DateTime(dt.Year, dt.Month, dt.Day, 09, 00, 00);
            //Console.WriteLine("평일 반복 설정됨 : " + data.DC_repeatTime.ToString("yyyy-MM-dd HH:mm:ss"));

            roundLabel4.Text = "반복 설정됨";
            roundLabel4.BackColor = PSEUDO_SELECTED_COLOR;
            if (data.DC_deadlineType == 0) // 기한설정이 되어 있지 않을때
            {
                data.DC_deadlineType = 1;
                data.DC_deadlineTime = new DateTime(dt.Year, dt.Month, dt.Day, 09, 00, 00);
                roundLabel3.Text = "기한 설정됨";
                roundLabel3.BackColor = PSEUDO_SELECTED_COLOR;
            }

            Change_TaskInfomationText(data);
            Update_Metadata();
        }

        private void OnEveryWeekRepeat_Click(object sender, EventArgs e)
        {
            Repeat_EveryWeek(m_Data[m_selected_position]);
        }

        private void Repeat_EveryWeek(CDataCell data)
        {
            data.DC_repeatType = 3;

            DateTime dt = DateTime.Now;

            dt = dt.AddDays(7);

            data.DC_repeatTime = new DateTime(dt.Year, dt.Month, dt.Day, 09, 00, 00);
            //Console.WriteLine("매주 반복 설정됨 : " + data.DC_repeatTime.ToString("yyyy-MM-dd HH:mm:ss"));

            roundLabel4.Text = "반복 설정됨";
            roundLabel4.BackColor = PSEUDO_SELECTED_COLOR;
            if (data.DC_deadlineType == 0) // 기한설정이 되어 있지 않을때
            {
                data.DC_deadlineType = 1;
                data.DC_deadlineTime = new DateTime(dt.Year, dt.Month, dt.Day, 09, 00, 00);
                roundLabel3.Text = "기한 설정됨";
                roundLabel3.BackColor = PSEUDO_SELECTED_COLOR;
            }

            Change_TaskInfomationText(data);
            Update_Metadata();
        }

        private void OnEveryMonthRepeat_Click(object sender, EventArgs e)
        {
            Repeat_EveryMonth(m_Data[m_selected_position]);
        }

        private void Repeat_EveryMonth(CDataCell data)
        {
            data.DC_repeatType = 4;

            DateTime dt = DateTime.Now;

            dt = dt.AddMonths(1); // 매달 말일 계산 필요 - 28/29/30/31일 경우

            data.DC_repeatTime = new DateTime(dt.Year, dt.Month, dt.Day, 09, 00, 00);
            //Console.WriteLine("매달 반복 설정됨 : " + data.DC_repeatTime.ToString("yyyy-MM-dd HH:mm:ss"));


            roundLabel4.Text = "반복 설정됨";
            roundLabel4.BackColor = PSEUDO_SELECTED_COLOR;
            if (data.DC_deadlineType == 0) // 기한설정이 되어 있지 않을때
            {
                data.DC_deadlineType = 1;
                data.DC_deadlineTime = new DateTime(dt.Year, dt.Month, dt.Day, 09, 00, 00);
                roundLabel3.Text = "기한 설정됨";
                roundLabel3.BackColor = PSEUDO_SELECTED_COLOR;
            }

            Change_TaskInfomationText(data);
            Update_Metadata();
        }

        private void OnEveryYearRepeat_Click(object sender, EventArgs e)
        {
            Repeat_EveryYear(m_Data[m_selected_position]);
        }

        private void Repeat_EveryYear(CDataCell data)
        {
            data.DC_repeatType = 5;

            DateTime dt = DateTime.Now;

            dt = dt.AddYears(1);  // 윤년 계산 필요 2월29일

            data.DC_repeatTime = new DateTime(dt.Year, dt.Month, dt.Day, 09, 00, 00);
            //Console.WriteLine("매년 반복 설정됨 : " + m_Data[m_selected_position].DC_repeatTime.ToString("yyyy-MM-dd HH:mm:ss"));

            roundLabel4.Text = "반복 설정됨";
            roundLabel4.BackColor = PSEUDO_SELECTED_COLOR;
            if (data.DC_deadlineType == 0) // 기한설정이 되어 있지 않을때
            {
                data.DC_deadlineType = 1;
                data.DC_deadlineTime = new DateTime(dt.Year, dt.Month, dt.Day, 09, 00, 00);
                roundLabel3.Text = "기한 설정됨";
                roundLabel3.BackColor = PSEUDO_SELECTED_COLOR;
            }

            Change_TaskInfomationText(data);
            Update_Metadata();
        }

        private void OnDeleteRepeat_Click(object sender, EventArgs e)
        {
            m_Data[m_selected_position].DC_repeatType = 0;
            m_Data[m_selected_position].DC_repeatTime = default;
            roundLabel4.Text = "반복";
            roundLabel4.BackColor = COLOR_DETAIL_WINDOW_BACK_COLOR;

            Change_TaskInfomationText(m_Data[m_selected_position]);
            Update_Metadata();
        }

        //
        // upArrow
        //
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

            int pos = m_selected_position;
            if (pos == 0) return;
            if (!m_Data[pos].DC_complete)
            {
                for (int i = pos - 1; i >= 0; i--)
                {
                    if (m_Data[i].DC_listName == m_Data[pos].DC_listName)
                    {
                        CDataCell dc = m_Data[pos]; //추출
                        m_Data.RemoveAt(pos); //삭제
                        m_Data.Insert(i, dc); // 삽입

                        m_selected_position = i;
                        break;
                    }
                }
            }
            else return;

            pos = 0;
            foreach (Todo_Item item in flowLayoutPanel2.Controls)
            {
                if (m_Data[m_selected_position].Equals(item.TD_DataCell))
                {
                    if (pos == 0) break;
                    flowLayoutPanel2.Controls.SetChildIndex(item, pos - 1);
                    break;
                }
                pos++;
            }
            /*
            foreach (TwoLineList item in flowLayoutPanel_Menulist.Controls)
            {
                if (item.IsSelected)
                {
                    switch (item.PrimaryText)
                    {
                        case "작업":
                            m_selected_menu = (int)MenuList.TODO_ITEM_MENU;
                            Menu_Task();
                            break;
                        default:
                            m_selected_menu = (int)MenuList.LIST_MENU;
                            Menu_List(item);
                            break;
                    }
                }
            }
            */
            Set_TodoItem_Width();
            Update_Metadata();
        }

        //
        // downArrow
        //
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

            int pos = m_selected_position;

            if (pos == (m_Data.Count - 1)) return;

            if (!m_Data[pos].DC_complete)
            {
                for (int i = pos + 1; i <= m_Data.Count - 1; i++)
                {
                    if (m_Data[i].DC_complete) return;
                    if (m_Data[i].DC_listName == m_Data[pos].DC_listName)
                    {
                        CDataCell dc = m_Data[pos]; //추출
                        m_Data.RemoveAt(pos); //삭제
                        m_Data.Insert(i, dc); // 삽입

                        m_selected_position = i;
                        break;
                    }
                }
            }
            else return;

            pos = 0;
            foreach (Todo_Item item in flowLayoutPanel2.Controls)
            {
                if (m_Data[m_selected_position].Equals(item.TD_DataCell))
                {
                    Console.WriteLine("m_selected_position : " + m_selected_position);
                    Console.WriteLine("pos : " + pos);
                    if (pos == flowLayoutPanel2.Controls.Count) break;
                    flowLayoutPanel2.Controls.SetChildIndex(item, pos + 1);
                    break;
                }
                pos++;
            }
            /*
            foreach (TwoLineList item in flowLayoutPanel_Menulist.Controls)
            {
                if (item.IsSelected)
                {
                    switch (item.PrimaryText)
                    {
                        case "작업":
                            m_selected_menu = (int)MenuList.TODO_ITEM_MENU;
                            Menu_Task();
                            break;
                        default:
                            m_selected_menu = (int)MenuList.LIST_MENU;
                            Menu_List(item);
                            break;
                    }
                }
            }
            */
            Set_TodoItem_Width();
            Update_Metadata();
        }

        // ------------------------------------------------------------------
        // 알람 체크
        // ------------------------------------------------------------------
        private void AlarmCheck()
        {
            bool alarm = false;
            int result;
            string txt;
            DateTime dt = DateTime.Now;
            DateTime tt;

            int pos = 0;
            foreach (CDataCell data in m_Data)
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
                        Change_TaskInfomationText(data);
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
                        Change_TaskInfomationText(data);
                        alarm = true;
                    }
                }

                // 기한 설정 체크
                if (data.DC_deadlineType > 0)
                {
                    tt = data.DC_deadlineTime;
                    result = DateTime.Compare(dt, tt);

                    if (result > 0) // 날짜 지남
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

                        data.DC_deadlineType = 0;
                        data.DC_deadlineTime = default;
                        Change_TaskInfomationText(data);
                        alarm = true;
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
                            m_selected_menu = (int)MenuList.MYTODAY_MENU;
                            Menu_MyToday();
                            break;
                        case "중요":
                            m_selected_menu = (int)MenuList.IMPORTANT_MENU;
                            Menu_Important();
                            break;
                        case "계획된 일정":
                            m_selected_menu = (int)MenuList.DEADLINE_MENU;
                            Menu_Planned();
                            break;
                        case "완료됨":
                            m_selected_menu = (int)MenuList.COMPLETE_MENU;
                            Menu_Completed();
                            break;
                        case "작업":
                            m_selected_menu = (int)MenuList.TODO_ITEM_MENU;
                            Menu_Task();
                            break;
                        default:
                            m_selected_menu = (int)MenuList.LIST_MENU;
                            Menu_List(item);
                            break;
                    }
                }
            }
            Set_TodoItem_Width();
            Update_Metadata();
        }

        private void Change_TaskInfomationText(CDataCell dc)
        {
            foreach (Todo_Item item in flowLayoutPanel2.Controls)  // dc로 td 찾기
            {
                if (dc.Equals(item.TD_DataCell))
                {
                    item.TD_infomation = MakeInfoTextFromDataCell(dc);
                    item.Refresh();
                    break;
                }
            }
        }

        private string MakeInfoTextFromDataCell(CDataCell dt)
        {
            string infoText = "";

            switch (m_selected_menu)
            {
                case (int)MenuList.LOGIN_SETTING_MENU:     // 로그인 메뉴에서 입력됨
                    break;
                case (int)MenuList.MYTODAY_MENU:     // 오늘 할 일 메뉴에서 표시됨
                    if (dt.DC_listName != "작업") infoText += "<" + dt.DC_listName + "> ";
                    break;
                case (int)MenuList.IMPORTANT_MENU:     // 중요 메뉴에서 표시됨
                    if (dt.DC_listName != "작업") infoText += "<" + dt.DC_listName + "> ";
                    break;
                case (int)MenuList.DEADLINE_MENU:     // 계획된 일정 메뉴에서 표시됨
                    if (dt.DC_listName != "작업") infoText += "<" + dt.DC_listName + "> ";
                    break;
                case (int)MenuList.COMPLETE_MENU:     // 완료됨 메뉴에서 표시됨
                    if (dt.DC_listName != "작업") infoText += "<" + dt.DC_listName + "> ";
                    break;
                case (int)MenuList.TODO_ITEM_MENU:     // 작업 메뉴에서 표시됨
                    break;
                case (int)MenuList.RESERVED_MENU:     // RESERVED MENU
                    break;
                case (int)MenuList.LIST_MENU:     // 새목록 메뉴에서 표시됨
                    break;
                default:
                    break;
            }

            if (dt.DC_myToday) infoText += " [오늘 할일]";

            switch (dt.DC_remindType)
            {
                case 1:
                    infoText = infoText + " [알람]" + dt.DC_remindTime.ToString("MM/dd(ddd)tthh:mm");
                    break;
                case 2:
                    infoText = infoText + " [알람]" + dt.DC_remindTime.ToString("MM/dd(ddd)tthh:mm");
                    break;
                case 3:
                    infoText = infoText + " [알람]" + dt.DC_remindTime.ToString("MM/dd(ddd)tthh:mm");
                    break;
                case 4:
                    infoText = infoText + " [알람]" + dt.DC_remindTime.ToString("MM/dd(ddd)tthh:mm");
                    break;
                default:
                    break;
            }

            switch (dt.DC_deadlineType)
            {
                case 1:
                    infoText = infoText + " [기한]" + dt.DC_deadlineTime.ToString("MM/dd(ddd)tthh:mm");
                    break;
                case 2:
                    infoText = infoText + " [기한]" + dt.DC_deadlineTime.ToString("MM/dd(ddd)tthh:mm");
                    break;
                case 3:
                    infoText = infoText + " [기한]" + dt.DC_deadlineTime.ToString("MM/dd(ddd)tthh:mm");
                    break;
                default:
                    break;
            }

            switch (dt.DC_repeatType)
            {
                case 1:
                    infoText = infoText + " [매일]";
                    break;
                case 2:
                    infoText = infoText + " [평일]";
                    break;
                case 3:
                    infoText = infoText + " [매주]";
                    break;
                case 4:
                    infoText = infoText + " [매월]";
                    break;
                case 5:
                    infoText = infoText + " [매년]";
                    break;
                case 6:
                    infoText = infoText + " [반복]";
                    break;
                default:
                    break;
            }
            return infoText;
        }

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