using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WellaTodo
{
    public class DoubleBufferedTableLayoutPanel : TableLayoutPanel
    {
        public DoubleBufferedTableLayoutPanel()
        {
            DoubleBuffered = true;
        }
    }

    public partial class CalendarForm : Form
    {
        public CalendarForm()
        {
            InitializeComponent();
        }

        protected override CreateParams CreateParams 
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        private void CalendarForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.Hide();
                e.Cancel = true;
            }
        }

        private void CalendarForm_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true;
            SeatGenerateTableLayOut(8, 6);
        }

        private void Initiate()
        {
            int columncount = 8;
            int rowcount = 4;
            int cellwidth = 10;
            int cellheight = 15;

            tableLayoutPanel1.RowStyles.Clear();
            tableLayoutPanel1.ColumnStyles.Clear();

            tableLayoutPanel1.RowCount = rowcount;
            tableLayoutPanel1.ColumnCount = columncount;

            for (int i = 0; i < rowcount; i++)
            {
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, cellheight));
            }
            for (int i = 0; i < columncount; i++)

            {
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, cellwidth));
            }
        }

        private Size GetActualPixelSize(TableLayoutPanel panel, int col, int row)
        {
            if (panel.ColumnCount <= col || col < 0 || panel.RowCount <= row || row < 0) return Size.Empty;

            int w = panel.Width, h = panel.Height;
            int nw, nh;

            if (panel.ColumnStyles[col].SizeType == SizeType.Absolute)  // 고정 픽셀이면 width를 바로 알수 있다.
            {
                nw = (int)panel.ColumnStyles[col].Width;
            }
            else
            {
                int another = 0;
                for (int i = 0; i < panel.ColumnCount; ++i)  // 다른 요소들을 검사후 값을 알아낼 수 있다.
                {
                    if (panel.ColumnStyles[i].SizeType == SizeType.Absolute)
                        another += (int)panel.ColumnStyles[i].Width;
                }
                nw = (int)((w - another) * (panel.ColumnStyles[col].Width / (float)100));
            }
            
            if (panel.RowStyles[row].SizeType == SizeType.Absolute) // 고정 픽셀이면 height를 바로 알수있다.
            {
                nh = (int)panel.RowStyles[row].Height;
            }
            else
            {
                int another = 0;
                for (int i = 0; i < panel.RowCount; ++i) // 다른 요소들을 검사후 값을 알아낼 수 있다.
                {
                    if (panel.RowStyles[i].SizeType == SizeType.Absolute)
                        another += (int)panel.RowStyles[i].Height;
                }
                nh = (int)((h - another) * (panel.RowStyles[row].Height / (float)100));
            }

            return new Size(nw, nh);
        }

        private void SeatGenerateTableLayOut(int columnCount, int rowCount)
        {
            tableLayoutPanel1.Controls.Clear();

            tableLayoutPanel1.ColumnStyles.Clear();
            tableLayoutPanel1.RowStyles.Clear();

            tableLayoutPanel1.ColumnCount = columnCount;
            tableLayoutPanel1.RowCount = rowCount;

            for (int x = 0; x < columnCount; x++)
            {
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100 / columnCount));
                for (int y = 0; y < rowCount; y++)
                {
                    if (x == 0)
                    {
                        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                    }

                    Label cmd = new Label();
                    cmd.Text = string.Format("({0}, {1})", x, y);
                    tableLayoutPanel1.Controls.Add(cmd, x, y);
                }
            }
        }
    }
}


/*
C# Winform에서 TableLayoutPanel로 Runtime시 동적으로 행을 숨기고 보이게 하는 방법

- 숨김 처리될 행의 SizeType을 Absolute으로 설정

- 숨김 처리될 행 아래에 행이 있다면 그 행을 SizeType을 Percent로 설정

- 숨김 처리될 행의 Height를 0으로 설정

- 숨김 처리될 행만큼 TableLayoutPanel의 Height 조절

*/
