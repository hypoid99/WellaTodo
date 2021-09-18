using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellaTodo
{
    [Serializable]
    public class CDataCell
    {
        private int _idNum;
        private string _title;
        private bool _complete;
        private bool _important;
        private string _memo;

        //private string _person;
        //private string _detailstep;
        //private string _myday;
        //private string _alarm;
        //private string _duerate;
        //private string _repeat;

        //private string _attachfile;
        //private string _memo;

        public int DC_idNum {
            get { return _idNum; }
            set { _idNum = value; }
        }

        public bool DC_complete {
            get { return _complete; }
            set { _complete = value; }
        }

        public string DC_title {
            get { return _title; }
            set { _title = value; }
        }

        public bool DC_important {
            get { return _important; }
            set { _important = value; }
        }

        public string DC_memo
        {
            get { return _memo; }
            set { _memo = value; }
        }

        public CDataCell()
        {
            _idNum = 0;
            _complete = false;
            _title = "입력하세요";
            _important = false;
            _memo = "메모추가";
        }

        public CDataCell(int idnum, bool complete, string title, bool important, string memo)
        {
            _idNum = idnum;
            _complete = complete;
            _title = title;
            _important = important;
            _memo = memo;
        }

        public override String ToString()
        {
            return String.Format("Title[{0}]", _title);
        }
    }
}
