// ----------------------------------------------------------
// Main Controller
// ----------------------------------------------------------
// 1. 사용자 요청을 분석한다
// 2. 뷰를 통해 입력된 데이터 가져오기
// 3. 프레임(뷰) 이동
// 4. 유효성 검사
// 5. 모델 객체 생성
// ----------------------------------------------------------

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
		List<IView> m_viewList = new List<IView>();
		MainModel m_model;

		public MainController(MainModel m)
        {
			m_model = m;
		}

		public MainController(IView v, IModel m)
		{
			m_view = v;
			m_model = (MainModel)m;
		}

		public MainController(IView v, MainModel m)
		{
			m_view = v;
			m_model = m;
		}

		public void Add_View(IView view)
        {
			view.SetController(this);
			view.View_Changed_Event += new ViewHandler<IView>(View_Changed_Event_method);

			m_model.Add_Observer((IModelObserver)view);
        }

		public void View_Changed_Event_method(IView v, ViewEventArgs e)
		{
			Send_Log_Message(e.Msg);
		}

		public void Send_Log_Message(string msg)
        {
			m_model.Notify_Log_Message(msg);
        }

		public void Send_DataCell(CDataCell dc)
        {
			m_model.Notify_DataCell(dc);
        }

		public void Load_Data_File()
        {
			Send_Log_Message("2>MainController::Load_Data_File");
			m_model.Load_Data();
		}

		public void Save_Data_File()
        {
			Send_Log_Message("2>MainController::Save_Data_File");
			m_model.Save_Data();
		}

		public void Print_Data_File()
        {
			Send_Log_Message("2>MainController::Print_Data_File");
			m_model.Print_Data();
        }

		public void New_Data_File()
        {
			Send_Log_Message("2>MainController::New_Data_File");
		}

		public void Open_Data_File(string filePath)
        {
			Send_Log_Message("2>MainController::Open_Data_File" + filePath);
			m_model.Open_Data(filePath);	
		}

		// -----------------------------------------------------------
		// Perform Command
		// -----------------------------------------------------------
		public void Perform_Menulist_Add(string target)
        {
			Send_Log_Message("2>MainController::Perform_Menulist_Add : " + target);
			m_model.Menulist_Add(target);
		}

		public void Perform_Menulist_Rename(string source, string target)
        {
			Send_Log_Message("2>MainController::Perform_Menulist_Rename : from " + source + " to " + target);
			m_model.Menulist_Rename(source, target);
        }

		public void Perform_Menulist_Delete(string target)
        {
			Send_Log_Message("2>MainController::Perform_Menulist_Delete : " + target);
			m_model.Menulist_Delete(target);
		}

		public void Perform_Menulist_Up(string target)
		{
			Send_Log_Message("2>MainController::Perform_Menulist_Up : " + target);
			m_model.Menulist_Up(target);
		}

		public void Perform_Menulist_Down(string target)
        {
			Send_Log_Message("2>MainController::Perform_Menulist_Down : " + target);
			m_model.Menulist_Down(target);
		}

		public void Perform_Add_Task(CDataCell dc)
        {
			Send_Log_Message("2>MainController::Perform_Add_Task : " + dc.DC_title);
			m_model.Add_Task(dc);
        }

		public void Perform_Delete_Task(CDataCell dc)
        {
			Send_Log_Message("2>MainController::Perform_Delete_Task : [" + dc.DC_task_ID + "]" + dc.DC_title);
			m_model.Delete_Task(dc);
		}

		public void Perform_Modify_MyToday(CDataCell dc, bool myToday, DateTime dt)
		{
			dc.DC_myToday = myToday;
			dc.DC_myTodayTime = dt;

			Send_Log_Message("2>MainController::Perform_Modify_MyToday : type [" + myToday + "]" + dc.DC_title);
			m_model.Modifiy_MyToday(dc);
		}

		public void Perform_Modify_Remind(CDataCell dc)
		{
			Send_Log_Message("2>MainController::Perform_Modify_Remind : " + dc.DC_title);
			m_model.Modifiy_Remind(dc);
		}

		public void Perform_Modify_Planned(CDataCell dc)
		{
			Send_Log_Message("2>MainController::Perform_Modify_Planned : type " + dc.DC_title);
			m_model.Modifiy_Planned(dc);
		}

		public void Perform_Modify_Repeat(CDataCell dc, int type, DateTime dt)
		{
			dc.DC_repeatType = type;
			dc.DC_repeatTime = dt;

			Send_Log_Message("2>MainController::Perform_Modify_Repeat : type [" + type + "]" + dc.DC_title);
			m_model.Modifiy_Repeat(dc);
		}

		public void Perform_Complete_Process(CDataCell dc)
		{
			Send_Log_Message("2>MainController::Perform_Complete_Process : " + dc.DC_complete);
			m_model.Complete_Process(dc);
		}

		public void Perform_Important_Process(CDataCell dc)
		{
			Send_Log_Message("2>MainController::Perform_Important_Process : " + dc.DC_important);
			m_model.Important_Process(dc);
		}

		public void Perform_Modify_Task_Title(CDataCell dc)
		{
			Send_Log_Message("2>MainController::Perform_Modify_Task_Title : " + dc.DC_title);
			m_model.Modify_Task_Title(dc);
		}

		public void Perform_Modify_Task_Memo(CDataCell dc)
		{
			Send_Log_Message("2>MainController::Perform_Modify_Task_Memo : " + dc.DC_title);
			m_model.Modify_Task_Memo(dc);
		}

		public void Perform_Task_Move_To(CDataCell source, CDataCell target)
        {
			Send_Log_Message("2>MainController::Perform_Task_Move_To -> Source : " + source.DC_title + " Target : " + target.DC_title);
			m_model.Task_Move_To(source, target);
		}

		public void Perform_Task_Move_Up(CDataCell dc)
		{
			Send_Log_Message("2>MainController::Perform_Task_Move_Up : " + dc.DC_title);
			m_model.Task_Move_Up(dc);
		}

		public void Perform_Task_Move_Down(CDataCell dc)
		{
			Send_Log_Message("2>MainController::Perform_Task_Move_Down : " + dc.DC_title);
			m_model.Task_Move_Down(dc);
		}

		public void Perform_Trasnfer_Task(CDataCell dc, string target)
        {
			Send_Log_Message("2>MainController::Perform_Trasnfer_Task : from " + dc.DC_listName + " to " + target);
			m_model.Transfer_Task(dc, target);
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

			Send_Log_Message("2>MainController::Perform_MyToday_Process : " + dc.DC_title);
			m_model.Modifiy_MyToday(dc);
		}

		// ----------------------------------------------------------
		// NotePad 문서편집
		// ----------------------------------------------------------
		public void Perform_Convert_NotePad(CDataCell dc)
        {
			Send_Log_Message("2>MainController::Perform_Convert_NotePad : " + dc.DC_title);
			m_model.Convert_NotePad(dc);
		}

		public void Perform_Transfer_RTF_Data(CDataCell dc)
		{
			Send_Log_Message("2>MainController::Perform_Transfer_RTF_Data : " + dc.DC_title);
			m_model.Transfer_RTF_Data(dc);
		}

		public void Perform_Save_RTF_Data(CDataCell dc)
		{
			Send_Log_Message("2>MainController::Perform_Save_RTF_Data : " + dc.DC_title);
			m_model.Save_RTF_Data(dc);
		}

		// ----------------------------------------------------------
		// Calendar 일정 
		// ----------------------------------------------------------
		public void Perform_Add_Plan(CDataCell dc)
		{
			Send_Log_Message("2>MainController::Perform_Add_Memo : " + dc.DC_title);
			m_model.Add_Plan(dc);
		}

		// ----------------------------------------------------------
		// BulletinBoard 메모
		// ----------------------------------------------------------
		public void Perform_Add_Memo(CDataCell dc)
		{
			Send_Log_Message("2>MainController::Perform_Add_Memo : " + dc.DC_title);
			m_model.Add_Memo(dc);
		}

		public void Perform_Modify_Memo_Archive(CDataCell dc)
		{
			Send_Log_Message("2>MainController::Perform_Modify_Memo_Archive : " + dc.DC_title);
			m_model.Modify_Memo_Archive(dc);
		}

		public void Perform_Modify_Memo_Color(CDataCell dc)
		{
			Send_Log_Message("2>MainController::Perform_Modify_Memo_Color : " + dc.DC_title);
			m_model.Modify_Memo_Color(dc);
		}

		public void Perform_Modify_Memo_Tag(CDataCell dc)
		{
			Send_Log_Message("2>MainController::Perform_Modify_Memo_Tag : " + dc.DC_title);
			m_model.Modify_Memo_Tag(dc);
		}

		public void Perform_Memo_Move_To(CDataCell source, CDataCell target)
		{
			Send_Log_Message("2>MainController::Perform_Memo_Move_To -> Source : " + source.DC_title + " Target : " + target.DC_title);
			m_model.Memo_Move_To(source, target);
		}

		// -----------------------------------------------------------
		// DB Query
		// -----------------------------------------------------------
		public List<string> Query_ListName()
		{
			List<string> list = new List<string>();
			IEnumerable<string> dataset = from string str in m_model.GetListCollection()
											 where true
											 select str;
			if (dataset.Count() > 0)
            {
				foreach (string item in dataset)
				{
					list.Add(item);
				}
            }
            else
            {
				Console.WriteLine("dataset.Count() is 0");
            }

			//Send_Log_Message("2>MainController::Query_ListName");
			return list;
		}

		public IEnumerable<CDataCell> Query_MyToday()
        {
			IEnumerable<CDataCell> dataset = from CDataCell dt in m_model.GetTaskCollection() 
											 where dt.DC_myToday && !dt.DC_complete select dt;
			List<CDataCell> deepCopy = List_DeepCopy(dataset);
			//Send_Log_Message("2>MainController::Query_MyToday -> Counter : " + deepCopy.Count);
			return deepCopy;
		}

		public IEnumerable<CDataCell> Query_Important()
		{
			IEnumerable<CDataCell> dataset = from CDataCell dc in m_model.GetTaskCollection() 
											 where dc.DC_important && !dc.DC_complete select dc;
			List<CDataCell> deepCopy = List_DeepCopy(dataset);
			//Send_Log_Message("2>MainController::Query_Important -> Counter : " + deepCopy.Count);
			return deepCopy;
		}

		public IEnumerable<CDataCell> Query_Planned()
		{
			IEnumerable<CDataCell> dataset = from CDataCell dc in m_model.GetTaskCollection() 
											 where (dc.DC_deadlineType > 0 || dc.DC_repeatType > 0) && !dc.DC_complete select dc;
			List<CDataCell> deepCopy = List_DeepCopy(dataset);
			//Send_Log_Message("2>MainController::Query_Planned -> Counter : " + deepCopy.Count);
			return deepCopy;
		}

		public IEnumerable<CDataCell> Query_Complete()
		{
			IEnumerable<CDataCell> dataset = from CDataCell dc in m_model.GetTaskCollection() 
											 where dc.DC_complete == true select dc;
			List<CDataCell> deepCopy = List_DeepCopy(dataset);
			//Send_Log_Message("2>MainController::Query_Complete -> Counter : " + deepCopy.Count);
			return deepCopy;
		}

		public IEnumerable<CDataCell> Query_Task(string listname)
		{
			IEnumerable < CDataCell > dataset = from CDataCell dc in m_model.GetTaskCollection() 
												where dc.DC_listName == listname select dc;
			List<CDataCell> deepCopy = List_DeepCopy(dataset);
			//Send_Log_Message("2>MainController::Query_Task -> Counter : " + deepCopy.Count);
			return deepCopy;
		}

		public IEnumerable<CDataCell> Query_Month_Calendar(DateTime curDate)
        {
			IEnumerable<CDataCell> dataset = from CDataCell dc
											 in m_model.GetTaskCollection()
											 where dc.DC_deadlineType > 0
											 && (curDate.Date == dc.DC_deadlineTime.Date)
											 select dc;
			List<CDataCell> deepCopy = List_DeepCopy(dataset);
			//Send_Log_Message("2>MainController::Query_Month_Calendar -> Counter : " + deepCopy.Count);
			return deepCopy;
		}

		public IEnumerable<CDataCell> Query_Task_Calendar(CDataCell sd)
        {
			IEnumerable<CDataCell> dataset = from CDataCell dc in m_model.GetTaskCollection()
											 where dc.DC_task_ID == sd.DC_task_ID
											 select dc;
			List<CDataCell> deepCopy = List_DeepCopy(dataset);
			//Send_Log_Message("2>MainController::Query_Task_Calendar -> Counter : " + deepCopy.Count);
			return deepCopy;
		}

		public IEnumerable<CDataCell> Query_All_Task()
        {
			IEnumerable<CDataCell> dataset = from CDataCell dc in m_model.GetTaskCollection()
											 where true
											 select dc;
			List<CDataCell> deepCopy = List_DeepCopy(dataset);
			//Send_Log_Message("2>MainController::Query_All_Task -> Counter : " + deepCopy.Count);
			return deepCopy;
		}

		public IEnumerable<CDataCell> Query_BulletineBoard()
		{
			IEnumerable<CDataCell> dataset = from CDataCell dt in m_model.GetTaskCollection()
											 where dt.DC_bulletin && (!dt.DC_archive)
											 select dt;
			List<CDataCell> deepCopy = List_DeepCopy(dataset);
			//Send_Log_Message("2>MainController::Query_BulletineBoard -> Counter : " + deepCopy.Count);
			return deepCopy;
		}

		public IEnumerable<CDataCell> Query_BulletineBoard_Archive()
		{
			IEnumerable<CDataCell> dataset = from CDataCell dt in m_model.GetTaskCollection()
											 where dt.DC_bulletin && (dt.DC_archive)
											 select dt;
			List<CDataCell> deepCopy = List_DeepCopy(dataset);
			//Send_Log_Message("2>MainController::Query_BulletineBoard -> Counter : " + deepCopy.Count);
			return deepCopy;
		}

		public IEnumerable<CDataCell> Query_BulletineBoard_Tag(int tag)
        {
			IEnumerable<CDataCell> dataset = from CDataCell dt in m_model.GetTaskCollection()
											 where dt.DC_bulletin && (!dt.DC_archive) && dt.DC_memoTag == tag
											 select dt;
			List<CDataCell> deepCopy = List_DeepCopy(dataset);
			//Send_Log_Message("2>MainController::Query_BulletineBoard_Tag -> Counter : " + deepCopy.Count);
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


