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
		IModel model;
		MainModel m_model;

		public MainController(IView v, IModel m)
		{
			m_view = v;
			model = m;

			m_view.SetIController(this);
			model.Attach_Model_Event((IModelObserver)m_view);
			m_view.Changed_View_Event += new ViewHandler<IView>(this.Changed_View_Event_method);
		}

		public MainController(IView v, MainModel m)
		{
			m_view = v;
			m_model = m;

			m_view.SetController(this);
			m_model.Attach_Model_Event((IModelObserver)m_view);
			m_view.Changed_View_Event += new ViewHandler<IView>(this.Changed_View_Event_method);
		}

		public void Changed_View_Event_method(IView v, ViewEventArgs e)
		{
			Console.WriteLine(">MainController::Changed_View_Event_method");
			//m_model.SetValue(e.value);
		}

		public void Update_Model()
        {
			Console.WriteLine(">MainController::Update_Model");
			m_model.Update_Model();
        }

		public MainModel Get_Model()
        {
			return m_model;
        }
	
	/*
		public void Changed_View()
        {
			Console.WriteLine(">MainController::Changed_View");
			m_model.Update_model();
        }

		public void Initiate_view()
        {
			m_view.Initiate_view();
        }
	*/
	}
}


