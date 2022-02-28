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
    public partial class OutputForm : Form, IView, IModelObserver
    {
        public event ViewHandler<IView> View_Changed_Event;

        MainController m_Controller;

        private string _textBoxString;
        public string TextBoxString { get => _textBoxString; set { _textBoxString = value; OutputText(value); } }

        public OutputForm()
        {
            InitializeComponent();
        }
        public void SetController(MainController controller)
        {
            m_Controller = controller;
        }

        public void Update_View(IModel m, ModelEventArgs e)
        {
            //CDataCell dc = e.Item;
            WParam param = e.Param;
            string msg = e.Msg;
            switch (param)
            {
                case WParam.WM_LOG_MESSAGE:
                    Output_Message(msg);
                    break;
                default:
                    break;
            }
        }

        private void Send_Log_Message(string msg)
        {
            try
            {
                View_Changed_Event.Invoke(this, new ViewEventArgs(msg));
            }
            catch (Exception)
            {
                MessageBox.Show("Please enter a valid number");
            }
        }

        private void Output_Message(string msg)
        {
            if (msg.Length == 0) return;

            if ((msg.Length + textBox1.TextLength) > textBox1.MaxLength)
            {
                textBox1.Clear();
                TextBoxString = "문자열이 너무 깁니다";
                textBox1.AppendText(TextBoxString);
            }
            else
                textBox1.AppendText(msg+"\r\n");
        }

        private void OutputText(string txt)
        {
            if (txt.Length  == 0) return;

            if ((txt.Length + textBox1.TextLength) > textBox1.MaxLength)
            {
                textBox1.Clear();
                TextBoxString = "문자열이 너무 깁니다";
                textBox1.AppendText(TextBoxString);
            } 
            else
                textBox1.AppendText(TextBoxString);
        }

        private void OutputForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Hide();
                e.Cancel = true;
            }
        }
    }
}
