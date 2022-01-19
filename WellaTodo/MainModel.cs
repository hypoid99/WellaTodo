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

namespace WellaTodo
{
	public enum WParam
	{
		WM_LOG_MESSAGE,
		WM_LOAD_DATA,
		WM_SAVE_DATA,
		WM_COMPLETE_PROCESS,
		WM_IMPORTANT_PROCESS,
		WM_TASK_ADD,
		WM_TASK_DELETE,
		WM_TASK_MOVE_UP,
		WM_TASK_MOVE_DOWN,
		WM_MODIFY_TASK_TITLE,
		WM_MODIFY_TASK_MEMO,
		WM_MODIFY_MYTODAY,
		WM_MODIFY_REMIND,
		WM_MODIFY_PLANNED,
		WM_MODIFY_REPEAT,
		WM_MENULIST_RENAME,
		WM_MENULIST_DELETE,
		WM_TRANSFER_TASK
	}

	public class MainModel : IModel
	{
		public event ModelHandler<MainModel> Update_View;

		List<CDataCell> myTaskItems = new List<CDataCell>();
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

		public void Update_Model()
		{
			// Model 데이타를 변경한다
			Console.WriteLine(">MainModel::Invoke Changed_Model_Event");
			//CDataCell dc1 = new CDataCell();
			//Model_Changed_Event.Invoke(this, new ModelEventArgs(dc1));
		}

		public List<CDataCell> GetDataCollection()
        {
			return myTaskItems;
        }

		public void SetDataCollection (List<CDataCell> data_collections)
        {
			myTaskItems = data_collections;
		}

		public void Load_Data()
        {
			Update_View.Invoke(this, new ModelEventArgs(WParam.WM_LOAD_DATA));
		}


		public void Save_Data()
		{
			Update_View.Invoke(this, new ModelEventArgs(WParam.WM_SAVE_DATA));
		}

		public void Notify_Log_Message(string msg)
		{
			CDataCell dc = new CDataCell();
			dc.DC_title = msg;
			Update_View.Invoke(this, new ModelEventArgs(dc, WParam.WM_LOG_MESSAGE));
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

			CDataCell rename = new CDataCell();
			rename.DC_listName = target;
			Update_View.Invoke(this, new ModelEventArgs(rename, WParam.WM_MENULIST_RENAME));
		}

		public void Menulist_Delete(string target)
        {
			int pos = 0;
			CDataCell dc = null;
			while (pos < myTaskItems.Count) // 리스트 제거
			{
				dc = myTaskItems[pos];
				if (dc.DC_listName == target)
				{
					myTaskItems.RemoveAt(pos);
				}
				else
				{
					++pos;
				}
			}
			Update_View.Invoke(this, new ModelEventArgs((CDataCell)dc.Clone(), WParam.WM_MENULIST_DELETE));
		}

		public void Transfer_Task(CDataCell dc, string target)
        {
			Find(dc).DC_listName = target;
			dc.DC_listName = target;
			Update_View.Invoke(this, new ModelEventArgs((CDataCell)dc.Clone(), WParam.WM_TRANSFER_TASK));
		}

		public void Add_Task(int id, string list, string title)
        {
			DateTime dt = DateTime.Now;

			CDataCell dc = new CDataCell(id, list, title);  // DataCell 생성
			myTaskItems.Insert(0, dc);
			myTaskItems[0].DC_dateCreated = dt;

			Update_View.Invoke(this, new ModelEventArgs((CDataCell)dc.Clone(), WParam.WM_TASK_ADD));  // deep copy 할 것!
		}

		public void Add_Task(CDataCell dc)
		{
			DateTime dt = DateTime.Now;

			myTaskItems.Insert(0, dc);
			myTaskItems[0].DC_dateCreated = dt;
			Update_View.Invoke(this, new ModelEventArgs((CDataCell)dc.Clone(), WParam.WM_TASK_ADD));  // deep copy 할 것!
		}

		public void Delete_Task(CDataCell dc)
        {
			Console.WriteLine("3>MainModel::Delete_Task : " + dc.DC_title);
			if (!myTaskItems.Remove(Find(dc)))
            {
				Console.WriteLine("3>MainModel::Delete_Task -> Data No matched!!");
            }

			Update_View.Invoke(this, new ModelEventArgs((CDataCell)dc.Clone(), WParam.WM_TASK_DELETE));
		}

