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
    public partial class AlarmForm : Form
    {
        private string _textBoxString;
        public string TextBoxString { get => _textBoxString; set { _textBoxString = value; OutputText(value); } }

        public AlarmForm()
        {
            InitializeComponent();

            textBox1.Font = new Font("맑은고딕", 18.0f, FontStyle.Regular);
        }

        private void OutputText(string txt)
        {
            if (txt.Length == 0) return;

            if ((txt.Length + textBox1.TextLength) > textBox1.MaxLength)
                MessageBox.Show("문자열이 너무 깁니다");
            else
                textBox1.AppendText(TextBoxString);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AlarmForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.Hide();
                e.Cancel = true;
            }
        }
    }
}
