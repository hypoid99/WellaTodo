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

        private string _textBoxString;
        public string TextBoxString
        {
            get => _textBoxString;
            set
            {
                _textBoxString = value;
                richTextBox.Text = _textBoxString;
            }
        }

        private string _textBoxRTFString;
        public string TextBoxRTFString
        {
            get => _textBoxRTFString;
            set
            {
                _textBoxRTFString = value;
                richTextBox.Rtf = _textBoxRTFString;
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
                BackColor = richTextBox.BackColor = _memoColor;
            }
        }

        public Post_it(CDataCell dc)
        {
            InitializeComponent();

            DataCell = dc;
            TextBoxRTFString = dc.DC_memoRTF;
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
            richTextBox.ReadOnly = true;

            pictureBox_New.Location = new Point(panel_Header.Width - 28, 4);
            pictureBox_Delete.Location = new Point(panel_Header.Width - 28, 4);

            panel_ColorPallet.Visible = false;
            panel_Tag.Visible = false;
        }

        private void Update_Post_it()
        {
            pictureBox_New.Location = new Point(panel_Header.Width - 28, 4);
            pictureBox_Delete.Location = new Point(panel_Header.Width - 28, 4);
        }

        private void pictureBox_Color_Click(object sender, EventArgs e)
        {
            //Console.WriteLine("pictureBox_Color_Click");

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
            //Console.WriteLine("pictureBox_Color_Click");

            PictureBox sd = (PictureBox)sender;

            switch (sd.Name)
            {
                case "pictureBox_Tag1":

                    break;
                case "pictureBox_Tag2":

                    break;
                case "pictureBox_Tag3":

                    break;
                case "pictureBox_Tag4":

                    break;
                case "pictureBox_Tag5":

                    break;
            }

            popup.Close();

            if (Post_it_Click != null) Post_it_Click?.Invoke(this, new UserCommandEventArgs("Color"));
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
            //Console.WriteLine("pictureBox_Edit_Click");
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
            //Console.WriteLine("pictureBox_New_Click");
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
            //Console.WriteLine("pictureBox_ColorPallet_Click");
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
            //Console.WriteLine("pictureBox_Label_Click");
            panel_Tag.Size = new Size(175, 40);

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
            //Console.WriteLine("pictureBox_Archive_Click");
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
            //Console.WriteLine("pictureBox_Delete_Click");
            if (Post_it_Click != null) Post_it_Click?.Invoke(this, new UserCommandEventArgs("Delete"));
        }

        private void panel_Header_DoubleClick(object sender, EventArgs e)
        {
            //Console.WriteLine("panel_Header_DoubleClick");
            if (Post_it_Click != null) Post_it_Click?.Invoke(this, new UserCommandEventArgs("Edit"));
        }

        private void richTextBox_DoubleClick(object sender, EventArgs e)
        {
            //Console.WriteLine("richTextBox_DoubleClick");
            if (Post_it_Click != null) Post_it_Click?.Invoke(this, new UserCommandEventArgs("Edit"));
        }

        private void richTextBox_TextChanged(object sender, EventArgs e)
        {
            //Console.WriteLine("richTextBox_TextChanged");
            if (Post_it_Click != null) Post_it_Click?.Invoke(this, new UserCommandEventArgs("Changed"));
        }

    }
}
