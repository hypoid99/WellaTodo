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
        static readonly Color PSEUDO_BORDER_COLOR = Color.Black;
        static readonly Color PSEUDO_FILL_COLOR = Color.Blue;
        static readonly float PSEUDO_PEN_THICKNESS = 1.0f;

        private int checkboxType;
        public int CheckboxType { get => checkboxType; set => checkboxType = value; }

        GraphicsPath starCheckboxPath = null;

        public StarCheckbox()
        {
            InitializeComponent();

            DoubleBuffered = true;
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;

            SetPathStarCheckbox();
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
                case 1: // 스타 체크박스
                    g.FillRectangle(new SolidBrush(BackColor), rc.Left - 1, rc.Top - 1, rc.Width + 1, rc.Height + 1);
                    if (Checked) g.FillPath(new SolidBrush(PSEUDO_FILL_COLOR), starCheckboxPath);
                    g.DrawPath(new Pen(PSEUDO_BORDER_COLOR, PSEUDO_PEN_THICKNESS), starCheckboxPath);
                    break;
                default:
                    break;
            }
        }
                
        protected override void OnClick(EventArgs e)
        {
            //base.OnClick(e);

            Checked = !Checked;
            Refresh();
        }

        private void SetPathStarCheckbox()
        {
            GraphicsPath path = new GraphicsPath();

            path.AddLine(12, 1, 9, 9);
            path.AddLine(9, 9, 1, 9);
            path.AddLine(1, 9, 7, 14);
            path.AddLine(7, 14, 4, 22);
            path.AddLine(4, 22, 12, 18);
            path.AddLine(12, 18, 20, 22);
            path.AddLine(20, 22, 17, 14);
            path.AddLine(17, 14, 23, 9);
            path.AddLine(23, 9, 15, 9);
            path.AddLine(15, 9, 12, 1);

            starCheckboxPath?.Dispose();
            starCheckboxPath = path;
        }
    }
}
