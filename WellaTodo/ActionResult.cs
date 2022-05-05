namespace WellaTodo
{
    public enum ErrorCode
    {
        S_OK,
        E_FAIL,
        E_TEXT_IS_EMPTY,
        E_ERROR
    }

    public class ActionResult
    {
        private ErrorCode _errCode;
        private string _msg;

        public ErrorCode ErrCode { get => _errCode; set => _errCode = value; }
        public string Msg { get => _msg; set => _msg = value; }

        public ActionResult()
        {
            _errCode = ErrorCode.S_OK;
            _msg = null;
        }

        public ActionResult(ErrorCode errCode, string msg)
        {
            _errCode = errCode;
            _msg = msg;
        }

        public ActionResult(ErrorCode errNo) : this(errNo, null) { }

        public static ActionResult Ok() => new ActionResult();

        public static ActionResult Fail(string msg) => new ActionResult(ErrorCode.E_FAIL, msg);

        public static ActionResult Fail(ErrorCode errCode, string msg) => new ActionResult(errCode, msg);
    }
}
