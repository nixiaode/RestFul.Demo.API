namespace RestFullDemoAPI.Services.Exchange.Input
{
    /// <summary>
    /// 交易所新增参数实体
    /// </summary>
    public class ExchangeCreateEntity : ExchangeUpdateEntity
    {
        /// <summary>
        /// 是否上线
        /// </summary>
        public bool IsOnline { get; set; }
    }
}
