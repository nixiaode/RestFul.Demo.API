namespace RestFullDemoAPI.Responses
{
    /// <summary>
    /// Post返回信息对象，用于创建对象返回
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PostSuccessResponse<T> : GetSuccessResponse<T>
    {
        /// <summary>
        /// 无参构造函数
        /// </summary>
        public PostSuccessResponse() { }
        /// <summary>
        /// 带参构造函数
        /// </summary>
        /// <param name="msg"></param>
        public PostSuccessResponse(string msg) : base(msg) { }
        /// <summary>
        /// 带参构造函数
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="data"></param>
        public PostSuccessResponse(T data, string msg = "Global.OperateSuccess") : base(data, msg) { }
    }
}
