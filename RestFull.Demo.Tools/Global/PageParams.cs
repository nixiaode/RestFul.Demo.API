namespace RestFull.Demo.Tools.Global
{
    /// <summary>
    /// 分页参数
    /// </summary>
    public class PageParams
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int? PageIndex { get; set; }
        /// <summary>
        /// 每页条数
        /// </summary>
        public int? PageSize { get; set; }
        /// <summary>
        /// 初始化方法
        /// </summary>
        public void InitParams()
        {
            if (PageIndex == null || PageIndex <= 0)
                PageIndex = 1;
            if (PageSize == null || PageSize <= 0)
                PageSize = 10;
        }
    }
}
