namespace WellaTodo
{
    partial class MainFrame
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

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFrame));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.flowLayoutPanel_Menulist = new System.Windows.Forms.FlowLayoutPanel();
            this.textBox_AddList = new System.Windows.Forms.TextBox();
            this.labelUserName = new System.Windows.Forms.Label();
            this.textBox_Task = new System.Windows.Forms.TextBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.panel_Header = new System.Windows.Forms.Panel();
            this.label_ListName = new System.Windows.Forms.Label();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.panel_Calendar = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.panel_Header.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.Menu;
            this.splitContainer1.Panel1.Controls.Add(this.flowLayoutPanel_Menulist);
            this.splitContainer1.Panel1.Controls.Add(this.textBox_AddList);
            this.splitContainer1.Panel1.Controls.Add(this.labelUserName);
            this.splitContainer1.Panel1MinSize = 100;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.Window;
            this.splitContainer1.Panel2.Controls.Add(this.textBox_Task);
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel2MinSize = 200;
            this.splitContainer1.Size = new System.Drawing.Size(982, 403);
            this.splitContainer1.SplitterDistance = 200;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 0;
            this.splitContainer1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.splitContainer1_MouseDown);
            this.splitContainer1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.splitContainer1_MouseMove);
            this.splitContainer1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.splitContainer1_MouseUp);
            // 
            // flowLayoutPanel_Menulist
            // 
            this.flowLayoutPanel_Menulist.Location = new System.Drawing.Point(3, 43);
            this.flowLayoutPanel_Menulist.Name = "flowLayoutPanel_Menulist";
            this.flowLayoutPanel_Menulist.Size = new System.Drawing.Size(197, 329);
            this.flowLayoutPanel_Menulist.TabIndex = 3;
            // 
            // textBox_AddList
            // 
            this.textBox_AddList.Location = new System.Drawing.Point(0, 378);
            this.textBox_AddList.Name = "textBox_AddList";
            this.textBox_AddList.Size = new System.Drawing.Size(204, 25);
            this.textBox_AddList.TabIndex = 5;
            this.textBox_AddList.Enter += new System.EventHandler(this.textBox_AddList_Enter);
            this.textBox_AddList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_AddList_KeyDown);
            this.textBox_AddList.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox_AddList_KeyUp);
            this.textBox_AddList.Leave += new System.EventHandler(this.textBox_AddList_Leave);
            this.textBox_AddList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.textBox_AddList_MouseDown);
            // 
            // labelUserName
            // 
            this.labelUserName.Location = new System.Drawing.Point(0, 0);
            this.labelUserName.Name = "labelUserName";
            this.labelUserName.Size = new System.Drawing.Size(204, 40);
            this.labelUserName.TabIndex = 1;
            this.labelUserName.Text = "      계정";
            this.labelUserName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelUserName.MouseClick += new System.Windows.Forms.MouseEventHandler(this.labelUserName_MouseClick);
            this.labelUserName.MouseEnter += new System.EventHandler(this.labelUserName_MouseEnter);
            this.labelUserName.MouseLeave += new System.EventHandler(this.labelUserName_MouseLeave);
            // 
            // textBox_Task
            // 
            this.textBox_Task.Location = new System.Drawing.Point(10, 354);
            this.textBox_Task.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_Task.Name = "textBox_Task";
            this.textBox_Task.Size = new System.Drawing.Size(329, 25);
            this.textBox_Task.TabIndex = 0;
            this.textBox_Task.Enter += new System.EventHandler(this.textBox_Task_Enter);
            this.textBox_Task.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox_Task_KeyUp);
            this.textBox_Task.Leave += new System.EventHandler(this.textBox_Task_Leave);
            this.textBox_Task.MouseDown += new System.Windows.Forms.MouseEventHandler(this.textBox_Task_MouseDown);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Location = new System.Drawing.Point(10, 10);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.panel_Calendar);
            this.splitContainer2.Panel1.Controls.Add(this.panel_Header);
            this.splitContainer2.Panel1.Controls.Add(this.flowLayoutPanel2);
            this.splitContainer2.Panel1MinSize = 200;
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.button2);
            this.splitContainer2.Panel2.Controls.Add(this.button1);
            this.splitContainer2.Panel2.Controls.Add(this.textBox3);
            this.splitContainer2.Panel2.Controls.Add(this.textBox1);
            this.splitContainer2.Panel2MinSize = 1;
            this.splitContainer2.Size = new System.Drawing.Size(600, 340);
            this.splitContainer2.SplitterDistance = 306;
            this.splitContainer2.SplitterWidth = 1;
            this.splitContainer2.TabIndex = 3;
            // 
            // panel_Header
            // 
            this.panel_Header.Controls.Add(this.label_ListName);
            this.panel_Header.Location = new System.Drawing.Point(12, 11);
            this.panel_Header.Name = "panel_Header";
            this.panel_Header.Size = new System.Drawing.Size(283, 55);
            this.panel_Header.TabIndex = 2;
            // 
            // label_ListName
            // 
            this.label_ListName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label_ListName.Location = new System.Drawing.Point(19, 18);
            this.label_ListName.Name = "label_ListName";
            this.label_ListName.Size = new System.Drawing.Size(100, 23);
            this.label_ListName.TabIndex = 1;
            this.label_ListName.Text = "ListName";
            this.label_ListName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoScroll = true;
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(12, 85);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(283, 166);
            this.flowLayoutPanel2.TabIndex = 0;
            this.flowLayoutPanel2.WrapContents = false;
            this.flowLayoutPanel2.Scroll += new System.Windows.Forms.ScrollEventHandler(this.flowLayoutPanel2_Scroll);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(200, 295);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 30);
            this.button2.TabIndex = 9;
            this.button2.Text = "삭제";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(15, 295);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 30);
            this.button1.TabIndex = 8;
            this.button1.Text = "닫기";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(40, 11);
            this.textBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(211, 25);
            this.textBox3.TabIndex = 7;
            this.textBox3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox3_KeyDown);
            this.textBox3.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox3_KeyUp);
            this.textBox3.Leave += new System.EventHandler(this.textBox3_Leave);
            this.textBox3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.textBox3_MouseDown);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(15, 173);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(260, 118);
            this.textBox1.TabIndex = 6;
            this.textBox1.Leave += new System.EventHandler(this.textBox1_Leave);
            this.textBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.textBox1_MouseDown);
            this.textBox1.MouseLeave += new System.EventHandler(this.textBox1_MouseLeave);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // printDocument1
            // 
            this.printDocument1.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.printDocument1_BeginPrint);
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // panel_Calendar
            // 
            this.panel_Calendar.Location = new System.Drawing.Point(14, 256);
            this.panel_Calendar.Name = "panel_Calendar";
            this.panel_Calendar.Size = new System.Drawing.Size(280, 68);
            this.panel_Calendar.TabIndex = 3;
            this.panel_Calendar.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_Calendar_Paint);
            this.panel_Calendar.Resize += new System.EventHandler(this.panel_Calendar_Resize);
            // 
            // MainFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 403);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MinimumSize = new System.Drawing.Size(400, 47);
            this.Name = "MainFrame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "WellaTodo v0.15";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFrame_FormClosing);
            this.Load += new System.EventHandler(this.MainFrame_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainFrame_Paint);
            this.Resize += new System.EventHandler(this.MainFrame_Resize);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.panel_Header.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TextBox textBox_Task;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox textBox_AddList;
        private System.Windows.Forms.Label labelUserName;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel_Menulist;
        private System.Windows.Forms.Label label_ListName;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.Panel panel_Header;
        private System.Windows.Forms.Panel panel_Calendar;
    }
}

