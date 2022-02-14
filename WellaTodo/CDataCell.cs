using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellaTodo
{
    [Serializable]
    public class CDataCell : ICloneable 
    {
        private int _task_ID;
        private string _listName;
        private string _title;
        private bool _complete;
        private bool _important;
        private string _memo;
        private DateTime _dateCreated;
        private bool _myToday;
        private DateTime _myTodayTime;
        private int _remindType;
        private DateTime _remindTime;
        private int _deadlineType;
        private DateTime _deadlineTime;
        private int _repeatType;
        private DateTime _repeatTime;

        private string _memoRTF;
        private bool _bulletin;
        private bool _archive;
        private int _memoTag;
        private string _memoColor;

        public int DC_task_ID { get => _task_ID; set => _task_ID = value; }
        public string DC_listName { get => _listName; set => _listName = value; }
        public string DC_title { get => _title; set => _title = value; }
        public bool DC_complete { get => _complete; set => _complete = value; }
        public bool DC_important { get => _important; set => _important = value; }
        public string DC_memo { get => _memo; set => _memo = value; }
        public DateTime DC_dateCreated { get => _dateCreated; set => _dateCreated = value; }
        public bool DC_myToday { get => _myToday; set => _myToday = value; }
        public DateTime DC_myTodayTime { get => _myTodayTime; set => _myTodayTime = value; }
        public int DC_remindType { get => _remindType; set => _remindType = value; }
        public DateTime DC_remindTime { get => _remindTime; set => _remindTime = value; }
        public int DC_deadlineType { get => _deadlineType; set => _deadlineType = value; }
        public DateTime DC_deadlineTime { get => _deadlineTime; set => _deadlineTime = value; }
        public int DC_repeatType { get => _repeatType; set => _repeatType = value; }
        public DateTime DC_repeatTime { get => _repeatTime; set => _repeatTime = value; }

        public string DC_memoRTF { get => _memoRTF; set => _memoRTF = value; }
        public bool DC_bulletin { get => _bulletin; set => _bulletin = value; }
        public bool DC_archive { get => _archive; set => _archive = value; }
        public int DC_memoTag { get => _memoTag; set => _memoTag = value; }
        public string DC_memoColor { get => _memoColor; set => _memoColor = value; }


        public CDataCell()
        {
            _task_ID = 0;
            _listName = "작업";
            _title = "입력하세요";
            _complete = false;
            _important = false;
            _memo = "메모추가";
            _dateCreated = DateTime.Now;
            _myToday = false;
            _myTodayTime = default;
            _remindType = 0;
            _remindTime = default(DateTime);   // 1/1/0001 12:00:00 AM.
            _deadlineType = 0;
            _deadlineTime = DateTime.MinValue; // 1/1/0001 12:00:00 AM.
            _repeatType = 0;
            _repeatTime = default;

            _memoRTF = "";
            _bulletin = false;
            _archive = false;
            _memoTag = 0;
            _memoColor = "";
        }

        public CDataCell(int id, string list, string title)
        {
            _task_ID = id;
            _listName = list.Length == 0 ? "작업" : list;
            _title = title;
            _complete = false;
            _important = false;
            _memo = "메모추가";
            _dateCreated = DateTime.Now;
            _myToday = false;
            _myTodayTime = default;
            _remindType = 0;
            _remindTime = default(DateTime);   // 1/1/0001 12:00:00 AM.
            _deadlineType = 0;
            _deadlineTime = DateTime.MinValue; // 1/1/0001 12:00:00 AM.
            _repeatType = 0;
            _repeatTime = default;

            _memoRTF = "";
            _bulletin = false;
            _archive = false;
            _memoTag = 0;
            _memoColor = "";
        }

        public CDataCell ShallowCopy()
        {
            return (CDataCell)MemberwiseClone();
        }

        public CDataCell DeepCopy()
        {
            CDataCell deepCopy = new CDataCell();
            deepCopy.DC_listName = "작업";
            return deepCopy;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public override String ToString()
        {
            return String.Format("Title[{0}]", _title);
        }
    }
}
