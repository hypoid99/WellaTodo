using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Drawing.Drawing2D;

namespace WellaTodo
{
    public partial class Todo_Item : UserControl
    {
        public event UserControl_Event UserControl_Event_method;

        static readonly int TODO_ITEM_WIDTH = 180;
        static readonly int TODO_ITEM_HEIGHT = 35;

        static readonly Color PSEUDO_BACK_COLOR = Color.White;
        static readonly Color PSEUDO_HIGHLIGHT_COLOR = Color.LightCyan;
        static readonly Color PSEUDO_SELECTED_COLOR = Color.Cyan;
        static readonly Color PSEUDO_BORDER_COLOR = Color.Black;
        static readonly Color PSEUDO_FILL_COLOR = Color.Gold;
        static readonly float PSEUDO_PEN_THICKNESS = 1.0f;

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
            set { _complete = value; roundCheckbox1.Checked = value; }
        }

        private bool _important;
        public bool TD_important
        {
            get { return _important; }
            set { _important = value; starCheckbox1.Checked = value; }
        }

        private bool isCompleteClicked = false;
        public bool IsCompleteClicked { get => isCompleteClicked; set => isCompleteClicked = value; }
        private bool isImportantClicked = false;
        public bool IsImportantClicked { get => isImportantClicked; set => isImportantClicked = value; }
        private bool isDeleteClicked = false;
        public bool IsDeleteClicked { get => isDeleteClicked; set => isDeleteClicked = value; }
        private bool isItemSelected = false;
        public bool IsItemSelected 
        { 
            get => isItemSelected; 
            set 
            { 
                isItemSelected = value; 
                if (IsItemSelected)
                {
                    BackColor = PSEUDO_SELECTED_COLOR;
                    roundCheckbox1.BackColor = PSEUDO_SELECTED_COLOR;
                    label1.BackColor = PSEUDO_SELECTED_COLOR;
                    starCheckbox1.BackColor = PSEUDO_SELECTED_COLOR;
                } 
                else
                {
                    BackColor = PSEUDO_BACK_COLOR;
                    roundCheckbox1.BackColor = PSEUDO_BACK_COLOR;
                    label1.BackColor = PSEUDO_BACK_COLOR;
                    starCheckbox1.BackColor = PSEUDO_BACK_COLOR;
                }
            } 
        }

        RoundCheckbox roundCheckbox1 = new RoundCheckbox();
        StarCheckbox starCheckbox1 = new StarCheckbox();

        GraphicsPath outerBorderPath = null;
        int cornerRadius = 10;

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
            Size = new Size(TODO_ITEM_WIDTH, TODO_ITEM_HEIGHT);
            BackColor = PSEUDO_BACK_COLOR;

            roundCheckbox1.CheckedChanged += new EventHandler(roundCheckbox1_CheckedChanged);
            roundCheckbox1.Location = new Point(12, 4);
            roundCheckbox1.Size = new Size(25, 25);
            roundCheckbox1.BackColor = PSEUDO_BACK_COLOR;
            Controls.Add(roundCheckbox1);

            starCheckbox1.CheckedChanged += new EventHandler(starCheckbox1_CheckedChanged);
            starCheckbox1.Location = new Point(Width - 40, 5);
            starCheckbox1.Size = new Size(25, 25);
            starCheckbox1.BackColor = PSEUDO_BACK_COLOR;
            Controls.Add(starCheckbox1);

