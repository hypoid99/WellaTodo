namespace WellaTodo
{
    partial class NoteFileList
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
            this.label_FileSize = new System.Windows.Forms.Label();
            this.label_FileName = new System.Windows.Forms.Label();
            this.pictureBox_Icon = new System.Windows.Forms.PictureBox();
            this.label_ModifiedDate = new System.Windows.Forms.Label();
            this.label_CreatedDate = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Icon)).BeginInit();
            this.SuspendLayout();
            // 
            // label_FileSize
            // 
            this.label_FileSize.AutoSize = true;
            this.label_FileSize.Location = new System.Drawing.Point(416, 17);
            this.label_FileSize.Name = "label_FileSize";
            this.label_FileSize.Size = new System.Drawing.Size(58, 15);
            this.label_FileSize.TabIndex = 6;
            this.label_FileSize.Text = "FileSize";
            // 
            // label_FileName
            // 
            this.label_FileName.AutoSize = true;
            this.label_FileName.Location = new System.Drawing.Point(67, 17);
            this.label_FileName.Name = "label_FileName";
            this.label_FileName.Size = new System.Drawing.Size(65, 15);
            this.label_FileName.TabIndex = 5;
            this.label_FileName.Text = "FileName";
            // 
            // pictureBox_Icon
            // 
            this.pictureBox_Icon.Location = new System.Drawing.Point(14, 10);
            this.pictureBox_Icon.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox_Icon.Name = "pictureBox_Icon";
            this.pictureBox_Icon.Size = new System.Drawing.Size(27, 30);
            this.pictureBox_Icon.TabIndex = 4;
            this.pictureBox_Icon.TabStop = false;
            // 
            // label_ModifiedDate
            // 
            this.label_ModifiedDate.AutoSize = true;
            this.label_ModifiedDate.Location = new System.Drawing.Point(178, 17);
            this.label_ModifiedDate.Name = "label_ModifiedDate";
            this.label_ModifiedDate.Size = new System.Drawing.Size(97, 15);
            this.label_ModifiedDate.TabIndex = 5;
            this.label_ModifiedDate.Text = "Modified Date";
            // 
            // label_CreatedDate
            // 
            this.label_CreatedDate.AutoSize = true;
            this.label_CreatedDate.Location = new System.Drawing.Point(297, 17);
            this.label_CreatedDate.Name = "label_CreatedDate";
            this.label_CreatedDate.Size = new System.Drawing.Size(92, 15);
            this.label_CreatedDate.TabIndex = 5;
            this.label_CreatedDate.Text = "Created Date";
            // 
            // NoteFileList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label_FileSize);
            this.Controls.Add(this.label_CreatedDate);
            this.Controls.Add(this.label_ModifiedDate);
            this.Controls.Add(this.label_FileName);
            this.Controls.Add(this.pictureBox_Icon);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "NoteFileList";
            this.Size = new System.Drawing.Size(500, 50);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Icon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_FileSize;
        private System.Windows.Forms.Label label_FileName;
        private System.Windows.Forms.PictureBox pictureBox_Icon;
        private System.Windows.Forms.Label label_ModifiedDate;
        private System.Windows.Forms.Label label_CreatedDate;
    }
}
