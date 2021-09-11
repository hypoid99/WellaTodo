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

namespace WellaTodo
{
    public delegate void UserControl_Event(int val, int idx);

    public partial class MainFrame : Form, IView, IModelObserver
    {
        MainController m_Controller;

        List<CDataCell> m_Data = new List<CDataCell>();

        public event ViewHandler<IView> Changed_View_Event;

        bool isTodo_detail = false;
        int m_data_position;

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

        private void Load_Item()
        {
            int idx;
            string text;
            bool chk;

            m_Data = m_Controller.Get_Model().GetDataCollection();

            foreach (CDataCell data in m_Data)
            {
                text = data.DC_title;
                chk = false;
                idx = data.DC_idNum;
                Todo_Item item = new Todo_Item(idx, text, chk);
                splitContainer2.Panel1.Controls.Add(item);
                item.UserControl_Event_method += new UserControl_Event(Click_Todo_Item);
            }
            Display_Todo_Item();
        }

        private void Add_Item(string text, bool chk)
        {
            int idx;
            m_Data = m_Controller.Get_Model().GetDataCollection();
            idx = m_Data.Count;
            idx++;

            Todo_Item item = new Todo_Item(idx, text, chk);
            splitContainer2.Panel1.Controls.Add(item);
            item.UserControl_Event_method += new UserControl_Event(Click_Todo_Item);

            //m_Controller.performAddItem();
            m_Data.Add(new CDataCell(idx, chk, text, false, "메모추가"));

            Display_Todo_Item();
        }

        private void Click_Todo_Item(int val, int idx)
        {
            switch (val)
            {
                case 1:
                    //m_Controller.PerformCheckedComplete();
                    break;
                case 2:
                    //m_Controller.PerformUncheckedComplete();
                    break;
                case 3:
                    //m_Controller.PerformClickedTodoItem();
                    int pos = 0;
                    int before_data_position;

                    before_data_position = m_data_position;

                    foreach (Todo_Item item in splitContainer2.Panel1.Controls)
                    {
                        pos++;
                        if (idx == item.IdNum) m_data_position = pos;
                    }

                    if (isTodo_detail && (before_data_position == m_data_position))
                    {
                        splitContainer2.SplitterDistance = splitContainer2.Width;
                        isTodo_detail = false;
                    }
                    else
                    {
                        splitContainer2.SplitterDistance = splitContainer2.Width / 2;
                        isTodo_detail = true;

                        m_Data = m_Controller.Get_Model().GetDataCollection();
                        textBox3.Text = m_Data[m_data_position - 1].DC_title;
                        textBox1.Text = "Data Position :" + m_data_position.ToString();
                    }
                    break;
                case 4:
                    //m_Controller.PerformCheckedImportant();
                    break;
                case 5:
                    //m_Controller.PerformUncheckedImportant();
                    break;
            }
            Display_Todo_Item();

            //m_Controller.Update_Model();
        }

        private void Display_Todo_Item()
        {
            int pos = 1;
            int hgt = 1;
            bool hasCompleted = false;

            m_Data = m_Controller.Get_Model().GetDataCollection();

            //Display Todo data
            foreach (Todo_Item item in splitContainer2.Panel1.Controls)
            {
                hgt = item.Height;
                if (item.isCompleted() == false)
                {
                    item.Top = pos;
                    item.Width = splitContainer2.Panel1.Width;
                    pos = item.Top + item.Height + 1;
                }
                else
                {
                    hasCompleted = true;
                }
            }
            //Display Completed Todo data
            if (hasCompleted)
            {
                //label7.Visible = true;
                //label7.Top = pos;
                //pos = label7.Top + label7.Height + 1;
                //label7.Text = "완료됨";
                pos = pos + hgt + 10;
                foreach (Todo_Item item in splitContainer2.Panel1.Controls)
                {
                    if (item.isCompleted() == true)
                    {
                        item.Top = pos;
                        item.Width = splitContainer2.Panel1.Width;
                        pos = item.Top + item.Height + 1;
                    }
                }
            }
            else
            {
                //label7.Visible = false;
                //label7.Text = "완료없음";
            }
        }

