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

		public void Perform_Modify_Remind(CDataCell dc, int type, DateTime dt)
		{
			CDataCell data = Find(dc);
			data.DC_remindType = type;
			data.DC_remindTime = dt;
			m_model.Modifiy_Remind(dc);
		}

		public void Perform_Modify_Planned(CDataCell dc, int type, DateTime dt)
		{
			CDataCell data = Find(dc);
			data.DC_deadlineType = type;
			data.DC_deadlineTime = dt;
		}

		public void Perform_Important_Process(CDataCell dc)
		{
			dc.DC_important = !dc.DC_important;
			m_model.Important_Process(dc);
		}

		public void Perform_Complete_Process(CDataCell dc)
        {
			dc.DC_complete = !dc.DC_complete;
			m_model.Complete_Process(dc);
		}

		public void Perform_Modify_Task_Title(CDataCell dc)
		{
			m_model.Modify_Task_Title(dc);
		}

		public void Perform_Modify_Task_Memo(CDataCell dc)
		{
			m_model.Modify_Task_Memo(dc);
		}


		public void Perform_Task_Move_Up(CDataCell dc)
		{
			m_model.Task_Move_Up(dc);
		}

		public void Perform_Task_Move_Down(CDataCell dc)
		{
			m_model.Task_Move_Down(dc);
		}

		public void Perform_MyToday_Process(CDataCell dc)
        {
			DateTime dt = DateTime.Now;

			if (dc.DC_myToday)
			{
				dc.DC_myToday = false;  // 해제
				dc.DC_myTodayTime = default;
			}
			else
			{
				dc.DC_myToday = true; // 설정
				dc.DC_myTodayTime = new DateTime(dt.Year, dt.Month, dt.Day, 22, 00, 00);
			}
			m_model.Modifiy_MyToday(dc);
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


