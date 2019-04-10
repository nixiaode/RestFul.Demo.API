using RestFull.Demo.DataModel.Entities;
using RestFull.Demo.Tools.Global;
using RestFullDemoAPI.Responses;
using RestFullDemoAPI.Services.SymbolPair.Input;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestFullDemoAPI.Services.SymbolPair.Interface
{
    public interface ISymbolPairManageServiceAsync
    {
        /// <summary>
        /// 根据ID查询交易对
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<SymbolPairEntity> GetByIDAsync(int id);
        /// <summary>
        /// 根据过滤条件查询交易对，支持分页、排序
        /// </summary>
        /// <param name="query"></param>
        /// <param name="exchangeID"></param>
        /// <param name="page"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        Task<PageGetSuccessResponse<List<SymbolPairEntity>>> GetByQueryAsync(SymbolPairSearchEntity query, int? exchangeID = null, PageParams page = null, Dictionary<string, bool> sort = null);
        /// <summary>
        /// 为指定交易所创建交易对数据
        /// </summary>
        /// <param name="exchangeID"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<SymbolPairEntity> CreateSymbolPairAsync(int exchangeID, SymbolPairCreateEntity entity);
        /// <summary>
        /// 根据ID更新指定交易对数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> UpdateSymbolPairAsync(int id, SymbolPairUpdateEntity entity);
        /// <summary>
        /// 设置交易对启用/禁用
        /// </summary>
        /// <param name="id"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        Task<bool> OnOffSymbolPairAsync(int id, bool flag);
        /// <summary>
        /// 根据ID删除交易对
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteSymbolPairAsync(int id);
        /// <summary>
        /// 根据ID集合批量删除交易对
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<bool> DeleteSymbolPairAsync(List<int> ids);
    }
}
