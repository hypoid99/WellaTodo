// copyright honeysoft v0.14

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
    public partial class MainFrame : Form, IView
    {
        IController m_Controller;

        public MainFrame()
        {
            Console.WriteLine(">MainFrame Construction");
            InitializeComponent();
        }

        public void setController(IController controller)
        {
            Console.WriteLine(">MainFrame::setController");
            m_Controller = controller;
        }
    }
}
