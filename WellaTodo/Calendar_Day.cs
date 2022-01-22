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
    public partial class Calendar_Day : Label
    {
        DateTime present_Day = default;
        public DateTime Present_Day { get => present_Day; set => present_Day = value; }

        public Calendar_Day()
        {
            InitializeComponent();
        }


        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
