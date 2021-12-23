
namespace WellaTodo
{
    partial class TaskEditForm
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
            this.panel_TaskEdit = new System.Windows.Forms.Panel();
            this.button_Close = new System.Windows.Forms.Button();
            this.button_Delete = new System.Windows.Forms.Button();
            this.textBox_Memo = new System.Windows.Forms.TextBox();
            this.textBox_Title = new System.Windows.Forms.TextBox();
            this.panel_TaskEdit.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_TaskEdit
            // 
            this.panel_TaskEdit.Controls.Add(this.textBox_Title);
            this.panel_TaskEdit.Controls.Add(this.textBox_Memo);
            this.panel_TaskEdit.Controls.Add(this.button_Delete);
            this.panel_TaskEdit.Controls.Add(this.button_Close);
            this.panel_TaskEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_TaskEdit.Location = new System.Drawing.Point(0, 0);
            this.panel_TaskEdit.Name = "panel_TaskEdit";
            this.panel_TaskEdit.Size = new System.Drawing.Size(344, 450);
            this.panel_TaskEdit.TabIndex = 0;
            // 
            // button_Close
            // 
            this.button_Close.Location = new System.Drawing.Point(51, 399);
            this.button_Close.Name = "button_Close";
            this.button_Close.Size = new System.Drawing.Size(60, 30);
            this.button_Close.TabIndex = 0;
            this.button_Close.Text = "닫기";
            this.button_Close.UseVisualStyleBackColor = true;
            this.button_Close.Click += new System.EventHandler(this.button_Close_Click);
            // 
            // button_Delete
            // 
            this.button_Delete.Location = new System.Drawing.Point(233, 399);
            this.button_Delete.Name = "button_Delete";
            this.button_Delete.Size = new System.Drawing.Size(60, 30);
            this.button_Delete.TabIndex = 1;
            this.button_Delete.Text = "삭제";
            this.button_Delete.UseVisualStyleBackColor = true;
            this.button_Delete.Click += new System.EventHandler(this.button_Delete_Click);
            // 
            // textBox_Memo
            // 
            this.textBox_Memo.Location = new System.Drawing.Point(55, 241);
            this.textBox_Memo.Multiline = true;
            this.textBox_Memo.Name = "textBox_Memo";
            this.textBox_Memo.Size = new System.Drawing.Size(237, 127);
            this.textBox_Memo.TabIndex = 2;
            // 
            // textBox_Title
            // 
            this.textBox_Title.Location = new System.Drawing.Point(56, 28);
            this.textBox_Title.Name = "textBox_Title";
            this.textBox_Title.Size = new System.Drawing.Size(237, 25);
            this.textBox_Title.TabIndex = 3;
            // 
            // TaskEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 450);
            this.Controls.Add(this.panel_TaskEdit);
            this.Name = "TaskEditForm";
            this.Text = "할 일 수정";
            this.Load += new System.EventHandler(this.TaskEditForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.TaskEditForm_Paint);
            this.Resize += new System.EventHandler(this.TaskEditForm_Resize);
            this.panel_TaskEdit.ResumeLayout(false);
            this.panel_TaskEdit.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_TaskEdit;
        private System.Windows.Forms.TextBox textBox_Title;
        private System.Windows.Forms.TextBox textBox_Memo;
        private System.Windows.Forms.Button button_Delete;
        private System.Windows.Forms.Button button_Close;
    }
}