using Microsoft.Extensions.Caching.Memory;
using RestFull.Demo.DataModel.Entities;
using RestFull.Demo.Tools.Exceptions;
using RestFull.Demo.Tools.Global;
using RestFullDemoAPI.Responses;
using RestFullDemoAPI.Services.Demo;
using RestFullDemoAPI.Services.Exchange.Interface;
using RestFullDemoAPI.Services.SymbolPair.Input;
using RestFullDemoAPI.Services.SymbolPair.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestFullDemoAPI.Services.SymbolPair
{
    public class SymbolPairManageServiceAsync : ISymbolPairManageServiceAsync
    {
        private readonly IExchangeManageServiceAsync _exchangeManageService;
        private readonly IMemoryCache _memoryCache;
        private readonly List<ExchangeEntity> Exchanges;
        public SymbolPairManageServiceAsync(IMemoryCache memoryCache, IExchangeManageServiceAsync exchangeManageService)
        {
            _exchangeManageService = exchangeManageService;
            _memoryCache = memoryCache;
            Exchanges = _memoryCache.GetOrCreate("Data", (item) => { return DefaultData.Exchanges(); });
        }
        /// <summary>
        /// 根据ID查询交易对
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<SymbolPairEntity> GetByIDAsync(int id)
        {
            try
            {
                var records = Exchanges.SelectMany(item => item.SymbolPairs);
                var data = records.FirstOrDefault(item => item.ID == id);
                if (data == null)
                    throw new NotFoundException();
                return await Task.FromResult(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        /// <summary>
        /// 根据过滤条件查询交易对，支持分页、排序
        /// </summary>
        /// <param name="query"></param>
        /// <param name="exchangeID"></param>
        /// <param name="page"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public async Task<PageGetSuccessResponse<List<SymbolPairEntity>>> GetByQueryAsync(SymbolPairSearchEntity query, int? exchangeID = null, PageParams page = null, Dictionary<string, bool> sort = null)
        {
            try
            {
                if (page != null)
                    page.InitParams();
                else
                    page = new PageParams() { PageIndex = 1, PageSize = 100 };

                PageGetSuccessResponse<List<SymbolPairEntity>> result = new PageGetSuccessResponse<List<SymbolPairEntity>>() { PageIndex = page.PageIndex.Value, PageSize = page.PageSize.Value };

                var records = Exchanges.SelectMany(item => item.SymbolPairs);
                if (exchangeID != null)
                    records = records.Where(item => item.ExchangeID == exchangeID);
                if (query != null)
                {
                    if (query.IsEnabled != null)
                        records = records.Where(item => item.EnableState == query.IsEnabled.Value);
                    if (!string.IsNullOrEmpty(query.Name))
                        records = records.Where(item => item.Pair.ToLower().Contains(query.Name.Trim().ToLower()));
                }
                if (records.Any())
                {
                    result.TotalCount = records.Count();
                    result.Result = records.ToList();
                    return await Task.FromResult(result);
                }

                throw new NotFoundException();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        /// <summary>
        /// 为指定交易所创建交易对数据
        /// </summary>
        /// <param name="exchangeID"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<SymbolPairEntity> CreateSymbolPairAsync(int exchangeID, SymbolPairCreateEntity entity)
        {
            try
            {
                var exchange = await _exchangeManageService.GetByIDAsync(exchangeID);
                //检查交易对是否重复
                if (exchange.SymbolPairs.Any(item => item.SourceSymbol.ToLower() == entity.SourceSymbol.Trim().ToLower() && item.TargetSymbol.ToLower() == entity.TargetSymbol.Trim().ToLower()))
                    throw new BadRequestException("交易对数据重复");
                SymbolPairEntity data = new SymbolPairEntity()
                {
                    ID = ++DefaultData.PairMaxID,
                    SourceSymbol = entity.SourceSymbol.Trim().ToUpper(),
                    TargetSymbol = entity.TargetSymbol.Trim().ToUpper(),
                    EnableState = entity.IsEnable,
                    ExchangeID = exchangeID,
                    ExchangePair = entity.ExchangePair.Trim(),
                    Pair = $"{entity.SourceSymbol.Trim().ToUpper()}/{entity.TargetSymbol.Trim().ToUpper()}"
                };
                exchange.SymbolPairs.Add(data);
                _memoryCache.Set("Data", Exchanges);
                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        /// <summary>
        /// 根据ID更新指定交易对数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> UpdateSymbolPairAsync(int id, SymbolPairUpdateEntity entity)
        {
            try
            {
                //根据ID查询交易对数据
                var record = await GetByIDAsync(id);
                //检查新的交易对是否与当前交易所下的交易对重复
                var excahnge = Exchanges.Find(item => item.ID == record.ExchangeID);
                if (excahnge.SymbolPairs.Any(item => item.SourceSymbol.ToLower() == entity.SourceSymbol.Trim().ToLower() && item.TargetSymbol.ToLower() == entity.TargetSymbol.Trim().ToLower() && item.ID != id))
                    throw new BadRequestException("交易对数据重复");
                record.SourceSymbol = entity.SourceSymbol.Trim().ToUpper();
                record.TargetSymbol = entity.TargetSymbol.Trim().ToUpper();
                record.ExchangePair = entity.ExchangePair.Trim();
                record.Pair = $"{record.SourceSymbol}/{record.TargetSymbol}";
                _memoryCache.Set("Data", Exchanges);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        /// <summary>
        /// 设置交易对启用/禁用
        /// </summary>
        /// <param name="id"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public async Task<bool> OnOffSymbolPairAsync(int id, bool flag)
        {
            try
            {
                //根据ID查询交易对数据
                var record = await GetByIDAsync(id);
                if (record.EnableState == flag)
                    throw new BadRequestException("没有任何变化");
                record.EnableState = flag;
                _memoryCache.Set("Data", Exchanges);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        /// <summary>
        /// 根据ID删除交易对
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteSymbolPairAsync(int id)
        {
            try
            {
                return await DeleteSymbolPairAsync(new List<int> { id });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        /// <summary>
        /// 根据ID集合批量删除交易对
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<bool> DeleteSymbolPairAsync(List<int> ids)
        {
            try
            {
                var removeRecords = Exchanges.SelectMany(item => item.SymbolPairs).Where(item => ids.Contains(item.ID));
                if(!removeRecords.Any())
                    throw new NotFoundException();
                while (removeRecords.Count() > 0)
                {
                    Exchanges.Find(item => item.ID == removeRecords.First().ExchangeID).SymbolPairs.Remove(removeRecords.First());
                }
                _memoryCache.Set("Data", Exchanges);
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
