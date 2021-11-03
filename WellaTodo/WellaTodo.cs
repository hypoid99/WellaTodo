//======================================================================================
// Wella Todo
// Copyright Honeysoft 20200924 v0.7
// modified 2021.7.19 -> 2021.9.4 -> 2021.9.11 -> 2021.9.18 -> 2021.9.23 -> 2021.9.27
//       -> 2021.10.3 -> 2021.10.7 -> 2021.10.31
//======================================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WellaTodo
{
    static class WellaTodo
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MainFrame mainFrame = new MainFrame();
            MainModel mainModel = new MainModel();
            new MainController(mainFrame, mainModel);
            Application.Run(mainFrame);
        }

        internal class StarCheckbox
        {
        }
    }
}