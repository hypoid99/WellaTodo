// copyright honeysoft 20200924 v0.5
// 수정작업 2021.7.19 -> 2021.9.4 -> 2021.9.11

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WellaTodo
{
    static class WellaTodo
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MainFrame mainFrame = new MainFrame();
            MainModel mainModel = new MainModel();
            new MainController(mainFrame, mainModel);
            Application.Run(mainFrame);
        }
    }
}

/* 직렬화 예제
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SerializingCollection
{
    [Serializable]
    class NameCard
    {
        public NameCard(string Name, string Phone, int Age)
        {
            this.Name = Name;
            this.Phone = Phone;
            this.Age = Age;
        }

        public string Name;
        public string Phone;
        public int Age;
    }

    class Program
    {
        static void Main(string[] args)
        {
            Stream ws = new FileStream("a.dat", FileMode.Create);
            BinaryFormatter serializer = new BinaryFormatter();

            List<NameCard> list = new List<NameCard>();
            list.Add(new NameCard("나노콛", "010-1234-4567", 22));
            list.Add(new NameCard("박보영", "010-9876-6543", 20));
            list.Add(new NameCard("김요시", "010-2222-4444", 19));

            serializer.Serialize(ws, list);
            ws.Close();

            Stream rs = new FileStream("a.dat", FileMode.Open);
            BinaryFormatter deserializer = new BinaryFormatter();

            List<NameCard> list2;
            list2 = (List<NameCard>)deserializer.Deserialize(rs);
            rs.Close();

            foreach(NameCard nc in list2)
                Console.WriteLine("Name: {0}, Phone: {1}, Age: {2}",nc.Name, nc.Phone, nc.Age);
        }
    }
}
*/

/* circular checkbox
 * class CircularCheckbox : Control
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

    public CircularCheckbox(bool IsChecked)
    {
        DoubleBuffered = true;
        SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        this.BackColor = Color.Transparent;

        this.IsChecked = IsChecked;
        Background = Color.White;
        BorderColor = Color.Black;
        TickColor = Color.Green;
    }

    public CircularCheckbox()
        : this(false)
    { }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        Rectangle rc = this.ClientRectangle;
        Graphics g = e.Graphics;
        StringFormat sf = new StringFormat();
        Font f = new Font("Arial", (float)rc.Height * 0.5f, FontStyle.Bold, GraphicsUnit.Pixel);

        sf.Alignment = StringAlignment.Center;
        sf.LineAlignment = StringAlignment.Center;

        g.FillEllipse(new SolidBrush(Background), rc.Left + 1.5f, rc.Top + 1.5f, rc.Width - 4.0f, rc.Height - 4.0f);
        g.DrawEllipse(new Pen(new SolidBrush(BorderColor), 3.0f), rc.Left + 1.5f, rc.Top + 1.5f, rc.Width - 4.0f, rc.Height - 4.0f);

        if (IsChecked)
            g.DrawString("\u2713", f, new SolidBrush(tickColor) , rc, sf);

    }

    protected override void OnClick(EventArgs e)
    {
        base.OnClick(e);

        IsChecked = IsChecked ? false : true;

        Invalidate();
    }
}
*/

/* 심플 체크박스
public class CustomCheckBox : CheckBox
{
    public CustomCheckBox()
    {
        this.SetStyle(ControlStyles.UserPaint, true);
        this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
    }

    protected override void OnPaint(PaintEventArgs pevent)
    {
        base.OnPaint(pevent);
        if (this.Checked)
        {
            pevent.Graphics.FillRectangle(new SolidBrush(Color.Blue), new Rectangle(0, 0, 16, 16));
        }
        else
        {
            pevent.Graphics.FillRectangle(new SolidBrush(Color.Red), new Rectangle(0, 0, 16, 16));
        }
    }
}


//커스톰 체크박스
using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsForms
{
   public class BiggerCheckBox : CheckBox
   {
        #region variables
        private int _boxsize = 14;
        private int _boxlocatx = 0;
        private int _boxlocaty = 0;
        private int _textX = 16;
        private int _textY = 1;
        private Color _boxBackColor = Color.Transparent;
        private Color _tickColor = Color.Black;
        private float _tickSize = 11f;
        private Color _boxColor = Color.Black;
        private float _tickLeftPosition = 0f;
        private float _tickTopPosition = 0f;
        #endregion

        #region Properties
        public int TextLocationX
        {
            get { return _textX; }
            set { _textX = value; Invalidate(); }
        }

        public int TextLocationY
        {
            get { return _textY; }
            set { _textY = value; Invalidate(); }
        }

        public int BoxSize
        {
            get { return _boxsize; }
            set { _boxsize = value; Invalidate(); }
        }

        public int BoxLocationX
        {
            get { return _boxlocatx; }
            set { _boxlocatx = value; Invalidate(); }
        }

        public int BoxLocationY
        {
            get { return _boxlocaty; }
            set { _boxlocaty = value; Invalidate(); }
        }
        public Color BoxBackColor
        {
            get { return _boxBackColor; }
            set { _boxBackColor = value; Invalidate(); }
        }
        public Color TickColor
        {
            get { return _tickColor; }
            set { _tickColor = value; Invalidate(); }
        }
        public float TickSize
        {
            get { return _tickSize; }
            set { _tickSize = value; Invalidate(); }
        }
        public Color BoxColor
        {
            get { return _boxColor; }
            set { _boxColor = value; Invalidate(); }
        }
        public float TickLeftPosition
        {
            get { return _tickLeftPosition; }
            set { _tickLeftPosition = value; Invalidate(); }
        }
        public float TickTopPosition
        {
            get { return _tickTopPosition; }
            set { _tickTopPosition = value; Invalidate(); }
        }
        #endregion

        #region Constrctors
        public BiggerCheckBox()
        {
            Appearance = Appearance.Button;
            FlatStyle = FlatStyle.Flat;
            TextAlign = ContentAlignment.MiddleRight;
            FlatAppearance.BorderSize = 0;
            AutoSize = false;
        }
        #endregion

        #region Methods
        protected override void OnPaint(PaintEventArgs pevent)
        {
            try
            {
                base.OnPaint(pevent);
                pevent.Graphics.Clear(BackColor);

                //checkbox text - using draw string method with specified location
                using (SolidBrush brushText = new SolidBrush(ForeColor))
                {
                    pevent.Graphics.DrawString(Text, Font, brushText, _textX, _textY);
                }

                //checkbox box -  using rectangle for checkbox box
                Rectangle _rectangleBox = new Rectangle(_boxlocatx, _boxlocaty, _boxsize, _boxsize);

                //checkbox box -  checckbox box back color and border color
                using (SolidBrush brushBackColor = new SolidBrush(_boxBackColor))
                {
                    pevent.Graphics.FillRectangle(brushBackColor, _rectangleBox);
                }
                using (Pen penBoxColor = new Pen(_boxColor))
                {
                    pevent.Graphics.DrawRectangle(penBoxColor, _rectangleBox);
                }

                //checkbox box -  check and uncheck
                if (Checked)
                {
                    using (SolidBrush brush = new SolidBrush(_tickColor))
                    {
                        using (Font wing = new Font("Wingdings", _tickSize))
                        {
                            pevent.Graphics.DrawString("ü", wing, brush, _tickLeftPosition, _tickTopPosition);
                        }
                    }
                }
                pevent.Dispose();
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
    }
}
*/