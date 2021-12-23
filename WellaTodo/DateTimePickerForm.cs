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
    public partial class DateTimePickerForm : Form
    {
        DateTime _selectedDateTime;
        public DateTime SelectedDateTime { get => _selectedDateTime; set => _selectedDateTime = value; }
        bool isSelected;
        public bool IsSelected { get => isSelected; set => isSelected = value; }

        public DateTimePickerForm()
        {
            InitializeComponent();
        }

        private void DateTimePickerForm_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Location = new Point(30, 25);
            dateTimePicker1.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.ShowUpDown = true;

            monthCalendar1.Location = new Point(30, 50);

            IsSelected = false;
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
            SelectedDateTime = dateTimePicker1.Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            isSelected = true;
            SelectedDateTime = dateTimePicker1.Value;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            isSelected = false;
            Close();
        }
    }
}
