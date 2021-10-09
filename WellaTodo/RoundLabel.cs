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
        static readonly Color PSEUDO_BACK_COLOR = Color.PapayaWhip;
        static readonly Color PSEUDO_HIGHLIGHT_COLOR = Color.LightCyan;
        static readonly Color PSEUDO_BORDER_COLOR = Color.DarkCyan;
        static readonly Color PSEUDO_FILL_COLOR = Color.Gold;
        static readonly float PSEUDO_PEN_THICKNESS = 1.0f;

        GraphicsPath roundRectanglePath = null;

        private int cornerRadius = 10;

        public RoundLabel()
        {
            InitializeComponent();

            DoubleBuffered = true;
            SetPathRoundRectangle();
        }

        protected override void OnResize(EventArgs e)
        {
            SetPathRoundRectangle();
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            //base.OnPaint(pevent);

            Graphics g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            Rectangle rc = ClientRectangle;

            g.FillRectangle(new SolidBrush(BackColor), rc.Left - 1, rc.Top - 1, rc.Width + 1, rc.Height + 1);
            //g.FillPath(new SolidBrush(PSEUDO_FILL_COLOR), roundRectanglePath);
            g.DrawPath(new Pen(PSEUDO_BORDER_COLOR, PSEUDO_PEN_THICKNESS), roundRectanglePath);
            TextRenderer.DrawText(g, Text, Font, ClientRectangle, Color.Black);
        }

        private void SetPathRoundRectangle()
        {
            Rectangle rc = this.ClientRectangle;
            float x = rc.X;
            float y = rc.Y;
            float width = rc.Width - 1;
            float height = rc.Height - 1;
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

            roundRectanglePath?.Dispose();
            roundRectanglePath = path;
        }
        /*
        protected override void OnMouseEnter(EventArgs e)
        {
            BackColor = PSEUDO_HIGHLIGHT_COLOR;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            BackColor = PSEUDO_BACK_COLOR;
        }
        */
    }
}
