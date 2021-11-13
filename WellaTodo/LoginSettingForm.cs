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
    public partial class LoginSettingForm : Form
    {
        public int ColorTheme { get; set; } = 1;
        public bool IsSaveClose { get; set; } = false;
        public string UserName { get; set; }

        public LoginSettingForm()
        {
            InitializeComponent();
        }

        private void LoginSettingForm_Load(object sender, EventArgs e)
        {
            UserName = "이름을 기입하세요";
            textBox1.Text = UserName;
            switch (ColorTheme)
            {
                case 1:
                    radioButton1.Checked = true;
                    break;
                case 2:
                    radioButton2.Checked = true;
                    break;
            }
        }

        private void LoginSettingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (radioButton1.Checked) ColorTheme = 1;
            if (radioButton2.Checked) ColorTheme = 2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IsSaveClose = false;
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            IsSaveClose = true;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.TextLength == 0) return;
            UserName = textBox1.Text;
            Close();
        }
    }
}
