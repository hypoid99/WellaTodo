using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellaTodo
{
    public interface IController
    {
        //IView View { get; set; }
        //void Update_Model();
    }

    public class ControllerBase : IController
    {
        private IView view;

        public virtual IView View
        {
            get { return view; }
            set { view = value; }
        }
    }
}

// class MyController : ControllerBase
// {
//     public override IView View
//     {
//         get { return base.View; }
//         set
//         {
//             base.View = value;
//             // Do view initialization
//         }
//     }
// }
// 