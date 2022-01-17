// ----------------------------------------------------------
// Main Controller
// ----------------------------------------------------------
// 1. 사용자 요청을 분석한다
// 2. 뷰를 통해 입력된 데이터 가져오기
// 3. 프레임 이동
// 4. 유효성 검사
// 5. 모델 객체 생성
// ----------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace WellaTodo
{
	public class MainController : IController
	{
		IView m_view;
		List<IView> m_viewList = new List<IView>();
		MainModel m_model;

		int m_Task_ID_Num = 0;

		public MainController(IView v, IModel m)
		{
			m_view = v;
			m_model = (MainModel)m;

			Initiate();
		}

		public MainController(IView v, MainModel m)
		{
			m_view = v;
			m_model = m;

			Initiate();
		}

		public void Initiate()
        {
			m_view.SetController(this);
			m_view.View_Changed_Event += new ViewHandler<IView>(View_Changed_Event_method);

			m_model.Add_Observer((IModelObserver)m_view);

			Load_Data_File();
		}

		public void Add_View(IView view)
        {
			view.SetController(this);
			view.View_Changed_Event += new ViewHandler<IView>(View_Changed_Event_method);

			m_model.Add_Observer((IModelObserver)view);
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

		public void Notify_Log_Message(string msg)
        {
			m_model.Notify_Log_Message(msg);
        }

		public void Load_Data_File()
        {
			Stream rs = new FileStream("task.dat", FileMode.Open);
			BinaryFormatter deserializer = new BinaryFormatter();

			List<CDataCell> todo_data = (List<CDataCell>)deserializer.Deserialize(rs);
			rs.Close();

			m_Task_ID_Num = todo_data.Count - 1;
			int pos = 0;
			for (int i = m_Task_ID_Num; i >= 0; i--)
            {
				todo_data[i].DC_task_ID = pos;
				pos++;
            }

			m_model.SetDataCollection(todo_data);
			m_model.Load_Data();
		}

		public void Save_Data_File()
        {
			Stream ws = new FileStream("task.dat", FileMode.Create);
			BinaryFormatter serializer = new BinaryFormatter();

			serializer.Serialize(ws, m_model.GetDataCollection());
			ws.Close();

			m_model.Save_Data();
		}

		public void Perform_Menulist_Rename(string source, string target)
        {
			m_model.Menulist_Rename(source, target);
        }

		public void Perform_Menulist_Delete(string target)
        {
			m_model.Menulist_Delete(target);
		}

		public void Perform_Add_Task(CDataCell dc)
        {
			m_Task_ID_Num++;
			dc.DC_task_ID = m_Task_ID_Num;
			m_model.Add_Task(dc);
        }

		public void Perform_Delete_Task(CDataCell dc)
        {
			Console.WriteLine("2>MainController::Perform_Delete_Task : " + dc.DC_title);
			m_model.Delete_Task(dc);
		}

		public void Perform_Modify_MyToday(CDataCell dc, bool myToday, DateTime dt)
		{
			dc.DC_myToday = myToday;
			dc.DC_myTodayTime = dt;
			m_model.Modifiy_MyToday(dc);
		}

		public void Perform_Modify_Remind(CDataCell dc, int type, DateTime dt)
		{
			dc.DC_remindType = type;
			dc.DC_remindTime = dt;
			m_model.Modifiy_Remind(dc);
		}

		public void Perform_Modify_Planned(CDataCell dc, int type, DateTime dt)
		{
			Console.WriteLine("2>MainController::Perform_Modify_Planned : type " + type);
			dc.DC_deadlineType = type;
			dc.DC_deadlineTime = dt;
			m_model.Modifiy_Planned(dc);
		}

		public void Perform_Modify_Repeat(CDataCell dc, int type, DateTime dt)
		{
			dc.DC_repeatType = type;
			dc.DC_repeatTime = dt;
			m_model.Modifiy_Repeat(dc);
		}

		public void Perform_Complete_Process(CDataCell dc)
		{
			Console.WriteLine("2>MainController::Perform_Complete_Process : " + dc.DC_complete);
			//m_model.Notify_Log_Message("2>MainController::Perform_Complete_Process : " + dc.DC_complete);
			m_model.Complete_Process(dc);
		}

		public void Perform_Important_Process(CDataCell dc)
		{
			Console.WriteLine("2>MainController::Perform_Important_Process : " + dc.DC_important);
			//m_model.Notify_Log_Message("2>MainController::Perform_Important_Process : " + dc.DC_important);
			m_model.Important_Process(dc);
		}

		public void Perform_Modify_Task_Title(CDataCell dc)
		{
			Console.WriteLine("2>MainController::Perform_Modify_Task_Title : " + dc.DC_title);
			m_model.Modify_Task_Title(dc);
		}

		public void Perform_Modify_Task_Memo(CDataCell dc)
		{
			Console.WriteLine("2>MainController::Perform_Modify_Task_Memo : " + dc.DC_title);
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

		public void Perform_Trasnfer_Task(CDataCell dc, string listname)
        {
			m_model.Transfer_Task(dc, listname);
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

		public IEnumerable<CDataCell> Query_MyToday()
        {
			IEnumerable<CDataCell> dataset = from CDataCell dt in m_model.GetDataCollection() 
											 where dt.DC_myToday && !dt.DC_complete select dt;
			List<CDataCell> deepCopy = List_DeepCopy(dataset);
			return deepCopy;
		}

		public IEnumerable<CDataCell> Query_Important()
		{
			IEnumerable<CDataCell> dataset = from CDataCell dc in m_model.GetDataCollection() 
											 where dc.DC_important && !dc.DC_complete select dc;
			List<CDataCell> deepCopy = List_DeepCopy(dataset);
			return deepCopy;
		}

		public IEnumerable<CDataCell> Query_Planned()
		{
			IEnumerable<CDataCell> dataset = from CDataCell dc in m_model.GetDataCollection() 
											 where (dc.DC_deadlineType > 0 || dc.DC_repeatType > 0) && !dc.DC_complete select dc;
			List<CDataCell> deepCopy = List_DeepCopy(dataset);
			return deepCopy;
		}

		public IEnumerable<CDataCell> Query_Complete()
		{
			IEnumerable<CDataCell> dataset = from CDataCell dc in m_model.GetDataCollection() 
											 where dc.DC_complete == true select dc;
			List<CDataCell> deepCopy = List_DeepCopy(dataset);
			return deepCopy;
		}

		public IEnumerable<CDataCell> Query_Task(string listname)
		{
			IEnumerable < CDataCell > dataset = from CDataCell dc in m_model.GetDataCollection() 
												where dc.DC_listName == listname select dc;
			List<CDataCell> deepCopy = List_DeepCopy(dataset);
			return deepCopy;
		}

		public IEnumerable<CDataCell> Query_Month_Calendar(DateTime curDate)
        {
			IEnumerable<CDataCell> dataset = from CDataCell dc
											 in m_model.GetDataCollection()
											 where dc.DC_deadlineType > 0
											 && (curDate.Date == dc.DC_deadlineTime.Date)
											 select dc;
			List<CDataCell> deepCopy = List_DeepCopy(dataset);
			return deepCopy;
		}

		public IEnumerable<CDataCell> Query_Task_Calendar(CDataCell sd)
        {
			IEnumerable<CDataCell> dataset = from CDataCell dc in m_model.GetDataCollection()
											 where dc.DC_task_ID == sd.DC_task_ID
											 select dc;
			List<CDataCell> deepCopy = List_DeepCopy(dataset);
			return deepCopy;
		}

		public IEnumerable<CDataCell> Query_All_Task()
        {
			IEnumerable<CDataCell> dataset = from CDataCell dc in m_model.GetDataCollection()
											 where true
											 select dc;
			List<CDataCell> deepCopy = List_DeepCopy(dataset);
			return deepCopy;
		}

		private List<CDataCell> List_DeepCopy(IEnumerable<CDataCell> dataset)
        {
			List<CDataCell> deepCopy = new List<CDataCell>();
			foreach (CDataCell dc in dataset)
			{
				deepCopy.Add((CDataCell)dc.Clone());
			}
			return deepCopy;
		}

		public void Perform_Display_Data()
        {
			m_model.Display_Data();
        }

	}
}


