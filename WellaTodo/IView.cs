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
        public int value;
        public ViewEventArgs(int val) { value = val; }
    }

    public interface IView
    {
        event ViewHandler<IView> Changed_View_Event;

        void SetController(MainController controller);
    }
}
