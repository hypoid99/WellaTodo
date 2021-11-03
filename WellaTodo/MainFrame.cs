﻿// copyright honeysoft v0.14 -> v0.7 -> v0.8

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

namespace WellaTodo
{
    public delegate void UserControl_Event(object sender, EventArgs e);

    public partial class MainFrame : Form, IView, IModelObserver
    {
        public event ViewHandler<IView> Changed_View_Event;

        static readonly string WINDOW_CAPTION = "Wella Todo v0.8";
        static readonly int WINDOW_WIDTH = 1000;
        static readonly int WINDOW_HEIGHT = 500;

        static readonly Color PSEUDO_BACK_COLOR = Color.White;
        static readonly Color PSEUDO_HIGHLIGHT_COLOR = Color.LightCyan;
        static readonly Color PSEUDO_SELECTED_COLOR = Color.Cyan;
        static readonly Color PSEUDO_TEXTBOX_BACK_COLOR = Color.LightCyan;
        //static readonly Color PSEUDO_DETAIL_WINDOW_BACK_COLOR = Color.White;
        static readonly Color PSEUDO_DETAIL_WINDOW_BACK_COLOR = Color.PapayaWhip;

        static readonly string FONT_NAME = "맑은고딕";
        static readonly float FONT_SIZE = 9.0f;

        static readonly int DETAIL_WINDOW_WIDTH = 260;
        static readonly int DETAIL_WINDOW_X1 = 5;
        static readonly int MENU_WINDOW_WIDTH = 150;

        public enum MenuList 
        { 
            LOGIN_SETTING_MENU      = 1,
            MYTODAY_MENU            = 2,
            IMPORTANT_MENU          = 3,
            DEADLINE_MENU           = 4,
            COMPLETE_MENU           = 5,
            TODO_ITEM_MENU          = 6,
            RESERVED_MENU           = 7
        }

        MainController m_Controller;
        List<CDataCell> m_Data = new List<CDataCell>();

        LoginSettingForm loginSettingForm = new LoginSettingForm();
        AlarmForm alarmForm = new AlarmForm();
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

        bool isDetailWindowOpen = false;
        bool isTextboxClicked = false;

        int m_present_data_position = -1; // 초기값 설정
        int m_before_data_position;
        int m_selectedMainMenu = 6; // 초기 작업 메뉴 설정

        public MainFrame()
        {
            InitializeComponent();
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
            Load_File();

            Initiate_View();
            Initiate_Item();

            m_selectedMainMenu = 6; // 작업
            Changed_MainMenu();

            timer1.Interval = 60000;
            timer1.Enabled = true;
        }

        private void MainFrame_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("저장할까요?", WINDOW_CAPTION, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Save_File();
            }
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

            labelUserName.Dock = DockStyle.Top;
            labelUserName.Text = "Noname";
            labelUserName.Size = new Size(labelUserName.Size.Width, 50);

            textBox4.Dock = DockStyle.Bottom;


            Image img = new Bitmap(Properties.Resources.outline_info_black_24dp);
            TwoLineList list1 = new TwoLineList(img, "primary1", "secondary", "meta");
            TwoLineList list2 = new TwoLineList(img, "primary2", "", "meta");
            TwoLineList list3 = new TwoLineList(img, "primary3", "secondary", "meta");
            flowLayoutPanel_Menulist.Visible = true;
            flowLayoutPanel_Menulist.Margin = new Padding(0);
            flowLayoutPanel_Menulist.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel_Menulist.WrapContents = false;
            flowLayoutPanel_Menulist.Width = splitContainer1.SplitterDistance;
            flowLayoutPanel_Menulist.Location = new Point(labelUserName.Location.X, labelUserName.Height);
            flowLayoutPanel_Menulist.Size = new Size(splitContainer1.SplitterDistance, splitContainer1.Panel1.Height - labelUserName.Height - textBox4.Height);
            flowLayoutPanel_Menulist.Controls.Add(list1);
            flowLayoutPanel_Menulist.Controls.Add(list2);
            flowLayoutPanel_Menulist.Controls.Add(list3);
            foreach (TwoLineList ctr in flowLayoutPanel_Menulist.Controls)
            {
                ctr.Width = flowLayoutPanel_Menulist.Width;
            }

            // 메뉴
            label1.Font = new Font(FONT_NAME, FONT_SIZE, FontStyle.Regular);
            label2.Font = new Font(FONT_NAME, FONT_SIZE, FontStyle.Regular);
            label3.Font = new Font(FONT_NAME, FONT_SIZE, FontStyle.Regular);
            label4.Font = new Font(FONT_NAME, FONT_SIZE, FontStyle.Regular);
            label5.Font = new Font(FONT_NAME, FONT_SIZE, FontStyle.Regular);
            label6.Font = new Font(FONT_NAME, FONT_SIZE, FontStyle.Regular);
            label7.Font = new Font(FONT_NAME, FONT_SIZE, FontStyle.Regular);

            flowLayoutPanel1.Visible = false;

            flowLayoutPanel1.Margin = new Padding(0);
            flowLayoutPanel1.Width = splitContainer1.SplitterDistance;
            flowLayoutPanel1.Location = new Point(labelUserName.Location.X, labelUserName.Height);
            flowLayoutPanel1.Size = new Size(splitContainer1.SplitterDistance, splitContainer1.Panel1.Height - labelUserName.Height - textBox4.Height);
            flowLayoutPanel1.BackColor = PSEUDO_BACK_COLOR;

            flowLayoutPanel1.AutoScroll = false;
            flowLayoutPanel1.HorizontalScroll.Maximum = 0;
            flowLayoutPanel1.HorizontalScroll.Enabled = false;
            flowLayoutPanel1.HorizontalScroll.Visible = false;
            flowLayoutPanel1.AutoScroll = true;

            flowLayoutPanel1.Controls.Add(label1);
            flowLayoutPanel1.Controls.Add(label2);
            flowLayoutPanel1.Controls.Add(label3);
            flowLayoutPanel1.Controls.Add(label4);
            flowLayoutPanel1.Controls.Add(label5);
            flowLayoutPanel1.Controls.Add(label6);
            flowLayoutPanel1.Controls.Add(label7);

            foreach (Label ctr in flowLayoutPanel1.Controls)
            {
                ctr.Width = flowLayoutPanel1.Width - 2;
                ctr.BackColor = PSEUDO_BACK_COLOR;
            }

            splitContainer2.SplitterDistance = splitContainer2.Width;
            splitContainer2.IsSplitterFixed = true;
            splitContainer2.Location = new Point(0, 0);

            splitContainer2.Size = new Size(splitContainer1.Panel2.Width - 5, splitContainer1.Panel2.Height - 50);
            splitContainer2.Panel2.BackColor = PSEUDO_DETAIL_WINDOW_BACK_COLOR;

            // 태스크 항목
            flowLayoutPanel2.AutoScroll = false;
            flowLayoutPanel2.HorizontalScroll.Maximum = 0;
            flowLayoutPanel2.HorizontalScroll.Enabled = false;
            flowLayoutPanel2.HorizontalScroll.Visible = false;
            flowLayoutPanel2.AutoScroll = true;

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
            roundCheckbox1.BackColor = PSEUDO_DETAIL_WINDOW_BACK_COLOR;
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
            starCheckbox1.BackColor = PSEUDO_DETAIL_WINDOW_BACK_COLOR;
            splitContainer2.Panel2.Controls.Add(starCheckbox1);

            roundLabel1.MouseClick += new MouseEventHandler(roundLabel1_Click);
            roundLabel1.MouseEnter += new EventHandler(roundLabel1_MouseEnter);
            roundLabel1.MouseLeave += new EventHandler(roundLabel1_MouseLeave);
            roundLabel1.Text = "나의 하루에 추가";
            roundLabel1.Location = new Point(DETAIL_WINDOW_X1 + 15, 40);
            roundLabel1.Size = new Size(DETAIL_WINDOW_WIDTH - 45, 30);
            roundLabel1.BackColor = PSEUDO_DETAIL_WINDOW_BACK_COLOR;
            splitContainer2.Panel2.Controls.Add(roundLabel1);

