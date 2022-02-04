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
            this.comboBox_FontSize = new System.Windows.Forms.ComboBox();
            this.comboBox_FontSelect = new System.Windows.Forms.ComboBox();
            this.button_Attachment = new System.Windows.Forms.Button();
            this.button_InsertImage = new System.Windows.Forms.Button();
            this.button_IndentIncrease = new System.Windows.Forms.Button();
            this.button_IndentDecrease = new System.Windows.Forms.Button();
            this.button_AlignJustify = new System.Windows.Forms.Button();
            this.button_AlignRight = new System.Windows.Forms.Button();
            this.button_AlignCenter = new System.Windows.Forms.Button();
            this.button_AlignLeft = new System.Windows.Forms.Button();
            this.button_TextFillColor = new System.Windows.Forms.Button();
            this.button_TextColor = new System.Windows.Forms.Button();
            this.button_Strike = new System.Windows.Forms.Button();
            this.button_Underline = new System.Windows.Forms.Button();
            this.button_Italic = new System.Windows.Forms.Button();
            this.button_Bold = new System.Windows.Forms.Button();
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
            this.menuStrip1.Size = new System.Drawing.Size(1169, 28);
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
            this.열기ToolStripMenuItem.Size = new System.Drawing.Size(222, 26);
            this.열기ToolStripMenuItem.Text = "열기";
            // 
            // 저장ToolStripMenuItem
            // 
            this.저장ToolStripMenuItem.Name = "저장ToolStripMenuItem";
            this.저장ToolStripMenuItem.Size = new System.Drawing.Size(222, 26);
            this.저장ToolStripMenuItem.Text = "저장";
            // 
            // 다른이름으로저장ToolStripMenuItem
            // 
            this.다른이름으로저장ToolStripMenuItem.Name = "다른이름으로저장ToolStripMenuItem";
            this.다른이름으로저장ToolStripMenuItem.Size = new System.Drawing.Size(222, 26);
            this.다른이름으로저장ToolStripMenuItem.Text = "다른 이름으로 저장";
            // 
            // 인쇄ToolStripMenuItem
            // 
            this.인쇄ToolStripMenuItem.Name = "인쇄ToolStripMenuItem";
            this.인쇄ToolStripMenuItem.Size = new System.Drawing.Size(222, 26);
            this.인쇄ToolStripMenuItem.Text = "인쇄";
            // 
            // 미리보기ToolStripMenuItem
            // 
            this.미리보기ToolStripMenuItem.Name = "미리보기ToolStripMenuItem";
            this.미리보기ToolStripMenuItem.Size = new System.Drawing.Size(222, 26);
            this.미리보기ToolStripMenuItem.Text = "미리보기";
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
            this.편집ToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
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
            // 
            // 도움말ToolStripMenuItem
            // 
            this.도움말ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.정보ToolStripMenuItem});
            this.도움말ToolStripMenuItem.Name = "도움말ToolStripMenuItem";
            this.도움말ToolStripMenuItem.Size = new System.Drawing.Size(68, 24);
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
            this.panel_Header.BackColor = System.Drawing.Color.White;
            this.panel_Header.Controls.Add(this.comboBox_FontSize);
            this.panel_Header.Controls.Add(this.comboBox_FontSelect);
            this.panel_Header.Controls.Add(this.button_Attachment);
            this.panel_Header.Controls.Add(this.button_InsertImage);
            this.panel_Header.Controls.Add(this.button_IndentIncrease);
            this.panel_Header.Controls.Add(this.button_IndentDecrease);
            this.panel_Header.Controls.Add(this.button_AlignJustify);
            this.panel_Header.Controls.Add(this.button_AlignRight);
            this.panel_Header.Controls.Add(this.button_AlignCenter);
            this.panel_Header.Controls.Add(this.button_AlignLeft);
            this.panel_Header.Controls.Add(this.button_TextFillColor);
            this.panel_Header.Controls.Add(this.button_TextColor);
            this.panel_Header.Controls.Add(this.button_Strike);
            this.panel_Header.Controls.Add(this.button_Underline);
            this.panel_Header.Controls.Add(this.button_Italic);
            this.panel_Header.Controls.Add(this.button_Bold);
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
            this.panel_Header.Name = "panel_Header";
            this.panel_Header.Size = new System.Drawing.Size(1169, 36);
            this.panel_Header.TabIndex = 2;
            // 
            // comboBox_FontSize
            // 
            this.comboBox_FontSize.FormattingEnabled = true;
            this.comboBox_FontSize.Location = new System.Drawing.Point(451, 7);
            this.comboBox_FontSize.Name = "comboBox_FontSize";
            this.comboBox_FontSize.Size = new System.Drawing.Size(50, 23);
            this.comboBox_FontSize.TabIndex = 2;
            // 
            // comboBox_FontSelect
            // 
            this.comboBox_FontSelect.FormattingEnabled = true;
            this.comboBox_FontSelect.Location = new System.Drawing.Point(315, 7);
            this.comboBox_FontSelect.Name = "comboBox_FontSelect";
            this.comboBox_FontSelect.Size = new System.Drawing.Size(130, 23);
            this.comboBox_FontSelect.TabIndex = 1;
            // 
            // button_Attachment
            // 
            this.button_Attachment.BackgroundImage = global::WellaTodo.Properties.Resources.outline_attach_file_black_24dp;
            this.button_Attachment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_Attachment.FlatAppearance.BorderSize = 0;
            this.button_Attachment.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.button_Attachment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Attachment.Location = new System.Drawing.Point(1039, 2);
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
            this.button_InsertImage.Location = new System.Drawing.Point(1007, 2);
            this.button_InsertImage.Margin = new System.Windows.Forms.Padding(0);
            this.button_InsertImage.Name = "button_InsertImage";
            this.button_InsertImage.Size = new System.Drawing.Size(32, 32);
            this.button_InsertImage.TabIndex = 0;
            this.button_InsertImage.UseVisualStyleBackColor = true;
            // 
            // button_IndentIncrease
            // 
            this.button_IndentIncrease.BackgroundImage = global::WellaTodo.Properties.Resources.outline_format_indent_increase_black_24dp;
            this.button_IndentIncrease.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_IndentIncrease.FlatAppearance.BorderSize = 0;
            this.button_IndentIncrease.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.button_IndentIncrease.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_IndentIncrease.Location = new System.Drawing.Point(966, 2);
            this.button_IndentIncrease.Margin = new System.Windows.Forms.Padding(0);
            this.button_IndentIncrease.Name = "button_IndentIncrease";
            this.button_IndentIncrease.Size = new System.Drawing.Size(32, 32);
            this.button_IndentIncrease.TabIndex = 0;
            this.button_IndentIncrease.UseVisualStyleBackColor = true;
            // 
            // button_IndentDecrease
            // 
            this.button_IndentDecrease.BackgroundImage = global::WellaTodo.Properties.Resources.outline_format_indent_decrease_black_24dp;
            this.button_IndentDecrease.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_IndentDecrease.FlatAppearance.BorderSize = 0;
            this.button_IndentDecrease.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.button_IndentDecrease.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_IndentDecrease.Location = new System.Drawing.Point(934, 2);
            this.button_IndentDecrease.Margin = new System.Windows.Forms.Padding(0);
            this.button_IndentDecrease.Name = "button_IndentDecrease";
            this.button_IndentDecrease.Size = new System.Drawing.Size(32, 32);
            this.button_IndentDecrease.TabIndex = 0;
            this.button_IndentDecrease.UseVisualStyleBackColor = true;
            // 
            // button_AlignJustify
            // 
            this.button_AlignJustify.BackgroundImage = global::WellaTodo.Properties.Resources.outline_format_align_justify_black_24dp;
            this.button_AlignJustify.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_AlignJustify.FlatAppearance.BorderSize = 0;
            this.button_AlignJustify.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.button_AlignJustify.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_AlignJustify.Location = new System.Drawing.Point(892, 2);
            this.button_AlignJustify.Margin = new System.Windows.Forms.Padding(0);
            this.button_AlignJustify.Name = "button_AlignJustify";
            this.button_AlignJustify.Size = new System.Drawing.Size(32, 32);
            this.button_AlignJustify.TabIndex = 0;
            this.button_AlignJustify.UseVisualStyleBackColor = true;
            // 
            // button_AlignRight
            // 
            this.button_AlignRight.BackgroundImage = global::WellaTodo.Properties.Resources.outline_format_align_right_black_24dp;
            this.button_AlignRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_AlignRight.FlatAppearance.BorderSize = 0;
            this.button_AlignRight.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.button_AlignRight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_AlignRight.Location = new System.Drawing.Point(860, 2);
            this.button_AlignRight.Margin = new System.Windows.Forms.Padding(0);
            this.button_AlignRight.Name = "button_AlignRight";
            this.button_AlignRight.Size = new System.Drawing.Size(32, 32);
            this.button_AlignRight.TabIndex = 0;
            this.button_AlignRight.UseVisualStyleBackColor = true;
            // 
            // button_AlignCenter
            // 
            this.button_AlignCenter.BackgroundImage = global::WellaTodo.Properties.Resources.outline_format_align_center_black_24dp;
            this.button_AlignCenter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_AlignCenter.FlatAppearance.BorderSize = 0;
            this.button_AlignCenter.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.button_AlignCenter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_AlignCenter.Location = new System.Drawing.Point(828, 2);
            this.button_AlignCenter.Margin = new System.Windows.Forms.Padding(0);
            this.button_AlignCenter.Name = "button_AlignCenter";
            this.button_AlignCenter.Size = new System.Drawing.Size(32, 32);
            this.button_AlignCenter.TabIndex = 0;
            this.button_AlignCenter.UseVisualStyleBackColor = true;
            // 
            // button_AlignLeft
            // 
            this.button_AlignLeft.BackgroundImage = global::WellaTodo.Properties.Resources.outline_format_align_left_black_24dp;
            this.button_AlignLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_AlignLeft.FlatAppearance.BorderSize = 0;
            this.button_AlignLeft.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.button_AlignLeft.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_AlignLeft.Location = new System.Drawing.Point(796, 2);
            this.button_AlignLeft.Margin = new System.Windows.Forms.Padding(0);
            this.button_AlignLeft.Name = "button_AlignLeft";
            this.button_AlignLeft.Size = new System.Drawing.Size(32, 32);
            this.button_AlignLeft.TabIndex = 0;
            this.button_AlignLeft.UseVisualStyleBackColor = true;
            // 
            // button_TextFillColor
            // 
            this.button_TextFillColor.BackgroundImage = global::WellaTodo.Properties.Resources.outline_format_color_fill_black_24dp;
            this.button_TextFillColor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_TextFillColor.FlatAppearance.BorderSize = 0;
            this.button_TextFillColor.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.button_TextFillColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_TextFillColor.Location = new System.Drawing.Point(750, 2);
            this.button_TextFillColor.Margin = new System.Windows.Forms.Padding(0);
            this.button_TextFillColor.Name = "button_TextFillColor";
            this.button_TextFillColor.Size = new System.Drawing.Size(32, 32);
            this.button_TextFillColor.TabIndex = 0;
            this.button_TextFillColor.UseVisualStyleBackColor = true;
            // 
            // button_TextColor
            // 
            this.button_TextColor.BackgroundImage = global::WellaTodo.Properties.Resources.outline_format_color_text_black_24dp;
            this.button_TextColor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_TextColor.FlatAppearance.BorderSize = 0;
            this.button_TextColor.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.button_TextColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_TextColor.Location = new System.Drawing.Point(718, 2);
            this.button_TextColor.Margin = new System.Windows.Forms.Padding(0);
            this.button_TextColor.Name = "button_TextColor";
            this.button_TextColor.Size = new System.Drawing.Size(32, 32);
            this.button_TextColor.TabIndex = 0;
            this.button_TextColor.UseVisualStyleBackColor = true;
            // 
            // button_Strike
            // 
            this.button_Strike.BackgroundImage = global::WellaTodo.Properties.Resources.outline_format_strikethrough_black_24dp;
            this.button_Strike.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_Strike.FlatAppearance.BorderSize = 0;
            this.button_Strike.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.button_Strike.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Strike.Location = new System.Drawing.Point(674, 2);
            this.button_Strike.Margin = new System.Windows.Forms.Padding(0);
            this.button_Strike.Name = "button_Strike";
            this.button_Strike.Size = new System.Drawing.Size(32, 32);
            this.button_Strike.TabIndex = 0;
            this.button_Strike.UseVisualStyleBackColor = true;
            // 
            // button_Underline
            // 
            this.button_Underline.BackgroundImage = global::WellaTodo.Properties.Resources.outline_format_underlined_black_24dp;
            this.button_Underline.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_Underline.FlatAppearance.BorderSize = 0;
            this.button_Underline.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.button_Underline.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Underline.Location = new System.Drawing.Point(642, 2);
            this.button_Underline.Margin = new System.Windows.Forms.Padding(0);
            this.button_Underline.Name = "button_Underline";
            this.button_Underline.Size = new System.Drawing.Size(32, 32);
            this.button_Underline.TabIndex = 0;
            this.button_Underline.UseVisualStyleBackColor = true;
            // 
            // button_Italic
            // 
            this.button_Italic.BackgroundImage = global::WellaTodo.Properties.Resources.outline_format_italic_black_24dp;
            this.button_Italic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_Italic.FlatAppearance.BorderSize = 0;
            this.button_Italic.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.button_Italic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Italic.Location = new System.Drawing.Point(610, 2);
            this.button_Italic.Margin = new System.Windows.Forms.Padding(0);
            this.button_Italic.Name = "button_Italic";
            this.button_Italic.Size = new System.Drawing.Size(32, 32);
            this.button_Italic.TabIndex = 0;
            this.button_Italic.UseVisualStyleBackColor = true;
            // 
            // button_Bold
            // 
            this.button_Bold.BackgroundImage = global::WellaTodo.Properties.Resources.outline_format_bold_black_24dp;
            this.button_Bold.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_Bold.FlatAppearance.BorderSize = 0;
            this.button_Bold.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.button_Bold.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Bold.Location = new System.Drawing.Point(578, 2);
            this.button_Bold.Margin = new System.Windows.Forms.Padding(0);
            this.button_Bold.Name = "button_Bold";
            this.button_Bold.Size = new System.Drawing.Size(32, 32);
            this.button_Bold.TabIndex = 0;
            this.button_Bold.UseVisualStyleBackColor = true;
            // 
            // button_FontSizeDown
            // 
            this.button_FontSizeDown.BackgroundImage = global::WellaTodo.Properties.Resources.outline_text_decrease_black_24dp;
            this.button_FontSizeDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_FontSizeDown.FlatAppearance.BorderSize = 0;
            this.button_FontSizeDown.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.button_FontSizeDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_FontSizeDown.Location = new System.Drawing.Point(539, 2);
            this.button_FontSizeDown.Margin = new System.Windows.Forms.Padding(0);
            this.button_FontSizeDown.Name = "button_FontSizeDown";
            this.button_FontSizeDown.Size = new System.Drawing.Size(32, 32);
            this.button_FontSizeDown.TabIndex = 0;
            this.button_FontSizeDown.UseVisualStyleBackColor = true;
            // 
            // button_FontSizeUp
            // 
            this.button_FontSizeUp.BackgroundImage = global::WellaTodo.Properties.Resources.outline_text_increase_black_24dp;
            this.button_FontSizeUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_FontSizeUp.FlatAppearance.BorderSize = 0;
            this.button_FontSizeUp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.button_FontSizeUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_FontSizeUp.Location = new System.Drawing.Point(507, 2);
            this.button_FontSizeUp.Margin = new System.Windows.Forms.Padding(0);
            this.button_FontSizeUp.Name = "button_FontSizeUp";
            this.button_FontSizeUp.Size = new System.Drawing.Size(32, 32);
            this.button_FontSizeUp.TabIndex = 0;
            this.button_FontSizeUp.UseVisualStyleBackColor = true;
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
            // 
            // button_Save
            // 
            this.button_Save.BackgroundImage = global::WellaTodo.Properties.Resources.outline_save_black_24dp;
            this.button_Save.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_Save.FlatAppearance.BorderSize = 0;
            this.button_Save.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.button_Save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Save.Location = new System.Drawing.Point(142, 2);
            this.button_Save.Margin = new System.Windows.Forms.Padding(0);
            this.button_Save.Name = "button_Save";
            this.button_Save.Size = new System.Drawing.Size(32, 32);
            this.button_Save.TabIndex = 0;
            this.button_Save.UseVisualStyleBackColor = true;
            // 
            // button_Open
            // 
            this.button_Open.BackgroundImage = global::WellaTodo.Properties.Resources.outline_file_open_black_24dp;
            this.button_Open.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_Open.FlatAppearance.BorderSize = 0;
            this.button_Open.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.button_Open.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Open.Location = new System.Drawing.Point(110, 2);
            this.button_Open.Margin = new System.Windows.Forms.Padding(0);
            this.button_Open.Name = "button_Open";
            this.button_Open.Size = new System.Drawing.Size(32, 32);
            this.button_Open.TabIndex = 0;
            this.button_Open.UseVisualStyleBackColor = true;
            // 
            // button_New
            // 
            this.button_New.BackgroundImage = global::WellaTodo.Properties.Resources.outline_open_in_new_black_24dp;
            this.button_New.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_New.FlatAppearance.BorderSize = 0;
            this.button_New.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.button_New.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_New.Location = new System.Drawing.Point(78, 2);
            this.button_New.Margin = new System.Windows.Forms.Padding(0);
            this.button_New.Name = "button_New";
            this.button_New.Size = new System.Drawing.Size(32, 32);
            this.button_New.TabIndex = 0;
            this.button_New.UseVisualStyleBackColor = true;
            // 
            // button_Print
            // 
            this.button_Print.BackgroundImage = global::WellaTodo.Properties.Resources.outline_print_black_24dp;
            this.button_Print.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_Print.FlatAppearance.BorderSize = 0;
            this.button_Print.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.button_Print.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Print.Location = new System.Drawing.Point(174, 2);
            this.button_Print.Margin = new System.Windows.Forms.Padding(0);
            this.button_Print.Name = "button_Print";
            this.button_Print.Size = new System.Drawing.Size(32, 32);
            this.button_Print.TabIndex = 0;
            this.button_Print.UseVisualStyleBackColor = true;
            // 
            // button_Redo
            // 
            this.button_Redo.BackgroundImage = global::WellaTodo.Properties.Resources.outline_redo_black_24dp;
            this.button_Redo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_Redo.FlatAppearance.BorderSize = 0;
            this.button_Redo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.button_Redo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Redo.Location = new System.Drawing.Point(35, 2);
            this.button_Redo.Margin = new System.Windows.Forms.Padding(0);
            this.button_Redo.Name = "button_Redo";
            this.button_Redo.Size = new System.Drawing.Size(32, 32);
            this.button_Redo.TabIndex = 0;
            this.button_Redo.UseVisualStyleBackColor = true;
            // 
            // button_Undo
            // 
            this.button_Undo.BackgroundImage = global::WellaTodo.Properties.Resources.outline_undo_black_24dp;
            this.button_Undo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_Undo.FlatAppearance.BorderSize = 0;
            this.button_Undo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.button_Undo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Undo.Location = new System.Drawing.Point(3, 2);
            this.button_Undo.Margin = new System.Windows.Forms.Padding(0);
            this.button_Undo.Name = "button_Undo";
            this.button_Undo.Size = new System.Drawing.Size(32, 32);
            this.button_Undo.TabIndex = 0;
            this.button_Undo.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.richTextBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 64);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1169, 386);
            this.panel1.TabIndex = 3;
            // 
            // richTextBox
            // 
            this.richTextBox.BackColor = System.Drawing.Color.LightCyan;
            this.richTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox.Location = new System.Drawing.Point(0, 0);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.Size = new System.Drawing.Size(1169, 386);
            this.richTextBox.TabIndex = 0;
            this.richTextBox.Text = "";
            // 
            // NotePadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1169, 450);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel_Header);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "NotePadForm";
            this.Text = "NotePadForm";
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
        private System.Windows.Forms.Button button_IndentIncrease;
        private System.Windows.Forms.Button button_IndentDecrease;
        private System.Windows.Forms.Button button_AlignJustify;
        private System.Windows.Forms.Button button_AlignRight;
        private System.Windows.Forms.Button button_AlignCenter;
        private System.Windows.Forms.Button button_AlignLeft;
        private System.Windows.Forms.Button button_TextFillColor;
        private System.Windows.Forms.Button button_TextColor;
        private System.Windows.Forms.Button button_Strike;
        private System.Windows.Forms.Button button_Underline;
        private System.Windows.Forms.Button button_Italic;
        private System.Windows.Forms.Button button_Bold;
        private System.Windows.Forms.Button button_FontSizeDown;
        private System.Windows.Forms.Button button_FontSizeUp;
        private System.Windows.Forms.Button button_Attachment;
        private System.Windows.Forms.Button button_Save;
        private System.Windows.Forms.Button button_Open;
        private System.Windows.Forms.Button button_New;
    }
}