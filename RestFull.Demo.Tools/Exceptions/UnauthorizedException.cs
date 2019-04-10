namespace RestFull.Demo.Tools.Exceptions
{
    /// <summary>
    /// 401异常，默认为Global.Unauthorized
    /// </summary>
    public class UnauthorizedException : BaseException
    {
        /// <summary>
        /// 无参构造函数
        /// </summary>
        public UnauthorizedException() : base("Global.Unauthorized") { }
        /// <summary>
        /// 带参构造函数
        /// </summary>
        /// <param name="msg"></param>
        public UnauthorizedException(string msg) : base(msg) { }
    }
}
