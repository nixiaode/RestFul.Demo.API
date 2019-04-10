using System.ComponentModel.DataAnnotations;

namespace RestFullDemoAPI.Services.Exchange.Input
{
    /// <summary>
    /// 交易所更新参数实体（故意不能修改上下线属性）
    /// </summary>
    public class ExchangeUpdateEntity
    {
        /// <summary>
        /// 全称
        /// </summary>
        [Required]
        public string FullName { get; set; }
        /// <summary>
        /// 简称
        /// </summary>
        [Required]
        public string ShortName { get; set; }
        /// <summary>
        /// 图标地址
        /// </summary>
        public string IconPath { get; set; }
        /// <summary>
        /// 网站地址
        /// </summary>
        public string WebSite { get; set; }
        /// <summary>
        /// 备注信息
        /// </summary>
        public string Remarks { get; set; }
    }
}
