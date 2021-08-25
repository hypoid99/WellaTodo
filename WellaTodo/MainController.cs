using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellaTodo
{
	public class MainController : IController
	{
		IView m_view;
		IModel m_model;

		public MainController(IView v, IModel m)
		{
			m_view = v;
			m_model = m;
			m_view.SetController(this);
			m_model.Attach_Model_Event((IModelObserver)m_view);
			m_view.Changed_View_Event += new ViewHandler<IView>(this.Changedm_view_Event_method);
		}

		public void Changedm_view_Event_method(IView v, ViewEventArgs e)
		{
			Console.WriteLine(">MainController::Changedm_view_Event_method");
			//m_model.SetValue(e.value);
		}

		public void Update_Model()
        {
			m_model.Update_Model();
        }
	
	/*
		public void Changedm_view()
        {
			Console.WriteLine(">MainController::Changedm_view");
			m_model.Update_model();
        }

		public void Initiate_view()
        {
			m_view.Initiate_view();
        }
	*/
	}
}