            roundLabel2.MouseClick += new MouseEventHandler(roundLabel2_Click);
            roundLabel2.MouseEnter += new EventHandler(roundLabel2_MouseEnter);
            roundLabel2.MouseLeave += new EventHandler(roundLabel2_MouseLeave);
            roundLabel2.Text = "미리 알림";
            roundLabel2.Location = new Point(DETAIL_WINDOW_X1 + 15, 75);
            roundLabel2.Size = new Size(DETAIL_WINDOW_WIDTH - 45, 30);
            roundLabel2.BackColor = PSEUDO_DETAIL_WINDOW_BACK_COLOR;
            splitContainer2.Panel2.Controls.Add(roundLabel2);

            roundLabel3.MouseClick += new MouseEventHandler(roundLabel3_Click);
            roundLabel3.MouseEnter += new EventHandler(roundLabel3_MouseEnter);
            roundLabel3.MouseLeave += new EventHandler(roundLabel3_MouseLeave);
            roundLabel3.Text = "기한 설정";
            roundLabel3.Location = new Point(DETAIL_WINDOW_X1 + 15, 110);
            roundLabel3.Size = new Size(DETAIL_WINDOW_WIDTH - 45, 30);
            roundLabel3.BackColor = PSEUDO_DETAIL_WINDOW_BACK_COLOR;
            splitContainer2.Panel2.Controls.Add(roundLabel3);

            roundLabel4.MouseClick += new MouseEventHandler(roundLabel4_Click);
            roundLabel4.MouseEnter += new EventHandler(roundLabel4_MouseEnter);
            roundLabel4.MouseLeave += new EventHandler(roundLabel4_MouseLeave);
            roundLabel4.Text = "반복";
            roundLabel4.Location = new Point(DETAIL_WINDOW_X1 + 15, 145);
            roundLabel4.Size = new Size(DETAIL_WINDOW_WIDTH - 45, 30);
            roundLabel4.BackColor = PSEUDO_DETAIL_WINDOW_BACK_COLOR;
            splitContainer2.Panel2.Controls.Add(roundLabel4);

            textBox1.Multiline = true;
            textBox1.Location = new Point(DETAIL_WINDOW_X1 + 5, 185);
            textBox1.Size = new Size(DETAIL_WINDOW_WIDTH - 25, 130);

            createDateLabel.Text = " 생성됨";
            createDateLabel.Location = new Point(DETAIL_WINDOW_X1 + 10, 325);
            createDateLabel.Size = new Size(100, 50);
            createDateLabel.BackColor = PSEUDO_DETAIL_WINDOW_BACK_COLOR;
            splitContainer2.Panel2.Controls.Add(createDateLabel);

            upArrow.Click += new EventHandler(upArrow_Click);
            upArrow.MouseEnter += new EventHandler(upArrow_MouseEnter);
            upArrow.MouseLeave += new EventHandler(upArrow_MouseLeave);
            upArrow.Text = "위로";
            upArrow.Location = new Point(DETAIL_WINDOW_X1 + 115, 320);
            upArrow.Size = new Size(60, 30);
            upArrow.BackColor = PSEUDO_DETAIL_WINDOW_BACK_COLOR;
            splitContainer2.Panel2.Controls.Add(upArrow);

            downArrow.Click += new EventHandler(downArrow_Click);
            downArrow.MouseEnter += new EventHandler(downArrow_MouseEnter);
            downArrow.MouseLeave += new EventHandler(downArrow_MouseLeave);
            downArrow.Text = "아래로";
            downArrow.Location = new Point(DETAIL_WINDOW_X1 + 180, 320);
            downArrow.Size = new Size(60, 30);
            downArrow.BackColor = PSEUDO_DETAIL_WINDOW_BACK_COLOR;
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
            flowLayoutPanel_Menulist.Width = splitContainer1.SplitterDistance;
            flowLayoutPanel_Menulist.Height = splitContainer1.Panel1.Height - labelUserName.Height - textBox4.Height;

            foreach (TwoLineList ctr in flowLayoutPanel_Menulist.Controls)
            {
                ctr.Width = flowLayoutPanel_Menulist.Width - 2;
            }

            flowLayoutPanel1.Width = splitContainer1.SplitterDistance;
            flowLayoutPanel1.Height = splitContainer1.Panel1.Height - labelUserName.Height - textBox4.Height;

            foreach (Label ctr in flowLayoutPanel1.Controls)
            {
                ctr.Width = flowLayoutPanel1.Width - 2;
            }

            splitContainer2.Size = new Size(splitContainer1.Panel2.Width - 5, splitContainer1.Panel2.Height - 50);

            textBox2.Location = new Point(10, splitContainer1.Panel2.Height - 35);
            textBox2.Size = new Size(splitContainer1.Panel2.Width - 20, 25);

            if (isDetailWindowOpen)
            {
                int width = splitContainer2.Width - DETAIL_WINDOW_WIDTH;
                splitContainer2.SplitterDistance = width < 0 ? 1 : width;
            }

            Set_TodoItem_Width();
        }

        //--------------------------------------------------------------
        // 할일 항목 폭 맞추기
        //--------------------------------------------------------------
        private void Set_TodoItem_Width()
        {
            int pos = 0;
            foreach (Todo_Item item in flowLayoutPanel2.Controls)
            {
                item.Width = flowLayoutPanel2.VerticalScroll.Visible
                    ? flowLayoutPanel2.Width - 5 - SystemInformation.VerticalScrollBarWidth
                    : flowLayoutPanel2.Width - 5;
                item.IsItemSelected = m_present_data_position == pos;
                pos++;
            }
            Display_Data();
        }

        private void CloseDetailWindow()
        {
            splitContainer2.SplitterDistance = splitContainer2.Width;
            isDetailWindowOpen = false;
        }

        private void Display_Data()
        {
            int pos;
            string txt;
            List<string> outText = new List<string>();
            if (outputForm.Visible)
            {
                pos = 0;
                foreach (Todo_Item item in flowLayoutPanel2.Controls)
                {
                    txt = ">Data TD:[" + pos + "] " + item.TD_title + "<----->";
                    outText.Add(txt);
                    pos++;
                }

                pos = 0;
                foreach (CDataCell data in m_Data)
                {
                    txt = outText[pos];
                    txt = txt + ">Data DC:[" + pos + "] " + data.DC_title + "\r\n";
                    outputForm.TextBoxString = txt;
                    pos++;
                }
                outputForm.TextBoxString = ">Data Present Position:[" + m_present_data_position + "]-------" + "\r\n";
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
        // 할일 항목 초기 데이타 로딩
        //--------------------------------------------------------------
        private void Initiate_Item()
        {
            string text;
            bool chk_complete;
            bool chk_important;

            foreach (CDataCell data in m_Data)
            {
                text = data.DC_title;
                chk_complete = data.DC_complete;
                chk_important = data.DC_important;
                Todo_Item item = new Todo_Item(text, chk_complete, chk_important);
                flowLayoutPanel2.Controls.Add(item);
                item.UserControl_Click += new UserControl_Event(TodoItem_UserControl_Click);
            }

            int pos = 0;
            foreach (Todo_Item item in flowLayoutPanel2.Controls)
            {
                item.TD_infomation = MakeInformationText(pos);
                pos++;
            }

            Set_TodoItem_Width();
        }

        //--------------------------------------------------------------
        // 할일 항목 추가
        //--------------------------------------------------------------
        private void Add_Item(string text)
        {
            //m_Controller.performAddItem();
            DateTime dt = DateTime.Now;
            m_Data.Insert(0, new CDataCell(text));
            m_Data[0].DC_dateCreated = dt;

            Todo_Item item = new Todo_Item(text, false, false);
            flowLayoutPanel2.Controls.Add(item);
            flowLayoutPanel2.Controls.SetChildIndex(item, 0);

            item.UserControl_Click += new UserControl_Event(TodoItem_UserControl_Click);

            flowLayoutPanel2.VerticalScroll.Value = 0;
            m_present_data_position = 0;

            switch (m_selectedMainMenu)
            {
                case 1:     // 로그인 메뉴에서 입력됨
                    break;
                case 2:     // 오늘 할 일 메뉴에서 입력됨
                    m_Data[0].DC_myToday = true;
                    m_Data[0].DC_myTodayTime = new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59);
                    item.TD_infomation = MakeInformationText(0);
                    break;
                case 3:     // 중요 메뉴에서 입력됨
                    m_Data[0].DC_important = true;
                    item.TD_important = true;
                    item.TD_infomation = MakeInformationText(0);
                    break;
                case 4:     // 계획된 일정 메뉴에서 입력됨
                    m_Data[0].DC_deadlineType = 1;
                    m_Data[0].DC_deadlineTime = new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59);
                    item.TD_infomation = MakeInformationText(0);
                    break;
                case 5:     // 완료됨 메뉴에서 입력됨
                    break;
                case 6:     // 작업 메뉴에서 입력됨
                    break;
                case 7:     // 새목록 만들기 메뉴에서 입력됨
                    break;
                default:
                    break;
            }
            SendDataToDetailWindow();
            Set_TodoItem_Width();
        }

