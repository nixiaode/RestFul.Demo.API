using System.ComponentModel.DataAnnotations;

namespace RestFullDemoAPI.Services.Exchange.Input
{
    /// <summary>
    /// 是否上线更新实体
    /// </summary>
    public class ExchangePatchEntity
    {
        /// <summary>
        /// 是否上线，默认等于false
        /// </summary>
        [Required]
        public bool IsOnLine { get; set; } = false;
    }
}
