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

        List<CDataCell> myData = new List<CDataCell>();

		public MainModel()
		{
			Initialize();
		}

		public void Attach_Model_Event(IModelObserver imo)
        {
			Changed_Model_Event += new ModelHandler<MainModel>(imo.Changed_Model_Event_method);
        }

		public void Update_Model()
		{
			Console.WriteLine(">MainModel::Invoke Changed_Model_Event");
			Changed_Model_Event.Invoke(this, new ModelEventArgs(1));
		}

		public List<CDataCell> GetDataCollection()
        {
			return myData;
        }

		public void Initialize()
        {
			Console.WriteLine("Initialize MainModel");
			myData.Add(new CDataCell("완료", "할일 1번째", "중요", "홍길동"));
			myData.Add(new CDataCell("완료", "할일 2번째", "중요", "홍길동"));
			myData.Add(new CDataCell("완료", "할일 3번째", "중요", "홍길동"));
			CDataCell newdata = new CDataCell("완료", "할일 4번째", "중요", "홍길동");
			myData.Insert(1, newdata);
			DisplayDataCell(myData);
		}

		public void DisplayDataCell(List<CDataCell> data_collection)
        {
			Console.WriteLine("---- start display datacell");
			foreach (CDataCell data in data_collection)
            {
				Console.WriteLine(data);
            }
			Console.WriteLine("------ end display datacell");
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

