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
            this.textBox_Title = new System.Windows.Forms.TextBox();
            this.label_Title = new System.Windows.Forms.Label();
            this.pictureBox_New = new System.Windows.Forms.PictureBox();
            this.pictureBox_Edit = new System.Windows.Forms.PictureBox();
            this.panel_Footer = new System.Windows.Forms.Panel();
            this.pictureBox_Delete = new System.Windows.Forms.PictureBox();
            this.pictureBox_Label = new System.Windows.Forms.PictureBox();
            this.pictureBox_Schedule = new System.Windows.Forms.PictureBox();
            this.pictureBox_Archive = new System.Windows.Forms.PictureBox();
            this.pictureBox_ColorPallet = new System.Windows.Forms.PictureBox();
            this.pictureBox_Alarm = new System.Windows.Forms.PictureBox();
            this.panel_Body = new System.Windows.Forms.Panel();
            this.panel_Information = new System.Windows.Forms.Panel();
            this.label_ScheduleDate = new System.Windows.Forms.Label();
            this.label_AlarmDate = new System.Windows.Forms.Label();
            this.panel_Schedule = new System.Windows.Forms.Panel();
            this.button_Schedule_Reset = new System.Windows.Forms.Button();
            this.button_Schedule_Set = new System.Windows.Forms.Button();
            this.panel_Alarm = new System.Windows.Forms.Panel();
            this.button_Alarm_Reset = new System.Windows.Forms.Button();
            this.button_Alarm_Set = new System.Windows.Forms.Button();
            this.panel_Tag = new System.Windows.Forms.Panel();
            this.pictureBox_Tag0 = new System.Windows.Forms.PictureBox();
            this.pictureBox_Tag5 = new System.Windows.Forms.PictureBox();
            this.pictureBox_Tag4 = new System.Windows.Forms.PictureBox();
            this.pictureBox_Tag3 = new System.Windows.Forms.PictureBox();
            this.pictureBox_Tag2 = new System.Windows.Forms.PictureBox();
            this.pictureBox_Tag1 = new System.Windows.Forms.PictureBox();
            this.panel_ColorPallet = new System.Windows.Forms.Panel();
            this.pictureBox_Color5 = new System.Windows.Forms.PictureBox();
            this.pictureBox_Color4 = new System.Windows.Forms.PictureBox();
            this.pictureBox_Color3 = new System.Windows.Forms.PictureBox();
            this.pictureBox_Color2 = new System.Windows.Forms.PictureBox();
            this.pictureBox_Color1 = new System.Windows.Forms.PictureBox();
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.panel_Header.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_New)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Edit)).BeginInit();
            this.panel_Footer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Delete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Label)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Schedule)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Archive)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ColorPallet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Alarm)).BeginInit();
            this.panel_Body.SuspendLayout();
            this.panel_Information.SuspendLayout();
            this.panel_Schedule.SuspendLayout();
            this.panel_Alarm.SuspendLayout();
            this.panel_Tag.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Tag0)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Tag5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Tag4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Tag3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Tag2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Tag1)).BeginInit();
            this.panel_ColorPallet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Color5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Color4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Color3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Color2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Color1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_Header
            // 
            this.panel_Header.Controls.Add(this.textBox_Title);
            this.panel_Header.Controls.Add(this.label_Title);
            this.panel_Header.Controls.Add(this.pictureBox_New);
            this.panel_Header.Controls.Add(this.pictureBox_Edit);
            this.panel_Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_Header.Location = new System.Drawing.Point(0, 0);
            this.panel_Header.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel_Header.Name = "panel_Header";
            this.panel_Header.Size = new System.Drawing.Size(195, 32);
            this.panel_Header.TabIndex = 0;
            // 
            // textBox_Title
            // 
            this.textBox_Title.Location = new System.Drawing.Point(34, 6);
            this.textBox_Title.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_Title.Name = "textBox_Title";
            this.textBox_Title.Size = new System.Drawing.Size(125, 21);
            this.textBox_Title.TabIndex = 2;
            this.textBox_Title.Enter += new System.EventHandler(this.textBox_Title_Enter);
            this.textBox_Title.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_Title_KeyDown);
            this.textBox_Title.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox_Title_KeyUp);
            this.textBox_Title.Leave += new System.EventHandler(this.textBox_Title_Leave);
            this.textBox_Title.MouseDown += new System.Windows.Forms.MouseEventHandler(this.textBox_Title_MouseDown);
            // 
            // label_Title
            // 
            this.label_Title.Location = new System.Drawing.Point(34, 3);
            this.label_Title.Name = "label_Title";
            this.label_Title.Size = new System.Drawing.Size(124, 26);
            this.label_Title.TabIndex = 1;
            this.label_Title.Text = "제목";
            this.label_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label_Title.Click += new System.EventHandler(this.label_Title_Click);
            // 
            // pictureBox_New
            // 
            this.pictureBox_New.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_New.BackgroundImage = global::WellaTodo.Properties.Resources.outline_add_circle_outline_black_24dp;
            this.pictureBox_New.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox_New.Location = new System.Drawing.Point(161, 3);
            this.pictureBox_New.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox_New.Name = "pictureBox_New";
            this.pictureBox_New.Size = new System.Drawing.Size(28, 26);
            this.pictureBox_New.TabIndex = 0;
            this.pictureBox_New.TabStop = false;
            this.pictureBox_New.Click += new System.EventHandler(this.pictureBox_New_Click);
            this.pictureBox_New.MouseEnter += new System.EventHandler(this.pictureBox_New_MouseEnter);
            this.pictureBox_New.MouseLeave += new System.EventHandler(this.pictureBox_New_MouseLeave);
            // 
            // pictureBox_Edit
            // 
            this.pictureBox_Edit.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_Edit.BackgroundImage = global::WellaTodo.Properties.Resources.outline_mode_edit_black_24dp;
            this.pictureBox_Edit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox_Edit.Location = new System.Drawing.Point(4, 3);
            this.pictureBox_Edit.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox_Edit.Name = "pictureBox_Edit";
            this.pictureBox_Edit.Size = new System.Drawing.Size(28, 26);
            this.pictureBox_Edit.TabIndex = 0;
            this.pictureBox_Edit.TabStop = false;
            this.pictureBox_Edit.Click += new System.EventHandler(this.pictureBox_Edit_Click);
            this.pictureBox_Edit.MouseEnter += new System.EventHandler(this.pictureBox_Edit_MouseEnter);
            this.pictureBox_Edit.MouseLeave += new System.EventHandler(this.pictureBox_Edit_MouseLeave);
            // 
            // panel_Footer
            // 
            this.panel_Footer.Controls.Add(this.pictureBox_Delete);
            this.panel_Footer.Controls.Add(this.pictureBox_Label);
            this.panel_Footer.Controls.Add(this.pictureBox_Schedule);
            this.panel_Footer.Controls.Add(this.pictureBox_Archive);
            this.panel_Footer.Controls.Add(this.pictureBox_ColorPallet);
            this.panel_Footer.Controls.Add(this.pictureBox_Alarm);
            this.panel_Footer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_Footer.Location = new System.Drawing.Point(0, 196);
            this.panel_Footer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel_Footer.Name = "panel_Footer";
            this.panel_Footer.Size = new System.Drawing.Size(195, 32);
            this.panel_Footer.TabIndex = 1;
            // 
            // pictureBox_Delete
            // 
            this.pictureBox_Delete.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_Delete.BackgroundImage = global::WellaTodo.Properties.Resources.outline_delete_black_24dp;
            this.pictureBox_Delete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox_Delete.Location = new System.Drawing.Point(161, 3);
            this.pictureBox_Delete.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox_Delete.Name = "pictureBox_Delete";
            this.pictureBox_Delete.Size = new System.Drawing.Size(28, 26);
            this.pictureBox_Delete.TabIndex = 0;
            this.pictureBox_Delete.TabStop = false;
            this.pictureBox_Delete.Click += new System.EventHandler(this.pictureBox_Delete_Click);
            this.pictureBox_Delete.MouseEnter += new System.EventHandler(this.pictureBox_Delete_MouseEnter);
            this.pictureBox_Delete.MouseLeave += new System.EventHandler(this.pictureBox_Delete_MouseLeave);
            // 
            // pictureBox_Label
            // 
            this.pictureBox_Label.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_Label.BackgroundImage = global::WellaTodo.Properties.Resources.outline_label_black_24dp;
            this.pictureBox_Label.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox_Label.Location = new System.Drawing.Point(96, 3);
            this.pictureBox_Label.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox_Label.Name = "pictureBox_Label";
            this.pictureBox_Label.Size = new System.Drawing.Size(28, 26);
            this.pictureBox_Label.TabIndex = 0;
            this.pictureBox_Label.TabStop = false;
            this.pictureBox_Label.Click += new System.EventHandler(this.pictureBox_Label_Click);
            this.pictureBox_Label.MouseEnter += new System.EventHandler(this.pictureBox_Label_MouseEnter);
            this.pictureBox_Label.MouseLeave += new System.EventHandler(this.pictureBox_Label_MouseLeave);
            // 
            // pictureBox_Schedule
            // 
            this.pictureBox_Schedule.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_Schedule.BackgroundImage = global::WellaTodo.Properties.Resources.outline_schedule_black_24dp;
            this.pictureBox_Schedule.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox_Schedule.Location = new System.Drawing.Point(35, 3);
            this.pictureBox_Schedule.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox_Schedule.Name = "pictureBox_Schedule";
            this.pictureBox_Schedule.Size = new System.Drawing.Size(28, 26);
            this.pictureBox_Schedule.TabIndex = 0;
            this.pictureBox_Schedule.TabStop = false;
            this.pictureBox_Schedule.Click += new System.EventHandler(this.pictureBox_Schedule_Click);
            this.pictureBox_Schedule.MouseEnter += new System.EventHandler(this.pictureBox_Schedule_MouseEnter);
            this.pictureBox_Schedule.MouseLeave += new System.EventHandler(this.pictureBox_Schedule_MouseLeave);
            // 
            // pictureBox_Archive
            // 
            this.pictureBox_Archive.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_Archive.BackgroundImage = global::WellaTodo.Properties.Resources.outline_archive_black_24dp;
            this.pictureBox_Archive.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox_Archive.Location = new System.Drawing.Point(127, 3);
            this.pictureBox_Archive.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox_Archive.Name = "pictureBox_Archive";
            this.pictureBox_Archive.Size = new System.Drawing.Size(28, 26);
            this.pictureBox_Archive.TabIndex = 0;
            this.pictureBox_Archive.TabStop = false;
            this.pictureBox_Archive.Click += new System.EventHandler(this.pictureBox_Archive_Click);
            this.pictureBox_Archive.MouseEnter += new System.EventHandler(this.pictureBox_Archive_MouseEnter);
            this.pictureBox_Archive.MouseLeave += new System.EventHandler(this.pictureBox_Archive_MouseLeave);
            // 
            // pictureBox_ColorPallet
            // 
            this.pictureBox_ColorPallet.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_ColorPallet.BackgroundImage = global::WellaTodo.Properties.Resources.outline_palette_black_24dp;
            this.pictureBox_ColorPallet.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox_ColorPallet.Location = new System.Drawing.Point(66, 3);
            this.pictureBox_ColorPallet.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox_ColorPallet.Name = "pictureBox_ColorPallet";
            this.pictureBox_ColorPallet.Size = new System.Drawing.Size(28, 26);
            this.pictureBox_ColorPallet.TabIndex = 0;
            this.pictureBox_ColorPallet.TabStop = false;
            this.pictureBox_ColorPallet.Click += new System.EventHandler(this.pictureBox_ColorPallet_Click);
            this.pictureBox_ColorPallet.MouseEnter += new System.EventHandler(this.pictureBox_ColorPallet_MouseEnter);
            this.pictureBox_ColorPallet.MouseLeave += new System.EventHandler(this.pictureBox_ColorPallet_MouseLeave);
            // 
            // pictureBox_Alarm
            // 
            this.pictureBox_Alarm.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_Alarm.BackgroundImage = global::WellaTodo.Properties.Resources.outline_access_alarms_black_24dp;
            this.pictureBox_Alarm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox_Alarm.Location = new System.Drawing.Point(4, 3);
            this.pictureBox_Alarm.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox_Alarm.Name = "pictureBox_Alarm";
            this.pictureBox_Alarm.Size = new System.Drawing.Size(28, 26);
            this.pictureBox_Alarm.TabIndex = 0;
            this.pictureBox_Alarm.TabStop = false;
            this.pictureBox_Alarm.Click += new System.EventHandler(this.pictureBox_Alarm_Click);
            this.pictureBox_Alarm.MouseEnter += new System.EventHandler(this.pictureBox_Alarm_MouseEnter);
            this.pictureBox_Alarm.MouseLeave += new System.EventHandler(this.pictureBox_Alarm_MouseLeave);
            // 
            // panel_Body
            // 
            this.panel_Body.BackColor = System.Drawing.Color.White;
            this.panel_Body.Controls.Add(this.panel_Information);
            this.panel_Body.Controls.Add(this.panel_Schedule);
            this.panel_Body.Controls.Add(this.panel_Alarm);
            this.panel_Body.Controls.Add(this.panel_Tag);
            this.panel_Body.Controls.Add(this.panel_ColorPallet);
            this.panel_Body.Controls.Add(this.richTextBox);
            this.panel_Body.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Body.Location = new System.Drawing.Point(0, 32);
            this.panel_Body.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel_Body.Name = "panel_Body";
            this.panel_Body.Size = new System.Drawing.Size(195, 164);
            this.panel_Body.TabIndex = 2;
            // 
            // panel_Information
            // 
            this.panel_Information.BackColor = System.Drawing.SystemColors.Control;
            this.panel_Information.Controls.Add(this.label_ScheduleDate);
            this.panel_Information.Controls.Add(this.label_AlarmDate);
            this.panel_Information.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_Information.Location = new System.Drawing.Point(0, 114);
            this.panel_Information.Margin = new System.Windows.Forms.Padding(0);
            this.panel_Information.Name = "panel_Information";
            this.panel_Information.Size = new System.Drawing.Size(195, 50);
            this.panel_Information.TabIndex = 3;
            // 
            // label_ScheduleDate
            // 
            this.label_ScheduleDate.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label_ScheduleDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label_ScheduleDate.Location = new System.Drawing.Point(0, 2);
            this.label_ScheduleDate.Margin = new System.Windows.Forms.Padding(0);
            this.label_ScheduleDate.Name = "label_ScheduleDate";
            this.label_ScheduleDate.Size = new System.Drawing.Size(195, 24);
            this.label_ScheduleDate.TabIndex = 1;
            this.label_ScheduleDate.Text = "기한 : ";
            this.label_ScheduleDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_AlarmDate
            // 
            this.label_AlarmDate.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label_AlarmDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label_AlarmDate.Location = new System.Drawing.Point(0, 26);
            this.label_AlarmDate.Margin = new System.Windows.Forms.Padding(0);
            this.label_AlarmDate.Name = "label_AlarmDate";
            this.label_AlarmDate.Size = new System.Drawing.Size(195, 24);
            this.label_AlarmDate.TabIndex = 0;
            this.label_AlarmDate.Text = "알림 : ";
            this.label_AlarmDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel_Schedule
            // 
            this.panel_Schedule.BackColor = System.Drawing.SystemColors.Control;
            this.panel_Schedule.Controls.Add(this.button_Schedule_Reset);
            this.panel_Schedule.Controls.Add(this.button_Schedule_Set);
            this.panel_Schedule.Location = new System.Drawing.Point(100, 74);
            this.panel_Schedule.Margin = new System.Windows.Forms.Padding(0);
            this.panel_Schedule.Name = "panel_Schedule";
            this.panel_Schedule.Size = new System.Drawing.Size(91, 32);
            this.panel_Schedule.TabIndex = 3;
            // 
            // button_Schedule_Reset
            // 
            this.button_Schedule_Reset.Location = new System.Drawing.Point(46, 3);
            this.button_Schedule_Reset.Name = "button_Schedule_Reset";
            this.button_Schedule_Reset.Size = new System.Drawing.Size(40, 26);
            this.button_Schedule_Reset.TabIndex = 0;
            this.button_Schedule_Reset.Text = "해제";
            this.button_Schedule_Reset.UseVisualStyleBackColor = true;
            this.button_Schedule_Reset.Click += new System.EventHandler(this.button_Schedule_Reset_Click);
            // 
            // button_Schedule_Set
            // 
            this.button_Schedule_Set.Location = new System.Drawing.Point(3, 3);
            this.button_Schedule_Set.Name = "button_Schedule_Set";
            this.button_Schedule_Set.Size = new System.Drawing.Size(40, 26);
            this.button_Schedule_Set.TabIndex = 0;
            this.button_Schedule_Set.Text = "설정";
            this.button_Schedule_Set.UseVisualStyleBackColor = true;
            this.button_Schedule_Set.Click += new System.EventHandler(this.button_Schedule_Set_Click);
            // 
            // panel_Alarm
            // 
            this.panel_Alarm.BackColor = System.Drawing.SystemColors.Control;
            this.panel_Alarm.Controls.Add(this.button_Alarm_Reset);
            this.panel_Alarm.Controls.Add(this.button_Alarm_Set);
            this.panel_Alarm.Location = new System.Drawing.Point(4, 74);
            this.panel_Alarm.Margin = new System.Windows.Forms.Padding(0);
            this.panel_Alarm.Name = "panel_Alarm";
            this.panel_Alarm.Size = new System.Drawing.Size(91, 32);
            this.panel_Alarm.TabIndex = 3;
            // 
            // button_Alarm_Reset
            // 
            this.button_Alarm_Reset.Location = new System.Drawing.Point(46, 3);
            this.button_Alarm_Reset.Name = "button_Alarm_Reset";
            this.button_Alarm_Reset.Size = new System.Drawing.Size(40, 26);
            this.button_Alarm_Reset.TabIndex = 0;
            this.button_Alarm_Reset.Text = "해제";
            this.button_Alarm_Reset.UseVisualStyleBackColor = true;
            this.button_Alarm_Reset.Click += new System.EventHandler(this.button_Alarm_Reset_Click);
            // 
            // button_Alarm_Set
            // 
            this.button_Alarm_Set.Location = new System.Drawing.Point(3, 3);
            this.button_Alarm_Set.Name = "button_Alarm_Set";
            this.button_Alarm_Set.Size = new System.Drawing.Size(40, 26);
            this.button_Alarm_Set.TabIndex = 0;
            this.button_Alarm_Set.Text = "설정";
            this.button_Alarm_Set.UseVisualStyleBackColor = true;
            this.button_Alarm_Set.Click += new System.EventHandler(this.button_Alarm_Set_Click);
            // 
            // panel_Tag
            // 
            this.panel_Tag.BackColor = System.Drawing.SystemColors.Control;
            this.panel_Tag.Controls.Add(this.pictureBox_Tag0);
            this.panel_Tag.Controls.Add(this.pictureBox_Tag5);
            this.panel_Tag.Controls.Add(this.pictureBox_Tag4);
            this.panel_Tag.Controls.Add(this.pictureBox_Tag3);
            this.panel_Tag.Controls.Add(this.pictureBox_Tag2);
            this.panel_Tag.Controls.Add(this.pictureBox_Tag1);
            this.panel_Tag.Location = new System.Drawing.Point(4, 38);
            this.panel_Tag.Margin = new System.Windows.Forms.Padding(0);
            this.panel_Tag.Name = "panel_Tag";
            this.panel_Tag.Size = new System.Drawing.Size(186, 32);
            this.panel_Tag.TabIndex = 2;
            // 
            // pictureBox_Tag0
            // 
            this.pictureBox_Tag0.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_Tag0.BackgroundImage = global::WellaTodo.Properties.Resources.outline_label_black_24dp;
            this.pictureBox_Tag0.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox_Tag0.Location = new System.Drawing.Point(153, 3);
            this.pictureBox_Tag0.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox_Tag0.Name = "pictureBox_Tag0";
            this.pictureBox_Tag0.Size = new System.Drawing.Size(28, 26);
            this.pictureBox_Tag0.TabIndex = 0;
            this.pictureBox_Tag0.TabStop = false;
            this.pictureBox_Tag0.Click += new System.EventHandler(this.pictureBox_Tag_Click);
            // 
            // pictureBox_Tag5
            // 
            this.pictureBox_Tag5.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_Tag5.BackgroundImage = global::WellaTodo.Properties.Resources.outline_label_blue_24dp;
            this.pictureBox_Tag5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox_Tag5.Location = new System.Drawing.Point(122, 3);
            this.pictureBox_Tag5.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox_Tag5.Name = "pictureBox_Tag5";
            this.pictureBox_Tag5.Size = new System.Drawing.Size(28, 26);
            this.pictureBox_Tag5.TabIndex = 0;
            this.pictureBox_Tag5.TabStop = false;
            this.pictureBox_Tag5.Click += new System.EventHandler(this.pictureBox_Tag_Click);
            // 
            // pictureBox_Tag4
            // 
            this.pictureBox_Tag4.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_Tag4.BackgroundImage = global::WellaTodo.Properties.Resources.outline_label_green_24dp;
            this.pictureBox_Tag4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox_Tag4.Location = new System.Drawing.Point(93, 3);
            this.pictureBox_Tag4.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox_Tag4.Name = "pictureBox_Tag4";
            this.pictureBox_Tag4.Size = new System.Drawing.Size(28, 26);
            this.pictureBox_Tag4.TabIndex = 0;
            this.pictureBox_Tag4.TabStop = false;
            this.pictureBox_Tag4.Click += new System.EventHandler(this.pictureBox_Tag_Click);
            // 
            // pictureBox_Tag3
            // 
            this.pictureBox_Tag3.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_Tag3.BackgroundImage = global::WellaTodo.Properties.Resources.outline_label_yellow_24dp;
            this.pictureBox_Tag3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox_Tag3.Location = new System.Drawing.Point(63, 3);
            this.pictureBox_Tag3.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox_Tag3.Name = "pictureBox_Tag3";
            this.pictureBox_Tag3.Size = new System.Drawing.Size(28, 26);
            this.pictureBox_Tag3.TabIndex = 0;
            this.pictureBox_Tag3.TabStop = false;
            this.pictureBox_Tag3.Click += new System.EventHandler(this.pictureBox_Tag_Click);
            // 
            // pictureBox_Tag2
            // 
            this.pictureBox_Tag2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_Tag2.BackgroundImage = global::WellaTodo.Properties.Resources.outline_label_orange_24dp;
            this.pictureBox_Tag2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox_Tag2.Location = new System.Drawing.Point(33, 3);
            this.pictureBox_Tag2.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox_Tag2.Name = "pictureBox_Tag2";
            this.pictureBox_Tag2.Size = new System.Drawing.Size(28, 26);
            this.pictureBox_Tag2.TabIndex = 0;
            this.pictureBox_Tag2.TabStop = false;
            this.pictureBox_Tag2.Click += new System.EventHandler(this.pictureBox_Tag_Click);
            // 
            // pictureBox_Tag1
            // 
            this.pictureBox_Tag1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_Tag1.BackgroundImage = global::WellaTodo.Properties.Resources.outline_label_red_24dp;
            this.pictureBox_Tag1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox_Tag1.Location = new System.Drawing.Point(4, 3);
            this.pictureBox_Tag1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox_Tag1.Name = "pictureBox_Tag1";
            this.pictureBox_Tag1.Size = new System.Drawing.Size(28, 26);
            this.pictureBox_Tag1.TabIndex = 0;
            this.pictureBox_Tag1.TabStop = false;
            this.pictureBox_Tag1.Click += new System.EventHandler(this.pictureBox_Tag_Click);
            // 
            // panel_ColorPallet
            // 
            this.panel_ColorPallet.BackColor = System.Drawing.SystemColors.Control;
            this.panel_ColorPallet.Controls.Add(this.pictureBox_Color5);
            this.panel_ColorPallet.Controls.Add(this.pictureBox_Color4);
            this.panel_ColorPallet.Controls.Add(this.pictureBox_Color3);
            this.panel_ColorPallet.Controls.Add(this.pictureBox_Color2);
            this.panel_ColorPallet.Controls.Add(this.pictureBox_Color1);
            this.panel_ColorPallet.Location = new System.Drawing.Point(4, 2);
            this.panel_ColorPallet.Margin = new System.Windows.Forms.Padding(0);
            this.panel_ColorPallet.Name = "panel_ColorPallet";
            this.panel_ColorPallet.Size = new System.Drawing.Size(153, 32);
            this.panel_ColorPallet.TabIndex = 1;
            // 
            // pictureBox_Color5
            // 
            this.pictureBox_Color5.BackColor = System.Drawing.Color.SkyBlue;
            this.pictureBox_Color5.BackgroundImage = global::WellaTodo.Properties.Resources.outline_palette_black_24dp;
            this.pictureBox_Color5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox_Color5.Location = new System.Drawing.Point(122, 3);
            this.pictureBox_Color5.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox_Color5.Name = "pictureBox_Color5";
            this.pictureBox_Color5.Size = new System.Drawing.Size(28, 26);
            this.pictureBox_Color5.TabIndex = 0;
            this.pictureBox_Color5.TabStop = false;
            this.pictureBox_Color5.Click += new System.EventHandler(this.pictureBox_Color_Click);
            // 
            // pictureBox_Color4
            // 
            this.pictureBox_Color4.BackColor = System.Drawing.Color.Orange;
            this.pictureBox_Color4.BackgroundImage = global::WellaTodo.Properties.Resources.outline_palette_black_24dp;
            this.pictureBox_Color4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox_Color4.Location = new System.Drawing.Point(93, 3);
            this.pictureBox_Color4.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox_Color4.Name = "pictureBox_Color4";
            this.pictureBox_Color4.Size = new System.Drawing.Size(28, 26);
            this.pictureBox_Color4.TabIndex = 0;
            this.pictureBox_Color4.TabStop = false;
            this.pictureBox_Color4.Click += new System.EventHandler(this.pictureBox_Color_Click);
            // 
            // pictureBox_Color3
            // 
            this.pictureBox_Color3.BackColor = System.Drawing.Color.PaleGreen;
            this.pictureBox_Color3.BackgroundImage = global::WellaTodo.Properties.Resources.outline_palette_black_24dp;
            this.pictureBox_Color3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox_Color3.Location = new System.Drawing.Point(63, 3);
            this.pictureBox_Color3.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox_Color3.Name = "pictureBox_Color3";
            this.pictureBox_Color3.Size = new System.Drawing.Size(28, 26);
            this.pictureBox_Color3.TabIndex = 0;
            this.pictureBox_Color3.TabStop = false;
            this.pictureBox_Color3.Click += new System.EventHandler(this.pictureBox_Color_Click);
            // 
            // pictureBox_Color2
            // 
            this.pictureBox_Color2.BackColor = System.Drawing.Color.Violet;
            this.pictureBox_Color2.BackgroundImage = global::WellaTodo.Properties.Resources.outline_palette_black_24dp;
            this.pictureBox_Color2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox_Color2.Location = new System.Drawing.Point(33, 3);
            this.pictureBox_Color2.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox_Color2.Name = "pictureBox_Color2";
            this.pictureBox_Color2.Size = new System.Drawing.Size(28, 26);
            this.pictureBox_Color2.TabIndex = 0;
            this.pictureBox_Color2.TabStop = false;
            this.pictureBox_Color2.Click += new System.EventHandler(this.pictureBox_Color_Click);
            // 
            // pictureBox_Color1
            // 
            this.pictureBox_Color1.BackColor = System.Drawing.Color.Yellow;
            this.pictureBox_Color1.BackgroundImage = global::WellaTodo.Properties.Resources.outline_palette_black_24dp;
            this.pictureBox_Color1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox_Color1.Location = new System.Drawing.Point(4, 3);
            this.pictureBox_Color1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox_Color1.Name = "pictureBox_Color1";
            this.pictureBox_Color1.Size = new System.Drawing.Size(28, 26);
            this.pictureBox_Color1.TabIndex = 0;
            this.pictureBox_Color1.TabStop = false;
            this.pictureBox_Color1.Click += new System.EventHandler(this.pictureBox_Color_Click);
            // 
            // richTextBox
            // 
            this.richTextBox.BackColor = System.Drawing.Color.Gold;
            this.richTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox.HideSelection = false;
            this.richTextBox.Location = new System.Drawing.Point(0, 0);
            this.richTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox.Size = new System.Drawing.Size(195, 164);
            this.richTextBox.TabIndex = 0;
            this.richTextBox.Text = "";
            this.richTextBox.TextChanged += new System.EventHandler(this.richTextBox_TextChanged);
            this.richTextBox.Leave += new System.EventHandler(this.richTextBox_Leave);
            // 
            // Post_it
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gold;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.panel_Body);
            this.Controls.Add(this.panel_Footer);
            this.Controls.Add(this.panel_Header);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "Post_it";
            this.Size = new System.Drawing.Size(195, 228);
            this.Load += new System.EventHandler(this.Post_it_Load);
            this.Resize += new System.EventHandler(this.Post_it_Resize);
            this.panel_Header.ResumeLayout(false);
            this.panel_Header.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_New)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Edit)).EndInit();
            this.panel_Footer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Delete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Label)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Schedule)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Archive)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ColorPallet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Alarm)).EndInit();
            this.panel_Body.ResumeLayout(false);
            this.panel_Information.ResumeLayout(false);
            this.panel_Schedule.ResumeLayout(false);
            this.panel_Alarm.ResumeLayout(false);
            this.panel_Tag.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Tag0)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Tag5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Tag4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Tag3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Tag2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Tag1)).EndInit();
            this.panel_ColorPallet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Color5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Color4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Color3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Color2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Color1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_Header;
        private System.Windows.Forms.Panel panel_Footer;
        private System.Windows.Forms.Panel panel_Body;
        private System.Windows.Forms.PictureBox pictureBox_Edit;
        private System.Windows.Forms.PictureBox pictureBox_Delete;
        private System.Windows.Forms.PictureBox pictureBox_Label;
        private System.Windows.Forms.PictureBox pictureBox_Archive;
        private System.Windows.Forms.PictureBox pictureBox_ColorPallet;
        private System.Windows.Forms.PictureBox pictureBox_Alarm;
        private System.Windows.Forms.RichTextBox richTextBox;
        private System.Windows.Forms.Panel panel_ColorPallet;
        private System.Windows.Forms.PictureBox pictureBox_Color2;
        private System.Windows.Forms.PictureBox pictureBox_Color1;
        private System.Windows.Forms.PictureBox pictureBox_Color5;
        private System.Windows.Forms.PictureBox pictureBox_Color4;
        private System.Windows.Forms.PictureBox pictureBox_Color3;
        private System.Windows.Forms.PictureBox pictureBox_New;
        private System.Windows.Forms.Panel panel_Tag;
        private System.Windows.Forms.PictureBox pictureBox_Tag5;
        private System.Windows.Forms.PictureBox pictureBox_Tag4;
        private System.Windows.Forms.PictureBox pictureBox_Tag3;
        private System.Windows.Forms.PictureBox pictureBox_Tag2;
        private System.Windows.Forms.PictureBox pictureBox_Tag1;
        private System.Windows.Forms.PictureBox pictureBox_Tag0;
        private System.Windows.Forms.Label label_Title;
        private System.Windows.Forms.TextBox textBox_Title;
        private System.Windows.Forms.Panel panel_Information;
        private System.Windows.Forms.Label label_AlarmDate;
        private System.Windows.Forms.Panel panel_Alarm;
        private System.Windows.Forms.Button button_Alarm_Reset;
        private System.Windows.Forms.Button button_Alarm_Set;
        private System.Windows.Forms.PictureBox pictureBox_Schedule;
        private System.Windows.Forms.Panel panel_Schedule;
        private System.Windows.Forms.Button button_Schedule_Reset;
        private System.Windows.Forms.Button button_Schedule_Set;
        private System.Windows.Forms.Label label_ScheduleDate;
    }
}
