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

        public Todo_Item()
        {
            InitializeComponent();
        }

        public Todo_Item(string text, bool chk)
        {
            InitializeComponent();
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
                this.BackColor = Color.FromArgb(100, 100, 100);
            }
            else
            {
                this.BackColor = Color.FromArgb(255, 126, 0);
            }
        }

        private void Todo_Item_Click(object sender, EventArgs e)
        {
            if (UserControl_Event_method != null)
            {
                UserControl_Event_method();
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
    }
}
