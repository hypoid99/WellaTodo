﻿// ----------------------------------------------------------
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

using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;

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

		// ----------------------------------------------------
		// View Event
		// ----------------------------------------------------
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

		public void Verify_DataCell(CDataCell dc)
        {
			m_model.Verify_DataCell(dc);
        }

		// --------------------------------------------------------
		// Load/Save/Open/Print 메서드
		// --------------------------------------------------------
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
		// Perform Command (Menulist)
		// -----------------------------------------------------------
		public bool Perform_Menulist_Add(string MenuName)
        {
			if (MenuName.Length == 0)
			{
				Send_Log_Message("Warning>MainController::Perform_Menulist_Add -> Leghth of Menu Name is zero!!");
				return false;
			}

			if (MenuName == "오늘 할 일" || MenuName == "중요" || MenuName == "계획된 일정" || MenuName == "완료됨" || MenuName == "작업")
			{
				Send_Log_Message("Warning>MainController::Perform_Menulist_Add -> Can't Add MenuList for Reserved Menu!!");
				return false;
			}

			if (m_model.IsThereSameMenuName(MenuName))
            {
				Send_Log_Message("Warning>MainController::Perform_Menulist_Add -> Can't Add MenuList for Same menu name exist!!");
				return false;
			}

			Send_Log_Message("2>MainController::Perform_Menulist_Add : " + MenuName);
			m_model.Menulist_Add(MenuName);
			return true;
		}

		public bool Perform_Menulist_Rename(string source, string target)
        {
			if (target == source)
			{
				Send_Log_Message("Warning>MainController::Perform_Menulist_Rename -> Before & After Name is same!!");
				return false;
			}

			if (target.Length == 0)
			{
				Send_Log_Message("Warning>MainController::Perform_Menulist_Rename -> Leghth of Menu Name is zero!!");
				return false;
			}

			if (target == "오늘 할 일" || target == "중요" || target == "계획된 일정" || target == "완료됨" || target == "작업")
			{
				Send_Log_Message("Warning>MainController::Perform_Menulist_Rename -> Can't Add MenuList for Reserved Menu!!");
				return false;
			}

			if (m_model.IsThereSameMenuName(target))
			{
				Send_Log_Message("Warning>MainController::Perform_Menulist_Rename -> Can't Add MenuList for Same menu name exist!!");
				return false;
			}

			Send_Log_Message("2>MainController::Perform_Menulist_Rename : from " + source + " to " + target);
			m_model.Menulist_Rename(source, target);
			return true;
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

		// -----------------------------------------------------------
		// Perform Command (Task)
		// -----------------------------------------------------------
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
			if (source.DC_task_ID == target.DC_task_ID)
			{
				Send_Log_Message("2>MainController::Perform_Task_Move_To -> Same task can't move");
				return;
			}

			if (source.DC_complete || target.DC_complete)
			{
				Send_Log_Message("2>MainController::Perform_Task_Move_To -> Can't move Completed Task");
				return;
			}

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

		public void Perform_Transfer_Task(CDataCell dc, string target)
        {
			if (dc.DC_listName == target)
			{
				Send_Log_Message("Warning>MainController::Perform_Transfer_Task -> Can't transfer item as same list");
				return;
			}
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
		// Perform Command - NotePad 문서편집
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
		// Perform Command - Calendar 일정 
		// ----------------------------------------------------------
		public void Perform_Add_Plan(CDataCell dc)
		{
			Send_Log_Message("2>MainController::Perform_Add_Plan : " + dc.DC_title);
			m_model.Add_Plan(dc);
		}

		// ----------------------------------------------------------
		// Perform Command - BulletinBoard 메모
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
			IEnumerable <CDataCell> dataset = from CDataCell dc in m_model.GetTaskCollection() 
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
				//deepCopy.Add((CDataCell)dc.Clone());
				//deepCopy.Add((CDataCell)DeepClone(dc));
				deepCopy.Add((CDataCell)SerializableDeepClone(dc));
			}
			return deepCopy;
		}

		// ----------------------------------------------------
		// Serializable 객체에 대한  Deep Clone 구현
		// ----------------------------------------------------
		private static T SerializableDeepClone<T>(T obj)
		{
			using (var ms = new MemoryStream())
			{
				BinaryFormatter formatter = new BinaryFormatter();
				formatter.Serialize(ms, obj);
				ms.Position = 0;

				return (T)formatter.Deserialize(ms);
			}
		}

		// ----------------------------------------------------
		// Deep Clone 구현
		// ----------------------------------------------------
		private static T DeepClone<T>(T obj)
		{
			if (obj == null)
				throw new ArgumentNullException("Object cannot be null.");

			return (T)Process(obj, new Dictionary<object, object>() { });
		}

		private static object Process(object obj, Dictionary<object, object> circular)
		{
			if (obj == null) return null;

			Type type = obj.GetType();

			if (type.IsValueType || type == typeof(string)) return obj;

			if (type.IsArray)
			{
				if (circular.ContainsKey(obj)) return circular[obj];

				string typeNoArray = type.FullName.Replace("[]", string.Empty);
				Type elementType = Type.GetType(typeNoArray + ", " + type.Assembly.FullName);
				var array = obj as Array;
				Array arrCopied = Array.CreateInstance(elementType, array.Length);

				circular[obj] = arrCopied;

				for (int i = 0; i < array.Length; i++)
				{
					object element = array.GetValue(i);
					object objCopy = null;

					if (element != null && circular.ContainsKey(element))
						objCopy = circular[element];
					else
						objCopy = Process(element, circular);

					arrCopied.SetValue(objCopy, i);
				}
				return Convert.ChangeType(arrCopied, obj.GetType());
			}

			if (type.IsClass)
			{
				if (circular.ContainsKey(obj)) return circular[obj];

				object objValue = Activator.CreateInstance(obj.GetType());
				circular[obj] = objValue;
				FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

				foreach (FieldInfo field in fields)
				{
					object fieldValue = field.GetValue(obj);

					if (fieldValue == null)
						continue;

					object objCopy = circular.ContainsKey(fieldValue) ? circular[fieldValue] : Process(fieldValue, circular);
					field.SetValue(objValue, objCopy);
				}
				return objValue;
			}
			else
			{
				throw new ArgumentException("Unknown type");
			}
		}
	}
}


