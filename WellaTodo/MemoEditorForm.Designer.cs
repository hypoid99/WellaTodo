namespace WellaTodo
{
    partial class MemoEditorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel_Header = new System.Windows.Forms.Panel();
            this.panel_Footer = new System.Windows.Forms.Panel();
            this.panel_Body = new System.Windows.Forms.Panel();
            this.pictureBox_Add_Note = new System.Windows.Forms.PictureBox();
            this.pictureBox_More_Hori = new System.Windows.Forms.PictureBox();
            this.pictureBox_Close = new System.Windows.Forms.PictureBox();
            this.pictureBox_Image = new System.Windows.Forms.PictureBox();
            this.pictureBox_Strikeout = new System.Windows.Forms.PictureBox();
            this.pictureBox_Underline = new System.Windows.Forms.PictureBox();
            this.pictureBox_Italic = new System.Windows.Forms.PictureBox();
            this.pictureBox_Bold = new System.Windows.Forms.PictureBox();
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.panel_Header.SuspendLayout();
            this.panel_Footer.SuspendLayout();
            this.panel_Body.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Add_Note)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_More_Hori)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Close)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Image)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Strikeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Underline)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Italic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Bold)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_Header
            // 
            this.panel_Header.Controls.Add(this.pictureBox_More_Hori);
            this.panel_Header.Controls.Add(this.pictureBox_Close);
            this.panel_Header.Controls.Add(this.pictureBox_Add_Note);
            this.panel_Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_Header.Location = new System.Drawing.Point(0, 0);
            this.panel_Header.Name = "panel_Header";
            this.panel_Header.Size = new System.Drawing.Size(534, 40);
            this.panel_Header.TabIndex = 0;
            // 
            // panel_Footer
            // 
            this.panel_Footer.Controls.Add(this.pictureBox_Image);
            this.panel_Footer.Controls.Add(this.pictureBox_Strikeout);
            this.panel_Footer.Controls.Add(this.pictureBox_Underline);
            this.panel_Footer.Controls.Add(this.pictureBox_Italic);
            this.panel_Footer.Controls.Add(this.pictureBox_Bold);
            this.panel_Footer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_Footer.Location = new System.Drawing.Point(0, 320);
            this.panel_Footer.Name = "panel_Footer";
            this.panel_Footer.Size = new System.Drawing.Size(534, 40);
            this.panel_Footer.TabIndex = 1;
            // 
            // panel_Body
            // 
            this.panel_Body.Controls.Add(this.richTextBox);
            this.panel_Body.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Body.Location = new System.Drawing.Point(0, 40);
            this.panel_Body.Name = "panel_Body";
            this.panel_Body.Size = new System.Drawing.Size(534, 280);
            this.panel_Body.TabIndex = 2;
            // 
            // pictureBox_Add_Note
            // 
            this.pictureBox_Add_Note.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_Add_Note.BackgroundImage = global::WellaTodo.Properties.Resources.outline_add_circle_outline_black_24dp;
            this.pictureBox_Add_Note.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox_Add_Note.Location = new System.Drawing.Point(4, 4);
            this.pictureBox_Add_Note.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox_Add_Note.Name = "pictureBox_Add_Note";
            this.pictureBox_Add_Note.Size = new System.Drawing.Size(32, 32);
            this.pictureBox_Add_Note.TabIndex = 3;
            this.pictureBox_Add_Note.TabStop = false;
            // 
            // pictureBox_More_Hori
            // 
            this.pictureBox_More_Hori.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_More_Hori.BackgroundImage = global::WellaTodo.Properties.Resources.outline_more_horiz_black_24dp;
            this.pictureBox_More_Hori.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox_More_Hori.Location = new System.Drawing.Point(457, 4);
            this.pictureBox_More_Hori.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox_More_Hori.Name = "pictureBox_More_Hori";
            this.pictureBox_More_Hori.Size = new System.Drawing.Size(32, 32);
            this.pictureBox_More_Hori.TabIndex = 4;
            this.pictureBox_More_Hori.TabStop = false;
            // 
            // pictureBox_Close
            // 
            this.pictureBox_Close.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_Close.BackgroundImage = global::WellaTodo.Properties.Resources.outline_close_black_24dp;
            this.pictureBox_Close.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox_Close.Location = new System.Drawing.Point(489, 4);
            this.pictureBox_Close.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox_Close.Name = "pictureBox_Close";
            this.pictureBox_Close.Size = new System.Drawing.Size(32, 32);
            this.pictureBox_Close.TabIndex = 5;
            this.pictureBox_Close.TabStop = false;
            this.pictureBox_Close.Click += new System.EventHandler(this.pictureBox_Close_Click);
            // 
            // pictureBox_Image
            // 
            this.pictureBox_Image.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_Image.BackgroundImage = global::WellaTodo.Properties.Resources.outline_insert_photo_black_24dp;
            this.pictureBox_Image.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox_Image.Location = new System.Drawing.Point(132, 4);
            this.pictureBox_Image.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox_Image.Name = "pictureBox_Image";
            this.pictureBox_Image.Size = new System.Drawing.Size(32, 32);
            this.pictureBox_Image.TabIndex = 1;
            this.pictureBox_Image.TabStop = false;
            // 
            // pictureBox_Strikeout
            // 
            this.pictureBox_Strikeout.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_Strikeout.BackgroundImage = global::WellaTodo.Properties.Resources.outline_format_strikethrough_black_24dp;
            this.pictureBox_Strikeout.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox_Strikeout.Location = new System.Drawing.Point(100, 4);
            this.pictureBox_Strikeout.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox_Strikeout.Name = "pictureBox_Strikeout";
            this.pictureBox_Strikeout.Size = new System.Drawing.Size(32, 32);
            this.pictureBox_Strikeout.TabIndex = 2;
            this.pictureBox_Strikeout.TabStop = false;
            // 
            // pictureBox_Underline
            // 
            this.pictureBox_Underline.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_Underline.BackgroundImage = global::WellaTodo.Properties.Resources.outline_format_underlined_black_24dp;
            this.pictureBox_Underline.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox_Underline.Location = new System.Drawing.Point(68, 4);
            this.pictureBox_Underline.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox_Underline.Name = "pictureBox_Underline";
            this.pictureBox_Underline.Size = new System.Drawing.Size(32, 32);
            this.pictureBox_Underline.TabIndex = 3;
            this.pictureBox_Underline.TabStop = false;
            // 
            // pictureBox_Italic
            // 
            this.pictureBox_Italic.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_Italic.BackgroundImage = global::WellaTodo.Properties.Resources.outline_format_italic_black_24dp;
            this.pictureBox_Italic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox_Italic.Location = new System.Drawing.Point(36, 4);
            this.pictureBox_Italic.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox_Italic.Name = "pictureBox_Italic";
            this.pictureBox_Italic.Size = new System.Drawing.Size(32, 32);
            this.pictureBox_Italic.TabIndex = 4;
            this.pictureBox_Italic.TabStop = false;
            // 
            // pictureBox_Bold
            // 
            this.pictureBox_Bold.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_Bold.BackgroundImage = global::WellaTodo.Properties.Resources.outline_format_bold_black_24dp;
            this.pictureBox_Bold.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox_Bold.Location = new System.Drawing.Point(4, 4);
            this.pictureBox_Bold.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox_Bold.Name = "pictureBox_Bold";
            this.pictureBox_Bold.Size = new System.Drawing.Size(32, 32);
            this.pictureBox_Bold.TabIndex = 5;
            this.pictureBox_Bold.TabStop = false;
            // 
            // richTextBox
            // 
            this.richTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox.HideSelection = false;
            this.richTextBox.Location = new System.Drawing.Point(0, 0);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.Size = new System.Drawing.Size(534, 280);
            this.richTextBox.TabIndex = 1;
            this.richTextBox.Text = "";
            // 
            // MemoEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 360);
            this.ControlBox = false;
            this.Controls.Add(this.panel_Body);
            this.Controls.Add(this.panel_Footer);
            this.Controls.Add(this.panel_Header);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MemoEditorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.panel_Header.ResumeLayout(false);
            this.panel_Footer.ResumeLayout(false);
            this.panel_Body.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Add_Note)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_More_Hori)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Close)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Image)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Strikeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Underline)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Italic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Bold)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_Header;
        private System.Windows.Forms.Panel panel_Footer;
        private System.Windows.Forms.Panel panel_Body;
        private System.Windows.Forms.PictureBox pictureBox_Add_Note;
        private System.Windows.Forms.PictureBox pictureBox_More_Hori;
        private System.Windows.Forms.PictureBox pictureBox_Close;
        private System.Windows.Forms.PictureBox pictureBox_Image;
        private System.Windows.Forms.PictureBox pictureBox_Strikeout;
        private System.Windows.Forms.PictureBox pictureBox_Underline;
        private System.Windows.Forms.PictureBox pictureBox_Italic;
        private System.Windows.Forms.PictureBox pictureBox_Bold;
        private System.Windows.Forms.RichTextBox richTextBox;
    }
}