            label1.Location = new Point(45, 12);
            label1.BackColor = PSEUDO_BACK_COLOR;
            if (TD_complete)
                label1.Font = new Font(label1.Font.Name, label1.Font.SizeInPoints, FontStyle.Strikeout);
            else
                label1.Font = new Font(label1.Font.Name, label1.Font.SizeInPoints, FontStyle.Regular);
        }

        private void Todo_Item_Resize(object sender, EventArgs e)
        {
            starCheckbox1.Location = new Point(Width - 40, 5);
            SetPathOuterBorder();
        }

        private void Todo_Item_Paint(object sender, PaintEventArgs pevent)
        {
            starCheckbox1.Location = new Point(Width - 40, 5);

            Graphics g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            Rectangle rc = ClientRectangle;

            g.FillRectangle(new SolidBrush(BackColor), rc.Left - 1, rc.Top - 1, rc.Width + 1, rc.Height + 1);
            g.DrawPath(new Pen(PSEUDO_BORDER_COLOR, PSEUDO_PEN_THICKNESS), outerBorderPath);
        }

        private void SetPathOuterBorder()
        {
            Rectangle rectangle = ClientRectangle;
            float x = rectangle.X;
            float y = rectangle.Y;
            float width = rectangle.Width - 1;
            float height = rectangle.Height - 1;
            float cr = cornerRadius;

            GraphicsPath path = new GraphicsPath();

            path.AddBezier(x, y + cr, x, y, x + cr, y, x + cr, y);
            path.AddLine(x + cr, y, x + width - cr, y);
            path.AddBezier(x + width - cr, y, x + width, y, x + width, y + cr, x + width, y + cr);
            path.AddLine(x + width, y + cr, x + width, y + height - cr);
            path.AddBezier(x + width, y + height - cr, x + width, y + height, x + width - cr, y + height, x + width - cr, y + height);
            path.AddLine(x + width - cr, y + height, x + cr, y + height);
            path.AddBezier(x + cr, y + height, x, y + height, x, y + height - cr, x, y + height - cr);
            path.AddLine(x, y + height - cr, x, y + cr);

            outerBorderPath?.Dispose();
            outerBorderPath = path;
        }

        private void Todo_Item_Click(object sender, MouseEventArgs e)
        {
            if (UserControl_Event_method != null)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        UserControl_Event_method?.Invoke(this, e);
                        break;
                    case MouseButtons.Right:
                        ContextMenu deleteMenu = new ContextMenu();
                        MenuItem deleteItem = new MenuItem("항목 삭제", new EventHandler(this.OnDeleteMenuItem_Click));
                        deleteMenu.MenuItems.Add(deleteItem);
                        deleteMenu.Show(this, new Point(e.X, e.Y));
                        break;
                }
            }
        }

        //---------------------------------------------------------
        // control event
        //---------------------------------------------------------
        private void Todo_Item_MouseEnter(object sender, EventArgs e)
        {
            BackColor = IsItemSelected ? PSEUDO_SELECTED_COLOR : PSEUDO_HIGHLIGHT_COLOR;
            roundCheckbox1.BackColor = IsItemSelected ? PSEUDO_SELECTED_COLOR : PSEUDO_HIGHLIGHT_COLOR;
            label1.BackColor = IsItemSelected ? PSEUDO_SELECTED_COLOR : PSEUDO_HIGHLIGHT_COLOR;
            starCheckbox1.BackColor = IsItemSelected ? PSEUDO_SELECTED_COLOR : PSEUDO_HIGHLIGHT_COLOR;
        }

        private void Todo_Item_MouseLeave(object sender, EventArgs e)
        {
            BackColor = IsItemSelected ? PSEUDO_SELECTED_COLOR : PSEUDO_BACK_COLOR;
            roundCheckbox1.BackColor = IsItemSelected ? PSEUDO_SELECTED_COLOR : PSEUDO_BACK_COLOR;
            label1.BackColor = IsItemSelected ? PSEUDO_SELECTED_COLOR : PSEUDO_BACK_COLOR;
            starCheckbox1.BackColor = IsItemSelected ? PSEUDO_SELECTED_COLOR : PSEUDO_BACK_COLOR;
        }

        private void label1_MouseEnter(object sender, EventArgs e)
        {
            BackColor = IsItemSelected ? PSEUDO_SELECTED_COLOR : PSEUDO_HIGHLIGHT_COLOR;
            roundCheckbox1.BackColor = IsItemSelected ? PSEUDO_SELECTED_COLOR : PSEUDO_HIGHLIGHT_COLOR;
            label1.BackColor = IsItemSelected ? PSEUDO_SELECTED_COLOR : PSEUDO_HIGHLIGHT_COLOR;
            starCheckbox1.BackColor = IsItemSelected ? PSEUDO_SELECTED_COLOR : PSEUDO_HIGHLIGHT_COLOR;
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            BackColor = IsItemSelected ? PSEUDO_SELECTED_COLOR : PSEUDO_BACK_COLOR;
            roundCheckbox1.BackColor = IsItemSelected ? PSEUDO_SELECTED_COLOR : PSEUDO_BACK_COLOR;
            label1.BackColor = IsItemSelected ? PSEUDO_SELECTED_COLOR : PSEUDO_BACK_COLOR;
            starCheckbox1.BackColor = IsItemSelected ? PSEUDO_SELECTED_COLOR : PSEUDO_BACK_COLOR;
        }

        private void Todo_Item_MouseClick(object sender, MouseEventArgs e)
        {
            Todo_Item_Click(sender, e);
        }

        private void label1_MouseClick(object sender, MouseEventArgs e)
        {
            Todo_Item_Click(sender, e);
        }

        private void OnDeleteMenuItem_Click(object sender, EventArgs e)
        {
            IsDeleteClicked = true;
            UserControl_Event_method?.Invoke(this, e);
            IsDeleteClicked = false;
        }

        private void roundCheckbox1_CheckedChanged(object sender, EventArgs e)
        {
            TD_complete = roundCheckbox1.Checked;
            if (TD_complete)
                label1.Font = new Font(label1.Font.Name, label1.Font.SizeInPoints, FontStyle.Strikeout);
            else
                label1.Font = new Font(label1.Font.Name, label1.Font.SizeInPoints, FontStyle.Regular);
            IsCompleteClicked = true;
            UserControl_Event_method?.Invoke(this, e);
            IsCompleteClicked = false;
        }

        private void starCheckbox1_CheckedChanged(object sender, EventArgs e)
        {
            TD_important = starCheckbox1.Checked;
            IsImportantClicked = true;
            UserControl_Event_method?.Invoke(this, e);
            IsImportantClicked = false;
        }
    }
}

