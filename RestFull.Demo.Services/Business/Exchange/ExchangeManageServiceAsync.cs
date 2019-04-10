using Microsoft.Extensions.Caching.Memory;
using RestFull.Demo.DataModel.Entities;
using RestFull.Demo.Tools.Exceptions;
using RestFull.Demo.Tools.Global;
using RestFullDemoAPI.Responses;
using RestFullDemoAPI.Services.Demo;
using RestFullDemoAPI.Services.Exchange.Input;
using RestFullDemoAPI.Services.Exchange.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestFullDemoAPI.Services.Exchange
{
    /// <summary>
    /// 交易所管理业务实现
    /// </summary>
    public class ExchangeManageServiceAsync : IExchangeManageServiceAsync
    {
        private readonly IMemoryCache _memoryCache;
        private readonly List<ExchangeEntity> Exchanges;
        public ExchangeManageServiceAsync(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            Exchanges = _memoryCache.GetOrCreate("Data", (item) => { return DefaultData.Exchanges(); });
        }
        /// <summary>
        /// 根据ID查询单个交易所
        /// </summary>
        /// <param name="id">数据ID</param>
        /// <returns></returns>
        public async Task<ExchangeEntity> GetByIDAsync(int id)
        {
            try
            {
                var data = Exchanges.FirstOrDefault(item => item.ID == id);
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
        /// 根据过滤条件查询交易所，支持分页、排序
        /// </summary>
        /// <param name="name">交易所名称</param>
        /// <param name="isOnline">是否已上线</param>
        /// <param name="page">分页对象</param>
        /// <param name="sort">排序对象集合</param>
        /// <returns></returns>
        public async Task<PageGetSuccessResponse<List<ExchangeEntity>>> GetByQueryAsync(ExchangeSearchEntity query, PageParams page, Dictionary<string, bool> sort)
        {
            try
            {
                if (page != null)
                    page.InitParams();
                else
                    page = new PageParams() { PageIndex = 1, PageSize = 100 };

                PageGetSuccessResponse<List<ExchangeEntity>> result = new PageGetSuccessResponse<List<ExchangeEntity>>() { PageIndex = page.PageIndex.Value, PageSize = page.PageSize.Value };
                
                Console.WriteLine("简单起见，这里不实现任何分页以及排序功能，仅实现根据名称过滤数据功能");

                var data = Exchanges;
                if (query.IsOnline != null)
                    data = data.Where(item => item.IsOnline == query.IsOnline.Value).ToList();
                if(!string.IsNullOrEmpty(query.Name))
                    data = data.Where(item => item.FullName.ToLower().Contains(query.Name.Trim().ToLower()) || item.ShortName.ToLower().Contains(query.Name.Trim().ToLower())).ToList();
                if (data.Any())
                {
                    result.TotalCount = data.Count();
                    result.Result = data;                    
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
        /// 创建新的交易所
        /// </summary>
        /// <param name="entity">新交易所对象</param>
        /// <returns></returns>
        public async Task<ExchangeEntity> CreateExchangeAsync(ExchangeCreateEntity entity)
        {
            try
            {
                //检查全称与简称是否重复
                if (Exchanges.Any(item => item.FullName == entity.FullName.Trim() || item.ShortName == entity.ShortName.Trim()))
                    throw new BadRequestException("数据重复");
                ExchangeEntity data = new ExchangeEntity() {
                    ID = ++DefaultData.ExchangeMaxID,
                    FullName = entity.FullName,
                    ShortName = entity.ShortName,
                    IconPath = entity.IconPath,
                    IsOnline = entity.IsOnline,
                    WebSite = entity.WebSite,
                    Remarks = entity.Remarks,
                    SymbolPairs = new List<SymbolPairEntity>()
                };
                Exchanges.Add(data);
                _memoryCache.Set("Data", Exchanges);
                return await Task.FromResult(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        /// <summary>
        /// 更新交易所对象
        /// </summary>
        /// <param name="id">数据ID</param>
        /// <param name="entity">交易所对象</param>
        /// <returns></returns>
        public async Task<bool> UpdateExchangeAsync(int id, ExchangeUpdateEntity entity)
        {
            try
            {
                var data = await GetByIDAsync(id);
                if (Exchanges.Any(item => item.FullName == entity.FullName.Trim() || item.ShortName == entity.ShortName.Trim()))
                    throw new BadRequestException("数据重复");
                data.FullName = entity.FullName;
                data.ShortName = entity.ShortName;
                data.IconPath = entity.IconPath;
                data.WebSite = entity.WebSite;
                data.Remarks = entity.Remarks;
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
        /// 上线、下线交易所
        /// </summary>
        /// <param name="id">数据ID</param>
        /// <param name="flag">true表示上线，false表示下线</param>
        /// <returns></returns>
        public async Task<bool> OnOffLineExchangeAsync(int id, bool flag)
        {
            try
            {
                var data = await GetByIDAsync(id);
                if (data.IsOnline == flag)
                    throw new BadRequestException("没有任何变化");
                data.IsOnline = flag;
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
        /// 删除交易所
        /// </summary>
        /// <param name="id">数据ID</param>
        /// <returns></returns>
        public async Task<bool> DeleteExchangeAsync(int id)
        {
            try
            {
                return await DeleteExchangeAsync(new List<int> { id });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        /// <summary>
        /// 批量删除交易所
        /// </summary>
        /// <param name="ids">数据ID集合</param>
        /// <returns></returns>
        public async Task<bool> DeleteExchangeAsync(List<int> ids)
        {
            try
            {
                if(!Exchanges.Any(item => ids.Contains(item.ID)))
                    throw new NotFoundException();
                Exchanges.RemoveAll(item => ids.Contains(item.ID));
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
