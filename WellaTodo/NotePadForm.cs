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
    public partial class NotePadForm : Form, IView, IModelObserver
    {
        public event ViewHandler<IView> View_Changed_Event;

        static readonly int LIST_WIDTH_GAP = 25;
        static readonly int HEADER_HEIGHT = 50;
        static readonly int TAIL_HEIGHT = 50;

        static readonly Image ICON_LIST = Properties.Resources.outline_list_black_24dp;

        static readonly Color BACK_COLOR = Color.White;
        static readonly Color HIGHLIGHT_COLOR = Color.LightCyan;
        static readonly Color SELECTED_COLOR = Color.Cyan;
        static readonly Color TEXTBOX_BACK_COLOR = Color.LightCyan;

        static readonly string FONT_NAME = "맑은 고딕";
        static readonly float FONT_SIZE_TEXT = 14.0f;

        MainController m_Controller;
        ToolTip m_TaskToolTip = new ToolTip();

        NoteFileList m_Pre_Selected_List;
        NoteFileList m_Selected_List;

        bool isActivated = false;
        bool isTextbox_New_Note_Clicked = false;

        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
        CDataCell m_DataCell;
        public CDataCell DataCell { get => m_DataCell; set => m_DataCell = value; }

        // --------------------------------------------------------------------
        // Constructor
        // --------------------------------------------------------------------
        public NotePadForm()
        {
            InitializeComponent();
        }

        public void SetController(MainController controller)
        {
            m_Controller = controller;
        }

        // --------------------------------------------------------------------
        // Form 이벤트
        // --------------------------------------------------------------------
        private void NotePadForm_Load(object sender, EventArgs e)
        {
            Initiate();
        }

        private void NotePadForm_Resize(object sender, EventArgs e)
        {
            Update_Display();
        }

        private void NotePadForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void NotePadForm_Activated(object sender, EventArgs e)
        {
            isActivated = true;
            Update_Display();
        }

        private void NotePadForm_Deactivate(object sender, EventArgs e)
        {
            isActivated = false;
        }

        //--------------------------------------------------------------
        // 초기화 및 데이터 로딩, Update Display
        //--------------------------------------------------------------
        private void Initiate()
        {
            flowLayoutPanel_List.AutoScroll = false;
            flowLayoutPanel_List.HorizontalScroll.Maximum = 0;
            flowLayoutPanel_List.HorizontalScroll.Enabled = false;
            flowLayoutPanel_List.HorizontalScroll.Visible = false;
            flowLayoutPanel_List.AutoScroll = true;

            //flowLayoutPanel_List.MouseWheel += new MouseEventHandler(flowLayoutPanel_List_MouseWheel);

            flowLayoutPanel_List.BackColor = BACK_COLOR;
            flowLayoutPanel_List.Margin = new Padding(0);
            flowLayoutPanel_List.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel_List.WrapContents = false;

            textBox_New_Note.Enter += TextBox_New_Note_Enter;
            textBox_New_Note.Leave += TextBox_New_Note_Leave;
            textBox_New_Note.KeyDown += TextBox_New_Note_KeyDown;
            textBox_New_Note.KeyUp += TextBox_New_Note_KeyUp;
            textBox_New_Note.MouseDown += TextBox_New_Note_MouseDown;
            textBox_New_Note.Font = new Font(FONT_NAME, FONT_SIZE_TEXT);
            textBox_New_Note.BackColor = TEXTBOX_BACK_COLOR;
            textBox_New_Note.Text = "+ 새 노트 추가";

            foreach (CDataCell data in m_Controller.Query_NoteFile()) // 노트 파일 리스트를 등록한다
            {
                NoteFileList list = CreateNoteFileList(data);
                m_TaskToolTip.SetToolTip(list, ConvertRichTextToString(list.DataCell.DC_RTF)) ;
                flowLayoutPanel_List.Controls.Add(list);
            }

            Update_Display();
        }

        private void Update_Display()
        {
            Update_List_Width();
        }

        //--------------------------------------------------------------
        // 메서드
        //--------------------------------------------------------------
        private void Update_List_Width()
        {
            panel_Header.Location = new Point(0, 0);
            panel_Header.Size = new Size(Width, HEADER_HEIGHT);

            flowLayoutPanel_List.Location = new Point(0, HEADER_HEIGHT);
            flowLayoutPanel_List.Size = new Size(Width - LIST_WIDTH_GAP, Height - HEADER_HEIGHT - TAIL_HEIGHT);

            panel_Footer.Location = new Point(0, HEADER_HEIGHT + flowLayoutPanel_List.Height);
            panel_Footer.Size = new Size(Width, TAIL_HEIGHT);

            label_Add_Note.Location = new Point(Width - 60, 9);

            textBox_New_Note.Location = new Point(10, 8);
            textBox_New_Note.Size = new Size(Width - LIST_WIDTH_GAP - 10, 25);

            foreach (NoteFileList item in flowLayoutPanel_List.Controls)
            {
                item.Width = flowLayoutPanel_List.Width - LIST_WIDTH_GAP;
            }
        }

        private NoteFileList CreateNoteFileList(CDataCell dc)
        {
            NoteFileList list = new NoteFileList(dc);

            list.NoteFileList_ClickEvent += List_MouseClick;
            list.FileName = dc.DC_title;
            list.Image = new Bitmap(ICON_LIST);

            return list;
        }

        private string ConvertRichTextToString(string rtf)
        {
            RichTextBox rtBox = new RichTextBox();
            rtBox.Rtf = rtf;
            return rtBox.Text;
        }

        //--------------------------------------------------------------
        // Model 이벤트
        //--------------------------------------------------------------
        public void Update_View(IModel m, ModelEventArgs e)
        {
            CDataCell dc = e.Item;
            WParam param = e.Param;

            switch (param)
            {
                case WParam.WM_NOTE_ADD:
                    Update_Add_Note(dc);
                    break;
                case WParam.WM_NOTE_DELETE:
                    Update_Delete_Note(dc);
                    break;
                case WParam.WM_MODIFY_NOTE:
                    Update_Modify_Note(dc);
                    break;
                case WParam.WM_CONVERT_NOTEPAD:

                    break;
                case WParam.WM_TRANSFER_RTF_NOTEPAD:

                    break;
                case WParam.WM_SAVE_RTF_NOTEPAD:

                    break;
                default:
                    break;
            }
        }

        private void Update_Add_Note(CDataCell dc)
        {
            NoteFileList list = CreateNoteFileList(dc);
            flowLayoutPanel_List.Controls.Add(list); // 판넬 컨렉션에 저장
            flowLayoutPanel_List.Controls.SetChildIndex(list, 0);

            m_TaskToolTip.SetToolTip(list, ConvertRichTextToString(list.DataCell.DC_RTF));

            Update_List_Width();

            Send_Log_Message("4>NotePadForm::Update_Add_Note -> Add Note : " + dc.DC_title);
        }

        private void Update_Delete_Note(CDataCell dc)
        {
            NoteFileList list = null;
            foreach (NoteFileList item in flowLayoutPanel_List.Controls)  // dc로 찾기
            {
                if (dc.DC_task_ID == item.DataCell.DC_task_ID)
                {
                    list = item;
                    break;
                }
            }

            list.NoteFileList_ClickEvent -= List_MouseClick;

            flowLayoutPanel_List.Controls.Remove(list);
            list.Dispose();

            Send_Log_Message("4>NotePadForm::Update_Delete_Note -> Delete Completed!");
        }

        private void Update_Modify_Note(CDataCell dc)
        {
            m_TaskToolTip.SetToolTip(m_Selected_List, ConvertRichTextToString(m_Selected_List.DataCell.DC_RTF));

            Send_Log_Message("4>NotePadForm::Update_Modify_Note -> Modify Note : " + dc.DC_title);
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
        // 노트패드 Command 처리
        // ----------------------------------------------------------
        private void New_Note()
        {
            CDataCell dc = new CDataCell();

            dc.DC_title = "새 노트를 작성하세요";
            dc.DC_notepad = true;

            Send_Log_Message("1>NotePadForm::New_Note -> Add Note : " + dc.DC_title);
            m_Controller.Perform_Add_Note(dc);
        }

        private void Add_Note(string title)
        {
            CDataCell dc = new CDataCell();

            dc.DC_title = title;
            dc.DC_notepad = true;

            Send_Log_Message("1>NotePadForm::Add_Note -> Add Note : " + dc.DC_title);
            m_Controller.Perform_Add_Note(dc);
        }

        private void Open_Note(NoteFileList list)
        {
            Send_Log_Message("1-1>NotePadForm::Open_Note -> Open NotePad Editor");

            NotePadEditorForm editorForm = new NotePadEditorForm();

            editorForm.StartPosition = FormStartPosition.CenterParent;
  
            editorForm.Note_RTF = list.DataCell.DC_RTF;
            string temp_RTF = list.DataCell.DC_RTF;
            editorForm.IsUnsaved = false;

            DialogResult result = editorForm.ShowDialog();

            if (result != DialogResult.Yes)
            {
                list.DataCell.DC_RTF = temp_RTF;

                Send_Log_Message("1-2>NotePadForm::Open_Note -> NotePad is not saved");
                return;
            }

            list.DataCell.DC_RTF = editorForm.Note_RTF;

            Send_Log_Message("1-2>NotePadForm::Open_Note -> NotePad is modified");
            m_Controller.Perform_Modify_Note(list.DataCell);
        }

        private void Delete_Note(NoteFileList list)
        {
            Send_Log_Message("1>NotePadForm::Delete_Note : " + list.DataCell.DC_title);
            m_Controller.Perform_Delete_Note(list.DataCell);
        }

        // ----------------------------------------------------------
        // 사용자 입력 처리
        // ----------------------------------------------------------
        private void List_MouseClick(object sender, UserCommandEventArgs e)
        {
            NoteFileList sd = (NoteFileList)sender;
            Console.WriteLine("List_MouseClick : "+sd.DataCell.DC_title);

            if (m_Pre_Selected_List == null) m_Pre_Selected_List = sd;

            if (!m_Pre_Selected_List.Equals(sd))
            {
                m_Pre_Selected_List.IsSelected = false;
            }

            m_Selected_List = sd;
            m_Selected_List.IsSelected = true;
            m_Pre_Selected_List = m_Selected_List;

            // 클릭시 데이타 내용 확인하기
            m_Controller.Verify_DataCell(sd.DataCell);

            switch (e.CommandName)
            {
                case "Click":
                    Console.WriteLine("Click : " + sd.DataCell.DC_title);
                    break;
                case "DoubleClick":
                    Console.WriteLine("DoubleClick : " + sd.DataCell.DC_title);
                    Open_Note(sd);
                    break;
                case "ContextMenu":
                    Console.WriteLine("ContextMenu : " + sd.DataCell.DC_title);
                    Execute_ContextMenu();
                    break;
                case "Middle":
                    Console.WriteLine("Middle : " + sd.DataCell.DC_title);
                    break;
            }
        }

        private void label_Add_Note_Click(object sender, EventArgs e)
        {
            New_Note();
        }

        private void Execute_ContextMenu()
        {
            ContextMenu contextMenu = new ContextMenu();
            contextMenu.Popup += new EventHandler(OnMenuPopupEvent);

            MenuItem openList = new MenuItem("목록 열기", new EventHandler(OnOpenMenu_Click));
            MenuItem duplicateList = new MenuItem("목록 복제", new EventHandler(OnDuplicateMenu_Click));
            MenuItem renameList = new MenuItem("목록 이름바꾸기", new EventHandler(OnRenameMenu_Click));
            MenuItem deleteList = new MenuItem("목록 삭제", new EventHandler(OnDeleteMenu_Click));
            MenuItem moveListUp = new MenuItem("목록 위로 이동", new EventHandler(OnMoveUpMenu_Click));
            MenuItem moveListDown = new MenuItem("목록 아래로 이동", new EventHandler(OnMoveDownMenu_Click));

            contextMenu.MenuItems.Add(openList);
            contextMenu.MenuItems.Add(duplicateList);
            contextMenu.MenuItems.Add(renameList);
            contextMenu.MenuItems.Add(deleteList);
            contextMenu.MenuItems.Add("-");
            contextMenu.MenuItems.Add(moveListUp);
            contextMenu.MenuItems.Add(moveListDown);

            Point cursor = PointToClient(Cursor.Position);
            contextMenu.Show(this, cursor);
        }

        private void OnMenuPopupEvent(object sender, EventArgs e)
        {

        }

        private void OnOpenMenu_Click(object sender, EventArgs e)
        {
            Open_Note(m_Selected_List);
        }

        private void OnDuplicateMenu_Click(object sender, EventArgs e)
        {

        }

        private void OnRenameMenu_Click(object sender, EventArgs e)
        {

        }

        private void OnDeleteMenu_Click(object sender, EventArgs e)
        {
            Delete_Note(m_Selected_List);
        }

        private void OnMoveUpMenu_Click(object sender, EventArgs e)
        {

        }

        private void OnMoveDownMenu_Click(object sender, EventArgs e)
        {

        }

        // -----------------------------------------------------------
        // 노트 생성 입력 처리
        // -----------------------------------------------------------
        private void TextBox_New_Note_Enter(object sender, EventArgs e)
        {
            if (!isTextbox_New_Note_Clicked) textBox_New_Note.Text = "";
            isTextbox_New_Note_Clicked = true;
        }

        private void TextBox_New_Note_Leave(object sender, EventArgs e)
        {
            textBox_New_Note.Text = "+ 새 노트 추가";
            isTextbox_New_Note_Clicked = false;
        }

        private void TextBox_New_Note_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void TextBox_New_Note_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.Handled = false;
            e.SuppressKeyPress = false;

            Send_Log_Message("1-1>NotePadForm::TextBox_New_Note_KeyUp -> Add New Note : " + textBox_New_Note.Text);
            Add_Note(textBox_New_Note.Text);
            textBox_New_Note.Text = "";
        }

        private void TextBox_New_Note_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu textboxMenu = new ContextMenu();
                MenuItem copyMenu = new MenuItem("복사", new EventHandler(OnCopyMenu_textBox_New_Note_Click));
                MenuItem cutMenu = new MenuItem("잘라내기", new EventHandler(OnCutMenu_textBox_New_Note_Click));
                MenuItem pasteMenu = new MenuItem("붙여넣기", new EventHandler(OnPasteMenu_textBox_New_Note_Click));

                textboxMenu.Popup += new EventHandler(OnPopupEvent_textBox_New_Note);
                textboxMenu.MenuItems.Add(copyMenu);
                textboxMenu.MenuItems.Add(cutMenu);
                textboxMenu.MenuItems.Add(pasteMenu);
                textBox_New_Note.ContextMenu = textboxMenu;

                textBox_New_Note.ContextMenu.Show(textBox_New_Note, new Point(e.X, e.Y));
            }
        }

        private void OnPopupEvent_textBox_New_Note(object sender, EventArgs e)
        {
            ContextMenu ctm = (ContextMenu)sender;
            ctm.MenuItems[0].Enabled = textBox_New_Note.SelectedText.Length != 0; // copy
            ctm.MenuItems[1].Enabled = textBox_New_Note.SelectedText.Length != 0; // cut
            ctm.MenuItems[2].Enabled = Clipboard.ContainsText(); // paste
        }
        private void OnCopyMenu_textBox_New_Note_Click(object sender, EventArgs e) { textBox_New_Note.Copy(); }
        private void OnCutMenu_textBox_New_Note_Click(object sender, EventArgs e) { textBox_New_Note.Cut(); }
        private void OnPasteMenu_textBox_New_Note_Click(object sender, EventArgs e) { textBox_New_Note.Paste(); }
    }
}
