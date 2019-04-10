using RestFull.Demo.DataModel.Entities;
using System.Collections.Generic;

namespace RestFullDemoAPI.Services.Demo
{
    /// <summary>
    /// 默认初始化数据
    /// </summary>
    public static class DefaultData
    {
        /// <summary>
        /// 当前最大交易所ID
        /// </summary>
        public static int ExchangeMaxID { get; set; } = 2;
        /// <summary>
        /// 当前最大交易对ID
        /// </summary>
        public static int PairMaxID { get; set; } = 2;
        /// <summary>
        /// 获得初始化数据集合
        /// </summary>
        /// <returns></returns>
        public static List<ExchangeEntity> Exchanges()
        {
            List<ExchangeEntity> result = new List<ExchangeEntity>();
            ExchangeEntity temp = new ExchangeEntity() {
                ID = 1,
                FullName = "火币",
                ShortName = "HB",
                IsOnline = true,
                SymbolPairs = new List<SymbolPairEntity>()
            };
            temp.SymbolPairs.Add(new SymbolPairEntity() {
                ID = 1,
                ExchangeID = 1,
                SourceSymbol = "BTC",
                TargetSymbol = "ETH",
                Pair = "ETH/BTC",
                ExchangePair = "eth-btc",
                EnableState = true
            });
            temp.SymbolPairs.Add(new SymbolPairEntity()
            {
                ID = 2,
                ExchangeID = 1,
                SourceSymbol = "BTC",
                TargetSymbol = "XRP",
                Pair = "XRP/BTC",
                ExchangePair = "xrp-btc",
                EnableState = true
            });
            result.Add(temp);
            temp = new ExchangeEntity()
            {
                ID = 2,
                FullName = "币安",
                ShortName = "BN",
                IsOnline = false,
                SymbolPairs = new List<SymbolPairEntity>()
            };
            result.Add(temp);
            return result;
        }
    }
}
