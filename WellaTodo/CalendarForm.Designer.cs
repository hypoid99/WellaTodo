﻿namespace WellaTodo
{
    partial class CalendarForm
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
            this.SuspendLayout();
            // 
            // CalendarForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "CalendarForm";
            this.Text = "CalendarForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CalendarForm_FormClosing);
            this.Load += new System.EventHandler(this.CalendarForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.CalendarForm_Paint);
            this.Resize += new System.EventHandler(this.CalendarForm_Resize);
            this.ResumeLayout(false);

        }

        #endregion
    }
}