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

        //void Execute(RequestContext requestContext);
        void Execute();
    }

    public abstract class ControllerBase : IController
    {
        private IView _view;

        public virtual IView View
        {
            get { return _view; }
            set { _view = value; }
        }

        protected virtual void Execute()
        {
            Initialize();
            ExecuteCore();
        }

        protected abstract void ExecuteCore();

        protected virtual void Initialize()
        {
            //ControllerContext = new ControllerContext(requestContext, this);
        }

        void IController.Execute()
        {
            //Execute(requestContext);
            Execute();
        }
    }

    public class MyCustomController : ControllerBase
    {
        protected override void ExecuteCore()
        {
            string controllername = "controller".ToString();
            string actionName = "action".ToString();
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