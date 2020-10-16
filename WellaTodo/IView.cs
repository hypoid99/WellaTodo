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

        void SetController(IController controller);

        void Initiate_View();
        void Clear_View();
        void Add_Model_To_View(CDataCell dc);
        void Update_View_With_Changed_Model(CDataCell dc);
        void Remove_Model_From_View(CDataCell dc);
    }
}
