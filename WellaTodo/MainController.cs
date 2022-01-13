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
			m_model.Modifiy_Planned(dc);
		}

		public void Perform_Modify_Repeat(CDataCell dc, int type, DateTime dt)
		{
			CDataCell data = Find(dc);
			data.DC_repeatType = type;
			data.DC_repeatTime = dt;
			m_model.Modifiy_Repeat(dc);
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
			return from CDataCell dt in m_model.GetDataCollection() 
				   where dt.DC_myToday && !dt.DC_complete select dt;
		}

		public IEnumerable<CDataCell> Query_Important()
		{
			return from CDataCell dt in m_model.GetDataCollection() 
				   where dt.DC_important && !dt.DC_complete select dt;
		}

		public IEnumerable<CDataCell> Query_Planned()
		{
			return from CDataCell dt in m_model.GetDataCollection() 
				   where (dt.DC_deadlineType > 0 || dt.DC_repeatType > 0) && !dt.DC_complete select dt;
		}

		public IEnumerable<CDataCell> Query_Complete()
		{
			return from CDataCell dt in m_model.GetDataCollection() 
				   where dt.DC_complete == true select dt;
		}

		public IEnumerable<CDataCell> Query_Task(string listname)
		{
			IEnumerable < CDataCell > dataset = from CDataCell dt in m_model.GetDataCollection() 
												where dt.DC_listName == listname select dt;
			// 리턴시 Deep Copy 할 것 !!!
			List<CDataCell> deepCopy = new List<CDataCell>();
			foreach (CDataCell dc in dataset)
            {
				deepCopy.Add((CDataCell)dc.Clone());
            }
			return deepCopy;
		}

		private CDataCell Find(CDataCell dc)
        {
			IEnumerable<CDataCell> dataset = from CDataCell data in m_model.GetDataCollection()
											 where dc.Equals(data) select data;

			if (dataset.Count() != 1) Console.WriteLine("Not Found Item!!");  // 에러 출력
			return dataset.First();
		}

		public void Perform_Display_Data()
        {
			m_model.Display_Data();
        }

	}
}


