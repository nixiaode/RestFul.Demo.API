namespace RestFullDemoAPI.Responses
{
    /// <summary>
    /// 200返回信息对象，不分页
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GetSuccessResponse<T> : BaseResponse
    {
        /// <summary>
        /// 泛型结果对象
        /// </summary>
        public T Result { get; set; }
        /// <summary>
        /// 无参构造函数
        /// </summary>
        public GetSuccessResponse() { }
        /// <summary>
        /// 带参构造函数
        /// </summary>
        /// <param name="msg"></param>
        public GetSuccessResponse(string msg) : base(msg) { }
        /// <summary>
        /// 带参构造函数
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="data"></param>
        public GetSuccessResponse(T data, string msg = "Global.OperateSuccess") : base(msg)
        {
            Result = data;
        }
    }
}
