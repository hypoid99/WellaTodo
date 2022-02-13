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
    public partial class MemoEditorForm : Form
    {
        private string _textBoxString;
        public string TextBoxString 
        { 
            get => _textBoxString;
            set 
            { 
                _textBoxString = value;
                richTextBox.Text = _textBoxString;
            }
        }

        private string _textBoxRTFString;
        public string TextBoxRTFString
        {
            get => _textBoxRTFString;
            set
            {
                _textBoxRTFString = value;
                richTextBox.Rtf = _textBoxRTFString;
            }
        }

        public MemoEditorForm()
        {
            InitializeComponent();
        }

        private void MemoEditorForm_Load(object sender, EventArgs e)
        {
            Initiate();
        }

        private void MemoEditorForm_Resize(object sender, EventArgs e)
        {
            Update_EditorForm();
        }

        private void MemoEditorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            TextBoxRTFString = richTextBox.Rtf;
            TextBoxString = richTextBox.Text;
        }

        private void Initiate()
        {
            panel_Header.BackColor = Color.Gold;
            panel_Footer.BackColor = Color.Gold;
            richTextBox.BackColor = Color.Gold;

            pictureBox_More_Hori.Location = new Point(panel_Header.Width - 72, 4);
            pictureBox_Delete.Location = new Point(panel_Header.Width - 40, 4);
        }

        private void Update_EditorForm()
        {
            pictureBox_More_Hori.Location = new Point(panel_Header.Width - 72, 4);
            pictureBox_Delete.Location = new Point(panel_Header.Width - 40, 4);
        }

        private void pictureBox_Delete_Click(object sender, EventArgs e)
        {

        }

        //
        // Form Border
        //
        /*
        protected override void OnPaint(PaintEventArgs e) // you can safely omit this method if you want
        {
            e.Graphics.FillRectangle(Brushes.Green, Top);
            e.Graphics.FillRectangle(Brushes.Green, Left);
            e.Graphics.FillRectangle(Brushes.Green, Right);
            e.Graphics.FillRectangle(Brushes.Green, Bottom);
        }

        private const int
            HTLEFT = 10,
            HTRIGHT = 11,
            HTTOP = 12,
            HTTOPLEFT = 13,
            HTTOPRIGHT = 14,
            HTBOTTOM = 15,
            HTBOTTOMLEFT = 16,
            HTBOTTOMRIGHT = 17;

        const int _ = 10; // you can rename this variable if you like

        Rectangle Top { get { return new Rectangle(0, 0, this.ClientSize.Width, _); } }
        Rectangle Left { get { return new Rectangle(0, 0, _, this.ClientSize.Height); } }
        Rectangle Bottom { get { return new Rectangle(0, this.ClientSize.Height - _, this.ClientSize.Width, _); } }
        Rectangle Right { get { return new Rectangle(this.ClientSize.Width - _, 0, _, this.ClientSize.Height); } }

        Rectangle TopLeft { get { return new Rectangle(0, 0, _, _); } }
        Rectangle TopRight { get { return new Rectangle(this.ClientSize.Width - _, 0, _, _); } }
        Rectangle BottomLeft { get { return new Rectangle(0, this.ClientSize.Height - _, _, _); } }
        Rectangle BottomRight { get { return new Rectangle(this.ClientSize.Width - _, this.ClientSize.Height - _, _, _); } }

        private const int WM_NCHITTEST = 0x84;
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;

        protected override void WndProc(ref Message message)
        {
            base.WndProc(ref message);

            switch (message.Msg)
            {
                case WM_NCHITTEST:
                    Console.WriteLine("WM_NCHITTEST");
                    var cursor = this.PointToClient(Cursor.Position);

                    if (TopLeft.Contains(cursor)) message.Result = (IntPtr)HTTOPLEFT;
                    else if (TopRight.Contains(cursor)) message.Result = (IntPtr)HTTOPRIGHT;
                    else if (BottomLeft.Contains(cursor)) message.Result = (IntPtr)HTBOTTOMLEFT;
                    else if (BottomRight.Contains(cursor)) message.Result = (IntPtr)HTBOTTOMRIGHT;

                    else if (Top.Contains(cursor)) message.Result = (IntPtr)HTTOP;
                    else if (Left.Contains(cursor)) message.Result = (IntPtr)HTLEFT;
                    else if (Right.Contains(cursor)) message.Result = (IntPtr)HTRIGHT;
                    else if (Bottom.Contains(cursor)) message.Result = (IntPtr)HTBOTTOM;

                    if ((int)message.Result == HTCLIENT)
                    {
                        Console.WriteLine("message.Result == HTCLIENT");
                        message.Result = (IntPtr)HTCAPTION;
                        return;
                    }
                    break;
            }
            base.WndProc(ref message);
        }
        */
    }
}
