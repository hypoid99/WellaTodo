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

			m_model.Add_Observer((IModelObserver)m_view);
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

		public void Perform_Add_Task(CDataCell dc)
        {
			m_model.Add_Task(dc);
        }

		public void Perform_Delete_Task(CDataCell dc)
        {
			m_model.Delete_Task(dc);
		}

		public void Perform_Modify_Task(CDataCell dc, string listName, string taskTitle, bool comp, bool impo)
        {
			CDataCell data = Find(dc);
			data.DC_listName = listName;
			data.DC_title = taskTitle;
			data.DC_complete = comp;
			data.DC_important = impo;
		}

		public void Perform_Modify_MyToday(CDataCell dc, bool myToday, DateTime dt)
		{
			CDataCell data = Find(dc);
			data.DC_myToday = myToday;
			data.DC_myTodayTime = dt;
		}

		public void Perform_Modify_Planned(CDataCell dc, int type, DateTime dt)
		{
			CDataCell data = Find(dc);
			data.DC_deadlineType = type;
			data.DC_deadlineTime = dt;
		}

		public void Perform_Important_Process(CDataCell dc)
		{
			Console.WriteLine(">MainController::Perform_Important_Process:" + dc.DC_title);
			m_model.Important_Process(dc);
		}

		public void Perform_Complete_Process(CDataCell dc)
        {
			Console.WriteLine(">MainController::Perform_Complete_Process:" + dc.DC_title);
			m_model.Complete_Process(dc);
		}

		public void Perform_Task_Right_Click(CDataCell dc)
		{
			Console.WriteLine(">MainController::Perform_Task_Right_Click:" + dc.DC_title);
		}

		private CDataCell Find(CDataCell dc)
        {
			IEnumerable<CDataCell> dataset = from CDataCell data in m_model.GetDataCollection()
											 where dc.Equals(data)
											 select data;
			if (dataset.Count() != 1) Console.WriteLine("Not Found Item!!");  // 에러 출력
			return dataset.First();
		}

	}
}


