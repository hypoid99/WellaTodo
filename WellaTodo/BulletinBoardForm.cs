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
        static readonly int NOTE_HEIGHT = 225;
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

        MemoForm memoForm = new MemoForm();
        MemoEditorForm memoEditorForm = new MemoEditorForm();
        MemoMenuList m_Selected_Menu;
        Post_it m_Selected_Memo;

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

        private void BulletinBoardForm_Paint(object sender, PaintEventArgs e)
        {
            Console.WriteLine("BulletinBoardForm_Paint");
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

            foreach (CDataCell dc in dataset)
            {
                Post_it note = new Post_it(dc);

                note.Size = new Size(NOTE_WIDTH, NOTE_HEIGHT);
                note.Post_it_Click -= new Post_it_Event(Post_it_Click);
                note.Post_it_Click += new Post_it_Event(Post_it_Click);
                note.DragEnter -= new DragEventHandler(Post_it_DragEnter);
                note.DragEnter += new DragEventHandler(Post_it_DragEnter);
                note.DragDrop -= new DragEventHandler(Post_it_DragDrop);
                note.DragDrop += new DragEventHandler(Post_it_DragDrop);

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

        private void Update_BulletinBoard_Menu()
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
                case WParam.WM_MEMO_ADD:
                    Update_New_Note(dc);
                    break;
                case WParam.WM_MODIFY_TASK_TITLE:
                    Update_Modify_Memo_Title(dc);
                    break;
                case WParam.WM_MODIFY_TASK_MEMO:
                    Update_Modify_Memo(dc);
                    break;
                case WParam.WM_TASK_DELETE:
                    Update_Delete_Memo(dc);
                    break;
                case WParam.WM_MODIFY_REMIND:
                    Update_Modify_Memo_Alarm(dc);
                    break;
                case WParam.WM_MODIFY_PLANNED:
                    Update_Modify_Memo_Schedule(dc);
                    break;
                case WParam.WM_COMPLETE_PROCESS:
                    Update_Modify_Memo_Archive(dc);
                    break;
                case WParam.WM_MODIFY_MEMO_COLOR:
                    Update_Modify_Memo_Color(dc);
                    break;
                case WParam.WM_MODIFY_MEMO_TAG:
                    Update_Modify_Memo_Tag(dc);
                    break;
                case WParam.WM_MEMO_MOVE_TO:
                    Update_Memo_Move_To(dc);
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
            note.DragEnter -= new DragEventHandler(Post_it_DragEnter);
            note.DragEnter += new DragEventHandler(Post_it_DragEnter);
            note.DragDrop -= new DragEventHandler(Post_it_DragDrop);
            note.DragDrop += new DragEventHandler(Post_it_DragDrop);

            panel_Bulletin.Controls.Add(note);

            Update_Notes_Position();

            Send_Log_Message("4>BulletinBoard::Update_New_Note -> Add Note : " + dc.DC_title);
        }

        private void Update_Modify_Memo_Alarm(CDataCell dc)
        {
            int counter = 0;
            foreach (Post_it note in panel_Bulletin.Controls)
            {
                if (dc.DC_task_ID == note.DataCell.DC_task_ID)
                {
                    note.DataCell.DC_remindType = dc.DC_remindType;
                    note.DataCell.DC_remindTime = dc.DC_remindTime;

                    if (dc.DC_remindType > 0)
                    {
                        note.IsAlarmVisible = true;
                    }
                    else
                    {
                        note.IsAlarmVisible = false;
                    }
                    counter++;
                    break;
                }
            }

            if (counter == 0)
            {
                Send_Log_Message("Warning>BulletinBoardForm::Update_Modify_Memo_Alarm -> No matching Data!!");
                return;
            }

            Send_Log_Message("4>BulletinBoardForm::Update_Modify_Memo_Alarm : -> Completed" + dc.DC_title);
        }

        private void Update_Modify_Memo_Schedule(CDataCell dc)
        {
            int counter = 0;
            foreach (Post_it note in panel_Bulletin.Controls)
            {
                if (dc.DC_task_ID == note.DataCell.DC_task_ID)
                {
                    note.DataCell.DC_deadlineType = dc.DC_deadlineType;
                    note.DataCell.DC_deadlineTime = dc.DC_deadlineTime;

                    if (dc.DC_deadlineType > 0)
                    {
                        note.IsScheduleVisible = true;
                    }
                    else
                    {
                        note.IsScheduleVisible = false;
                    }
                    counter++;
                    break;
                }
            }

            if (counter == 0)
            {
                Send_Log_Message("Warning>BulletinBoardForm::Update_Modify_Memo_Schedule -> No matching Data!!");
                return;
            }

            Send_Log_Message("4>BulletinBoardForm::Update_Modify_Memo_Schedule : -> Completed" + dc.DC_title);
        }

        private void Update_Modify_Memo_Title(CDataCell dc)
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
                Send_Log_Message("Warning>BulletinBoardForm::Update_Modify_Memo_Title -> No matching Data!!");
                return;
            }

            Send_Log_Message("4>BulletinBoardForm::Update_Modify_Title_BulletinBoard : -> Completed" + dc.DC_title);
        }

        private void Update_Modify_Memo_Archive(CDataCell dc)
        {
            if (m_Selected_Menu == MemoMenuList.MEMO_MENU && dc.DC_archive == false) // 출력
            {
                Display_Memo_Menu();
                Send_Log_Message("4>BulletinBoardForm::Update_Modify_Memo_Archive : -> Display_Memo_Menu" + dc.DC_title);
                return;
            }

            if (m_Selected_Menu == MemoMenuList.ARCHIVE_MENU && dc.DC_archive == true) // 출력
            {
                Display_Archive_Menu();
                Send_Log_Message("4>BulletinBoardForm::Update_Modify_Memo_Archive : -> Display_Archive_Menu" + dc.DC_title);
                return;
            }

            int counter = 0;
            foreach (Post_it note in panel_Bulletin.Controls)
            {
                if (dc.DC_task_ID == note.DataCell.DC_task_ID)
                {
                    note.IsArchive = dc.DC_archive;

                    note.Post_it_Click -= new Post_it_Event(Post_it_Click);
                    note.DragEnter -= new DragEventHandler(Post_it_DragEnter);
                    note.DragDrop -= new DragEventHandler(Post_it_DragDrop);

                    panel_Bulletin.Controls.Remove(note);
                    note.Dispose();

                    Update_Notes_Position();

                    counter++;
                    break;
                }
            }

            if (counter == 0)
            {
                Send_Log_Message("Warning>BulletinBoardForm::Update_Modify_Memo_Archive -> No matching Data!!");
                return;
            }

            Send_Log_Message("4>BulletinBoardForm::Update_Modify_Memo_Archive : -> Completed" + dc.DC_title);
        }

        private void Update_Modify_Memo(CDataCell dc)
        {
            int counter = 0;
            foreach (Post_it note in panel_Bulletin.Controls)
            {
                if (dc.DC_task_ID == note.DataCell.DC_task_ID)
                {
                    note.MemoText = dc.DC_memo;

                    counter++;
                    break;
                }
            }

            if (counter == 0)
            {
                Send_Log_Message("Warning>BulletinBoardForm::Update_Modify_Memo -> No matching Data!!");
            }

            Send_Log_Message("4>BulletinBoardForm::Update_Modify_Memo : -> Completed" + dc.DC_title);
        }

        private void Update_Modify_Memo_Color(CDataCell dc)
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

            Send_Log_Message("4>BulletinBoardForm::Update_Modify_Color_BulletinBoard : -> Completed" + dc.DC_title);
        }

        private void Update_Modify_Memo_Tag(CDataCell dc)
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

            Send_Log_Message("4>BulletinBoardForm::Update_Modify_Tag_BulletinBoard : -> Completed" + dc.DC_title);
        }

        private void Update_Delete_Memo(CDataCell dc)
        {
            int counter = 0;
            foreach (Post_it note in panel_Bulletin.Controls)
            {
                if (dc.DC_task_ID == note.DataCell.DC_task_ID)
                {
                    note.Post_it_Click -= new Post_it_Event(Post_it_Click);
                    note.DragEnter -= new DragEventHandler(Post_it_DragEnter);
                    note.DragDrop -= new DragEventHandler(Post_it_DragDrop);

                    panel_Bulletin.Controls.Remove(note);
                    note.Dispose();

                    counter++;
                    break;
                }
            }

            if (counter == 0)
            {
                Send_Log_Message("Warning>BulletinBoardForm::Update_Delete_Memo -> No matching Data!!");
            }

            Update_Notes_Position();

            Send_Log_Message("4>BulletinBoardForm::Update_Delete_Memo : -> Completed" + dc.DC_title);
        }

        private void Update_Memo_Move_To(CDataCell dc)
        {
            int counter = 0;
            foreach (Post_it note in panel_Bulletin.Controls)
            {
                if (dc.DC_task_ID == note.DataCell.DC_task_ID)
                {
                    panel_Bulletin.Controls.SetChildIndex(m_Selected_Memo, counter);
                    counter++;
                    break;
                }
            }

            if (counter == 0)
            {
                Send_Log_Message("Warning>BulletinBoardForm::Update_Memo_Move_To -> No matching Data!!");
            }

            //Update_Notes_Position();
            Update_BulletinBoard_Menu();

            Send_Log_Message("4>BulletinBoardForm::Update_Memo_Move_To -> Source : " + m_Selected_Memo.DataCell.DC_title + " Target : " + dc.DC_title);
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
                case "AlarmSet":
                    Set_Alarm_Note(note);
                    break;
                case "AlarmReset":
                    Reset_Alarm_Note(note);
                    break;
                case "ScheduleSet":
                    Set_Schedule_Note(note);
                    break;
                case "ScheduleReset":
                    Reset_Schedule_Note(note);
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
                    Change_Memo(note);
                    break;
            }
        }

        private void New_Note()
        {
            CDataCell dc = new CDataCell();

            dc.DC_listName = "작업";

            dc.DC_title = "메모";
            dc.DC_memo = "";
            dc.DC_bulletin = true;
            dc.DC_archive = false;
            dc.DC_memoTag = 0;
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
            m_Controller.Perform_Add_Memo(dc);
        }

        private void Edit_Note(Post_it note)
        {
            memoForm.StartPosition = FormStartPosition.CenterParent;

            memoForm.TextBoxString = note.MemoText;
            memoForm.Text = note.DataCell.DC_title;

            memoForm.ShowDialog();

            if (!memoForm.IsTextBoxChanged)
            {
                Send_Log_Message("1>BulletinBoardForm::Edit_Note -> Memo Not Changed :");
                return;
            }

            note.MemoText = memoForm.TextBoxString;

            //메모 내용에 변경이 있는지 확인(?)
            note.DataCell.DC_memo = note.MemoText;  // 입력 사항에 오류가 있는지 체크할 것

            Send_Log_Message("1>BulletinBoardForm::Edit_Note -> Memo Changed :" + note.DataCell.DC_title);
            m_Controller.Perform_Modify_Task_Memo(note.DataCell);
        }

        private void Change_Memo(Post_it note)
        {
            note.DataCell.DC_memo = note.MemoText;

            Send_Log_Message("1>BulletinBoardForm::Change_Memo -> Memo Changed :" + note.DataCell.DC_title);
            m_Controller.Perform_Modify_Task_Memo(note.DataCell);
        }

        private void Set_Alarm_Note(Post_it note)
        {
            DateTimePickerForm calendar = new DateTimePickerForm();
            calendar.ShowDialog();

            DateTime dt = calendar.SelectedDateTime;

            if (!calendar.IsSelected || calendar.SelectedDateTime == default)
            {
                Send_Log_Message("1>BulletinBoardForm::Set_Alarm_Note -> Canceled!!!");
                return;
            }
            calendar.IsSelected = false;

            if (dt.Hour == 0 && dt.Minute == 0 && dt.Second == 0) // 시간을 입력하지 않을때
            {
                dt = new DateTime(dt.Year, dt.Month, dt.Day, 22, 00, 00);
            }

            note.DataCell.DC_remindType = 4;
            note.DataCell.DC_remindTime = dt;

            note.IsAlarmVisible = true;

            Send_Log_Message("1>BulletinBoardForm::Set_Alarm_Note");
            m_Controller.Perform_Modify_Remind(note.DataCell);
        }

        private void Reset_Alarm_Note(Post_it note)
        {
            note.DataCell.DC_remindType = 0;
            note.DataCell.DC_remindTime = default;

            note.IsAlarmVisible = false;

            Send_Log_Message("1>BulletinBoardForm::Reset_Alarm_Note");
            m_Controller.Perform_Modify_Remind(note.DataCell);
        }

        private void Set_Schedule_Note(Post_it note)
        {
            DateTimePickerForm calendar = new DateTimePickerForm();
            calendar.ShowDialog();

            DateTime dt = calendar.SelectedDateTime;

            if (!calendar.IsSelected || calendar.SelectedDateTime == default)
            {
                Send_Log_Message("1>BulletinBoardForm::Set_Schedule_Note -> Canceled!!!");
                return;
            }
            calendar.IsSelected = false;

            if (dt.Hour == 0 && dt.Minute == 0 && dt.Second == 0) // 시간을 입력하지 않을때
            {
                dt = new DateTime(dt.Year, dt.Month, dt.Day, 22, 00, 00);
            }

            note.DataCell.DC_deadlineType = 4;
            note.DataCell.DC_deadlineTime = dt;

            note.IsScheduleVisible = true;

            Send_Log_Message("1>BulletinBoardForm::Set_Schedule_Note");
            m_Controller.Perform_Modify_Planned(note.DataCell);
        }

        private void Reset_Schedule_Note(Post_it note)
        {
            note.DataCell.DC_deadlineType = 0;
            note.DataCell.DC_deadlineTime = default;

            note.IsScheduleVisible = false;

            Send_Log_Message("1>BulletinBoardForm::Reset_Schedule_Note");
            m_Controller.Perform_Modify_Planned(note.DataCell);
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

            Send_Log_Message("1>BulletinBoardForm::Delete_Note : " + note.DataCell.DC_title);
            m_Controller.Perform_Delete_Task(note.DataCell);

            if (panel_Bulletin.Controls.Count == 0) // BulletinBoard에 메모가 없을시 한개 추가
            {
                New_Note();
            }
        }

        private void Change_Title(Post_it note)
        {
            note.DataCell.DC_title = note.MemoTitle;

            Send_Log_Message("1>BulletinBoardForm::Change_Title -> Title is Changed : " + note.DataCell.DC_title);
            m_Controller.Perform_Modify_Task_Title(note.DataCell);
        }

        private void Change_Color(Post_it note)
        {
            if (note.IsMemoTextChanged)
            {
                Change_Memo(note);
            }

            note.DataCell.DC_memoColor = note.BackColor.Name;

            Send_Log_Message("1>BulletinBoardForm::Change_Color -> Pallet Color Changed : " + note.DataCell.DC_title);
            m_Controller.Perform_Modify_Memo_Color(note.DataCell);
        }

        private void Change_Tag(Post_it note)
        {
            if (note.IsMemoTextChanged)
            {
                Change_Memo(note);
            }

            note.DataCell.DC_memoTag = note.MemoTag;

            Send_Log_Message("1>BulletinBoardForm::Change_Tag -> Tag Color Changed :" + note.DataCell.DC_title);
            m_Controller.Perform_Modify_Memo_Tag(note.DataCell);
        }

        private void Archive_Note(Post_it note)
        {
            if (note.IsMemoTextChanged)
            {
                Change_Memo(note);
            }

            if (note.IsArchive == false && note.Memo_TextLength == 0) // 보관처리시 메모에 내용이 없으면 보관처리하지 말고 리턴
            {
                MessageBox.Show("메모 내용이 없어 보관처리되지 않읍니다.");
                Send_Log_Message("1>BulletinBoardForm::Archive_Note -> Can't Archive for Empty");
                return;
            }

            note.DataCell.DC_archive = note.IsArchive = !note.IsArchive;

            Send_Log_Message("1>BulletinBoardForm::Archive_Note :" + note.DataCell.DC_title);
            m_Controller.Perform_Modify_Memo_Archive(note.DataCell);

            if (m_Selected_Menu == MemoMenuList.MEMO_MENU) // 보관처리시 판넬이 비면 새로운 메모 생성
            {
                if (note.IsArchive && panel_Bulletin.Controls.Count == 0) 
                {
                    Send_Log_Message("1>BulletinBoardForm::Archive_Note -> Create New, Panel is Empty!!");
                    New_Note();
                }
            }
        }

        // -----------------------------------------------------------------
        // 메뉴 Display 처리 (메모/알람/보관처리/태그)
        // -----------------------------------------------------------------
        private void Display_Memo_Menu()
        {
            Add_Memo_To_BulletinBoard(m_Controller.Query_BulletineBoard());

            Send_Log_Message(">BulletinBoardForm::Display_Memo_Menu : ");
        }

        private void Display_Alarm_Menu()
        {
            //Add_Alarm_To_BulletinBoard(m_Controller.Query_BulletineBoard_Alarm());

            Send_Log_Message(">BulletinBoardForm::Display_Alarm_Menu : ");
        }

        private void Display_Tag_Menu(int tag)
        {
            Add_Memo_To_BulletinBoard(m_Controller.Query_BulletineBoard_Tag(tag));
      
            Send_Log_Message(">BulletinBoardForm::Display_Tag_Menu -> TAG : " + tag);
        }

        private void Display_Archive_Menu()
        {
            Add_Memo_To_BulletinBoard(m_Controller.Query_BulletineBoard_Archive());

            Send_Log_Message(">BulletinBoardForm::Display_Archive_Menu : ");
        }

        private void Add_Memo_To_BulletinBoard(IEnumerable<CDataCell> dataset)
        {
            // 1. 기존 항목 안보이게 설정
            foreach (Post_it note in panel_Bulletin.Controls)
            {
                note.Visible = false;
            }

            // 2. 기존 항목 제거 및 클리어
            foreach (Post_it note in panel_Bulletin.Controls)
            {
                note.Post_it_Click -= new Post_it_Event(Post_it_Click);
                note.DragEnter -= new DragEventHandler(Post_it_DragEnter);
                note.DragDrop -= new DragEventHandler(Post_it_DragDrop);

                panel_Bulletin.Controls.Remove(note);
                note.Dispose();
            }
            panel_Bulletin.Controls.Clear();

            // 3. 신규 항목 안보이게 생성
            foreach (CDataCell dc in dataset)
            {
                Post_it note = new Post_it(dc);

                note.Visible = false;

                note.Size = new Size(NOTE_WIDTH, NOTE_HEIGHT);
                note.Post_it_Click -= new Post_it_Event(Post_it_Click);
                note.Post_it_Click += new Post_it_Event(Post_it_Click); 
                note.DragEnter -= new DragEventHandler(Post_it_DragEnter);
                note.DragEnter += new DragEventHandler(Post_it_DragEnter);
                note.DragDrop -= new DragEventHandler(Post_it_DragDrop);
                note.DragDrop += new DragEventHandler(Post_it_DragDrop);

                panel_Bulletin.Controls.Add(note);
                panel_Bulletin.Controls.SetChildIndex(note, 0);

                note.IsMemoTextChanged = false;
            }

            // 4. 신규 항목 위치 선정
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

            // 5. 신규 항목 보이게 설정
            foreach (Post_it note in panel_Bulletin.Controls)
            {
                note.Visible = true;
                if (note.DataCell.DC_remindType > 0) note.IsAlarmVisible = true;
                if (note.DataCell.DC_deadlineType > 0) note.IsScheduleVisible = true;
            }
        }

        // ----------------------------------------------------------
        // 메뉴, 툴바 이벤트
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

        //--------------------------------------------------------------
        // 드래그 앤 드롭 - Target
        //--------------------------------------------------------------
        private void Post_it_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(Post_it)))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void Post_it_DragDrop(object sender, DragEventArgs e)
        {
            Post_it data = null;
            if (e.Data.GetDataPresent(typeof(Post_it)))
            {
                data = e.Data.GetData(typeof(Post_it)) as Post_it;
            }

            Point p = panel_Bulletin.PointToClient(new Point(e.X, e.Y));
            Post_it note = (Post_it)panel_Bulletin.GetChildAtPoint(p);
            
            if (data.DataCell.DC_task_ID == note.DataCell.DC_task_ID)
            {
                Console.WriteLine("TodoItem_DragDrop -> Same memo can't move");
                return;
            }
            
            if (note.IsArchive)
            {
                Console.WriteLine("Post_it_DragDrop -> Can't move over Archive Memo");
                return;
            }

            m_Selected_Memo = data;

            Send_Log_Message("1>BulletinBoardForm::Post_it_DragDrop");
            m_Controller.Perform_Memo_Move_To(data.DataCell, note.DataCell);
            
        }
    }
}
