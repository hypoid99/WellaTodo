using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellaTodo
{
	public class MainModel : IModel
	{
		public event ModelHandler<MainModel> Model_Changed_Event;
		public event ModelHandler<MainModel> Update_View_Event;
		public event ModelHandler<MainModel> Update_Add_Task;

		List<CDataCell> myTaskItems = new List<CDataCell>();

		List<CDataCell> m_Task_Items = new List<CDataCell>();
        public List<CDataCell> Task_Item_Storage { get => m_Task_Items; set => m_Task_Items = value; }

        public MainModel()
		{
			Initialize();
		}

		public void Attach_Model_Event(IModelObserver imo)
        {
			Model_Changed_Event += new ModelHandler<MainModel>(imo.ModelObserver_Event_method);
			Update_View_Event += new ModelHandler<MainModel>(imo.Update_View_Event_method);
			Update_Add_Task += new ModelHandler<MainModel>(imo.Update_Add_Task);
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

			CDataCell dc1 = new CDataCell(list, title);  // DataCell 생성
			Task_Item_Storage.Insert(0, dc1);
			Task_Item_Storage[0].DC_dateCreated = dt;

			CDataCell dc = new CDataCell(list, title);  // DataCell 생성
			myTaskItems.Insert(0, dc);
			myTaskItems[0].DC_dateCreated = dt;

			Update_Add_Task.Invoke (this, new ModelEventArgs(dc));
		}

		private void Update_View()
        {
			Console.WriteLine(">MainModel::Update_View");
			Update_View_Event.Invoke(this, new ModelEventArgs(new CDataCell()));
		}
	}
}

