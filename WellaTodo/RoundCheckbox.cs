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
    public partial class RoundCheckbox : CheckBox
    {
        static readonly Color PSEUDO_BORDER_COLOR = Color.Black;
        static readonly Color PSEUDO_CHECK_COLOR = Color.Black;
        static readonly Color PSEUDO_TICK_COLOR = Color.Yellow;
        static readonly float PSEUDO_PEN_THICKNESS = 1.0f;

        private int checkboxType;
        public int CheckboxType { get => checkboxType; set => checkboxType = value; }

        public RoundCheckbox()
        {
            InitializeComponent();

            DoubleBuffered = true;
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            //base.OnPaint(pevent);

            Graphics g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            Rectangle rc = ClientRectangle;

            checkboxType = 1;
            switch (CheckboxType)
            {
                case 1: // 라운드 체크박스
                    g.FillRectangle(new SolidBrush(BackColor), rc.Left - 1, rc.Top - 1, rc.Width + 1, rc.Height + 1);
                    if (Checked)
                    {
                        g.FillEllipse(new SolidBrush(PSEUDO_CHECK_COLOR), rc.Left + 3, rc.Top + 3, rc.Width - 5, rc.Height - 5);
                        g.DrawLine(new Pen(new SolidBrush(PSEUDO_TICK_COLOR), PSEUDO_PEN_THICKNESS + 1.0f), rc.Left + 7, rc.Top + 10, rc.Left + 13, rc.Top + 17);
                        g.DrawLine(new Pen(new SolidBrush(PSEUDO_TICK_COLOR), PSEUDO_PEN_THICKNESS + 1.0f), rc.Left + 13, rc.Top + 17, rc.Left + 20, rc.Top + 10);
                        g.DrawEllipse(new Pen(new SolidBrush(PSEUDO_CHECK_COLOR), PSEUDO_PEN_THICKNESS), rc.Left + 3, rc.Top + 3, rc.Width - 5, rc.Height - 5);
                    }
                    else
                    {
                        g.DrawEllipse(new Pen(new SolidBrush(PSEUDO_BORDER_COLOR), PSEUDO_PEN_THICKNESS), rc.Left + 3, rc.Top + 3, rc.Width - 5, rc.Height - 5);
                    }
                    
                    break;
                default:
                    break;
            }
        }

        protected override void OnClick(EventArgs e)
        {
            //base.OnClick(e);

            Checked = !Checked;
            Invalidate();
        }
    }
}
