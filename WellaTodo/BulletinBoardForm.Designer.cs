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
            this.label_Menu = new System.Windows.Forms.Label();
            this.label_Title = new System.Windows.Forms.Label();
            this.panel_Menu = new System.Windows.Forms.Panel();
            this.button_Label_Common = new System.Windows.Forms.Button();
            this.button_Label_Blue = new System.Windows.Forms.Button();
            this.button_Label_Green = new System.Windows.Forms.Button();
            this.button_Label_Yellow = new System.Windows.Forms.Button();
            this.button_Label_Orange = new System.Windows.Forms.Button();
            this.button_Label_Red = new System.Windows.Forms.Button();
            this.button_Archive = new System.Windows.Forms.Button();
            this.button_Alarm = new System.Windows.Forms.Button();
            this.button_Memo = new System.Windows.Forms.Button();
            this.panel_Bulletin = new System.Windows.Forms.Panel();
            this.panel_Header.SuspendLayout();
            this.panel_Menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_Header
            // 
            this.panel_Header.Controls.Add(this.label_Menu);
            this.panel_Header.Controls.Add(this.label_Title);
            this.panel_Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_Header.Location = new System.Drawing.Point(0, 0);
            this.panel_Header.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel_Header.Name = "panel_Header";
            this.panel_Header.Size = new System.Drawing.Size(700, 40);
            this.panel_Header.TabIndex = 0;
            // 
            // label_Menu
            // 
            this.label_Menu.Image = global::WellaTodo.Properties.Resources.outline_apps_black_24dp;
            this.label_Menu.Location = new System.Drawing.Point(8, 7);
            this.label_Menu.Margin = new System.Windows.Forms.Padding(0);
            this.label_Menu.Name = "label_Menu";
            this.label_Menu.Size = new System.Drawing.Size(28, 26);
            this.label_Menu.TabIndex = 1;
            this.label_Menu.Click += new System.EventHandler(this.label_Menu_Click);
            // 
            // label_Title
            // 
            this.label_Title.Font = new System.Drawing.Font("휴먼둥근헤드라인", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_Title.Location = new System.Drawing.Point(36, 1);
            this.label_Title.Margin = new System.Windows.Forms.Padding(0);
            this.label_Title.Name = "label_Title";
            this.label_Title.Size = new System.Drawing.Size(350, 38);
            this.label_Title.TabIndex = 0;
            this.label_Title.Text = "Bulletin Board";
            this.label_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel_Menu
            // 
            this.panel_Menu.Controls.Add(this.button_Label_Common);
            this.panel_Menu.Controls.Add(this.button_Label_Blue);
            this.panel_Menu.Controls.Add(this.button_Label_Green);
            this.panel_Menu.Controls.Add(this.button_Label_Yellow);
            this.panel_Menu.Controls.Add(this.button_Label_Orange);
            this.panel_Menu.Controls.Add(this.button_Label_Red);
            this.panel_Menu.Controls.Add(this.button_Archive);
            this.panel_Menu.Controls.Add(this.button_Alarm);
            this.panel_Menu.Controls.Add(this.button_Memo);
            this.panel_Menu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_Menu.Location = new System.Drawing.Point(0, 40);
            this.panel_Menu.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel_Menu.Name = "panel_Menu";
            this.panel_Menu.Size = new System.Drawing.Size(105, 320);
            this.panel_Menu.TabIndex = 1;
            // 
            // button_Label_Common
            // 
            this.button_Label_Common.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_Label_Common.FlatAppearance.BorderSize = 0;
            this.button_Label_Common.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Cyan;
            this.button_Label_Common.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightCyan;
            this.button_Label_Common.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Label_Common.Image = global::WellaTodo.Properties.Resources.outline_label_black_24dp;
            this.button_Label_Common.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_Label_Common.Location = new System.Drawing.Point(0, 256);
            this.button_Label_Common.Margin = new System.Windows.Forms.Padding(0);
            this.button_Label_Common.Name = "button_Label_Common";
            this.button_Label_Common.Size = new System.Drawing.Size(105, 32);
            this.button_Label_Common.TabIndex = 9;
            this.button_Label_Common.Text = "라벨(해제)";
            this.button_Label_Common.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_Label_Common.UseVisualStyleBackColor = true;
            this.button_Label_Common.Click += new System.EventHandler(this.button_Label_Click);
            // 
            // button_Label_Blue
            // 
            this.button_Label_Blue.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_Label_Blue.FlatAppearance.BorderSize = 0;
            this.button_Label_Blue.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Cyan;
            this.button_Label_Blue.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightCyan;
            this.button_Label_Blue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Label_Blue.Image = global::WellaTodo.Properties.Resources.outline_label_blue_24dp;
            this.button_Label_Blue.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_Label_Blue.Location = new System.Drawing.Point(0, 224);
            this.button_Label_Blue.Margin = new System.Windows.Forms.Padding(0);
            this.button_Label_Blue.Name = "button_Label_Blue";
            this.button_Label_Blue.Size = new System.Drawing.Size(105, 32);
            this.button_Label_Blue.TabIndex = 8;
            this.button_Label_Blue.Text = "라벨(파랑)";
            this.button_Label_Blue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_Label_Blue.UseVisualStyleBackColor = true;
            this.button_Label_Blue.Click += new System.EventHandler(this.button_Label_Click);
            // 
            // button_Label_Green
            // 
            this.button_Label_Green.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_Label_Green.FlatAppearance.BorderSize = 0;
            this.button_Label_Green.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Cyan;
            this.button_Label_Green.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightCyan;
            this.button_Label_Green.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Label_Green.Image = global::WellaTodo.Properties.Resources.outline_label_green_24dp;
            this.button_Label_Green.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_Label_Green.Location = new System.Drawing.Point(0, 192);
            this.button_Label_Green.Margin = new System.Windows.Forms.Padding(0);
            this.button_Label_Green.Name = "button_Label_Green";
            this.button_Label_Green.Size = new System.Drawing.Size(105, 32);
            this.button_Label_Green.TabIndex = 7;
            this.button_Label_Green.Text = "라벨(초록)";
            this.button_Label_Green.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_Label_Green.UseVisualStyleBackColor = true;
            this.button_Label_Green.Click += new System.EventHandler(this.button_Label_Click);
            // 
            // button_Label_Yellow
            // 
            this.button_Label_Yellow.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_Label_Yellow.FlatAppearance.BorderSize = 0;
            this.button_Label_Yellow.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Cyan;
            this.button_Label_Yellow.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightCyan;
            this.button_Label_Yellow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Label_Yellow.Image = global::WellaTodo.Properties.Resources.outline_label_yellow_24dp;
            this.button_Label_Yellow.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_Label_Yellow.Location = new System.Drawing.Point(0, 160);
            this.button_Label_Yellow.Margin = new System.Windows.Forms.Padding(0);
            this.button_Label_Yellow.Name = "button_Label_Yellow";
            this.button_Label_Yellow.Size = new System.Drawing.Size(105, 32);
            this.button_Label_Yellow.TabIndex = 6;
            this.button_Label_Yellow.Text = "라벨(노랑)";
            this.button_Label_Yellow.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_Label_Yellow.UseVisualStyleBackColor = true;
            this.button_Label_Yellow.Click += new System.EventHandler(this.button_Label_Click);
            // 
            // button_Label_Orange
            // 
            this.button_Label_Orange.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_Label_Orange.FlatAppearance.BorderSize = 0;
            this.button_Label_Orange.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Cyan;
            this.button_Label_Orange.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightCyan;
            this.button_Label_Orange.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Label_Orange.Image = global::WellaTodo.Properties.Resources.outline_label_orange_24dp;
            this.button_Label_Orange.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_Label_Orange.Location = new System.Drawing.Point(0, 128);
            this.button_Label_Orange.Margin = new System.Windows.Forms.Padding(0);
            this.button_Label_Orange.Name = "button_Label_Orange";
            this.button_Label_Orange.Size = new System.Drawing.Size(105, 32);
            this.button_Label_Orange.TabIndex = 5;
            this.button_Label_Orange.Text = "라벨(주황)";
            this.button_Label_Orange.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_Label_Orange.UseVisualStyleBackColor = true;
            this.button_Label_Orange.Click += new System.EventHandler(this.button_Label_Click);
            // 
            // button_Label_Red
            // 
            this.button_Label_Red.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_Label_Red.FlatAppearance.BorderSize = 0;
            this.button_Label_Red.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Cyan;
            this.button_Label_Red.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightCyan;
            this.button_Label_Red.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Label_Red.Image = global::WellaTodo.Properties.Resources.outline_label_red_24dp;
            this.button_Label_Red.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_Label_Red.Location = new System.Drawing.Point(0, 96);
            this.button_Label_Red.Margin = new System.Windows.Forms.Padding(0);
            this.button_Label_Red.Name = "button_Label_Red";
            this.button_Label_Red.Size = new System.Drawing.Size(105, 32);
            this.button_Label_Red.TabIndex = 4;
            this.button_Label_Red.Text = "라벨(빨강)";
            this.button_Label_Red.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_Label_Red.UseVisualStyleBackColor = true;
            this.button_Label_Red.Click += new System.EventHandler(this.button_Label_Click);
            // 
            // button_Archive
            // 
            this.button_Archive.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_Archive.FlatAppearance.BorderSize = 0;
            this.button_Archive.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Cyan;
            this.button_Archive.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightCyan;
            this.button_Archive.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Archive.Image = global::WellaTodo.Properties.Resources.outline_archive_black_24dp;
            this.button_Archive.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_Archive.Location = new System.Drawing.Point(0, 64);
            this.button_Archive.Margin = new System.Windows.Forms.Padding(0);
            this.button_Archive.Name = "button_Archive";
            this.button_Archive.Size = new System.Drawing.Size(105, 32);
            this.button_Archive.TabIndex = 3;
            this.button_Archive.Text = "보관처리";
            this.button_Archive.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_Archive.UseVisualStyleBackColor = true;
            this.button_Archive.Click += new System.EventHandler(this.button_Archive_Click);
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
            this.button_Alarm.Location = new System.Drawing.Point(0, 32);
            this.button_Alarm.Margin = new System.Windows.Forms.Padding(0);
            this.button_Alarm.Name = "button_Alarm";
            this.button_Alarm.Size = new System.Drawing.Size(105, 32);
            this.button_Alarm.TabIndex = 2;
            this.button_Alarm.Text = "알  람";
            this.button_Alarm.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_Alarm.UseVisualStyleBackColor = true;
            this.button_Alarm.Click += new System.EventHandler(this.button_Alarm_Click);
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
            this.button_Memo.Size = new System.Drawing.Size(105, 32);
            this.button_Memo.TabIndex = 1;
            this.button_Memo.Text = "메  모";
            this.button_Memo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_Memo.UseVisualStyleBackColor = true;
            this.button_Memo.Click += new System.EventHandler(this.button_Memo_Click);
            // 
            // panel_Bulletin
            // 
            this.panel_Bulletin.AutoScroll = true;
            this.panel_Bulletin.BackColor = System.Drawing.Color.White;
            this.panel_Bulletin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Bulletin.Location = new System.Drawing.Point(105, 40);
            this.panel_Bulletin.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel_Bulletin.Name = "panel_Bulletin";
            this.panel_Bulletin.Size = new System.Drawing.Size(595, 320);
            this.panel_Bulletin.TabIndex = 2;
            // 
            // BulletinBoardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(700, 360);
            this.Controls.Add(this.panel_Bulletin);
            this.Controls.Add(this.panel_Menu);
            this.Controls.Add(this.panel_Header);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "BulletinBoardForm";
            this.Text = "BulletinBoardForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BulletinBoardForm_FormClosing);
            this.Load += new System.EventHandler(this.BulletinBoardForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.BulletinBoardForm_Paint);
            this.Resize += new System.EventHandler(this.BulletinBoardForm_Resize);
            this.panel_Header.ResumeLayout(false);
            this.panel_Menu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_Header;
        private System.Windows.Forms.Panel panel_Menu;
        private System.Windows.Forms.Panel panel_Bulletin;
        private System.Windows.Forms.Label label_Title;
        private System.Windows.Forms.Label label_Menu;
        private System.Windows.Forms.Button button_Archive;
        private System.Windows.Forms.Button button_Alarm;
        private System.Windows.Forms.Button button_Memo;
        private System.Windows.Forms.Button button_Label_Red;
        private System.Windows.Forms.Button button_Label_Blue;
        private System.Windows.Forms.Button button_Label_Green;
        private System.Windows.Forms.Button button_Label_Yellow;
        private System.Windows.Forms.Button button_Label_Orange;
        private System.Windows.Forms.Button button_Label_Common;
    }
}