        private void Display_Data()
        {
            m_Data = m_Controller.Get_Model().GetDataCollection();
            Console.WriteLine(">Detail Data Position : [{0}]", m_data_position);
            foreach (Todo_Item item in splitContainer2.Panel1.Controls)
            {         
                Console.WriteLine(">Data TD:[{0}]-[{1}]", item.IdNum, item.TD_title);
            }

            foreach (CDataCell data in m_Data)
            {
                Console.WriteLine(">Data DC:[{0}]-[{1}]", data.DC_idNum, data.DC_title);
            }
            Console.WriteLine(">------------------"); 
        }

        private void Repaint()
        {
            label1.Width = splitContainer1.SplitterDistance;
            label2.Width = splitContainer1.SplitterDistance;
            label3.Width = splitContainer1.SplitterDistance;
            label4.Width = splitContainer1.SplitterDistance;
            label5.Width = splitContainer1.SplitterDistance;
            label6.Width = splitContainer1.SplitterDistance;

            splitContainer1.Refresh();

            splitContainer2.Location = new Point(10, 10);
            splitContainer2.Size = new Size(splitContainer1.Panel2.Width - 20, splitContainer1.Panel2.Height - 50);

            textBox2.Location = new Point(10, splitContainer1.Panel2.Height - 35);
            textBox2.Size = new Size(splitContainer1.Panel2.Width - 20, 25);

            foreach (Control contr in splitContainer2.Panel1.Controls)
            {
                if (contr is Todo_Item)
                {
                    contr.Width = splitContainer2.Panel1.Width;
                }
            }
            splitContainer2.Refresh();
        }

        //
        // Control Event --------------------------------------------------------------------
        //

        private void MainFrame_Load(object sender, EventArgs e)
        {
            Initiate_View();
            Load_Item();
        }

        private void MainFrame_Resize(object sender, EventArgs e)
        {
            Repaint();
        }

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
            if (splitContainer1.IsSplitterFixed )
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
                } else
                {
                    splitContainer1.IsSplitterFixed = false;
                }
            }
        }

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

        private void button2_Click(object sender, EventArgs e)
        {
            Console.WriteLine(">MainFrame::button2_Click");
            if (textBox2.Text .Trim ().Length == 0)
            {
                MessageBox.Show("Invalid Text");
                return;
            }
            Add_Item(textBox2.Text, false);
            textBox2.Text = "";

            m_Controller.Update_Model();
        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox2.Text.Trim().Length == 0) return;
                Add_Item(textBox2.Text, false);
                textBox2.Text = "";
            }
        }

        private void textBox3_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox3.Text.Trim().Length == 0)
                {
                    textBox3.Text = m_Data[m_data_position - 1].DC_title;
                    return;
                }

                m_Data = m_Controller.Get_Model().GetDataCollection();
                foreach (Todo_Item item in splitContainer2.Panel1.Controls)
                {
                    if (item.IdNum == m_data_position)
                    {
                        item.TD_title = textBox3.Text;
                        m_Data[m_data_position - 1].DC_title = textBox3.Text;
                    }
                }
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            m_Data = m_Controller.Get_Model().GetDataCollection();
            foreach (Todo_Item item in splitContainer2.Panel1.Controls)
            {
                if (item.IdNum == m_data_position)
                {
                    if (textBox3.Text.Trim().Length == 0)
                    {
                        textBox3.Text = m_Data[m_data_position - 1].DC_title;
                        return;
                    }
                    item.TD_title = textBox3.Text;
                    m_Data[m_data_position - 1].DC_title = textBox3.Text;
                }
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
                foreach (Todo_Item item in splitContainer2.Panel1.Controls)
                {
                    pos++;
                    if (pos == m_data_position)
                    {
                        item.UserControl_Event_method -= new UserControl_Event(Click_Todo_Item);
                        splitContainer2.Panel1.Controls.Remove(item);
                        item.Dispose();

                        m_Data.Remove(m_Data[pos-1]);
                        break;
                    }
                }

                splitContainer2.SplitterDistance = splitContainer2.Width;
                isTodo_detail = false;
            }
            Display_Todo_Item();
        }
    }
}
