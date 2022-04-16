using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellaTodo
{
    [Serializable]
    public class CMemoCell : ICloneable
    {
        private int         _ID;
        private string      _title;
        private string      _memo;
        private DateTime    _dateCreated;

        private bool        _archive;
        private int         _memoTag;
        private string      _memoColor;

        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
        public int MC_ID { get => _ID; set => _ID = value; }
        public string MC_Title { get => _title; set => _title = value; }
        public string MC_Memo { get => _memo; set => _memo = value; }
        public bool MC_Archive { get => _archive; set => _archive = value; }
        public int MC_Tag { get => _memoTag; set => _memoTag = value; }
        public string MC_Color { get => _memoColor; set => _memoColor = value; }
        public DateTime MC_DateCreated { get => _dateCreated; set => _dateCreated = value; }

        // --------------------------------------------------
        // Constructor
        // --------------------------------------------------
        public CMemoCell()
        {
            _ID = 0;
            _title = "입력하세요";
            _memo = "메모추가";
            _dateCreated = DateTime.Now;
            _archive = false;
            _memoTag = 0;
            _memoColor = "";
        }

        public CMemoCell (int id, string title)
        {
            _ID = id;
            _title = title;
            _memo = "메모추가";
            _dateCreated = DateTime.Now;
            _archive = false;
            _memoTag = 0;
            _memoColor = "";
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
