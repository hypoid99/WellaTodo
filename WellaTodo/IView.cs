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
        public CDataCell Item { get; private set; }

        public ViewEventArgs(CDataCell dc) { Item = dc; }
    }

    public interface IView
    {
        event ViewHandler<IView> View_Changed_Event;

        //IController Controller { get; set; }
        //string ViewName { get; set; }

        void SetController(MainController controller);
    }

    public interface INotifiedView
    {
        void Activate(bool activate);
        void Initialize();
    }

    public interface IViewsManager
    {
        //Navigator Navigator { get; set; }
        //ViewInfoCollection ViewInfos { get; set; }
        void ActivateView(string viewName);
        IView GetView(string viewName);
    }

    public class ViewsManagerBase : IViewsManager
    {
        //private Navigator navigator;
        //private ViewInfoCollection viewInfos;
        /*
        public virtual ViewInfoCollection ViewInfos
        {
            get { return viewInfos; }
            set { viewInfos = value; }
        }

        public virtual Navigator Navigator
        {
            get { return navigator; }
            set { navigator = value; }
        }
        */
        public virtual void ActivateView(string viewName)
        {

        }

        public virtual IView GetView(string viewName)
        {
            return null;
        }
    }
}
