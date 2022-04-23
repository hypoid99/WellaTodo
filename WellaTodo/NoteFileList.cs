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

        static readonly string FONT_NAME = "돋움";
        static readonly float FONT_SIZE_PRIMARY = 11.0f;
        static readonly float FONT_SIZE_SECONDARY = 8.0f;
        static readonly float FONT_SIZE_METADATA = 8.0f;

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
        public string FileName { get => _fileName; set => _fileName = value; }

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

            pictureBox_Icon.Size = new Size(24, 24);
            pictureBox_Icon.Location = new Point(5, 4);

            label_FileName.MouseEnter += NoteFileList_MouseEnter;
            label_FileName.MouseLeave += NoteFileList_MouseLeave;
            label_FileName.MouseClick += NoteFileList_MouseClick;
            label_FileName.MouseDoubleClick += NoteFileList_MouseDoubleClick;
            label_FileName.Font = new Font(FONT_NAME, FONT_SIZE_PRIMARY, FontStyle.Regular);
            label_FileName.Location = new Point(30, LOCATION_Y);
            label_FileName.BackColor = BACK_COLOR;

            label_ModifiedDate.MouseEnter += NoteFileList_MouseEnter;
            label_ModifiedDate.MouseLeave += NoteFileList_MouseLeave;
            label_ModifiedDate.MouseClick += NoteFileList_MouseClick;
            label_ModifiedDate.MouseDoubleClick += NoteFileList_MouseDoubleClick;
            label_ModifiedDate.Font = new Font(FONT_NAME, FONT_SIZE_PRIMARY, FontStyle.Regular);
            label_ModifiedDate.Location = new Point(130, LOCATION_Y);
            label_ModifiedDate.BackColor = BACK_COLOR;

            label_CreatedDate.MouseEnter += NoteFileList_MouseEnter;
            label_CreatedDate.MouseLeave += NoteFileList_MouseLeave;
            label_CreatedDate.MouseClick += NoteFileList_MouseClick;
            label_CreatedDate.MouseDoubleClick += NoteFileList_MouseDoubleClick;
            label_CreatedDate.Font = new Font(FONT_NAME, FONT_SIZE_PRIMARY, FontStyle.Regular);
            label_CreatedDate.Location = new Point(230, LOCATION_Y);
            label_CreatedDate.BackColor = BACK_COLOR;

            label_FileSize.MouseEnter += NoteFileList_MouseEnter;
            label_FileSize.MouseLeave += NoteFileList_MouseLeave;
            label_FileSize.MouseClick += NoteFileList_MouseClick;
            label_FileSize.MouseDoubleClick += NoteFileList_MouseDoubleClick;
            label_FileSize.Font = new Font(FONT_NAME, FONT_SIZE_PRIMARY, FontStyle.Regular);
            label_FileSize.Location = new Point(330, LOCATION_Y);
            label_FileSize.BackColor = BACK_COLOR;
        }

        private void Update_Display()
        {

        }

        // --------------------------------------------------
        // 사용자 입력 처리
        // --------------------------------------------------
        private void NoteFileList_MouseClick(object sender, MouseEventArgs e)
        {
            if (NoteFileList_ClickEvent != null) NoteFileList_ClickEvent?.Invoke(this, new UserCommandEventArgs("Click"));
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
