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
        private string _title;
        private bool _complete;
        private bool _important;
        private string _memo;
        private DateTime _dateCreated;
        private bool _myToday;
        private DateTime _remindTime;
        private DateTime _deadlineTime;
        private int _repeatPeriod;

        public string DC_title { get => _title; set => _title = value; }
        public bool DC_complete { get => _complete; set => _complete = value; }
        public bool DC_important { get => _important; set => _important = value; }
        public string DC_memo { get => _memo; set => _memo = value; }
        public DateTime DC_dateCreated { get => _dateCreated; set => _dateCreated = value; }
        public bool DC_myToday { get => _myToday; set => _myToday = value; }
        public DateTime DC_deadlineTime { get => _deadlineTime; set => _deadlineTime = value; }
        public int DC_repeatPeriod { get => _repeatPeriod; set => _repeatPeriod = value; }
        public DateTime DC_remindTime { get => _remindTime; set => _remindTime = value; }


        public CDataCell()
        {
            _title = "입력하세요";
            _complete = false;
            _important = false;
            _memo = "메모추가";
            _dateCreated = DateTime.Now;
            _myToday = false;
            _remindTime = default(DateTime);   // 1/1/0001 12:00:00 AM.
            _deadlineTime = DateTime.MinValue; // 1/1/0001 12:00:00 AM.
            _repeatPeriod = 0;
        }

        public CDataCell(string title)
        {
            _title = title;
            _complete = false;
            _important = false;
            _memo = "메모추가";
            _dateCreated = DateTime.Now;
            _myToday = false;
            _remindTime = default(DateTime);   // 1/1/0001 12:00:00 AM.
            _deadlineTime = DateTime.MinValue; // 1/1/0001 12:00:00 AM.
            _repeatPeriod = 0;
        }

        public override String ToString()
        {
            return String.Format("Title[{0}]", _title);
        }
    }
}
