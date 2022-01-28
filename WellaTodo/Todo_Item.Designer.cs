
namespace WellaTodo
{
    partial class Todo_Item
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
            this.SuspendLayout();
            // 
            // Todo_Item
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Todo_Item";
            this.Size = new System.Drawing.Size(381, 35);
            this.Load += new System.EventHandler(this.Todo_Item_Load);
            this.GiveFeedback += new System.Windows.Forms.GiveFeedbackEventHandler(this.Todo_Item_GiveFeedback);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Todo_Item_Paint);
            this.QueryContinueDrag += new System.Windows.Forms.QueryContinueDragEventHandler(this.Todo_Item_QueryContinueDrag);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Todo_Item_MouseClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Todo_Item_MouseDown);
            this.MouseEnter += new System.EventHandler(this.Todo_Item_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.Todo_Item_MouseLeave);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Todo_Item_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Todo_Item_MouseUp);
            this.Resize += new System.EventHandler(this.Todo_Item_Resize);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
