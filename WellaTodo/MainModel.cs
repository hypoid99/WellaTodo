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
			Console.WriteLine(">MainModel Construction");

			myData.Add(new CDataCell(0, "할일 1번째"));
			myData.Add(new CDataCell(1, "할일 2번째"));
			myData.Add(new CDataCell(2, "할일 3번째"));
			CDataCell newdata = new CDataCell(3,"할일 4번째");
			myData.Insert(1, newdata);
			displayDataCell(myData);
		}

		public void Attach_Model_Event(IModelObserver imo)
        {
			Changed_Model_Event += new ModelHandler<MainModel>(imo.Changed_Model_Event_method);
        }

		public void Update_Model()
		{
			// Update_Model;
			Console.WriteLine(">MainModel::Invoke Changed_Model_Event");
			Changed_Model_Event.Invoke(this, new ModelEventArgs(1));
		}
		public void setValue(int value)
        {

        }

		private static void displayDataCell(List<CDataCell> data_collection)
        {
			foreach (CDataCell data in data_collection)
            {
				Console.WriteLine(data);
            }
			Console.WriteLine("--------End");
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

