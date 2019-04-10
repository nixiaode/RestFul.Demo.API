namespace RestFullDemoAPI.Services.Exchange.Input
{
    /// <summary>
    /// 交易所查询实体
    /// </summary>
    public class ExchangeSearchEntity
    {
        public string Name { get; set; }
        public bool? IsOnline { get; set; }
    }
}
