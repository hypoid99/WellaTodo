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
    public partial class MainFrame : Form, IView, IModelObserver
    {
        IController m_Controller;
        List<CDataCell> m_Data;

        public event ViewHandler<IView> Changed_View_Event;

        public MainFrame()
        {
            InitializeComponent();
        }

        public void SetController(IController controller)
        {
            m_Controller = controller;
        }

        public void Initiate_View()
        {
        /*
            m_Data = m_Controller.Get_Model().GetDataCollection();

            textBox1.Text = "Hi World, Welcome!";

            dataGridView1.ColumnCount = 4;
            dataGridView1.Columns[0].Name = "완료";
            dataGridView1.Columns[1].Name = "내용";
            dataGridView1.Columns[2].Name = "중요";
            dataGridView1.Columns[3].Name = "대상";
            foreach (CDataCell data in m_Data)
            {
                string complete = data.DC_complete;
                string title = data.DC_title;
                string important = data.DC_important;
                string person = data.DC_person;

                dataGridView1.Rows.Add(complete, title, important, person);
            }
            //dataGridView1.DataSource = m_Data;

            listView1.View = View.Details;
            listView1.BeginUpdate();
            listView1.Columns.Add("완료");
            listView1.Columns.Add("내용");
            listView1.Columns.Add("중요");
            listView1.Columns.Add("대상");
            foreach (CDataCell data in m_Data)
            {
                string complete = data.DC_complete;
                string title = data.DC_title;
                string important = data.DC_important;
                string person = data.DC_person;

                ListViewItem item = new ListViewItem(complete);
                item.SubItems.Add(title);
                item.SubItems.Add(important);
                item.SubItems.Add(person);
                listView1.Items.Add(item);
            }
            listView1.EndUpdate();
        */
        }

        public void Clear_View()
        {

        }

        public void Add_Model_To_View(CDataCell dc)
        {
            
        }

        public void Update_View_With_Changed_Model(CDataCell dc)
        {

        }

        public void Remove_Model_From_View(CDataCell dc)
        {

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

        private void MainFrame_Load(object sender, EventArgs e)
        {
            //m_Controller.Initiate_View();
            Initiate_View();
            Repaint();
        }

        private void Repaint()
        {
            label1.Width = splitContainer1.SplitterDistance;
            label2.Width = splitContainer1.SplitterDistance;
            label3.Width = splitContainer1.SplitterDistance;
            label4.Width = splitContainer1.SplitterDistance;
            label5.Width = splitContainer1.SplitterDistance;
            label6.Width = splitContainer1.SplitterDistance;

            tabControl1.Width = splitContainer1.Panel2.Width;
            tabControl1.Height = splitContainer1.Panel2.Height;

            splitContainer1.Refresh();
        }

        private void splitContainer1_Resize(object sender, EventArgs e)
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
            tabControl1.SelectedIndex = 0;

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
            tabControl1.SelectedIndex = 1;
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
            tabControl1.SelectedIndex = 2;
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
            tabControl1.SelectedIndex = 3;
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
            tabControl1.SelectedIndex = 4;
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
            tabControl1.SelectedIndex = 5;
        }
    }
}
