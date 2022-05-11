namespace WellaTodo
{
    partial class TaskTitleForm
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
            this.textBox_Title = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBox_Title
            // 
            this.textBox_Title.Location = new System.Drawing.Point(47, 40);
            this.textBox_Title.Name = "textBox_Title";
            this.textBox_Title.Size = new System.Drawing.Size(686, 25);
            this.textBox_Title.TabIndex = 0;
            this.textBox_Title.TextChanged += new System.EventHandler(this.textBox_Title_TextChanged);
            // 
            // TaskTitleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(773, 122);
            this.Controls.Add(this.textBox_Title);
            this.Name = "TaskTitleForm";
            this.Text = "TaskTitleForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TaskTitleForm_FormClosing);
            this.Load += new System.EventHandler(this.TaskTitleForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.TaskTitleForm_Paint);
            this.Resize += new System.EventHandler(this.TaskTitleForm_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_Title;
    }
}