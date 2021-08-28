// copyright honeysoft 20200924 v0.1
// 수정작업 2021.7.19

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WellaTodo
{
    static class WellaTodo
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Console.WriteLine(">WellaTodo::Start");
            MainFrame mainFrame = new MainFrame();
            MainModel mainModel = new MainModel();
            new MainController(mainFrame, mainModel);
            Console.WriteLine(">WellaTodo::Running");
            Application.Run(mainFrame);
        }
    }
}

/* 직렬화 예제
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SerializingCollection
{
    [Serializable]
    class NameCard
    {
        public NameCard(string Name, string Phone, int Age)
        {
            this.Name = Name;
            this.Phone = Phone;
            this.Age = Age;
        }

        public string Name;
        public string Phone;
        public int Age;
    }

    class Program
    {
        static void Main(string[] args)
        {
            Stream ws = new FileStream("a.dat", FileMode.Create);
            BinaryFormatter serializer = new BinaryFormatter();

            List<NameCard> list = new List<NameCard>();
            list.Add(new NameCard("나노콛", "010-1234-4567", 22));
            list.Add(new NameCard("박보영", "010-9876-6543", 20));
            list.Add(new NameCard("김요시", "010-2222-4444", 19));

            serializer.Serialize(ws, list);
            ws.Close();

            Stream rs = new FileStream("a.dat", FileMode.Open);
            BinaryFormatter deserializer = new BinaryFormatter();

            List<NameCard> list2;
            list2 = (List<NameCard>)deserializer.Deserialize(rs);
            rs.Close();

            foreach(NameCard nc in list2)
                Console.WriteLine("Name: {0}, Phone: {1}, Age: {2}",nc.Name, nc.Phone, nc.Age);
        }
    }
}
*/
