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
        public string UserName { get; set; } = "홍길동";

        public LoginSettingForm()
        {
            InitializeComponent();
        }

        private void LoginSettingForm_Load(object sender, EventArgs e)
        {
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

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            FormEffect(this);
        }

        private void FormEffect(Form fm)
        {
            double[] opacity = new double[] { 0.1d, 0.3d, 0.7d, 0.8d, 0.9d, 1.0d };
            int cnt = 0;
            Timer tm = new Timer();
            {
                fm.RightToLeftLayout = false;
                fm.Opacity = 0d;
                tm.Interval = 100;   // 나타나는 속도를 조정함.          
                tm.Tick += delegate (object obj, EventArgs e)
                {
                    if ((cnt + 1 > opacity.Length) || fm == null)
                    {
                        tm.Stop();
                        tm.Dispose();
                        tm = null;
                        return;
                    }
                    else
                    {
                        fm.Opacity = opacity[cnt++];
                    }
                };
                tm.Start();
            }
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
