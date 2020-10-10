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

        public event ViewHandler<IView> Changed_View_Event;

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
