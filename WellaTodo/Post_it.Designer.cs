namespace WellaTodo
{
    partial class Post_it
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel_Header = new System.Windows.Forms.Panel();
            this.panel_Footer = new System.Windows.Forms.Panel();
            this.panel_Body = new System.Windows.Forms.Panel();
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.pictureBox_Label = new System.Windows.Forms.PictureBox();
            this.pictureBox_Store = new System.Windows.Forms.PictureBox();
            this.pictureBox_Image = new System.Windows.Forms.PictureBox();
            this.pictureBox_ColorPallet = new System.Windows.Forms.PictureBox();
            this.pictureBox_Alarm = new System.Windows.Forms.PictureBox();
            this.pictureBox_Delete = new System.Windows.Forms.PictureBox();
            this.pictureBox_Edit = new System.Windows.Forms.PictureBox();
            this.panel_Header.SuspendLayout();
            this.panel_Footer.SuspendLayout();
            this.panel_Body.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Label)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Store)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Image)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ColorPallet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Alarm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Delete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Edit)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_Header
            // 
            this.panel_Header.Controls.Add(this.pictureBox_Delete);
            this.panel_Header.Controls.Add(this.pictureBox_Edit);
            this.panel_Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_Header.Location = new System.Drawing.Point(0, 0);
            this.panel_Header.Name = "panel_Header";
            this.panel_Header.Size = new System.Drawing.Size(183, 40);
            this.panel_Header.TabIndex = 0;
            // 
            // panel_Footer
            // 
            this.panel_Footer.Controls.Add(this.pictureBox_Label);
            this.panel_Footer.Controls.Add(this.pictureBox_Store);
            this.panel_Footer.Controls.Add(this.pictureBox_Image);
            this.panel_Footer.Controls.Add(this.pictureBox_ColorPallet);
            this.panel_Footer.Controls.Add(this.pictureBox_Alarm);
            this.panel_Footer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_Footer.Location = new System.Drawing.Point(0, 180);
            this.panel_Footer.Name = "panel_Footer";
            this.panel_Footer.Size = new System.Drawing.Size(183, 40);
            this.panel_Footer.TabIndex = 1;
            // 
            // panel_Body
            // 
            this.panel_Body.BackColor = System.Drawing.Color.White;
            this.panel_Body.Controls.Add(this.richTextBox);
            this.panel_Body.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Body.Location = new System.Drawing.Point(0, 40);
            this.panel_Body.Name = "panel_Body";
            this.panel_Body.Size = new System.Drawing.Size(183, 140);
            this.panel_Body.TabIndex = 2;
            // 
            // richTextBox
            // 
            this.richTextBox.BackColor = System.Drawing.Color.Gold;
            this.richTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox.HideSelection = false;
            this.richTextBox.Location = new System.Drawing.Point(0, 0);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox.Size = new System.Drawing.Size(183, 140);
            this.richTextBox.TabIndex = 0;
            this.richTextBox.Text = "";
            this.richTextBox.DoubleClick += new System.EventHandler(this.richTextBox_DoubleClick);
            // 
            // pictureBox_Label
            // 
            this.pictureBox_Label.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_Label.BackgroundImage = global::WellaTodo.Properties.Resources.outline_label_black_24dp;
            this.pictureBox_Label.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox_Label.Location = new System.Drawing.Point(144, 4);
            this.pictureBox_Label.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox_Label.Name = "pictureBox_Label";
            this.pictureBox_Label.Size = new System.Drawing.Size(32, 32);
            this.pictureBox_Label.TabIndex = 0;
            this.pictureBox_Label.TabStop = false;
            // 
            // pictureBox_Store
            // 
            this.pictureBox_Store.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_Store.BackgroundImage = global::WellaTodo.Properties.Resources.outline_archive_black_24dp;
            this.pictureBox_Store.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox_Store.Location = new System.Drawing.Point(109, 4);
            this.pictureBox_Store.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox_Store.Name = "pictureBox_Store";
            this.pictureBox_Store.Size = new System.Drawing.Size(32, 32);
            this.pictureBox_Store.TabIndex = 0;
            this.pictureBox_Store.TabStop = false;
            // 
            // pictureBox_Image
            // 
            this.pictureBox_Image.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_Image.BackgroundImage = global::WellaTodo.Properties.Resources.outline_insert_photo_black_24dp;
            this.pictureBox_Image.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox_Image.Location = new System.Drawing.Point(74, 4);
            this.pictureBox_Image.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox_Image.Name = "pictureBox_Image";
            this.pictureBox_Image.Size = new System.Drawing.Size(32, 32);
            this.pictureBox_Image.TabIndex = 0;
            this.pictureBox_Image.TabStop = false;
            // 
            // pictureBox_ColorPallet
            // 
            this.pictureBox_ColorPallet.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_ColorPallet.BackgroundImage = global::WellaTodo.Properties.Resources.outline_palette_black_24dp;
            this.pictureBox_ColorPallet.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox_ColorPallet.Location = new System.Drawing.Point(39, 4);
            this.pictureBox_ColorPallet.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox_ColorPallet.Name = "pictureBox_ColorPallet";
            this.pictureBox_ColorPallet.Size = new System.Drawing.Size(32, 32);
            this.pictureBox_ColorPallet.TabIndex = 0;
            this.pictureBox_ColorPallet.TabStop = false;
            // 
            // pictureBox_Alarm
            // 
            this.pictureBox_Alarm.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_Alarm.BackgroundImage = global::WellaTodo.Properties.Resources.outline_access_alarms_black_24dp;
            this.pictureBox_Alarm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox_Alarm.Location = new System.Drawing.Point(4, 4);
            this.pictureBox_Alarm.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox_Alarm.Name = "pictureBox_Alarm";
            this.pictureBox_Alarm.Size = new System.Drawing.Size(32, 32);
            this.pictureBox_Alarm.TabIndex = 0;
            this.pictureBox_Alarm.TabStop = false;
            // 
            // pictureBox_Delete
            // 
            this.pictureBox_Delete.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_Delete.BackgroundImage = global::WellaTodo.Properties.Resources.outline_delete_black_24dp;
            this.pictureBox_Delete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox_Delete.Location = new System.Drawing.Point(144, 4);
            this.pictureBox_Delete.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox_Delete.Name = "pictureBox_Delete";
            this.pictureBox_Delete.Size = new System.Drawing.Size(32, 32);
            this.pictureBox_Delete.TabIndex = 0;
            this.pictureBox_Delete.TabStop = false;
            this.pictureBox_Delete.Click += new System.EventHandler(this.pictureBox_Close_Click);
            // 
            // pictureBox_Edit
            // 
            this.pictureBox_Edit.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_Edit.BackgroundImage = global::WellaTodo.Properties.Resources.outline_mode_edit_black_24dp;
            this.pictureBox_Edit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox_Edit.Location = new System.Drawing.Point(4, 4);
            this.pictureBox_Edit.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox_Edit.Name = "pictureBox_Edit";
            this.pictureBox_Edit.Size = new System.Drawing.Size(32, 32);
            this.pictureBox_Edit.TabIndex = 0;
            this.pictureBox_Edit.TabStop = false;
            this.pictureBox_Edit.Click += new System.EventHandler(this.pictureBox_Edit_Click);
            // 
            // Post_it
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gold;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.panel_Body);
            this.Controls.Add(this.panel_Footer);
            this.Controls.Add(this.panel_Header);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "Post_it";
            this.Size = new System.Drawing.Size(183, 220);
            this.Load += new System.EventHandler(this.Post_it_Load);
            this.Resize += new System.EventHandler(this.Post_it_Resize);
            this.panel_Header.ResumeLayout(false);
            this.panel_Footer.ResumeLayout(false);
            this.panel_Body.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Label)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Store)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Image)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ColorPallet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Alarm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Delete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Edit)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_Header;
        private System.Windows.Forms.Panel panel_Footer;
        private System.Windows.Forms.Panel panel_Body;
        private System.Windows.Forms.PictureBox pictureBox_Edit;
        private System.Windows.Forms.PictureBox pictureBox_Delete;
        private System.Windows.Forms.PictureBox pictureBox_Label;
        private System.Windows.Forms.PictureBox pictureBox_Store;
        private System.Windows.Forms.PictureBox pictureBox_Image;
        private System.Windows.Forms.PictureBox pictureBox_ColorPallet;
        private System.Windows.Forms.PictureBox pictureBox_Alarm;
        private System.Windows.Forms.RichTextBox richTextBox;
    }
}
