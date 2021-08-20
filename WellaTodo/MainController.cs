using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellaTodo
{
	public class MainController : IController
	{
		IView _view;
		IModel _model;

		public MainController(IView v, IModel m)
		{
			_view = v;
			_model = m;
			_view.SetController(this);
			_model.Attach_Model_Event((IModelObserver)_view);
			_view.Changed_View_Event += new ViewHandler<IView>(this.Changed_View_Event_method);
		}

		public void Changed_View_Event_method(IView v, ViewEventArgs e)
		{
			Console.WriteLine(">MainController::Changed_View_Event_method");
			//_model.SetValue(e.value);
		}

		public void Update_Model()
        {
			_model.Update_Model();
        }
	/*
		public IModel Get_Model()
        {
			return _model;
        }

		public void Changed_View()
        {
			Console.WriteLine(">MainController::Changed_View");
			_model.Update_Model();
        }

		public void Initiate_View()
        {
			_view.Initiate_View();
        }
	*/
	}
}