        //--------------------------------------------------------------
        // 할일 파일 로딩
        //--------------------------------------------------------------
        private void Load_File()
        {
            Stream rs = new FileStream("a.dat", FileMode.Open);
            BinaryFormatter deserializer = new BinaryFormatter();

            List<CDataCell> todo_data = (List<CDataCell>)deserializer.Deserialize(rs);
            rs.Close();

            foreach (CDataCell dt in todo_data)
            {
                m_Data.Insert(m_Data.Count, dt);
            }
        }

        //--------------------------------------------------------------
        // 할일 파일 세이브
        //--------------------------------------------------------------
        private void Save_File()
        {
            Stream ws = new FileStream("a.dat", FileMode.Create);
            BinaryFormatter serializer = new BinaryFormatter();

            serializer.Serialize(ws, m_Data);
            ws.Close();
        }

        //--------------------------------------------------------------
        // 할일 항목을 클릭했을때 처리
        //--------------------------------------------------------------
        private void TodoItem_UserControl_Click(object sender, EventArgs e)
        {
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

            foreach (Todo_Item item in flowLayoutPanel2.Controls)
            {
                if (item.Equals(sd))
                {
                    //완료됨 클릭시
                    if (item.IsCompleteClicked)
                    {
                        Complete_Process(item, pos);
                        if (m_selectedMainMenu == (int)MenuList.MYTODAY_MENU) label2_Click(sender, e); // 오늘할일 메뉴에서 실행
                        if (m_selectedMainMenu == (int)MenuList.IMPORTANT_MENU) label3_Click(sender, e); // 중요 메뉴에서 실행
                        if (m_selectedMainMenu == (int)MenuList.DEADLINE_MENU) label4_Click(sender, e); // 계획된 일정에서 실행
                        if (m_selectedMainMenu == (int)MenuList.TODO_ITEM_MENU) label5_Click(sender, e); // 완료됨에서 실행
                        break;
                    }

                    // 중요항목 클릭시
                    if (item.IsImportantClicked)
                    {
                        Important_Process(item, pos);
                        if (m_selectedMainMenu == (int)MenuList.IMPORTANT_MENU) label3_Click(sender, e); // 중요 메뉴에서 실행
                        break;
                    }

                    //Todo 아이템 클릭시
                    m_present_data_position = pos;
                    if (isDetailWindowOpen && (m_before_data_position == pos)) // 동일 항목 재클릭시 닫기
                    {
                        CloseDetailWindow();
                        break;
                    }
                    else
                    {
                        splitContainer2.SplitterDistance = splitContainer2.Width - DETAIL_WINDOW_WIDTH;
                        isDetailWindowOpen = true;

                        //Console.WriteLine("w : sc2[{0}] sc2-p1[{1}] sc2-p2[{2}] sd[{3}] flow[{4}]", splitContainer2.Width, splitContainer2.Panel1.Width , splitContainer2.Panel2.Width, splitContainer2.SplitterDistance, flowLayoutPanel2.Width);

                        SendDataToDetailWindow();
                        break;
                    }
                }
                pos++;
            }
            Set_TodoItem_Width();
        }

        private void Important_Process(Todo_Item item, int pos)
        {
            if (item.TD_important && !item.TD_complete)
            {
                flowLayoutPanel2.Controls.SetChildIndex(item, 0);

                m_Data[pos].DC_important = true;
                CDataCell dc = m_Data[pos]; //추출
                m_Data.RemoveAt(pos); //삭제
                m_Data.Insert(0, dc); //삽입

                flowLayoutPanel2.VerticalScroll.Value = 0;
                starCheckbox1.Checked = true;
                m_present_data_position = 0;
            }
            else if (item.TD_important && item.TD_complete)
            {
                m_Data[pos].DC_important = true;
                starCheckbox1.Checked = true;
                m_present_data_position = 0;
            }
            else if (!item.TD_important)
            {
                m_Data[pos].DC_important = false;
                starCheckbox1.Checked = false;
                m_present_data_position = 0;
            }
            SendDataToDetailWindow();
        }

        private void Complete_Process(Todo_Item item, int pos)
        {
            if (item.TD_complete)
            {
                int cnt = flowLayoutPanel2.Controls.Count;
                flowLayoutPanel2.Controls.SetChildIndex(item, cnt);

                m_Data[pos].DC_complete = true;
                CDataCell dc = m_Data[pos]; //추출
                m_Data.RemoveAt(pos); //삭제
                m_Data.Insert(m_Data.Count, dc); //삽입

                m_present_data_position = m_Data.Count - 1;
                roundCheckbox1.Checked = true;
            }
            else
            {
                flowLayoutPanel2.Controls.SetChildIndex(item, 0);

                m_Data[pos].DC_complete = false;
                CDataCell dc = m_Data[pos]; //추출
                m_Data.RemoveAt(pos); //삭제
                m_Data.Insert(0, dc); //삽입

                m_present_data_position = 0;
                roundCheckbox1.Checked = false;
            }
            SendDataToDetailWindow();
        }

        private void TodoItem_Right_Click(object sender, MouseEventArgs e)
        {
            int pos = 0;
            Todo_Item sd = (Todo_Item)sender;

            foreach (Todo_Item item in flowLayoutPanel2.Controls)
            {
                if (item.Equals(sd))
                {
                    m_present_data_position = pos;
                    break;
                }
                pos++;
            }
            SendDataToDetailWindow();

            ContextMenu todoItemContextMenu = new ContextMenu();
            todoItemContextMenu.Popup += new EventHandler(OnTodoItemPopupEvent);

            MenuItem myTodayItem = new MenuItem("나의 하루에 추가", new EventHandler(OnMyTodayMenuItem_Click));
            MenuItem importantItem = new MenuItem("중요로 표시", new EventHandler(OnImportantMenuItem_Click));
            MenuItem completeItem = new MenuItem("완료됨으로 표시", new EventHandler(OnCompleteMenuItem_Click));
            MenuItem toTodayItem = new MenuItem("오늘까지", new EventHandler(OnToTodayMenuItem_Click));
            MenuItem toTomorrowItem = new MenuItem("내일까지", new EventHandler(OnToTomorrowMenuItem_Click));
            MenuItem selectDayItem = new MenuItem("날짜 선택", new EventHandler(OnSelectDayMenuItem_Click));
            MenuItem deleteDeadlineItem = new MenuItem("기한 제거", new EventHandler(OnDeleteDeadlineMenuItem_Click));
            MenuItem menuEditItem = new MenuItem("메모 확장", new EventHandler(OnMemoEditMenuItem_Click));
            MenuItem deleteItem = new MenuItem("항목 삭제", new EventHandler(OnDeleteMenuItem_Click));

            todoItemContextMenu.MenuItems.Add(myTodayItem);
            todoItemContextMenu.MenuItems.Add(importantItem);
            todoItemContextMenu.MenuItems.Add(completeItem);
            todoItemContextMenu.MenuItems.Add("-");
            todoItemContextMenu.MenuItems.Add(toTodayItem);
            todoItemContextMenu.MenuItems.Add(toTomorrowItem);
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

            ctm.MenuItems[0].Text = m_Data[m_present_data_position].DC_myToday ? "나의 하루에서 제거" : "나의 하루에 추가";
            ctm.MenuItems[1].Text = m_Data[m_present_data_position].DC_important ? "중요도 제거" : "중요로 표시";
            ctm.MenuItems[2].Text = m_Data[m_present_data_position].DC_complete ? "완료되지 않음으로 표시" : "완료됨으로 표시";
            ctm.MenuItems[4].Enabled = m_Data[m_present_data_position].DC_deadlineType != 1;
            ctm.MenuItems[5].Enabled = m_Data[m_present_data_position].DC_deadlineType != 2;
            ctm.MenuItems[7].Enabled = m_Data[m_present_data_position].DC_deadlineType > 0;
        }

        private void OnMyTodayMenuItem_Click(object sender, EventArgs e)
        {
            Register_MyToday();
        }

