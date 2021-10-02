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
                    label1.BackColor = PSEUDO_SELECTED_COLOR;
                } 
                else
                {
                    BackColor = PSEUDO_BACK_COLOR;
                    label1.BackColor = PSEUDO_BACK_COLOR;
                }
            } 
        }

        Pen outerBorderColorPen = new Pen(Color.LightGray);
        GraphicsPath outerBorderPath = null;
        GraphicsPath highlightPath = null;
        Color highlightColor = Color.Yellow;
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

            /*
            checkBox1.Appearance = Appearance.Button;
            checkBox1.BackgroundImage = Properties.Resources.uncheck_Round;
            checkBox1.BackgroundImageLayout = ImageLayout.None;
            checkBox1.FlatAppearance.BorderSize = 0;
            checkBox1.FlatAppearance.CheckedBackColor = Color.Transparent;
            checkBox1.FlatAppearance.MouseDownBackColor = Color.Transparent;
            checkBox1.FlatAppearance.MouseOverBackColor = Color.Transparent;
            checkBox1.FlatStyle = FlatStyle.Flat;
            checkBox1.AutoSize = false;
            checkBox1.Size = new Size(24, 24);
            */

            label1.BackColor = PSEUDO_BACK_COLOR;

            SetOuterBorderPath(ClientRectangle, this.cornerRadius);
            SetHighlightPath(ClientRectangle, this.cornerRadius);

            SetControlLocation();
        }

        private void Todo_Item_Paint(object sender, PaintEventArgs e)
        {
            /*
            if (checkBox1.Checked)
            {
                checkBox1.BackgroundImage = Properties.Resources.check_Round;
            }
            else
            {
                checkBox1.BackgroundImage = Properties.Resources.uncheck_Round;
            }
            */
            SetControlLocation();

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

            Rectangle rc = this.ClientRectangle;
            e.Graphics.FillRectangle(new SolidBrush(this.BackColor), rc.Left - 1, rc.Top - 1, rc.Width + 1, rc.Height + 1);

            DrawOuterBorder(e.Graphics);
        }

        private void Todo_Item_Resize(object sender, EventArgs e)
        {
            SetControlLocation();

            Rectangle rectangle = ClientRectangle;
            rectangle.X -= 1;
            rectangle.Y -= 1;
            rectangle.Width += 2;
            rectangle.Height += 2;

            using(GraphicsPath graphicsPath = GetShapePath(rectangle,this.cornerRadius,this.cornerRadius,this.cornerRadius,this.cornerRadius))
            {
                Region = new Region(graphicsPath);
            }

            SetOuterBorderPath(ClientRectangle, this.cornerRadius);
            SetHighlightPath(ClientRectangle, this.cornerRadius);
        }

        private void SetControlLocation()
        {
            checkBox1.Location = new Point(20, 10);
            label1.Location = new Point(45, 10);
            checkBox2.Location = new Point(Width - 30, 10);
        }

        public bool isCompleted()
        {
            return checkBox1.Checked;
        }

        public bool isImportant()
        {
            return checkBox2.Checked;
        }

        private void DrawOuterBorder(Graphics g)
        {
            g.DrawPath(this.outerBorderColorPen, this.outerBorderPath);
        }

        private void DrawHighlight(Graphics graphics)
        {
            if (!Enabled) return;

            //if (this.buttonStyle == CheckButtonStyle.Flat && this.buttonState == CheckButtonState.None) return;
            //int alpha = (buttonState == CheckButtonState.Pressed) ? 60 : 150;
            int alpha = 60;

            LinearGradientBrush highlightColorBrush = new LinearGradientBrush
            (
                this.highlightPath.GetBounds(),
                Color.FromArgb(alpha, this.highlightColor),
                Color.FromArgb(alpha / 3, this.highlightColor),
                LinearGradientMode.Vertical
            );

            graphics.FillPath(highlightColorBrush, this.highlightPath);
        }

        private void SetOuterBorderPath(Rectangle clientRectangle, float cornerRadius)
        {
            Rectangle rectangle = clientRectangle;

            rectangle.Width -= 1;
            rectangle.Height -= 1;

            GraphicsPath outerBorderPath = GetShapePath(rectangle, cornerRadius, cornerRadius, cornerRadius, cornerRadius);

            this.outerBorderPath?.Dispose();
            this.outerBorderPath = outerBorderPath;
        }

        private void SetHighlightPath(Rectangle clientRectangle, int cornerRadius)
        {
            Rectangle rectangle = new Rectangle(0, clientRectangle.Height / 2, clientRectangle.Width, clientRectangle.Height);

            GraphicsPath highlightPath = GetShapePath(rectangle, 0, 0, cornerRadius, cornerRadius);

            this.highlightPath?.Dispose();
            this.highlightPath = highlightPath;
        }

        private GraphicsPath GetShapePath(RectangleF rectangle, float r1, float r2, float r3, float r4)
        {
            float x = rectangle.X;
            float y = rectangle.Y;
            float width = rectangle.Width;
            float height = rectangle.Height;

            GraphicsPath graphicsPath = new GraphicsPath();

            graphicsPath.AddBezier(x, y + r1, x, y, x + r1, y, x + r1, y);
            graphicsPath.AddLine(x + r1, y, x + width - r2, y);
            graphicsPath.AddBezier(x + width - r2, y, x + width, y, x + width, y + r2, x + width, y + r2);
            graphicsPath.AddLine(x + width, y + r2, x + width, y + height - r3);
            graphicsPath.AddBezier(x + width,y + height - r3,x + width,y + height,x + width - r3,y + height,x + width - r3,y + height);
            graphicsPath.AddLine(x + width - r3, y + height, x + r4, y + height);
            graphicsPath.AddBezier(x + r4, y + height, x, y + height, x, y + height - r4, x, y + height - r4);
            graphicsPath.AddLine(x, y + height - r4, x, y + r1);

            return graphicsPath;
        }

        private void Todo_Item_Click(object sender, MouseEventArgs e)
        {
            SetControlLocation();
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
            label1.BackColor = IsItemSelected ? PSEUDO_SELECTED_COLOR : PSEUDO_HIGHLIGHT_COLOR;
        }

        private void Todo_Item_MouseLeave(object sender, EventArgs e)
        {
            BackColor = IsItemSelected ? PSEUDO_SELECTED_COLOR : PSEUDO_BACK_COLOR;
            label1.BackColor = IsItemSelected ? PSEUDO_SELECTED_COLOR : PSEUDO_BACK_COLOR;
        }

        private void label1_MouseEnter(object sender, EventArgs e)
        {
            BackColor = IsItemSelected ? PSEUDO_SELECTED_COLOR : PSEUDO_HIGHLIGHT_COLOR;
            label1.BackColor = IsItemSelected ? PSEUDO_SELECTED_COLOR : PSEUDO_HIGHLIGHT_COLOR;
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            BackColor = IsItemSelected ? PSEUDO_SELECTED_COLOR : PSEUDO_BACK_COLOR;
            label1.BackColor = IsItemSelected ? PSEUDO_SELECTED_COLOR : PSEUDO_BACK_COLOR;
        }

        /*
        private void checkBox1_MouseEnter(object sender, EventArgs e)
        {
            BackColor = IsItemSelected ? PSEUDO_SELECTED_COLOR : PSEUDO_HIGHLIGHT_COLOR;
            label1.BackColor = IsItemSelected ? PSEUDO_SELECTED_COLOR : PSEUDO_HIGHLIGHT_COLOR;
            Console.WriteLine("mouse enter");
        }

        private void checkBox1_MouseLeave(object sender, EventArgs e)
        {
            BackColor = IsItemSelected ? PSEUDO_SELECTED_COLOR : PSEUDO_BACK_COLOR;
            label1.BackColor = IsItemSelected ? PSEUDO_SELECTED_COLOR : PSEUDO_BACK_COLOR;
        }

        private void checkBox2_MouseEnter(object sender, EventArgs e)
        {
            BackColor = IsItemSelected ? PSEUDO_SELECTED_COLOR : PSEUDO_HIGHLIGHT_COLOR;
            label1.BackColor = IsItemSelected ? PSEUDO_SELECTED_COLOR : PSEUDO_HIGHLIGHT_COLOR;
        }

        private void checkBox2_MouseLeave(object sender, EventArgs e)
        {
            BackColor = IsItemSelected ? PSEUDO_SELECTED_COLOR : PSEUDO_BACK_COLOR;
            label1.BackColor = IsItemSelected ? PSEUDO_SELECTED_COLOR : PSEUDO_BACK_COLOR;
        }
        */

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            /*
            if (checkBox1.Checked)
            {
                checkBox1.BackgroundImage = Properties.Resources.check_Round;
            }
            else
            {
                checkBox1.BackgroundImage = Properties.Resources.uncheck_Round;
            }
            */

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

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            TD_important = checkBox2.Checked;
            IsImportantClicked = true;
            UserControl_Event_method?.Invoke(this, e);
            IsImportantClicked = false;
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
    }
}

