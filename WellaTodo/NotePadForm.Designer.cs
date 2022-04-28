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
            this.flowLayoutPanel_List = new System.Windows.Forms.FlowLayoutPanel();
            this.panel_Header = new System.Windows.Forms.Panel();
            this.label_Add_Note = new System.Windows.Forms.Label();
            this.label_Menu = new System.Windows.Forms.Label();
            this.label_Title = new System.Windows.Forms.Label();
            this.panel_Footer = new System.Windows.Forms.Panel();
            this.textBox_New_Note = new System.Windows.Forms.TextBox();
            this.panel_Header.SuspendLayout();
            this.panel_Footer.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel_List
            // 
            this.flowLayoutPanel_List.AutoScroll = true;
            this.flowLayoutPanel_List.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel_List.Location = new System.Drawing.Point(0, 53);
            this.flowLayoutPanel_List.Name = "flowLayoutPanel_List";
            this.flowLayoutPanel_List.Size = new System.Drawing.Size(800, 332);
            this.flowLayoutPanel_List.TabIndex = 0;
            // 
            // panel_Header
            // 
            this.panel_Header.Controls.Add(this.label_Add_Note);
            this.panel_Header.Controls.Add(this.label_Menu);
            this.panel_Header.Controls.Add(this.label_Title);
            this.panel_Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_Header.Location = new System.Drawing.Point(0, 0);
            this.panel_Header.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel_Header.Name = "panel_Header";
            this.panel_Header.Size = new System.Drawing.Size(800, 48);
            this.panel_Header.TabIndex = 1;
            // 
            // label_Add_Note
            // 
            this.label_Add_Note.Image = global::WellaTodo.Properties.Resources.outline_add_circle_outline_black_24dp;
            this.label_Add_Note.Location = new System.Drawing.Point(756, 8);
            this.label_Add_Note.Margin = new System.Windows.Forms.Padding(0);
            this.label_Add_Note.Name = "label_Add_Note";
            this.label_Add_Note.Size = new System.Drawing.Size(32, 32);
            this.label_Add_Note.TabIndex = 4;
            this.label_Add_Note.Click += new System.EventHandler(this.label_Add_Note_Click);
            // 
            // label_Menu
            // 
            this.label_Menu.Image = global::WellaTodo.Properties.Resources.outline_apps_black_24dp;
            this.label_Menu.Location = new System.Drawing.Point(8, 8);
            this.label_Menu.Margin = new System.Windows.Forms.Padding(0);
            this.label_Menu.Name = "label_Menu";
            this.label_Menu.Size = new System.Drawing.Size(32, 32);
            this.label_Menu.TabIndex = 3;
            // 
            // label_Title
            // 
            this.label_Title.Font = new System.Drawing.Font("휴먼둥근헤드라인", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_Title.Location = new System.Drawing.Point(42, 6);
            this.label_Title.Margin = new System.Windows.Forms.Padding(0);
            this.label_Title.Name = "label_Title";
            this.label_Title.Size = new System.Drawing.Size(154, 36);
            this.label_Title.TabIndex = 2;
            this.label_Title.Text = "NotePad";
            this.label_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel_Footer
            // 
            this.panel_Footer.Controls.Add(this.textBox_New_Note);
            this.panel_Footer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_Footer.Location = new System.Drawing.Point(0, 390);
            this.panel_Footer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel_Footer.Name = "panel_Footer";
            this.panel_Footer.Size = new System.Drawing.Size(800, 60);
            this.panel_Footer.TabIndex = 2;
            // 
            // textBox_New_Note
            // 
            this.textBox_New_Note.Location = new System.Drawing.Point(11, 10);
            this.textBox_New_Note.Name = "textBox_New_Note";
            this.textBox_New_Note.Size = new System.Drawing.Size(777, 25);
            this.textBox_New_Note.TabIndex = 0;
            // 
            // NotePadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel_Footer);
            this.Controls.Add(this.panel_Header);
            this.Controls.Add(this.flowLayoutPanel_List);
            this.Name = "NotePadForm";
            this.Text = "NotePadForm";
            this.Activated += new System.EventHandler(this.NotePadForm_Activated);
            this.Deactivate += new System.EventHandler(this.NotePadForm_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NotePadForm_FormClosing);
            this.Load += new System.EventHandler(this.NotePadForm_Load);
            this.Resize += new System.EventHandler(this.NotePadForm_Resize);
            this.panel_Header.ResumeLayout(false);
            this.panel_Footer.ResumeLayout(false);
            this.panel_Footer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel_List;
        private System.Windows.Forms.Panel panel_Header;
        private System.Windows.Forms.Label label_Menu;
        private System.Windows.Forms.Label label_Title;
        private System.Windows.Forms.Label label_Add_Note;
        private System.Windows.Forms.Panel panel_Footer;
        private System.Windows.Forms.TextBox textBox_New_Note;
    }
}