		public void Complete_Process(CDataCell dc)
		{
			Console.WriteLine("3>MainModel::Complete_Process : " + dc.DC_complete);
			//Notify_Log_Message("3>MainModel::Complete_Process : " + dc.DC_complete);
			if (dc.DC_complete)  // 완료시 맨밑으로, 해제시는 맨위로 보내기
			{
				myTaskItems.Remove(Find(dc)); // 삭제
				myTaskItems.Insert(myTaskItems.Count, dc); // 맨밑에 삽입
			}
			else
			{
				myTaskItems.Remove(Find(dc)); // 삭제
				myTaskItems.Insert(0, dc); // 맨위에 삽입
			}

			Update_View.Invoke(this, new ModelEventArgs((CDataCell)dc.Clone(), WParam.WM_COMPLETE_PROCESS));
		}

		public void Important_Process(CDataCell dc)
		{
			Console.WriteLine("3>MainModel::Important_Process : " + dc.DC_important);
			//Notify_Log_Message("3>MainModel::Important_Process : " + dc.DC_important);
			CDataCell data = Find(dc);
			data.DC_important = dc.DC_important;
			if (dc.DC_important && !dc.DC_complete)  // 중요 & 미완료시 -> 맨위로 이동
			{
				myTaskItems.Remove(data); // 삭제
				myTaskItems.Insert(0, data); //삽입
            }

			Update_View.Invoke(this, new ModelEventArgs((CDataCell)dc.Clone(), WParam.WM_IMPORTANT_PROCESS));
		}

		public void Modify_Task_Title(CDataCell dc)
        {
			Console.WriteLine("3>MainModel::Modify_Task_Title : " + dc.DC_title);
			Find(dc).DC_title =  dc.DC_title;
			Update_View.Invoke(this, new ModelEventArgs((CDataCell)dc.Clone(), WParam.WM_MODIFY_TASK_TITLE));
		}

		public void Modify_Task_Memo(CDataCell dc)
		{
			Console.WriteLine("3>MainModel::Modify_Task_Memo : " + dc.DC_title);
			Find(dc).DC_memo = dc.DC_memo;
			Update_View.Invoke(this, new ModelEventArgs((CDataCell)dc.Clone(), WParam.WM_MODIFY_TASK_MEMO));
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

			Update_View.Invoke(this, new ModelEventArgs((CDataCell)dc.Clone(), WParam.WM_TASK_MOVE_UP));
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

			Update_View.Invoke(this, new ModelEventArgs((CDataCell)dc.Clone(), WParam.WM_TASK_MOVE_DOWN));
			return true;
		}

		public void Modifiy_MyToday(CDataCell dc)
        {
			CDataCell data = Find(dc);
			data.DC_myToday = dc.DC_myToday;
			data.DC_myTodayTime = dc.DC_myTodayTime;

			Update_View.Invoke(this, new ModelEventArgs((CDataCell)dc.Clone(), WParam.WM_MODIFY_MYTODAY));
		}

		public void Modifiy_Remind(CDataCell dc)
        {
			CDataCell data = Find(dc);
			data.DC_remindType = dc.DC_remindType;
			data.DC_remindTime = dc.DC_remindTime;

			Update_View.Invoke(this, new ModelEventArgs((CDataCell)dc.Clone(), WParam.WM_MODIFY_REMIND));
		}

		public void Modifiy_Planned(CDataCell dc)
		{
			Console.WriteLine("3>MainModel::Modifiy_Planned : type " + dc.DC_deadlineType);

			CDataCell data = Find(dc);
			data.DC_deadlineType = dc.DC_deadlineType;
			data.DC_deadlineTime = dc.DC_deadlineTime;

			Update_View.Invoke(this, new ModelEventArgs((CDataCell)dc.Clone(), WParam.WM_MODIFY_PLANNED));
		}

		public void Modifiy_Repeat(CDataCell dc)
		{
			CDataCell data = Find(dc);
			data.DC_repeatType = dc.DC_repeatType;
			data.DC_repeatTime = dc.DC_repeatTime;

			Update_View.Invoke(this, new ModelEventArgs((CDataCell)dc.Clone(), WParam.WM_MODIFY_REPEAT));
		}

		private CDataCell Find(CDataCell dc)
		{
			int task_ID = dc.DC_task_ID;
			IEnumerable<CDataCell> dataset = from CDataCell data in myTaskItems
											 where data.DC_task_ID == task_ID select data;
			if (dataset.Count() != 1)
			{
				Console.WriteLine("Error>MainModel::Find -> Not Found Item!!");  // 에러 출력
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
        }
	}
}

