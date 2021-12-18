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
    public delegate void SingleLineList_Event(object sender, EventArgs e);

    public partial class SingleLineList : UserControl
    {
        public event SingleLineList_Event SingleLineList_Click;

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

        public SingleLineList()
        {
            InitializeComponent();

            IconImage = null;
            PrimaryText = "제목없음";

            Initialize();
        }

        public SingleLineList(string primaryText)
        {
            InitializeComponent();

            IconImage = null;
            PrimaryText = primaryText;

            Initialize();
        }

        public SingleLineList(Image iconImage, string primaryText)
        {
            InitializeComponent();

            IconImage = iconImage;
            PrimaryText = primaryText;

            Initialize();
        }

        private void Initialize()
        {
            Size = new Size(LIST_WIDTH, LIST_HEIGHT);
            Margin = new Padding(1);
            BackColor = BACK_COLOR;
            Paint += new PaintEventHandler(SingleLineList_Paint);
            MouseClick += new MouseEventHandler(SingleLineList_MouseClick);
            MouseEnter += new EventHandler(SingleLineList_MouseEnter);
            MouseLeave += new EventHandler(SingleLineList_MouseLeave);

            if (IconImage != null)
            {
                pictureBox_Icon.Size = new Size(12, 12);
                pictureBox_Icon.MouseClick += new MouseEventHandler(SingleLineList_MouseClick);
                pictureBox_Icon.MouseEnter += new EventHandler(SingleLineList_MouseEnter);
                pictureBox_Icon.MouseLeave += new EventHandler(SingleLineList_MouseLeave);
                pictureBox_Icon.Location = new Point(5, 8);
                Controls.Add(pictureBox_Icon);
            }

            label_PrimaryText.Font = new Font(FONT_NAME, FONT_SIZE_PRIMARY, FontStyle.Regular);
            label_PrimaryText.MouseClick += new MouseEventHandler(SingleLineList_MouseClick);
            label_PrimaryText.MouseEnter += new EventHandler(SingleLineList_MouseEnter);
            label_PrimaryText.MouseLeave += new EventHandler(SingleLineList_MouseLeave);
            label_PrimaryText.Location = new Point(0, 0);
            label_PrimaryText.BackColor = BACK_COLOR;
            Controls.Add(label_PrimaryText);
        }

        private void SingleLineList_Paint(object sender, PaintEventArgs pevent)
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
        private void SingleLineList_MouseEnter(object sender, EventArgs e)
        {
            //foreach (Control c in Controls) c.BackColor = HIGHLIGHT_COLOR;
        }

        private void SingleLineList_MouseLeave(object sender, EventArgs e)
        {
            //foreach (Control c in Controls) c.BackColor = BACK_COLOR;
        }

        private void SingleLineList_MouseClick(object sender, MouseEventArgs e)
        {
            Focus();
            if (SingleLineList_Click != null) SingleLineList_Click?.Invoke(this, e);
        }

        public override String ToString()
        {
            return PrimaryText;
        }
    }
}
