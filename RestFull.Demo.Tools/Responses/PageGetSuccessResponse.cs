namespace RestFullDemoAPI.Responses
{
    /// <summary>
    /// 200返回信息对象，带分页参数
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageGetSuccessResponse<T> : GetSuccessResponse<T>
    {
        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 每页条数
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 数据总数，注意，不是当前页数据总条数，指查询结果数据总条数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 无参构造函数
        /// </summary>
        public PageGetSuccessResponse() { }
        /// <summary>
        /// 带参构造函数
        /// </summary>
        /// <param name="msg"></param>
        public PageGetSuccessResponse(string msg) : base(msg) { }
        /// <summary>
        /// 带参构造函数
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="data"></param>
        public PageGetSuccessResponse(T data, string msg = "Global.OperateSuccess") : base(data, msg) { }
    }
}
