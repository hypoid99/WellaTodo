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
    public partial class RoundLabel : Label
    {
        public int cornerRadius = 10;
        public int borderWidth = 1;
        public Color borderColor = Color.DarkGray;
        public Color backColor = Color.LightGray;

        public bool isFillLeftTop = false;
        public bool isFillRightTop = false;
        public bool isFillLeftBtm = false;
        public bool isFillRightBtm = false;

        public RoundLabel()
        {
            InitializeComponent();

            this.DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);

            Graphics g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            using (var graphicsPath = GetRoundRectangle(this.ClientRectangle))
            {
                Rectangle rc = this.ClientRectangle;
                g.FillRectangle(new SolidBrush(this.BackColor), rc.Left - 1, rc.Top - 1, rc.Width + 1, rc.Height + 1);
                g.FillPath(new SolidBrush(this.BackColor), graphicsPath);
                g.DrawPath(new Pen(borderColor, borderWidth), graphicsPath);
                TextRenderer.DrawText(g, Text, this.Font, this.ClientRectangle, this.ForeColor);
            }
        }

        private GraphicsPath GetRoundRectangle(Rectangle rectangle)
        {
            GraphicsPath path = new GraphicsPath();

            //path.AddArc(rectangle.X, rectangle.Y, cornerRadius, cornerRadius, 180, 90);

            int left = rectangle.X;
            int top = rectangle.Y;
            int right = rectangle.X + rectangle.Width - borderWidth;
            int bottom = rectangle.Y + rectangle.Height - borderWidth;

            if (isFillLeftTop)
            {//좌상
                path.AddLine(left, top + cornerRadius, left, top);
                path.AddLine(left, top, left + cornerRadius, top);
            }
            else
            {
                path.AddArc(rectangle.X, rectangle.Y, cornerRadius, cornerRadius, 180, 90);
            }
            if (isFillRightTop)
            {//우상
                path.AddLine(right - cornerRadius, top, right, top);
                path.AddLine(right, top, right, top + cornerRadius);
            }
            else
            {
                path.AddArc(rectangle.X + rectangle.Width - cornerRadius - borderWidth, rectangle.Y, cornerRadius, cornerRadius, 270, 90);
            }
            if (isFillRightBtm)
            {//우하
                path.AddLine(right, bottom - cornerRadius, right, bottom);
                path.AddLine(right, bottom, right - cornerRadius, bottom);
            }
            else
            {
                path.AddArc(rectangle.X + rectangle.Width - cornerRadius - borderWidth, rectangle.Y + rectangle.Height - cornerRadius - borderWidth, cornerRadius, cornerRadius, 0, 90);
            }
            if (isFillLeftBtm)
            {//좌하
                path.AddLine(left + cornerRadius, bottom, left, bottom);
                path.AddLine(left, bottom, left, bottom - cornerRadius);
            }
            else
            {
                path.AddArc(rectangle.X, rectangle.Y + rectangle.Height - cornerRadius - borderWidth, cornerRadius, cornerRadius, 90, 90);
            }

            path.CloseAllFigures();

            return path;
        }
    }
}
