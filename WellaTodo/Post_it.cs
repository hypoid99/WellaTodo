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
    public delegate void Post_it_Event(object sender, UserCommandEventArgs e);

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
        private void pictureBox_Edit_Click(object sender, EventArgs e)
        {
            Console.WriteLine("pictureBox_Edit_Click");
            if (Post_it_Click != null) Post_it_Click?.Invoke(this, new UserCommandEventArgs ("Edit"));
        }

        private void richTextBox_DoubleClick(object sender, EventArgs e)
        {
            Console.WriteLine("richTextBox_DoubleClick");
            if (Post_it_Click != null) Post_it_Click?.Invoke(this, new UserCommandEventArgs("Edit"));
        }

        private void pictureBox_Delete_Click(object sender, EventArgs e)
        {
            Console.WriteLine("pictureBox_Delete_Click");
            if (Post_it_Click != null) Post_it_Click?.Invoke(this, new UserCommandEventArgs("Delete"));
        }

        private void richTextBox_TextChanged(object sender, EventArgs e)
        {
            Console.WriteLine("richTextBox_TextChanged");
            if (Post_it_Click != null) Post_it_Click?.Invoke(this, new UserCommandEventArgs("Changed"));
        }

        private void pictureBox_ColorPallet_Click(object sender, EventArgs e)
        {
            Console.WriteLine("pictureBox_ColorPallet_Click");
            Popup complex;
            Panel cp;
            cp = new Panel();
            cp.Size = new Size(500, 500);
            Button btn1 = new Button();
            btn1.Text = "No1";
            btn1.Location = new Point(10, 10);
            Button btn2 = new Button();
            btn2.Text = "No2";
            btn2.Location = new Point(10, 50);
            cp.Controls.Add(btn1);
            cp.Controls.Add(btn2);

            complex = new Popup(cp);
            complex.Show(sender as PictureBox);
        }

        private void pictureBox_Image_Click(object sender, EventArgs e)
        {

        }

    }
}
