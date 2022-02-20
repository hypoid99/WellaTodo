namespace WellaTodo
{
    partial class TwoLineList
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
            this.pictureBox_Icon = new System.Windows.Forms.PictureBox();
            this.label_PrimaryText = new System.Windows.Forms.Label();
            this.label_SecondaryText = new System.Windows.Forms.Label();
            this.label_Metadata = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Icon)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox_Icon
            // 
            this.pictureBox_Icon.Location = new System.Drawing.Point(7, 4);
            this.pictureBox_Icon.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox_Icon.Name = "pictureBox_Icon";
            this.pictureBox_Icon.Size = new System.Drawing.Size(24, 24);
            this.pictureBox_Icon.TabIndex = 0;
            this.pictureBox_Icon.TabStop = false;
            this.pictureBox_Icon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.List_MouseClick);
            this.pictureBox_Icon.MouseEnter += new System.EventHandler(this.List_MouseEnter);
            this.pictureBox_Icon.MouseLeave += new System.EventHandler(this.List_MouseLeave);
            // 
            // label_PrimaryText
            // 
            this.label_PrimaryText.AutoSize = true;
            this.label_PrimaryText.Location = new System.Drawing.Point(34, 6);
            this.label_PrimaryText.Name = "label_PrimaryText";
            this.label_PrimaryText.Size = new System.Drawing.Size(78, 12);
            this.label_PrimaryText.TabIndex = 1;
            this.label_PrimaryText.Text = "Primary Text";
            this.label_PrimaryText.MouseClick += new System.Windows.Forms.MouseEventHandler(this.List_MouseClick);
            this.label_PrimaryText.MouseEnter += new System.EventHandler(this.List_MouseEnter);
            this.label_PrimaryText.MouseLeave += new System.EventHandler(this.List_MouseLeave);
            // 
            // label_SecondaryText
            // 
            this.label_SecondaryText.AutoSize = true;
            this.label_SecondaryText.Location = new System.Drawing.Point(34, 15);
            this.label_SecondaryText.Name = "label_SecondaryText";
            this.label_SecondaryText.Size = new System.Drawing.Size(95, 12);
            this.label_SecondaryText.TabIndex = 2;
            this.label_SecondaryText.Text = "Secondary Text";
            this.label_SecondaryText.MouseClick += new System.Windows.Forms.MouseEventHandler(this.List_MouseClick);
            this.label_SecondaryText.MouseEnter += new System.EventHandler(this.List_MouseEnter);
            this.label_SecondaryText.MouseLeave += new System.EventHandler(this.List_MouseLeave);
            // 
            // label_Metadata
            // 
            this.label_Metadata.AutoSize = true;
            this.label_Metadata.Location = new System.Drawing.Point(150, 10);
            this.label_Metadata.Name = "label_Metadata";
            this.label_Metadata.Size = new System.Drawing.Size(57, 12);
            this.label_Metadata.TabIndex = 3;
            this.label_Metadata.Text = "Metadata";
            this.label_Metadata.MouseClick += new System.Windows.Forms.MouseEventHandler(this.List_MouseClick);
            this.label_Metadata.MouseEnter += new System.EventHandler(this.List_MouseEnter);
            this.label_Metadata.MouseLeave += new System.EventHandler(this.List_MouseLeave);
            // 
            // TwoLineList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label_Metadata);
            this.Controls.Add(this.label_SecondaryText);
            this.Controls.Add(this.label_PrimaryText);
            this.Controls.Add(this.pictureBox_Icon);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "TwoLineList";
            this.Size = new System.Drawing.Size(219, 32);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.TwoLineList_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.List_MouseClick);
            this.MouseEnter += new System.EventHandler(this.List_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.List_MouseLeave);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Icon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_Icon;
        private System.Windows.Forms.Label label_PrimaryText;
        private System.Windows.Forms.Label label_SecondaryText;
        private System.Windows.Forms.Label label_Metadata;
    }
}
