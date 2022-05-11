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
    public partial class TaskTitleForm : Form
    {
        static readonly int PANEL_SX = 10;
        static readonly int PANEL_WIDTH = 800;
        static readonly int PANEL_HEIGHT = 100;

        static readonly Color PSEUDO_BACK_COLOR = Color.White;
        static readonly Color PSEUDO_HIGHLIGHT_COLOR = Color.LightCyan;
        static readonly Color PSEUDO_SELECTED_COLOR = Color.Cyan;
        static readonly Color PSEUDO_TEXTBOX_BACK_COLOR = Color.LightCyan;

        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
        private string _textBoxString;
        public string TextBoxString
        {
            get
            {
                _textBoxString = textBox_Title.Text;
                return _textBoxString;
            }
            set 
            { 
                _textBoxString = value; 
                textBox_Title.Text = _textBoxString;
            }
        }

        bool isTextBoxChanged = false;
        public bool IsTextBoxChanged
        {
            get => isTextBoxChanged;
            set => isTextBoxChanged = value;
        }

        // --------------------------------------------------
        // Constructor
        // --------------------------------------------------
        public TaskTitleForm()
        {
            InitializeComponent();
        }

        // --------------------------------------------------
        // Form 이벤트
        // --------------------------------------------------
        private void TaskTitleForm_Load(object sender, EventArgs e)
        {
            Size = new Size(PANEL_WIDTH, PANEL_HEIGHT);
            BackColor = PSEUDO_BACK_COLOR;

            textBox_Title.Enter += new EventHandler(textBox_Title_Enter);
            textBox_Title.Leave += new EventHandler(textBox_Title_Leave);
            textBox_Title.KeyDown += new KeyEventHandler(textBox_Title_KeyDown);
            textBox_Title.KeyUp += new KeyEventHandler(textBox_Title_KeyUp);
            textBox_Title.MouseDown += new MouseEventHandler(textBox_Title_MouseDown);

            textBox_Title.BackColor = PSEUDO_TEXTBOX_BACK_COLOR;

            textBox_Title.Text = TextBoxString;
            textBox_Title.SelectionStart = textBox_Title.Text.Length;
            textBox_Title.Font = new Font("돋움", 14.0f, FontStyle.Regular);

            IsTextBoxChanged = false;
        }

        private void TaskTitleForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            TextBoxString = textBox_Title.Text;
        }

        private void TaskTitleForm_Resize(object sender, EventArgs e)
        {
            Refresh();
        }

        private void TaskTitleForm_Paint(object sender, PaintEventArgs e)
        {
            textBox_Title.Location = new Point(PANEL_SX, 8);
            textBox_Title.Size = new Size(Size.Width - PANEL_SX * 4, 25);
        }

        // -----------------------------------------
        // 제목창
        // -----------------------------------------
        private void textBox_Title_TextChanged(object sender, EventArgs e)
        {
            IsTextBoxChanged = true;
        }

        private void textBox_Title_Enter(object sender, EventArgs e)
        {

        }

        private void textBox_Title_Leave(object sender, EventArgs e)
        {

        }

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
            if (e.KeyCode != Keys.Enter) return;
            e.Handled = false;
            e.SuppressKeyPress = false;

            //IsTextBoxChanged = true;

            Close();
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
    }
}
