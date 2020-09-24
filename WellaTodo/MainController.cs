using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellaTodo
{
	public class MainController : IController
	{
		IView m_View;
		IModel m_Model;

		public MainController(IView view, IModel model)
		{
			Console.WriteLine(">MainController Construction");
			m_View = view;
			m_Model = model;
			Console.WriteLine(">IView & IModel assigned to Controller");
		}

		public void IncValue()
        {

        }
	}
}


