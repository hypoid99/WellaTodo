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

        private void BulletinBoardForm_Load(object sender, EventArgs e)
        {
            Initiate();

            //Open_File();

            Update_BulletinBoard();
        }

        private void BulletinBoardForm_Resize(object sender, EventArgs e)
        {
            Update_BulletinBoard();
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

        //--------------------------------------------------------------
        // 초기화 및 디스플레이 업데이트
        //--------------------------------------------------------------
        private void Initiate()
        {
            pictureBox_Add_Note.Location = new Point(panel_Header.Width - 45, 4);

            IEnumerable<CDataCell> dataset = m_Controller.Query_BulletineBoard();
            foreach (CDataCell data in dataset)  // Post_it 생성 및 m_Post_it에 저장
            {
                Post_it note = new Post_it();

                note.Size = new Size(NOTE_WIDTH, NOTE_HEIGHT);
                note.Post_it_Click -= new Post_it_Event(Post_it_Click);
                note.Post_it_Click += new Post_it_Event(Post_it_Click);
                note.TextBoxRTFString = data.DC_memoRTF;
                //note.MemoColor = data.DC_memoColor;
                note.MemoColor = Color.Gold;

                panel_Bulletin.Controls.Add(note);
            }

            IsUnsaved = false;
            Update_BulletinBoard();
        }

        private void Update_BulletinBoard()
        {
            pictureBox_Add_Note.Location = new Point(panel_Header.Width - 45, 4);

            Update_Notes_Position();
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

        //--------------------------------------------------------------
        // Model 이벤트
        //--------------------------------------------------------------
        public void ModelObserver_Event_method(IModel m, ModelEventArgs e)
        {
            Console.WriteLine(">MainFrame::ModelObserver_Event_method");
            // Model에서 온 데이타로 View를 업데이트
        }

        public void Update_View(IModel m, ModelEventArgs e)
        {
            //Console.WriteLine(">MainFrame::Update_View");
            CDataCell dc = e.Item;
            WParam param = e.Param;
            switch (param)
            {
                case WParam.WM_LOAD_DATA:
                    //Update_Load_Data();
                    break;
                case WParam.WM_SAVE_DATA:
                    //Update_Save_Data();
                    break;
                case WParam.WM_OPEN_DATA:
                    //Update_Open_Data();
                    break;
                case WParam.WM_PRINT_DATA:
                    //Update_Print_Data();
                    break;
                case WParam.WM_COMPLETE_PROCESS:
                    //Update_Complete_Process(dc);
                    break;
                case WParam.WM_IMPORTANT_PROCESS:
                    //Update_Important_Process(dc);
                    break;
                case WParam.WM_MODIFY_TASK_TITLE:
                    //Update_Modify_Task_Title(dc);
                    break;
                case WParam.WM_MODIFY_TASK_MEMO:
                    //Update_Modify_Task_Memo(dc);
                    break;
                case WParam.WM_BULLETINBOARD_ADD:
                    Update_New_Note(dc);
                    break;
                case WParam.WM_TASK_DELETE:
                    //Update_Delete_Task(dc);
                    break;
                case WParam.WM_TASK_MOVE_TO:
                    //Update_Task_Move_To(dc);
                    break;
                case WParam.WM_TASK_MOVE_UP:
                    //Update_Task_Move_Up(dc);
                    break;
                case WParam.WM_TASK_MOVE_DOWN:
                    //Update_Task_Move_Down(dc);
                    break;
                case WParam.WM_MODIFY_MYTODAY:
                    //Update_Modify_MyToday(dc);
                    break;
                case WParam.WM_MODIFY_REMIND:
                    //Update_Modify_Remind(dc);
                    break;
                case WParam.WM_MODIFY_PLANNED:
                    //Update_Modify_Planned(dc);
                    break;
                case WParam.WM_MODIFY_REPEAT:
                    //Update_Modify_Repeat(dc);
                    break;
                case WParam.WM_MENULIST_ADD:
                    //Update_Menulist_Add(dc);
                    break;
                case WParam.WM_MENULIST_RENAME:
                    //Update_Menulist_Rename(dc);
                    break;
                case WParam.WM_MENULIST_DELETE:
                    //Update_Menulist_Delete(dc);
                    break;
                case WParam.WM_MENULIST_UP:
                    //Update_Menulist_Up(dc);
                    break;
                case WParam.WM_MENULIST_DOWN:
                    //Update_Menulist_Down(dc);
                    break;
                case WParam.WM_TRANSFER_TASK:
                    //Update_Transfer_Task(dc);
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

        // ----------------------------------------------------------
        // 포스트잇 새로만들기, 열기, 저장, 삭제하기
        // ----------------------------------------------------------
        private void New_Note()
        {
            CDataCell dc = new CDataCell();
            DateTime dt = DateTime.Now;

            dc.DC_listName = "작업";
            dc.DC_title = "새로운 메모를 작성하세요";
            dc.DC_bulletin = true;
            dc.DC_memoRTF = "";
            dc.DC_memoColor = "Gold";

            Send_Log_Message("1>BulletinBoard::New_Note -> Add Note : " + dc.DC_title);
            m_Controller.Perform_Add_BulletinBoard(dc);
        }

        private void Update_New_Note(CDataCell dc)
        {
            Post_it note = new Post_it();

            note.Size = new Size(NOTE_WIDTH, NOTE_HEIGHT);
            note.Post_it_Click -= new Post_it_Event(Post_it_Click);
            note.Post_it_Click += new Post_it_Event(Post_it_Click);
            note.TextBoxRTFString = dc.DC_memoRTF;
            //note.MemoColor = data.DC_memoColor;
            note.MemoColor = Color.Gold;

            panel_Bulletin.Controls.Add(note);

            Update_Notes_Position();
            Send_Log_Message("4>BulletinBoard::Update_New_Note -> Add Note : " + dc.DC_title);
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

        // ----------------------------------------------------------
        // 포스트잇 클릭 처리
        // ----------------------------------------------------------
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

        private void Show_Note_Editor(Post_it note)
        {
            Console.WriteLine("BulletinBoardForm::Show_Note_Editor");
            memoEditorForm.TextBoxRTFString = note.TextBoxRTFString;
            memoEditorForm.StartPosition = FormStartPosition.CenterParent;
            memoEditorForm.ShowDialog();
            note.TextBoxRTFString = memoEditorForm.TextBoxRTFString;
        }

        // ----------------------------------------------------------
        // 메뉴, 툴바
        // ----------------------------------------------------------
        private void pictureBox_Add_Note_Click(object sender, EventArgs e)
        {
            Console.WriteLine("BulletinBoardForm::pictureBox_Add_Note_Click");
            New_Note();
        }

        private void button_Label_Click(object sender, EventArgs e)
        {
            Console.WriteLine("BulletinBoardForm::button_Label_Click");
        }

        private void label_Menu_Click(object sender, EventArgs e)
        {
            Console.WriteLine("BulletinBoardForm::label_Menu_Click");
        }

    }
}
