using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WellaTodo
{
    public partial class CDataCellForm : Form, IView, IModelObserver
    {
        public event ViewHandler<IView> View_Changed_Event;

        MainController m_Controller;

        CDataCell m_DataCell;
        public CDataCell DataCell { get => m_DataCell; set => m_DataCell = value; }

        public CDataCellForm()
        {
            InitializeComponent();
        }

        public void SetController(MainController controller)
        {
            m_Controller = controller;
        }

        public void Update_View(IModel m, ModelEventArgs e)
        {
            CDataCell dc = e.Item;
            WParam param = e.Param;

            switch (param)
            {
                case WParam.WM_DATACELL:
                    Update_DataCell(dc);
                    break;
                default:
                    break;
            }
        }

        private void Send_Log_Message(string msg)
        {
            try
            {
                View_Changed_Event.Invoke(this, new ViewEventArgs(msg));
            }
            catch (Exception)
            {
                MessageBox.Show("Please enter a valid number");
            }
        }

        private void Update_DataCell(CDataCell dc)
        {
            if (dc == null) return;
            textBox_Task_ID.Text = dc.DC_task_ID.ToString();
            textBox_ListName.Text = dc.DC_listName;
            textBox_Title.Text = dc.DC_title;

            textBox_Complete.Text = dc.DC_complete ? "true" : "false";
            textBox_Important.Text = dc.DC_important ? "true" : "false";
            textBox_Memo.Text = dc.DC_memo;

            textBox_MyToday.Text = dc.DC_myToday.ToString();
            textBox_MyToday_DT.Text = dc.DC_myTodayTime.ToString("yyyy-MM-dd HH:mm:ss");
            textBox_Remind.Text = dc.DC_remindType.ToString();
            textBox_Remind_DT.Text = dc.DC_remindTime.ToString("yyyy-MM-dd HH:mm:ss");
            textBox_Deadline.Text = dc.DC_deadlineType.ToString();
            textBox_Deadline_DT.Text = dc.DC_deadlineTime.ToString("yyyy-MM-dd HH:mm:ss");
            textBox_Repeat.Text = dc.DC_repeatType.ToString();
            textBox_Repeat_DT.Text = dc.DC_repeatTime.ToString("yyyy-MM-dd HH:mm:ss");

            textBox_Bulletin.Text = dc.DC_bulletin ? "true" : "false";
            textBox_Archive.Text = dc.DC_archive ? "true" : "false";
            textBox_Memo_Tag.Text = dc.DC_memoTag.ToString();
            textBox_Memo_Color.Text = dc.DC_memoColor;

            textBox_NotePad.Text = dc.DC_notepad ? "true" : "false";
            richTextBox.Text = dc.DC_RTF;
        }

        private void CDataCellForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Hide();
                e.Cancel = true;
            }
        }
    }
}
