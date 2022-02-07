namespace WellaTodo
{
    partial class NotePadForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.파일ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.새로만들기ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.열기ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.저장ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.다른이름으로저장ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.인쇄ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.미리보기ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.종료ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.편집ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.취소ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.다시실행ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.잘라내기ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.복사ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.붙여넣기ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.모두선택ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.도움말ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.정보ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel_Header = new System.Windows.Forms.Panel();
            this.checkBox_AlignRight = new System.Windows.Forms.CheckBox();
            this.checkBox_AlignCenter = new System.Windows.Forms.CheckBox();
            this.checkBox_AlignLeft = new System.Windows.Forms.CheckBox();
            this.checkBox_Strike = new System.Windows.Forms.CheckBox();
            this.checkBox_Underline = new System.Windows.Forms.CheckBox();
            this.checkBox_Italic = new System.Windows.Forms.CheckBox();
            this.checkBox_Bold = new System.Windows.Forms.CheckBox();
            this.comboBox_FontSize = new System.Windows.Forms.ComboBox();
            this.comboBox_FontSelect = new System.Windows.Forms.ComboBox();
            this.button_Attachment = new System.Windows.Forms.Button();
            this.button_InsertImage = new System.Windows.Forms.Button();
            this.button_IndentInc = new System.Windows.Forms.Button();
            this.button_IndentDec = new System.Windows.Forms.Button();
            this.button_TextFillColor = new System.Windows.Forms.Button();
            this.button_TextColor = new System.Windows.Forms.Button();
            this.button_FontSizeDown = new System.Windows.Forms.Button();
            this.button_FontSizeUp = new System.Windows.Forms.Button();
            this.button_Paste = new System.Windows.Forms.Button();
            this.button_Copy = new System.Windows.Forms.Button();
            this.button_Cut = new System.Windows.Forms.Button();
            this.button_Save = new System.Windows.Forms.Button();
            this.button_Open = new System.Windows.Forms.Button();
            this.button_New = new System.Windows.Forms.Button();
            this.button_Print = new System.Windows.Forms.Button();
            this.button_Redo = new System.Windows.Forms.Button();
            this.button_Undo = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.menuStrip1.SuspendLayout();
            this.panel_Header.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.AllowMerge = false;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.파일ToolStripMenuItem,
            this.편집ToolStripMenuItem,
            this.도움말ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(6, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1153, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 파일ToolStripMenuItem
            // 
            this.파일ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.새로만들기ToolStripMenuItem,
            this.열기ToolStripMenuItem,
            this.저장ToolStripMenuItem,
            this.다른이름으로저장ToolStripMenuItem,
            this.인쇄ToolStripMenuItem,
            this.미리보기ToolStripMenuItem,
            this.종료ToolStripMenuItem});
            this.파일ToolStripMenuItem.Name = "파일ToolStripMenuItem";
            this.파일ToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.파일ToolStripMenuItem.Text = "파일";
            // 
            // 새로만들기ToolStripMenuItem
            // 
            this.새로만들기ToolStripMenuItem.Name = "새로만들기ToolStripMenuItem";
            this.새로만들기ToolStripMenuItem.Size = new System.Drawing.Size(222, 26);
            this.새로만들기ToolStripMenuItem.Text = "새로만들기";
            this.새로만들기ToolStripMenuItem.Click += new System.EventHandler(this.새로만들기ToolStripMenuItem_Click);
            // 
            // 열기ToolStripMenuItem
            // 
            this.열기ToolStripMenuItem.Name = "열기ToolStripMenuItem";
            this.열기ToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.열기ToolStripMenuItem.Text = "열기";
            this.열기ToolStripMenuItem.Click += new System.EventHandler(this.열기ToolStripMenuItem_Click);
            // 
            // 저장ToolStripMenuItem
            // 
            this.저장ToolStripMenuItem.Name = "저장ToolStripMenuItem";
            this.저장ToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.저장ToolStripMenuItem.Text = "저장";
            this.저장ToolStripMenuItem.Click += new System.EventHandler(this.저장ToolStripMenuItem_Click);
            // 
            // 다른이름으로저장ToolStripMenuItem
            // 
            this.다른이름으로저장ToolStripMenuItem.Name = "다른이름으로저장ToolStripMenuItem";
            this.다른이름으로저장ToolStripMenuItem.Size = new System.Drawing.Size(222, 26);
            this.다른이름으로저장ToolStripMenuItem.Text = "다른 이름으로 저장";
            this.다른이름으로저장ToolStripMenuItem.Click += new System.EventHandler(this.다른이름으로저장ToolStripMenuItem_Click);
            // 
            // 인쇄ToolStripMenuItem
            // 
            this.인쇄ToolStripMenuItem.Name = "인쇄ToolStripMenuItem";
            this.인쇄ToolStripMenuItem.Size = new System.Drawing.Size(222, 26);
            this.인쇄ToolStripMenuItem.Text = "인쇄";
            this.인쇄ToolStripMenuItem.Click += new System.EventHandler(this.인쇄ToolStripMenuItem_Click);
            // 
            // 미리보기ToolStripMenuItem
            // 
            this.미리보기ToolStripMenuItem.Name = "미리보기ToolStripMenuItem";
            this.미리보기ToolStripMenuItem.Size = new System.Drawing.Size(222, 26);
            this.미리보기ToolStripMenuItem.Text = "미리보기";
            this.미리보기ToolStripMenuItem.Click += new System.EventHandler(this.미리보기ToolStripMenuItem_Click);
            // 
            // 종료ToolStripMenuItem
            // 
            this.종료ToolStripMenuItem.Name = "종료ToolStripMenuItem";
            this.종료ToolStripMenuItem.Size = new System.Drawing.Size(222, 26);
            this.종료ToolStripMenuItem.Text = "종료";
            // 
            // 편집ToolStripMenuItem
            // 
            this.편집ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.취소ToolStripMenuItem,
            this.다시실행ToolStripMenuItem,
            this.잘라내기ToolStripMenuItem,
            this.복사ToolStripMenuItem,
            this.붙여넣기ToolStripMenuItem,
            this.모두선택ToolStripMenuItem});
            this.편집ToolStripMenuItem.Name = "편집ToolStripMenuItem";
            this.편집ToolStripMenuItem.Size = new System.Drawing.Size(53, 26);
            this.편집ToolStripMenuItem.Text = "편집";
            // 
            // 취소ToolStripMenuItem
            // 
            this.취소ToolStripMenuItem.Name = "취소ToolStripMenuItem";
            this.취소ToolStripMenuItem.Size = new System.Drawing.Size(157, 26);
            this.취소ToolStripMenuItem.Text = "실행 취소";
            // 
            // 다시실행ToolStripMenuItem
            // 
            this.다시실행ToolStripMenuItem.Name = "다시실행ToolStripMenuItem";
            this.다시실행ToolStripMenuItem.Size = new System.Drawing.Size(157, 26);
            this.다시실행ToolStripMenuItem.Text = "다시 실행";
            // 
            // 잘라내기ToolStripMenuItem
            // 
            this.잘라내기ToolStripMenuItem.Name = "잘라내기ToolStripMenuItem";
            this.잘라내기ToolStripMenuItem.Size = new System.Drawing.Size(157, 26);
            this.잘라내기ToolStripMenuItem.Text = "잘라내기";
            // 
            // 복사ToolStripMenuItem
            // 
            this.복사ToolStripMenuItem.Name = "복사ToolStripMenuItem";
            this.복사ToolStripMenuItem.Size = new System.Drawing.Size(157, 26);
            this.복사ToolStripMenuItem.Text = "복사";
            // 
            // 붙여넣기ToolStripMenuItem
            // 
            this.붙여넣기ToolStripMenuItem.Name = "붙여넣기ToolStripMenuItem";
            this.붙여넣기ToolStripMenuItem.Size = new System.Drawing.Size(157, 26);
            this.붙여넣기ToolStripMenuItem.Text = "붙여넣기";
            // 
            // 모두선택ToolStripMenuItem
            // 
            this.모두선택ToolStripMenuItem.Name = "모두선택ToolStripMenuItem";
            this.모두선택ToolStripMenuItem.Size = new System.Drawing.Size(157, 26);
            this.모두선택ToolStripMenuItem.Text = "모두 선택";
            this.모두선택ToolStripMenuItem.Click += new System.EventHandler(this.모두선택ToolStripMenuItem_Click);
            // 
            // 도움말ToolStripMenuItem
            // 
            this.도움말ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.정보ToolStripMenuItem});
            this.도움말ToolStripMenuItem.Name = "도움말ToolStripMenuItem";
            this.도움말ToolStripMenuItem.Size = new System.Drawing.Size(68, 26);
            this.도움말ToolStripMenuItem.Text = "도움말";
            // 
            // 정보ToolStripMenuItem
            // 
            this.정보ToolStripMenuItem.Name = "정보ToolStripMenuItem";
            this.정보ToolStripMenuItem.Size = new System.Drawing.Size(122, 26);
            this.정보ToolStripMenuItem.Text = "정보";
            // 
            // panel_Header
            // 
            this.panel_Header.BackColor = System.Drawing.SystemColors.Control;
            this.panel_Header.Controls.Add(this.checkBox_AlignRight);
            this.panel_Header.Controls.Add(this.checkBox_AlignCenter);
            this.panel_Header.Controls.Add(this.checkBox_AlignLeft);
            this.panel_Header.Controls.Add(this.checkBox_Strike);
            this.panel_Header.Controls.Add(this.checkBox_Underline);
            this.panel_Header.Controls.Add(this.checkBox_Italic);
            this.panel_Header.Controls.Add(this.checkBox_Bold);
            this.panel_Header.Controls.Add(this.comboBox_FontSize);
            this.panel_Header.Controls.Add(this.comboBox_FontSelect);
            this.panel_Header.Controls.Add(this.button_Attachment);
            this.panel_Header.Controls.Add(this.button_InsertImage);
            this.panel_Header.Controls.Add(this.button_IndentInc);
            this.panel_Header.Controls.Add(this.button_IndentDec);
            this.panel_Header.Controls.Add(this.button_TextFillColor);
            this.panel_Header.Controls.Add(this.button_TextColor);
            this.panel_Header.Controls.Add(this.button_FontSizeDown);
            this.panel_Header.Controls.Add(this.button_FontSizeUp);
            this.panel_Header.Controls.Add(this.button_Paste);
            this.panel_Header.Controls.Add(this.button_Copy);
            this.panel_Header.Controls.Add(this.button_Cut);
            this.panel_Header.Controls.Add(this.button_Save);
            this.panel_Header.Controls.Add(this.button_Open);
            this.panel_Header.Controls.Add(this.button_New);
            this.panel_Header.Controls.Add(this.button_Print);
            this.panel_Header.Controls.Add(this.button_Redo);
            this.panel_Header.Controls.Add(this.button_Undo);
            this.panel_Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_Header.Location = new System.Drawing.Point(0, 28);
            this.panel_Header.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel_Header.Name = "panel_Header";
            this.panel_Header.Size = new System.Drawing.Size(1153, 36);
            this.panel_Header.TabIndex = 2;
            // 
            // checkBox_AlignRight
            // 
            this.checkBox_AlignRight.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox_AlignRight.BackgroundImage = global::WellaTodo.Properties.Resources.outline_format_align_right_black_24dp;
            this.checkBox_AlignRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.checkBox_AlignRight.FlatAppearance.BorderSize = 0;
            this.checkBox_AlignRight.FlatAppearance.CheckedBackColor = System.Drawing.Color.Cyan;
            this.checkBox_AlignRight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBox_AlignRight.Location = new System.Drawing.Point(904, 2);
            this.checkBox_AlignRight.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBox_AlignRight.Name = "checkBox_AlignRight";
            this.checkBox_AlignRight.Size = new System.Drawing.Size(32, 32);
            this.checkBox_AlignRight.TabIndex = 3;
            this.checkBox_AlignRight.UseVisualStyleBackColor = false;
            // 
            // checkBox_AlignCenter
            // 
            this.checkBox_AlignCenter.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox_AlignCenter.BackgroundImage = global::WellaTodo.Properties.Resources.outline_format_align_center_black_24dp;
            this.checkBox_AlignCenter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.checkBox_AlignCenter.FlatAppearance.BorderSize = 0;
            this.checkBox_AlignCenter.FlatAppearance.CheckedBackColor = System.Drawing.Color.Cyan;
            this.checkBox_AlignCenter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBox_AlignCenter.Location = new System.Drawing.Point(872, 2);
            this.checkBox_AlignCenter.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBox_AlignCenter.Name = "checkBox_AlignCenter";
            this.checkBox_AlignCenter.Size = new System.Drawing.Size(32, 32);
            this.checkBox_AlignCenter.TabIndex = 3;
            this.checkBox_AlignCenter.UseVisualStyleBackColor = false;
            // 
            // checkBox_AlignLeft
            // 
            this.checkBox_AlignLeft.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox_AlignLeft.BackgroundImage = global::WellaTodo.Properties.Resources.outline_format_align_left_black_24dp;
            this.checkBox_AlignLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.checkBox_AlignLeft.FlatAppearance.BorderSize = 0;
            this.checkBox_AlignLeft.FlatAppearance.CheckedBackColor = System.Drawing.Color.Cyan;
            this.checkBox_AlignLeft.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBox_AlignLeft.Location = new System.Drawing.Point(840, 2);
            this.checkBox_AlignLeft.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBox_AlignLeft.Name = "checkBox_AlignLeft";
            this.checkBox_AlignLeft.Size = new System.Drawing.Size(32, 32);
            this.checkBox_AlignLeft.TabIndex = 3;
            this.checkBox_AlignLeft.UseVisualStyleBackColor = false;
            // 
            // checkBox_Strike
            // 
            this.checkBox_Strike.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox_Strike.BackgroundImage = global::WellaTodo.Properties.Resources.outline_format_strikethrough_black_24dp;
            this.checkBox_Strike.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.checkBox_Strike.FlatAppearance.BorderSize = 0;
            this.checkBox_Strike.FlatAppearance.CheckedBackColor = System.Drawing.Color.Cyan;
            this.checkBox_Strike.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBox_Strike.Location = new System.Drawing.Point(720, 2);
            this.checkBox_Strike.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBox_Strike.Name = "checkBox_Strike";
            this.checkBox_Strike.Size = new System.Drawing.Size(32, 32);
            this.checkBox_Strike.TabIndex = 3;
            this.checkBox_Strike.UseVisualStyleBackColor = false;
            this.checkBox_Strike.CheckedChanged += new System.EventHandler(this.checkBox_Strike_CheckedChanged);
            // 
            // checkBox_Underline
            // 
            this.checkBox_Underline.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox_Underline.BackgroundImage = global::WellaTodo.Properties.Resources.outline_format_underlined_black_24dp;
            this.checkBox_Underline.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.checkBox_Underline.FlatAppearance.BorderSize = 0;
            this.checkBox_Underline.FlatAppearance.CheckedBackColor = System.Drawing.Color.Cyan;
            this.checkBox_Underline.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBox_Underline.Location = new System.Drawing.Point(688, 2);
            this.checkBox_Underline.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBox_Underline.Name = "checkBox_Underline";
            this.checkBox_Underline.Size = new System.Drawing.Size(32, 32);
            this.checkBox_Underline.TabIndex = 3;
            this.checkBox_Underline.UseVisualStyleBackColor = false;
            this.checkBox_Underline.CheckedChanged += new System.EventHandler(this.checkBox_Underline_CheckedChanged);
            // 
            // checkBox_Italic
            // 
            this.checkBox_Italic.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox_Italic.BackgroundImage = global::WellaTodo.Properties.Resources.outline_format_italic_black_24dp;
            this.checkBox_Italic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.checkBox_Italic.FlatAppearance.BorderSize = 0;
            this.checkBox_Italic.FlatAppearance.CheckedBackColor = System.Drawing.Color.Cyan;
            this.checkBox_Italic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBox_Italic.Location = new System.Drawing.Point(656, 2);
            this.checkBox_Italic.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBox_Italic.Name = "checkBox_Italic";
            this.checkBox_Italic.Size = new System.Drawing.Size(32, 32);
            this.checkBox_Italic.TabIndex = 3;
            this.checkBox_Italic.UseVisualStyleBackColor = false;
            this.checkBox_Italic.CheckedChanged += new System.EventHandler(this.checkBox_Italic_CheckedChanged);
            // 
            // checkBox_Bold
            // 
            this.checkBox_Bold.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox_Bold.BackgroundImage = global::WellaTodo.Properties.Resources.outline_format_bold_black_24dp;
            this.checkBox_Bold.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.checkBox_Bold.FlatAppearance.BorderSize = 0;
            this.checkBox_Bold.FlatAppearance.CheckedBackColor = System.Drawing.Color.Cyan;
            this.checkBox_Bold.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBox_Bold.Location = new System.Drawing.Point(624, 2);
            this.checkBox_Bold.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBox_Bold.Name = "checkBox_Bold";
            this.checkBox_Bold.Size = new System.Drawing.Size(32, 32);
            this.checkBox_Bold.TabIndex = 3;
            this.checkBox_Bold.UseVisualStyleBackColor = false;
            this.checkBox_Bold.CheckedChanged += new System.EventHandler(this.checkBox_Bold_CheckedChanged);
            // 
            // comboBox_FontSize
            // 
            this.comboBox_FontSize.FormattingEnabled = true;
            this.comboBox_FontSize.Location = new System.Drawing.Point(495, 8);
            this.comboBox_FontSize.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBox_FontSize.Name = "comboBox_FontSize";
            this.comboBox_FontSize.Size = new System.Drawing.Size(50, 23);
            this.comboBox_FontSize.TabIndex = 2;
            this.comboBox_FontSize.SelectedIndexChanged += new System.EventHandler(this.comboBox_FontSize_SelectedIndexChanged);
            // 
            // comboBox_FontSelect
            // 
            this.comboBox_FontSelect.FormattingEnabled = true;
            this.comboBox_FontSelect.Location = new System.Drawing.Point(315, 8);
            this.comboBox_FontSelect.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBox_FontSelect.Name = "comboBox_FontSelect";
            this.comboBox_FontSelect.Size = new System.Drawing.Size(172, 23);
            this.comboBox_FontSelect.TabIndex = 1;
            this.comboBox_FontSelect.SelectedIndexChanged += new System.EventHandler(this.comboBox_FontSelect_SelectedIndexChanged);
            // 
            // button_Attachment
            // 
            this.button_Attachment.BackgroundImage = global::WellaTodo.Properties.Resources.outline_attach_file_black_24dp;
            this.button_Attachment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_Attachment.FlatAppearance.BorderSize = 0;
            this.button_Attachment.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.button_Attachment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Attachment.Location = new System.Drawing.Point(1051, 2);
            this.button_Attachment.Margin = new System.Windows.Forms.Padding(0);
            this.button_Attachment.Name = "button_Attachment";
            this.button_Attachment.Size = new System.Drawing.Size(32, 32);
            this.button_Attachment.TabIndex = 0;
            this.button_Attachment.UseVisualStyleBackColor = true;
            // 
            // button_InsertImage
            // 
            this.button_InsertImage.BackgroundImage = global::WellaTodo.Properties.Resources.outline_insert_photo_black_24dp;
            this.button_InsertImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_InsertImage.FlatAppearance.BorderSize = 0;
            this.button_InsertImage.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.button_InsertImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_InsertImage.Location = new System.Drawing.Point(1019, 2);
            this.button_InsertImage.Margin = new System.Windows.Forms.Padding(0);
            this.button_InsertImage.Name = "button_InsertImage";
            this.button_InsertImage.Size = new System.Drawing.Size(32, 32);
            this.button_InsertImage.TabIndex = 0;
            this.button_InsertImage.UseVisualStyleBackColor = true;
            // 
            // button_IndentInc
            // 
            this.button_IndentInc.BackgroundImage = global::WellaTodo.Properties.Resources.outline_format_indent_increase_black_24dp;
            this.button_IndentInc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_IndentInc.FlatAppearance.BorderSize = 0;
            this.button_IndentInc.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.button_IndentInc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_IndentInc.Location = new System.Drawing.Point(978, 2);
            this.button_IndentInc.Margin = new System.Windows.Forms.Padding(0);
            this.button_IndentInc.Name = "button_IndentInc";
            this.button_IndentInc.Size = new System.Drawing.Size(32, 32);
            this.button_IndentInc.TabIndex = 0;
            this.button_IndentInc.UseVisualStyleBackColor = true;
            this.button_IndentInc.Click += new System.EventHandler(this.button_IndentInc_Click);
            // 
            // button_IndentDec
            // 
            this.button_IndentDec.BackgroundImage = global::WellaTodo.Properties.Resources.outline_format_indent_decrease_black_24dp;
            this.button_IndentDec.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_IndentDec.FlatAppearance.BorderSize = 0;
            this.button_IndentDec.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.button_IndentDec.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_IndentDec.Location = new System.Drawing.Point(946, 2);
            this.button_IndentDec.Margin = new System.Windows.Forms.Padding(0);
            this.button_IndentDec.Name = "button_IndentDec";
            this.button_IndentDec.Size = new System.Drawing.Size(32, 32);
            this.button_IndentDec.TabIndex = 0;
            this.button_IndentDec.UseVisualStyleBackColor = true;
            this.button_IndentDec.Click += new System.EventHandler(this.button_IndentDec_Click);
            // 
            // button_TextFillColor
            // 
            this.button_TextFillColor.BackgroundImage = global::WellaTodo.Properties.Resources.outline_format_color_fill_black_24dp;
            this.button_TextFillColor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_TextFillColor.FlatAppearance.BorderSize = 0;
            this.button_TextFillColor.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.button_TextFillColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_TextFillColor.Location = new System.Drawing.Point(793, 2);
            this.button_TextFillColor.Margin = new System.Windows.Forms.Padding(0);
            this.button_TextFillColor.Name = "button_TextFillColor";
            this.button_TextFillColor.Size = new System.Drawing.Size(32, 32);
            this.button_TextFillColor.TabIndex = 0;
            this.button_TextFillColor.UseVisualStyleBackColor = true;
            this.button_TextFillColor.Click += new System.EventHandler(this.button_TextFillColor_Click);
            // 
            // button_TextColor
            // 
            this.button_TextColor.BackgroundImage = global::WellaTodo.Properties.Resources.outline_format_color_text_black_24dp;
            this.button_TextColor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_TextColor.FlatAppearance.BorderSize = 0;
            this.button_TextColor.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.button_TextColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_TextColor.Location = new System.Drawing.Point(761, 2);
            this.button_TextColor.Margin = new System.Windows.Forms.Padding(0);
            this.button_TextColor.Name = "button_TextColor";
            this.button_TextColor.Size = new System.Drawing.Size(32, 32);
            this.button_TextColor.TabIndex = 0;
            this.button_TextColor.UseVisualStyleBackColor = true;
            this.button_TextColor.Click += new System.EventHandler(this.button_TextColor_Click);
            // 
            // button_FontSizeDown
            // 
            this.button_FontSizeDown.BackgroundImage = global::WellaTodo.Properties.Resources.outline_text_decrease_black_24dp;
            this.button_FontSizeDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_FontSizeDown.FlatAppearance.BorderSize = 0;
            this.button_FontSizeDown.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.button_FontSizeDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_FontSizeDown.Location = new System.Drawing.Point(583, 2);
            this.button_FontSizeDown.Margin = new System.Windows.Forms.Padding(0);
            this.button_FontSizeDown.Name = "button_FontSizeDown";
            this.button_FontSizeDown.Size = new System.Drawing.Size(32, 32);
            this.button_FontSizeDown.TabIndex = 0;
            this.button_FontSizeDown.UseVisualStyleBackColor = true;
            this.button_FontSizeDown.Click += new System.EventHandler(this.button_FontSizeDown_Click);
            // 
            // button_FontSizeUp
            // 
            this.button_FontSizeUp.BackgroundImage = global::WellaTodo.Properties.Resources.outline_text_increase_black_24dp;
            this.button_FontSizeUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_FontSizeUp.FlatAppearance.BorderSize = 0;
            this.button_FontSizeUp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.button_FontSizeUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_FontSizeUp.Location = new System.Drawing.Point(551, 2);
            this.button_FontSizeUp.Margin = new System.Windows.Forms.Padding(0);
            this.button_FontSizeUp.Name = "button_FontSizeUp";
            this.button_FontSizeUp.Size = new System.Drawing.Size(32, 32);
            this.button_FontSizeUp.TabIndex = 0;
            this.button_FontSizeUp.UseVisualStyleBackColor = true;
            this.button_FontSizeUp.Click += new System.EventHandler(this.button_FontSizeUp_Click);
            // 
            // button_Paste
            // 
            this.button_Paste.BackgroundImage = global::WellaTodo.Properties.Resources.outline_content_paste_black_24dp;
            this.button_Paste.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_Paste.FlatAppearance.BorderSize = 0;
            this.button_Paste.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.button_Paste.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Paste.Location = new System.Drawing.Point(280, 2);
            this.button_Paste.Margin = new System.Windows.Forms.Padding(0);
            this.button_Paste.Name = "button_Paste";
            this.button_Paste.Size = new System.Drawing.Size(32, 32);
            this.button_Paste.TabIndex = 0;
            this.button_Paste.UseVisualStyleBackColor = true;
            this.button_Paste.Click += new System.EventHandler(this.button_Paste_Click);
            // 
            // button_Copy
            // 
            this.button_Copy.BackgroundImage = global::WellaTodo.Properties.Resources.outline_content_copy_black_24dp;
            this.button_Copy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_Copy.FlatAppearance.BorderSize = 0;
            this.button_Copy.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.button_Copy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Copy.Location = new System.Drawing.Point(248, 2);
            this.button_Copy.Margin = new System.Windows.Forms.Padding(0);
            this.button_Copy.Name = "button_Copy";
            this.button_Copy.Size = new System.Drawing.Size(32, 32);
            this.button_Copy.TabIndex = 0;
            this.button_Copy.UseVisualStyleBackColor = true;
            this.button_Copy.Click += new System.EventHandler(this.button_Copy_Click);
            // 
            // button_Cut
            // 
            this.button_Cut.BackgroundImage = global::WellaTodo.Properties.Resources.outline_content_cut_black_24dp;
            this.button_Cut.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_Cut.FlatAppearance.BorderSize = 0;
            this.button_Cut.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.button_Cut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Cut.Location = new System.Drawing.Point(216, 2);
            this.button_Cut.Margin = new System.Windows.Forms.Padding(0);
            this.button_Cut.Name = "button_Cut";
            this.button_Cut.Size = new System.Drawing.Size(32, 32);
            this.button_Cut.TabIndex = 0;
            this.button_Cut.UseVisualStyleBackColor = true;
            this.button_Cut.Click += new System.EventHandler(this.button_Cut_Click);
            // 
            // button_Save
            // 
            this.button_Save.BackgroundImage = global::WellaTodo.Properties.Resources.outline_save_black_24dp;
            this.button_Save.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_Save.FlatAppearance.BorderSize = 0;
            this.button_Save.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.button_Save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Save.Location = new System.Drawing.Point(67, 2);
            this.button_Save.Margin = new System.Windows.Forms.Padding(0);
            this.button_Save.Name = "button_Save";
            this.button_Save.Size = new System.Drawing.Size(32, 32);
            this.button_Save.TabIndex = 0;
            this.button_Save.UseVisualStyleBackColor = true;
            this.button_Save.Click += new System.EventHandler(this.button_Save_Click);
            // 
            // button_Open
            // 
            this.button_Open.BackgroundImage = global::WellaTodo.Properties.Resources.outline_file_open_black_24dp;
            this.button_Open.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_Open.FlatAppearance.BorderSize = 0;
            this.button_Open.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.button_Open.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Open.Location = new System.Drawing.Point(35, 2);
            this.button_Open.Margin = new System.Windows.Forms.Padding(0);
            this.button_Open.Name = "button_Open";
            this.button_Open.Size = new System.Drawing.Size(32, 32);
            this.button_Open.TabIndex = 0;
            this.button_Open.UseVisualStyleBackColor = true;
            this.button_Open.Click += new System.EventHandler(this.button_Open_Click);
            // 
            // button_New
            // 
            this.button_New.BackgroundImage = global::WellaTodo.Properties.Resources.outline_open_in_new_black_24dp;
            this.button_New.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_New.FlatAppearance.BorderSize = 0;
            this.button_New.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.button_New.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_New.Location = new System.Drawing.Point(3, 2);
            this.button_New.Margin = new System.Windows.Forms.Padding(0);
            this.button_New.Name = "button_New";
            this.button_New.Size = new System.Drawing.Size(32, 32);
            this.button_New.TabIndex = 0;
            this.button_New.UseVisualStyleBackColor = true;
            this.button_New.Click += new System.EventHandler(this.button_New_Click);
            // 
            // button_Print
            // 
            this.button_Print.BackgroundImage = global::WellaTodo.Properties.Resources.outline_print_black_24dp;
            this.button_Print.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_Print.FlatAppearance.BorderSize = 0;
            this.button_Print.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.button_Print.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Print.Location = new System.Drawing.Point(99, 2);
            this.button_Print.Margin = new System.Windows.Forms.Padding(0);
            this.button_Print.Name = "button_Print";
            this.button_Print.Size = new System.Drawing.Size(32, 32);
            this.button_Print.TabIndex = 0;
            this.button_Print.UseVisualStyleBackColor = true;
            this.button_Print.Click += new System.EventHandler(this.button_Print_Click);
            // 
            // button_Redo
            // 
            this.button_Redo.BackgroundImage = global::WellaTodo.Properties.Resources.outline_redo_black_24dp;
            this.button_Redo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_Redo.FlatAppearance.BorderSize = 0;
            this.button_Redo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.button_Redo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Redo.Location = new System.Drawing.Point(175, 2);
            this.button_Redo.Margin = new System.Windows.Forms.Padding(0);
            this.button_Redo.Name = "button_Redo";
            this.button_Redo.Size = new System.Drawing.Size(32, 32);
            this.button_Redo.TabIndex = 0;
            this.button_Redo.UseVisualStyleBackColor = true;
            this.button_Redo.Click += new System.EventHandler(this.button_Redo_Click);
            // 
            // button_Undo
            // 
            this.button_Undo.BackgroundImage = global::WellaTodo.Properties.Resources.outline_undo_black_24dp;
            this.button_Undo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_Undo.FlatAppearance.BorderSize = 0;
            this.button_Undo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.button_Undo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Undo.Location = new System.Drawing.Point(143, 2);
            this.button_Undo.Margin = new System.Windows.Forms.Padding(0);
            this.button_Undo.Name = "button_Undo";
            this.button_Undo.Size = new System.Drawing.Size(32, 32);
            this.button_Undo.TabIndex = 0;
            this.button_Undo.UseVisualStyleBackColor = true;
            this.button_Undo.Click += new System.EventHandler(this.button_Undo_Click);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.richTextBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 64);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1153, 386);
            this.panel1.TabIndex = 3;
            // 
            // richTextBox
            // 
            this.richTextBox.AcceptsTab = true;
            this.richTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox.EnableAutoDragDrop = true;
            this.richTextBox.Location = new System.Drawing.Point(0, 0);
            this.richTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.Size = new System.Drawing.Size(1153, 386);
            this.richTextBox.TabIndex = 0;
            this.richTextBox.Text = "";
            this.richTextBox.WordWrap = false;
            this.richTextBox.SelectionChanged += new System.EventHandler(this.richTextBox_SelectionChanged);
            this.richTextBox.TextChanged += new System.EventHandler(this.richTextBox_TextChanged);
            // 
            // NotePadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1153, 450);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel_Header);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "NotePadForm";
            this.Text = "NotePadForm";
            this.Load += new System.EventHandler(this.NotePadForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel_Header.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 파일ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 새로만들기ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 열기ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 저장ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 다른이름으로저장ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 인쇄ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 미리보기ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 종료ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 편집ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 취소ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 다시실행ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 잘라내기ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 복사ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 붙여넣기ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 모두선택ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 도움말ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 정보ToolStripMenuItem;
        private System.Windows.Forms.Panel panel_Header;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RichTextBox richTextBox;
        private System.Windows.Forms.Button button_Undo;
        private System.Windows.Forms.Button button_Redo;
        private System.Windows.Forms.Button button_Paste;
        private System.Windows.Forms.Button button_Copy;
        private System.Windows.Forms.Button button_Cut;
        private System.Windows.Forms.Button button_Print;
        private System.Windows.Forms.ComboBox comboBox_FontSize;
        private System.Windows.Forms.ComboBox comboBox_FontSelect;
        private System.Windows.Forms.Button button_InsertImage;
        private System.Windows.Forms.Button button_IndentInc;
        private System.Windows.Forms.Button button_IndentDec;
        private System.Windows.Forms.Button button_TextFillColor;
        private System.Windows.Forms.Button button_TextColor;
        private System.Windows.Forms.Button button_FontSizeDown;
        private System.Windows.Forms.Button button_FontSizeUp;
        private System.Windows.Forms.Button button_Attachment;
        private System.Windows.Forms.Button button_Save;
        private System.Windows.Forms.Button button_Open;
        private System.Windows.Forms.Button button_New;
        private System.Windows.Forms.CheckBox checkBox_Bold;
        private System.Windows.Forms.CheckBox checkBox_Strike;
        private System.Windows.Forms.CheckBox checkBox_Underline;
        private System.Windows.Forms.CheckBox checkBox_Italic;
        private System.Windows.Forms.CheckBox checkBox_AlignRight;
        private System.Windows.Forms.CheckBox checkBox_AlignCenter;
        private System.Windows.Forms.CheckBox checkBox_AlignLeft;
    }
}