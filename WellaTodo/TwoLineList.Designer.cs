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
            this.pictureBox_Icon.Location = new System.Drawing.Point(8, 5);
            this.pictureBox_Icon.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox_Icon.Name = "pictureBox_Icon";
            this.pictureBox_Icon.Size = new System.Drawing.Size(27, 30);
            this.pictureBox_Icon.TabIndex = 0;
            this.pictureBox_Icon.TabStop = false;
            // 
            // label_PrimaryText
            // 
            this.label_PrimaryText.AutoSize = true;
            this.label_PrimaryText.Location = new System.Drawing.Point(39, 8);
            this.label_PrimaryText.Name = "label_PrimaryText";
            this.label_PrimaryText.Size = new System.Drawing.Size(89, 15);
            this.label_PrimaryText.TabIndex = 1;
            this.label_PrimaryText.Text = "Primary Text";
            // 
            // label_SecondaryText
            // 
            this.label_SecondaryText.AutoSize = true;
            this.label_SecondaryText.Location = new System.Drawing.Point(39, 19);
            this.label_SecondaryText.Name = "label_SecondaryText";
            this.label_SecondaryText.Size = new System.Drawing.Size(112, 15);
            this.label_SecondaryText.TabIndex = 2;
            this.label_SecondaryText.Text = "Secondary Text";
            // 
            // label_Metadata
            // 
            this.label_Metadata.AutoSize = true;
            this.label_Metadata.Location = new System.Drawing.Point(171, 12);
            this.label_Metadata.Name = "label_Metadata";
            this.label_Metadata.Size = new System.Drawing.Size(67, 15);
            this.label_Metadata.TabIndex = 3;
            this.label_Metadata.Text = "Metadata";
            // 
            // TwoLineList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label_Metadata);
            this.Controls.Add(this.label_SecondaryText);
            this.Controls.Add(this.label_PrimaryText);
            this.Controls.Add(this.pictureBox_Icon);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "TwoLineList";
            this.Size = new System.Drawing.Size(250, 40);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.TwoLineList_Paint);
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
