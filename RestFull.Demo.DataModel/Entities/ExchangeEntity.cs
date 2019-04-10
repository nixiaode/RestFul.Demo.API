using System.Collections.Generic;

namespace RestFull.Demo.DataModel.Entities
{
    /// <summary>
    /// 交易所实体
    /// </summary>
    public class ExchangeEntity
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 全称
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// 简称
        /// </summary>
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
        /// 是否上线
        /// </summary>
        public bool IsOnline { get; set; }
        /// <summary>
        /// 备注信息
        /// </summary>
        public string Remarks { get; set; }
        /// <summary>
        /// 交易对集合
        /// </summary>
        public ICollection<SymbolPairEntity> SymbolPairs { get; set; }
    }
}
