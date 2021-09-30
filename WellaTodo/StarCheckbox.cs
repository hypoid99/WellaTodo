using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace WellaTodo
{
    public partial class StarCheckbox : CheckBox
    {
        private Color borderColor;
        public Color BorderColor
        {
            get { return borderColor; }
            set { borderColor = value; }
        }

        private Color background;
        public Color Background
        {
            get { return background; }
            set { background = value; }
        }

        private Color tickColor;
        public Color TickColor
        {
            get { return tickColor; }
            set { tickColor = value; }
        }

        public StarCheckbox()
        {
            InitializeComponent();

            DoubleBuffered = true;
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;
            Background = Color.White;
            BorderColor = Color.Black;
            TickColor = Color.Green;
        }
        /*
        public StarCheckbox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
        */
        protected override void OnPaint(PaintEventArgs pevent)
        {
            //base.OnPaint(pevent);

            Graphics g = pevent.Graphics;
            pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            pevent.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            Rectangle rc = this.ClientRectangle;

            // 라운드 체크박스
            g.FillRectangle(new SolidBrush(this.BackColor), rc.Left - 1, rc.Top - 1, rc.Width + 1, rc.Height + 1);
            g.FillEllipse(new SolidBrush(Background), rc.Left + 3, rc.Top + 3 , rc.Width - 5, rc.Height - 5);
            g.DrawEllipse(new Pen(new SolidBrush(BorderColor), 1.0f), rc.Left + 3, rc.Top + 3, rc.Width - 5, rc.Height - 5);
            if (Checked)
            {
                g.DrawLine(new Pen(new SolidBrush(BorderColor), 2.0f), rc.Left + 7, rc.Top + 10, rc.Left + 13, rc.Top + 17);
                g.DrawLine(new Pen(new SolidBrush(BorderColor), 2.0f), rc.Left + 13, rc.Top + 17, rc.Left + 20, rc.Top + 10);
            }
        }
                
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            Checked = !Checked;
            Invalidate();
        }
    }
}
