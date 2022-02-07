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
            this.SuspendLayout();
            // 
            // panel_Header
            // 
            this.panel_Header.BackColor = System.Drawing.Color.Cyan;
            this.panel_Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_Header.Location = new System.Drawing.Point(0, 0);
            this.panel_Header.Name = "panel_Header";
            this.panel_Header.Size = new System.Drawing.Size(493, 47);
            this.panel_Header.TabIndex = 0;
            // 
            // panel_Footer
            // 
            this.panel_Footer.BackColor = System.Drawing.Color.Cyan;
            this.panel_Footer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_Footer.Location = new System.Drawing.Point(0, 383);
            this.panel_Footer.Name = "panel_Footer";
            this.panel_Footer.Size = new System.Drawing.Size(493, 60);
            this.panel_Footer.TabIndex = 1;
            // 
            // panel_Body
            // 
            this.panel_Body.BackColor = System.Drawing.Color.White;
            this.panel_Body.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Body.Location = new System.Drawing.Point(0, 47);
            this.panel_Body.Name = "panel_Body";
            this.panel_Body.Size = new System.Drawing.Size(493, 336);
            this.panel_Body.TabIndex = 2;
            // 
            // Post_it
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel_Body);
            this.Controls.Add(this.panel_Footer);
            this.Controls.Add(this.panel_Header);
            this.Name = "Post_it";
            this.Size = new System.Drawing.Size(493, 443);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_Header;
        private System.Windows.Forms.Panel panel_Footer;
        private System.Windows.Forms.Panel panel_Body;
    }
}
