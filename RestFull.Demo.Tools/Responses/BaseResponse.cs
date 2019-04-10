namespace RestFullDemoAPI.Responses
{
    /// <summary>
    /// 返回信息基类
    /// </summary>
    public class BaseResponse
    {
        /// <summary>
        /// 返回消息
        /// </summary>
        public string Message { get; set; } = "Global.OperateSuccess";
        public BaseResponse() { }
        public BaseResponse(string msg)
        {
            Message = msg;
        }
    }
}
