// ----------------------------------------------------------
// Main Model
// ----------------------------------------------------------
// 1. 데이터 저장, 수정, 삭제
// 2. 데이터 가공 처리
// 3. DB Access
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
	public enum WParam
	{
		WM_LOG_MESSAGE,
		WM_LOAD_DATA,
		WM_SAVE_DATA,
		WM_OPEN_DATA,
		WM_PRINT_DATA,
		WM_COMPLETE_PROCESS,
		WM_IMPORTANT_PROCESS,
		WM_TASK_ADD,
		WM_TASK_DELETE,
		WM_TASK_MOVE_TO,
		WM_TASK_MOVE_UP,
		WM_TASK_MOVE_DOWN,
		WM_MODIFY_TASK_TITLE,
		WM_MODIFY_TASK_MEMO,
		WM_MODIFY_MYTODAY,
		WM_MODIFY_REMIND,
		WM_MODIFY_PLANNED,
		WM_MODIFY_REPEAT,
		WM_MENULIST_ADD,
		WM_MENULIST_RENAME,
		WM_MENULIST_DELETE,
		WM_MENULIST_UP,
		WM_MENULIST_DOWN,
		WM_TRANSFER_TASK,
		WM_BULLETINBOARD_ADD
	}

	public class MainModel : IModel
	{
		public event ModelHandler<MainModel> Update_View;

		List<CDataCell> myTaskItems = new List<CDataCell>();
		List<string> myListNames = new List<string>();
		int m_Task_ID_Num = 0;
		string m_FileName = "task.dat";

		List<IModelObserver> ObserverList = new List<IModelObserver>();

        public MainModel()
		{

		}

		public void Add_Observer(IModelObserver imo)
        {
			Update_View += new ModelHandler<MainModel>(imo.Update_View);

			ObserverList.Add(imo);
		}

		public void Remove_Observer(IModelObserver imo)
        {
			Update_View -= new ModelHandler<MainModel>(imo.Update_View);

			ObserverList.Remove(imo);
		}

		public void Notify_Observer()
        {
			foreach (IModelObserver imo in ObserverList)
			{
				imo.Update_View(this, new ModelEventArgs());
			}
		}

		public List<CDataCell> GetTaskCollection()
        {
			return myTaskItems;
        }

		private void SetTaskCollection (List<CDataCell> task_collections)
        {
			// 중요 데이터 set
			myTaskItems = task_collections;
		}

		public List<string> GetListCollection()
		{
			return myListNames;
		}

		private void SetListCollection(List<string> list_collections)
		{
			// 중요 데이터 set
			myListNames = list_collections;
		}


		public void Load_Data()
        {
			List<CDataCell> todo_data = new List<CDataCell>();
			List<string> list_name = new List<string>();

			// Loading Task File
			if (File.Exists(m_FileName))
            {
				try
				{
					Stream rs = new FileStream(m_FileName, FileMode.Open);
					BinaryFormatter deserializer = new BinaryFormatter();

					todo_data = (List<CDataCell>)deserializer.Deserialize(rs);
					list_name = (List<string>)deserializer.Deserialize(rs);

					rs.Close();
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
				}
			}

			m_Task_ID_Num = todo_data.Count - 1;

			int pos = 0;
			for (int i = m_Task_ID_Num; i >= 0; i--)
			{
				todo_data[i].DC_task_ID = pos;
				pos++;
			}

			SetTaskCollection(todo_data); // deep copy로 변경할 것
			SetListCollection(list_name);

			Notify_Log_Message("3>MainModel::Load_Data -> " + m_FileName + "[" + m_Task_ID_Num + "]");
			Update_View.Invoke(this, new ModelEventArgs(WParam.WM_LOAD_DATA));
		}

		public void Save_Data()
		{
			// Saving Task File
			Stream ws = new FileStream(m_FileName, FileMode.Create);
			BinaryFormatter serializer = new BinaryFormatter();

			serializer.Serialize(ws, GetTaskCollection());
			serializer.Serialize(ws, GetListCollection());

			ws.Close();

			Notify_Log_Message(">MainModel::Save_Data -> " + m_FileName + "[" + m_Task_ID_Num + "]");
			Update_View.Invoke(this, new ModelEventArgs(WParam.WM_SAVE_DATA));
		}

		public void Open_Data(string filename)
        {
			List<CDataCell> todo_data = new List<CDataCell>();
			List<string> list_name = new List<string>();

			m_FileName = filename;

			// Loading Task File
			if (File.Exists(m_FileName))
			{
				try
				{
					Stream rs = new FileStream(m_FileName, FileMode.Open);
					BinaryFormatter deserializer = new BinaryFormatter();

					todo_data = (List<CDataCell>)deserializer.Deserialize(rs);
					list_name = (List<string>)deserializer.Deserialize(rs);

					rs.Close();
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
				}
			}

			m_Task_ID_Num = todo_data.Count - 1;

			int pos = 0;
			for (int i = m_Task_ID_Num; i >= 0; i--)
			{
				todo_data[i].DC_task_ID = pos;
				pos++;
			}

			SetTaskCollection(todo_data); // deep copy로 변경할 것
			SetListCollection(list_name);
			//Display_Data();
			Notify_Log_Message(">MainModel::Open_Data -> " + m_FileName + "[" + m_Task_ID_Num + "]");
			Update_View.Invoke(this, new ModelEventArgs(WParam.WM_OPEN_DATA));
		}

		public void Print_Data()
        {
			Notify_Log_Message(">MainModel::Print_Data");
			Update_View.Invoke(this, new ModelEventArgs(WParam.WM_PRINT_DATA));
		}

		public void Notify_Log_Message(string msg)
		{
			CDataCell dc = new CDataCell();
			dc.DC_title = msg;

			Update_View.Invoke(this, new ModelEventArgs(dc, WParam.WM_LOG_MESSAGE));
		}

		public void Menulist_Add(string target)
        {
			myListNames.Add(target);

			CDataCell list = new CDataCell();
			list.DC_listName = target;

			Notify_Log_Message("3>MainModel::Menulist_Add -> Created New Menulist : " + target);
			Update_View.Invoke(this, new ModelEventArgs((CDataCell)list.Clone(), WParam.WM_MENULIST_ADD));  // deep copy 할 것!
		}

		public void Menulist_Rename(string source, string target)
        {
			int pos = 0;
			foreach (CDataCell dc in myTaskItems)  // 데이터내 목록명 변경
			{
				if (source == dc.DC_listName)
				{
					myTaskItems[pos].DC_listName = target;
				}
				pos++;
			}

			pos = 0;
            for (int i = 0; i < myListNames.Count; i++)  // 목록 리스트내 목록 이름 변경
			{
                string str = myListNames[i];
                if (source == str)
				{
					myListNames[pos] = target;
				}
				pos++;
			}

			CDataCell rename = new CDataCell();
			rename.DC_listName = target;
			rename.DC_title = source;

			Notify_Log_Message("3>MainModel::Menulist_Rename -> from " + rename.DC_title + " to " + rename.DC_listName);
			Update_View.Invoke(this, new ModelEventArgs((CDataCell)rename.Clone(), WParam.WM_MENULIST_RENAME));
		}

		public void Menulist_Delete(string target)
        {
			CDataCell data = null;

			int pos = 0;
			while (pos < myTaskItems.Count) // 리스트 제거
			{
				data = myTaskItems[pos];

				if (data.DC_listName == target)
				{
					data.DC_listName = target;
					myTaskItems.RemoveAt(pos);
				}
				else
				{
					pos++;
				}
			}

			pos = 0;
			while (pos < myListNames.Count) // 목록 제거
			{
				if (myListNames[pos] == target)
				{
					myListNames.RemoveAt(pos);
					break;
				}
				pos++;
			}

			CDataCell dc = new CDataCell();
			dc.DC_listName = target;

			Notify_Log_Message("3>MainModel::Menulist_Delete -> Delete Task & Menu : " + target);
			Update_View.Invoke(this, new ModelEventArgs((CDataCell)dc.Clone(), WParam.WM_MENULIST_DELETE));
		}

		public void Menulist_Up(string target)
        {
			int pos = 0;
			for (int i = 0; i < myListNames.Count; i++)
			{
				if (myListNames[i] == target)
				{
					pos = i;
				}
			}

			if (pos == 1)  // 작업 메뉴 위로 UP 불가
			{
				Notify_Log_Message("Warning>MainModel::Menulist_Up -> Can't move Up");
				return;
			}

			string str = myListNames[pos]; //추출
			myListNames.RemoveAt(pos); //삭제
			myListNames.Insert(pos - 1, str); // 삽입

			CDataCell data = new CDataCell();

			Notify_Log_Message("3>MainModel::Menulist_Up : " + target);
			Update_View.Invoke(this, new ModelEventArgs((CDataCell)data.Clone(), WParam.WM_MENULIST_UP));
		}

		public void Menulist_Down(string target)
        {
			int pos = 0;
			for (int i = 0; i < myListNames.Count; i++)
			{
				if (myListNames[i] == target)
				{
					pos = i;
				}
			}

			if (pos == myListNames.Count - 1)
			{
				Notify_Log_Message("Warning>MainModel::Menulist_Down -> Can't move Down");
				return;
			}

			string str = myListNames[pos]; //추출
			myListNames.RemoveAt(pos); //삭제  
			myListNames.Insert(pos + 1, str); // 삽입

			CDataCell data = new CDataCell();

			Notify_Log_Message("3>MainModel::Menulist_Down : " + target);
			Update_View.Invoke(this, new ModelEventArgs((CDataCell)data.Clone(), WParam.WM_MENULIST_DOWN));
		}

		public void Transfer_Task(CDataCell dc, string target)
        {
			CDataCell data = Find(dc);

			if (data == null)
			{
				Notify_Log_Message("Warning>MainModel::Transfer_Task -> Find() Not Found Item!!");
				return;
			}

			string source = data.DC_listName;
			data.DC_listName = target;

			Notify_Log_Message("3>MainModel::Transfer_Task : from " + source + " to " + target);
			Update_View.Invoke(this, new ModelEventArgs((CDataCell)data.Clone(), WParam.WM_TRANSFER_TASK));
		}

		public void Add_Task(CDataCell dc)
		{
			CDataCell data = (CDataCell)dc.Clone();

			m_Task_ID_Num++;

			data.DC_task_ID = m_Task_ID_Num;
			data.DC_dateCreated = DateTime.Now;

			myTaskItems.Insert(0, data);

			Notify_Log_Message("3>MainModel::Add_Task -> Created New CDataCell [" + data.DC_task_ID + "]" + data.DC_title);
			Update_View.Invoke(this, new ModelEventArgs((CDataCell)data.Clone(), WParam.WM_TASK_ADD));  // deep copy 할 것!
		}

		public bool Delete_Task(CDataCell dc)
        {
			CDataCell data = Find(dc);

			if (data == null)
			{
				Notify_Log_Message("Warning>MainModel::Delete_Task -> Find() Not Found Item!!");
				return false;
			}

			if (myTaskItems.Remove(data))
            {
				Notify_Log_Message("3>MainModel::Delete_Task -> Data is Deleted!! [" + data.DC_task_ID + "]" + data.DC_title);
            }
            else
            {
				Notify_Log_Message("Warning>MainModel::Delete_Task -> Remove() Not Found Item!!");
				return false;
			}

			Update_View.Invoke(this, new ModelEventArgs((CDataCell)data.Clone(), WParam.WM_TASK_DELETE));
			return true;
		}

		public void Complete_Process(CDataCell dc)
		{
			CDataCell data = Find(dc);

			if (data == null)
			{
				Notify_Log_Message("Warning>MainModel::Complete_Process -> Find() Not Found Item!!");
				return;
			}

			data.DC_complete = dc.DC_complete;

			if (data.DC_complete)  // 완료시 맨밑으로, 해제시는 맨위로 보내기
			{
				myTaskItems.Remove(data); // 삭제
				myTaskItems.Insert(myTaskItems.Count, data); // 맨밑에 삽입
				Notify_Log_Message("3>MainModel::Complete_Process -> Set Complete to Bottom : " + data.DC_complete);
			}
			else
			{
				myTaskItems.Remove(data); // 삭제
				myTaskItems.Insert(0, data); // 맨위에 삽입
				Notify_Log_Message("3>MainModel::Complete_Process -> Reset Complete to Top " + data.DC_complete);
			}

			Update_View.Invoke(this, new ModelEventArgs((CDataCell)data.Clone(), WParam.WM_COMPLETE_PROCESS));
		}

		public void Important_Process(CDataCell dc)
		{
			CDataCell data = Find(dc);

			if (data == null)
			{
				Notify_Log_Message("Warning>MainModel::Important_Process -> Find() Not Found Item!!");
				return;
			}

			data.DC_important = dc.DC_important;

			if (data.DC_important && !data.DC_complete)  // 중요 & 미완료시 -> 맨위로 이동
			{
				myTaskItems.Remove(data); // 삭제
				myTaskItems.Insert(0, data); //삽입
            }

			Notify_Log_Message("3>MainModel::Important_Process : " + data.DC_important);
			Update_View.Invoke(this, new ModelEventArgs((CDataCell)data.Clone(), WParam.WM_IMPORTANT_PROCESS));
		}

		public void Modify_Task_Title(CDataCell dc)
        {
			CDataCell data = Find(dc);

			if (data == null)
			{
				Notify_Log_Message("Warning>MainModel::Modify_Task_Title -> Find() Not Found Item!!");
				return;
			}

			data.DC_title = dc.DC_title;

			Notify_Log_Message("3>MainModel::Modify_Task_Title : " + data.DC_title);
			Update_View.Invoke(this, new ModelEventArgs((CDataCell)data.Clone(), WParam.WM_MODIFY_TASK_TITLE));
		}

		public void Modify_Task_Memo(CDataCell dc)
		{
			CDataCell data = Find(dc);

			if (data == null)
			{
				Notify_Log_Message("Warning>MainModel::Modify_Task_Memo -> Find() Not Found Item!!");
				return;
			}

			data.DC_memo = dc.DC_memo;

			Notify_Log_Message("3>MainModel::Modify_Task_Memo : " + data.DC_title);
			Update_View.Invoke(this, new ModelEventArgs((CDataCell)data.Clone(), WParam.WM_MODIFY_TASK_MEMO));
		}

		public void Task_Move_To(CDataCell source, CDataCell target)
        {
			CDataCell src = Find(source);
			CDataCell tgt = Find(target);
			int src_index = 0;
			int tar_index = 0; ;
            for (int i = 0; i <= myTaskItems.Count - 1; i++)
            {
				if (source.DC_task_ID == myTaskItems[i].DC_task_ID) src_index = i;
				if (target.DC_task_ID == myTaskItems[i].DC_task_ID) tar_index = i;
            }
			Console.WriteLine("source" + source.DC_title + "-" + src_index);
			Console.WriteLine("target" + target.DC_title + "-" + tar_index);

			CDataCell temp = myTaskItems[src_index]; //추출
			myTaskItems.RemoveAt(src_index); //삭제
			myTaskItems.Insert(tar_index, temp); // 삽입

			Update_View.Invoke(this, new ModelEventArgs((CDataCell)target.Clone(), WParam.WM_TASK_MOVE_TO));
		}

		public bool Task_Move_Up(CDataCell dc)
		{
			CDataCell data = Find(dc);

			if (data == null)
			{
				Notify_Log_Message("Warning>MainModel::Task_Move_Up -> Find() Not Found Item!!");
				return false;
			}

			int pos = myTaskItems.IndexOf(data);
			if (pos == -1)
            {
				Notify_Log_Message("Warning>MainModel::Task_Move_Up -> IndexOf() Not Found Item!!");
				return false;
			}

			if (pos == 0)
			{
				Notify_Log_Message("Warning>MainModel::Task_Move_Up -> List Poistion is Top, Can't move!");
				return false;
			}

			if (!myTaskItems[pos].DC_complete)
			{
				int counter = 0;
				for (int i = pos - 1; i >= 0; i--)  // 상향 탐색
				{
					if (myTaskItems[i].DC_listName == myTaskItems[pos].DC_listName)
					{
						CDataCell temp = myTaskItems[pos]; //추출
						myTaskItems.RemoveAt(pos); //삭제
						myTaskItems.Insert(i, temp); // 삽입
						dc = myTaskItems[i];
						counter++;
						Notify_Log_Message("3>MainModel::Task_Move_Up -> Move Up Completed! " + dc.DC_title);
						break;
					}
				}
				if (counter == 0)
				{
					Notify_Log_Message("Warning>MainModel::Task_Move_Up -> Top position of ListName, Can't move!");
					return false;
				}
			}
			else 
			{
				Notify_Log_Message("Warning>MainModel::Task_Move_Up -> Task is complete, Can't move!");
				return false;
			}

			Update_View.Invoke(this, new ModelEventArgs((CDataCell)data.Clone(), WParam.WM_TASK_MOVE_UP));
			return true;
		}

		public bool Task_Move_Down(CDataCell dc)
		{
			CDataCell data = Find(dc);

			if (data == null)
            {
				Notify_Log_Message("Warning>MainModel::Task_Move_Down -> Find() Not Found Item!!");
				return false;
            }

			int pos = myTaskItems.IndexOf(data);
			if (pos == -1)
			{
				Notify_Log_Message("Warning>MainModel::Task_Move_Down -> IndexOf() Not Found Item!!");
				return false;
			}

			if (pos == (myTaskItems.Count - 1))
			{
				Notify_Log_Message("Warning>MainModel::Task_Move_Down -> List Poistion is Bottom, Can't move!");
				return false;
			}

			if (!myTaskItems[pos].DC_complete)
			{
				int counter = 0;
				for (int i = pos + 1; i <= myTaskItems.Count - 1; i++)  // 하향 탐색
				{
					if (myTaskItems[i].DC_complete)  // 아래 항목이 complete이면 맨 밑임
					{
						Notify_Log_Message("Warning>MainModel::Task_Move_Down -> Top position of complete tasks, Can't move!");
						return false;
					}
					if (myTaskItems[i].DC_listName == myTaskItems[pos].DC_listName)
					{
						CDataCell temp = myTaskItems[pos]; //추출
						myTaskItems.RemoveAt(pos); //삭제
						myTaskItems.Insert(i, temp); // 삽입
						dc = myTaskItems[i];
						counter++;
						Notify_Log_Message("3>MainModel::Task_Move_Down -> Move Down Completed! " + dc.DC_title);
						break;
					}
				}
				if (counter == 0)
				{
					Notify_Log_Message("Warning>MainModel::Task_Move_Down -> Bottom position of ListName, Can't move!");
					return false;
				}
			}
			else 
			{
				Notify_Log_Message("Warning>MainModel::Task_Move_Down -> Task is complete, Can't move!");
				return false; 
			}

			Update_View.Invoke(this, new ModelEventArgs((CDataCell)data.Clone(), WParam.WM_TASK_MOVE_DOWN));
			return true;
		}

		public void Modifiy_MyToday(CDataCell dc)
        {
			CDataCell data = Find(dc);

			if (data == null)
			{
				Notify_Log_Message("Warning>MainModel::Modifiy_MyToday -> Find() Not Found Item!!");
				return;
			}

			data.DC_myToday = dc.DC_myToday;
			data.DC_myTodayTime = dc.DC_myTodayTime;

			Notify_Log_Message("3>MainModel::Modifiy_MyToday : type " + data.DC_myToday);
			Update_View.Invoke(this, new ModelEventArgs((CDataCell)data.Clone(), WParam.WM_MODIFY_MYTODAY));
		}

		public void Modifiy_Remind(CDataCell dc)
        {
			CDataCell data = Find(dc);

			if (data == null)
			{
				Notify_Log_Message("Warning>MainModel::Modifiy_Remind -> Find() Not Found Item!!");
				return;
			}

			data.DC_remindType = dc.DC_remindType;
			data.DC_remindTime = dc.DC_remindTime;

			Notify_Log_Message("3>MainModel::Modifiy_Remind : type " + data.DC_remindType);
			Update_View.Invoke(this, new ModelEventArgs((CDataCell)data.Clone(), WParam.WM_MODIFY_REMIND));
		}

		public void Modifiy_Planned(CDataCell dc)
		{
			CDataCell data = Find(dc);

			if (data == null)
			{
				Notify_Log_Message("Warning>MainModel::Modifiy_Planned -> Find() Not Found Item!!");
				return;
			}

			data.DC_deadlineType = dc.DC_deadlineType;
			data.DC_deadlineTime = dc.DC_deadlineTime;

			Notify_Log_Message("3>MainModel::Modifiy_Planned : type " + data.DC_deadlineType);
			Update_View.Invoke(this, new ModelEventArgs((CDataCell)data.Clone(), WParam.WM_MODIFY_PLANNED));
		}

		public void Modifiy_Repeat(CDataCell dc)
		{
			CDataCell data = Find(dc);

			if (data == null)
			{
				Notify_Log_Message("Warning>MainModel::Modifiy_Repeat -> Find() Not Found Item!!");
				return;
			}

			data.DC_repeatType = dc.DC_repeatType;
			data.DC_repeatTime = dc.DC_repeatTime;

			Notify_Log_Message("3>MainModel::Modifiy_Repeat : type " + data.DC_repeatType);
			Update_View.Invoke(this, new ModelEventArgs((CDataCell)data.Clone(), WParam.WM_MODIFY_REPEAT));
		}

		public void Add_BulletinBoard(CDataCell dc)
        {
			CDataCell data = (CDataCell)dc.Clone();

			m_Task_ID_Num++;

			data.DC_task_ID = m_Task_ID_Num;
			data.DC_dateCreated = DateTime.Now;

			myTaskItems.Insert(0, data);

			Notify_Log_Message("3>MainModel::Add_BulletinBoard -> Created New CDataCell [" + data.DC_task_ID + "]" + data.DC_title);
			Update_View.Invoke(this, new ModelEventArgs((CDataCell)data.Clone(), WParam.WM_BULLETINBOARD_ADD));  // deep copy 할 것!
		}

		private CDataCell Find(CDataCell dc)
		{
			int task_ID = dc.DC_task_ID;
			IEnumerable<CDataCell> dataset = from CDataCell data in myTaskItems
											 where data.DC_task_ID == task_ID select data;
			if (dataset.Count() != 1)
			{
				Notify_Log_Message("Error>MainModel::Find -> Not Found Item!!");  // 에러 출력
				return null;
			}
			return dataset.First();
		}

		public void Display_Data()
        {
			foreach (CDataCell dc in myTaskItems)
            {
				Console.WriteLine("<"+dc.DC_task_ID+">"+"[" + dc.DC_listName + "]" + dc.DC_title);
            }

			foreach (string list in myListNames)
			{
				Console.WriteLine("<List Name : " + list);
			}
		}

	}
}

