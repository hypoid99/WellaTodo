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

        MainController m_Controller;
        List<CDataCell> m_Data = new List<CDataCell>();

        int m_Todo_Item_Counter = 1;
        bool isTodo_detail = false;
        int m_data_position;
        int m_before_data_position;

        public MainFrame()
        {
            InitializeComponent();
        }

        public void SetController(MainController controller)
        {
            m_Controller = controller;
        }

        public void Initiate_View()
        {
            m_Data = m_Controller.Get_Model().GetDataCollection();

            flowLayoutPanel1.Controls.Add(label1);
            flowLayoutPanel1.Controls.Add(label2);
            flowLayoutPanel1.Controls.Add(label3);
            flowLayoutPanel1.Controls.Add(label4);
            flowLayoutPanel1.Controls.Add(label5);
            flowLayoutPanel1.Controls.Add(label6);
            label1.Width = splitContainer1.SplitterDistance;
            label2.Width = splitContainer1.SplitterDistance;
            label3.Width = splitContainer1.SplitterDistance;
            label4.Width = splitContainer1.SplitterDistance;
            label5.Width = splitContainer1.SplitterDistance;
            label6.Width = splitContainer1.SplitterDistance;

            splitContainer2.SplitterDistance = splitContainer2.Width;
            splitContainer2.IsSplitterFixed = true;
            splitContainer2.Location = new Point(10, 10);
            splitContainer2.Size = new Size(splitContainer1.Panel2.Width - 20, splitContainer1.Panel2.Height - 50);

            flowLayoutPanel2.AutoScroll = false;
            flowLayoutPanel2.HorizontalScroll.Maximum = 0;
            flowLayoutPanel2.HorizontalScroll.Enabled = false;
            flowLayoutPanel2.HorizontalScroll.Visible = false;
            flowLayoutPanel2.AutoScroll = true;

            textBox2.Location = new Point(10, splitContainer1.Panel2.Height - 35);
            textBox2.Size = new Size(splitContainer1.Panel2.Width - 20, 25);

            splitContainer1.Refresh();
            splitContainer2.Refresh();
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
                MessageBox.Show("Please enter a valid number");
            }
        }

        //--------------------------------------------------------------
        // 할일 항목 초기 데이타 로딩
        //--------------------------------------------------------------
        private void Load_Item()
        {
            int idx;
            string text;
            bool chk_complete;
            bool chk_important;

            LoadFile();

            m_Data = m_Controller.Get_Model().GetDataCollection();
            foreach (CDataCell data in m_Data)
            {
                idx = data.DC_idNum;
                text = data.DC_title;
                chk_complete = data.DC_complete;
                chk_important = data.DC_important;
                Todo_Item item = new Todo_Item(idx, text, chk_complete, chk_important);
                flowLayoutPanel2.Controls.Add(item);
                m_Todo_Item_Counter++;
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
            Console.WriteLine(">Add Item Count : [{0}]", m_Todo_Item_Counter);
            m_Data.Insert(0, new CDataCell(m_Todo_Item_Counter, false, text, false, "메모추가"));

            Todo_Item item = new Todo_Item(m_Todo_Item_Counter, text, false, false);
            flowLayoutPanel2.Controls.Add(item);
            flowLayoutPanel2.Controls.SetChildIndex(item, 0);
            m_Todo_Item_Counter++;
            item.UserControl_Event_method += new UserControl_Event(Click_Todo_Item);
            item.Width = flowLayoutPanel2.Width;

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
                        isTodo_detail = false;

                        if (item.isCompleted())
                        {
                            int cnt = flowLayoutPanel2.Controls.Count;
                            flowLayoutPanel2.Controls.SetChildIndex(item, cnt);

                            m_Data = m_Controller.Get_Model().GetDataCollection();
                            m_Data[pos].DC_complete = true;
                            CDataCell dc = m_Data[pos]; //추출
                            m_Data.RemoveAt(pos); //삭제
                            m_Data.Insert(m_Data.Count, dc); //삽입         

                            Console.WriteLine(">complete : [{0}]", dc.DC_important);
                        }
                        else
                        {
                            flowLayoutPanel2.Controls.SetChildIndex(item, 0);

                            m_Data = m_Controller.Get_Model().GetDataCollection();
                            m_Data[pos].DC_complete = false;
                            CDataCell dc = m_Data[pos]; //추출
                            m_Data.RemoveAt(pos); //삭제
                            m_Data.Insert(0, dc); //삽입

                            Console.WriteLine(">complete : [{0}]", dc.DC_important);
                        }
                        break;
                    }

                    // 중요항목 클릭시
                    if (item.IsImportantClicked)
                    {
                        splitContainer2.SplitterDistance = splitContainer2.Width;
                        isTodo_detail = false;

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
                        splitContainer2.SplitterDistance = splitContainer2.Width;
                        isTodo_detail = false;

                        item.UserControl_Event_method -= new UserControl_Event(Click_Todo_Item);
                        splitContainer2.Panel1.Controls.Remove(item);
                        item.Dispose();

                        m_Data.Remove(m_Data[pos]);
                        break;
                    }

                    //Todo 아이템 클릭시
                    m_data_position = pos;
                    if (isTodo_detail && (m_before_data_position == pos))
                    {
                        splitContainer2.SplitterDistance = splitContainer2.Width;
                        isTodo_detail = false;
                        break;
                    }
                    else
                    {
                        splitContainer2.SplitterDistance = splitContainer2.Width / 2;
                        isTodo_detail = true;

                        m_Data = m_Controller.Get_Model().GetDataCollection();
                        textBox3.Text = item.TD_title;
                        checkBox1.Checked = item.TD_complete;
                        checkBox2.Checked = item.TD_important;
                        textBox1.Text = "Data Position : " + m_data_position.ToString();
                        m_before_data_position = m_data_position;
                        break;
                    }
                }
                pos++;
            }
            Display_Todo_Item();
        }

        //--------------------------------------------------------------
        //화면에 할일 출력
        //--------------------------------------------------------------
        private void Display_Todo_Item()
        {
            foreach (Todo_Item item in flowLayoutPanel2.Controls)
            {
                item.Width = flowLayoutPanel2.Width;
            }
            Display_Data();
        }

        private void Display_Data()
        {
            m_Data = m_Controller.Get_Model().GetDataCollection();
            Console.WriteLine(">Todo Item Count : [{0}]", m_Todo_Item_Counter);
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
            Console.WriteLine(">------------------");
        }

        //--------------------------------------------------------------
        //Repaint
        //--------------------------------------------------------------
        private void Repaint()
        {
            label1.Width = splitContainer1.SplitterDistance;
            label2.Width = splitContainer1.SplitterDistance;
            label3.Width = splitContainer1.SplitterDistance;
            label4.Width = splitContainer1.SplitterDistance;
            label5.Width = splitContainer1.SplitterDistance;
            label6.Width = splitContainer1.SplitterDistance;

            splitContainer2.Location = new Point(10, 10);
            splitContainer2.Size = new Size(splitContainer1.Panel2.Width - 20, splitContainer1.Panel2.Height - 50);

            textBox2.Location = new Point(10, splitContainer1.Panel2.Height - 35);
            textBox2.Size = new Size(splitContainer1.Panel2.Width - 20, 25);

            foreach (Todo_Item item in flowLayoutPanel2.Controls)
            {
                item.Width = flowLayoutPanel2.Width;
            }

            this.Refresh();
        }

        //--------------------------------------------------------------
        // Control Event 
        //--------------------------------------------------------------

        //메인프레임 이벤트
        private void MainFrame_Load(object sender, EventArgs e)
        {
            Initiate_View();
            Load_Item();
        }

        private void MainFrame_Resize(object sender, EventArgs e)
        {
            Repaint();
        }

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
            label1.BackColor = System.Drawing.SystemColors.ControlDark;
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            label1.Font = new Font(label1.Font, FontStyle.Regular);
            label1.BackColor = System.Drawing.SystemColors.Control;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Console.WriteLine(">Label_1::clicked");

            Invoke_View_Event();
        }

        private void label2_MouseEnter(object sender, EventArgs e)
        {
            label2.Font = new Font(label2.Font, FontStyle.Underline);
            label2.BackColor = System.Drawing.SystemColors.ControlDark;
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            label2.Font = new Font(label2.Font, FontStyle.Regular);
            label2.BackColor = System.Drawing.SystemColors.Control;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            SaveFile();
            Console.WriteLine(">Label_2::clicked");
        }

        private void label3_MouseEnter(object sender, EventArgs e)
        {
            label3.Font = new Font(label3.Font, FontStyle.Underline);
            label3.BackColor = System.Drawing.SystemColors.ControlDark;
        }

        private void label3_MouseLeave(object sender, EventArgs e)
        {
            label3.Font = new Font(label3.Font, FontStyle.Regular);
            label3.BackColor = System.Drawing.SystemColors.Control;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Console.WriteLine(">Label_3::clicked");
        }

        private void label4_MouseEnter(object sender, EventArgs e)
        {
            label4.Font = new Font(label4.Font, FontStyle.Underline);
            label4.BackColor = System.Drawing.SystemColors.ControlDark;
        }

        private void label4_MouseLeave(object sender, EventArgs e)
        {
            label4.Font = new Font(label4.Font, FontStyle.Regular);
            label4.BackColor = System.Drawing.SystemColors.Control;
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Console.WriteLine(">Label_4::clicked");
        }

        private void label5_MouseEnter(object sender, EventArgs e)
        {
            label5.Font = new Font(label5.Font, FontStyle.Underline);
            label5.BackColor = System.Drawing.SystemColors.ControlDark;
        }

        private void label5_MouseLeave(object sender, EventArgs e)
        {
            label5.Font = new Font(label5.Font, FontStyle.Regular);
            label5.BackColor = System.Drawing.SystemColors.Control;
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Console.WriteLine(">Label_5::clicked");
            Display_Todo_Item();
        }

        private void label6_MouseEnter(object sender, EventArgs e)
        {
            label6.Font = new Font(label6.Font, FontStyle.Underline);
            label6.BackColor = System.Drawing.SystemColors.ControlDark;
        }

        private void label6_MouseLeave(object sender, EventArgs e)
        {
            label6.Font = new Font(label6.Font, FontStyle.Regular);
            label6.BackColor = System.Drawing.SystemColors.Control;
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Console.WriteLine(">Label_6::clicked");
        }

        //할일 입력창
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
                    textBox3.Text = m_Data[m_data_position].DC_title;
                    return;
                }

                m_Data = m_Controller.Get_Model().GetDataCollection();
                m_Data[m_data_position].DC_title = textBox3.Text;

                int pos = 0;
                foreach (Todo_Item item in flowLayoutPanel2.Controls)
                {
                    if (pos == m_data_position)
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
                textBox3.Text = m_Data[m_data_position].DC_title;
                return;
            }

            m_Data = m_Controller.Get_Model().GetDataCollection();
            m_Data[m_data_position].DC_title = textBox3.Text;

            int pos = 0;
            foreach (Todo_Item item in flowLayoutPanel2.Controls)
            {
                if (pos == m_data_position)
                {
                    item.TD_title = textBox3.Text;
                    break;
                }
                pos++;
            }
        }

        // 상세창 닫기 버튼
        private void button1_Click(object sender, EventArgs e)
        {
            if (isTodo_detail)
            {
                splitContainer2.SplitterDistance = splitContainer2.Width;
                isTodo_detail = false;
            }
            Display_Todo_Item();
        }

        //상세창 삭제 버튼
        private void button2_Click_1(object sender, EventArgs e)
        {
            int pos = 0;
            if (isTodo_detail)
            {
                m_Data = m_Controller.Get_Model().GetDataCollection();
                foreach (Todo_Item item in flowLayoutPanel2.Controls)
                {
                    if (pos == m_data_position)
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
                isTodo_detail = false;
            }
            Display_Todo_Item();
        }

        //상세창 완료 체크시
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (isTodo_detail)
            {
                m_Data = m_Controller.Get_Model().GetDataCollection();
                //m_Data[m_data_position].DC_complete = 

                int pos = 0;
                foreach (Todo_Item item in flowLayoutPanel2.Controls)
                {
                    if (pos == m_data_position)
                    {
                        item.TD_title = textBox3.Text;
                        break;
                    }
                    pos++;
                }
            }
            splitContainer2.SplitterDistance = splitContainer2.Width;
            isTodo_detail = false;
        }
    }
}
