using System.Collections.Generic;

namespace RestFull.Demo.Tools.Exceptions
{
    /// <summary>
    /// 404数据未找到异常，默认为Global.NoFound，如需使用其他提示信息请使用带参构造函数
    /// </summary>
    public class NotFoundException : BaseException
    {
        /// <summary>
        /// 无参构造函数
        /// </summary>
        public NotFoundException() : base("Global.NoFound") { }
        /// <summary>
        /// 带参构造函数，自定义消息返回请传入第二个字典参数
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public NotFoundException(string msg, Dictionary<string, string> args = null) : base(msg, args) { }
    }
}
