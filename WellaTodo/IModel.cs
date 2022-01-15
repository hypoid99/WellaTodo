using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellaTodo
{
	public delegate void ModelHandler<IModel>(IModel sender, ModelEventArgs e);

    public class ModelEventArgs : EventArgs
    {
        public CDataCell Item { get; private set; }
        public WParam Param { get; set; }
        public List<Object> Data { get; set; }

        public ModelEventArgs() { Item = null; }
        public ModelEventArgs(CDataCell dc) { Item = dc; }
        public ModelEventArgs(WParam param) { Param = param; }
        public ModelEventArgs(CDataCell dc, WParam param ) { Item = dc; Param = param; }
    }

    public interface IModelObserver
    {
        void Update_View(IModel model, ModelEventArgs e);
        //void Update_Add_Task(IModel model, ModelEventArgs e);
        //void Update_Delete_Task(IModel model, ModelEventArgs e);
    }

    public interface IModel
	{
        //List<IModelObserver> ObserverList { get; set; }
        void Add_Observer(IModelObserver imo);
        void Remove_Observer(IModelObserver imo);
        void Notify_Observer();
	}
}