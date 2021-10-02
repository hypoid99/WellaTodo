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
        //public int Star => 1;
        //public int Round => 2;

        private Color borderColor;
        public Color BorderColor
        {
            get { return borderColor; }
            set { borderColor = value; }
        }

        private Color borderHilightColor;
        public Color BorderHilightColor
        {
            get { return borderHilightColor; }
            set { borderHilightColor = value; }
        }

        private Color backgroundColor;
        public Color BackgroundColor
        {
            get { return backgroundColor; }
            set { backgroundColor = value; }
        }

        private Color tickColor;
        public Color TickColor
        {
            get { return tickColor; }
            set { tickColor = value; }
        }

        private bool isMouseEntered;
        public bool IsMouseEntered { get => isMouseEntered; set => isMouseEntered = value; }

        private int checkboxType;
        public int CheckboxType { get => checkboxType; set => checkboxType = value; }

        public StarCheckbox()
        {
            InitializeComponent();

            DoubleBuffered = true;
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            BackgroundColor = Color.White;
            BorderColor = Color.Black;
            BorderHilightColor = Color.Yellow;
            TickColor = Color.Green;

            CheckboxType = 1;
        }

        public StarCheckbox(int checkboxType)
        {
            InitializeComponent();

            DoubleBuffered = true;
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            BackgroundColor = Color.White;
            BorderColor = Color.Black;
            BorderHilightColor = Color.Yellow;
            TickColor = Color.Green;

            CheckboxType = checkboxType;
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            //base.OnPaint(pevent);

            Graphics g = pevent.Graphics;
            pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            pevent.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            Rectangle rc = this.ClientRectangle;

            switch (CheckboxType)
            {
                case 1: // 스타 체크박스
                    g.FillRectangle(new SolidBrush(this.BackColor), rc.Left - 1, rc.Top - 1, rc.Width + 1, rc.Height + 1);
                    g.FillEllipse(new SolidBrush(BackgroundColor), rc.Left + 3, rc.Top + 3, rc.Width - 5, rc.Height - 5);
                    g.DrawEllipse(new Pen(new SolidBrush(BorderHilightColor), 1.0f), rc.Left + 3, rc.Top + 3, rc.Width - 5, rc.Height - 5);
                    if (Checked)
                    {
                        g.DrawLine(new Pen(new SolidBrush(BorderHilightColor), 2.0f), rc.Left + 7, rc.Top + 10, rc.Left + 13, rc.Top + 17);
                        g.DrawLine(new Pen(new SolidBrush(BorderHilightColor), 2.0f), rc.Left + 13, rc.Top + 17, rc.Left + 20, rc.Top + 10);
                    }
                    break;
                case 2: // 라운드 체크박스
                    g.FillRectangle(new SolidBrush(this.BackColor), rc.Left - 1, rc.Top - 1, rc.Width + 1, rc.Height + 1);
                    g.FillEllipse(new SolidBrush(BackgroundColor), rc.Left + 3, rc.Top + 3, rc.Width - 5, rc.Height - 5);
                    g.DrawEllipse(new Pen(new SolidBrush(BorderColor), 1.0f), rc.Left + 3, rc.Top + 3, rc.Width - 5, rc.Height - 5);
                    if (Checked)
                    {
                        g.DrawLine(new Pen(new SolidBrush(BorderColor), 2.0f), rc.Left + 7, rc.Top + 10, rc.Left + 13, rc.Top + 17);
                        g.DrawLine(new Pen(new SolidBrush(BorderColor), 2.0f), rc.Left + 13, rc.Top + 17, rc.Left + 20, rc.Top + 10);
                    }
                    break;
                default:
                    break;
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
