using RestFull.Demo.DataModel.Entities;
using RestFull.Demo.Tools.Global;
using RestFullDemoAPI.Responses;
using RestFullDemoAPI.Services.Exchange.Input;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestFullDemoAPI.Services.Exchange.Interface
{
    /// <summary>
    /// 交易所管理业务接口
    /// </summary>
    public interface IExchangeManageServiceAsync
    {
        /// <summary>
        /// 根据ID查询单个交易所
        /// </summary>
        /// <param name="id">数据ID</param>
        /// <returns></returns>
        Task<ExchangeEntity> GetByIDAsync(int id);
        /// <summary>
        /// 根据过滤条件查询交易所，支持分页、排序
        /// </summary>
        /// <param name="name">交易所名称</param>
        /// <param name="isOnline">是否已上线</param>
        /// <param name="page">分页对象</param>
        /// <param name="sort">排序对象集合</param>
        /// <returns></returns>
        Task<PageGetSuccessResponse<List<ExchangeEntity>>> GetByQueryAsync(ExchangeSearchEntity query, PageParams page = null, Dictionary<string, bool> sort = null);
        /// <summary>
        /// 创建新的交易所
        /// </summary>
        /// <param name="entity">新交易所对象</param>
        /// <returns></returns>
        Task<ExchangeEntity> CreateExchangeAsync(ExchangeCreateEntity entity);
        /// <summary>
        /// 更新交易所对象
        /// </summary>
        /// <param name="id">数据ID</param>
        /// <param name="entity">交易所对象</param>
        /// <returns></returns>
        Task<bool> UpdateExchangeAsync(int id, ExchangeUpdateEntity entity);
        /// <summary>
        /// 上线、下线交易所
        /// </summary>
        /// <param name="id">数据ID</param>
        /// <param name="flag">true表示上线，false表示下线</param>
        /// <returns></returns>
        Task<bool> OnOffLineExchangeAsync(int id, bool flag);
        /// <summary>
        /// 删除交易所
        /// </summary>
        /// <param name="id">数据ID</param>
        /// <returns></returns>
        Task<bool> DeleteExchangeAsync(int id);
        /// <summary>
        /// 批量删除交易所
        /// </summary>
        /// <param name="ids">数据ID集合</param>
        /// <returns></returns>
        Task<bool> DeleteExchangeAsync(List<int> ids);
    }
}
