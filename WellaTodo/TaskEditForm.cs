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
    public partial class TaskEditForm : Form
    {
        static readonly int PANEL_SX = 10;
        static readonly int PANEL_WIDTH = 300;
        static readonly int PANEL_HEIGHT = 350;

        static readonly Color PSEUDO_BACK_COLOR = Color.White;
        static readonly Color PSEUDO_HIGHLIGHT_COLOR = Color.LightCyan;
        static readonly Color PSEUDO_SELECTED_COLOR = Color.Cyan;
        static readonly Color PSEUDO_TEXTBOX_BACK_COLOR = Color.LightCyan;

        RoundCheckbox roundCheckbox = new RoundCheckbox();
        StarCheckbox starCheckbox = new StarCheckbox();

        RoundLabel roundLabel_Planned = new RoundLabel();
        RoundLabel roundLabel_Repeat = new RoundLabel();

        private CDataCell m_DataCell;
        public CDataCell TE_DataCell { get => m_DataCell; set => m_DataCell = value; }

        private bool isNewTask = false;
        public bool IsNewTask { get => isNewTask; set => isNewTask = value; }
        private bool isCompleteChanged = false;
        public bool IsCompleteChanged { get => isCompleteChanged; set => isCompleteChanged = value; }
        private bool isTitleChanged = false;
        public bool IsTitleChanged { get => isTitleChanged; set => isTitleChanged = value; }
        private bool isImportantChanged = false;
        public bool IsImportantChanged { get => isImportantChanged; set => isImportantChanged = value; }
        private bool isPlannedChanged = false;
        public bool IsPlannedChanged { get => isPlannedChanged; set => isPlannedChanged = value; }
        private bool isPlannedDeleted = false;
        public bool IsPlannedDeleted { get => isPlannedDeleted; set => isPlannedDeleted = value; }
        private bool isRepeatChanged = false;
        public bool IsRepeatChanged { get => isRepeatChanged; set => isRepeatChanged = value; }
        private bool isMemoChanged = false;
        public bool IsMemoChanged { get => isMemoChanged; set => isMemoChanged = value; }
        private bool isCreated = false;
        public bool IsCreated { get => isCreated; set => isCreated = value; }
        private bool isDeleted = false;
        public bool IsDeleted { get => isDeleted; set => isDeleted = value; }


        private string[] repeatType = new string[]{"없음", "매일", "평일", "매주", "매월", "매년"};

        public TaskEditForm()
        {
            InitializeComponent();

            TE_DataCell = null;
        }

        private void Initiate()
        {
            Size = new Size(PANEL_WIDTH, PANEL_HEIGHT);
            BackColor = PSEUDO_BACK_COLOR;

            roundCheckbox.MouseClick += new MouseEventHandler(roundCheckbox_MouseClick);
            roundCheckbox.Location = new Point(PANEL_SX, 5);
            roundCheckbox.Size = new Size(25, 25);
            roundCheckbox.BackColor = PSEUDO_BACK_COLOR;
            panel_TaskEdit.Controls.Add(roundCheckbox);

            textBox_Title.MouseDown += new MouseEventHandler(textBox_Title_MouseDown);
            textBox_Title.Leave += new EventHandler(textBox_Title_Leave);
            textBox_Title.KeyDown += new KeyEventHandler(textBox_Title_KeyDown);
            textBox_Title.KeyUp += new KeyEventHandler(textBox_Title_KeyUp);
            textBox_Title.Location = new Point(PANEL_SX + 35, 8);
            textBox_Title.Size = new Size(PANEL_WIDTH - 100, 25);
            textBox_Title.BackColor = PSEUDO_TEXTBOX_BACK_COLOR;

            starCheckbox.MouseClick += new MouseEventHandler(starCheckbox_MouseClick);
            starCheckbox.Location = new Point(PANEL_WIDTH - 50, 5);
            starCheckbox.Size = new Size(25, 25);
            starCheckbox.BackColor = PSEUDO_BACK_COLOR;  // BackColor 설정안하면 이상하게됨....
            panel_TaskEdit.Controls.Add(starCheckbox);

            roundLabel_Planned.MouseClick += new MouseEventHandler(roundLabel_Planned_Click);
            roundLabel_Planned.MouseEnter += new EventHandler(TaskEdit_MouseEnter);
            roundLabel_Planned.MouseLeave += new EventHandler(TaskEdit_MouseLeave);
            roundLabel_Planned.Text = "기한 : " + TE_DataCell.DC_deadlineTime.ToString();
            roundLabel_Planned.Location = new Point(PANEL_SX + 5, 40);
            roundLabel_Planned.Size = new Size(PANEL_WIDTH - 50, 30);
            panel_TaskEdit.Controls.Add(roundLabel_Planned);

            roundLabel_Repeat.MouseClick += new MouseEventHandler(roundLabel_Repeat_Click);
            roundLabel_Repeat.MouseEnter += new EventHandler(TaskEdit_MouseEnter);
            roundLabel_Repeat.MouseLeave += new EventHandler(TaskEdit_MouseLeave);
            roundLabel_Repeat.Text = "반복 : " + repeatType[TE_DataCell.DC_repeatType];
            roundLabel_Repeat.Location = new Point(PANEL_SX + 5, 75);
            roundLabel_Repeat.Size = new Size(PANEL_WIDTH - 50, 30);
            panel_TaskEdit.Controls.Add(roundLabel_Repeat);

            textBox_Memo.MouseDown += new MouseEventHandler(textBox_Memo_MouseDown);
            textBox_Memo.Leave += new EventHandler(textBox_Memo_Leave);
            textBox_Memo.Multiline = true;
            textBox_Memo.Location = new Point(PANEL_SX + 5, 115);
            textBox_Memo.Size = new Size(PANEL_WIDTH - 50, 130);
            textBox_Memo.Text = TE_DataCell.DC_memo;

            // 닫기 버튼
            button_Close.Location = new Point(PANEL_SX + 20, 255);
            button_Close.Size = new Size(75, 25);

            // 생성 또는 삭제 버튼
            if (IsNewTask)
            {
                Text = "할 일 생성";
                roundCheckbox.Enabled = false;
                button_Delete.Text = "생성";
            }
            else
            {
                Text = "할 일 수정";
                roundCheckbox.Enabled = true;
                button_Delete.Text = "삭제";
            }
            button_Delete.Location = new Point(PANEL_SX + 160, 255);
            button_Delete.Size = new Size(75, 25);
        }

        private void TaskEditForm_Load(object sender, EventArgs e)
        {
            Initiate();
        }

        private void TaskEditForm_Paint(object sender, PaintEventArgs e)
        {
            textBox_Title.Text = TE_DataCell.DC_title;
            roundCheckbox.Checked = TE_DataCell.DC_complete;
            starCheckbox.Checked = TE_DataCell.DC_important;
        }

        private void TaskEditForm_Resize(object sender, EventArgs e)
        {
            Refresh();
        }

        private void button_Close_Click(object sender, EventArgs e)
        {
            if (IsNewTask)
            {
                Console.WriteLine("1>TaskEditForm::button_Close_Click -> Task Create is Canceled!!");
                IsCreated = false;
                Close();
            }
            else
            {
                IsDeleted = false;
                Close();
            }
        }

        private void button_Delete_Click(object sender, EventArgs e)
        {
            if (IsNewTask)
            {
                Console.WriteLine("1>TaskEditForm::button_Delete_Click -> Task is Created!!");
                IsCreated = true;
                Close();
            }
            else
            {
                Console.WriteLine("1>TaskEditForm::button_Delete_Click -> Task Delete");
                IsDeleted = true;
                Close();
            }
        }

        private void roundCheckbox_MouseClick(object sender, EventArgs e)
        {
            Console.WriteLine("1>TaskEditForm::starCheckbox_MouseClick -> Complete Changed");
            TE_DataCell.DC_complete = roundCheckbox.Checked;
            isCompleteChanged = true;
        }

        private void starCheckbox_MouseClick(object sender, EventArgs e)
        {
            Console.WriteLine("1>TaskEditForm::starCheckbox_MouseClick -> Important Changed");
            TE_DataCell.DC_important = starCheckbox.Checked;
            isImportantChanged = true;
        }

        private void TaskEdit_MouseEnter(object sender, EventArgs e)
        {
            //foreach (Control c in Controls) c.BackColor = HIGHLIGHT_COLOR;
        }

        private void TaskEdit_MouseLeave(object sender, EventArgs e)
        {
            //foreach (Control c in Controls) c.BackColor = BACK_COLOR;
        }

        //
        // 제목창
        //
        private void textBox_Title_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void textBox_Title_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = false;
                e.SuppressKeyPress = false;

                if (textBox_Title.Text.Trim().Length == 0)
                {
                    textBox_Title.Text = TE_DataCell.DC_title;
                    return;
                }

                TE_DataCell.DC_title = textBox_Title.Text;  // 입력 사항에 오류가 있는지 체크할 것

                if (IsNewTask)
                {
                    Console.WriteLine("1>TaskEditForm::textBox_Title_KeyUp -> Task is Created");
                    IsCreated = true;
                }
                else
                {
                    Console.WriteLine("1>TaskEditForm::textBox_Title_KeyUp -> Task Title Changed");
                    IsTitleChanged = true;
                }
                Close();
            }
        }

        private void textBox_Title_Leave(object sender, EventArgs e)
        {
            if (textBox_Title.Text.Trim().Length == 0)
            {
                textBox_Title.Text = TE_DataCell.DC_title;
                return;
            }

            TE_DataCell.DC_title = textBox_Title.Text;  // 입력 사항에 오류가 있는지 체크할 것

            if (IsNewTask)
            {
                Console.WriteLine("1>TaskEditForm::textBox_Title_Leave -> Task is Created");
                IsCreated = true;
            }
            else
            {
                Console.WriteLine("1>TaskEditForm::textBox_Title_Leave -> Task Title Changed");
                IsTitleChanged = true;
            }
        }

        private void textBox_Title_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu textboxMenu = new ContextMenu();
                MenuItem copyMenu = new MenuItem("복사", new EventHandler(OnCopyMenu_textBox_Title_Click));
                MenuItem cutMenu = new MenuItem("잘라내기", new EventHandler(OnCutMenu_textBox_Title_Click));
                MenuItem pasteMenu = new MenuItem("붙여넣기", new EventHandler(OnPasteMenu_textBox_Title_Click));

                textboxMenu.Popup += new EventHandler(OnPopupEvent_textBox_Title);
                textboxMenu.MenuItems.Add(copyMenu);
                textboxMenu.MenuItems.Add(cutMenu);
                textboxMenu.MenuItems.Add(pasteMenu);
                textBox_Title.ContextMenu = textboxMenu;

                textBox_Title.ContextMenu.Show(textBox_Title, new Point(e.X, e.Y));
            }
        }

        private void OnPopupEvent_textBox_Title(object sender, EventArgs e)
        {
            ContextMenu ctm = (ContextMenu)sender;
            ctm.MenuItems[0].Enabled = textBox_Title.SelectedText.Length != 0; // copy
            ctm.MenuItems[1].Enabled = textBox_Title.SelectedText.Length != 0; // cut
            ctm.MenuItems[2].Enabled = Clipboard.ContainsText(); // paste
        }
        private void OnCopyMenu_textBox_Title_Click(object sender, EventArgs e) { textBox_Title.Copy(); }
        private void OnCutMenu_textBox_Title_Click(object sender, EventArgs e) { textBox_Title.Cut(); }
        private void OnPasteMenu_textBox_Title_Click(object sender, EventArgs e) { textBox_Title.Paste(); }

        //
        // 기한 설정
        //
        private void roundLabel_Planned_MouseEnter(object sender, EventArgs e)
        {
            roundLabel_Planned.BackColor = PSEUDO_HIGHLIGHT_COLOR;
        }

        private void roundLabel_Planned_MouseLeave(object sender, EventArgs e)
        {
            roundLabel_Planned.BackColor = TE_DataCell.DC_deadlineType > 0 ? PSEUDO_SELECTED_COLOR : PSEUDO_BACK_COLOR;
        }

        private void roundLabel_Planned_Click(object sender, MouseEventArgs e)
        {
            roundLabel_Planned.Focus();
            if (e.Button != MouseButtons.Left) return;

            ContextMenu deadlineMenu = new ContextMenu();
            MenuItem selectDeadline = new MenuItem("날짜 선택", new EventHandler(OnSelectDeadline_Click));
            MenuItem deleteDeadline = new MenuItem("기한 설정 제거", new EventHandler(OnDeleteDeadline_Click));
            deadlineMenu.MenuItems.Add(selectDeadline);
            deadlineMenu.MenuItems.Add(deleteDeadline);

            int px = roundLabel_Planned.Location.X;
            int py = roundLabel_Planned.Location.Y + roundLabel_Planned.Height;
            deadlineMenu.Show(this, new Point(px, py));
        }

        private void OnSelectDeadline_Click(object sender, EventArgs e)
        {
            DateTimePickerForm carendar = new DateTimePickerForm();
            carendar.ShowDialog();

            DateTime dt = carendar.SelectedDateTime;
            if (carendar.IsSelected && (carendar.SelectedDateTime != default))
            {
                if (dt.Hour == 0 && dt.Minute == 0 && dt.Second == 0) // 시간을 입력하지 않을때
                {
                    dt = new DateTime(dt.Year, dt.Month, dt.Day, 22, 00, 00);
                }
                carendar.IsSelected = false;
                Console.WriteLine("1>TaskEditForm::OnSelectDeadline_Click -> 기한 설정 type 4");
                TE_DataCell.DC_deadlineType = 4;
                TE_DataCell.DC_deadlineTime = dt;
            }
            else
            {
                Console.WriteLine("1>TaskEditForm::OnSelectDeadline_Click -> 기한 해제 type 0");
                TE_DataCell.DC_deadlineType = 0;
                TE_DataCell.DC_deadlineTime = default;
            }

            if (TE_DataCell.DC_deadlineType > 0)
            {
                roundLabel_Planned.Text = "기한 : " + TE_DataCell.DC_deadlineTime.ToString();
            }
            else
            {
                roundLabel_Planned.Text = "기한 해제";
            }

            IsPlannedChanged = true;
        }

        private void OnDeleteDeadline_Click(object sender, EventArgs e)
        {
            Console.WriteLine("1>TaskEditForm::OnDeleteDeadline_Click -> 기한 해제");
            TE_DataCell.DC_deadlineType = 0;
            TE_DataCell.DC_deadlineTime = default;
            roundLabel_Planned.Text = "기한 해제";

            IsPlannedDeleted = true;
        }

        //
        // 반복 메뉴
        //
        private void roundLabel_Repeat_MouseEnter(object sender, EventArgs e)
        {
            roundLabel_Repeat.BackColor = PSEUDO_HIGHLIGHT_COLOR;
        }

        private void roundLabel_Repeat_MouseLeave(object sender, EventArgs e)
        {
            roundLabel_Repeat.BackColor = TE_DataCell.DC_repeatType > 0 ? PSEUDO_SELECTED_COLOR : PSEUDO_BACK_COLOR;
        }

        private void roundLabel_Repeat_Click(object sender, MouseEventArgs e)
        {
            roundLabel_Repeat.Focus();
            if (e.Button != MouseButtons.Left) return;

            ContextMenu repeatMenu = new ContextMenu();
            MenuItem everyDayRepeat = new MenuItem("매일", new EventHandler(OnEveryDayRepeat_Click));
            MenuItem workingDayRepeat = new MenuItem("평일", new EventHandler(OnWorkingDayRepeat_Click));
            MenuItem everyWeekRepeat = new MenuItem("매주", new EventHandler(OnEveryWeekRepeat_Click));
            MenuItem everyMonthRepeat = new MenuItem("매월", new EventHandler(OnEveryMonthRepeat_Click));
            MenuItem everyYearRepeat = new MenuItem("매년", new EventHandler(OnEveryYearRepeat_Click));
            MenuItem deleteRepeat = new MenuItem("반복 제거", new EventHandler(OnDeleteRepeat_Click));
            repeatMenu.MenuItems.Add(everyDayRepeat);
            repeatMenu.MenuItems.Add(workingDayRepeat);
            repeatMenu.MenuItems.Add(everyWeekRepeat);
            repeatMenu.MenuItems.Add(everyMonthRepeat);
            repeatMenu.MenuItems.Add(everyYearRepeat);
            repeatMenu.MenuItems.Add(deleteRepeat);

            int px = roundLabel_Repeat.Location.X;
            int py = roundLabel_Repeat.Location.Y + roundLabel_Repeat.Height;
            repeatMenu.Show(this, new Point(px, py));
        }

        private void OnEveryDayRepeat_Click(object sender, EventArgs e)
        {

        }

        private void OnWorkingDayRepeat_Click(object sender, EventArgs e)
        {

        }

        private void OnEveryWeekRepeat_Click(object sender, EventArgs e)
        {

        }

        private void OnEveryMonthRepeat_Click(object sender, EventArgs e)
        {

        }

        private void OnEveryYearRepeat_Click(object sender, EventArgs e)
        {

        }

        private void OnDeleteRepeat_Click(object sender, EventArgs e)
        {

        }

        //
        // 메모창
        //
        private void textBox_Memo_Leave(object sender, EventArgs e)
        {
            Console.WriteLine("1>TaskEditForm::textBox_Memo_Leave -> Memo Changed");

            //메모 내용에 변경이 있는지 확인(?)
            TE_DataCell.DC_memo = textBox_Memo.Text;

            isMemoChanged = true;
        }

        private void textBox_Memo_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu textboxMenu = new ContextMenu();
                MenuItem copyMenu = new MenuItem("복사", new EventHandler(OnCopyMenu_textBox_Memo_Click));
                MenuItem cutMenu = new MenuItem("잘라내기", new EventHandler(OnCutMenu_textBox_Memo_Click));
                MenuItem pasteMenu = new MenuItem("붙여넣기", new EventHandler(OnPasteMenu_textBox_Memo_Click));
                MenuItem selectAllMenu = new MenuItem("전체 선택", new EventHandler(OnSelectAllMenu_textBox_Memo_Click));
                MenuItem undoMenu = new MenuItem("실행 취소", new EventHandler(OnUndoMenu_textBox_Memo_Click));

                textboxMenu.Popup += new EventHandler(OnPopupEvent_textBox_Memo);
                textboxMenu.MenuItems.Add(copyMenu);
                textboxMenu.MenuItems.Add(cutMenu);
                textboxMenu.MenuItems.Add(pasteMenu);
                textboxMenu.MenuItems.Add(selectAllMenu);
                textboxMenu.MenuItems.Add("-");
                textboxMenu.MenuItems.Add(undoMenu);
                textBox_Memo.ContextMenu = textboxMenu;

                textBox_Memo.ContextMenu.Show(textBox_Memo, new Point(e.X, e.Y));
            }
        }

        private void OnPopupEvent_textBox_Memo(object sender, EventArgs e)
        {
            ContextMenu ctm = (ContextMenu)sender;

            ctm.MenuItems[0].Enabled = textBox_Memo.SelectedText.Length != 0; // copy
            ctm.MenuItems[1].Enabled = textBox_Memo.SelectedText.Length != 0; // cut
            ctm.MenuItems[2].Enabled = Clipboard.ContainsText(); // paste
            ctm.MenuItems[3].Enabled = textBox_Memo.Text.Length != 0; // selectAll
            ctm.MenuItems[5].Enabled = textBox_Memo.CanUndo; // undo
        }

        private void OnCopyMenu_textBox_Memo_Click(object sender, EventArgs e) { textBox_Memo.Copy(); }
        private void OnCutMenu_textBox_Memo_Click(object sender, EventArgs e) { textBox_Memo.Cut(); }
        private void OnPasteMenu_textBox_Memo_Click(object sender, EventArgs e) { textBox_Memo.Paste(); }
        private void OnSelectAllMenu_textBox_Memo_Click(object sender, EventArgs e) { textBox_Memo.SelectAll(); }
        private void OnUndoMenu_textBox_Memo_Click(object sender, EventArgs e) { textBox_Memo.Undo(); }
    }
}
