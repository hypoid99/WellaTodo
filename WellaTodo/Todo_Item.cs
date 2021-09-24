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
    public partial class Todo_Item : UserControl
    {
        public event UserControl_Event UserControl_Event_method;

        static readonly Color PSEUDO_BACK_COLOR = Color.White;
        static readonly Color PSEUDO_HIGHLIGHT_COLOR = Color.Gray;

        private string _title;
        public string TD_title
        {
            get { return _title; }
            set { _title = value; label1.Text = value; }
        }

        private bool _complete;
        public bool TD_complete
        {
            get { return _complete; }
            set { _complete = value; checkBox1.Checked = value; }
        }

        private bool _important;
        public bool TD_important
        {
            get { return _important; }
            set { _important = value; checkBox2.Checked = value; }
        }

        private bool isCompleteClicked = false;
        private bool isImportantClicked = false;
        private bool isDeleteClicked = false;
        public bool IsCompleteClicked { get => isCompleteClicked; set => isCompleteClicked = value; }
        public bool IsImportantClicked { get => isImportantClicked; set => isImportantClicked = value; }
        public bool IsDeleteClicked { get => isDeleteClicked; set => isDeleteClicked = value; }

        public Todo_Item()
        {
            InitializeComponent();
        }

        public Todo_Item(string text, bool chk_complete, bool chk_important)
        {
            InitializeComponent();

            TD_title = text;
            TD_complete = chk_complete;
            TD_important = chk_important;
        }

        private void Todo_Item_Load(object sender, EventArgs e)
        {
            this.BackColor = PSEUDO_BACK_COLOR;
            label1.BackColor = PSEUDO_BACK_COLOR;
        }

        private void Todo_Item_Paint(object sender, PaintEventArgs e)
        {
            checkBox2.Location = new Point(this.Width - 50, checkBox2.Location.Y);
        }


        private void Todo_Item_Resize(object sender, EventArgs e)
        {
            checkBox2.Location = new Point(this.Width - 50, checkBox2.Location.Y);
        }

        public bool isCompleted()
        {
            return checkBox1.Checked;
        }

        public bool isImportant()
        {
            return checkBox2.Checked;
        }

        private void Todo_Item_Click(object sender, MouseEventArgs e)
        {
            checkBox2.Location = new Point(this.Width - 50, checkBox2.Location.Y);
            if (UserControl_Event_method != null)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        UserControl_Event_method?.Invoke(this, e);
                        break;
                    case MouseButtons.Right:
                        ContextMenu deleteMenu = new ContextMenu();
                        MenuItem deleteItem = new MenuItem("Delete", new System.EventHandler(this.OnDeleteMenuItem_Click));
                        deleteMenu.MenuItems.Add(deleteItem);
                        deleteMenu.Show(this, new Point(e.X, e.Y));
                        break;
                }
            }
        }

        //
        // event
        //
        private void Todo_Item_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = PSEUDO_HIGHLIGHT_COLOR;
            label1.BackColor = PSEUDO_HIGHLIGHT_COLOR;
        }

        private void Todo_Item_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = PSEUDO_BACK_COLOR;
            label1.BackColor = PSEUDO_BACK_COLOR;
        }

        private void label1_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = PSEUDO_HIGHLIGHT_COLOR;
            label1.BackColor = PSEUDO_HIGHLIGHT_COLOR;
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = PSEUDO_BACK_COLOR;
            label1.BackColor = PSEUDO_BACK_COLOR;
        }

        private void checkBox1_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = PSEUDO_HIGHLIGHT_COLOR;
            label1.BackColor = PSEUDO_HIGHLIGHT_COLOR;
        }

        private void checkBox1_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = PSEUDO_BACK_COLOR;
            label1.BackColor = PSEUDO_BACK_COLOR;
        }

        private void checkBox2_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = PSEUDO_HIGHLIGHT_COLOR;
            label1.BackColor = PSEUDO_HIGHLIGHT_COLOR;
        }

        private void checkBox2_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = PSEUDO_BACK_COLOR;
            label1.BackColor = PSEUDO_BACK_COLOR;
        }

        // 완료 체크시
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            TD_complete = checkBox1.Checked;
            if (TD_complete)
            {
                label1.Font = new Font(label1.Font.Name, label1.Font.SizeInPoints, FontStyle.Strikeout);
            }
            else
            {
                label1.Font = new Font(label1.Font.Name, label1.Font.SizeInPoints, FontStyle.Regular);
            }
            IsCompleteClicked = true;
            UserControl_Event_method?.Invoke(this, e);
            IsCompleteClicked = false;
        }

        // 중요 체크시
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            TD_important = checkBox2.Checked;
            IsImportantClicked = true;
            UserControl_Event_method?.Invoke(this, e);
            IsImportantClicked = false;
        }

        // 할일 클릭시
        private void Todo_Item_MouseClick(object sender, MouseEventArgs e)
        {
            Todo_Item_Click(sender, e);
        }

        private void label1_MouseClick(object sender, MouseEventArgs e)
        {
            Todo_Item_Click(sender, e);
        }

        // 할일 삭제시
        private void OnDeleteMenuItem_Click(object sender, EventArgs e)
        {
            IsDeleteClicked = true;
            UserControl_Event_method?.Invoke(this, e);
            IsDeleteClicked = false;
        }
    }
}