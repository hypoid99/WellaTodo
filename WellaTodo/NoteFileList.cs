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
    public delegate void NoteFileList_Event(object sender, UserCommandEventArgs e);

    public partial class NoteFileList : UserControl
    {
        public event NoteFileList_Event NoteFileList_ClickEvent;

        static readonly int LIST_WIDTH = 250;
        static readonly int LIST_HEIGHT = 32;
        static readonly int LOCATION_Y = 8;

        static readonly Color BACK_COLOR = Color.White;
        static readonly Color HIGHLIGHT_COLOR = Color.LightCyan;
        static readonly Color SELECTED_COLOR = Color.Cyan;
        static readonly Color BORDER_COLOR = Color.LightGray;
        static readonly float PEN_THICKNESS = 1.0f;

        static readonly string FONT_NAME = "돋움";
        static readonly float FONT_SIZE_PRIMARY = 11.0f;
        static readonly float FONT_SIZE_SECONDARY = 8.0f;
        static readonly float FONT_SIZE_METADATA = 8.0f;

        private TextBox textBox_Rename;

        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
        private CDataCell _dataCell;
        public CDataCell DataCell { get => _dataCell; set => _dataCell = value; }

        private Image _image;
        public Image Image
        {
            get => _image;
            set => pictureBox_Icon.BackgroundImage = _image = value;
        }

        private string _fileName;
        public string FileName 
        { 
            get => _fileName; 
            set => _fileName = label_FileName.Text = value; 
        }

        private DateTime _modifiedDate;
        public DateTime ModifiedDate { get => _modifiedDate; set => _modifiedDate = value; }

        private DateTime _createdDate;
        public DateTime CreatedDate { get => _createdDate; set => _createdDate = value; }

        private bool _isSelected = false;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                BackColor = IsSelected ? SELECTED_COLOR : BACK_COLOR;
                foreach (Control c in Controls) c.BackColor = IsSelected ? SELECTED_COLOR : BACK_COLOR;
            }
        }

        private string fileName_Renamed;
        public string FileName_Renamed
        {
            get => fileName_Renamed;
            set => fileName_Renamed = value;
        }

        bool isTextboxClicked = false;
        public bool IsTextboxClicked
        {
            get => isTextboxClicked;
            set => isTextboxClicked = value;
        }

        // --------------------------------------------------
        // Constructor
        // --------------------------------------------------
        public NoteFileList()
        {
            InitializeComponent();
        }

        public NoteFileList(CDataCell dc)
        {
            InitializeComponent();

            DataCell = dc;

            label_FileName.Text = dc.DC_title;

            Load += NoteFileList_Load;
            Resize += NoteFileList_Resize;
        }

        // --------------------------------------------------
        // Form 이벤트
        // --------------------------------------------------
        private void NoteFileList_Load(object sender, EventArgs e)
        {
            Initiate();
        }

        private void NoteFileList_Resize(object sender, EventArgs e)
        {
            Update_Display();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Update_Display();

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            Rectangle rc = ClientRectangle;
            int x1 = rc.Left;
            int y1 = rc.Top;
            int x2 = rc.Left + rc.Width - 1;
            int y2 = rc.Top + rc.Height - 1;
            g.FillRectangle(new SolidBrush(BackColor), x1 - 1, y1 - 1, rc.Width + 1, rc.Height + 1);
            g.DrawLine(new Pen(BORDER_COLOR, PEN_THICKNESS), x1, y2, x2, y2);
        }

        // --------------------------------------------------
        // 초기화 및 Update Display
        // --------------------------------------------------
        private void Initiate()
        {
            Size = new Size(LIST_WIDTH, LIST_HEIGHT);
            Margin = new Padding(1);
            BackColor = BACK_COLOR;

            AllowDrop = true;

            MouseClick += NoteFileList_MouseClick;
            MouseDoubleClick += NoteFileList_MouseDoubleClick;
            MouseEnter += NoteFileList_MouseEnter;
            MouseLeave += NoteFileList_MouseLeave;

            label_FileName.MouseEnter += NoteFileList_MouseEnter;
            label_FileName.MouseLeave += NoteFileList_MouseLeave;
            label_FileName.MouseClick += NoteFileList_MouseClick;
            label_FileName.MouseDoubleClick += NoteFileList_MouseDoubleClick;
            label_FileName.Font = new Font(FONT_NAME, FONT_SIZE_PRIMARY, FontStyle.Regular);
            label_FileName.BackColor = BACK_COLOR;

            label_ModifiedDate.MouseEnter += NoteFileList_MouseEnter;
            label_ModifiedDate.MouseLeave += NoteFileList_MouseLeave;
            label_ModifiedDate.MouseClick += NoteFileList_MouseClick;
            label_ModifiedDate.MouseDoubleClick += NoteFileList_MouseDoubleClick;
            label_ModifiedDate.Font = new Font(FONT_NAME, FONT_SIZE_PRIMARY, FontStyle.Regular);
            label_ModifiedDate.BackColor = BACK_COLOR;

            label_CreatedDate.MouseEnter += NoteFileList_MouseEnter;
            label_CreatedDate.MouseLeave += NoteFileList_MouseLeave;
            label_CreatedDate.MouseClick += NoteFileList_MouseClick;
            label_CreatedDate.MouseDoubleClick += NoteFileList_MouseDoubleClick;
            label_CreatedDate.Font = new Font(FONT_NAME, FONT_SIZE_PRIMARY, FontStyle.Regular);
            label_CreatedDate.BackColor = BACK_COLOR;

            label_FileSize.MouseEnter += NoteFileList_MouseEnter;
            label_FileSize.MouseLeave += NoteFileList_MouseLeave;
            label_FileSize.MouseClick += NoteFileList_MouseClick;
            label_FileSize.MouseDoubleClick += NoteFileList_MouseDoubleClick;
            label_FileSize.Font = new Font(FONT_NAME, FONT_SIZE_PRIMARY, FontStyle.Regular);
            label_FileSize.BackColor = BACK_COLOR;

            textBox_Rename = new TextBox();
            textBox_Rename.Enter += new EventHandler(textBox_Rename_Enter);
            textBox_Rename.KeyDown += new KeyEventHandler(textBox_Rename_KeyDown);
            textBox_Rename.KeyUp += new KeyEventHandler(textBox_Rename_KeyUp);
            textBox_Rename.Leave += new EventHandler(textBox_Rename_Leave);
            textBox_Rename.Visible = false;
            textBox_Rename.Location = new Point(30, LOCATION_Y);
            Controls.Add(textBox_Rename);
        }

        private void Update_Display()
        {
            pictureBox_Icon.Size = new Size(24, 24);
            pictureBox_Icon.Location = new Point(5, 4);

            label_FileName.Location = new Point(30, LOCATION_Y);

            label_ModifiedDate.Location = new Point(330, LOCATION_Y);

            label_CreatedDate.Location = new Point(430, LOCATION_Y);

            label_FileSize.Location = new Point(530, LOCATION_Y);
        }

        //---------------------------------------------------------
        // Textbox Rename
        //---------------------------------------------------------
        public void Rename_1st_Process()
        {
            textBox_Rename.Visible = true;
            label_FileName.Visible = false;
            textBox_Rename.Text = FileName;
            textBox_Rename.Focus();
        }

        private void Rename_2nd_Process()
        {
            textBox_Rename.Visible = false;
            label_FileName.Visible = true;

            if (FileName_Renamed == textBox_Rename.Text)
            {
                return;
            }
            FileName_Renamed = textBox_Rename.Text;

            if (NoteFileList_ClickEvent != null) NoteFileList_ClickEvent?.Invoke(this, new UserCommandEventArgs("Rename"));
        }

        private void textBox_Rename_Enter(object sender, EventArgs e)
        {
            IsTextboxClicked = true;
        }

        private void textBox_Rename_Leave(object sender, EventArgs e)
        {
            Rename_2nd_Process();
            IsTextboxClicked = false;
        }

        private void textBox_Rename_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                textBox_Rename.Visible = false;
                label_FileName.Visible = true;
                return;
            }

            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void textBox_Rename_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.Handled = false;
            e.SuppressKeyPress = false;

            Rename_2nd_Process();
            IsTextboxClicked = false;
        }

        // --------------------------------------------------
        // 사용자 입력 처리
        // --------------------------------------------------
        private void NoteFileList_MouseClick(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    if (NoteFileList_ClickEvent != null) NoteFileList_ClickEvent?.Invoke(this, new UserCommandEventArgs("Click"));
                    break;
                case MouseButtons.Right:
                    if (NoteFileList_ClickEvent != null) NoteFileList_ClickEvent?.Invoke(this, new UserCommandEventArgs("ContextMenu"));
                    break;
                case MouseButtons.Middle:
                    if (NoteFileList_ClickEvent != null) NoteFileList_ClickEvent?.Invoke(this, new UserCommandEventArgs("Middle"));
                    break;
            }
        }

        private void NoteFileList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (NoteFileList_ClickEvent != null) NoteFileList_ClickEvent?.Invoke(this, new UserCommandEventArgs("DoubleClick"));
        }

        private void NoteFileList_MouseLeave(object sender, EventArgs e)
        {
            BackColor = IsSelected ? SELECTED_COLOR : BACK_COLOR;
            foreach (Control c in Controls) c.BackColor = IsSelected ? SELECTED_COLOR : BACK_COLOR;
        }

        private void NoteFileList_MouseEnter(object sender, EventArgs e)
        {
            BackColor = IsSelected ? SELECTED_COLOR : HIGHLIGHT_COLOR;
            foreach (Control c in Controls) c.BackColor = IsSelected ? SELECTED_COLOR : HIGHLIGHT_COLOR;
        }

        
    }
}
