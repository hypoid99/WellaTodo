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
    public delegate void Post_it_Event(object sender, UserCommandEventArgs e);

    public partial class Post_it : UserControl
    {
        public event Post_it_Event Post_it_Click;

        Popup popup;

        CDataCell m_DataCell;
        public CDataCell DataCell { get => m_DataCell; set => m_DataCell = value; }

        private string _memoTitle;
        public string MemoTitle
        {
            get => _memoTitle;
            set
            {
                _memoTitle = value;
                label_Title.Text = value;
            }
        }

        private string _textBoxString;
        public string MemoString
        {
            get
            {
                _textBoxString = richTextBox.Text;
                return _textBoxString;
            }
            set
            {
                _textBoxString = value;
                richTextBox.Text = _textBoxString;
            }
        }

        private string _textBoxRTFString;
        public string MemoRTFString
        {
            get
            {
                _textBoxRTFString = richTextBox.Rtf;
                return _textBoxRTFString;
            }
            set
            {
                _textBoxRTFString = value;
                richTextBox.Rtf = _textBoxRTFString;
            }
        }

        private bool _setAlarm;
        public bool SetAlarm
        {
            get
            {
                return _setAlarm;
            }
            set
            {
                _setAlarm = value;
                if (value)
                {
                    panel_Alarm_Date.Visible = true;
                    label_AlarmDate.Text = "알림 : " + DataCell.DC_deadlineTime.ToShortDateString();
                }
                else
                {
                    panel_Alarm_Date.Visible = false;
                }
            }
        }

        private Color _memoColor;
        public Color MemoColor 
        {
            get 
            {
                _memoColor = BackColor;
                return _memoColor;
            }
            set 
            { 
                _memoColor = value;
                BackColor = richTextBox.BackColor = label_AlarmDate.BackColor = _memoColor;
            }
        }

        private int _memoTag;
        public int MemoTag
        {
            get => _memoTag;
            set
            {
                _memoTag = value;
                Update_Tag();
            }
        }

        private bool _archive;
        public bool IsArchive
        {
            get => _archive;
            set
            {
                _archive = value;
                if (value)
                {
                    pictureBox_Archive.BackgroundImage = Properties.Resources.outline_unarchive_black_24dp;
                    pictureBox_New.Visible = false;
                }
                else
                {
                    pictureBox_Archive.BackgroundImage = Properties.Resources.outline_archive_black_24dp;
                    pictureBox_New.Visible = true;
                }
            }
        }

        private bool _bulletin;
        public bool IsBulletin { get => _bulletin; set => _bulletin = value; }

        public int GetMemoLength
        {
            get
            {
                return richTextBox.TextLength;
            }
        }

        bool isRichTextBox_Changed = false;
        public bool IsRichTextBox_Changed { get => isRichTextBox_Changed; set => isRichTextBox_Changed = value; }
        bool isTextbox_Title_Clicked = false;

        public Post_it(CDataCell dc)
        {
            InitializeComponent();

            DataCell = dc;

            MemoTitle = dc.DC_title;
            MemoRTFString = dc.DC_memoRTF;
            IsBulletin = dc.DC_bulletin;
            IsArchive = dc.DC_archive;
            MemoTag = dc.DC_memoTag;
            MemoColor = Color.FromName(dc.DC_memoColor);
        }

        private void Post_it_Load(object sender, EventArgs e)
        {
            Initiate();
        }

        private void Post_it_Resize(object sender, EventArgs e)
        {
            Update_Post_it();
        }

        private void Initiate()
        {
            IsRichTextBox_Changed = false;
            pictureBox_Edit.BackColor = Color.Transparent;

            pictureBox_New.Location = new Point(panel_Header.Width - 28, 4);
            pictureBox_Delete.Location = new Point(panel_Header.Width - 28, 4);

            textBox_Title .Visible = false;
            panel_ColorPallet.Visible = false;
            panel_Tag.Visible = false;
            panel_Alarm.Visible = false;
            panel_Alarm_Date.Visible = false;
        }

        private void Update_Post_it()
        {
            pictureBox_New.Location = new Point(panel_Header.Width - 28, 4);
            pictureBox_Delete.Location = new Point(panel_Header.Width - 28, 4);
        }

        private void Update_Tag()
        {
            switch (MemoTag)
            {
                case 0:
                    pictureBox_Label.BackColor = BackColor;
                    break;
                case 1:
                    pictureBox_Label.BackColor = Color.Red;
                    break;
                case 2:
                    pictureBox_Label.BackColor = Color.Orange;
                    break;
                case 3:
                    pictureBox_Label.BackColor = Color.Yellow;
                    break;
                case 4:
                    pictureBox_Label.BackColor = Color.Green;
                    break;
                case 5:
                    pictureBox_Label.BackColor = Color.Blue;
                    break;
            }
            this.Invalidate();
        }

        private void button_Alarm_Set_Click(object sender, EventArgs e)
        {
            popup.Close();

            if (Post_it_Click != null) Post_it_Click?.Invoke(this, new UserCommandEventArgs("AlarmSet"));
        }

        private void button_Alarm_Reset_Click(object sender, EventArgs e)
        {
            popup.Close();

            if (Post_it_Click != null) Post_it_Click?.Invoke(this, new UserCommandEventArgs("AlarmReset"));
        }

        private void pictureBox_Color_Click(object sender, EventArgs e)
        {
            PictureBox sd = (PictureBox)sender;

            switch (sd.Name)
            {
                case "pictureBox_Color1":
                    BackColor = richTextBox.BackColor = pictureBox_Color1.BackColor;
                    break;
                case "pictureBox_Color2":
                    BackColor = richTextBox.BackColor = pictureBox_Color2.BackColor;
                    break;
                case "pictureBox_Color3":
                    BackColor = richTextBox.BackColor = pictureBox_Color3.BackColor;
                    break;
                case "pictureBox_Color4":
                    BackColor = richTextBox.BackColor = pictureBox_Color4.BackColor;
                    break;
                case "pictureBox_Color5":
                    BackColor = richTextBox.BackColor = pictureBox_Color5.BackColor;
                    break;
            }

            popup.Close();
            if (Post_it_Click != null) Post_it_Click?.Invoke(this, new UserCommandEventArgs("Color"));
        }

        private void pictureBox_Tag_Click(object sender, EventArgs e)
        {
            PictureBox sd = (PictureBox)sender;

            switch (sd.Name)
            {
                case "pictureBox_Tag0":
                    MemoTag = 0;
                    break;
                case "pictureBox_Tag1":
                    MemoTag = 1;
                    break;
                case "pictureBox_Tag2":
                    MemoTag = 2;
                    break;
                case "pictureBox_Tag3":
                    MemoTag = 3;
                    break;
                case "pictureBox_Tag4":
                    MemoTag = 4;
                    break;
                case "pictureBox_Tag5":
                    MemoTag = 5;
                    break;
            }

            popup.Close();

            if (Post_it_Click != null) Post_it_Click?.Invoke(this, new UserCommandEventArgs("Tag"));
        }

        // --------------------------------------------------------------
        // 툴바 이벤트
        // --------------------------------------------------------------
        private void pictureBox_Edit_MouseEnter(object sender, EventArgs e)
        {
            ((PictureBox)sender).BorderStyle = BorderStyle.FixedSingle;
        }

        private void pictureBox_Edit_MouseLeave(object sender, EventArgs e)
        {
            ((PictureBox)sender).BorderStyle = BorderStyle.None;
        }

        private void pictureBox_Edit_Click(object sender, EventArgs e)
        {
            if (Post_it_Click != null) Post_it_Click?.Invoke(this, new UserCommandEventArgs ("Edit"));
        }

        private void pictureBox_New_MouseEnter(object sender, EventArgs e)
        {
            ((PictureBox)sender).BorderStyle = BorderStyle.FixedSingle;
        }

        private void pictureBox_New_MouseLeave(object sender, EventArgs e)
        {
            ((PictureBox)sender).BorderStyle = BorderStyle.None;
        }

        private void pictureBox_New_Click(object sender, EventArgs e)
        {
            if (Post_it_Click != null) Post_it_Click?.Invoke(this, new UserCommandEventArgs("New"));
        }

        private void pictureBox_Alarm_MouseEnter(object sender, EventArgs e)
        {
            ((PictureBox)sender).BorderStyle = BorderStyle.FixedSingle;
        }

        private void pictureBox_Alarm_MouseLeave(object sender, EventArgs e)
        {
            ((PictureBox)sender).BorderStyle = BorderStyle.None;
        }

        private void pictureBox_Alarm_Click(object sender, EventArgs e)
        {
            panel_Alarm.Size = new Size(100, 40);

            popup = new Popup(panel_Alarm);
            popup.Show(sender as PictureBox);
        }

        private void pictureBox_ColorPallet_MouseEnter(object sender, EventArgs e)
        {
            ((PictureBox)sender).BorderStyle = BorderStyle.FixedSingle;
        }

        private void pictureBox_ColorPallet_MouseLeave(object sender, EventArgs e)
        {
            ((PictureBox)sender).BorderStyle = BorderStyle.None;
        }

        private void pictureBox_ColorPallet_Click(object sender, EventArgs e)
        {
            panel_ColorPallet.Size = new Size(175, 40);

            popup = new Popup(panel_ColorPallet);
            popup.Show(sender as PictureBox);
        }

        private void pictureBox_Label_MouseEnter(object sender, EventArgs e)
        {
            ((PictureBox)sender).BorderStyle = BorderStyle.FixedSingle;
        }

        private void pictureBox_Label_MouseLeave(object sender, EventArgs e)
        {
            ((PictureBox)sender).BorderStyle = BorderStyle.None;
        }

        private void pictureBox_Label_Click(object sender, EventArgs e)
        {
            panel_Tag.Size = new Size(205, 40);

            popup = new Popup(panel_Tag);
            popup.Show(sender as PictureBox);
        }

        private void pictureBox_Archive_MouseEnter(object sender, EventArgs e)
        {
            ((PictureBox)sender).BorderStyle = BorderStyle.FixedSingle;
        }

        private void pictureBox_Archive_MouseLeave(object sender, EventArgs e)
        {
            ((PictureBox)sender).BorderStyle = BorderStyle.None;
        }

        private void pictureBox_Archive_Click(object sender, EventArgs e)
        {
            if (Post_it_Click != null) Post_it_Click?.Invoke(this, new UserCommandEventArgs("Archive"));
        }

        private void pictureBox_Delete_MouseEnter(object sender, EventArgs e)
        {
            ((PictureBox)sender).BorderStyle = BorderStyle.FixedSingle;
        }

        private void pictureBox_Delete_MouseLeave(object sender, EventArgs e)
        {
            ((PictureBox)sender).BorderStyle = BorderStyle.None;
        }

        private void pictureBox_Delete_Click(object sender, EventArgs e)
        {
            if (Post_it_Click != null) Post_it_Click?.Invoke(this, new UserCommandEventArgs("Delete"));
        }

        private void richTextBox_TextChanged(object sender, EventArgs e)
        {
            isRichTextBox_Changed = true;
            pictureBox_Edit.BackColor = Color.White;
        }

        private void richTextBox_Leave(object sender, EventArgs e)
        {
            if (!isRichTextBox_Changed)
            {
                return;
            }

            if (Post_it_Click != null) Post_it_Click?.Invoke(this, new UserCommandEventArgs("Changed"));
        }

        private void label_Title_Click(object sender, EventArgs e)
        {
            textBox_Title.Visible = true;
            textBox_Title.Text = MemoTitle;
            textBox_Title.Focus();
        }

        private void textBox_Title_Enter(object sender, EventArgs e)
        {
            if (!isTextbox_Title_Clicked) textBox_Title.Text = MemoTitle;
            isTextbox_Title_Clicked = true;
        }

        private void textBox_Title_Leave(object sender, EventArgs e)
        {
            if (textBox_Title.Text.Trim().Length == 0)
            {
                textBox_Title.Visible = false;
                return;
            }

            if (!textBox_Title.Visible)  // 엔터키 이벤트후 Leave 이벤트 막기
            {
                return;
            }

            MemoTitle = textBox_Title.Text;

            isTextbox_Title_Clicked = false;
            textBox_Title.Text = "";
            textBox_Title.Visible = false;

            if (Post_it_Click != null) Post_it_Click?.Invoke(this, new UserCommandEventArgs("Title"));
        }

        private void textBox_Title_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void textBox_Title_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = false;
                e.SuppressKeyPress = false;

                if (textBox_Title.Text.Trim().Length == 0)
                {
                    textBox_Title.Visible = false;
                    return;
                }

                MemoTitle = textBox_Title.Text;

                isTextbox_Title_Clicked = false;
                textBox_Title.Text = "";
                textBox_Title.Visible = false;

                if (Post_it_Click != null) Post_it_Click?.Invoke(this, new UserCommandEventArgs("Title"));
            }
        }

        private void textBox_Title_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu textboxMenu = new ContextMenu();
                MenuItem copyMenu = new MenuItem("복사", new EventHandler(OnCopyMenu_textBox_Title_Click));
                MenuItem cutMenu = new MenuItem("잘라내기", new EventHandler(OnCutMenu_textBox_Title_Click));
                MenuItem pasteMenu = new MenuItem("붙여넣기", new EventHandler(OnPasteMenu_textBox_Title_Click));

                textboxMenu.Popup += new EventHandler(OnPopupEvent_textBox_Title);
                textboxMenu.MenuItems.Add(copyMenu);
                textboxMenu.MenuItems.Add(cutMenu);
                textboxMenu.MenuItems.Add(pasteMenu);
                textBox_Title.ContextMenu = textboxMenu;

                textBox_Title.ContextMenu.Show(textBox_Title, new Point(e.X, e.Y));
            }
        }

        private void OnPopupEvent_textBox_Title(object sender, EventArgs e)
        {
            ContextMenu ctm = (ContextMenu)sender;
            ctm.MenuItems[0].Enabled = textBox_Title.SelectedText.Length != 0; // copy
            ctm.MenuItems[1].Enabled = textBox_Title.SelectedText.Length != 0; // cut
            ctm.MenuItems[2].Enabled = Clipboard.ContainsText(); // paste
        }
        private void OnCopyMenu_textBox_Title_Click(object sender, EventArgs e) { textBox_Title.Copy(); }
        private void OnCutMenu_textBox_Title_Click(object sender, EventArgs e) { textBox_Title.Cut(); }
        private void OnPasteMenu_textBox_Title_Click(object sender, EventArgs e) { textBox_Title.Paste(); }
    }
}
