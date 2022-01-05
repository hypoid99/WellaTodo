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
		MainModel m_model;

		public MainController(IView v, IModel m)
		{
			m_view = v;
			m_model = (MainModel)m;

			m_view.SetController(this);
			m_model.Attach_Model_Event((IModelObserver)m_view);
			m_view.View_Changed_Event += new ViewHandler<IView>(this.View_Changed_Event_method);
			m_view.Add_Task_Event += new ViewHandler<IView>(Add_Task_Event_method);
			m_view.Delete_Task_Event += new ViewHandler<IView>(Delete_Task_Event_method);
		}

		public MainController(IView v, MainModel m)
		{
			m_view = v;
			m_model = m;

			m_view.SetController(this);
			m_model.Attach_Model_Event((IModelObserver)m_view);
			m_view.View_Changed_Event += new ViewHandler<IView>(View_Changed_Event_method);
		}

		public void View_Changed_Event_method(IView v, ViewEventArgs e)
		{
			Console.WriteLine(">MainController::Changed_View_Event_method");
			Console.WriteLine("listName:" + e.Item.DC_listName);
			Console.WriteLine("title:" + e.Item.DC_title);
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

		public void Add_Task_Event_method(IView v, ViewEventArgs e)
		{
			Console.WriteLine(">MainController::Add_Task_Event_method");
			Console.WriteLine("listName:" + e.Item.DC_listName);
			Console.WriteLine("title:" + e.Item.DC_title);
			m_model.Add_Task(e.Item.DC_listName, e.Item.DC_title);
		}

		public void PerformAddTask(string list, string title)
        {
			Console.WriteLine(">MainController::PerformAddTask");
			m_model.Add_Task(list, title);
        }

		public void Delete_Task_Event_method(IView v, ViewEventArgs e)
		{
			Console.WriteLine(">MainController::Delete_Task_Event_method");
			Console.WriteLine("listName:" + e.Item.DC_listName);
			Console.WriteLine("title:" + e.Item.DC_title);
			//m_model.SetValue(e.value);
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


