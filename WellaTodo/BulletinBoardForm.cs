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

        int m_SelectedMenu = 1;  // 메모 메뉴

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

            Update_Notes_Position();
        }

        private void BulletinBoardForm_Resize(object sender, EventArgs e)
        {
            Update_Notes_Position();
        }

        private void BulletinBoardForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        //--------------------------------------------------------------
        // 초기화 및 데이터 로딩, 디스플레이 업데이트
        //--------------------------------------------------------------
        private void Initiate()
        {
            Load_Data();

            m_SelectedMenu = 1;  // 메모 메뉴
            label_Title.Text = "Bulleting Board - 메모";

            Update_BulletinBoard();
        }

        private void Load_Data()
        {
            //Console.WriteLine("BulletinBoardForm::Load_Data");

            IEnumerable<CDataCell> dataset = m_Controller.Query_BulletineBoard();

            foreach (CDataCell dc in dataset)  // Post_it 생성 및 m_Post_it에 저장
            {
                Post_it note = new Post_it(dc);

                note.Size = new Size(NOTE_WIDTH, NOTE_HEIGHT);
                note.Post_it_Click -= new Post_it_Event(Post_it_Click);
                note.Post_it_Click += new Post_it_Event(Post_it_Click);

                note.MemoTitle = dc.DC_title;
                note.MemoRTFString = dc.DC_memoRTF;
                note.IsBulletin = dc.DC_bulletin;
                note.IsArchive = dc.DC_archive;
                note.MemoTag = dc.DC_memoTag;
                note.MemoColor = Color.FromName(dc.DC_memoColor);

                panel_Bulletin.Controls.Add(note);
                panel_Bulletin.Controls.SetChildIndex(note, 0);
            }
        }

        private void Update_BulletinBoard()
        {
            /*
            switch (m_SelectedMenu)
            {
                case 1:  // 메모
                    List_Note();
                    break;
                case 2:  // 알람
                    break;
                case 3:  // 보관처리
                    List_Archive();
                    break;
                case 4:  // 빨강
                    List_Note(1);
                    break;
                case 5:  // 주황
                    List_Note(2);
                    break;
                case 6:  // 노랑
                    List_Note(3);
                    break;
                case 7:  // 초록
                    List_Note(4);
                    break;
                case 8:  // 파랑
                    List_Note(5);
                    break;
                case 9:  // 해제
                    List_Note(0);
                    break;
            }
            */
            Update_Notes_Position();
        }

        private void Update_Notes_Position()
        {
            //Console.WriteLine("BulletinBoardForm::Update_Notes_Position");
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
            Console.WriteLine(">BulletinBoard::ModelObserver_Event_method");
        }

        public void Update_View(IModel m, ModelEventArgs e)
        {
            //Console.WriteLine(">BulletinBoard::Update_View");
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
                case WParam.WM_BULLETINBOARD_ADD:
                    Update_New_Note(dc);
                    break;
                case WParam.WM_BULLETINBOARD_MODIFY_MEMO:
                    Update_Modify_Memo_BulletinBoard(dc);
                    break;
                case WParam.WM_BULLETINBOARD_DELETE:
                    Update_Delete_BulletinBoard(dc);
                    break;
                case WParam.WM_BULLETINBOARD_MODIFY_TITLE:
                    Update_Modify_Title_BulletinBoard(dc);
                    break;
                case WParam.WM_BULLETINBOARD_MODIFY_ARCHIVE:
                    Update_Modify_Archive_BulletinBoard(dc);
                    break;
                    
                default:
                    break;
            }
        }

        private void Update_New_Note(CDataCell dc)
        {
            Post_it note = new Post_it(dc);

            note.Size = new Size(NOTE_WIDTH, NOTE_HEIGHT);

            note.Post_it_Click -= new Post_it_Event(Post_it_Click);
            note.Post_it_Click += new Post_it_Event(Post_it_Click);

            note.MemoTitle = dc.DC_title;
            Console.WriteLine(note.MemoTitle);
            note.MemoRTFString = dc.DC_memoRTF;
            note.IsBulletin = dc.DC_bulletin;
            note.IsArchive = dc.DC_archive;
            note.MemoTag = dc.DC_memoTag;
            note.MemoColor = Color.FromName(dc.DC_memoColor);

            panel_Bulletin.Controls.Add(note);

            Update_BulletinBoard();
            Send_Log_Message("4>BulletinBoard::Update_New_Note -> Add Note : " + dc.DC_title);
        }

        private void Update_Modify_Title_BulletinBoard(CDataCell dc)
        {
            int counter = 0;
            foreach (Post_it note in panel_Bulletin.Controls)
            {
                if (dc.DC_task_ID == note.DataCell.DC_task_ID)
                {
                    note.MemoTitle = dc.DC_title;

                    counter++;
                    break;
                }
            }

            if (counter == 0)
            {
                Send_Log_Message("Warning>BulletinBoardForm::Update_Modify_BulletinBoard -> No matching Data!!");
                return;
            }

            Update_BulletinBoard();
            Send_Log_Message("4>BulletinBoardForm::Update_Modify_Title_BulletinBoard : -> Completed" + dc.DC_title);
        }

        private void Update_Modify_Archive_BulletinBoard(CDataCell dc)
        {
            int counter = 0;
            foreach (Post_it note in panel_Bulletin.Controls)
            {
                if (dc.DC_task_ID == note.DataCell.DC_task_ID)
                {
                    note.IsArchive = dc.DC_archive;

                    counter++;
                    break;
                }
            }

            if (counter == 0)
            {
                Send_Log_Message("Warning>BulletinBoardForm::Update_Modify_Archive_BulletinBoard -> No matching Data!!");
                return;
            }

            Update_BulletinBoard();
            Send_Log_Message("4>BulletinBoardForm::Update_Modify_Archive_BulletinBoard : -> Completed" + dc.DC_title);
        }

        private void Update_Modify_Memo_BulletinBoard(CDataCell dc)
        {
            int counter = 0;
            foreach (Post_it note in panel_Bulletin.Controls)
            {
                if (dc.DC_task_ID == note.DataCell.DC_task_ID)
                {
                    note.MemoRTFString = dc.DC_memoRTF;
                    note.MemoString = dc.DC_memo;

                    counter++;
                    break;
                }
            }

            if (counter == 0)
            {
                Send_Log_Message("Warning>BulletinBoardForm::Update_Modify_BulletinBoard -> No matching Data!!");
                return;
            }

            Update_BulletinBoard();
            Send_Log_Message("4>BulletinBoardForm::Update_Modify_Memo_BulletinBoard : -> Completed" + dc.DC_title);
        }

        private void Update_Delete_BulletinBoard(CDataCell dc)
        {
            int counter = 0;
            foreach (Post_it note in panel_Bulletin.Controls)
            {
                if (dc.DC_task_ID == note.DataCell.DC_task_ID)
                {
                    note.Post_it_Click -= new Post_it_Event(Post_it_Click);
                    panel_Bulletin.Controls.Remove(note);
                    note.Dispose();

                    counter++;
                    break;
                }
            }

            if (counter == 0)
            {
                Send_Log_Message("Warning>BulletinBoardForm::Update_Delete_BulletinBoard -> No matching Data!!");
            }

            Update_BulletinBoard();
            Send_Log_Message("4>BulletinBoardForm::Update_Delete_BulletinBoard : -> Completed" + dc.DC_title);
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
        // 포스트잇 클릭 처리
        // ----------------------------------------------------------
        private void Post_it_Click(object sender, UserCommandEventArgs e)
        {
            //Console.WriteLine("BulletinBoardForm::note_Post_it_Click");

            Post_it note = (Post_it)sender;

            switch (e.CommandName)
            {
                case "Edit":
                    Edit_Note(note);
                    break;
                case "New":
                    New_Note();
                    break;
                case "Delete":
                    Delete_Note(note);
                    break;
                case "Archive":
                    Archive_Note(note);
                    break;
                case "Color":
                    Change_Color(note);
                    break;
                case "Tag":
                    Change_Tag(note);
                    break;
                case "Title":
                    Change_Title(note);
                    break;
                case "Changed":
                    break;
            }
        }

        private void New_Note()
        {
            CDataCell dc = new CDataCell();

            dc.DC_listName = "작업";
            dc.DC_title = "메모";

            dc.DC_memoRTF = "";
            dc.DC_bulletin = true;
            dc.DC_archive = false;
            dc.DC_memoTag = 0;
            switch (m_SelectedMenu)
            {
                case 4:  // 빨강
                    dc.DC_memoTag = 1;
                    break;
                case 5:  // 주황
                    dc.DC_memoTag = 2;
                    break;
                case 6:  // 노랑
                    dc.DC_memoTag = 3;
                    break;
                case 7:  // 초록
                    dc.DC_memoTag = 4;
                    break;
                case 8:  // 파랑
                    dc.DC_memoTag = 5;
                    break;
                case 9:  // 해제
                    dc.DC_memoTag = 0;
                    break;
            }
            dc.DC_memoColor = "Yellow";

            Send_Log_Message("1>BulletinBoard::New_Note -> Add Note : " + dc.DC_title);
            m_Controller.Perform_Add_BulletinBoard(dc);
        }

        private void Edit_Note(Post_it note)
        {
            memoEditorForm.TextBoxRTFString = note.MemoRTFString;
            memoEditorForm.StartPosition = FormStartPosition.CenterParent;

            memoEditorForm.ShowDialog();

            note.MemoRTFString = memoEditorForm.TextBoxRTFString;
            note.DataCell.DC_memoRTF = memoEditorForm.TextBoxRTFString;
            note.DataCell.DC_memo = memoEditorForm.TextBoxString;

            Send_Log_Message("1>BulletinBoardForm::Edit_Note -> Note Changed :" + note.DataCell.DC_title);
            m_Controller.Perform_Modify_Memo_BulletinBoard(note.DataCell);
        }

        private void Delete_Note(Post_it note)
        {
            Send_Log_Message("1>BulletinBoardForm::Delete_Note :" + note.DataCell.DC_title);
            m_Controller.Perform_Delete_BulletinBoard(note.DataCell);

            if (panel_Bulletin.Controls.Count == 0)
            {
                New_Note();
            }
        }

        private void Change_Title(Post_it note)
        {
            note.DataCell.DC_title = note.MemoTitle;

            Send_Log_Message("1>BulletinBoardForm::Change_Title -> Title is Changed :" + note.DataCell.DC_title);
            m_Controller.Perform_Modify_Title_BulletinBoard(note.DataCell);
        }

        private void Change_Color(Post_it note)
        {
            note.DataCell.DC_memoColor = note.BackColor.Name;

            Send_Log_Message("1>BulletinBoardForm::Change_Color -> Pallet Color Changed :" + note.DataCell.DC_title);
            //m_Controller.Perform_Modify_BulletinBoard(note.DataCell);
        }

        private void Change_Tag(Post_it note)
        {
            note.DataCell.DC_memoTag = note.MemoTag;

            Send_Log_Message("1>BulletinBoardForm::Change_Tag -> Tag Color Changed :" + note.DataCell.DC_title);
            //m_Controller.Perform_Modify_BulletinBoard(note.DataCell);
        }

        private void Archive_Note(Post_it note)
        {
            note.DataCell.DC_archive = note.IsArchive = !note.IsArchive;

            Send_Log_Message("1>BulletinBoardForm::Archive_Note :" + note.DataCell.DC_title);
            m_Controller.Perform_Modify_Archive_BulletinBoard(note.DataCell);
        }

        private void List_Note()
        {
            foreach (Post_it note in panel_Bulletin.Controls)
            {
                note.Post_it_Click -= new Post_it_Event(Post_it_Click);
                panel_Bulletin.Controls.Remove(note);
                note.Dispose();
            }
            panel_Bulletin.Controls.Clear();

            Load_Data();

            Update_BulletinBoard();

            Send_Log_Message("1>BulletinBoardForm::List_Note : ");
        }

        private void List_Note(int tag)
        {
            foreach (Post_it note in panel_Bulletin.Controls)
            {
                note.Post_it_Click -= new Post_it_Event(Post_it_Click);
                panel_Bulletin.Controls.Remove(note);
                note.Dispose();
            }
            panel_Bulletin.Controls.Clear();

            IEnumerable<CDataCell> dataset = m_Controller.Query_BulletineBoard_Tag(tag);

            foreach (CDataCell dc in dataset)  // Post_it 생성 및 m_Post_it에 저장
            {
                Post_it note = new Post_it(dc);

                note.Size = new Size(NOTE_WIDTH, NOTE_HEIGHT);
                note.Post_it_Click -= new Post_it_Event(Post_it_Click);
                note.Post_it_Click += new Post_it_Event(Post_it_Click);

                note.MemoTitle = dc.DC_title;
                note.MemoRTFString = dc.DC_memoRTF;
                note.IsBulletin = dc.DC_bulletin;
                note.IsArchive = dc.DC_archive;
                note.MemoTag = dc.DC_memoTag;
                note.MemoColor = Color.FromName(dc.DC_memoColor);

                panel_Bulletin.Controls.Add(note);
                panel_Bulletin.Controls.SetChildIndex(note, 0);
            }

            Update_BulletinBoard();

            Send_Log_Message("1>BulletinBoardForm::List_Note -> TAG : " + tag);
        }

        private void List_Archive()
        {
            foreach (Post_it note in panel_Bulletin.Controls)
            {
                note.Post_it_Click -= new Post_it_Event(Post_it_Click);
                panel_Bulletin.Controls.Remove(note);
                note.Dispose();
            }
            panel_Bulletin.Controls.Clear();

            IEnumerable<CDataCell> dataset = m_Controller.Query_BulletineBoard_Archive();

            foreach (CDataCell dc in dataset)  // Post_it 생성 및 m_Post_it에 저장
            {
                Post_it note = new Post_it(dc);

                note.Size = new Size(NOTE_WIDTH, NOTE_HEIGHT);
                note.Post_it_Click -= new Post_it_Event(Post_it_Click);
                note.Post_it_Click += new Post_it_Event(Post_it_Click);

                note.MemoTitle = dc.DC_title;
                note.MemoRTFString = dc.DC_memoRTF;
                note.IsBulletin = dc.DC_bulletin;
                note.IsArchive = dc.DC_archive;
                note.MemoTag = dc.DC_memoTag;
                note.MemoColor = Color.FromName(dc.DC_memoColor);

                panel_Bulletin.Controls.Add(note);
                panel_Bulletin.Controls.SetChildIndex(note, 0);
            }

            Update_BulletinBoard();

            Send_Log_Message("1>BulletinBoardForm::List_Archive : ");
        }

        // ----------------------------------------------------------
        // 메뉴, 툴바
        // ----------------------------------------------------------
        private void label_Menu_Click(object sender, EventArgs e)
        {
            Console.WriteLine("BulletinBoardForm::label_Menu_Click");
        }

        private void button_Memo_Click(object sender, EventArgs e)
        {
            //Console.WriteLine("BulletinBoardForm::button_Memo_Click");
            label_Title.Text = "Bulleting Board - 메모";
            m_SelectedMenu = 1;
            List_Note();
        }

        private void button_Alarm_Click(object sender, EventArgs e)
        {
            //Console.WriteLine("BulletinBoardForm::button_Alarm_Click");
            label_Title.Text = "Bulleting Board - 알람";
            m_SelectedMenu = 2;
        }

        private void button_Archive_Click(object sender, EventArgs e)
        {
            label_Title.Text = "Bulleting Board - 보관처리";
            m_SelectedMenu = 3;
            List_Archive();
        }

        private void button_Label_Click(object sender, EventArgs e)
        {
            //Console.WriteLine("BulletinBoardForm::button_Label_Click");

            Button sd = (Button)sender;

            switch (sd.Name)
            {
                case "button_Label_Common":
                    m_SelectedMenu = 9;
                    label_Title.Text = "Bulleting Board - 라벨(해제)";
                    List_Note(0);
                    break;
                case "button_Label_Red":
                    label_Title.Text = "Bulleting Board - 라벨(빨강)";
                    m_SelectedMenu = 4;
                    List_Note(1);
                    break;
                case "button_Label_Orange":
                    label_Title.Text = "Bulleting Board - 라벨(주황)";
                    m_SelectedMenu = 5;
                    List_Note(2);
                    break;
                case "button_Label_Yellow":
                    label_Title.Text = "Bulleting Board - 라벨(노랑)";
                    m_SelectedMenu = 6;
                    List_Note(3);
                    break;
                case "button_Label_Green":
                    label_Title.Text = "Bulleting Board - 라벨(초록)";
                    m_SelectedMenu = 7;
                    List_Note(4);
                    break;
                case "button_Label_Blue":
                    label_Title.Text = "Bulleting Board - 라벨(파랑)";
                    m_SelectedMenu = 8;
                    List_Note(5);
                    break;
            }
        }
    }
}
