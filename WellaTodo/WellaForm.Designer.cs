
namespace WellaTodo
{
    partial class WellaForm
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
            this.저장ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.종료ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.보기ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toDoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.달력ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.계산기ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.창ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.캐스케이드ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.수직정렬ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.수평정렬ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.아이콘화ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.도움말ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.정보ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.출력창ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.파일ToolStripMenuItem,
            this.보기ToolStripMenuItem,
            this.창ToolStripMenuItem,
            this.도움말ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(782, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 파일ToolStripMenuItem
            // 
            this.파일ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.저장ToolStripMenuItem,
            this.종료ToolStripMenuItem});
            this.파일ToolStripMenuItem.Name = "파일ToolStripMenuItem";
            this.파일ToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.파일ToolStripMenuItem.Text = "파일";
            // 
            // 저장ToolStripMenuItem
            // 
            this.저장ToolStripMenuItem.Name = "저장ToolStripMenuItem";
            this.저장ToolStripMenuItem.Size = new System.Drawing.Size(122, 26);
            this.저장ToolStripMenuItem.Text = "저장";
            // 
            // 종료ToolStripMenuItem
            // 
            this.종료ToolStripMenuItem.Name = "종료ToolStripMenuItem";
            this.종료ToolStripMenuItem.Size = new System.Drawing.Size(122, 26);
            this.종료ToolStripMenuItem.Text = "종료";
            this.종료ToolStripMenuItem.Click += new System.EventHandler(this.종료ToolStripMenuItem_Click);
            // 
            // 보기ToolStripMenuItem
            // 
            this.보기ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toDoToolStripMenuItem,
            this.달력ToolStripMenuItem,
            this.계산기ToolStripMenuItem,
            this.출력창ToolStripMenuItem});
            this.보기ToolStripMenuItem.Name = "보기ToolStripMenuItem";
            this.보기ToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.보기ToolStripMenuItem.Text = "보기";
            // 
            // toDoToolStripMenuItem
            // 
            this.toDoToolStripMenuItem.Name = "toDoToolStripMenuItem";
            this.toDoToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.toDoToolStripMenuItem.Text = "할일";
            this.toDoToolStripMenuItem.Click += new System.EventHandler(this.toDoToolStripMenuItem_Click);
            // 
            // 달력ToolStripMenuItem
            // 
            this.달력ToolStripMenuItem.Name = "달력ToolStripMenuItem";
            this.달력ToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.달력ToolStripMenuItem.Text = "달력";
            this.달력ToolStripMenuItem.Click += new System.EventHandler(this.달력ToolStripMenuItem_Click);
            // 
            // 계산기ToolStripMenuItem
            // 
            this.계산기ToolStripMenuItem.Name = "계산기ToolStripMenuItem";
            this.계산기ToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.계산기ToolStripMenuItem.Text = "계산기";
            this.계산기ToolStripMenuItem.Click += new System.EventHandler(this.계산기ToolStripMenuItem_Click);
            // 
            // 창ToolStripMenuItem
            // 
            this.창ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.캐스케이드ToolStripMenuItem,
            this.수직정렬ToolStripMenuItem,
            this.수평정렬ToolStripMenuItem,
            this.아이콘화ToolStripMenuItem});
            this.창ToolStripMenuItem.Name = "창ToolStripMenuItem";
            this.창ToolStripMenuItem.Size = new System.Drawing.Size(38, 24);
            this.창ToolStripMenuItem.Text = "창";
            // 
            // 캐스케이드ToolStripMenuItem
            // 
            this.캐스케이드ToolStripMenuItem.Name = "캐스케이드ToolStripMenuItem";
            this.캐스케이드ToolStripMenuItem.Size = new System.Drawing.Size(167, 26);
            this.캐스케이드ToolStripMenuItem.Text = "캐스케이드";
            this.캐스케이드ToolStripMenuItem.Click += new System.EventHandler(this.캐스케이드ToolStripMenuItem_Click);
            // 
            // 수직정렬ToolStripMenuItem
            // 
            this.수직정렬ToolStripMenuItem.Name = "수직정렬ToolStripMenuItem";
            this.수직정렬ToolStripMenuItem.Size = new System.Drawing.Size(167, 26);
            this.수직정렬ToolStripMenuItem.Text = "수직정렬";
            this.수직정렬ToolStripMenuItem.Click += new System.EventHandler(this.수직정렬ToolStripMenuItem_Click);
            // 
            // 수평정렬ToolStripMenuItem
            // 
            this.수평정렬ToolStripMenuItem.Name = "수평정렬ToolStripMenuItem";
            this.수평정렬ToolStripMenuItem.Size = new System.Drawing.Size(167, 26);
            this.수평정렬ToolStripMenuItem.Text = "수평정렬";
            this.수평정렬ToolStripMenuItem.Click += new System.EventHandler(this.수평정렬ToolStripMenuItem_Click);
            // 
            // 아이콘화ToolStripMenuItem
            // 
            this.아이콘화ToolStripMenuItem.Name = "아이콘화ToolStripMenuItem";
            this.아이콘화ToolStripMenuItem.Size = new System.Drawing.Size(167, 26);
            this.아이콘화ToolStripMenuItem.Text = "아이콘화";
            this.아이콘화ToolStripMenuItem.Click += new System.EventHandler(this.아이콘화ToolStripMenuItem_Click);
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
            this.정보ToolStripMenuItem.Click += new System.EventHandler(this.정보ToolStripMenuItem_Click);
            // 
            // 출력창ToolStripMenuItem
            // 
            this.출력창ToolStripMenuItem.Name = "출력창ToolStripMenuItem";
            this.출력창ToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.출력창ToolStripMenuItem.Text = "출력창";
            this.출력창ToolStripMenuItem.Click += new System.EventHandler(this.출력창ToolStripMenuItem_Click);
            // 
            // WellaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 423);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "WellaForm";
            this.Text = "WellaForm";
            this.Load += new System.EventHandler(this.WellaForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 파일ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 종료ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 보기ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toDoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 달력ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 계산기ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 창ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 캐스케이드ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 수직정렬ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 수평정렬ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 아이콘화ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 도움말ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 정보ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 저장ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 출력창ToolStripMenuItem;
    }
}