        private void OnImportantMenuItem_Click(object sender, EventArgs e)
        {
            if (m_Data[m_present_data_position].DC_important)
            {
                starCheckbox1.Checked = false;
                m_Data[m_present_data_position].DC_important = false;
            } else
            {
                starCheckbox1.Checked = true;
                m_Data[m_present_data_position].DC_important = true;
            }

            int pos = 0;
            foreach (Todo_Item item in flowLayoutPanel2.Controls)
            {
                if (pos == m_present_data_position)
                {
                    item.TD_important = starCheckbox1.Checked;
                    Important_Process(item, pos);
                    if (m_selectedMainMenu == 3) label3_Click(sender, e); // 중요 메뉴에서 실행
                    break;
                }
                pos++;
            }
        }

        private void OnCompleteMenuItem_Click(object sender, EventArgs e)
        {
            if (m_Data[m_present_data_position].DC_complete)
            {
                roundCheckbox1.Checked = false;
                m_Data[m_present_data_position].DC_complete = false;
            }
            else
            {
                roundCheckbox1.Checked = true;
                m_Data[m_present_data_position].DC_complete = true;
            }

            int pos = 0;
            foreach (Todo_Item item in flowLayoutPanel2.Controls)
            {
                if (pos == m_present_data_position)
                {
                    item.TD_complete = roundCheckbox1.Checked;
                    Complete_Process(item, pos);
                    if (m_selectedMainMenu == 2) label2_Click(sender, e); // 오늘할일 메뉴에서 실행
                    if (m_selectedMainMenu == 3) label3_Click(sender, e); // 중요 메뉴에서 실행
                    if (m_selectedMainMenu == 4) label4_Click(sender, e); // 계획된 일정에서 실행
                    if (m_selectedMainMenu == 5) label5_Click(sender, e); // 완료됨에서 실행
                    break;
                }
                pos++;
            }
        }

        private void OnToTodayMenuItem_Click(object sender, EventArgs e)
        {
            Console.WriteLine("OnToTodayMenuItem_Click");
        }

        private void OnToTomorrowMenuItem_Click(object sender, EventArgs e)
        {
            Console.WriteLine("OnToTomorrowMenuItem_Click");
        }

        private void OnSelectDayMenuItem_Click(object sender, EventArgs e)
        {
            Console.WriteLine("OnSelectDayMenuItem_Click");
        }

        private void OnDeleteDeadlineMenuItem_Click(object sender, EventArgs e)
        {
            Console.WriteLine("OnDeleteDeadlineMenuItem_Click");
        }

        private void OnMemoEditMenuItem_Click(object sender, EventArgs e)
        {
            memoForm.StartPosition = FormStartPosition.Manual;
            memoForm.Location = new Point(Location.X + (Width - memoForm.Width) / 2, Location.Y + (Height - memoForm.Height) / 2);
            memoForm.TextBoxString = textBox1.Text;
            memoForm.Text = m_Data[m_present_data_position].DC_title;
            memoForm.ShowDialog();
            textBox1.Text = memoForm.TextBoxString;
            textBox1.SelectionStart = textBox1.Text.Length;

            m_Data[m_present_data_position].DC_memo = textBox1.Text;
        }

