using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestFull.Demo.DataModel.Entities;
using RestFull.Demo.Tools.Global;
using RestFullDemoAPI.Responses;
using RestFullDemoAPI.Services.SymbolPair.Input;
using RestFullDemoAPI.Services.SymbolPair.Interface;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace RestFullDemoAPI.Controllers
{
    /// <summary>
    /// 交易对管理控制器
    /// </summary>
    [Produces("application/json")]
    //[Route("platform/v1/[controller]")]
    public class SymbolPairesController : BaseController
    {
        private readonly ISymbolPairManageServiceAsync _pairManageServiceAsync;
        public SymbolPairesController(ISymbolPairManageServiceAsync pairManageServiceAsync, IConfiguration cfg) : base(cfg)
        {
            _pairManageServiceAsync = pairManageServiceAsync;
        }

        /// <summary>
        /// 查询指定交易所下的交易对数据，Demo不实现分页及排序
        /// </summary>
        /// <param name="exchangeID">交易所ID</param>
        /// <param name="query">查询条件</param>
        /// <param name="page">分页条件</param>
        /// <param name="sortBy">排序条件</param>
        /// <returns></returns>
        [Route("platform/v1/Exchanges/{exchangeID}/SymbolPaires")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "交易对" })]
        [ProducesResponseType(typeof(PageGetSuccessResponse<List<SymbolPairEntity>>), 200)]
        [ProducesResponseType(typeof(BaseResponse), 202)]
        [ProducesResponseType(typeof(BaseResponse), 400)]
        [ProducesResponseType(typeof(BaseResponse), 401)]
        [ProducesResponseType(typeof(BaseResponse), 403)]
        public async Task<IActionResult> QueryByExchange(int exchangeID,[FromQuery]SymbolPairSearchEntity query, PageParams page, string sortBy)
        {
            return Ok(MessageTransform(await _pairManageServiceAsync.GetByQueryAsync(query, exchangeID, page, SortTransform(sortBy))));
        }

        /// <summary>
        /// 与交易所无关，根据条件查询交易对，Demo不实现分页及排序
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="page">分页条件</param>
        /// <param name="sortBy">排序条件</param>
        /// <returns></returns>
        [Route("platform/v1/SymbolPaires")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "交易对" })]
        [ProducesResponseType(typeof(PageGetSuccessResponse<List<SymbolPairEntity>>), 200)]
        [ProducesResponseType(typeof(BaseResponse), 202)]
        [ProducesResponseType(typeof(BaseResponse), 400)]
        [ProducesResponseType(typeof(BaseResponse), 401)]
        [ProducesResponseType(typeof(BaseResponse), 403)]
        public async Task<IActionResult> QuerySymbolPaires([FromQuery]SymbolPairSearchEntity query, PageParams page, string sortBy)
        {
            return Ok(MessageTransform(await _pairManageServiceAsync.GetByQueryAsync(query, null, page, SortTransform(sortBy))));
        }
        /// <summary>
        /// 根据ID查询交易对
        /// </summary>
        /// <param name="id">交易对ID</param>
        /// <returns></returns>
        [Route("platform/v1/SymbolPaires/{id}")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "交易对" })]
        [ProducesResponseType(typeof(GetSuccessResponse<SymbolPairEntity>), 200)]
        [ProducesResponseType(typeof(BaseResponse), 202)]
        [ProducesResponseType(typeof(BaseResponse), 400)]
        [ProducesResponseType(typeof(BaseResponse), 401)]
        [ProducesResponseType(typeof(BaseResponse), 403)]
        public async Task<IActionResult> GetByID(int id)
        {
            return Ok(MessageTransform(new GetSuccessResponse<SymbolPairEntity>(await _pairManageServiceAsync.GetByIDAsync(id))));
        }

        /// <summary>
        /// 为指定交易所创建交易对数据
        /// </summary>
        /// <param name="exchangeID">交易所ID</param>
        /// <param name="entity">交易对实体</param>
        /// <returns></returns>
        [Route("platform/v1/SymbolPaires")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "交易对" })]
        [ProducesResponseType(typeof(PostSuccessResponse<SymbolPairEntity>), 201)]
        [ProducesResponseType(typeof(BaseResponse), 202)]
        [ProducesResponseType(typeof(BaseResponse), 400)]
        [ProducesResponseType(typeof(BaseResponse), 401)]
        [ProducesResponseType(typeof(BaseResponse), 403)]
        [ProducesResponseType(typeof(BaseResponse), 404)]
        public async Task<IActionResult> Post(int exchangeID, [FromBody]SymbolPairCreateEntity entity)
        {
            return Created("", MessageTransform(new PostSuccessResponse<SymbolPairEntity>(await _pairManageServiceAsync.CreateSymbolPairAsync(exchangeID, entity))));
        }

        /// <summary>
        /// 更新指定交易对，参数中故意把是否启用移除，用于演示
        /// </summary>
        /// <param name="id">交易对ID</param>
        /// <param name="entity">更新实体</param>
        /// <returns></returns>
        [Route("platform/v1/SymbolPaires/{id}")]
        [HttpPut]
        [SwaggerOperation(Tags = new[] { "交易对" })]
        [ProducesResponseType(typeof(PutSuccessResponse), 201)]
        [ProducesResponseType(typeof(BaseResponse), 202)]
        [ProducesResponseType(typeof(BaseResponse), 400)]
        [ProducesResponseType(typeof(BaseResponse), 401)]
        [ProducesResponseType(typeof(BaseResponse), 403)]
        [ProducesResponseType(typeof(BaseResponse), 404)]
        public async Task<IActionResult> Put(int id, [FromBody]SymbolPairUpdateEntity entity)
        {
            await _pairManageServiceAsync.UpdateSymbolPairAsync(id, entity);
            return Created("", MessageTransform(new PutSuccessResponse()));
        }

        /// <summary>
        /// 设置指定交易对是否启用
        /// </summary>
        /// <param name="id">交易对ID</param>
        /// <param name="entity">更新实体</param>
        /// <returns></returns>
        [Route("platform/v1/SymbolPaires/{id}")]
        [HttpPatch]
        [SwaggerOperation(Tags = new[] { "交易对" })]
        [ProducesResponseType(typeof(PutSuccessResponse), 201)]
        [ProducesResponseType(typeof(BaseResponse), 202)]
        [ProducesResponseType(typeof(BaseResponse), 400)]
        [ProducesResponseType(typeof(BaseResponse), 401)]
        [ProducesResponseType(typeof(BaseResponse), 403)]
        [ProducesResponseType(typeof(BaseResponse), 404)]
        public async Task<IActionResult> Patch(int id, [FromBody]SymbolPairPatchEntity entity)
        {
            await _pairManageServiceAsync.OnOffSymbolPairAsync(id, entity.IsEnable);
            return Created("", MessageTransform(new PutSuccessResponse()));
        }

        /// <summary>
        /// 删除指定ID的交易对
        /// </summary>
        /// <param name="id">交易对ID</param>
        /// <returns></returns>
        [Route("platform/v1/SymbolPaires/{id}")]
        [HttpDelete]
        [SwaggerOperation(Tags = new[] { "交易对" })]
        [ProducesResponseType(typeof(string), 204)]
        [ProducesResponseType(typeof(BaseResponse), 202)]
        [ProducesResponseType(typeof(BaseResponse), 400)]
        [ProducesResponseType(typeof(BaseResponse), 401)]
        [ProducesResponseType(typeof(BaseResponse), 403)]
        [ProducesResponseType(typeof(BaseResponse), 404)]
        public async Task<IActionResult> DeleteByID(int id)
        {
            await _pairManageServiceAsync.DeleteSymbolPairAsync(id);
            return NoContent();
        }

        /// <summary>
        /// 批量删除指定ID集合交易对
        /// </summary>
        /// <param name="ids">交易对ID集合</param>
        /// <returns></returns>
        [Route("platform/v1/SymbolPaires")]
        [HttpDelete]        
        [SwaggerOperation(Tags = new[] { "交易对" })]
        [ProducesResponseType(typeof(string), 204)]
        [ProducesResponseType(typeof(BaseResponse), 202)]
        [ProducesResponseType(typeof(BaseResponse), 400)]
        [ProducesResponseType(typeof(BaseResponse), 401)]
        [ProducesResponseType(typeof(BaseResponse), 403)]
        [ProducesResponseType(typeof(BaseResponse), 404)]
        public async Task<IActionResult> Delete([Required]List<int> ids)
        {
            await _pairManageServiceAsync.DeleteSymbolPairAsync(ids);
            return NoContent();
        }
    }
}
