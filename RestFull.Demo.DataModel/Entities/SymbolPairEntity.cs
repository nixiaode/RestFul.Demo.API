namespace RestFull.Demo.DataModel.Entities
{
    /// <summary>
    /// 交易对实体
    /// </summary>
    public class SymbolPairEntity
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 所属交易所主键ID
        /// </summary>
        public int ExchangeID { get; set; }
        /// <summary>
        /// 基础币种
        /// </summary>
        public string SourceSymbol { get; set; }
        /// <summary>
        /// 计价币种
        /// </summary>
        public string TargetSymbol { get; set; }
        /// <summary>
        /// 统一交易对
        /// </summary>
        public string Pair { get; set; }
        /// <summary>
        /// 交易所交易对
        /// </summary>
        public string ExchangePair { get; set; }
        /// <summary>
        /// 启用状态
        /// </summary>
        public bool EnableState { get; set; }
    }
}
