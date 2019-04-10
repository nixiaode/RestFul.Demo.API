namespace RestFullDemoAPI.Responses
{
    /// <summary>
    /// 更新返回信息，用于返回更新数据
    /// </summary>
    public class PutSuccessResponse : BaseResponse
    {
        /// <summary>
        /// 无参构造函数
        /// </summary>
        public PutSuccessResponse() { }
        /// <summary>
        /// 带参构造函数
        /// </summary>
        /// <param name="msg"></param>
        public PutSuccessResponse(string msg) : base(msg) { }
    }
}
