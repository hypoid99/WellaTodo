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
            this.pictureBox_Delete = new System.Windows.Forms.PictureBox();
            this.pictureBox_Edit = new System.Windows.Forms.PictureBox();
            this.panel_Footer = new System.Windows.Forms.Panel();
            this.pictureBox_Label = new System.Windows.Forms.PictureBox();
            this.pictureBox_Store = new System.Windows.Forms.PictureBox();
            this.pictureBox_Image = new System.Windows.Forms.PictureBox();
            this.pictureBox_ColorPallet = new System.Windows.Forms.PictureBox();
            this.pictureBox_Alarm = new System.Windows.Forms.PictureBox();
            this.panel_Body = new System.Windows.Forms.Panel();
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.panel_ColorPallet = new System.Windows.Forms.Panel();
            this.pictureBox_Color1 = new System.Windows.Forms.PictureBox();
            this.pictureBox_Color2 = new System.Windows.Forms.PictureBox();
            this.pictureBox_Color3 = new System.Windows.Forms.PictureBox();
            this.pictureBox_Color4 = new System.Windows.Forms.PictureBox();
            this.pictureBox_Color5 = new System.Windows.Forms.PictureBox();
            this.panel_Header.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Delete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Edit)).BeginInit();
            this.panel_Footer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Label)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Store)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Image)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ColorPallet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Alarm)).BeginInit();
            this.panel_Body.SuspendLayout();
            this.panel_ColorPallet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Color1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Color2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Color3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Color4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Color5)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_Header
            // 
            this.panel_Header.Controls.Add(this.pictureBox_Delete);
            this.panel_Header.Controls.Add(this.pictureBox_Edit);
            this.panel_Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_Header.Location = new System.Drawing.Point(0, 0);
            this.panel_Header.Name = "panel_Header";
            this.panel_Header.Size = new System.Drawing.Size(202, 40);
            this.panel_Header.TabIndex = 0;
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
            this.pictureBox_Delete.Click += new System.EventHandler(this.pictureBox_Delete_Click);
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
            this.panel_Footer.Size = new System.Drawing.Size(202, 40);
            this.panel_Footer.TabIndex = 1;
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
            this.pictureBox_Image.Click += new System.EventHandler(this.pictureBox_Image_Click);
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
            this.pictureBox_ColorPallet.Click += new System.EventHandler(this.pictureBox_ColorPallet_Click);
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
            // panel_Body
            // 
            this.panel_Body.BackColor = System.Drawing.Color.White;
            this.panel_Body.Controls.Add(this.panel_ColorPallet);
            this.panel_Body.Controls.Add(this.richTextBox);
            this.panel_Body.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Body.Location = new System.Drawing.Point(0, 40);
            this.panel_Body.Name = "panel_Body";
            this.panel_Body.Size = new System.Drawing.Size(202, 140);
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
            this.richTextBox.Size = new System.Drawing.Size(202, 140);
            this.richTextBox.TabIndex = 0;
            this.richTextBox.Text = "";
            this.richTextBox.TextChanged += new System.EventHandler(this.richTextBox_TextChanged);
            this.richTextBox.DoubleClick += new System.EventHandler(this.richTextBox_DoubleClick);
            // 
            // panel_ColorPallet
            // 
            this.panel_ColorPallet.BackColor = System.Drawing.Color.GreenYellow;
            this.panel_ColorPallet.Controls.Add(this.pictureBox_Color5);
            this.panel_ColorPallet.Controls.Add(this.pictureBox_Color4);
            this.panel_ColorPallet.Controls.Add(this.pictureBox_Color3);
            this.panel_ColorPallet.Controls.Add(this.pictureBox_Color2);
            this.panel_ColorPallet.Controls.Add(this.pictureBox_Color1);
            this.panel_ColorPallet.Location = new System.Drawing.Point(4, 3);
            this.panel_ColorPallet.Margin = new System.Windows.Forms.Padding(0);
            this.panel_ColorPallet.Name = "panel_ColorPallet";
            this.panel_ColorPallet.Size = new System.Drawing.Size(175, 40);
            this.panel_ColorPallet.TabIndex = 1;
            // 
            // pictureBox_Color1
            // 
            this.pictureBox_Color1.BackColor = System.Drawing.Color.Orange;
            this.pictureBox_Color1.BackgroundImage = global::WellaTodo.Properties.Resources.outline_palette_black_24dp;
            this.pictureBox_Color1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox_Color1.Location = new System.Drawing.Point(4, 4);
            this.pictureBox_Color1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox_Color1.Name = "pictureBox_Color1";
            this.pictureBox_Color1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox_Color1.TabIndex = 0;
            this.pictureBox_Color1.TabStop = false;
            this.pictureBox_Color1.Click += new System.EventHandler(this.pictureBox_Color_Click);
            // 
            // pictureBox_Color2
            // 
            this.pictureBox_Color2.BackColor = System.Drawing.Color.Turquoise;
            this.pictureBox_Color2.BackgroundImage = global::WellaTodo.Properties.Resources.outline_palette_black_24dp;
            this.pictureBox_Color2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox_Color2.Location = new System.Drawing.Point(38, 4);
            this.pictureBox_Color2.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox_Color2.Name = "pictureBox_Color2";
            this.pictureBox_Color2.Size = new System.Drawing.Size(32, 32);
            this.pictureBox_Color2.TabIndex = 0;
            this.pictureBox_Color2.TabStop = false;
            this.pictureBox_Color2.Click += new System.EventHandler(this.pictureBox_Color_Click);
            // 
            // pictureBox_Color3
            // 
            this.pictureBox_Color3.BackColor = System.Drawing.Color.Tomato;
            this.pictureBox_Color3.BackgroundImage = global::WellaTodo.Properties.Resources.outline_palette_black_24dp;
            this.pictureBox_Color3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox_Color3.Location = new System.Drawing.Point(72, 4);
            this.pictureBox_Color3.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox_Color3.Name = "pictureBox_Color3";
            this.pictureBox_Color3.Size = new System.Drawing.Size(32, 32);
            this.pictureBox_Color3.TabIndex = 0;
            this.pictureBox_Color3.TabStop = false;
            this.pictureBox_Color3.Click += new System.EventHandler(this.pictureBox_Color_Click);
            // 
            // pictureBox_Color4
            // 
            this.pictureBox_Color4.BackColor = System.Drawing.Color.Yellow;
            this.pictureBox_Color4.BackgroundImage = global::WellaTodo.Properties.Resources.outline_palette_black_24dp;
            this.pictureBox_Color4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox_Color4.Location = new System.Drawing.Point(106, 4);
            this.pictureBox_Color4.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox_Color4.Name = "pictureBox_Color4";
            this.pictureBox_Color4.Size = new System.Drawing.Size(32, 32);
            this.pictureBox_Color4.TabIndex = 0;
            this.pictureBox_Color4.TabStop = false;
            this.pictureBox_Color4.Click += new System.EventHandler(this.pictureBox_Color_Click);
            // 
            // pictureBox_Color5
            // 
            this.pictureBox_Color5.BackColor = System.Drawing.Color.Violet;
            this.pictureBox_Color5.BackgroundImage = global::WellaTodo.Properties.Resources.outline_palette_black_24dp;
            this.pictureBox_Color5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox_Color5.Location = new System.Drawing.Point(140, 4);
            this.pictureBox_Color5.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox_Color5.Name = "pictureBox_Color5";
            this.pictureBox_Color5.Size = new System.Drawing.Size(32, 32);
            this.pictureBox_Color5.TabIndex = 0;
            this.pictureBox_Color5.TabStop = false;
            this.pictureBox_Color5.Click += new System.EventHandler(this.pictureBox_Color_Click);
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
            this.Size = new System.Drawing.Size(202, 220);
            this.Load += new System.EventHandler(this.Post_it_Load);
            this.Resize += new System.EventHandler(this.Post_it_Resize);
            this.panel_Header.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Delete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Edit)).EndInit();
            this.panel_Footer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Label)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Store)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Image)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ColorPallet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Alarm)).EndInit();
            this.panel_Body.ResumeLayout(false);
            this.panel_ColorPallet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Color1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Color2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Color3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Color4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Color5)).EndInit();
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
        private System.Windows.Forms.Panel panel_ColorPallet;
        private System.Windows.Forms.PictureBox pictureBox_Color2;
        private System.Windows.Forms.PictureBox pictureBox_Color1;
        private System.Windows.Forms.PictureBox pictureBox_Color5;
        private System.Windows.Forms.PictureBox pictureBox_Color4;
        private System.Windows.Forms.PictureBox pictureBox_Color3;
    }
}