        private void OnDeleteMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("항목 삭제?", WINDOW_CAPTION, MessageBoxButtons.YesNo) == DialogResult.No) return;

            CloseDetailWindow();

            int pos = 0;
            foreach (Todo_Item item in flowLayoutPanel2.Controls)
            {
                if (m_present_data_position == pos)
                {
                    item.UserControl_Click -= new UserControl_Event(TodoItem_UserControl_Click);
                    splitContainer2.Panel1.Controls.Remove(item);
                    item.Dispose();

                    m_Data.Remove(m_Data[m_present_data_position]);
                    break;
                }
                pos++;
            }
            Set_TodoItem_Width();
        }

        private void SendDataToDetailWindow()
        {
            textBox3.Text = m_Data[m_present_data_position].DC_title;
            roundCheckbox1.Checked = m_Data[m_present_data_position].DC_complete;
            starCheckbox1.Checked = m_Data[m_present_data_position].DC_important;
            textBox1.Text = m_Data[m_present_data_position].DC_memo;
            textBox1.SelectionStart = textBox1.Text.Length;
            
            if (m_Data[m_present_data_position].DC_myToday)
            {
                roundLabel1.Text = "나의하루에 추가됨";
                roundLabel1.BackColor = PSEUDO_SELECTED_COLOR;

            } else
            {
                roundLabel1.Text = "나의하루에 추가";
                roundLabel1.BackColor = PSEUDO_DETAIL_WINDOW_BACK_COLOR;
            }

            if (m_Data[m_present_data_position].DC_remindType > 0)
            {
                roundLabel2.Text = "알림 설정됨";
                roundLabel2.BackColor = PSEUDO_SELECTED_COLOR;
            }
            else
            {
                roundLabel2.Text = "미리 알림";
                roundLabel2.BackColor = PSEUDO_DETAIL_WINDOW_BACK_COLOR;
            }

            if (m_Data[m_present_data_position].DC_deadlineType > 0)
            {
                roundLabel3.Text = "기한 설정됨";
                roundLabel3.BackColor = PSEUDO_SELECTED_COLOR;
            }
            else
            {
                roundLabel3.Text = "기한 설정";
                roundLabel3.BackColor = PSEUDO_DETAIL_WINDOW_BACK_COLOR;
            }

            if (m_Data[m_present_data_position].DC_repeatType > 0)
            {
                roundLabel4.Text = "반복 설정됨";
                roundLabel4.BackColor = PSEUDO_SELECTED_COLOR;
            }
            else
            {
                roundLabel4.Text = "반복";
                roundLabel4.BackColor = PSEUDO_DETAIL_WINDOW_BACK_COLOR;
            }

            createDateLabel.Text = m_Data[m_present_data_position].DC_dateCreated.ToString("yyyy-MM-dd(ddd)\r\n") 
                + "생성됨["+ m_present_data_position.ToString() + "]";

            m_before_data_position = m_present_data_position;
        }

        //
        // 스프릿컨테이너-1 이벤트
        //
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

        //
        // 메뉴 이벤트 처리 부분 ===================
        //
        private void label1_MouseEnter(object sender, EventArgs e)
        {
            label1.Font = new Font(FONT_NAME, FONT_SIZE, FontStyle.Underline);
            label1.BackColor = PSEUDO_HIGHLIGHT_COLOR;
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            label1.Font = new Font(FONT_NAME, FONT_SIZE, FontStyle.Regular);
            label1.BackColor = m_selectedMainMenu == (int)MenuList.LOGIN_SETTING_MENU ? PSEUDO_SELECTED_COLOR : PSEUDO_BACK_COLOR;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            int temp;
            temp = m_selectedMainMenu;
            m_selectedMainMenu = (int)MenuList.LOGIN_SETTING_MENU;
            Changed_MainMenu();

            loginSettingForm.StartPosition = FormStartPosition.CenterParent;
            loginSettingForm.ShowDialog();

            if (loginSettingForm.IsSaveClose)
            {
                if (MessageBox.Show("저장할까요?", WINDOW_CAPTION, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Save_File();
                }
            }
            
            switch (loginSettingForm.ColorTheme)
            {
                case 1:
                    break;
                case 2:
                    break;
            }

            m_selectedMainMenu = temp;
            Changed_MainMenu();
        }

        private void label2_MouseEnter(object sender, EventArgs e)
        {
            label2.Font = new Font(FONT_NAME, FONT_SIZE, FontStyle.Underline);
            label2.BackColor = PSEUDO_HIGHLIGHT_COLOR;
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            label2.Font = new Font(FONT_NAME, FONT_SIZE, FontStyle.Regular);
            label2.BackColor = m_selectedMainMenu == (int)MenuList .MYTODAY_MENU ? PSEUDO_SELECTED_COLOR : PSEUDO_BACK_COLOR;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            CloseDetailWindow();

            flowLayoutPanel2.AutoScroll = false;

            for (int i = flowLayoutPanel2.Controls.Count - 1; i >= 0; i--)
            {
                Todo_Item item = (Todo_Item)flowLayoutPanel2.Controls[i];
                if (!m_Data[i].DC_myToday || item.TD_complete)
                    item.Visible = false;
                else
                    item.Visible = true;
            }

            Set_TodoItem_Width();
            flowLayoutPanel2.AutoScroll = true;

            m_selectedMainMenu = (int)MenuList.MYTODAY_MENU; // 오늘 할 일
            Changed_MainMenu();
        }

        private void label3_MouseEnter(object sender, EventArgs e)
        {
            label3.Font = new Font(FONT_NAME, FONT_SIZE, FontStyle.Underline);
            label3.BackColor = PSEUDO_HIGHLIGHT_COLOR;
        }

        private void label3_MouseLeave(object sender, EventArgs e)
        {
            label3.Font = new Font(FONT_NAME, FONT_SIZE, FontStyle.Regular);
            label3.BackColor = m_selectedMainMenu == (int)MenuList.IMPORTANT_MENU ? PSEUDO_SELECTED_COLOR : PSEUDO_BACK_COLOR;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            CloseDetailWindow();

            flowLayoutPanel2.AutoScroll = false;

            for (int i = flowLayoutPanel2.Controls.Count - 1; i >= 0; i--)
            {
                Todo_Item item = (Todo_Item)flowLayoutPanel2.Controls[i];
                if (!item.TD_important || item.TD_complete)
                    item.Visible = false;
                else
                    item.Visible = true;
            }

            Set_TodoItem_Width();
            flowLayoutPanel2.AutoScroll = true;
            m_selectedMainMenu = (int)MenuList.IMPORTANT_MENU; // 중요
            Changed_MainMenu();
        }

        private void label4_MouseEnter(object sender, EventArgs e)
        {
            label4.Font = new Font(FONT_NAME, FONT_SIZE, FontStyle.Underline);
            label4.BackColor = PSEUDO_HIGHLIGHT_COLOR;
        }

        private void label4_MouseLeave(object sender, EventArgs e)
        {
            label4.Font = new Font(FONT_NAME, FONT_SIZE, FontStyle.Regular);
            label4.BackColor = m_selectedMainMenu == (int)MenuList.DEADLINE_MENU ? PSEUDO_SELECTED_COLOR : PSEUDO_BACK_COLOR;
        }

        private void label4_Click(object sender, EventArgs e)
        {
            CloseDetailWindow();
            flowLayoutPanel2.AutoScroll = false;
            int pos = 0;
            int sum;
            foreach (Todo_Item item in flowLayoutPanel2.Controls)
            {
                sum = m_Data[pos].DC_remindType + m_Data[pos].DC_deadlineType + m_Data[pos].DC_repeatType;
                if ((!m_Data[pos].DC_myToday && sum == 0) || item.TD_complete)
                    item.Visible = false;
                else
                    item.Visible = true;
                pos++;
            }

            Set_TodoItem_Width();
            flowLayoutPanel2.AutoScroll = true;
            m_selectedMainMenu = (int)MenuList.DEADLINE_MENU; //계획된 일정
            Changed_MainMenu();
        }

        private void label5_MouseEnter(object sender, EventArgs e)
        {
            label5.Font = new Font(FONT_NAME, FONT_SIZE, FontStyle.Underline);
            label5.BackColor = PSEUDO_HIGHLIGHT_COLOR;
        }

        private void label5_MouseLeave(object sender, EventArgs e)
        {
            label5.Font = new Font(FONT_NAME, FONT_SIZE, FontStyle.Regular);
            label5.BackColor = m_selectedMainMenu == (int)MenuList.COMPLETE_MENU ? PSEUDO_SELECTED_COLOR : PSEUDO_BACK_COLOR;
        }

        private void label5_Click(object sender, EventArgs e)
        {
            CloseDetailWindow();
            flowLayoutPanel2.AutoScroll = false;

            for (int i = flowLayoutPanel2.Controls.Count - 1; i >= 0; i--)
            {
                Todo_Item item = (Todo_Item)flowLayoutPanel2.Controls[i];
                if (!item.TD_complete)
                    item.Visible = false;
                else
                    item.Visible = true;
            }

            Set_TodoItem_Width();
            flowLayoutPanel2.AutoScroll = true;
            m_selectedMainMenu = (int)MenuList.COMPLETE_MENU; // 완료됨
            Changed_MainMenu();
        }

        private void label6_MouseEnter(object sender, EventArgs e)
        {
            label6.Font = new Font(FONT_NAME, FONT_SIZE, FontStyle.Underline);
            label6.BackColor = PSEUDO_HIGHLIGHT_COLOR;
        }

        private void label6_MouseLeave(object sender, EventArgs e)
        {
            label6.Font = new Font(FONT_NAME, FONT_SIZE, FontStyle.Regular);
            label6.BackColor = m_selectedMainMenu == (int)MenuList.TODO_ITEM_MENU ? PSEUDO_SELECTED_COLOR : PSEUDO_BACK_COLOR;
        }

        private void label6_Click(object sender, EventArgs e)
        {
            CloseDetailWindow();
            flowLayoutPanel2.AutoScroll = false;
            foreach (Todo_Item item in flowLayoutPanel2.Controls)
            {
                item.Visible = true;
            }

            /* LINQ Query
            var subset = from Todo_Item item in flowLayoutPanel2.Controls
                         where item.TD_complete == true
                         select item;
            foreach (Todo_Item item in subset)
            {
                Console.WriteLine(item.TD_title.ToString () + "is compelte" + item.TD_complete.ToString());
            }
            */

            Set_TodoItem_Width();
            flowLayoutPanel2.AutoScroll = true;
            m_selectedMainMenu = (int)MenuList.TODO_ITEM_MENU; // 모든 작업
            Changed_MainMenu();
        }

        private void label7_MouseEnter(object sender, EventArgs e)
        {
            label7.Font = new Font(FONT_NAME, FONT_SIZE, FontStyle.Underline);
            label7.BackColor = PSEUDO_HIGHLIGHT_COLOR;
        }

        private void label7_MouseLeave(object sender, EventArgs e)
        {
            label7.Font = new Font(FONT_NAME, FONT_SIZE, FontStyle.Regular);
            label7.BackColor = m_selectedMainMenu == (int)MenuList.RESERVED_MENU ? PSEUDO_SELECTED_COLOR : PSEUDO_BACK_COLOR;
        }

        private void label7_Click(object sender, EventArgs e)
        {
            int temp;
            temp = m_selectedMainMenu;
            m_selectedMainMenu = (int)MenuList.RESERVED_MENU; // 새목록 만들기
            Changed_MainMenu();

            outputForm.StartPosition = FormStartPosition.Manual;
            outputForm.Location = new Point(Location.X + (Width - outputForm.Width) / 2, Location.Y + (Height - outputForm.Height) / 2);

            if (outputForm.Visible)
                outputForm.Hide();
            else
                outputForm.Show();

            m_selectedMainMenu = temp;
            Changed_MainMenu();
        }

        //
        // 메뉴별 색상 및 위/아래 이동 버튼 처리
        //
        private void Changed_MainMenu()
        {
            int pos = 1;
            foreach (Label ctr in flowLayoutPanel1.Controls)
            {
                if (pos == m_selectedMainMenu)
                    ctr.BackColor = PSEUDO_SELECTED_COLOR;
                else
                    ctr.BackColor = PSEUDO_BACK_COLOR;
                pos++;
            }

            if (m_selectedMainMenu == 6) // 작업 메뉴에만 위아래 버튼 보이기
            {
                upArrow.Visible = true;
                downArrow.Visible = true;
            }
            else
            {
                upArrow.Visible = false;
                downArrow.Visible = false;
            }
        }

        //
        // 할일 입력 처리 부분 -----------------
        //

        //할일 입력창
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
                Add_Item(textBox2.Text);
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

        //
        // 상세창 처리 부분 (제목 / 완료 / 중요 / 메모 / 위아래 / 닫기 / 삭제)
        //

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
                    textBox3.Text = m_Data[m_present_data_position].DC_title;
                    return;
                }

                m_Data[m_present_data_position].DC_title = textBox3.Text;

                int pos = 0;
                foreach (Todo_Item item in flowLayoutPanel2.Controls)
                {
                    if (pos == m_present_data_position)
                    {
                        item.TD_title = textBox3.Text;
                        break;
                    }
                    pos++;
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
                textBox3.Text = m_Data[m_present_data_position].DC_title;
                return;
            }

            m_Data[m_present_data_position].DC_title = textBox3.Text;

            int pos = 0;
            foreach (Todo_Item item in flowLayoutPanel2.Controls)
            {
                if (pos == m_present_data_position)
                {
                    item.TD_title = textBox3.Text;
                    break;
                }
                pos++;
            }
        }

        // 상세창 메모 커서 벗어남
        private void textBox1_Leave(object sender, EventArgs e)
        {
            //메모 내용에 변경이 있는지 확인(?)
            m_Data[m_present_data_position].DC_memo = textBox1.Text;
        }

        private void textBox1_MouseLeave(object sender, EventArgs e)
        {
            m_Data[m_present_data_position].DC_memo = textBox1.Text;
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

            m_Data[m_present_data_position].DC_memo = textBox1.Text;
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

            int pos = 0;
            if (isDetailWindowOpen)
            {
                foreach (Todo_Item item in flowLayoutPanel2.Controls)
                {
                    if (pos == m_present_data_position)
                    {
                        item.UserControl_Click -= new UserControl_Event(TodoItem_UserControl_Click);
                        splitContainer2.Panel1.Controls.Remove(item);
                        item.Dispose();

                        m_Data.Remove(m_Data[pos]);
                        break;
                    }
                    pos++;
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
                m_Data[m_present_data_position].DC_complete = roundCheckbox1.Checked;

                int pos = 0;
                foreach (Todo_Item item in flowLayoutPanel2.Controls)
                {
                    if (m_present_data_position == pos)
                    {
                        item.TD_complete = roundCheckbox1.Checked;
                        Complete_Process(item, pos);
                        break;
                    }
                    pos++;
                }
            }
        }

        private void roundCheckbox1_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void roundCheckbox1_MouseEnter(object sender, EventArgs e)
        {
            
        }

        private void roundCheckbox1_MouseLeave(object sender, EventArgs e)
        {
            roundCheckbox1.BackColor = PSEUDO_DETAIL_WINDOW_BACK_COLOR;
        }

        //
        //상세창 중요 체크시
        //
        private void starCheckbox1_MouseClick(object sender, EventArgs e)
        {
            if (isDetailWindowOpen)
            {
                m_Data[m_present_data_position].DC_important = starCheckbox1.Checked;

                int pos = 0;
                foreach (Todo_Item item in flowLayoutPanel2.Controls)
                {
                    if (m_present_data_position == pos)
                    {
                        item.TD_important = starCheckbox1.Checked;
                        Important_Process(item, pos);
                        break;
                    }
                    pos++;
                }
            }
        }

        private void starCheckbox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void starCheckbox1_MouseEnter(object sender, EventArgs e)
        {
            
        }

        private void starCheckbox1_MouseLeave(object sender, EventArgs e)
        {
            starCheckbox1.BackColor = PSEUDO_DETAIL_WINDOW_BACK_COLOR;
        }

        //
        // 상세창 - 나의 하루에 추가 메뉴
        //
        private void roundLabel1_Click(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            Register_MyToday();
        }

        private void Register_MyToday()
        {
            if (m_Data[m_present_data_position].DC_myToday)
            {
                m_Data[m_present_data_position].DC_myToday = false;
                m_Data[m_present_data_position].DC_myTodayTime = default;
                roundLabel1.Text = "나의 하루에 추가";
                roundLabel1.BackColor = PSEUDO_DETAIL_WINDOW_BACK_COLOR;
            }
            else
            {
                m_Data[m_present_data_position].DC_myToday = true;
                DateTime dt = DateTime.Now;
                m_Data[m_present_data_position].DC_myTodayTime = new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59);
                roundLabel1.Text = "나의 하루에 추가됨";
                roundLabel1.BackColor = PSEUDO_SELECTED_COLOR;
            }
            SetInformationText();
        }

        private void roundLabel1_MouseEnter(object sender, EventArgs e)
        {
            roundLabel1.BackColor = PSEUDO_HIGHLIGHT_COLOR;
        }

        private void roundLabel1_MouseLeave(object sender, EventArgs e)
        {
            roundLabel1.BackColor = m_Data[m_present_data_position].DC_myToday ? PSEUDO_SELECTED_COLOR : PSEUDO_DETAIL_WINDOW_BACK_COLOR;
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
            roundLabel2.BackColor = m_Data[m_present_data_position].DC_remindType > 0 ? PSEUDO_SELECTED_COLOR : PSEUDO_DETAIL_WINDOW_BACK_COLOR;
        }

        private void roundLabel2_Click(object sender, MouseEventArgs e)
        {
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
            m_Data[m_present_data_position].DC_remindType = 1;

            DateTime dt = DateTime.Now;
            dt = dt.Minute < 30 ? dt.AddHours(3) : dt.AddHours(4);
            m_Data[m_present_data_position].DC_remindTime = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, 0, 0);
            roundLabel2.Text = "알림 설정됨";
            roundLabel2.BackColor = PSEUDO_SELECTED_COLOR;
            SetInformationText();
        }

        private void OnTomorrowRemind_Click(object sender, EventArgs e)
        {
            m_Data[m_present_data_position].DC_remindType = 2;

            DateTime dt = DateTime.Now;
            dt = dt.AddDays(1);
            m_Data[m_present_data_position].DC_remindTime = new DateTime(dt.Year, dt.Month, dt.Day, 08, 00, 00);
            roundLabel2.Text = "알림 설정됨";
            roundLabel2.BackColor = PSEUDO_SELECTED_COLOR;
            SetInformationText();
        }

        private void OnNextWeekRemind_Click(object sender, EventArgs e)
        {
            m_Data[m_present_data_position].DC_remindType = 3;

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
            m_Data[m_present_data_position].DC_remindTime = new DateTime(dt.Year, dt.Month, dt.Day, 08, 00, 00);
            roundLabel2.Text = "알림 설정됨";
            roundLabel2.BackColor = PSEUDO_SELECTED_COLOR;
            SetInformationText();
        }

        private void OnSelectRemind_Click(object sender, EventArgs e)
        {
            CarendarForm carendar = new CarendarForm();
            carendar.ShowDialog();
            if (carendar.IsSelected && (carendar.SelectedDateTime != default))
            {
                m_Data[m_present_data_position].DC_remindType = 4;
                m_Data[m_present_data_position].DC_remindTime = carendar.SelectedDateTime;
                roundLabel2.Text = "알림 설정됨";
                roundLabel2.BackColor = PSEUDO_SELECTED_COLOR;
                carendar.IsSelected = false;
            }
            else
            {
                m_Data[m_present_data_position].DC_remindType = 0;
                m_Data[m_present_data_position].DC_remindTime = default;
                roundLabel2.Text = "미리 알림";
                roundLabel2.BackColor = PSEUDO_DETAIL_WINDOW_BACK_COLOR;
            }
            SetInformationText();
        }

        private void OnDeleteRemind_Click(object sender, EventArgs e)
        {
            m_Data[m_present_data_position].DC_remindType = 0;
            m_Data[m_present_data_position].DC_remindTime = default;
            roundLabel2.Text = "미리 알림";
            roundLabel2.BackColor = PSEUDO_DETAIL_WINDOW_BACK_COLOR;
            SetInformationText();
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
            roundLabel3.BackColor = m_Data[m_present_data_position].DC_deadlineType > 0 ? PSEUDO_SELECTED_COLOR : PSEUDO_DETAIL_WINDOW_BACK_COLOR;
        }

        private void roundLabel3_Click(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            ContextMenu deadlineMenu = new ContextMenu();
            MenuItem todayDeadline = new MenuItem("오늘", new EventHandler(OnTodayDeadline_Click));
            MenuItem tomorrowDeadline = new MenuItem("내일", new EventHandler(OnTomorrowDeadline_Click));
            MenuItem selectDeadline = new MenuItem("날짜 선택", new EventHandler(OnSelectDeadline_Click));
            MenuItem deleteDeadline = new MenuItem("기한 설정 제거", new EventHandler(OnDeleteDeadline_Click));
            deadlineMenu.MenuItems.Add(todayDeadline);
            deadlineMenu.MenuItems.Add(tomorrowDeadline);
            deadlineMenu.MenuItems.Add(selectDeadline);
            deadlineMenu.MenuItems.Add(deleteDeadline);

            int px = splitContainer1.SplitterDistance + splitContainer2.SplitterDistance + 60;
            int py = 142;
            deadlineMenu.Show(this, new Point(px, py));
        }

        private void OnTodayDeadline_Click(object sender, EventArgs e)
        {
            m_Data[m_present_data_position].DC_deadlineType = 1;

            DateTime dt = DateTime.Now;
            m_Data[m_present_data_position].DC_deadlineTime = new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59);
            roundLabel3.Text = "기한 설정됨";
            roundLabel3.BackColor = PSEUDO_SELECTED_COLOR;
            SetInformationText();
        }

        private void OnTomorrowDeadline_Click(object sender, EventArgs e)
        {
            m_Data[m_present_data_position].DC_deadlineType = 2;

            DateTime dt = DateTime.Now;
            dt = dt.AddDays(1);
            m_Data[m_present_data_position].DC_deadlineTime = new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59);
            roundLabel3.Text = "기한 설정됨";
            roundLabel3.BackColor = PSEUDO_SELECTED_COLOR;
            SetInformationText();
        }

        private void OnSelectDeadline_Click(object sender, EventArgs e)
        {
            CarendarForm carendar = new CarendarForm();
            carendar.ShowDialog();
            if (carendar.IsSelected && (carendar.SelectedDateTime != default))
            {
                m_Data[m_present_data_position].DC_deadlineType = 3;
                m_Data[m_present_data_position].DC_deadlineTime = carendar.SelectedDateTime;
                roundLabel3.Text = "기한 설정됨";
                roundLabel3.BackColor = PSEUDO_SELECTED_COLOR;
                carendar.IsSelected = false;
            }
            else
            {
                m_Data[m_present_data_position].DC_deadlineType = 0;
                m_Data[m_present_data_position].DC_deadlineTime = default;
                roundLabel3.Text = "기한 설정";
                roundLabel3.BackColor = PSEUDO_DETAIL_WINDOW_BACK_COLOR;
            }
            SetInformationText();
        }

        private void OnDeleteDeadline_Click(object sender, EventArgs e)
        {
            m_Data[m_present_data_position].DC_deadlineType = 0;
            m_Data[m_present_data_position].DC_deadlineTime = default;
            roundLabel3.Text = "기한 설정";
            roundLabel3.BackColor = PSEUDO_DETAIL_WINDOW_BACK_COLOR;
            if (m_Data[m_present_data_position].DC_repeatType > 0) // 반복이 되어 있을때
            {
                m_Data[m_present_data_position].DC_repeatType = 0;
                m_Data[m_present_data_position].DC_repeatTime = default;
                roundLabel4.Text = "반복";
                roundLabel4.BackColor = PSEUDO_DETAIL_WINDOW_BACK_COLOR;
            }
            SetInformationText();
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
            roundLabel4.BackColor = m_Data[m_present_data_position].DC_repeatType > 0 ? PSEUDO_SELECTED_COLOR : PSEUDO_DETAIL_WINDOW_BACK_COLOR;
        }

        private void roundLabel4_Click(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            ContextMenu repeatMenu = new ContextMenu();
            MenuItem everyDayRepeat = new MenuItem("매일", new EventHandler(OnEveryDayRepeat_Click));
            MenuItem workingDayRepeat = new MenuItem("평일", new EventHandler(OnWorkingDayRepeat_Click));
            MenuItem everyWeekRepeat = new MenuItem("매주", new EventHandler(OnEveryWeekRepeat_Click));
            MenuItem everyMonthRepeat = new MenuItem("매월", new EventHandler(OnEveryMonthRepeat_Click));
            MenuItem everyYearRepeat = new MenuItem("매년", new EventHandler(OnEveryYearRepeat_Click));
            //MenuItem userDefineRepeat = new MenuItem("사용자 정의", new EventHandler(this.OnUserDefineRepeat_Click));
            MenuItem deleteRepeat = new MenuItem("반복 제거", new EventHandler(OnDeleteRepeat_Click));
            repeatMenu.MenuItems.Add(everyDayRepeat);
            repeatMenu.MenuItems.Add(workingDayRepeat);
            repeatMenu.MenuItems.Add(everyWeekRepeat);
            repeatMenu.MenuItems.Add(everyMonthRepeat);
            repeatMenu.MenuItems.Add(everyYearRepeat);
            //repeatMenu.MenuItems.Add(userDefineRepeat);
            repeatMenu.MenuItems.Add(deleteRepeat);

            int px = splitContainer1.SplitterDistance + splitContainer2.SplitterDistance + 60;
            int py = 177;
            repeatMenu.Show(this, new Point(px, py));
        }

        private void OnEveryDayRepeat_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            m_Data[m_present_data_position].DC_repeatType = 1;
            m_Data[m_present_data_position].DC_repeatTime = dt;
            roundLabel4.Text = "반복 설정됨";
            roundLabel4.BackColor = PSEUDO_SELECTED_COLOR;
            if (m_Data[m_present_data_position].DC_deadlineType == 0) // 기한설정이 되어 있지 않을때
            {
                m_Data[m_present_data_position].DC_deadlineType = 1;
                m_Data[m_present_data_position].DC_deadlineTime = new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59);
                roundLabel3.Text = "기한 설정됨";
                roundLabel3.BackColor = PSEUDO_SELECTED_COLOR;
            }
            SetInformationText();
        }

        private void OnWorkingDayRepeat_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            m_Data[m_present_data_position].DC_repeatType = 2;
            m_Data[m_present_data_position].DC_repeatTime = dt;
            roundLabel4.Text = "반복 설정됨";
            roundLabel4.BackColor = PSEUDO_SELECTED_COLOR;
            if (m_Data[m_present_data_position].DC_deadlineType == 0) // 기한설정이 되어 있지 않을때
            {
                m_Data[m_present_data_position].DC_deadlineType = 1;
                m_Data[m_present_data_position].DC_deadlineTime = new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59);
                roundLabel3.Text = "기한 설정됨";
                roundLabel3.BackColor = PSEUDO_SELECTED_COLOR;
            }
            SetInformationText();
        }

        private void OnEveryWeekRepeat_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            m_Data[m_present_data_position].DC_repeatType = 3;
            m_Data[m_present_data_position].DC_repeatTime = dt;
            roundLabel4.Text = "반복 설정됨";
            roundLabel4.BackColor = PSEUDO_SELECTED_COLOR;
            if (m_Data[m_present_data_position].DC_deadlineType == 0) // 기한설정이 되어 있지 않을때
            {
                m_Data[m_present_data_position].DC_deadlineType = 1;
                m_Data[m_present_data_position].DC_deadlineTime = new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59);
                roundLabel3.Text = "기한 설정됨";
                roundLabel3.BackColor = PSEUDO_SELECTED_COLOR;
            }
            SetInformationText();
        }

        private void OnEveryMonthRepeat_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            m_Data[m_present_data_position].DC_repeatType = 4;
            m_Data[m_present_data_position].DC_repeatTime = dt;
            roundLabel4.Text = "반복 설정됨";
            roundLabel4.BackColor = PSEUDO_SELECTED_COLOR;
            if (m_Data[m_present_data_position].DC_deadlineType == 0) // 기한설정이 되어 있지 않을때
            {
                m_Data[m_present_data_position].DC_deadlineType = 1;
                m_Data[m_present_data_position].DC_deadlineTime = new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59);
                roundLabel3.Text = "기한 설정됨";
                roundLabel3.BackColor = PSEUDO_SELECTED_COLOR;
            }
            SetInformationText();
        }

        private void OnEveryYearRepeat_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            m_Data[m_present_data_position].DC_repeatType = 5;
            m_Data[m_present_data_position].DC_repeatTime = dt;
            roundLabel4.Text = "반복 설정됨";
            roundLabel4.BackColor = PSEUDO_SELECTED_COLOR;
            if (m_Data[m_present_data_position].DC_deadlineType == 0) // 기한설정이 되어 있지 않을때
            {
                m_Data[m_present_data_position].DC_deadlineType = 1;
                m_Data[m_present_data_position].DC_deadlineTime = new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59);
                roundLabel3.Text = "기한 설정됨";
                roundLabel3.BackColor = PSEUDO_SELECTED_COLOR;
            }
            SetInformationText();
        }

        private void OnDeleteRepeat_Click(object sender, EventArgs e)
        {
            m_Data[m_present_data_position].DC_repeatType = 0;
            m_Data[m_present_data_position].DC_repeatTime = default;
            roundLabel4.Text = "반복";
            roundLabel4.BackColor = PSEUDO_DETAIL_WINDOW_BACK_COLOR;

            SetInformationText();
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
            upArrow.BackColor = PSEUDO_DETAIL_WINDOW_BACK_COLOR;
        }

        private void upArrow_Click(object sender, EventArgs e)
        {
            int pos = m_present_data_position;

            if (pos == 0) return;

            if (!m_Data[pos].DC_complete)
            {
                Control c = flowLayoutPanel2.Controls[pos];
                Todo_Item item = c as Todo_Item;
                flowLayoutPanel2.Controls.SetChildIndex(item, pos - 1);

                CDataCell dc = m_Data[pos]; //추출
                m_Data.RemoveAt(pos); //삭제
                m_Data.Insert(pos - 1, dc); //삽입

                m_present_data_position = pos - 1;
                SendDataToDetailWindow();
            }
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
            downArrow.BackColor = PSEUDO_DETAIL_WINDOW_BACK_COLOR;
        }

        private void downArrow_Click(object sender, EventArgs e)
        {
            int pos = m_present_data_position;

            if (pos == (m_Data.Count - 1)) return;

            if (m_Data[pos+1].DC_complete) return;

            if (!m_Data[pos].DC_complete)
            {
                Control c = flowLayoutPanel2.Controls[pos];
                Todo_Item item = c as Todo_Item;
                flowLayoutPanel2.Controls.SetChildIndex(item, pos + 1);

                CDataCell dc = m_Data[pos]; //추출
                m_Data.RemoveAt(pos); //삭제
                m_Data.Insert(pos + 1, dc); //삽입

                m_present_data_position = pos + 1;
                SendDataToDetailWindow();
            }
        }

        //
        // 알람 체크
        //
        private void AlarmCheck()
        {
            int result;
            string txt;
            DateTime dt = DateTime.Now;
            DateTime tt;

            alarmForm.StartPosition = FormStartPosition.Manual;
            alarmForm.Location = new Point(Location.X + (Width - alarmForm.Width) / 2, Location.Y + (Height - alarmForm.Height) / 2);

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
                        Console.WriteLine("MyToday - pos[{0}] today [{1}] target[{2}] result[{3}]", pos, dt.ToString("yyyy-MM-dd HH:mm:ss"), tt.ToString("yyyy-MM-dd HH:mm:ss"), result);

                        if (!alarmForm.Visible ) alarmForm.Show();
                        txt = "MyToday - pos:" + pos.ToString() + "today:" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "target:" + tt.ToString("yyyy-MM-dd HH:mm:ss") + "result:" + result.ToString() + "\r\n";
                        alarmForm.TextBoxString = txt;

                        data.DC_myToday = false;
                        data.DC_myTodayTime = default;
                        ChangeAlarmInformationText(pos);
                    }
                }

                // 미리 알림 체크
                if (data.DC_remindType > 0)
                {
                    tt = data.DC_remindTime;
                    result = DateTime.Compare(dt, tt);

                    if (result > 0) // 날짜 지남
                    {
                        Console.WriteLine("Remind - pos[{0}] today [{1}] target[{2}] result[{3}]", pos, dt.ToString("yyyy-MM-dd HH:mm:ss"), tt.ToString("yyyy-MM-dd HH:mm:ss"), result);

                        if (!alarmForm.Visible) alarmForm.Show();
                        txt = "Remind - pos:" + pos.ToString() + "today:" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "target:" + tt.ToString("yyyy-MM-dd HH:mm:ss") + "result:" + result.ToString() + "\r\n";
                        alarmForm.TextBoxString = txt;

                        data.DC_remindType = 0;
                        data.DC_remindTime = default;
                        ChangeAlarmInformationText(pos);
                    }
                }

                // 기한 설정 체크
                if (data.DC_deadlineType > 0)
                {
                    tt = data.DC_deadlineTime;
                    result = DateTime.Compare(dt, tt);

                    if (result > 0) // 날짜 지남
                    {
                        Console.WriteLine("Deadline - pos[{0}] today [{1}] target[{2}] result[{3}]", pos, dt.ToString("yyyy-MM-dd HH:mm:ss"), tt.ToString("yyyy-MM-dd HH:mm:ss"), result);

                        if (!alarmForm.Visible) alarmForm.Show();
                        txt = "Deadline - pos:" + pos.ToString() + "today:" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "target:" + tt.ToString("yyyy-MM-dd HH:mm:ss") + "result:" + result.ToString() + "\r\n";
                        alarmForm.TextBoxString = txt;

                        data.DC_deadlineType = 0;
                        data.DC_deadlineTime = default;
                        ChangeAlarmInformationText(pos);
                    }
                }

                // 반복 체크
                if (data.DC_repeatType > 0)
                {
                    tt = data.DC_repeatTime;
                    result = DateTime.Compare(dt, tt);

                    if (result > 0) // 날짜 지남
                    {
                        Console.WriteLine("Repeat - pos[{0}] today [{1}] target[{2}] result[{3}]", pos, dt.ToString("yyyy-MM-dd HH:mm:ss"), tt.ToString("yyyy-MM-dd HH:mm:ss"), result);

                        if (!alarmForm.Visible) alarmForm.Show();
                        txt = "Repeat - pos:" + pos.ToString() + "today:" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "target:" + tt.ToString("yyyy-MM-dd HH:mm:ss") + "result:" + result.ToString() + "\r\n";
                        alarmForm.TextBoxString = txt;

                        data.DC_repeatType = 0;
                        data.DC_repeatTime = default;
                        ChangeAlarmInformationText(pos);
                    }
                }
                pos++;
            }
            if (!alarmForm.Visible) alarmForm.Show();
            txt = "Now :" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "--------------------------------" + "\r\n";
            alarmForm.TextBoxString = txt;
        }

        private void ChangeAlarmInformationText(int pos)
        {
            string infoText = MakeInformationText(pos);
            int cnt = 0;
            foreach (Todo_Item item in flowLayoutPanel2.Controls)
            {
                if (pos == cnt)
                {
                    item.TD_infomation = infoText;
                    item.Refresh();
                    break;
                }
                cnt++;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            Text = WINDOW_CAPTION + " [" + dt.ToString("yyyy-MM-dd(ddd)") + "]";

            AlarmCheck();
        }

        private void SetInformationText()
        {
            string infoText = MakeInformationText(m_present_data_position);

            int pos = 0;
            foreach (Todo_Item item in flowLayoutPanel2.Controls)
            {
                if (m_present_data_position == pos)
                {
                    item.TD_infomation = infoText;
                    item.Refresh();
                    break;
                }
                pos++;
            }
        }

        private string MakeInformationText(int pos)
        {
            string infoText = "";

            if (m_Data[pos].DC_myToday) infoText += "[오늘 할일]";

            switch (m_Data[pos].DC_remindType)
            {
                case 1:
                    infoText = infoText + "[알람]" + m_Data[pos].DC_remindTime.ToString("MM/dd(ddd)tthh:mm");
                    break;
                case 2:
                    infoText = infoText + "[알람]" + m_Data[pos].DC_remindTime.ToString("MM/dd(ddd)tthh:mm");
                    break;
                case 3:
                    infoText = infoText + "[알람]" + m_Data[pos].DC_remindTime.ToString("MM/dd(ddd)tthh:mm");
                    break;
                case 4:
                    infoText = infoText + "[알람]" + m_Data[pos].DC_remindTime.ToString("MM/dd(ddd)tthh:mm");
                    break;
                default:
                    break;
            }

            switch (m_Data[pos].DC_deadlineType)
            {
                case 1:
                    infoText = infoText + "[기한]" + m_Data[pos].DC_deadlineTime.ToString("MM/dd(ddd)tthh:mm");
                    break;
                case 2:
                    infoText = infoText + "[기한]" + m_Data[pos].DC_deadlineTime.ToString("MM/dd(ddd)tthh:mm");
                    break;
                case 3:
                    infoText = infoText + "[기한]" + m_Data[pos].DC_deadlineTime.ToString("MM/dd(ddd)tthh:mm");
                    break;
                default:
                    break;
            }

            switch (m_Data[pos].DC_repeatType)
            {
                case 1:
                    infoText = infoText + "[매일]";
                    break;
                case 2:
                    infoText = infoText + "[평일]";
                    break;
                case 3:
                    infoText = infoText + "[매주]";
                    break;
                case 4:
                    infoText = infoText + "[매월]";
                    break;
                case 5:
                    infoText = infoText + "[매년]";
                    break;
                case 6:
                    infoText = infoText + "[반복]";
                    break;
                default:
                    break;
            }
            return infoText;
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
*/

/*
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