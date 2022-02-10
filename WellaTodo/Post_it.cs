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
    public delegate void Post_it_Event(object sender, EventArgs e);

    public partial class Post_it : UserControl
    {
        public event Post_it_Event Post_it_Click;

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

        public Post_it()
        {
            InitializeComponent();
        }

        private void Post_it_Load(object sender, EventArgs e)
        {
            Initiate();
        }

        private void Post_it_Resize(object sender, EventArgs e)
        {
            Update_Post_it();
        }

        private void Initiate()
        {
            TextBoxString = "새로운 메모를 작성하세요";
            string rtfFormattedString = richTextBox.Rtf;
            TextBoxRTFString = rtfFormattedString;
            richTextBox.ReadOnly = true;

            pictureBox_Delete.Location = new Point(panel_Header.Width - 28, 4);
        }

        private void Update_Post_it()
        {
            pictureBox_Delete.Location = new Point(panel_Header.Width - 28, 4);
        }

        //
        // 툴바 이벤트
        //
        private void pictureBox_Close_Click(object sender, EventArgs e)
        {
            Console.WriteLine("pictureBox_Close_Click");
            if (Post_it_Click != null) Post_it_Click?.Invoke(this, e);
        }

        private void pictureBox_Edit_Click(object sender, EventArgs e)
        {
            Console.WriteLine("pictureBox_Edit_Click");
            if (Post_it_Click != null) Post_it_Click?.Invoke(this, e);
        }

        private void richTextBox_DoubleClick(object sender, EventArgs e)
        {
            Console.WriteLine("richTextBox_DoubleClick");
            if (Post_it_Click != null) Post_it_Click?.Invoke(this, e);
        }
    }
}
