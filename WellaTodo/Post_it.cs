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

        public Post_it()
        {
            InitializeComponent();

            Initiate();
        }

        private void Initiate()
        {
            richTextBox.Text = "새로운 메모를 작성하세요";
            richTextBox.ReadOnly = true;

        }

        private void richTextBox_DoubleClick(object sender, EventArgs e)
        {
            Console.WriteLine("richTextBox_DoubleClick");
        }

        private void richTextBox_Enter(object sender, EventArgs e)
        {
            Console.WriteLine("richTextBox_Enter");
        }

        private void pictureBox_Add_Note_Click(object sender, EventArgs e)
        {
            if (Post_it_Click != null) Post_it_Click?.Invoke(this, e);
        }
    }
}
