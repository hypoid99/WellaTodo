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

        static readonly string FONT_NAME = "맑은 고딕";
        static readonly float FONT_SIZE_TEXT = 14.0f;
        static readonly int MAX_COUNT_FONT_SIZE = 16;

        MainController m_Controller;

        //NotePadEditorForm editorForm = new NotePadEditorForm();

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
            foreach (TwoLineList item in flowLayoutPanel_List.Controls)
            {
                item.Width = flowLayoutPanel_List.Width - LIST_WIDTH_GAP;
                //item.Width = flowLayoutPanel2.VerticalScroll.Visible
                //    ? flowLayoutPanel2.Width - 8 - SystemInformation.VerticalScrollBarWidth
                //    : flowLayoutPanel2.Width - 8;
            }
        }

        private TwoLineList CreateTwoLineList(CDataCell dc)
        {
            TwoLineList list = new TwoLineList(new Bitmap(ICON_LIST), dc.DC_title, "", "");
            list.TwoLineList_Click += new TwoLineList_Event(TwoLineList_Click);
            list.TwoLineList_DoubleClick += new TwoLineList_Event(TwoLineList_DoubleClick);
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
            TwoLineList list = CreateTwoLineList(dc);

            flowLayoutPanel_List.Controls.Add(list); // 판넬 컨렉션에 저장

            Update_List_Width();

            Send_Log_Message("4>NotePadForm::Update_Add_Note -> Add Note : " + dc.DC_title);
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

        private void Open_NotePadForm(TwoLineList list)
        {
            NotePadEditorForm editorForm = new NotePadEditorForm();

            editorForm.StartPosition = FormStartPosition.CenterParent;

            editorForm.ShowDialog();

            Send_Log_Message("1>NotePadForm::Open_NotePadForm -> NotePad contents Changed");
            //m_Controller.Perform_Modify_Memo_Text(note.DataCell);
        }

        // ----------------------------------------------------------
        // 사용자 입력 처리
        // ----------------------------------------------------------
        private void TwoLineList_Click(object sender, EventArgs e)
        {
            TwoLineList sd = (TwoLineList)sender;
            MouseEventArgs me = (MouseEventArgs)e;

            switch (me.Button)
            {
                case MouseButtons.Left:
                    Send_Log_Message(">NotePadForm::TwoLineList_Click -> Left Button");
                    break;
                case MouseButtons.Right:
                    Send_Log_Message(">NotePadForm::TwoLineList_Click -> Right Button");
                    //MenuList_Right_Click_ContextMenu();  // 컨텍스트 메뉴
                    break;
                case MouseButtons.Middle:
                    Send_Log_Message(">NotePadForm::TwoLineList_Click -> Middle Button");
                    break;
            }
        }

        private void TwoLineList_DoubleClick(object sender, EventArgs e)
        {
            TwoLineList sd = (TwoLineList)sender;
            MouseEventArgs me = (MouseEventArgs)e;

            switch (me.Button)
            {
                case MouseButtons.Left:
                    Send_Log_Message(">NotePadForm::TwoLineList_DoubleClick -> Left Button");
                    Open_NotePadForm(sd);
                    break;
                case MouseButtons.Right:
                    Send_Log_Message(">NotePadForm::TwoLineList_DoubleClick -> Right Button");
                    //MenuList_Right_Click_ContextMenu();  // 컨텍스트 메뉴
                    break;
                case MouseButtons.Middle:
                    Send_Log_Message(">NotePadForm::TwoLineList_DoubleClick -> Middle Button");
                    break;
            }
        }

        private void label_Add_Note_Click(object sender, EventArgs e)
        {
            New_Note();
        }
    }
}
