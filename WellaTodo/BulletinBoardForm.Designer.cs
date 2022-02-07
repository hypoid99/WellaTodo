namespace WellaTodo
{
    partial class BulletinBoardForm
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
            this.panel_Header = new System.Windows.Forms.Panel();
            this.label_Menu = new System.Windows.Forms.Label();
            this.label_Title = new System.Windows.Forms.Label();
            this.panel_Menu = new System.Windows.Forms.Panel();
            this.panel_Bulletin = new System.Windows.Forms.Panel();
            this.panel_Header.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_Header
            // 
            this.panel_Header.Controls.Add(this.label_Menu);
            this.panel_Header.Controls.Add(this.label_Title);
            this.panel_Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_Header.Location = new System.Drawing.Point(0, 0);
            this.panel_Header.Name = "panel_Header";
            this.panel_Header.Size = new System.Drawing.Size(800, 50);
            this.panel_Header.TabIndex = 0;
            // 
            // label_Menu
            // 
            this.label_Menu.Image = global::WellaTodo.Properties.Resources.outline_menu_black_24dp;
            this.label_Menu.Location = new System.Drawing.Point(9, 9);
            this.label_Menu.Margin = new System.Windows.Forms.Padding(0);
            this.label_Menu.Name = "label_Menu";
            this.label_Menu.Size = new System.Drawing.Size(32, 32);
            this.label_Menu.TabIndex = 1;
            // 
            // label_Title
            // 
            this.label_Title.Font = new System.Drawing.Font("휴먼둥근헤드라인", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_Title.Location = new System.Drawing.Point(41, 1);
            this.label_Title.Margin = new System.Windows.Forms.Padding(0);
            this.label_Title.Name = "label_Title";
            this.label_Title.Size = new System.Drawing.Size(207, 48);
            this.label_Title.TabIndex = 0;
            this.label_Title.Text = "Bulletin Board";
            this.label_Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel_Menu
            // 
            this.panel_Menu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_Menu.Location = new System.Drawing.Point(0, 50);
            this.panel_Menu.Name = "panel_Menu";
            this.panel_Menu.Size = new System.Drawing.Size(100, 400);
            this.panel_Menu.TabIndex = 1;
            // 
            // panel_Bulletin
            // 
            this.panel_Bulletin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Bulletin.Location = new System.Drawing.Point(100, 50);
            this.panel_Bulletin.Name = "panel_Bulletin";
            this.panel_Bulletin.Size = new System.Drawing.Size(700, 400);
            this.panel_Bulletin.TabIndex = 2;
            // 
            // BulletinBoardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel_Bulletin);
            this.Controls.Add(this.panel_Menu);
            this.Controls.Add(this.panel_Header);
            this.Name = "BulletinBoardForm";
            this.Text = "BulletinBoardForm";
            this.Load += new System.EventHandler(this.BulletinBoardForm_Load);
            this.panel_Header.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_Header;
        private System.Windows.Forms.Panel panel_Menu;
        private System.Windows.Forms.Panel panel_Bulletin;
        private System.Windows.Forms.Label label_Title;
        private System.Windows.Forms.Label label_Menu;
    }
}