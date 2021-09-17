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

        private bool isChecked;
        public bool IsChecked
        {
            get { return isChecked; }
            set { isChecked = value; }
        }

        public StarCheckbox()
        {
            InitializeComponent();

            DoubleBuffered = true;
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;

            this.IsChecked = false;
            Background = Color.White;
            BorderColor = Color.Black;
            TickColor = Color.Green;
        }

        public StarCheckbox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        private bool _check = false;

        public bool Check
        {
            get { return _check; }
            set { _check = value; Invalidate(); }
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);

            pevent.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Rectangle rc = this.ClientRectangle;
            Graphics g = pevent.Graphics;
            StringFormat sf = new StringFormat();
            Font f = new Font("Arial", (float)rc.Height * 0.5f, FontStyle.Bold, GraphicsUnit.Pixel);

            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;

            g.FillEllipse(new SolidBrush(Background), rc.Left + 1.5f, rc.Top + 1.5f, rc.Width - 4.0f, rc.Height - 4.0f);
            g.DrawEllipse(new Pen(new SolidBrush(BorderColor), 3.0f), rc.Left + 1.5f, rc.Top + 1.5f, rc.Width - 4.0f, rc.Height - 4.0f);

            if (IsChecked)
                g.DrawString("\u2713", f, new SolidBrush(tickColor), rc, sf);
        }

        private void StarCheckbox_Click(object sender, EventArgs e)
        {
            _check = !_check;
            Invalidate();
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            IsChecked = IsChecked ? false : true;
            Invalidate();
        }
    }
}
