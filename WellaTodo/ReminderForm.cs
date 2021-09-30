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
    public partial class ReminderForm : Form
    {
        private bool isTomorrowRemind;
        public bool IsNextWeekRemind { get => isNextWeekRemind; set => isNextWeekRemind = value; }
        private bool isNextWeekRemind;
        public bool IsTomorrowRemind { get => isTomorrowRemind; set => isTomorrowRemind = value; }

        public ReminderForm()
        {
            InitializeComponent();
        }

        private void ReminderForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            CarendarForm carendarForm = new CarendarForm();

            carendarForm.ShowDialog();
        }
    }
}
