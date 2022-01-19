using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellaTodo
{
    public delegate void ViewHandler<IView>(IView sender, ViewEventArgs e);

    public class ViewEventArgs : EventArgs
    {
        public string Msg { get; set; }

        public ViewEventArgs(string msg) { Msg = msg; }
    }

    public interface IView
    {
        event ViewHandler<IView> View_Changed_Event;

        void SetController(MainController controller);
    }
}
