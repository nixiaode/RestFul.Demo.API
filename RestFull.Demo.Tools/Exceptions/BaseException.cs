using System;
using System.Collections.Generic;

namespace RestFull.Demo.Tools.Exceptions
{
    /// <summary>
    /// 基础异常类
    /// </summary>
    public class BaseException : ApplicationException
    {
        /// <summary>
        /// 自定义参数字段对象
        /// </summary>
        public Dictionary<string, string> Args { get; set; }
        /// <summary>
        /// 异常消息属性
        /// </summary>
        public override string Message { get; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message"></param>
        public BaseException(string message) : base(message) { Message = message; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public BaseException(string message, Dictionary<string, string> args = null) : base(message)
        {
            Message = message;
            Args = args;
        }
    }
}
