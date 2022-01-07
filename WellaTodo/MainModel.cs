using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellaTodo
{
	public enum WParam
	{
		WM_COMPLETE_PROCESS = 1,
		WM_IMPORTANT_PROCESS = 2,
	}

	public class MainModel : IModel
	{
		public event ModelHandler<MainModel> Update_View;
		public event ModelHandler<MainModel> Update_Add_Task;
		public event ModelHandler<MainModel> Update_Delete_Task;

		List<CDataCell> myTaskItems = new List<CDataCell>();

		List<CDataCell> m_Task_Items = new List<CDataCell>();

		List<IModelObserver> ObserverList = new List<IModelObserver>();

		public List<CDataCell> Task_Item_Storage { get => m_Task_Items; set => m_Task_Items = value; }

        public MainModel()
		{
			Initialize();
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

		private void Initialize()
        {

		}

		public void Add_Task(string list, string title)
        {
			Console.WriteLine(">MainModel::Add_Task");
			DateTime dt = DateTime.Now;

			CDataCell dc = new CDataCell(list, title);  // DataCell 생성
			myTaskItems.Insert(0, dc);
			myTaskItems[0].DC_dateCreated = dt;

			Update_Add_Task.Invoke (this, new ModelEventArgs(dc));
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
			Console.WriteLine(">MainModel::Important_Process");


			Update_View.Invoke(this, new ModelEventArgs(dc, WParam.WM_IMPORTANT_PROCESS));
		}

		public void Complete_Process(CDataCell dc)
		{
			Console.WriteLine(">MainModel::Complete_Process");


			Update_View.Invoke(this, new ModelEventArgs(dc, WParam.WM_COMPLETE_PROCESS));
		}
	}
}

