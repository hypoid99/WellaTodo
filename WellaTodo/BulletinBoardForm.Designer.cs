namespace WellaTodo
{
    partial class BulletinBoardForm
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
            this.pictureBox_Add_Note = new System.Windows.Forms.PictureBox();
            this.label_Menu = new System.Windows.Forms.Label();
            this.label_Title = new System.Windows.Forms.Label();
            this.panel_Menu = new System.Windows.Forms.Panel();
            this.button_Label = new System.Windows.Forms.Button();
            this.button_Store = new System.Windows.Forms.Button();
            this.button_Alarm = new System.Windows.Forms.Button();
            this.button_Memo = new System.Windows.Forms.Button();
            this.panel_Bulletin = new System.Windows.Forms.Panel();
            this.panel_Header.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Add_Note)).BeginInit();
            this.panel_Menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_Header
            // 
            this.panel_Header.Controls.Add(this.pictureBox_Add_Note);
            this.panel_Header.Controls.Add(this.label_Menu);
            this.panel_Header.Controls.Add(this.label_Title);
            this.panel_Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_Header.Location = new System.Drawing.Point(0, 0);
            this.panel_Header.Name = "panel_Header";
            this.panel_Header.Size = new System.Drawing.Size(800, 50);
            this.panel_Header.TabIndex = 0;
            // 
            // pictureBox_Add_Note
            // 
            this.pictureBox_Add_Note.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_Add_Note.BackgroundImage = global::WellaTodo.Properties.Resources.outline_add_circle_outline_black_24dp;
            this.pictureBox_Add_Note.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox_Add_Note.Location = new System.Drawing.Point(749, 4);
            this.pictureBox_Add_Note.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox_Add_Note.Name = "pictureBox_Add_Note";
            this.pictureBox_Add_Note.Size = new System.Drawing.Size(42, 42);
            this.pictureBox_Add_Note.TabIndex = 2;
            this.pictureBox_Add_Note.TabStop = false;
            this.pictureBox_Add_Note.Click += new System.EventHandler(this.pictureBox_Add_Note_Click);
            // 
            // label_Menu
            // 
            this.label_Menu.Image = global::WellaTodo.Properties.Resources.outline_apps_black_24dp;
            this.label_Menu.Location = new System.Drawing.Point(9, 9);
            this.label_Menu.Margin = new System.Windows.Forms.Padding(0);
            this.label_Menu.Name = "label_Menu";
            this.label_Menu.Size = new System.Drawing.Size(32, 32);
            this.label_Menu.TabIndex = 1;
            this.label_Menu.Click += new System.EventHandler(this.label_Menu_Click);
            // 
            // label_Title
            // 
            this.label_Title.Font = new System.Drawing.Font("휴먼둥근헤드라인", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_Title.Location = new System.Drawing.Point(41, 1);
            this.label_Title.Margin = new System.Windows.Forms.Padding(0);
            this.label_Title.Name = "label_Title";
            this.label_Title.Size = new System.Drawing.Size(207, 48);
            this.label_Title.TabIndex = 0;
            this.label_Title.Text = "Bulletin Board";
            this.label_Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel_Menu
            // 
            this.panel_Menu.Controls.Add(this.button_Label);
            this.panel_Menu.Controls.Add(this.button_Store);
            this.panel_Menu.Controls.Add(this.button_Alarm);
            this.panel_Menu.Controls.Add(this.button_Memo);
            this.panel_Menu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_Menu.Location = new System.Drawing.Point(0, 50);
            this.panel_Menu.Name = "panel_Menu";
            this.panel_Menu.Size = new System.Drawing.Size(100, 400);
            this.panel_Menu.TabIndex = 1;
            // 
            // button_Label
            // 
            this.button_Label.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_Label.FlatAppearance.BorderSize = 0;
            this.button_Label.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Cyan;
            this.button_Label.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightCyan;
            this.button_Label.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Label.Image = global::WellaTodo.Properties.Resources.outline_new_label_black_24dp;
            this.button_Label.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_Label.Location = new System.Drawing.Point(0, 120);
            this.button_Label.Margin = new System.Windows.Forms.Padding(0);
            this.button_Label.Name = "button_Label";
            this.button_Label.Size = new System.Drawing.Size(100, 40);
            this.button_Label.TabIndex = 4;
            this.button_Label.Text = "라벨";
            this.button_Label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_Label.UseVisualStyleBackColor = true;
            this.button_Label.Click += new System.EventHandler(this.button_Label_Click);
            // 
            // button_Store
            // 
            this.button_Store.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_Store.FlatAppearance.BorderSize = 0;
            this.button_Store.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Cyan;
            this.button_Store.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightCyan;
            this.button_Store.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Store.Image = global::WellaTodo.Properties.Resources.outline_archive_black_24dp;
            this.button_Store.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_Store.Location = new System.Drawing.Point(0, 80);
            this.button_Store.Margin = new System.Windows.Forms.Padding(0);
            this.button_Store.Name = "button_Store";
            this.button_Store.Size = new System.Drawing.Size(100, 40);
            this.button_Store.TabIndex = 3;
            this.button_Store.Text = "보관";
            this.button_Store.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_Store.UseVisualStyleBackColor = true;
            // 
            // button_Alarm
            // 
            this.button_Alarm.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_Alarm.FlatAppearance.BorderSize = 0;
            this.button_Alarm.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Cyan;
            this.button_Alarm.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightCyan;
            this.button_Alarm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Alarm.Image = global::WellaTodo.Properties.Resources.outline_access_alarms_black_24dp;
            this.button_Alarm.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_Alarm.Location = new System.Drawing.Point(0, 40);
            this.button_Alarm.Margin = new System.Windows.Forms.Padding(0);
            this.button_Alarm.Name = "button_Alarm";
            this.button_Alarm.Size = new System.Drawing.Size(100, 40);
            this.button_Alarm.TabIndex = 2;
            this.button_Alarm.Text = "알람";
            this.button_Alarm.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_Alarm.UseVisualStyleBackColor = true;
            // 
            // button_Memo
            // 
            this.button_Memo.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_Memo.FlatAppearance.BorderSize = 0;
            this.button_Memo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Cyan;
            this.button_Memo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightCyan;
            this.button_Memo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Memo.Image = global::WellaTodo.Properties.Resources.outline_note_alt_black_24dp;
            this.button_Memo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_Memo.Location = new System.Drawing.Point(0, 0);
            this.button_Memo.Margin = new System.Windows.Forms.Padding(0);
            this.button_Memo.Name = "button_Memo";
            this.button_Memo.Size = new System.Drawing.Size(100, 40);
            this.button_Memo.TabIndex = 1;
            this.button_Memo.Text = "메모";
            this.button_Memo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_Memo.UseVisualStyleBackColor = true;
            // 
            // panel_Bulletin
            // 
            this.panel_Bulletin.AutoScroll = true;
            this.panel_Bulletin.BackColor = System.Drawing.Color.White;
            this.panel_Bulletin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Bulletin.Location = new System.Drawing.Point(100, 50);
            this.panel_Bulletin.Name = "panel_Bulletin";
            this.panel_Bulletin.Size = new System.Drawing.Size(700, 400);
            this.panel_Bulletin.TabIndex = 2;
            // 
            // BulletinBoardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel_Bulletin);
            this.Controls.Add(this.panel_Menu);
            this.Controls.Add(this.panel_Header);
            this.Name = "BulletinBoardForm";
            this.Text = "BulletinBoardForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BulletinBoardForm_FormClosing);
            this.Load += new System.EventHandler(this.BulletinBoardForm_Load);
            this.Resize += new System.EventHandler(this.BulletinBoardForm_Resize);
            this.panel_Header.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Add_Note)).EndInit();
            this.panel_Menu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_Header;
        private System.Windows.Forms.Panel panel_Menu;
        private System.Windows.Forms.Panel panel_Bulletin;
        private System.Windows.Forms.Label label_Title;
        private System.Windows.Forms.Label label_Menu;
        private System.Windows.Forms.PictureBox pictureBox_Add_Note;
        private System.Windows.Forms.Button button_Store;
        private System.Windows.Forms.Button button_Alarm;
        private System.Windows.Forms.Button button_Memo;
        private System.Windows.Forms.Button button_Label;
    }
}