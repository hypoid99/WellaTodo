using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

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

        string m_FileName = "memo.dat";
        public bool IsUnsaved { get; set; } = false;

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
        }

        private void BulletinBoardForm_Load(object sender, EventArgs e)
        {
            Initiate();

            Open_File();
        }

        private void BulletinBoardForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsUnsaved)
            {
                DialogResult savePrompt = MessageBox.Show("저장할까요?", "BulletinBoard", MessageBoxButtons.YesNoCancel);

                switch (savePrompt)
                {
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        return;
                    case DialogResult.No:
                        break;
                    case DialogResult.Yes:
                        Save_File();
                        break;
                }
            }

            IsUnsaved = false;

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
            Console.WriteLine("BulletinBoardForm::label_Menu_Click");
        }

        private void Post_it_Click(object sender, UserCommandEventArgs e)
        {
            Console.WriteLine("BulletinBoardForm::note_Post_it_Click");

            Post_it note = (Post_it)sender;

            switch (e.CommandName)
            {
                case "Edit":
                    Show_Note_Editor(note);
                    break;
                case "Delete":
                    Delete_Note(note);
                    break;
                case "Changed":
                    IsUnsaved = true;
                    break;
            }
        }

        private void Update_Notes_Position()
        {
            Console.WriteLine("BulletinBoardForm::Update_Notes_Position");
            int posX = 10;
            int posY = 10;
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

        private void Show_Note_Editor(Post_it note)
        {
            Console.WriteLine("BulletinBoardForm::Show_Note_Editor");
            memoEditorForm.TextBoxRTFString = note.TextBoxRTFString;
            memoEditorForm.StartPosition = FormStartPosition.CenterParent;
            memoEditorForm.ShowDialog();
            note.TextBoxRTFString = memoEditorForm.TextBoxRTFString;
        }

        private void New_Note()
        {
            Console.WriteLine("BulletinBoardForm::New_Note");
            Post_it note = new Post_it();
            panel_Bulletin.Controls.Add(note);
            note.Size = new Size(NOTE_WIDTH, NOTE_HEIGHT);
            note.Post_it_Click += new Post_it_Event(Post_it_Click);

            Update_Notes_Position();
        }

        private void Delete_Note(Post_it note)
        {
            Console.WriteLine("BulletinBoardForm::Delete_Note");
            note.Post_it_Click -= new Post_it_Event(Post_it_Click);
            panel_Bulletin.Controls.Remove(note);
            note.Dispose();

            IsUnsaved = true;

            Update_Notes_Position();
        }

        private void Save_File()
        {
            Console.WriteLine("BulletinBoardForm::Save_File");

            List<string> stringRTF = new List<string>();

            foreach (Post_it note in panel_Bulletin.Controls)
            {
                stringRTF.Add(note.TextBoxRTFString);
            }

            Stream ws = new FileStream(m_FileName, FileMode.Create);
            BinaryFormatter serializer = new BinaryFormatter();
            serializer.Serialize(ws, stringRTF);
            ws.Close();

            IsUnsaved = false;
        }

        private void Open_File()
        {
            Console.WriteLine("BulletinBoardForm::Open_File");

            List<string> stringRTF = new List<string>();

            if (File.Exists(m_FileName))
            {
                try
                {
                    Stream rs = new FileStream(m_FileName, FileMode.Open);
                    BinaryFormatter deserializer = new BinaryFormatter();
                    stringRTF = (List<string>)deserializer.Deserialize(rs);
                    rs.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            foreach (string str in stringRTF)
            {
                Post_it note = new Post_it();
                panel_Bulletin.Controls.Add(note);
                note.Size = new Size(NOTE_WIDTH, NOTE_HEIGHT);
                note.Post_it_Click += new Post_it_Event(Post_it_Click);
                note.TextBoxRTFString = str;

                Console.WriteLine(note.TextBoxRTFString);
            }

            IsUnsaved = false;
        }



        private void button_Label_Click(object sender, EventArgs e)
        {
            Console.WriteLine("BulletinBoardForm::button_Label_Click");
            Popup complex;
            Panel cp;
            cp = new Panel();
            cp.Size = new Size(300, 300);
            Post_it po = new Post_it();

            cp.Controls.Add(po);

            complex = new Popup(cp);
            complex.Show(sender as Button);
        }
    }
}
