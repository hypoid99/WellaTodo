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

        static readonly string WINDOW_CAPTION = "BulletinBoard";
        static readonly int NOTE_WIDTH = 200;
        static readonly int NOTE_HEIGHT = 170;
        static readonly int NOTE_GAP = 10;

        public enum MemoMenuList
        {
            MEMO_MENU = 1,
            ALARM_MENU = 2,
            ARCHIVE_MENU = 3,
            TAG_RED_MENU = 4,
            TAG_ORANGE_MENU = 5,
            TAG_YELLOW_MENU = 6,
            TAG_GREEN_MENU = 7,
            TAG_BLUE_MENU = 8,
            TAG_COMMON_MENU = 9
        }

        public enum MemoTagList
        {
            TAG_COMMON = 0,
            TAG_RED = 1,
            TAG_ORANGE = 2,
            TAG_YELLOW = 3,
            TAG_GREEN = 4,
            TAG_BLUE = 5
        }

        MainController m_Controller;

        MemoEditorForm memoEditorForm = new MemoEditorForm();
        MemoMenuList m_Selected_Menu;
        MemoTagList m_Tag;

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
        }

        private void BulletinBoardForm_Resize(object sender, EventArgs e)
        {
            Update_Display();
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

            m_Selected_Menu = MemoMenuList.MEMO_MENU;
            label_Title.Text = "Bulleting Board - 메모";

            Update_Display();
        }

        private void Load_Data()
        {
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

        private void Update_Display()
        {
            Update_Notes_Position();
        }

        private void Update_Notes_Position()
        {
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

        private void Update_BulletinBoard()
        {
            switch (m_Selected_Menu)
            {
                case MemoMenuList.MEMO_MENU:  // 메모
                    Display_Memo_Menu();
                    break;
                case MemoMenuList.ALARM_MENU:  // 알람
                    break;
                case MemoMenuList.ARCHIVE_MENU:  // 보관처리
                    Display_Archive_Menu();
                    break;
                case MemoMenuList.TAG_RED_MENU:  // 빨강
                    Display_Tag_Menu(1);
                    break;
                case MemoMenuList.TAG_ORANGE_MENU:  // 주황
                    Display_Tag_Menu(2);
                    break;
                case MemoMenuList.TAG_YELLOW_MENU:  // 노랑
                    Display_Tag_Menu(3);
                    break;
                case MemoMenuList.TAG_GREEN_MENU:  // 초록
                    Display_Tag_Menu(4);
                    break;
                case MemoMenuList.TAG_BLUE_MENU:  // 파랑
                    Display_Tag_Menu(5);
                    break;
                case MemoMenuList.TAG_COMMON_MENU:  // 해제
                    Display_Tag_Menu(0);
                    break;
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
            CDataCell dc = e.Item;
            WParam param = e.Param;
            switch (param)
            {
                case WParam.WM_BULLETINBOARD_ADD:
                    Update_New_Note(dc);
                    break;
                case WParam.WM_BULLETINBOARD_DELETE:
                    Update_Delete_BulletinBoard(dc);
                    break;
                case WParam.WM_BULLETINBOARD_MODIFY_MEMO:
                    Update_Modify_Memo_BulletinBoard(dc);
                    break;
                case WParam.WM_BULLETINBOARD_MODIFY_TITLE:
                    Update_Modify_Title_BulletinBoard(dc);
                    break;
                case WParam.WM_BULLETINBOARD_MODIFY_ARCHIVE:
                    Update_Modify_Archive_BulletinBoard(dc);
                    break;
                case WParam.WM_BULLETINBOARD_MODIFY_COLOR:
                    Update_Modify_Color_BulletinBoard(dc);
                    break;
                case WParam.WM_BULLETINBOARD_MODIFY_TAG:
                    Update_Modify_Tag_BulletinBoard(dc);
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

        private void Update_Modify_Color_BulletinBoard(CDataCell dc)
        {
            int counter = 0;
            foreach (Post_it note in panel_Bulletin.Controls)
            {
                if (dc.DC_task_ID == note.DataCell.DC_task_ID)
                {
                    note.MemoColor = Color.FromName(dc.DC_memoColor);

                    counter++;
                    break;
                }
            }

            if (counter == 0)
            {
                Send_Log_Message("Warning>BulletinBoardForm::Update_Modify_Color_BulletinBoard -> No matching Data!!");
                return;
            }

            Update_BulletinBoard();
            Send_Log_Message("4>BulletinBoardForm::Update_Modify_Color_BulletinBoard : -> Completed" + dc.DC_title);
        }

        private void Update_Modify_Tag_BulletinBoard(CDataCell dc)
        {
            int counter = 0;
            foreach (Post_it note in panel_Bulletin.Controls)
            {
                if (dc.DC_task_ID == note.DataCell.DC_task_ID)
                {
                    note.MemoTag = dc.DC_memoTag;

                    counter++;
                    break;
                }
            }

            if (counter == 0)
            {
                Send_Log_Message("Warning>BulletinBoardForm::Update_Modify_Tag_BulletinBoard -> No matching Data!!");
                return;
            }

            Update_BulletinBoard();
            Send_Log_Message("4>BulletinBoardForm::Update_Modify_Tag_BulletinBoard : -> Completed" + dc.DC_title);
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
            Post_it note = (Post_it)sender;

            switch (e.CommandName)
            {
                case "New":
                    New_Note();
                    break;
                case "Edit":
                    Edit_Note(note);
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
            dc.DC_memoColor = "Yellow";

            switch (m_Selected_Menu)
            {
                case MemoMenuList.TAG_RED_MENU:  // 빨강
                    dc.DC_memoTag = (int)MemoTagList.TAG_RED;
                    break;
                case MemoMenuList.TAG_ORANGE_MENU:  // 주황
                    dc.DC_memoTag = (int)MemoTagList.TAG_ORANGE;
                    break;
                case MemoMenuList.TAG_YELLOW_MENU:  // 노랑
                    dc.DC_memoTag = (int)MemoTagList.TAG_YELLOW; ;
                    break;
                case MemoMenuList.TAG_GREEN_MENU:  // 초록
                    dc.DC_memoTag = (int)MemoTagList.TAG_GREEN; ;
                    break;
                case MemoMenuList.TAG_BLUE_MENU:  // 파랑
                    dc.DC_memoTag = (int)MemoTagList.TAG_BLUE; ;
                    break;
                case MemoMenuList.TAG_COMMON_MENU:  // 해제
                    dc.DC_memoTag = (int)MemoTagList.TAG_COMMON; ;
                    break;
            }

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
            DialogResult result = MessageBox.Show("메모를 삭제할까요?", WINDOW_CAPTION, MessageBoxButtons.YesNo);
            switch (result)
            {
                case DialogResult.No:
                    return;
                case DialogResult.Yes:
                    break;
            }

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
            m_Controller.Perform_Modify_Color_BulletinBoard(note.DataCell);
        }

        private void Change_Tag(Post_it note)
        {
            note.DataCell.DC_memoTag = note.MemoTag;

            Send_Log_Message("1>BulletinBoardForm::Change_Tag -> Tag Color Changed :" + note.DataCell.DC_title);
            m_Controller.Perform_Modify_Tag_BulletinBoard(note.DataCell);
        }

        private void Archive_Note(Post_it note)
        {
            if (note.MemoRTFString.Length == 0)
            {
                return;
            }

            note.DataCell.DC_archive = note.IsArchive = !note.IsArchive;

            Send_Log_Message("1>BulletinBoardForm::Archive_Note :" + note.DataCell.DC_title);
            m_Controller.Perform_Modify_Archive_BulletinBoard(note.DataCell);

            if (panel_Bulletin.Controls.Count == 0)
            {
                New_Note();
            }
        }

        private void Display_Memo_Menu()
        {
            foreach (Post_it note in panel_Bulletin.Controls)
            {
                note.Post_it_Click -= new Post_it_Event(Post_it_Click);
                panel_Bulletin.Controls.Remove(note);
                note.Dispose();
            }
            panel_Bulletin.Controls.Clear();

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

            Update_Notes_Position();
            Send_Log_Message("1>BulletinBoardForm::Display_Memo_Menu : ");
        }

        private void Display_Tag_Menu(int tag)
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

            Update_Notes_Position();
            Send_Log_Message("1>BulletinBoardForm::Display_Tag_Menu -> TAG : " + tag);
        }

        private void Display_Archive_Menu()
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

            Update_Notes_Position();
            Send_Log_Message("1>BulletinBoardForm::Display_Archive_Menu : ");
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
            label_Title.Text = "Bulleting Board - 메모";
            m_Selected_Menu = MemoMenuList.MEMO_MENU;
            Display_Memo_Menu();
        }

        private void button_Alarm_Click(object sender, EventArgs e)
        {
            label_Title.Text = "Bulleting Board - 알람";
            m_Selected_Menu = MemoMenuList.ALARM_MENU;
        }

        private void button_Archive_Click(object sender, EventArgs e)
        {
            label_Title.Text = "Bulleting Board - 보관처리";
            m_Selected_Menu = MemoMenuList.ARCHIVE_MENU;
            Display_Archive_Menu();
        }

        private void button_Label_Click(object sender, EventArgs e)
        {
            Button sd = (Button)sender;

            switch (sd.Name)
            {
                case "button_Label_Red":
                    label_Title.Text = "Bulleting Board - 라벨(빨강)";
                    m_Selected_Menu = MemoMenuList.TAG_RED_MENU;
                    Display_Tag_Menu(1);
                    break;
                case "button_Label_Orange":
                    label_Title.Text = "Bulleting Board - 라벨(주황)";
                    m_Selected_Menu = MemoMenuList.TAG_ORANGE_MENU;
                    Display_Tag_Menu(2);
                    break;
                case "button_Label_Yellow":
                    label_Title.Text = "Bulleting Board - 라벨(노랑)";
                    m_Selected_Menu = MemoMenuList.TAG_YELLOW_MENU;
                    Display_Tag_Menu(3);
                    break;
                case "button_Label_Green":
                    label_Title.Text = "Bulleting Board - 라벨(초록)";
                    m_Selected_Menu = MemoMenuList.TAG_GREEN_MENU;
                    Display_Tag_Menu(4);
                    break;
                case "button_Label_Blue":
                    label_Title.Text = "Bulleting Board - 라벨(파랑)";
                    m_Selected_Menu = MemoMenuList.TAG_BLUE_MENU;
                    Display_Tag_Menu(5);
                    break;
                case "button_Label_Common":
                    m_Selected_Menu = MemoMenuList.TAG_COMMON_MENU;
                    label_Title.Text = "Bulleting Board - 라벨(해제)";
                    Display_Tag_Menu(0);
                    break;
            }
        }
    }
}
