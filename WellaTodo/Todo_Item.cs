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
            this.BackColor = PSEUDO_BACK_COLOR;
            label1.BackColor = PSEUDO_BACK_COLOR;

            SetControlLocation();

            SetOuterBorderPath(ClientRectangle, this.cornerRadius);
            SetHighlightPath(ClientRectangle, this.cornerRadius);
        }

        private void Todo_Item_Paint(object sender, PaintEventArgs e)
        {
            SetControlLocation();

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

            //Rectangle rectangle = ClientRectangle;
            //rectangle.X -= 1;
            //rectangle.Y -= 1;
            //rectangle.Width += 2;
            //rectangle.Height += 2;

            Rectangle rc = this.ClientRectangle;
            e.Graphics.FillRectangle(new SolidBrush(this.BackColor), rc.Left - 1, rc.Top - 1, rc.Width + 1, rc.Height + 1);

            DrawOuterBorder(e.Graphics);
            //DrawBackgroundImage(e.Graphics); 
            //DrawHighlight(e.Graphics); 
            //DrawImage(e.Graphics); 
            //DrawTitle(e.Graphics); 
            //DrawGlow(e.Graphics); 
            //DrawInnerBorder(e.Graphics); 
            //DrawCheckBox(e.Graphics);
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
            //SetInnerBorderPath(ClientRectangle, this.cornerRadius);
            SetHighlightPath(ClientRectangle, this.cornerRadius);
            //SetTitleRectangle(ClientRectangle);
        }

        private void SetControlLocation()
        {
            checkBox1.Location = new Point(20, checkBox2.Location.Y);
            label1.Location = new Point(45, checkBox2.Location.Y);
            checkBox2.Location = new Point(this.Width - 30, checkBox2.Location.Y);
        }

        public bool isCompleted()
        {
            return checkBox1.Checked;
        }

        public bool isImportant()
        {
            return checkBox2.Checked;
        }

        /// <summary>
        /// 라운드 테두리 그리기
        /// </summary>
        private void DrawOuterBorder(Graphics g)
        {
            g.DrawPath(this.outerBorderColorPen, this.outerBorderPath);
        }

        /// <summary>
        /// 하이라이트 그리기
        /// </summary>
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

        /// <summary>
        /// 라운드 테두리 경로 지정
        /// </summary>
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

        /// <summary>
        /// 라운드 테두리 경로 계산
        /// </summary>
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

        /*
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.container != null)
                {
                    this.container.Dispose();
                    this.outerBorderPath?.Dispose();
                    this.innerBorderPath?.Dispose();
                    this.highlightPath?.Dispose();
                    this.buttonColorPen2?.Dispose();
                    this.highlightColorPen?.Dispose();
                    this.buttonColorBrush1?.Dispose();
                }
            }
            base.Dispose(disposing);
        }
        */

        /// <summary>
        /// 항목 클릭 이벤트 처리
        /// </summary>
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
                        MenuItem deleteItem = new MenuItem("항목 삭제", new System.EventHandler(this.OnDeleteMenuItem_Click));
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

        private void checkBox1_MouseEnter(object sender, EventArgs e)
        {
            BackColor = IsItemSelected ? PSEUDO_SELECTED_COLOR : PSEUDO_HIGHLIGHT_COLOR;
            label1.BackColor = IsItemSelected ? PSEUDO_SELECTED_COLOR : PSEUDO_HIGHLIGHT_COLOR;
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

        /// <summary>
        /// 완료 체크시
        /// </summary>
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
        /// <summary>
        /// 중요 체크시
        /// </summary>
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            TD_important = checkBox2.Checked;
            IsImportantClicked = true;
            UserControl_Event_method?.Invoke(this, e);
            IsImportantClicked = false;
        }

        /// <summary>
        /// 할일 클릭시
        /// </summary>
        private void Todo_Item_MouseClick(object sender, MouseEventArgs e)
        {
            Todo_Item_Click(sender, e);
        }

        private void label1_MouseClick(object sender, MouseEventArgs e)
        {
            Todo_Item_Click(sender, e);
        }

        /// <summary>
        /// 할일 삭제시
        /// </summary>
        private void OnDeleteMenuItem_Click(object sender, EventArgs e)
        {
            IsDeleteClicked = true;
            UserControl_Event_method?.Invoke(this, e);
            IsDeleteClicked = false;
        }
    }
}

