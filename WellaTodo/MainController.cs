using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellaTodo
{
	public class MainController : IController
	{
		IView m_View;
		IModel m_Model;

		public MainController(IView view, IModel model)
		{
			Console.WriteLine(">MainController Construction");
			m_View = view;
			m_Model = model;
			m_View.SetController(this);
			Console.WriteLine(">IView & IModel assigned to Controller");
			m_Model.Attach_Model_Event((IModelObserver)view);
			m_View.Changed_View_Event += new ViewHandler<IView>(this.Changed_View_Event_method);
		}

		public void Changed_View_Event_method(IView v, ViewEventArgs e)
		{
			Console.WriteLine(">MainController::Changed_View_Event_method");
			m_Model.setValue(e.value);
		}

		public void Changed_View()
        {
			Console.WriteLine(">MainController::Changed_View");
			m_Model.Update_Model();
        }

		public void Initiate_View()
        {
			m_View.Initiate_View();
        }
	}
}


