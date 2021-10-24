using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellaTodo
{
	public class MainModel : IModel
	{
		public event ModelHandler<MainModel> Changed_Model_Event;

		List<List<CDataCell>> myDataBase = new List<List<CDataCell>>();
		List<CDataCell> myData = new List<CDataCell>();
		
		public MainModel()
		{
			Initialize();
		}

		public void Attach_Model_Event(IModelObserver imo)
        {
			Changed_Model_Event += new ModelHandler<MainModel>(imo.ModelObserver_Event_method);
        }
		
		public void Update_Model()
		{
			// Model 데이타를 변경한다
			Console.WriteLine(">MainModel::Invoke Changed_Model_Event");
			Changed_Model_Event.Invoke(this, new ModelEventArgs(1));
		}

		public List<CDataCell> GetDataCollection()
        {
			return myData;
        }

		private void Initialize()
        {

		}

		public void StoreData(int index)
        {
            if (myDataBase.Count <= index) return;

            myDataBase[index] = myData;
        }

		public List<CDataCell> RestoreData(int index)
        {
			return myDataBase[index];
        }


		private void DisplayDataCell(List<CDataCell> data_collection)
        {
			Console.WriteLine(">MainModel-- start display datacell");
			foreach (CDataCell data in data_collection)
            {
				Console.WriteLine(data);
            }
			Console.WriteLine(">MainModel---- end display datacell");
		}

		/*
		private string _name;
		public string Name
        {
			get { return _name; }
            set { _name = value; }
        }

		private string _id;
		public string ID
        {
			get => _id;
			set => _id = value;
        }

		public string Numbers { get; set; }
		*/
	}
}

