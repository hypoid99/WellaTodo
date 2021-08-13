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

            Console.WriteLine(">WellaTodo start");
            IModel mainModel = new MainModel();
            MainFrame mainFrame = new MainFrame();
            IController mainController = new MainController(mainFrame, mainModel);
            Console.WriteLine(">WellaTodo running");
            Application.Run(mainFrame);
        }
    }
}
