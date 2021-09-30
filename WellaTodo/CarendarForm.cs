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
    public partial class CarendarForm : Form
    {
        public CarendarForm()
        {
            InitializeComponent();
        }

        private void CarendarForm_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Location = new Point(30, 25);
            dateTimePicker1.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            dateTimePicker1.Format = DateTimePickerFormat.Custom ;
            dateTimePicker1.ShowUpDown = true;

            monthCalendar1.Location = new Point(30, 50);
        }

        private void ConvertStringToDateTime()
        {
            //dateTimePicker1.Value = Convert.ToDateTime(textBox.Text);
        }

        private void ConvertDateTimeToString()
        {
            //textBox.Text = dateTimePicker1.Value.ToString("yyyy-MM-dd");
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            monthCalendar1.SetDate(dateTimePicker1.Value);
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            dateTimePicker1.Value = monthCalendar1.SelectionStart;

            // 현재시간으로 시간 변경 추가하기
        }
    }
}
