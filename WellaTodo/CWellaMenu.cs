using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellaTodo
{
    class CWellaMenu : IView
    {
        IController m_Controller;

        public CWellaMenu()
        {
            Console.WriteLine(">CWellaMenu Construction");
        }

        public void setController(IController controller)
        {
            Console.WriteLine(">CWellaMenu::setController");
            m_Controller = controller;
        }
    }
}
