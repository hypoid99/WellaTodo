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
    public partial class OutputForm : Form
    {
        private string _textBoxString;
        public string TextBoxString { get => _textBoxString; set { _textBoxString = value; OutputText(value); } }

        public OutputForm()
        {
            InitializeComponent();
        }

        private void OutputText(string txt)
        {
            if (txt.Length  == 0) return;

            if ((txt.Length + textBox1.TextLength) > textBox1.MaxLength)
                MessageBox.Show("문자열이 너무 깁니다");
            else
                textBox1.AppendText(TextBoxString);
        }

        private void OutputForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.Hide();
                e.Cancel = true;
            }
        }
    }
}
