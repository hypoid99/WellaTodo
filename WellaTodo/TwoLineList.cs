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
    public partial class TwoLineList : UserControl
    {
        public event TwoLineList_Event TwoLineList_Click;

        private TextBox textBox_Rename;

        static readonly int LIST_WIDTH = 250;
        static readonly int LIST_HEIGHT = 40;

        static readonly Color BACK_COLOR = Color.White;
        static readonly Color HIGHLIGHT_COLOR = Color.LightCyan;
        static readonly Color SELECTED_COLOR = Color.Cyan;

        static readonly string FONT_NAME = "맑은고딕";
        static readonly float FONT_SIZE_PRIMARY = 14.0f;
        static readonly float FONT_SIZE_SECONDARY = 8.0f;
        static readonly float FONT_SIZE_METADATA = 8.0f;

        public Image IconImage
        {
            get => pictureBox_Icon.BackgroundImage;
            set => pictureBox_Icon.BackgroundImage = value;
        }

        public string PrimaryText
        {
            get => label_PrimaryText.Text;
            set => label_PrimaryText.Text = value;
        }

        public string SecondaryText
        {
            get => label_SecondaryText.Text;
            set => label_SecondaryText.Text = value;
        }

        public string MetadataText
        {
            get => label_Metadata.Text;
            set => label_Metadata.Text = value;
        }

        private bool isSelected = false;
        public bool IsSelected
        {
            get => isSelected;
            set
            {
                isSelected = value;
                ChangeSelectedColor();
            }
        }

        private string primaryText_Renamed;
        public string PrimaryText_Renamed { get => primaryText_Renamed; set => primaryText_Renamed = value; }

        bool isTextboxClicked = false;
        

        public TwoLineList()
        {
            InitializeComponent();

            IconImage = null;
            PrimaryText = "제목없음";
            SecondaryText = "";
            MetadataText = "";

            Initialize();
        }

        public TwoLineList(Image iconImage, string primaryText, string secondaryText, string metadataText)
        {
            InitializeComponent();

            IconImage = iconImage;
            PrimaryText = primaryText;
            SecondaryText = secondaryText;
            MetadataText = metadataText;

            Initialize();
        }

        private void Initialize()
        {
            Size = new Size(LIST_WIDTH, LIST_HEIGHT);
            Margin = new Padding(1);
            BackColor = BACK_COLOR;

            pictureBox_Icon.Size = new Size(24, 24);
            pictureBox_Icon.Location = new Point(5, 8);

            label_PrimaryText.Font = new Font(FONT_NAME, FONT_SIZE_PRIMARY, FontStyle.Regular);
            label_PrimaryText.Location = new Point(30, 5);
            label_PrimaryText.BackColor = BACK_COLOR;

            label_SecondaryText.Font = new Font(FONT_NAME, FONT_SIZE_SECONDARY, FontStyle.Regular);
            label_SecondaryText.Location = new Point(30, 20);
            label_SecondaryText.BackColor = BACK_COLOR;

            label_Metadata.AutoSize = false;
            label_Metadata.Font = new Font(FONT_NAME, FONT_SIZE_METADATA, FontStyle.Regular);
            label_Metadata.Location = new Point(Width - 35, 3);
            label_Metadata.Size = new Size(30, 30);
            label_Metadata.TextAlign = ContentAlignment.MiddleRight;
            label_Metadata.BackColor = BACK_COLOR;

            textBox_Rename = new TextBox();
            textBox_Rename.Visible = false;
            textBox_Rename.Location = new Point(30, 10);
            Controls.Add(textBox_Rename);
            textBox_Rename.Enter += new System.EventHandler(textBox_Rename_Enter);
            textBox_Rename.KeyDown += new System.Windows.Forms.KeyEventHandler(textBox_Rename_KeyDown);
            textBox_Rename.KeyUp += new System.Windows.Forms.KeyEventHandler(textBox_Rename_KeyUp);
            textBox_Rename.Leave += new System.EventHandler(textBox_Rename_Leave);
            textBox_Rename.MouseDown += new System.Windows.Forms.MouseEventHandler(textBox_Rename_MouseDown);
        }

        private void TwoLineList_Paint(object sender, PaintEventArgs e)
        {
            if (SecondaryText.Length == 0)
            {
                label_PrimaryText.Location = new Point(30, 10);

                label_SecondaryText.Location = new Point(245, 20);
                label_SecondaryText.Size = new Size(0, 13);
                label_SecondaryText.Text = "";
                label_SecondaryText.AutoSize = false;
            }
            else
            {
                label_PrimaryText.Location = new Point(30, 2);

                label_SecondaryText.Location = new Point(30, 20);
                label_SecondaryText.Text = SecondaryText;
                label_SecondaryText.AutoSize = true;
            }

            label_Metadata.Location = new Point(Width - 35, 3);
        }

        private void Mouse_Clicked(object sender, MouseEventArgs e)
        {
            if (TwoLineList_Click != null) TwoLineList_Click?.Invoke(this, e);
        }

        private void textBox_Rename_Enter(object sender, EventArgs e)
        {
            isTextboxClicked = true;
        }

        private void textBox_Rename_Leave(object sender, EventArgs e)
        {
            textBox_Rename.Visible = false;
            label_PrimaryText.Visible = true;

            if (isTextboxClicked)
            {
                if (textBox_Rename.Text.Trim().Length == 0)
                {
                    isTextboxClicked = false;
                    return;
                }

                // Change PrimaryText
                PrimaryText_Renamed = textBox_Rename.Text;

                MouseEventArgs me = new MouseEventArgs(MouseButtons.Middle, 1, 42, 42, 1);
                if (TwoLineList_Click != null) TwoLineList_Click?.Invoke(this, me);
            }
            isTextboxClicked = false;
        }

        private void textBox_Rename_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                textBox_Rename.Visible = false;
                label_PrimaryText.Visible = true;
                return;
            }

            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = false;
                e.SuppressKeyPress = false;
                if (textBox_Rename.Text.Trim().Length == 0) 
                {
                    textBox_Rename.Visible = false;
                    label_PrimaryText.Visible = true;
                    return;
                }

                // Change PrimaryText
                textBox_Rename.Visible = false;
                label_PrimaryText.Visible = true;
                PrimaryText_Renamed = textBox_Rename.Text;

                MouseEventArgs me = new MouseEventArgs(MouseButtons.Middle, 1, 42, 42, 1);
                if (TwoLineList_Click != null) TwoLineList_Click?.Invoke(this, me);
            }
        }

        private void textBox_Rename_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void textBox_Rename_MouseDown(object sender, MouseEventArgs e)
        {

        }

        public void RenamePrimaryText()
        {
            textBox_Rename.Visible = true;
            label_PrimaryText.Visible = false;
            textBox_Rename.Text = PrimaryText;
            textBox_Rename.Focus();
        }

        private void ChangeToBackColor()
        {
            BackColor = IsSelected ? SELECTED_COLOR : BACK_COLOR;
            label_PrimaryText.BackColor = IsSelected ? SELECTED_COLOR : BACK_COLOR;
            label_SecondaryText.BackColor = IsSelected ? SELECTED_COLOR : BACK_COLOR;
            label_Metadata.BackColor = IsSelected ? SELECTED_COLOR : BACK_COLOR;
        }

        private void ChangeToHighlightColor()
        {
            BackColor = IsSelected ? SELECTED_COLOR : HIGHLIGHT_COLOR;
            label_PrimaryText.BackColor = IsSelected ? SELECTED_COLOR : HIGHLIGHT_COLOR;
            label_SecondaryText.BackColor = IsSelected ? SELECTED_COLOR : HIGHLIGHT_COLOR;
            label_Metadata.BackColor = IsSelected ? SELECTED_COLOR : HIGHLIGHT_COLOR;
        }

        private void ChangeSelectedColor()
        {
            if (IsSelected)
            {
                BackColor = SELECTED_COLOR;
                label_PrimaryText.BackColor = SELECTED_COLOR;
                label_SecondaryText.BackColor = SELECTED_COLOR;
                label_Metadata.BackColor = SELECTED_COLOR;

            }
            else
            {
                BackColor = BACK_COLOR;
                label_PrimaryText.BackColor = BACK_COLOR;
                label_SecondaryText.BackColor = BACK_COLOR;
                label_Metadata.BackColor = BACK_COLOR;
            }
        }

        //---------------------------------------------------------
        // control event
        //---------------------------------------------------------
        private void TwoLineList_MouseEnter(object sender, EventArgs e)
        {
            ChangeToHighlightColor();
        }

        private void TwoLineList_MouseLeave(object sender, EventArgs e)
        {
            ChangeToBackColor();
        }

        private void pictureBox_Icon_MouseEnter(object sender, EventArgs e)
        {
            ChangeToHighlightColor();
        }

        private void pictureBox_Icon_MouseLeave(object sender, EventArgs e)
        {
            ChangeToBackColor();
        }

        private void label_PrimaryText_MouseEnter(object sender, EventArgs e)
        {
            ChangeToHighlightColor();
        }

        private void label_PrimaryText_MouseLeave(object sender, EventArgs e)
        {
            ChangeToBackColor();
        }

        private void label_SecondaryText_MouseEnter(object sender, EventArgs e)
        {
            ChangeToHighlightColor();
        }

        private void label_SecondaryText_MouseLeave(object sender, EventArgs e)
        {
            ChangeToBackColor();
        }

        private void label_Metadata_MouseEnter(object sender, EventArgs e)
        {
            ChangeToHighlightColor();
        }

        private void label_Metadata_MouseLeave(object sender, EventArgs e)
        {
            ChangeToBackColor();
        }

        private void TwoLineList_MouseClick(object sender, MouseEventArgs e)
        {
            Focus();
            Mouse_Clicked(sender, e);
        }

        private void pictureBox_Icon_MouseClick(object sender, MouseEventArgs e)
        {
            Focus();
            Mouse_Clicked(sender, e);
        }

        private void label_PrimaryText_MouseClick(object sender, MouseEventArgs e)
        {
            Focus();
            Mouse_Clicked(sender, e);
        }

        private void label_SecondaryText_MouseClick(object sender, MouseEventArgs e)
        {
            Focus();
            Mouse_Clicked(sender, e);
        }

        private void label_Metadata_MouseClick(object sender, MouseEventArgs e)
        {
            Focus();
            Mouse_Clicked(sender, e);
        }

        public override String ToString()
        {
            return PrimaryText;
        }
    }
}
