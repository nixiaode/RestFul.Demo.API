using System.Collections.Generic;

namespace RestFull.Demo.Tools.Exceptions
{
    /// <summary>
    /// 400异常
    /// </summary>
    public class BadRequestException : BaseException
    {
        /// <summary>
        /// 构造函数，自定义消息返回请传入第二个字典参数
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public BadRequestException(string msg, Dictionary<string, string> args = null) : base(msg, args) { }
    }
}
