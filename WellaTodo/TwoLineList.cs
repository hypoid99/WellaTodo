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
    public delegate void TwoLineList_Event(object sender, EventArgs e);

    public partial class TwoLineList : UserControl
    {
        public event TwoLineList_Event TwoLineList_Click;
        public event TwoLineList_Event TwoLineList_DoubleClick;

        static readonly int LIST_WIDTH = 250;
        static readonly int LIST_HEIGHT = 32;
        static readonly int PRIMARY_LOCATION_Y1 = 8;
        static readonly int PRIMARY_LOCATION_Y2 = 1;
        static readonly int SECONDARY_LOCATION_Y = 20;
        static readonly int METADATA_LOCATION_Y = 3;

        static readonly Color BACK_COLOR = Color.White;
        static readonly Color HIGHLIGHT_COLOR = Color.LightCyan;
        static readonly Color SELECTED_COLOR = Color.Cyan;
        static readonly Color BORDER_COLOR = Color.LightGray;

        static readonly string FONT_NAME = "돋움";
        static readonly float FONT_SIZE_PRIMARY = 11.0f;
        static readonly float FONT_SIZE_SECONDARY = 8.0f;
        static readonly float FONT_SIZE_METADATA = 8.0f;

        private TextBox textBox_Rename;

        bool isDragging = false;
        Point DragStartPoint;

        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
        public Image IconImage
        {
            get => pictureBox_Icon.BackgroundImage;
            set => pictureBox_Icon.BackgroundImage = value;
        }

        public string PrimaryText
        {
            get => label_PrimaryText.Text;
            set => label_PrimaryText.Text = value;
        }

        public string SecondaryText
        {
            get => label_SecondaryText.Text;
            set => label_SecondaryText.Text = value;
        }

        public string MetadataText
        {
            get => label_Metadata.Text;
            set => label_Metadata.Text = value;
        }

        private bool isDivider = false;
        public bool IsDivider
        {
            get => isDivider;
            set => isDivider = value;
        }

        private bool isSelected = false;
        public bool IsSelected
        {
            get => isSelected;
            set 
            { 
                isSelected = value;
                BackColor = IsSelected ? SELECTED_COLOR : BACK_COLOR;
                foreach (Control c in Controls) c.BackColor = IsSelected ? SELECTED_COLOR : BACK_COLOR;
            }
        }

        private string primaryText_Renamed;
        public string PrimaryText_Renamed 
        { 
            get => primaryText_Renamed; 
            set => primaryText_Renamed = value; 
        }

        bool isTextboxClicked = false;
        public bool IsTextboxClicked 
        { 
            get => isTextboxClicked; 
            set => isTextboxClicked = value; 
        }

        // --------------------------------------------------
        // Constructor
        // --------------------------------------------------
        public TwoLineList()
        {
            InitializeComponent();

            IconImage = null;
            PrimaryText = "제목없음";
            SecondaryText = "";
            MetadataText = "";
            isDivider = true;

            PrimaryText_Renamed = PrimaryText;

            Initialize();
        }

        public TwoLineList(Image iconImage, string primaryText, string secondaryText, string metadataText)
        {
            InitializeComponent();

            IconImage = iconImage;
            PrimaryText = primaryText;
            SecondaryText = secondaryText;
            MetadataText = metadataText;
            isDivider = false;

            PrimaryText_Renamed = PrimaryText;

            Initialize();
        }

        // --------------------------------------------------
        // Form 이벤트
        // --------------------------------------------------
        private void TwoLineList_Paint(object sender, PaintEventArgs pevent)
        {
            if (isDivider)
            {
                Graphics g = pevent.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                Rectangle rc = ClientRectangle;
                int x1 = rc.Left;
                int y1 = rc.Top;
                int x2 = rc.Left + rc.Width - 1;
                int y2 = rc.Top + rc.Height - 1;
                g.DrawLine(new Pen(BORDER_COLOR, 1.0f), x1, y1, x2, y1);
                g.DrawLine(new Pen(BORDER_COLOR, 1.0f), x1, y2, x2, y2);
                return;
            }

            if (SecondaryText.Length == 0)
            {
                label_PrimaryText.Location = new Point(30, PRIMARY_LOCATION_Y1);

                label_SecondaryText.Location = new Point(245, SECONDARY_LOCATION_Y);
                label_SecondaryText.Size = new Size(0, 13);
                label_SecondaryText.Text = "";
                label_SecondaryText.AutoSize = false;
            }
            else
            {
                label_PrimaryText.Location = new Point(30, PRIMARY_LOCATION_Y2);

                label_SecondaryText.Location = new Point(30, SECONDARY_LOCATION_Y);
                label_SecondaryText.Text = SecondaryText;
                label_SecondaryText.AutoSize = true;
            }

            label_Metadata.Location = new Point(Width - 35, METADATA_LOCATION_Y);
        }

        //--------------------------------------------------------------
        // 초기화 및 Update Display
        //--------------------------------------------------------------
        private void Initialize()
        {
            if (isDivider)
            {
                Size = new Size(LIST_WIDTH, 2);
                Margin = new Padding(0);
                BackColor = BACK_COLOR;

                pictureBox_Icon.Visible = false;
                label_PrimaryText.Visible = false;
                label_SecondaryText.Visible = false;
                label_Metadata.Visible = false;

                return;
            }

            Size = new Size(LIST_WIDTH, LIST_HEIGHT);
            Margin = new Padding(1);
            BackColor = BACK_COLOR;

            AllowDrop = true;
            DragEnter += new DragEventHandler(List_DragEnter);
            DragOver += new DragEventHandler(List_DragOver);
            DragLeave += new EventHandler(List_DragLeave);
            MouseEnter += new EventHandler(List_MouseEnter);
            MouseLeave += new EventHandler(List_MouseLeave);
            MouseClick += new MouseEventHandler(List_MouseClick);
            MouseDown += new MouseEventHandler(List_MouseDown);
            MouseMove += new MouseEventHandler(List_MouseMove);
            MouseUp += new MouseEventHandler(List_MouseUp);
            MouseDoubleClick += new MouseEventHandler(List_MouseDoubleClick);

            pictureBox_Icon.Size = new Size(24, 24);
            pictureBox_Icon.Location = new Point(5, 4);

            label_PrimaryText.MouseEnter += new EventHandler(List_MouseEnter);
            label_PrimaryText.MouseLeave += new EventHandler(List_MouseLeave);
            label_PrimaryText.MouseClick += new MouseEventHandler(List_MouseClick);
            label_PrimaryText.MouseDown += new MouseEventHandler(List_MouseDown);
            label_PrimaryText.MouseMove += new MouseEventHandler(List_MouseMove);
            label_PrimaryText.MouseUp += new MouseEventHandler(List_MouseUp);
            label_PrimaryText.MouseDoubleClick += new MouseEventHandler(List_MouseDoubleClick);
            label_PrimaryText.Font = new Font(FONT_NAME, FONT_SIZE_PRIMARY, FontStyle.Regular);
            label_PrimaryText.Location = new Point(30, PRIMARY_LOCATION_Y1);
            label_PrimaryText.BackColor = BACK_COLOR;

            label_SecondaryText.MouseEnter += new EventHandler(List_MouseEnter);
            label_SecondaryText.MouseLeave += new EventHandler(List_MouseLeave);
            label_SecondaryText.MouseClick += new MouseEventHandler(List_MouseClick);
            label_SecondaryText.MouseDown += new MouseEventHandler(List_MouseDown);
            label_SecondaryText.MouseMove += new MouseEventHandler(List_MouseMove);
            label_SecondaryText.MouseUp += new MouseEventHandler(List_MouseUp);
            label_SecondaryText.MouseDoubleClick += new MouseEventHandler(List_MouseDoubleClick);
            label_SecondaryText.Font = new Font(FONT_NAME, FONT_SIZE_SECONDARY, FontStyle.Regular);
            label_SecondaryText.Location = new Point(30, SECONDARY_LOCATION_Y);
            label_SecondaryText.BackColor = BACK_COLOR;

            label_Metadata.AutoSize = false;
            label_Metadata.Font = new Font(FONT_NAME, FONT_SIZE_METADATA, FontStyle.Regular);
            label_Metadata.Location = new Point(Width - 35, METADATA_LOCATION_Y);
            label_Metadata.Size = new Size(30, 30);
            label_Metadata.TextAlign = ContentAlignment.MiddleRight;
            label_Metadata.BackColor = BACK_COLOR;

            textBox_Rename = new TextBox();
            textBox_Rename.Enter += new EventHandler(textBox_Rename_Enter);
            textBox_Rename.KeyDown += new KeyEventHandler(textBox_Rename_KeyDown);
            textBox_Rename.KeyUp += new KeyEventHandler(textBox_Rename_KeyUp);
            textBox_Rename.Leave += new EventHandler(textBox_Rename_Leave);
            textBox_Rename.Visible = false;
            textBox_Rename.Location = new Point(30, PRIMARY_LOCATION_Y1);
            Controls.Add(textBox_Rename);
        }

        //---------------------------------------------------------
        // Textbox Rename
        //---------------------------------------------------------
        public void Rename_Process()
        {
            textBox_Rename.Visible = true;
            label_PrimaryText.Visible = false;
            textBox_Rename.Text = PrimaryText;
            textBox_Rename.Focus();
        }

        private void List_Rename()
        {
            textBox_Rename.Visible = false;
            label_PrimaryText.Visible = true;

            // Change PrimaryText
            if (PrimaryText_Renamed == textBox_Rename.Text)
            {
                //Console.WriteLine("PrimaryText_Renamed == textBox_Rename.Text");
                return;
            }
            PrimaryText_Renamed = textBox_Rename.Text;

            MouseEventArgs me = new MouseEventArgs(MouseButtons.Middle, 1, 42, 42, 1);
            if (TwoLineList_Click != null) TwoLineList_Click?.Invoke(this, me);
        }

        private void textBox_Rename_Enter(object sender, EventArgs e)
        {
            IsTextboxClicked = true;
        }

        private void textBox_Rename_Leave(object sender, EventArgs e)
        {
            List_Rename();
            IsTextboxClicked = false;
        }

        private void textBox_Rename_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                textBox_Rename.Visible = false;
                label_PrimaryText.Visible = true;
                return;
            }

            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void textBox_Rename_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.Handled = false;
            e.SuppressKeyPress = false;

            List_Rename();
            IsTextboxClicked = false;
        }

        //---------------------------------------------------------
        // 사용자 이벤트 처리 & 드래그&드롭
        //---------------------------------------------------------
        private void Mouse_Clicked(object sender, MouseEventArgs e)
        {
            Focus();
            Console.WriteLine("Mouse_Clicked");
            if (TwoLineList_Click != null) TwoLineList_Click?.Invoke(this, e);
        }

        private void Mouse_DoubleClicked(object sender, MouseEventArgs e)
        {
            Console.WriteLine("Mouse_DoubleClicked");
            if (TwoLineList_DoubleClick != null) TwoLineList_DoubleClick?.Invoke(this, e);
        }

        private void List_MouseEnter(object sender, EventArgs e)
        {
            BackColor = IsSelected ? SELECTED_COLOR : HIGHLIGHT_COLOR;
            foreach (Control c in Controls) c.BackColor = IsSelected ? SELECTED_COLOR : HIGHLIGHT_COLOR;
        }

        private void List_MouseLeave(object sender, EventArgs e)
        {
            BackColor = IsSelected ? SELECTED_COLOR : BACK_COLOR;
            foreach (Control c in Controls) c.BackColor = IsSelected ? SELECTED_COLOR : BACK_COLOR;
        }

        private void List_MouseClick(object sender, MouseEventArgs e)
        {
            //Mouse_Clicked(sender, e);
        }

        private void List_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Mouse_DoubleClicked(sender, e);
        }

        private void List_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = false;
            DragStartPoint = PointToScreen(new Point(e.X, e.Y));
        }

        private void List_MouseUp(object sender, MouseEventArgs e)
        {
            if (!isDragging)
            {
                Mouse_Clicked(sender, e);
            }
            //isDragging = false;
        }

        private void List_MouseMove(object sender, MouseEventArgs e)
        {
            int threshold = 10;
            int deltaX;
            int deltaY;
            Point DragCurrentPoint = PointToScreen(new Point(e.X, e.Y));
            deltaX = Math.Abs(DragCurrentPoint.X - DragStartPoint.X);
            deltaY = Math.Abs(DragCurrentPoint.Y - DragStartPoint.Y);

            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            if (isDragging)
            {
                return;
            }

            if (deltaX >= threshold || deltaY >= threshold)
            {
                isDragging = true;
                Mouse_Clicked(sender, e);
                DoDragDrop(this, DragDropEffects.All);
            }
        }

        private void List_DragEnter(object sender, DragEventArgs e)
        {
            //Console.WriteLine("List_DragEnter");
            BackColor = IsSelected ? SELECTED_COLOR : HIGHLIGHT_COLOR;
            foreach (Control c in Controls) c.BackColor = IsSelected ? SELECTED_COLOR : HIGHLIGHT_COLOR;
        }

        private void List_DragOver(object sender, DragEventArgs e)
        {
            //Console.WriteLine("List_DragOver");
        }

        private void List_DragLeave(object sender, EventArgs e)
        {
            //Console.WriteLine("List_DragLeave");
            BackColor = IsSelected ? SELECTED_COLOR : BACK_COLOR;
            foreach (Control c in Controls) c.BackColor = IsSelected ? SELECTED_COLOR : BACK_COLOR;
        }
    }
}
