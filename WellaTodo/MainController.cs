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

			m_view.View_Changed_Event += new ViewHandler<IView>(View_Changed_Event_method);

			m_model.Attach_Model_Event((IModelObserver)m_view);
		}

		public MainController(IView v, MainModel m)
		{
			m_view = v;
			m_model = m;

			m_view.SetController(this);
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

		public void Perform_Add_Task(string listName, string taskTitle)
        {
			Console.WriteLine(">MainController::PerformAddTask");
			m_model.Add_Task(listName, taskTitle);
        }

		public void Perform_Modify_Task(CDataCell dc, string listName, string taskTitle, bool comp, bool impo)
        {
			IEnumerable<CDataCell> dataset = from CDataCell data in m_model.GetDataCollection() 
											 where dc.Equals(data) select data;
			Console.WriteLine("Perform_Modify_Task:" + dataset.Count());
			foreach (CDataCell data in dataset)
            {
				data.DC_listName = listName;
				data.DC_title = taskTitle;
				data.DC_complete = comp;
				data.DC_important = impo;
            }
		}

		public void Perform_Modify_MyToday(CDataCell dc, bool myToday, DateTime dt)
		{
			IEnumerable<CDataCell> dataset = from CDataCell data in m_model.GetDataCollection()
											 where dc.Equals(data)
											 select data;
			Console.WriteLine("Perform_Modify_Task_MyToday:" + dataset.Count());
			foreach (CDataCell data in dataset)
			{
				data.DC_myToday = myToday;
				data.DC_myTodayTime = dt;
			}
		}

		public void Perform_Modify_Planned(CDataCell dc, int type, DateTime dt)
		{
			IEnumerable<CDataCell> dataset = from CDataCell data in m_model.GetDataCollection()
											 where dc.Equals(data)
											 select data;
			Console.WriteLine("Perform_Modify_Planned:" + dataset.Count());
			foreach (CDataCell data in dataset)
			{
				data.DC_deadlineType = type;
				data.DC_deadlineTime = dt;
			}
		}
	}
}


