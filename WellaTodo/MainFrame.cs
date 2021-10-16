// copyright honeysoft v0.14 -> v0.7

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
        static readonly Color PSEUDO_DETAIL_WINDOW_BACK_COLOR = Color.PapayaWhip;

        static readonly int DETAIL_WINDOW_WIDTH = 260;
        static readonly int DETAIL_WINDOW_X1 = 5;
        static readonly int MENU_WINDOW_WIDTH = 150;

        MainController m_Controller;
        List<CDataCell> m_Data = new List<CDataCell>();
        LoginSettingForm loginSettingForm = new LoginSettingForm();
        OutputForm outputForm = new OutputForm();

        RoundCheckbox roundCheckbox1 = new RoundCheckbox();
        StarCheckbox starCheckbox1 = new StarCheckbox();

        RoundLabel roundLabel1 = new RoundLabel();
        RoundLabel roundLabel2 = new RoundLabel();
        RoundLabel roundLabel3 = new RoundLabel();
        RoundLabel roundLabel4 = new RoundLabel();

        Label createDate = new Label();
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

            Initiate_View();
            Load_Item();

            m_selectedMainMenu = 5; // 완료됨
            Changed_MainMenu();

            timer1.Interval = 60000;
            timer1.Enabled = true;
        }

        private void MainFrame_Resize(object sender, EventArgs e)
        {
            Repaint();
        }

        public void Initiate_View()
        {
            splitContainer1.SplitterDistance = MENU_WINDOW_WIDTH;
            splitContainer1.Panel1MinSize = 100;
            splitContainer1.Panel2MinSize = 200;

            flowLayoutPanel1.BackColor = PSEUDO_BACK_COLOR;
            flowLayoutPanel1.Controls.Add(label1);
            flowLayoutPanel1.Controls.Add(label2);
            flowLayoutPanel1.Controls.Add(label3);
            flowLayoutPanel1.Controls.Add(label4);
            flowLayoutPanel1.Controls.Add(label5);
            flowLayoutPanel1.Controls.Add(label6);
            flowLayoutPanel1.Controls.Add(label7);

            foreach (Label ctr in flowLayoutPanel1.Controls)
            {
                ctr.Width = splitContainer1.SplitterDistance;
                ctr.BackColor = PSEUDO_BACK_COLOR;
            }

            splitContainer2.SplitterDistance = splitContainer2.Width;
            splitContainer2.IsSplitterFixed = true;
            splitContainer2.Location = new Point(0, 0);
            splitContainer2.Size = new Size(splitContainer1.Panel2.Width - 5, splitContainer1.Panel2.Height - 50);
            splitContainer2.Panel2.BackColor = PSEUDO_DETAIL_WINDOW_BACK_COLOR;

            flowLayoutPanel2.AutoScroll = false;
            flowLayoutPanel2.HorizontalScroll.Maximum = 0;
            flowLayoutPanel2.HorizontalScroll.Enabled = false;
            flowLayoutPanel2.HorizontalScroll.Visible = false;
            flowLayoutPanel2.AutoScroll = true;

            textBox2.Location = new Point(10, splitContainer1.Panel2.Height - 35);
            textBox2.Size = new Size(splitContainer1.Panel2.Width - 25, 25);
            textBox2.BackColor = PSEUDO_TEXTBOX_BACK_COLOR;
            textBox2.Text = "+ 작업 추가";

            roundCheckbox1.CheckedChanged += new EventHandler(roundCheckbox1_CheckedChanged);
            roundCheckbox1.MouseEnter += new EventHandler(roundCheckbox1_MouseEnter);
            roundCheckbox1.MouseLeave += new EventHandler(roundCheckbox1_MouseLeave);
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
            starCheckbox1.Location = new Point(DETAIL_WINDOW_X1 + 215, 5);
            starCheckbox1.Size = new Size(25, 25);
            starCheckbox1.BackColor = PSEUDO_DETAIL_WINDOW_BACK_COLOR;
            splitContainer2.Panel2.Controls.Add(starCheckbox1);

            roundLabel1.Click += new EventHandler(roundLabel1_Click);
            roundLabel1.MouseEnter += new EventHandler(roundLabel1_MouseEnter);
            roundLabel1.MouseLeave += new EventHandler(roundLabel1_MouseLeave);
            roundLabel1.Text = "나의 하루에 추가";
            roundLabel1.Location = new Point(DETAIL_WINDOW_X1 + 15, 40);
            roundLabel1.Size = new Size(DETAIL_WINDOW_WIDTH - 45, 30);
            roundLabel1.BackColor = PSEUDO_DETAIL_WINDOW_BACK_COLOR;
            splitContainer2.Panel2.Controls.Add(roundLabel1);

            roundLabel2.Click += new EventHandler(roundLabel2_Click);
            roundLabel2.MouseEnter += new EventHandler(roundLabel2_MouseEnter);
            roundLabel2.MouseLeave += new EventHandler(roundLabel2_MouseLeave);
            roundLabel2.Text = "미리 알림";
            roundLabel2.Location = new Point(DETAIL_WINDOW_X1 + 15, 75);
            roundLabel2.Size = new Size(DETAIL_WINDOW_WIDTH - 45, 30);
            roundLabel2.BackColor = PSEUDO_DETAIL_WINDOW_BACK_COLOR;
            splitContainer2.Panel2.Controls.Add(roundLabel2);

            roundLabel3.Click += new EventHandler(roundLabel3_Click);
            roundLabel3.MouseEnter += new EventHandler(roundLabel3_MouseEnter);
            roundLabel3.MouseLeave += new EventHandler(roundLabel3_MouseLeave);
            roundLabel3.Text = "기한 설정";
            roundLabel3.Location = new Point(DETAIL_WINDOW_X1 + 15, 110);
            roundLabel3.Size = new Size(DETAIL_WINDOW_WIDTH - 45, 30);
            roundLabel3.BackColor = PSEUDO_DETAIL_WINDOW_BACK_COLOR;
            splitContainer2.Panel2.Controls.Add(roundLabel3);

            roundLabel4.Click += new EventHandler(roundLabel4_Click);
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

            createDate.Text = " 생성됨";
            createDate.Location = new Point(DETAIL_WINDOW_X1 + 10, 325);
            createDate.Size = new Size(100, 50);
            createDate.BackColor = PSEUDO_DETAIL_WINDOW_BACK_COLOR;
            splitContainer2.Panel2.Controls.Add(createDate);

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

            button1.Location = new Point(DETAIL_WINDOW_X1 + 15, 360);
            button1.Size = new Size(75, 25);

            button2.Location = new Point(DETAIL_WINDOW_X1 + 160, 360);
            button2.Size = new Size(75, 25);
        }

        //--------------------------------------------------------------
        //Repaint
        //--------------------------------------------------------------
        private void Repaint()
        {
            foreach (Label ctr in flowLayoutPanel1.Controls)
            {
                ctr.Width = splitContainer1.SplitterDistance;
            }

            splitContainer2.Location = new Point(0, 0);
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
                    ? flowLayoutPanel2.Width - SystemInformation.VerticalScrollBarWidth - 5
                    : flowLayoutPanel2.Width - 5;
                item.IsItemSelected = m_present_data_position == pos;
                pos++;
            }
            Display_Data();
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
        private void Load_Item()
        {
            string text;
            bool chk_complete;
            bool chk_important;

            LoadFile();

            foreach (CDataCell data in m_Data)
            {
                text = data.DC_title;
                chk_complete = data.DC_complete;
                chk_important = data.DC_important;
                Todo_Item item = new Todo_Item(text, chk_complete, chk_important);
                flowLayoutPanel2.Controls.Add(item);
                item.UserControl_Event_method += new UserControl_Event(Click_Todo_Item);
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

            item.UserControl_Event_method += new UserControl_Event(Click_Todo_Item);

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
            DataCellToControl();
            Set_TodoItem_Width();
        }

        //--------------------------------------------------------------
        // 할일 파일 로딩
        //--------------------------------------------------------------
        private void LoadFile()
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
        private void SaveFile()
        {
            Stream ws = new FileStream("a.dat", FileMode.Create);
            BinaryFormatter serializer = new BinaryFormatter();

            serializer.Serialize(ws, m_Data);
            ws.Close();
        }

        //--------------------------------------------------------------
        // 할일 항목을 클릭했을때 처리
        //--------------------------------------------------------------
        private void Click_Todo_Item(object sender, EventArgs e)
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

                            DataCellToControl();
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

                            DataCellToControl();
                        }
                        if (m_selectedMainMenu == 2) label2_Click(sender, e); // 오늘할일 메뉴에서 실행
                        if (m_selectedMainMenu == 3) label3_Click(sender, e); // 중요 메뉴에서 실행
                        if (m_selectedMainMenu == 4) label4_Click(sender, e); // 계획된 일정에서 실행
                        if (m_selectedMainMenu == 5) label5_Click(sender, e); // 완료됨에서 실행
                        break;
                    }

                    // 중요항목 클릭시
                    if (item.IsImportantClicked)
                    {
                        if (item.TD_important && !item.TD_complete)
                        {
                            flowLayoutPanel2.Controls.SetChildIndex(item, 0);

                            m_Data[pos].DC_important = true;
                            CDataCell dc = m_Data[pos]; //추출
                            m_Data.RemoveAt(pos); //삭제
                            m_Data.Insert(0, dc); //삽입

                            flowLayoutPanel2.VerticalScroll.Value = 0;
                            m_present_data_position = 0;
                            starCheckbox1.Checked = true;

                            DataCellToControl();
                        }
                        else if (item.TD_important && item.TD_complete)
                        {
                            m_Data[pos].DC_important = true;
                            m_present_data_position = pos;
                            starCheckbox1.Checked = true;

                            DataCellToControl();
                        }
                        else if (!item.TD_important)
                        {
                            m_Data[pos].DC_important = false;
                            m_present_data_position = pos;
                            starCheckbox1.Checked = false;

                            DataCellToControl();
                        }
                        if (m_selectedMainMenu == 3) label3_Click(sender, e); // 중요 메뉴에서 실행
                        break;
                    }

                    // 컨텍스트 메뉴 삭제 클릭시
                    if (item.IsDeleteClicked)
                    {
                        if (MessageBox.Show("항목 삭제?", WINDOW_CAPTION, MessageBoxButtons.YesNo) == DialogResult.No)
                        {
                            break;
                        }
                        splitContainer2.SplitterDistance = splitContainer2.Width;
                        isDetailWindowOpen = false;

                        item.UserControl_Event_method -= new UserControl_Event(Click_Todo_Item);
                        splitContainer2.Panel1.Controls.Remove(item);
                        item.Dispose();

                        m_Data.Remove(m_Data[pos]);
                        break;
                    }

                    //Todo 아이템 클릭시
                    m_present_data_position = pos;
                    if (isDetailWindowOpen && (m_before_data_position == pos))
                    {
                        splitContainer2.SplitterDistance = splitContainer2.Width;
                        isDetailWindowOpen = false;
                        break;
                    }
                    else
                    {
                        DataCellToControl();

                        splitContainer2.SplitterDistance = splitContainer2.Width - DETAIL_WINDOW_WIDTH;
                        isDetailWindowOpen = true;
                        break;
                    }
                }
                pos++;
            }
            Set_TodoItem_Width();
        }

        private void DataCellToControl()
        {
            textBox3.Text = m_Data[m_present_data_position].DC_title;
            roundCheckbox1.Checked = m_Data[m_present_data_position].DC_complete;
            starCheckbox1.Checked = m_Data[m_present_data_position].DC_important;
            textBox1.Text = m_Data[m_present_data_position].DC_memo;

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

            createDate.Text = m_Data[m_present_data_position].DC_dateCreated.ToString("yyyy-MM-dd(ddd)\r\n") 
                + "생성됨["+ m_present_data_position.ToString() + "]";

            m_before_data_position = m_present_data_position;
        }

        //--------------------------------------------------------------
        // Control Event 
        //--------------------------------------------------------------

        //스프릿컨테이너-1 이벤트
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
            label1.Font = new Font(label1.Font, FontStyle.Underline);
            label1.BackColor = PSEUDO_HIGHLIGHT_COLOR;
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            label1.Font = new Font(label1.Font, FontStyle.Regular);

            if (m_selectedMainMenu == 1)
                label1.BackColor = PSEUDO_SELECTED_COLOR;
            else
                label1.BackColor = PSEUDO_BACK_COLOR;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            m_selectedMainMenu = 1;
            Changed_MainMenu();

            loginSettingForm.StartPosition = FormStartPosition.CenterParent;
            loginSettingForm.ShowDialog();

            //Invoke_View_Event();
        }

        private void label2_MouseEnter(object sender, EventArgs e)
        {
            label2.Font = new Font(label2.Font, FontStyle.Underline);
            label2.BackColor = PSEUDO_HIGHLIGHT_COLOR;
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            label2.Font = new Font(label2.Font, FontStyle.Regular);

            if (m_selectedMainMenu == 2)
                label2.BackColor = PSEUDO_SELECTED_COLOR;
            else
                label2.BackColor = PSEUDO_BACK_COLOR;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            int cnt = 0;
            int pos = 0;
            foreach (Todo_Item item in flowLayoutPanel2.Controls)
            {
                if (!m_Data[pos].DC_myToday || item.TD_complete)
                {
                    item.Visible = false;
                }
                else
                {
                    item.Visible = true;
                    cnt++;
                }
                pos++;
            }

            if (cnt == 0)
            {
                splitContainer2.SplitterDistance = splitContainer2.Width;
                isDetailWindowOpen = false;
            }
            Set_TodoItem_Width();

            m_selectedMainMenu = 2; // 오늘 할 일
            Changed_MainMenu();
        }

        private void label3_MouseEnter(object sender, EventArgs e)
        {
            label3.Font = new Font(label3.Font, FontStyle.Underline);
            label3.BackColor = PSEUDO_HIGHLIGHT_COLOR;
        }

        private void label3_MouseLeave(object sender, EventArgs e)
        {
            label3.Font = new Font(label3.Font, FontStyle.Regular);

            if (m_selectedMainMenu == 3)
                label3.BackColor = PSEUDO_SELECTED_COLOR;
            else
                label3.BackColor = PSEUDO_BACK_COLOR;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            int cnt = 0;
            int pos = 0;
            foreach (Todo_Item item in flowLayoutPanel2.Controls)
            {
                if (!item.TD_important || item.TD_complete)
                {
                    item.Visible = false;
                }
                else
                {
                    item.Visible = true;
                    cnt++;
                }
                pos++;
            }

            if (cnt == 0)
            {
                splitContainer2.SplitterDistance = splitContainer2.Width;
                isDetailWindowOpen = false;
            }
            Set_TodoItem_Width();

            m_selectedMainMenu = 3; // 중요
            Changed_MainMenu();
        }

        private void label4_MouseEnter(object sender, EventArgs e)
        {
            label4.Font = new Font(label4.Font, FontStyle.Underline);
            label4.BackColor = PSEUDO_HIGHLIGHT_COLOR;
        }

        private void label4_MouseLeave(object sender, EventArgs e)
        {
            label4.Font = new Font(label4.Font, FontStyle.Regular);

            if (m_selectedMainMenu == 4)
                label4.BackColor = PSEUDO_SELECTED_COLOR;
            else
                label4.BackColor = PSEUDO_BACK_COLOR;
        }

        private void label4_Click(object sender, EventArgs e)
        {
            int cnt = 0;
            int pos = 0;
            int sum;
            foreach (Todo_Item item in flowLayoutPanel2.Controls)
            {
                sum = m_Data[pos].DC_remindType + m_Data[pos].DC_deadlineType + m_Data[pos].DC_repeatType;
                if ((!m_Data[pos].DC_myToday && sum == 0) || item.TD_complete)
                {
                    item.Visible = false;
                }
                else
                {
                    item.Visible = true;
                    cnt++;
                }
                pos++;
            }

            if (cnt == 0)
            {
                splitContainer2.SplitterDistance = splitContainer2.Width;
                isDetailWindowOpen = false;
            }
            Set_TodoItem_Width();

            m_selectedMainMenu = 4; //계획된 일정
            Changed_MainMenu();
        }

        private void label5_MouseEnter(object sender, EventArgs e)
        {
            label5.Font = new Font(label5.Font, FontStyle.Underline);
            label5.BackColor = PSEUDO_HIGHLIGHT_COLOR;
        }

        private void label5_MouseLeave(object sender, EventArgs e)
        {
            label5.Font = new Font(label5.Font, FontStyle.Regular);

            if (m_selectedMainMenu == 5)
                label5.BackColor = PSEUDO_SELECTED_COLOR;
            else
                label5.BackColor = PSEUDO_BACK_COLOR;
        }

        private void label5_Click(object sender, EventArgs e)
        {
            int cnt = 0;
            int pos = 0;
            foreach (Todo_Item item in flowLayoutPanel2.Controls)
            {
                if (!item.TD_complete)
                {
                    item.Visible = false;
                }
                else
                {
                    item.Visible = true;
                    cnt++;
                }
                pos++;
            }

            if (cnt == 0)
            {
                splitContainer2.SplitterDistance = splitContainer2.Width;
                isDetailWindowOpen = false;
            }
            Set_TodoItem_Width();

            m_selectedMainMenu = 5; // 완료됨
            Changed_MainMenu();
        }

        private void label6_MouseEnter(object sender, EventArgs e)
        {
            label6.Font = new Font(label6.Font, FontStyle.Underline);
            label6.BackColor = PSEUDO_HIGHLIGHT_COLOR;
        }

        private void label6_MouseLeave(object sender, EventArgs e)
        {
            label6.Font = new Font(label6.Font, FontStyle.Regular);

            if (m_selectedMainMenu == 6)
                label6.BackColor = PSEUDO_SELECTED_COLOR;
            else
                label6.BackColor = PSEUDO_BACK_COLOR;
        }

        private void label6_Click(object sender, EventArgs e)
        {
            int cnt = 0;
            int pos = 0;
            foreach (Todo_Item item in flowLayoutPanel2.Controls)
            {
                item.Visible = true;
                cnt++;
                pos++;
            }

            if (cnt == 0)
            {
                splitContainer2.SplitterDistance = splitContainer2.Width;
                isDetailWindowOpen = false;
            }
            Set_TodoItem_Width();

            m_selectedMainMenu = 6; // 모든 작업
            Changed_MainMenu();
        }

        private void label7_MouseEnter(object sender, EventArgs e)
        {
            label7.Font = new Font(label7.Font, FontStyle.Underline);
            label7.BackColor = PSEUDO_HIGHLIGHT_COLOR;
        }

        private void label7_MouseLeave(object sender, EventArgs e)
        {
            label7.Font = new Font(label7.Font, FontStyle.Regular);

            if (m_selectedMainMenu == 7)
                label7.BackColor = PSEUDO_SELECTED_COLOR;
            else
                label7.BackColor = PSEUDO_BACK_COLOR;
        }

        private void label7_Click(object sender, EventArgs e)
        {
            outputForm.StartPosition = FormStartPosition.CenterParent;

            if (outputForm.Visible)
            {
                outputForm.Hide();
            }
            else
            {
                outputForm.Show();
            }

            m_selectedMainMenu = 7; // 새목록 만들기
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
                ctr.Width = splitContainer1.SplitterDistance;
                if (pos == m_selectedMainMenu)
                    ctr.BackColor = PSEUDO_SELECTED_COLOR;
                else
                    ctr.BackColor = PSEUDO_BACK_COLOR;
                pos++;
            }

            if (m_selectedMainMenu == 6)
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

        //상세창 제목 키 입력
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

        //상세창 제목 커서 벗어남
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

        // 상세창 닫기 버튼
        private void button1_Click(object sender, EventArgs e)
        {
            if (isDetailWindowOpen)
            {
                splitContainer2.SplitterDistance = splitContainer2.Width;
                isDetailWindowOpen = false;
            }
            Set_TodoItem_Width();
        }

        //상세창 삭제 버튼
        private void button2_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("항목 삭제?", WINDOW_CAPTION, MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }

            int pos = 0;
            if (isDetailWindowOpen)
            {
                foreach (Todo_Item item in flowLayoutPanel2.Controls)
                {
                    if (pos == m_present_data_position)
                    {
                        item.UserControl_Event_method -= new UserControl_Event(Click_Todo_Item);
                        splitContainer2.Panel1.Controls.Remove(item);
                        item.Dispose();

                        m_Data.Remove(m_Data[pos]);
                        break;
                    }
                    pos++;
                }
                splitContainer2.SplitterDistance = splitContainer2.Width;
                isDetailWindowOpen = false;
            }
            Set_TodoItem_Width();
        }

        //상세창 완료 체크시
        private void roundCheckbox1_CheckedChanged(object sender, EventArgs e)
        {
            if (isDetailWindowOpen)
            {
                m_Data[m_present_data_position].DC_complete = roundCheckbox1.Checked;

                int pos = 0;
                foreach (Todo_Item item in flowLayoutPanel2.Controls)
                {
                    if (pos == m_present_data_position)
                    {
                        item.TD_complete = roundCheckbox1.Checked;
                        break;
                    }
                    pos++;
                }
            }
        }

        private void roundCheckbox1_MouseEnter(object sender, EventArgs e)
        {
            
        }

        private void roundCheckbox1_MouseLeave(object sender, EventArgs e)
        {
            roundCheckbox1.BackColor = PSEUDO_DETAIL_WINDOW_BACK_COLOR;
        }

        //상세창 중요 체크시
        private void starCheckbox1_CheckedChanged(object sender, EventArgs e)
        {
            if (isDetailWindowOpen)
            {
                m_Data[m_present_data_position].DC_important = starCheckbox1.Checked;

                int pos = 0;
                foreach (Todo_Item item in flowLayoutPanel2.Controls)
                {
                    if (pos == m_present_data_position)
                    {
                        item.TD_important = starCheckbox1.Checked;
                        break;
                    }
                    pos++;
                }
            }
        }

        private void starCheckbox1_MouseEnter(object sender, EventArgs e)
        {
            
        }

        private void starCheckbox1_MouseLeave(object sender, EventArgs e)
        {
            starCheckbox1.BackColor = PSEUDO_DETAIL_WINDOW_BACK_COLOR;
        }

        // 끝낼때 저장 여부 묻기
        private void MainFrame_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("저장할까요?", WINDOW_CAPTION, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                SaveFile();
            }
        }

        // 상세창 - 나의 하루에 추가 메뉴
        private void roundLabel1_Click(object sender, EventArgs e)
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
            if (m_Data[m_present_data_position].DC_myToday)
                roundLabel1.BackColor = PSEUDO_SELECTED_COLOR;
            else
                roundLabel1.BackColor = PSEUDO_DETAIL_WINDOW_BACK_COLOR;
        }

        // 상세창 - 미리 알림 메뉴
        private void roundLabel2_MouseEnter(object sender, EventArgs e)
        {
            roundLabel2.BackColor = PSEUDO_HIGHLIGHT_COLOR;
        }

        private void roundLabel2_MouseLeave(object sender, EventArgs e)
        {
            if (m_Data[m_present_data_position].DC_remindType > 0)
                roundLabel2.BackColor = PSEUDO_SELECTED_COLOR;
            else
                roundLabel2.BackColor = PSEUDO_DETAIL_WINDOW_BACK_COLOR;
        }

        private void roundLabel2_Click(object sender, EventArgs e)
        {
            ContextMenu remindMenu = new ContextMenu();
            MenuItem todayRemind = new MenuItem("오늘 나중에", new EventHandler(this.OnTodayRemind_Click));
            MenuItem tomorrowRemind = new MenuItem("내일", new EventHandler(this.OnTomorrowRemind_Click));
            MenuItem nextWeekRemind = new MenuItem("다음 주", new EventHandler(this.OnNextWeekRemind_Click));
            MenuItem selectRemind = new MenuItem("날짜 및 시간 선택", new EventHandler(this.OnSelectRemind_Click));
            MenuItem deleteRemind = new MenuItem("미리 알림 제거", new EventHandler(this.OnDeleteRemind_Click));
            remindMenu.MenuItems.Add(todayRemind);
            remindMenu.MenuItems.Add(tomorrowRemind);
            remindMenu.MenuItems.Add(nextWeekRemind);
            remindMenu.MenuItems.Add(selectRemind);
            remindMenu.MenuItems.Add(deleteRemind);

            var mouseEventArgs = e as MouseEventArgs;
            if (mouseEventArgs != null)
            {
                int px = splitContainer1.SplitterDistance + splitContainer2.SplitterDistance + 60;
                int py = 107;
                remindMenu.Show(this, new Point(px, py));
            }
        }

        private void OnTodayRemind_Click(object sender, EventArgs e)
        {
            m_Data[m_present_data_position].DC_remindType = 1;

            DateTime dt = DateTime.Now;
            if (dt.Minute < 30)
                dt = dt.AddHours(3);
            else
                dt = dt.AddHours(4);
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

        // 상세창 - 기한 설정 메뉴
        private void roundLabel3_MouseEnter(object sender, EventArgs e)
        {
            roundLabel3.BackColor = PSEUDO_HIGHLIGHT_COLOR;
        }

        private void roundLabel3_MouseLeave(object sender, EventArgs e)
        {
            if (m_Data[m_present_data_position].DC_deadlineType > 0)
                roundLabel3.BackColor = PSEUDO_SELECTED_COLOR;
            else
                roundLabel3.BackColor = PSEUDO_DETAIL_WINDOW_BACK_COLOR;
        }

        private void roundLabel3_Click(object sender, EventArgs e)
        {
            ContextMenu deadlineMenu = new ContextMenu();
            MenuItem todayDeadline = new MenuItem("오늘", new EventHandler(this.OnTodayDeadline_Click));
            MenuItem tomorrowDeadline = new MenuItem("내일", new EventHandler(this.OnTomorrowDeadline_Click));
            MenuItem selectDeadline = new MenuItem("날짜 선택", new EventHandler(this.OnSelectDeadline_Click));
            MenuItem deleteDeadline = new MenuItem("기한 설정 제거", new EventHandler(this.OnDeleteDeadline_Click));
            deadlineMenu.MenuItems.Add(todayDeadline);
            deadlineMenu.MenuItems.Add(tomorrowDeadline);
            deadlineMenu.MenuItems.Add(selectDeadline);
            deadlineMenu.MenuItems.Add(deleteDeadline);

            var mouseEventArgs = e as MouseEventArgs;
            if (mouseEventArgs != null)
            {
                int px = splitContainer1.SplitterDistance + splitContainer2.SplitterDistance + 60;
                int py = 142;
                deadlineMenu.Show(this, new Point(px, py));
            }
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

        // 상세창 - 반복 메뉴
        private void roundLabel4_MouseEnter(object sender, EventArgs e)
        {
            roundLabel4.BackColor = PSEUDO_HIGHLIGHT_COLOR;
        }

        private void roundLabel4_MouseLeave(object sender, EventArgs e)
        {
            if (m_Data[m_present_data_position].DC_repeatType > 0)
                roundLabel4.BackColor = PSEUDO_SELECTED_COLOR;
            else
                roundLabel4.BackColor = PSEUDO_DETAIL_WINDOW_BACK_COLOR;
        }

        private void roundLabel4_Click(object sender, EventArgs e)
        {
            ContextMenu repeatMenu = new ContextMenu();
            MenuItem everyDayRepeat = new MenuItem("매일", new EventHandler(this.OnEveryDayRepeat_Click));
            MenuItem workingDayRepeat = new MenuItem("평일", new EventHandler(this.OnWorkingDayRepeat_Click));
            MenuItem everyWeekRepeat = new MenuItem("매주", new EventHandler(this.OnEveryWeekRepeat_Click));
            MenuItem everyMonthRepeat = new MenuItem("매월", new EventHandler(this.OnEveryMonthRepeat_Click));
            MenuItem everyYearRepeat = new MenuItem("매년", new EventHandler(this.OnEveryYearRepeat_Click));
            //MenuItem userDefineRepeat = new MenuItem("사용자 정의", new EventHandler(this.OnUserDefineRepeat_Click));
            MenuItem deleteRepeat = new MenuItem("반복 제거", new EventHandler(this.OnDeleteRepeat_Click));
            repeatMenu.MenuItems.Add(everyDayRepeat);
            repeatMenu.MenuItems.Add(workingDayRepeat);
            repeatMenu.MenuItems.Add(everyWeekRepeat);
            repeatMenu.MenuItems.Add(everyMonthRepeat);
            repeatMenu.MenuItems.Add(everyYearRepeat);
            //repeatMenu.MenuItems.Add(userDefineRepeat);
            repeatMenu.MenuItems.Add(deleteRepeat);

            var mouseEventArgs = e as MouseEventArgs;
            if (mouseEventArgs != null)
            {
                int px = splitContainer1.SplitterDistance + splitContainer2.SplitterDistance + 60;
                int py = 177;
                repeatMenu.Show(this, new Point(px, py));
            }
                
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

                DataCellToControl();
            }
        }

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

                DataCellToControl();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;

            Text = WINDOW_CAPTION + " [" + dt.ToString("yyyy-MM-dd(ddd) tt h:mm") + "]";
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

            if (m_Data[pos].DC_myToday)
            {
                infoText = infoText + "[오늘 할일]";
            }

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