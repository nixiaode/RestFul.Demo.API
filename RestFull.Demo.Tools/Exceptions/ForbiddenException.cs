namespace RestFull.Demo.Tools.Exceptions
{
    /// <summary>
    /// 403异常，默认为Global.Forbidden，如需使用其他提示信息请使用带参构造函数
    /// </summary>
    public class ForbiddenException : BaseException
    {
        /// <summary>
        /// 无参构造函数
        /// </summary>
        public ForbiddenException() : base("Global.Forbidden") { }
        /// <summary>
        /// 带参构造函数
        /// </summary>
        /// <param name="msg"></param>
        public ForbiddenException(string msg) : base(msg) { }
    }
}
