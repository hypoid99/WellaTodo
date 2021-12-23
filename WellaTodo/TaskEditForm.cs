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
        static readonly int PANEL_SY = 10;
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

        private bool isChanged = false;
        public bool IsChanged { get => isChanged; set => isChanged = value; }

        public TaskEditForm()
        {
            InitializeComponent();

            IsChanged = false;
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
            roundLabel_Planned.Text = "기한 설정";
            roundLabel_Planned.Location = new Point(PANEL_SX + 5, 40);
            roundLabel_Planned.Size = new Size(PANEL_WIDTH - 50, 30);
            panel_TaskEdit.Controls.Add(roundLabel_Planned);

            roundLabel_Repeat.MouseClick += new MouseEventHandler(roundLabel_Repeat_Click);
            roundLabel_Repeat.MouseEnter += new EventHandler(TaskEdit_MouseEnter);
            roundLabel_Repeat.MouseLeave += new EventHandler(TaskEdit_MouseLeave);
            roundLabel_Repeat.Text = "반복";
            roundLabel_Repeat.Location = new Point(PANEL_SX + 5, 75);
            roundLabel_Repeat.Size = new Size(PANEL_WIDTH - 50, 30);
            panel_TaskEdit.Controls.Add(roundLabel_Repeat);

            textBox_Memo.Multiline = true;
            textBox_Memo.Location = new Point(PANEL_SX + 5, 115);
            textBox_Memo.Size = new Size(PANEL_WIDTH - 50, 130);

            // 닫기 버튼
            button_Close.Location = new Point(PANEL_SX + 20, 255);
            button_Close.Size = new Size(75, 25);

            // 삭제 버튼
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
            if (isChanged)
            {

            }
            Close();
        }

        private void button_Delete_Click(object sender, EventArgs e)
        {

        }

        private void roundCheckbox_MouseClick(object sender, EventArgs e)
        {
            TE_DataCell.DC_complete = roundCheckbox.Checked;
            isChanged = true;
        }

        private void starCheckbox_MouseClick(object sender, EventArgs e)
        {
            TE_DataCell.DC_important = starCheckbox.Checked;
            isChanged = true;
        }

        private void TaskEdit_MouseEnter(object sender, EventArgs e)
        {
            //foreach (Control c in Controls) c.BackColor = HIGHLIGHT_COLOR;
        }

        private void TaskEdit_MouseLeave(object sender, EventArgs e)
        {
            //foreach (Control c in Controls) c.BackColor = BACK_COLOR;
        }

        private void roundLabel_Planned_Click(object sender, MouseEventArgs e)
        {

        }

        private void roundLabel_Repeat_Click(object sender, MouseEventArgs e)
        {

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

        private void textBox_Title_Leave(object sender, EventArgs e)
        {
            if (textBox_Title.Text.Trim().Length == 0)
            {
                textBox_Title.Text = TE_DataCell.DC_title;
                return;
            }

            TE_DataCell.DC_title = textBox_Title.Text;  // 입력 사항에 오류가 있는지 체크할 것
            isChanged = true;
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
                isChanged = true;

                Close();
            }
        }

        private void textBox_Title_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
    }
}
