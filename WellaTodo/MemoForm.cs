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
    public partial class MemoForm : Form
    {
        private string _textBoxString;
        public string TextBoxString { get => _textBoxString; set => _textBoxString = value; }

        bool isTextBoxChanged = false;
        public bool IsTextBoxChanged
        {
            get => isTextBoxChanged;
            set => isTextBoxChanged = value;
        }

        public MemoForm()
        {
            InitializeComponent();
        }

        private void MemoForm_Load(object sender, EventArgs e)
        {
            textBox1.Text = TextBoxString;
            textBox1.SelectionStart = textBox1.Text.Length;
            textBox1.Font = new Font("돋움", 14.0f, FontStyle.Regular);

            IsTextBoxChanged = false;
        }

        private void MemoForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            IsTextBoxChanged = false;

            TextBoxString = textBox1.Text;
        }

        private void MemoForm_Resize(object sender, EventArgs e)
        {
            Refresh();
        }

        private void MemoForm_Paint(object sender, PaintEventArgs e)
        {
            textBox1.Location = new Point(10, 10);
            textBox1.Size = new Size(Width - 40, Height - 100);

            button1.Location = new Point(Width / 2 - 30, Height - 80);
            button1.Size = new Size(60, 30);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            isTextBoxChanged = true;
        }

        private void textBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu textboxMenu = new ContextMenu();
                MenuItem copyMenu = new MenuItem("복사", new EventHandler(OnCopyMenu_Click));
                MenuItem cutMenu = new MenuItem("잘라내기", new EventHandler(OnCutMenu_Click));
                MenuItem pasteMenu = new MenuItem("붙여넣기", new EventHandler(OnPasteMenu_Click));
                MenuItem selectAllMenu = new MenuItem("전체 선택", new EventHandler(OnSelectAllMenu_Click));
                MenuItem undoMenu = new MenuItem("실행 취소", new EventHandler(OnUndoMenu_Click));

                textboxMenu.Popup += new EventHandler(OnPopupEvent);
                textboxMenu.MenuItems.Add(copyMenu);
                textboxMenu.MenuItems.Add(cutMenu);
                textboxMenu.MenuItems.Add(pasteMenu);
                textboxMenu.MenuItems.Add(selectAllMenu);
                textboxMenu.MenuItems.Add("-");
                textboxMenu.MenuItems.Add(undoMenu);
                textBox1.ContextMenu = textboxMenu;

                textBox1.ContextMenu.Show(this, new Point(e.X, e.Y));
            }
        }

        private void OnPopupEvent(object sender, EventArgs e)
        {
            ContextMenu ctm = (ContextMenu)sender;

            ctm.MenuItems[0].Enabled = textBox1.SelectedText.Length != 0; // copy
            ctm.MenuItems[1].Enabled = textBox1.SelectedText.Length != 0; // cut
            ctm.MenuItems[2].Enabled = Clipboard.ContainsText(); // paste
            ctm.MenuItems[3].Enabled = textBox1.Text.Length != 0; // selectAll
            ctm.MenuItems[5].Enabled = textBox1.CanUndo; // undo
        }

        private void OnCopyMenu_Click(object sender, EventArgs e) { textBox1.Copy(); }
        private void OnCutMenu_Click(object sender, EventArgs e) { textBox1.Cut(); }
        private void OnPasteMenu_Click(object sender, EventArgs e) { textBox1.Paste(); }
        private void OnSelectAllMenu_Click(object sender, EventArgs e) { textBox1.SelectAll(); }
        private void OnUndoMenu_Click(object sender, EventArgs e) { textBox1.Undo(); }
    }
}
