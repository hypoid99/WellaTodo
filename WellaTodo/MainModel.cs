using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellaTodo
{
	public enum WParam
	{
		WM_COMPLETE_PROCESS,
		WM_IMPORTANT_PROCESS,
		WM_MODIFY_TASK_TITLE,
		WM_MODIFY_TASK_MEMO,
		WM_TASK_MOVE_UP,
		WM_TASK_MOVE_DOWN,
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
		public event ModelHandler<MainModel> Update_Add_Task;
		public event ModelHandler<MainModel> Update_Delete_Task;

		List<CDataCell> myTaskItems = new List<CDataCell>();
		List<IModelObserver> ObserverList = new List<IModelObserver>();

        public MainModel()
		{
		}

		public void Add_Observer(IModelObserver imo)
        {
			Update_View += new ModelHandler<MainModel>(imo.Update_View);
			Update_Add_Task += new ModelHandler<MainModel>(imo.Update_Add_Task);
			Update_Delete_Task += new ModelHandler<MainModel>(imo.Update_Delete_Task);

			ObserverList.Add(imo);
		}

		public void Remove_Observer(IModelObserver imo)
        {
			Update_View -= new ModelHandler<MainModel>(imo.Update_View);
			Update_Add_Task -= new ModelHandler<MainModel>(imo.Update_Add_Task);
			Update_Delete_Task -= new ModelHandler<MainModel>(imo.Update_Delete_Task);

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

		public void SetDataCollection (List<CDataCell> dataset)
        {
			myTaskItems = dataset;
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
			Update_View.Invoke(this, new ModelEventArgs(dc, WParam.WM_MENULIST_DELETE));
		}

		public void Transfer_Task(CDataCell dc, string target)
        {
			Find(dc).DC_listName = target;

			Update_View.Invoke(this, new ModelEventArgs(dc, WParam.WM_TRANSFER_TASK));
		}

		public void Add_Task(string list, string title)
        {
			Console.WriteLine(">MainModel::Add_Task");
			DateTime dt = DateTime.Now;

			CDataCell dc = new CDataCell(list, title);  // DataCell 생성
			myTaskItems.Insert(0, dc);
			myTaskItems[0].DC_dateCreated = dt;

			Update_Add_Task.Invoke(this, new ModelEventArgs(dc));
		}

		public void Add_Task(CDataCell dc)
		{
			Console.WriteLine(">MainModel::Add_Task");
			DateTime dt = DateTime.Now;

			myTaskItems.Insert(0, dc);
			myTaskItems[0].DC_dateCreated = dt;

			Update_Add_Task.Invoke(this, new ModelEventArgs(dc));
		}

		public void Delete_Task(CDataCell dc)
        {
			Console.WriteLine(">MainModel::Delete_Task");
			myTaskItems.Remove(dc);

			Update_Delete_Task.Invoke(this, new ModelEventArgs(dc));
		}

		public void Important_Process(CDataCell dc)
		{
			Console.WriteLine(">MainModel::Important_Process:"+dc.DC_important);

			if (dc.DC_important && !dc.DC_complete)  // 중요 & 미완료시 -> 맨위로 이동
			{
				myTaskItems.Remove(dc); // 삭제
				myTaskItems.Insert(0, dc); //삽입
			}

			Update_View.Invoke(this, new ModelEventArgs(dc, WParam.WM_IMPORTANT_PROCESS));
		}

		public void Complete_Process(CDataCell dc)
		{
			Console.WriteLine(">MainModel::Complete_Process:" + dc.DC_complete);

			if (dc.DC_complete)  // 완료시 맨밑으로, 해제시는 맨위로 보내기
			{
				myTaskItems.Remove(dc); // 삭제
				myTaskItems.Insert(myTaskItems.Count, dc); // 맨밑에 삽입
			}
			else
			{
				myTaskItems.Remove(dc); // 삭제
				myTaskItems.Insert(0, dc); // 맨위에 삽입
			}

			Update_View.Invoke(this, new ModelEventArgs(dc, WParam.WM_COMPLETE_PROCESS));
		}

		public void Modify_Task_Title(CDataCell dc)
        {
			Console.WriteLine("Befor title : " + Find(dc).DC_title);
			Console.WriteLine("After title : " + dc.DC_title);
			Find(dc).DC_title =  dc.DC_title;
			Update_View.Invoke(this, new ModelEventArgs(dc, WParam.WM_MODIFY_TASK_TITLE));
		}

		public void Modify_Task_Memo(CDataCell dc)
		{
			Find(dc).DC_memo = dc.DC_memo;

			Update_View.Invoke(this, new ModelEventArgs(dc, WParam.WM_MODIFY_TASK_MEMO));
		}

		public void Task_Move_Up(CDataCell dc)
		{
			CDataCell data = Find(dc);

			int pos = myTaskItems.IndexOf(data);
			if ( pos == 0) return;

			if (!myTaskItems[pos].DC_complete)
			{
				for (int i = pos - 1; i >= 0; i--)  // 상향 탐색
				{
					if (myTaskItems[i].DC_listName == myTaskItems[pos].DC_listName)
					{
						CDataCell temp = myTaskItems[pos]; //추출
						myTaskItems.RemoveAt(pos); //삭제
						myTaskItems.Insert(i, temp); // 삽입
						break;
					}
				}
			}
			else return;

			Update_View.Invoke(this, new ModelEventArgs(dc, WParam.WM_TASK_MOVE_UP));
		}

		public void Task_Move_Down(CDataCell dc)
		{
			CDataCell data = Find(dc);

			int pos = myTaskItems.IndexOf(data);

			if (pos == (myTaskItems.Count - 1)) return;

			if (!myTaskItems[pos].DC_complete)
			{
				for (int i = pos + 1; i <= myTaskItems.Count - 1; i++)
				{
					if (myTaskItems[i].DC_complete) return;
					if (myTaskItems[i].DC_listName == myTaskItems[pos].DC_listName)
					{
						CDataCell temp = myTaskItems[pos]; //추출
						myTaskItems.RemoveAt(pos); //삭제
						myTaskItems.Insert(i, temp); // 삽입
						break;
					}
				}
			}
			else return;

			Update_View.Invoke(this, new ModelEventArgs(dc, WParam.WM_TASK_MOVE_DOWN));
		}

		public void Modifiy_MyToday(CDataCell dc)
        {
			CDataCell data = Find(dc);
			data.DC_myToday = dc.DC_myToday;
			data.DC_myTodayTime = dc.DC_myTodayTime;

			Update_View.Invoke(this, new ModelEventArgs(dc, WParam.WM_MODIFY_MYTODAY));
		}

		public void Modifiy_Remind(CDataCell dc)
        {
			Update_View.Invoke(this, new ModelEventArgs(dc, WParam.WM_MODIFY_REMIND));
		}

		public void Modifiy_Planned(CDataCell dc)
		{
			Update_View.Invoke(this, new ModelEventArgs(dc, WParam.WM_MODIFY_PLANNED));
		}

		public void Modifiy_Repeat(CDataCell dc)
		{
			Update_View.Invoke(this, new ModelEventArgs(dc, WParam.WM_MODIFY_REPEAT));
		}

		private CDataCell Find(CDataCell dc)
		{
			IEnumerable<CDataCell> dataset = from CDataCell data in myTaskItems
											 where dc.Equals(data)
											 select data;
			if (dataset.Count() != 1) Console.WriteLine("Not Found Item!!");  // 에러 출력
			return dataset.First();
		}
	}
}

