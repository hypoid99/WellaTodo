using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellaTodo
{
    class CDataCell
    {
        private int _id_no;
        private string _date;
        private string _title;
        private bool _complete;
        private bool _important;

        private string _detailstep;

        private string _myday;
        private string _alarm;
        private string _duerate;
        private string _repeat;

        private string _attachfile;
        private string _memo;

        public string P_date { get; set; }
        public string P_title { get; set; }

        public CDataCell()
        {
            _title = "입력하세요";
        }

        public CDataCell(int id_no, string title)
        {
            _id_no = id_no;
            _title = title;
        }

        public override String ToString()
        {
            return String.Format("ID[{0}] Title[{1}]", _id_no, _title);
        }
    }
}
