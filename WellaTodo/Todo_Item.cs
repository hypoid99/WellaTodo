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
        public event TodoItemList_Event UserControl_Click;

        static readonly int TODO_ITEM_WIDTH = 180;
        static readonly int TODO_ITEM_HEIGHT = 32;
        static readonly int PRIMARY_LOCATION_X = 45;
        static readonly int PRIMARY_LOCATION_Y1 = 7;
        static readonly int PRIMARY_LOCATION_Y2 = 2;
        static readonly int SECONDARY_LOCATION_Y = 20;
        static readonly int ROUNDCHECK_LOCATION_X = 12;
        static readonly int ROUNDCHECK_LOCATION_Y = 2;
        static readonly int STARCHECK_LOCATION_Y = 3;

        static readonly Color PSEUDO_BACK_COLOR = Color.White;
        static readonly Color PSEUDO_HIGHLIGHT_COLOR = Color.LightCyan;
        static readonly Color PSEUDO_SELECTED_COLOR = Color.Cyan;
        static readonly Color PSEUDO_BORDER_COLOR = Color.LightGray;
        static readonly Color PSEUDO_FORE_TEXT_COLOR = Color.Black;
        static readonly Color PSEUDO_IMPORTANT_TEXT_COLOR = Color.Blue;
        static readonly Color PSEUDO_INFORMATION_TEXT_COLOR = Color.Gray;
        static readonly Color PSEUDO_COMPLETE_TEXT_COLOR = Color.Gray;
        static readonly float PSEUDO_PEN_THICKNESS = 1.0f;

        static readonly string FONT_NAME = "돋움";
        static readonly float FONT_SIZE_TITLE = 11.0f;
        static readonly float FONT_SIZE_INFORMATION = 8.0f;

        CDataCell m_DataCell;
        public CDataCell TD_DataCell { get => m_DataCell; set => m_DataCell = value; }

        public string TD_title
        {
            get => TD_DataCell.DC_title;
            set { TD_DataCell.DC_title = value; label1.Text = value; }
        }

        public bool TD_complete
        {
            get => TD_DataCell.DC_complete;
            set { TD_DataCell.DC_complete = value; roundCheckbox1.Checked = value; }
        }

        public bool TD_important
        {
            get => TD_DataCell.DC_important;
            set { TD_DataCell.DC_important = value; starCheckbox1.Checked = value; }
        }

        public string TD_infomation { get; set; }

        public bool IsCompleteClicked { get; set; } = false;
        public bool IsImportantClicked { get; set; } = false;

        private bool isItemSelected = false;
        public bool IsItemSelected 
        { 
            get => isItemSelected; 
            set { isItemSelected = value; ChangeItemSelectedColor(); } 
        }

        public int GetItemHeight => TODO_ITEM_HEIGHT;

        Label label1 = new Label();
        Label label2 = new Label();
        RoundCheckbox roundCheckbox1 = new RoundCheckbox();
        StarCheckbox starCheckbox1 = new StarCheckbox();
        PictureBox pictureBox_Bulletin = new PictureBox();
        PictureBox pictureBox_Notepad = new PictureBox();
        
        GraphicsPath outerBorderPath = null;
        int cornerRadius = 10;

        bool isDragging = false;
        Point DragStartPoint;

        public Todo_Item(CDataCell dc)
        {
            InitializeComponent();

            TD_DataCell = dc;
            TD_infomation = "";
        }

        // --------------------------------------------------
        // Form 이벤트
        // --------------------------------------------------
        private void Todo_Item_Load(object sender, EventArgs e)
        {
            Initiate_View();
        }

        private void Todo_Item_Resize(object sender, EventArgs e)
        {
            starCheckbox1.Location = new Point(Width - 40, STARCHECK_LOCATION_Y);
        }

        private void Todo_Item_Paint(object sender, PaintEventArgs pevent)
        {
            Update_Display();
            
            Graphics g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            Rectangle rc = ClientRectangle;

            int x1 = rc.Left;
            int y1 = rc.Top;
            int x2 = rc.Left + rc.Width - 1;
            int y2 = rc.Top + rc.Height - 1;
            g.FillRectangle(new SolidBrush(BackColor), x1 - 1, y1 - 1, rc.Width + 1, rc.Height + 1);
            g.DrawLine(new Pen(PSEUDO_BORDER_COLOR, PSEUDO_PEN_THICKNESS), x1, y2, x2, y2);
        }

        //--------------------------------------------------------------
        // 초기화 및 Update Display
        //--------------------------------------------------------------
        private void Initiate_View()
        {
            Size = new Size(TODO_ITEM_WIDTH, TODO_ITEM_HEIGHT);
            BackColor = PSEUDO_BACK_COLOR;
            Margin = new Padding(3, 1, 3, 1);

            AllowDrop = true;
            DragEnter += new DragEventHandler(Todo_Item_DragEnter);
            DragOver += new DragEventHandler(Todo_Item_DragOver);
            DragLeave += new EventHandler(Todo_Item_DragLeave);

            roundCheckbox1.MouseEnter += new EventHandler(Todo_Item_MouseEnter);
            roundCheckbox1.MouseLeave += new EventHandler(Todo_Item_MouseLeave);
            roundCheckbox1.MouseClick += new MouseEventHandler(roundCheckbox1_MouseClick);
            roundCheckbox1.Location = new Point(ROUNDCHECK_LOCATION_X, ROUNDCHECK_LOCATION_Y);
            roundCheckbox1.Size = new Size(25, 25);
            roundCheckbox1.BackColor = PSEUDO_BACK_COLOR;
            Controls.Add(roundCheckbox1);

            starCheckbox1.MouseEnter += new EventHandler(Todo_Item_MouseEnter);
            starCheckbox1.MouseLeave += new EventHandler(Todo_Item_MouseLeave);
            starCheckbox1.MouseClick += new MouseEventHandler(starCheckbox1_MouseClick);
            starCheckbox1.Location = new Point(Width - 40, STARCHECK_LOCATION_Y);
            starCheckbox1.Size = new Size(25, 25);
            starCheckbox1.BackColor = PSEUDO_BACK_COLOR;
            Controls.Add(starCheckbox1);

            label1.MouseEnter += new EventHandler(Todo_Item_MouseEnter);
            label1.MouseLeave += new EventHandler(Todo_Item_MouseLeave);
            label1.MouseClick += new MouseEventHandler(Todo_Item_MouseClick);
            label1.MouseDown += new MouseEventHandler(Todo_Item_MouseDown);
            label1.MouseMove += new MouseEventHandler(Todo_Item_MouseMove);
            label1.Font = new Font(FONT_NAME, FONT_SIZE_TITLE, FontStyle.Regular);
            label1.Location = new Point(PRIMARY_LOCATION_X, PRIMARY_LOCATION_Y1);
            label1.ForeColor = PSEUDO_FORE_TEXT_COLOR;
            label1.BackColor = PSEUDO_BACK_COLOR;
            label1.AutoSize = true;
            label1.Size = new Size(77, 15);
            Controls.Add(label1);

            label2.MouseEnter += new EventHandler(Todo_Item_MouseEnter);
            label2.MouseLeave += new EventHandler(Todo_Item_MouseLeave);
            label2.MouseClick += new MouseEventHandler(Todo_Item_MouseClick);
            label2.MouseDown += new MouseEventHandler(Todo_Item_MouseDown);
            label2.MouseMove += new MouseEventHandler(Todo_Item_MouseMove);
            label2.Font = new Font(FONT_NAME, FONT_SIZE_INFORMATION, FontStyle.Regular);
            label2.BackColor = PSEUDO_BACK_COLOR;
            label2.ForeColor = PSEUDO_INFORMATION_TEXT_COLOR;
            Controls.Add(label2);

            pictureBox_Bulletin.BackColor = Color.Transparent;
            pictureBox_Bulletin.BackgroundImage = Properties.Resources.outline_note_alt_black_24dp;
            pictureBox_Bulletin.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBox_Bulletin.Location = new Point(ROUNDCHECK_LOCATION_X, ROUNDCHECK_LOCATION_Y);
            pictureBox_Bulletin.Margin = new Padding(0);
            pictureBox_Bulletin.Size = new Size(28, 26);
            Controls.Add(pictureBox_Bulletin);

            pictureBox_Notepad.BackColor = Color.Transparent;
            pictureBox_Notepad.BackgroundImage = Properties.Resources.outline_edit_note_black_24dp;
            pictureBox_Notepad.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBox_Notepad.Location = new Point(ROUNDCHECK_LOCATION_X, ROUNDCHECK_LOCATION_Y);
            pictureBox_Notepad.Margin = new Padding(0);
            pictureBox_Notepad.Size = new Size(28, 26);
            Controls.Add(pictureBox_Notepad);
        }

        private void Update_Display()
        {
            label1.Text = TD_DataCell.DC_title;
            roundCheckbox1.Checked = TD_DataCell.DC_complete;
            starCheckbox1.Checked = TD_DataCell.DC_important;

            if (TD_DataCell.DC_bulletin)
            {
                roundCheckbox1.Visible = false;
                starCheckbox1.Visible = false;
                pictureBox_Bulletin.Visible = true;
                pictureBox_Notepad.Visible = false;
            }
            else if (TD_DataCell.DC_notepad)
            {
                roundCheckbox1.Visible = false;
                starCheckbox1.Visible = false;
                pictureBox_Bulletin.Visible = false;
                pictureBox_Notepad.Visible = true;
            }
            else
            {
                roundCheckbox1.Visible = true;
                starCheckbox1.Visible = true;
                pictureBox_Bulletin.Visible = false;
                pictureBox_Notepad.Visible = false;
            }

            if (TD_infomation.Length == 0) // 가운데
            {
                label1.Location = new Point(PRIMARY_LOCATION_X, PRIMARY_LOCATION_Y1);
                label2.Location = new Point(PRIMARY_LOCATION_X, SECONDARY_LOCATION_Y);
                label2.Visible = false;
                label2.Text = "";
                label2.AutoSize = false;
            }
            else
            {
                label1.Location = new Point(PRIMARY_LOCATION_X, PRIMARY_LOCATION_Y2);  // 윗쪽
                label2.Location = new Point(PRIMARY_LOCATION_X, SECONDARY_LOCATION_Y);
                label2.Visible = true;
                label2.Text = TD_infomation;
                label2.AutoSize = true;
            }

            if (TD_DataCell.DC_complete)
            {
                label1.Font = new Font(FONT_NAME, FONT_SIZE_TITLE, FontStyle.Strikeout);
                label1.ForeColor = PSEUDO_COMPLETE_TEXT_COLOR;
                label2.ForeColor = PSEUDO_COMPLETE_TEXT_COLOR;

            }
            else if (TD_DataCell.DC_important)
            {
                label1.Font = new Font(FONT_NAME, FONT_SIZE_TITLE, FontStyle.Bold);
                label1.ForeColor = PSEUDO_IMPORTANT_TEXT_COLOR;
                label2.ForeColor = PSEUDO_INFORMATION_TEXT_COLOR;
            }
            else
            {
                label1.Font = new Font(FONT_NAME, FONT_SIZE_TITLE, FontStyle.Regular);
                label1.ForeColor = PSEUDO_FORE_TEXT_COLOR;
                label2.ForeColor = PSEUDO_INFORMATION_TEXT_COLOR;
            }

            starCheckbox1.Location = new Point(Width - PRIMARY_LOCATION_X, STARCHECK_LOCATION_Y);
        }

        // --------------------------------------------
        // 헬프 메서드
        // --------------------------------------------
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

        private void ChangeItemSelectedColor()
        {
            BackColor = IsItemSelected ? PSEUDO_SELECTED_COLOR : PSEUDO_BACK_COLOR;
            foreach (Control c in Controls) c.BackColor = IsItemSelected ? PSEUDO_SELECTED_COLOR : PSEUDO_BACK_COLOR;
        }

        public void Display_Event_Status()
        {
            if (this.UserControl_Click != null)
            {
                foreach (Delegate d in UserControl_Click.GetInvocationList())
                {
                    Console.WriteLine(TD_DataCell.DC_title + "-" + d.Method.ToString());
                }
            }
        }

        //---------------------------------------------------------
        // 사용자 이벤트 처리 - Control Event
        //---------------------------------------------------------
        private void Todo_Item_Click(object sender, MouseEventArgs e)
        {
            Focus();
            if (UserControl_Click != null) UserControl_Click?.Invoke(this, e);
        }

        private void Todo_Item_MouseClick(object sender, MouseEventArgs e)
        {
            Todo_Item_Click(sender, e);
        }

        private void Todo_Item_MouseEnter(object sender, EventArgs e)
        {
            BackColor = IsItemSelected ? PSEUDO_SELECTED_COLOR : PSEUDO_HIGHLIGHT_COLOR;
            foreach (Control c in Controls) c.BackColor = IsItemSelected ? PSEUDO_SELECTED_COLOR : PSEUDO_HIGHLIGHT_COLOR;
            if (sender is StarCheckbox)
            {
                Cursor = Cursors.Hand;
            }
            if (sender is RoundCheckbox)
            {
                Cursor = Cursors.Hand;
            }
        }

        private void Todo_Item_MouseLeave(object sender, EventArgs e)
        {
            BackColor = IsItemSelected ? PSEUDO_SELECTED_COLOR : PSEUDO_BACK_COLOR;
            foreach (Control c in Controls) c.BackColor = IsItemSelected ? PSEUDO_SELECTED_COLOR : PSEUDO_BACK_COLOR;
            Cursor = Cursors.Default;
        }

        private void roundCheckbox1_MouseClick(object sender, MouseEventArgs e)
        {
            TD_DataCell.DC_complete = roundCheckbox1.Checked;

            if (TD_DataCell.DC_complete)
            {
                label1.Font = new Font(FONT_NAME, FONT_SIZE_TITLE, FontStyle.Strikeout);
                label1.ForeColor = PSEUDO_COMPLETE_TEXT_COLOR;
                label2.ForeColor = PSEUDO_COMPLETE_TEXT_COLOR;
            }
            else
            {
                label1.Font = new Font(FONT_NAME, FONT_SIZE_TITLE, FontStyle.Regular);
                label1.ForeColor = PSEUDO_FORE_TEXT_COLOR;
                label2.ForeColor = PSEUDO_INFORMATION_TEXT_COLOR;
            }

            IsCompleteClicked = true;
            Todo_Item_Click(sender, e);
        }

        private void starCheckbox1_MouseClick(object sender, MouseEventArgs e)
        {
            TD_DataCell.DC_important = starCheckbox1.Checked;

            IsImportantClicked = true;
            Todo_Item_Click(sender, e);
        }

        // ---------------------------------------------------------------------------
        // 드래그앤드롭 Drag & Drop - Source
        // ---------------------------------------------------------------------------
        private void Todo_Item_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = false;
            DragStartPoint = PointToScreen(new Point(e.X, e.Y));
        }

        private void Todo_Item_MouseUp(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                //Console.WriteLine("Todo_Item_MouseUp - DragDrop");
            }
            else
            {
                //Console.WriteLine("Todo_Item_MouseUp - Click");
            }
            isDragging = false;
        }

        private void Todo_Item_MouseMove(object sender, MouseEventArgs e)
        {
            int threshold = 10;
            int deltaX;
            int deltaY;
            Point DragCurrentPoint = PointToScreen(new Point(e.X, e.Y));
            deltaX = Math.Abs(DragCurrentPoint.X - DragStartPoint.X);
            deltaY = Math.Abs(DragCurrentPoint.Y - DragStartPoint.Y);
            if (!isDragging)
            {
                if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
                {
                    if ((deltaX < threshold) && (deltaY < threshold))
                    {
                        //Console.WriteLine("Todo_Item_MouseMove -> DoDragDrop");
                        Todo_Item_Click(sender, e);  // click
                        DoDragDrop(this, DragDropEffects.All);
                        isDragging = true;
                        return;
                    }
                }
            }
        }

        private void Todo_Item_QueryContinueDrag(object sender, QueryContinueDragEventArgs e)
        {
            //Console.WriteLine("Todo_Item_QueryContinueDrag");
        }

        private void Todo_Item_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            //Console.WriteLine("Todo_Item_GiveFeedback");
        }

        private void Todo_Item_DragEnter(object sender, DragEventArgs e)
        {
            //Console.WriteLine("Todo_Item_DragEnter");
            BackColor = IsItemSelected ? PSEUDO_SELECTED_COLOR : PSEUDO_HIGHLIGHT_COLOR;
            foreach (Control c in Controls) c.BackColor = IsItemSelected ? PSEUDO_SELECTED_COLOR : PSEUDO_HIGHLIGHT_COLOR;
        }

        private void Todo_Item_DragOver(object sender, DragEventArgs e)
        {
            //Console.WriteLine("Todo_Item_DragOver");
        }

        private void Todo_Item_DragLeave(object sender, EventArgs e)
        {
            //Console.WriteLine("Todo_Item_DragLeave");
            BackColor = IsItemSelected ? PSEUDO_SELECTED_COLOR : PSEUDO_BACK_COLOR;
            foreach (Control c in Controls) c.BackColor = IsItemSelected ? PSEUDO_SELECTED_COLOR : PSEUDO_BACK_COLOR;
        }
    }
}

