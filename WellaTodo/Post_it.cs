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

        bool isDragging = false;
        Point DragStartPoint;

        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
        CDataCell m_DataCell;
        public CDataCell DataCell 
        { 
            get => m_DataCell; 
            set => m_DataCell = value; 
        }

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

        private string _memoText;
        public string MemoText
        {
            get
            {
                _memoText = textBox_Memo.Text;
                return _memoText;
            }
            set
            {
                _memoText = value;
                textBox_Memo.Text = value;
                textBox_Memo.SelectionStart = textBox_Memo.Text.Length;
            }
        }

        private bool _setAlarm;
        public bool IsAlarmVisible
        {
            get
            {
                return _setAlarm;
            }
            set
            {
                _setAlarm = value;
                Update_Information();
            }
        }

        private bool _setSchedule;
        public bool IsScheduleVisible
        {
            get
            {
                return _setSchedule;
            }
            set
            {
                _setSchedule = value;
                Update_Information();
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
                BackColor = textBox_Memo.BackColor = _memoColor;
                label_AlarmDate.BackColor = label_ScheduleDate.BackColor = _memoColor;
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
                Update_Archive();
            }
        }

        private bool _bulletin;
        public bool IsBulletin 
        { 
            get => _bulletin; 
            set => _bulletin = value; 
        }

        bool isMemoTextChanged = false;
        public bool IsMemoTextChanged
        {
            get => isMemoTextChanged;
            set 
            { 
                isMemoTextChanged = value;
                if (value)
                {
                    pictureBox_Edit.BackColor = Color.White;
                }
                else
                {
                    pictureBox_Edit.BackColor = Color.Transparent;
                }
                
            }
        }

        bool isTitleTextChanged = false;
        public bool IsTitleTextChanged
        {
            get => isTitleTextChanged;
            set => isTitleTextChanged = value;
        }

        public int Memo_TextLength
        {
            get
            {
                return textBox_Memo.TextLength;
            }
        }

        bool isTextbox_Title_Clicked = false;
        public bool IsTextbox_Title_Clicked 
        { 
            get => isTextbox_Title_Clicked; 
            set => isTextbox_Title_Clicked = value; 
        }

        // --------------------------------------------------
        // Constructor
        // --------------------------------------------------
        public Post_it(CDataCell dc)
        {
            InitializeComponent();

            DataCell = dc;

            MemoTitle = dc.DC_title;
            MemoText = dc.DC_memo;

            IsBulletin = dc.DC_bulletin;
            IsArchive = dc.DC_archive;
            MemoTag = dc.DC_memoTag;
            MemoColor = Color.FromName(dc.DC_memoColor);

            if (dc.DC_remindType > 0) IsAlarmVisible = true;
            if (dc.DC_deadlineType > 0) IsScheduleVisible = true;
        }

        // --------------------------------------------------
        // Form 이벤트
        // --------------------------------------------------
        private void Post_it_Load(object sender, EventArgs e)
        {
            Initiate();
        }

        private void Post_it_Resize(object sender, EventArgs e)
        {
            Update_Post_it();
        }

        // --------------------------------------------------
        // 초기화, Update Display
        // --------------------------------------------------
        private void Initiate()
        {
            AllowDrop = true;

            pictureBox_Edit.BackColor = Color.Transparent;

            pictureBox_New.Location = new Point(panel_Header.Width - 28, 4);
            pictureBox_Delete.Location = new Point(panel_Header.Width - 28, 4);

            textBox_Memo.Dock = DockStyle.Fill;

            textBox_Title .Visible = false;
            panel_ColorPallet.Visible = false;
            panel_Tag.Visible = false;
            panel_Alarm.Visible = false;
            panel_Schedule.Visible = false;
            panel_Information.Visible = false;

            IsMemoTextChanged = false;

            if (DataCell.DC_remindType > 0) IsAlarmVisible = true;
            if (DataCell.DC_deadlineType > 0) IsScheduleVisible = true;
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
                    //pictureBox_Label.BackColor = BackColor;
                    pictureBox_Label.BackgroundImage = Properties.Resources.outline_label_black_24dp;
                    break;
                case 1:
                    //pictureBox_Label.BackColor = Color.Red;
                    pictureBox_Label.BackgroundImage = Properties.Resources.outline_label_red_24dp;
                    break;
                case 2:
                    //pictureBox_Label.BackColor = Color.Orange;
                    pictureBox_Label.BackgroundImage = Properties.Resources.outline_label_orange_24dp;
                    break;
                case 3:
                    //pictureBox_Label.BackColor = Color.Yellow;
                    pictureBox_Label.BackgroundImage = Properties.Resources.outline_label_yellow_24dp;
                    break;
                case 4:
                    //pictureBox_Label.BackColor = Color.Green;
                    pictureBox_Label.BackgroundImage = Properties.Resources.outline_label_green_24dp;
                    break;
                case 5:
                    //pictureBox_Label.BackColor = Color.Blue;
                    pictureBox_Label.BackgroundImage = Properties.Resources.outline_label_blue_24dp;
                    break;
            }
            this.Invalidate();
        }

        private void Update_Archive()
        {
            if (IsArchive)
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

        private void Update_Information()
        {
            if (IsAlarmVisible && IsScheduleVisible)
            {
                panel_Information.Visible = true;
                panel_Information.Size = new Size(panel_Header.Width - 28, 48);
                label_AlarmDate.Visible = true;
                label_AlarmDate.Dock = DockStyle.Bottom;
                label_AlarmDate.Text = "알림 : " + DataCell.DC_remindTime.ToString("MM/dd(ddd)tthh:mm");
                label_ScheduleDate.Visible = true;
                label_ScheduleDate.Dock = DockStyle.Bottom;
                label_ScheduleDate.Text = "기한 : " + DataCell.DC_deadlineTime.ToString("MM/dd(ddd)tthh:mm");
            }
            else if (IsAlarmVisible && !IsScheduleVisible)
            {
                panel_Information.Visible = true;
                panel_Information.Size = new Size(panel_Header.Width - 28, 24);
                label_AlarmDate.Visible = true;
                label_AlarmDate.Dock = DockStyle.Bottom;
                label_AlarmDate.Text = "알림 : " + DataCell.DC_remindTime.ToString("MM/dd(ddd)tthh:mm");
                label_ScheduleDate.Visible = false;
            }
            else if (!IsAlarmVisible && IsScheduleVisible)
            {
                panel_Information.Visible = true;
                panel_Information.Size = new Size(panel_Header.Width - 28, 24);
                label_ScheduleDate.Visible = true;
                label_ScheduleDate.Dock = DockStyle.Bottom;
                label_ScheduleDate.Text = "기한 : " + DataCell.DC_deadlineTime.ToString("MM/dd(ddd)tthh:mm");
                label_AlarmDate.Visible = false;
            }
            else
            {
                panel_Information.Visible = false;
            }
        }

        private string ConvertToRtf(string value)
        {
            RichTextBox richTextBox = new RichTextBox();
            richTextBox.Text = value;
            int offset = richTextBox.Rtf.IndexOf(@"\f0\fs17") + 8; // offset = 118;
            int len = richTextBox.Rtf.LastIndexOf(@"\par") - offset;
            string result = richTextBox.Rtf.Substring(offset, len).Trim();
            return result;
        }

        // --------------------------------------------------------------
        // POP-UP 메뉴 이벤트
        // --------------------------------------------------------------
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

        private void button_Schedule_Set_Click(object sender, EventArgs e)
        {
            popup.Close();

            if (Post_it_Click != null) Post_it_Click?.Invoke(this, new UserCommandEventArgs("ScheduleSet"));
        }

        private void button_Schedule_Reset_Click(object sender, EventArgs e)
        {
            popup.Close();

            if (Post_it_Click != null) Post_it_Click?.Invoke(this, new UserCommandEventArgs("ScheduleReset"));
        }

        private void pictureBox_Color_Click(object sender, EventArgs e)
        {
            PictureBox sd = (PictureBox)sender;

            switch (sd.Name)
            {
                case "pictureBox_Color1":
                    BackColor = textBox_Memo.BackColor = pictureBox_Color1.BackColor;
                    break;
                case "pictureBox_Color2":
                    BackColor = textBox_Memo.BackColor = pictureBox_Color2.BackColor;
                    break;
                case "pictureBox_Color3":
                    BackColor = textBox_Memo.BackColor = pictureBox_Color3.BackColor;
                    break;
                case "pictureBox_Color4":
                    BackColor = textBox_Memo.BackColor = pictureBox_Color4.BackColor;
                    break;
                case "pictureBox_Color5":
                    BackColor = textBox_Memo.BackColor = pictureBox_Color5.BackColor;
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
        // 사용자 입력 처리
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
            panel_Alarm.Size = new Size(90, 40);

            popup = new Popup(panel_Alarm);
            popup.Show(sender as PictureBox);
        }

        private void pictureBox_Schedule_MouseEnter(object sender, EventArgs e)
        {
            ((PictureBox)sender).BorderStyle = BorderStyle.FixedSingle;
        }

        private void pictureBox_Schedule_MouseLeave(object sender, EventArgs e)
        {
            ((PictureBox)sender).BorderStyle = BorderStyle.None;
        }

        private void pictureBox_Schedule_Click(object sender, EventArgs e)
        {
            panel_Schedule.Size = new Size(90, 40);

            popup = new Popup(panel_Schedule);
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

        private void textBox_Memo_TextChanged(object sender, EventArgs e)
        {
            IsMemoTextChanged = true;
        }

        private void textBox_Memo_Leave(object sender, EventArgs e)
        {
            if (!IsMemoTextChanged)
            {
                return;
            }
            IsMemoTextChanged = false;

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
            if (!IsTextbox_Title_Clicked) textBox_Title.Text = MemoTitle;
            IsTextbox_Title_Clicked = true;
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

            IsTextbox_Title_Clicked = false;
            textBox_Title.Text = "";
            textBox_Title.Visible = false;

            if (Post_it_Click != null) Post_it_Click?.Invoke(this, new UserCommandEventArgs("Title"));
        }

        private void textBox_Memo_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu textboxMenu = new ContextMenu();
                MenuItem copyMenu = new MenuItem("복사", new EventHandler(OnCopyMenu_textBox_Memo_Click));
                MenuItem cutMenu = new MenuItem("잘라내기", new EventHandler(OnCutMenu_textBox_Memo_Click));
                MenuItem pasteMenu = new MenuItem("붙여넣기", new EventHandler(OnPasteMenu_textBox_Memo_Click));

                textboxMenu.Popup += new EventHandler(OnPopupEvent_textBox_Memo);
                textboxMenu.MenuItems.Add(copyMenu);
                textboxMenu.MenuItems.Add(cutMenu);
                textboxMenu.MenuItems.Add(pasteMenu);
                textBox_Memo.ContextMenu = textboxMenu;

                textBox_Memo.ContextMenu.Show(textBox_Memo, new Point(e.X, e.Y));
            }
        }

        private void OnPopupEvent_textBox_Memo(object sender, EventArgs e)
        {
            ContextMenu ctm = (ContextMenu)sender;
            ctm.MenuItems[0].Enabled = textBox_Memo.SelectedText.Length != 0; // copy
            ctm.MenuItems[1].Enabled = textBox_Memo.SelectedText.Length != 0; // cut
            ctm.MenuItems[2].Enabled = Clipboard.ContainsText(); // paste
        }
        private void OnCopyMenu_textBox_Memo_Click(object sender, EventArgs e) { textBox_Memo.Copy(); }
        private void OnCutMenu_textBox_Memo_Click(object sender, EventArgs e) { textBox_Memo.Cut(); }
        private void OnPasteMenu_textBox_Memo_Click(object sender, EventArgs e) { textBox_Memo.Paste(); }

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

                IsTextbox_Title_Clicked = false;
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

        // ---------------------------------------------------------------------------
        // Post it 드래그앤드롭 Drag & Drop - Source
        // ---------------------------------------------------------------------------
        private void Post_it_MouseDown(object sender, MouseEventArgs e)
        {
            //Console.WriteLine("Post_it_MouseDown");
        }

        private void label_Title_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = false;
            DragStartPoint = PointToScreen(new Point(e.X, e.Y));
        }

        private void label_Title_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void label_Title_MouseMove(object sender, MouseEventArgs e)
        {
            int threshold = 10;
            int deltaX;
            int deltaY;
            Point DragCurrentPoint = PointToScreen(new Point(e.X, e.Y));
            deltaX = Math.Abs(DragCurrentPoint.X - DragStartPoint.X);
            deltaY = Math.Abs(DragCurrentPoint.Y - DragStartPoint.Y);
            if (!isDragging)
            {
                if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
                {
                    if ((deltaX < threshold) && (deltaY < threshold))
                    {
                        //Todo_Item_Click(sender, e);  // click
                        DoDragDrop(this, DragDropEffects.All);
                        isDragging = true;
                        return;
                    }
                }
            }
        }
    }
}
