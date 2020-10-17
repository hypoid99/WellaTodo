using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellaTodo
{
    public class CDataCell
    {
        private string _title;
        private string _complete;
        private string _important;
        private string _person;

        //private string _detailstep;

        //private string _myday;
        //private string _alarm;
        //private string _duerate;
        //private string _repeat;

        //private string _attachfile;
        //private string _memo;

        public string DC_complete {
            get { return _complete; }
            set { _complete = value; }
        }

        public string DC_title {
            get { return _title; }
            set { _title = value; }
        }

        public string DC_important {
            get { return _important; }
            set { _important = value; }
        }

        public string DC_person {
            get { return _person; }
            set { _person = value; }
        }

        public CDataCell()
        {
            _complete = "입력하세요";
            _title = "입력하세요";
            _important = "입력하세요";
            _person = "입력하세요";
        }

        public CDataCell(string complete, string title, string important, string person)
        {
            _complete = complete;
            _title = title;
            _important = important;
            _person = person;
        }

        public override String ToString()
        {
            return String.Format("Title[{0}] Person[{1}]", _title, _person);
        }
    }
}
