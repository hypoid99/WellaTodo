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
    public delegate void Calendar_Item_Event(object sender, EventArgs e);

    public partial class Calendar_Item : UserControl
    {
        public event Calendar_Item_Event Calendar_Item_Click;

        static readonly int LIST_WIDTH = 100;
        static readonly int LIST_HEIGHT = 40;

        static readonly Color BACK_COLOR = Color.White;
        static readonly Color HIGHLIGHT_COLOR = Color.LightCyan;
        static readonly Color SELECTED_COLOR = Color.Cyan;
        static readonly Color BORDER_COLOR = Color.LightGray;

        static readonly string FONT_NAME = "돋움";
        static readonly float FONT_SIZE_PRIMARY = 8.0f;

        private PictureBox pictureBox_Icon = new PictureBox();
        private Label label_PrimaryText = new Label();
        ToolTip m_ToolTip = new ToolTip();

        private CDataCell m_DataCell;
        public CDataCell CD_DataCell { get => m_DataCell; set => m_DataCell = value; }

        bool isDragging = false;
        Point DragStartPoint;

        private Font m_Font = DefaultFont;
        public override Font Font
        {
            get { return m_Font; }
            set
            {
                m_Font = value;
                label_PrimaryText.Font = m_Font;
            }
        }

        private Color m_Color = DefaultBackColor;
        public override Color BackColor
        {
            get { return m_Color; }
            set
            {
                m_Color = value;
                label_PrimaryText.BackColor = m_Color;
            }
        }

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

        public Calendar_Item()
        {
            InitializeComponent();

            IconImage = null;
            PrimaryText = "제목없음";

            Initialize();
        }

        public Calendar_Item(CDataCell dc)
        {
            InitializeComponent();

            CD_DataCell = dc;
            IconImage = null;
            PrimaryText = dc.DC_title;

            Initialize();
        }

        public Calendar_Item(CDataCell dc, Image iconImage)
        {
            InitializeComponent();

            CD_DataCell = dc;
            IconImage = iconImage;
            PrimaryText = dc.DC_title;

            Initialize();
        }

        private void Initialize()
        {
            Size = new Size(LIST_WIDTH, LIST_HEIGHT);
            Margin = new Padding(1);
            BackColor = BACK_COLOR;
            Paint += new PaintEventHandler(Calendar_Item_Paint);
            MouseClick += new MouseEventHandler(Calendar_Item_MouseClick);
            MouseEnter += new EventHandler(Calendar_Item_MouseEnter);
            MouseLeave += new EventHandler(Calendar_Item_MouseLeave);

            AllowDrop = true;

            if (IconImage != null)
            {
                pictureBox_Icon.Size = new Size(12, 12);
                pictureBox_Icon.MouseClick += new MouseEventHandler(Calendar_Item_MouseClick);
                pictureBox_Icon.MouseEnter += new EventHandler(Calendar_Item_MouseEnter);
                pictureBox_Icon.MouseLeave += new EventHandler(Calendar_Item_MouseLeave);
                pictureBox_Icon.Location = new Point(5, 8);
                Controls.Add(pictureBox_Icon);
            }

            label_PrimaryText.Font = new Font(FONT_NAME, FONT_SIZE_PRIMARY, FontStyle.Regular);
            label_PrimaryText.MouseClick += new MouseEventHandler(Calendar_Item_MouseClick);
            label_PrimaryText.MouseEnter += new EventHandler(Calendar_Item_MouseEnter);
            label_PrimaryText.MouseLeave += new EventHandler(Calendar_Item_MouseLeave);
            label_PrimaryText.MouseDown += new MouseEventHandler(Calendar_Item_MouseDown);
            label_PrimaryText.MouseUp += new MouseEventHandler(Calendar_Item_MouseUp);
            label_PrimaryText.MouseMove += new MouseEventHandler(Calendar_Item_MouseMove);
            //label_PrimaryText.Location = new Point(0, 0);
            label_PrimaryText.BackColor = BACK_COLOR;
            Controls.Add(label_PrimaryText);

            m_ToolTip.SetToolTip(label_PrimaryText, label_PrimaryText.Text);
            //m_ToolTip.SetToolTip(this, label_PrimaryText.Text);
        }

        private void Calendar_Item_Paint(object sender, PaintEventArgs pevent)
        {
            Graphics g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            Rectangle rc = ClientRectangle;
            int x = rc.Left;
            int y = rc.Top;
            int w = rc.Width - 2;
            int h = rc.Height - 1;
            g.DrawRectangle(new Pen(BORDER_COLOR, 1.0f), x, y, w, h);

            label_PrimaryText.Size = new Size(Size.Width - 3, Size.Height - 2);
            label_PrimaryText.Location = new Point(1, 1);
        }

        //---------------------------------------------------------
        // control event
        //---------------------------------------------------------
        private void Calendar_Item_MouseEnter(object sender, EventArgs e)
        {
            //foreach (Control c in Controls) c.BackColor = HIGHLIGHT_COLOR;
            Cursor = Cursors.Hand;
        }

        private void Calendar_Item_MouseLeave(object sender, EventArgs e)
        {
            //foreach (Control c in Controls) c.BackColor = BACK_COLOR;
            Cursor = Cursors.Default;
        }

        private void Calendar_Item_MouseClick(object sender, MouseEventArgs e)
        {
            Focus();
            if (Calendar_Item_Click != null) Calendar_Item_Click?.Invoke(this, e);
        }

        //---------------------------------------------------------
        // 드래그 앤 드롭
        //---------------------------------------------------------
        private void Calendar_Item_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = false;
            DragStartPoint = PointToScreen(new Point(e.X, e.Y));
        }

        private void Calendar_Item_MouseUp(object sender, MouseEventArgs e)
        {
            Console.WriteLine("Calendar_Item_MouseUp");
            if (isDragging)
            {
                //Console.WriteLine("Calendar_Item_MouseUp - DragDrop");
            }
            else
            {
                //Console.WriteLine("Calendar_Item_MouseUp - Click");
            }
            isDragging = false;
        }

        private void Calendar_Item_MouseMove(object sender, MouseEventArgs e)
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
                        //Console.WriteLine("Calendar_Item_MouseMove -> DoDragDrop : " + PrimaryText);
                        DoDragDrop(this, DragDropEffects.All);
                        isDragging = true;
                        return;
                    }
                }
            }
        }

    }
}
