// copyright honeysoft 20200924 v0.1
// 수정작업 2021.7.19


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WellaTodo
{
    static class WellaTodo
    {
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new MainFrame());

            Console.WriteLine(">WellaTodo Program");
            IModel m_MainModel = new MainModel();
            MainFrame m_MainFrame = new MainFrame();
            IController m_MainController = new MainController(m_MainFrame, m_MainModel);
            Console.WriteLine(">MVC created");
            Application.Run(m_MainFrame);
            //MainModel model = new MainModel();
            //model.ID = 10;
        }
    }
}
