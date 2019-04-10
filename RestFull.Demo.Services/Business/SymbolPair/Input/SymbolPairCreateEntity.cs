using System.ComponentModel.DataAnnotations;

namespace RestFullDemoAPI.Services.SymbolPair.Input
{
    /// <summary>
    /// 交易对创建对象
    /// </summary>
    public class SymbolPairCreateEntity : SymbolPairUpdateEntity
    {
        /// <summary>
        /// 是否启用
        /// </summary>
        [Required]
        public bool IsEnable { get; set; } = true;
    }
}
