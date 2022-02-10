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
    public partial class BulletinBoardForm : Form, IView, IModelObserver
    {
        public event ViewHandler<IView> View_Changed_Event;

        static readonly int NOTE_WIDTH = 200;
        static readonly int NOTE_HEIGHT = 170;
        static readonly int NOTE_GAP = 10;

        MainController m_Controller;

        MemoEditorForm memoEditorForm = new MemoEditorForm();

        public BulletinBoardForm()
        {
            InitializeComponent();
        }

        public void SetController(MainController controller)
        {
            m_Controller = controller;
        }

        public void Update_View(IModel m, ModelEventArgs e)
        {

        }

        private void Initiate()
        {
            pictureBox_Add_Note.Location = new Point(panel_Header.Width - 45, 4);

            label_Title.Focus();
        }

        private void BulletinBoardForm_Load(object sender, EventArgs e)
        {
            Initiate();
        }

        private void BulletinBoardForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void BulletinBoardForm_Resize(object sender, EventArgs e)
        {
            Update_BulletinBoard();
        }

        private void label_Menu_Click(object sender, EventArgs e)
        {

        }

        private void Post_it_Click(object sender, EventArgs e)
        {
            Console.WriteLine("BulletinBoardForm::note_Post_it_Click");

            Post_it note = (Post_it)sender;
            memoEditorForm.TextBoxRTFString = note.TextBoxRTFString;

            memoEditorForm.StartPosition = FormStartPosition.CenterParent;
            memoEditorForm.ShowDialog();

            note.TextBoxRTFString = memoEditorForm.TextBoxRTFString;
        }

        private void Update_Notes_Position()
        {
            Console.WriteLine("BulletinBoardForm::Update_Notes_Position");
            int posX = 10;
            int posY = 10;
            int num_Notes = panel_Bulletin.Controls.Count;
            int num_Column = panel_Bulletin.Width / (NOTE_WIDTH + NOTE_GAP);
            int cnt = 0;
            foreach (Post_it note in panel_Bulletin.Controls)
            {
                note.Location = new Point(posX, posY);
                posX += NOTE_WIDTH + NOTE_GAP;
                cnt++;
                if (cnt == num_Column)
                {
                    cnt = 0;
                    posX = 10;
                    posY += NOTE_HEIGHT + NOTE_GAP;
                }
            }
        }

        private void pictureBox_Add_Note_Click(object sender, EventArgs e)
        {
            Console.WriteLine("BulletinBoardForm::pictureBox_Add_Note_Click");

            New_Note();
        }

        private void Update_BulletinBoard()
        {
            pictureBox_Add_Note.Location = new Point(panel_Header.Width - 45, 4);

            Update_Notes_Position();
        }

        private void New_Note()
        {
            Post_it note = new Post_it();
            panel_Bulletin.Controls.Add(note);
            note.Size = new Size(NOTE_WIDTH, NOTE_HEIGHT);
            note.Post_it_Click += new Post_it_Event(Post_it_Click);

            Update_Notes_Position();
        }

    }
}
