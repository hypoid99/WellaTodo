// copyright honeysoft v0.14

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

        static readonly string WINDOW_CAPTION = "Wella Todo v0.7";
        static readonly int WINDOW_WIDTH = 900;
        static readonly int WINDOW_HEIGHT = 500;

        static readonly Color PSEUDO_BACK_COLOR = Color.White;
        static readonly Color PSEUDO_HIGHLIGHT_COLOR = Color.LightCyan;
        static readonly Color PSEUDO_TEXTBOX_BACK_COLOR = Color.LightCyan;
        static readonly Color PSEUDO_DETAIL_WINDOW_BACK_COLOR = Color.PapayaWhip;

        static readonly int DETAIL_WINDOW_WIDTH = 260;
        static readonly int MENU_WINDOW_WIDTH = 150;

        MainController m_Controller;
        List<CDataCell> m_Data = new List<CDataCell>();

        bool isDetailWindowOpen = false;
        bool isTextboxClicked = false;

        int m_present_data_position = -1;
        int m_before_data_position;

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
            this.Size = new Size(WINDOW_WIDTH, WINDOW_HEIGHT);
            this.Text = WINDOW_CAPTION;

            Initiate_View();
            Load_Item();
        }

        private void MainFrame_Resize(object sender, EventArgs e)
        {
            Repaint();
        }

        public void Initiate_View()
        {
            m_Data = m_Controller.Get_Model().GetDataCollection();

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

            foreach (Label ctr in flowLayoutPanel1.Controls)
            {
                ctr.Width = splitContainer1.SplitterDistance;
                ctr.BackColor = PSEUDO_BACK_COLOR;
            }

            splitContainer2.SplitterDistance = splitContainer2.Width;
            splitContainer2.IsSplitterFixed = true;
            splitContainer2.Location = new Point(0, 0);
            splitContainer2.Size = new Size(splitContainer1.Panel2.Width - 20, splitContainer1.Panel2.Height - 50);

            splitContainer2.Panel2.BackColor = PSEUDO_DETAIL_WINDOW_BACK_COLOR;

            flowLayoutPanel2.AutoScroll = false;
            flowLayoutPanel2.HorizontalScroll.Maximum = 0;
            flowLayoutPanel2.HorizontalScroll.Enabled = false;
            flowLayoutPanel2.HorizontalScroll.Visible = false;
            flowLayoutPanel2.AutoScroll = true;

            textBox2.Location = new Point(10, splitContainer1.Panel2.Height - 35);
            textBox2.Size = new Size(splitContainer1.Panel2.Width - 20, 25);
            textBox2.BackColor = PSEUDO_TEXTBOX_BACK_COLOR;
            textBox2.Text = "+ 작업 추가";

            splitContainer1.Refresh();
            splitContainer2.Refresh();
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
            splitContainer2.Size = new Size(splitContainer1.Panel2.Width - 20, splitContainer1.Panel2.Height - 50);

            textBox2.Location = new Point(10, splitContainer1.Panel2.Height - 35);
            textBox2.Size = new Size(splitContainer1.Panel2.Width - 20, 25);

            if (isDetailWindowOpen)
            {
                int width = splitContainer2.Width - DETAIL_WINDOW_WIDTH;
                splitContainer2.SplitterDistance = width < 0 ? 1 : width;
            }

            Display_Todo_Item();
        }

        //--------------------------------------------------------------
        //화면에 할일 출력
        //--------------------------------------------------------------
        private void Display_Todo_Item()
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
        }

        private void Display_Data()
        {
            m_Data = m_Controller.Get_Model().GetDataCollection();
            int pos = 0;
            foreach (Todo_Item item in flowLayoutPanel2.Controls)
            {
                Console.WriteLine(">Data TD:[{0}]-[{1}]", pos, item.TD_title);
                pos++;
            }

            pos = 0;
            foreach (CDataCell data in m_Data)
            {
                Console.WriteLine(">Data DC:[{0}]-[{1}]", pos, data.DC_title);
                pos++;
            }
            Console.WriteLine(">Data Present : [{0}]", m_present_data_position);
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
            //int idx;
            string text;
            bool chk_complete;
            bool chk_important;

            LoadFile();

            m_Data = m_Controller.Get_Model().GetDataCollection();
            foreach (CDataCell data in m_Data)
            {
                text = data.DC_title;
                chk_complete = data.DC_complete;
                chk_important = data.DC_important;
                Todo_Item item = new Todo_Item(text, chk_complete, chk_important);
                flowLayoutPanel2.Controls.Add(item);
                item.UserControl_Event_method += new UserControl_Event(Click_Todo_Item);
            }
            Display_Todo_Item();
        }

        //--------------------------------------------------------------
        // 할일 항목 추가
        //--------------------------------------------------------------
        private void Add_Item(string text)
        {
            m_Data = m_Controller.Get_Model().GetDataCollection();

            //m_Controller.performAddItem();
            m_Data.Insert(0, new CDataCell(text, false, false, "메모추가"));

            Todo_Item item = new Todo_Item(text, false, false);
            flowLayoutPanel2.Controls.Add(item);
            flowLayoutPanel2.Controls.SetChildIndex(item, 0);

            item.UserControl_Event_method += new UserControl_Event(Click_Todo_Item);

            Display_Todo_Item();
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
                Console.WriteLine(">Loading Data:[{0}]]", dt.DC_title);
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

            m_Data = m_Controller.Get_Model().GetDataCollection();
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
                        splitContainer2.SplitterDistance = splitContainer2.Width;
                        isDetailWindowOpen = false;

                        if (item.isCompleted())
                        {
                            int cnt = flowLayoutPanel2.Controls.Count;
                            flowLayoutPanel2.Controls.SetChildIndex(item, cnt);

                            m_Data = m_Controller.Get_Model().GetDataCollection();
                            m_Data[pos].DC_complete = true;
                            CDataCell dc = m_Data[pos]; //추출
                            m_Data.RemoveAt(pos); //삭제
                            m_Data.Insert(m_Data.Count, dc); //삽입         
                        }
                        else
                        {
                            flowLayoutPanel2.Controls.SetChildIndex(item, 0);

                            m_Data = m_Controller.Get_Model().GetDataCollection();
                            m_Data[pos].DC_complete = false;
                            CDataCell dc = m_Data[pos]; //추출
                            m_Data.RemoveAt(pos); //삭제
                            m_Data.Insert(0, dc); //삽입
                        }
                        break;
                    }

                    // 중요항목 클릭시
                    if (item.IsImportantClicked)
                    {
                        splitContainer2.SplitterDistance = splitContainer2.Width;
                        isDetailWindowOpen = false;

                        if (item.isImportant() && !item.isCompleted())
                        {
                            flowLayoutPanel2.Controls.SetChildIndex(item, 0);

                            m_Data = m_Controller.Get_Model().GetDataCollection();
                            m_Data[pos].DC_important = true;
                            CDataCell dc = m_Data[pos]; //추출
                            m_Data.RemoveAt(pos); //삭제
                            m_Data.Insert(0, dc); //삽입

                            flowLayoutPanel2.VerticalScroll.Value = 0;
                        }
                        else
                        {
                            m_Data = m_Controller.Get_Model().GetDataCollection();
                            m_Data[pos].DC_important = false;
                        }
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
                        m_Data = m_Controller.Get_Model().GetDataCollection();
                        textBox3.Text = m_Data[pos].DC_title;
                        checkBox1.Checked = m_Data[pos].DC_complete;
                        checkBox2.Checked = m_Data[pos].DC_important;
                        textBox1.Text = m_Data[pos].DC_memo;

                        m_before_data_position = m_present_data_position;

                        splitContainer2.SplitterDistance = splitContainer2.Width - DETAIL_WINDOW_WIDTH;
                        isDetailWindowOpen = true;
                        break;
                    }
                }
                pos++;
            }
            Display_Todo_Item();
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

        //메뉴 이벤트
        private void label1_MouseEnter(object sender, EventArgs e)
        {
            label1.Font = new Font(label1.Font, FontStyle.Underline);
            label1.BackColor = PSEUDO_HIGHLIGHT_COLOR;
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            label1.Font = new Font(label1.Font, FontStyle.Regular);
            label1.BackColor = PSEUDO_BACK_COLOR;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Console.WriteLine(">Label_1::clicked");
            Invoke_View_Event();
        }

        private void label2_MouseEnter(object sender, EventArgs e)
        {
            label2.Font = new Font(label2.Font, FontStyle.Underline);
            label2.BackColor = PSEUDO_HIGHLIGHT_COLOR;
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            label2.Font = new Font(label2.Font, FontStyle.Regular);
            label2.BackColor = PSEUDO_BACK_COLOR;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Console.WriteLine(">Label_2::clicked");
        }

        private void label3_MouseEnter(object sender, EventArgs e)
        {
            label3.Font = new Font(label3.Font, FontStyle.Underline);
            label3.BackColor = PSEUDO_HIGHLIGHT_COLOR;
        }

        private void label3_MouseLeave(object sender, EventArgs e)
        {
            label3.Font = new Font(label3.Font, FontStyle.Regular);
            label3.BackColor = PSEUDO_BACK_COLOR;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Console.WriteLine(">Label_3::clicked");
        }

        private void label4_MouseEnter(object sender, EventArgs e)
        {
            label4.Font = new Font(label4.Font, FontStyle.Underline);
            label4.BackColor = PSEUDO_HIGHLIGHT_COLOR;
        }

        private void label4_MouseLeave(object sender, EventArgs e)
        {
            label4.Font = new Font(label4.Font, FontStyle.Regular);
            label4.BackColor = PSEUDO_BACK_COLOR;
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Console.WriteLine(">Label_4::clicked");
        }

        private void label5_MouseEnter(object sender, EventArgs e)
        {
            label5.Font = new Font(label5.Font, FontStyle.Underline);
            label5.BackColor = PSEUDO_HIGHLIGHT_COLOR;
        }

        private void label5_MouseLeave(object sender, EventArgs e)
        {
            label5.Font = new Font(label5.Font, FontStyle.Regular);
            label5.BackColor = PSEUDO_BACK_COLOR;
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Console.WriteLine(">Label_5::clicked");
        }

        private void label6_MouseEnter(object sender, EventArgs e)
        {
            label6.Font = new Font(label6.Font, FontStyle.Underline);
            label6.BackColor = PSEUDO_HIGHLIGHT_COLOR;
        }

        private void label6_MouseLeave(object sender, EventArgs e)
        {
            label6.Font = new Font(label6.Font, FontStyle.Regular);
            label6.BackColor = PSEUDO_BACK_COLOR;
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Console.WriteLine(">Label_6::clicked");
        }

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

                m_Data = m_Controller.Get_Model().GetDataCollection();
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

            m_Data = m_Controller.Get_Model().GetDataCollection();
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
            m_Data = m_Controller.Get_Model().GetDataCollection();
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
            Display_Todo_Item();
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
                m_Data = m_Controller.Get_Model().GetDataCollection();
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
            Display_Todo_Item();
        }

        //상세창 완료 체크시
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (isDetailWindowOpen)
            {
                m_Data = m_Controller.Get_Model().GetDataCollection();
                m_Data[m_present_data_position].DC_complete = checkBox1.Checked;

                int pos = 0;
                foreach (Todo_Item item in flowLayoutPanel2.Controls)
                {
                    if (pos == m_present_data_position)
                    {
                        item.TD_complete = checkBox1.Checked;
                        break;
                    }
                    pos++;
                }
            }
        }

        //상세창 중요 체크시
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (isDetailWindowOpen)
            {
                m_Data = m_Controller.Get_Model().GetDataCollection();
                m_Data[m_present_data_position].DC_important = checkBox2.Checked;

                int pos = 0;
                foreach (Todo_Item item in flowLayoutPanel2.Controls)
                {
                    if (pos == m_present_data_position)
                    {
                        item.TD_important = checkBox2.Checked;
                        break;
                    }
                    pos++;
                }
            }
        }

        // 끝낼때 저장 여부 묻기
        private void MainFrame_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("저장할까요?", WINDOW_CAPTION, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                SaveFile();
            }
        }
    }
}
