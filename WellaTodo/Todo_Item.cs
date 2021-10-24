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
        public event UserControl_Event UserControl_Click;

        static readonly int TODO_ITEM_WIDTH = 180;
        static readonly int TODO_ITEM_HEIGHT = 35;

        static readonly Color PSEUDO_BACK_COLOR = Color.White;
        static readonly Color PSEUDO_HIGHLIGHT_COLOR = Color.LightCyan;
        static readonly Color PSEUDO_SELECTED_COLOR = Color.Cyan;
        static readonly Color PSEUDO_BORDER_COLOR = Color.LightGray;
        static readonly float PSEUDO_PEN_THICKNESS = 1.0f;

        static readonly float FONT_SIZE_TITLE = 9.0f;
        static readonly float FONT_SIZE_INFORMATION = 8.0f;

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

        public string TD_infomation { get; set; }
        public bool IsCompleteClicked { get; set; } = false;
        public bool IsImportantClicked { get; set; } = false;
        public bool IsDeleteClicked { get; set; } = false;
        private bool isItemSelected = false;
        public bool IsItemSelected 
        { 
            get => isItemSelected; 
            set 
            { 
                isItemSelected = value;
                ChangeItemColor();
            } 
        }

        RoundCheckbox roundCheckbox1 = new RoundCheckbox();
        StarCheckbox starCheckbox1 = new StarCheckbox();
        Label label2 = new Label();

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
            TD_infomation = "";
        }

        private void Todo_Item_Load(object sender, EventArgs e)
        {
            Size = new Size(TODO_ITEM_WIDTH, TODO_ITEM_HEIGHT);
            BackColor = PSEUDO_BACK_COLOR;

            roundCheckbox1.CheckedChanged += new EventHandler(roundCheckbox1_CheckedChanged);
            roundCheckbox1.MouseEnter += new EventHandler(roundCheckbox1_MouseEnter);
            roundCheckbox1.MouseLeave += new EventHandler(roundCheckbox1_MouseLeave);
            roundCheckbox1.MouseClick += new MouseEventHandler(roundCheckbox1_MouseClick);
            roundCheckbox1.Location = new Point(12, 4);
            roundCheckbox1.Size = new Size(25, 25);
            roundCheckbox1.BackColor = PSEUDO_BACK_COLOR;
            Controls.Add(roundCheckbox1);

            starCheckbox1.CheckedChanged += new EventHandler(starCheckbox1_CheckedChanged);
            starCheckbox1.MouseEnter += new EventHandler(starCheckbox1_MouseEnter); 
            starCheckbox1.MouseLeave += new EventHandler(starCheckbox1_MouseLeave);
            starCheckbox1.MouseClick += new MouseEventHandler(starCheckbox1_MouseClick);
            starCheckbox1.Location = new Point(Width - 40, 5);
            starCheckbox1.Size = new Size(25, 25);
            starCheckbox1.BackColor = PSEUDO_BACK_COLOR;
            Controls.Add(starCheckbox1);

            label1.Location = new Point(45, 13);
            label1.BackColor = PSEUDO_BACK_COLOR;
            if (TD_complete)
                label1.Font = new Font(label1.Font.Name, FONT_SIZE_TITLE, FontStyle.Strikeout);
            else
                label1.Font = new Font(label1.Font.Name, FONT_SIZE_TITLE, FontStyle.Regular);

            label2.MouseEnter += new EventHandler(label2_MouseEnter);
            label2.MouseLeave += new EventHandler(label2_MouseLeave);
            label2.MouseClick += new MouseEventHandler(label2_MouseClick);
            label2.Font = new Font(label2.Font.Name, FONT_SIZE_INFORMATION, FontStyle.Regular);
            label2.Location = new Point(245, 20);
            label2.BackColor = PSEUDO_BACK_COLOR;
            label2.Size = new Size(0, 13);
            label2.Text = "";
            Controls.Add(label2);
        }

        private void Todo_Item_Resize(object sender, EventArgs e)
        {
            starCheckbox1.Location = new Point(Width - 40, 5);
            SetPathOuterBorder();
        }

        private void Todo_Item_Paint(object sender, PaintEventArgs pevent)
        {
            if (TD_infomation.Length == 0)
            {
                label1.Location = new Point(45, 13);

                label2.Location = new Point(245, 20);
                label2.Size = new Size(0, 13);
                label2.Text = "";
                label2.AutoSize = false;
            }
            else
            {
                label1.Location = new Point(45, 5);

                label2.Location = new Point(45, 20);
                label2.Text = TD_infomation;
                label2.AutoSize = true;
            }

            starCheckbox1.Location = new Point(Width - 40, 5);

            Graphics g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            Rectangle rc = ClientRectangle;

            g.FillRectangle(new SolidBrush(BackColor), rc.Left - 1, rc.Top - 1, rc.Width + 1, rc.Height + 1);
            g.DrawRectangle(new Pen(PSEUDO_BORDER_COLOR, PSEUDO_PEN_THICKNESS), rc.Left, rc.Top, rc.Width-1, rc.Height-1);
            //g.DrawPath(new Pen(PSEUDO_BORDER_COLOR, PSEUDO_PEN_THICKNESS), outerBorderPath);
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
            if (UserControl_Click != null) UserControl_Click?.Invoke(this, e);
        }

        private void ChangeToBackColor()
        {
            BackColor = IsItemSelected ? PSEUDO_SELECTED_COLOR : PSEUDO_BACK_COLOR;
            roundCheckbox1.BackColor = IsItemSelected ? PSEUDO_SELECTED_COLOR : PSEUDO_BACK_COLOR;
            label1.BackColor = IsItemSelected ? PSEUDO_SELECTED_COLOR : PSEUDO_BACK_COLOR;
            label2.BackColor = IsItemSelected ? PSEUDO_SELECTED_COLOR : PSEUDO_BACK_COLOR;
            starCheckbox1.BackColor = IsItemSelected ? PSEUDO_SELECTED_COLOR : PSEUDO_BACK_COLOR;
        }

        private void ChangeToHighlightColor()
        {
            BackColor = IsItemSelected ? PSEUDO_SELECTED_COLOR : PSEUDO_HIGHLIGHT_COLOR;
            roundCheckbox1.BackColor = IsItemSelected ? PSEUDO_SELECTED_COLOR : PSEUDO_HIGHLIGHT_COLOR;
            label1.BackColor = IsItemSelected ? PSEUDO_SELECTED_COLOR : PSEUDO_HIGHLIGHT_COLOR;
            label2.BackColor = IsItemSelected ? PSEUDO_SELECTED_COLOR : PSEUDO_HIGHLIGHT_COLOR;
            starCheckbox1.BackColor = IsItemSelected ? PSEUDO_SELECTED_COLOR : PSEUDO_HIGHLIGHT_COLOR;
        }

        private void ChangeItemColor()
        {
            if (IsItemSelected)
            {
                BackColor = PSEUDO_SELECTED_COLOR;
                roundCheckbox1.BackColor = PSEUDO_SELECTED_COLOR;
                label1.BackColor = PSEUDO_SELECTED_COLOR;
                label2.BackColor = PSEUDO_SELECTED_COLOR;
                starCheckbox1.BackColor = PSEUDO_SELECTED_COLOR;
            }
            else
            {
                BackColor = PSEUDO_BACK_COLOR;
                roundCheckbox1.BackColor = PSEUDO_BACK_COLOR;
                label1.BackColor = PSEUDO_BACK_COLOR;
                label2.BackColor = PSEUDO_BACK_COLOR;
                starCheckbox1.BackColor = PSEUDO_BACK_COLOR;
            }
        }

        //---------------------------------------------------------
        // control event
        //---------------------------------------------------------
        private void Todo_Item_MouseEnter(object sender, EventArgs e)
        {
            ChangeToHighlightColor();
        }

        private void Todo_Item_MouseLeave(object sender, EventArgs e)
        {
            ChangeToBackColor();
        }

        private void label1_MouseEnter(object sender, EventArgs e)
        {
            ChangeToHighlightColor();
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            ChangeToBackColor();
        }

        private void label2_MouseEnter(object sender, EventArgs e)
        {
            ChangeToHighlightColor();
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            ChangeToBackColor();
        }

        private void Todo_Item_MouseClick(object sender, MouseEventArgs e)
        {
            Todo_Item_Click(sender, e);
        }

        private void label1_MouseClick(object sender, MouseEventArgs e)
        {
            Todo_Item_Click(sender, e);
        }

        private void label2_MouseClick(object sender, MouseEventArgs e)
        {
            Todo_Item_Click(sender, e);
        }

        private void roundCheckbox1_MouseClick(object sender, MouseEventArgs e)
        {
            IsCompleteClicked = true;
            Todo_Item_Click(sender, e);
            IsCompleteClicked = false;
        }

        private void roundCheckbox1_MouseEnter(object sender, EventArgs e)
        {
            ChangeToHighlightColor();
        }

        private void roundCheckbox1_MouseLeave(object sender, EventArgs e)
        {
            ChangeToBackColor();
        }

        private void roundCheckbox1_CheckedChanged(object sender, EventArgs e)
        {
            TD_complete = roundCheckbox1.Checked;
            if (TD_complete)
                label1.Font = new Font(label1.Font.Name, FONT_SIZE_TITLE, FontStyle.Strikeout);
            else
                label1.Font = new Font(label1.Font.Name, FONT_SIZE_TITLE, FontStyle.Regular);
        }

        private void starCheckbox1_MouseClick(object sender, MouseEventArgs e)
        {
            IsImportantClicked = true;
            Todo_Item_Click(sender, e);
            IsImportantClicked = false;
        }

        private void starCheckbox1_MouseEnter(object sender, EventArgs e)
        {
            ChangeToHighlightColor();
        }

        private void starCheckbox1_MouseLeave(object sender, EventArgs e)
        {
            ChangeToBackColor();
        }

        private void starCheckbox1_CheckedChanged(object sender, EventArgs e)
        {
            TD_important = starCheckbox1.Checked;
        }
    }
}

