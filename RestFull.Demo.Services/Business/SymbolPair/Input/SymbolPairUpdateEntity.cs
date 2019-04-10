using System.ComponentModel.DataAnnotations;

namespace RestFullDemoAPI.Services.SymbolPair.Input
{
    /// <summary>
    /// 交易对更新实体对象
    /// </summary>
    public class SymbolPairUpdateEntity
    {
        /// <summary>
        /// 基础币种名称
        /// </summary>
        [Required]
        public string SourceSymbol { get; set; }
        /// <summary>
        /// 计价币种名称
        /// </summary>
        [Required]
        public string TargetSymbol { get; set; }
        /// <summary>
        /// 交易所使用交易对
        /// </summary>
        [Required]
        public string ExchangePair { get; set; }
    }
}
