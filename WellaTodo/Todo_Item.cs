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
    public partial class Todo_Item : UserControl
    {
        public event UserControl_Event UserControl_Event_method;

        private string _title;

        public int IdNum { get; set; }

        public string TD_title
        {
            get { return _title; }
            set { _title = value; label1.Text = value; }
        }

        public bool IsCompleteClicked { get => isCompleteClicked; set => isCompleteClicked = value; }
        public bool IsImportantClicked { get => isImportantClicked; set => isImportantClicked = value; }

        private bool isCompleteClicked = false;
        private bool isImportantClicked = false;

        public Todo_Item()
        {
            InitializeComponent();
        }

        public Todo_Item(int idnum, string text, bool chk)
        {
            InitializeComponent();
            IdNum = idnum;
            _title = text;
            label1.Text = text;
            checkBox1.Checked = chk;
        }

        private void Repaint()
        {
            if (checkBox1.Checked)
            {
                label1.Font = new Font(label1.Font.Name, label1.Font.SizeInPoints, FontStyle.Strikeout);
            }
            else
            {
                label1.Font = new Font(label1.Font.Name, label1.Font.SizeInPoints, FontStyle.Regular);
            }

            checkBox2.Location = new Point(this.Width - 50, checkBox2.Location.Y);
        }

        public bool isCompleted()
        {
            return checkBox1.Checked;
        }

        public bool isImportant()
        {
            return checkBox2.Checked;
        }

        //
        // event
        //

        private void Todo_Item_Load(object sender, EventArgs e)
        {
            Repaint();
        }

        private void Todo_Item_Paint(object sender, PaintEventArgs e)
        {
            Repaint();
        }

        private void Todo_Item_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(125, 125, 125);
        }

        private void Todo_Item_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(255, 126, 0);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                IsCompleteClicked = true;
                UserControl_Event_method?.Invoke(this, e);
                BackColor = Color.FromArgb(100, 100, 100);
                IsCompleteClicked = false;
            }
            else
            {
                IsCompleteClicked = true;
                UserControl_Event_method?.Invoke(this, e);
                BackColor = Color.FromArgb(255, 126, 0);
                IsCompleteClicked = false;
            }
            Repaint();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                IsImportantClicked = true;
                UserControl_Event_method?.Invoke(this, e);
                IsImportantClicked = false;
            }
            else
            {
                IsImportantClicked = true;
                UserControl_Event_method?.Invoke(this, e);
                IsImportantClicked = false;
            }
        }

        private void Todo_Item_MouseClick(object sender, MouseEventArgs e)
        {
            if (UserControl_Event_method != null)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        UserControl_Event_method?.Invoke(this, e);
                        break;
                    case MouseButtons.Right:
                        MessageBox.Show("Right Button");
                        break;
                }
            }
        }
    }
}


/*
private void checkBox2_Paint(object sender, PaintEventArgs e)
{
Console.WriteLine("checkbox repaint");
CheckState cs = checkBox2.CheckState;
if (cs == CheckState.Checked)
{
using (SolidBrush brush = new SolidBrush(checkBox2.BackColor))
   e.Graphics.FillRectangle(brush, 0, 1, 14, 14);
e.Graphics.FillRectangle(Brushes.Green, 3, 4, 8, 8);
e.Graphics.DrawRectangle(Pens.Black, 0, 1, 13, 13);
Console.WriteLine("checkbox repaint-----------------------");
}
}
*/