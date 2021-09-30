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
        private string _txt;
        public string Txt
        {
            get => _txt; 
            set 
            { _txt = value; OutputText(_txt);
            }
        }

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
                textBox1.AppendText(txt);
        }
    }
}
