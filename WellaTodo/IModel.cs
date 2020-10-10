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
        public int value;
        public ModelEventArgs(int val) { value = val; }
    }

    public interface IModelObserver
    {
        void Changed_Model_Event_method(IModel model, ModelEventArgs e);
    }
    public interface IModel
	{
        void Attach_Model_Event(IModelObserver imo);
        void Update_Model();
		void setValue(int value);
	}
}