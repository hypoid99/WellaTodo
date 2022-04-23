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

        static readonly Image ICON_LIST = Properties.Resources.outline_list_black_24dp;

        static readonly Color BACK_COLOR = Color.White;
        static readonly Color HIGHLIGHT_COLOR = Color.LightCyan;
        static readonly Color SELECTED_COLOR = Color.Cyan;
        static readonly Color TEXTBOX_BACK_COLOR = Color.LightCyan;

        static readonly string FONT_NAME = "맑은 고딕";
        static readonly float FONT_SIZE_TEXT = 14.0f;
        static readonly int MAX_COUNT_FONT_SIZE = 16;

        MainController m_Controller;

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

        //--------------------------------------------------------------
        // 초기화 및 데이터 로딩, Update Display
        //--------------------------------------------------------------
        private void Initiate()
        {
            panel_Header.Width = Width;

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
            flowLayoutPanel_List.Width = Width;
            //flowLayoutPanel_List.Location = new Point(labelUserName.Location.X, labelUserName.Height);
            //flowLayoutPanel_List.Size = new Size(splitContainer1.SplitterDistance, splitContainer1.Panel1.Height - labelUserName.Height - TAIL_HEIGHT);

            panel_Footer.Width = Width;

            textBox_New_Note.MouseEnter += TextBox_New_Note_MouseEnter;
            textBox_New_Note.MouseLeave += TextBox_New_Note_MouseLeave;
            textBox_New_Note.KeyDown += TextBox_New_Note_KeyDown;
            textBox_New_Note.KeyUp += TextBox_New_Note_KeyUp;
            textBox_New_Note.MouseDown += TextBox_New_Note_MouseDown;
            textBox_New_Note.Font = new Font(FONT_NAME, FONT_SIZE_TEXT);
            textBox_New_Note.Location = new Point(10, 8);
            textBox_New_Note.Size = new Size(Width - 20, 25);
            textBox_New_Note.BackColor = TEXTBOX_BACK_COLOR;
            textBox_New_Note.Text = "+ 새 노트 추가";

            foreach (CDataCell data in m_Controller.Query_NoteFile()) // 노트 파일 리스트를 등록한다
            {
                NoteFileList list = CreateNoteFileList(data);
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
            panel_Header.Width = Width;
            label_Add_Note.Location = new Point(Width - 60, 9);
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
                case WParam.WM_MODIFY_NOTE_TEXT:
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

            Update_List_Width();

            Send_Log_Message("4>NotePadForm::Update_Add_Note -> Add Note : " + dc.DC_title);
        }

        private void Update_Modify_Note(CDataCell dc)
        {
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

        private void Open_NotePadForm(NoteFileList list)
        {
            NotePadEditorForm editorForm = new NotePadEditorForm();

            editorForm.StartPosition = FormStartPosition.CenterParent;
            editorForm.Note_RTF = list.DataCell.DC_RTF;

            editorForm.ShowDialog();

            if (editorForm.IsUnsaved)
            {

            }

            if (editorForm.Note_RTF == String.Empty)
            {
                Send_Log_Message("1>NotePadForm::Open_NotePadForm -> NotePad contents is Empty");
            }

            list.DataCell.DC_RTF = editorForm.Note_RTF;

            Send_Log_Message("1>NotePadForm::Open_NotePadForm -> NotePad contents is changed");
            m_Controller.Perform_Modify_Note_Text(list.DataCell);
        }

        // ----------------------------------------------------------
        // 사용자 입력 처리
        // ----------------------------------------------------------
        private void List_MouseClick(object sender, UserCommandEventArgs e)
        {
            NoteFileList sd = (NoteFileList)sender;

            // 클릭시 데이타 내용 확인하기
            m_Controller.Verify_DataCell(sd.DataCell);

            switch (e.CommandName)
            {
                case "Click":

                    break;
                case "DoubleClick":
                    Open_NotePadForm(sd);
                    break;
            }
        }

        private void label_Add_Note_Click(object sender, EventArgs e)
        {
            New_Note();
        }

        // -----------------------------------------------------------
        // 노트 생성 및 입력 처리 부분
        // -----------------------------------------------------------
        private void TextBox_New_Note_MouseEnter(object sender, EventArgs e)
        {
            if (!isTextbox_New_Note_Clicked) textBox_New_Note.Text = "";
            isTextbox_New_Note_Clicked = true;
        }

        private void TextBox_New_Note_MouseLeave(object sender, EventArgs e)
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
