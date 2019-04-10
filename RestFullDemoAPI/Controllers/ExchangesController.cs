using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestFull.Demo.DataModel.Entities;
using RestFull.Demo.Tools.Global;
using RestFullDemoAPI.Responses;
using RestFullDemoAPI.Services.Exchange.Input;
using RestFullDemoAPI.Services.Exchange.Interface;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace RestFullDemoAPI.Controllers
{
    /// <summary>
    /// 交易所管理控制器
    /// </summary>
    [Produces("application/json")]
    [Route("platform/v1/[controller]")]
    public class ExchangesController : BaseController
    {
        private readonly IExchangeManageServiceAsync _exchangeAsync;
        public ExchangesController(IExchangeManageServiceAsync exchangeAsync, IConfiguration cfg) : base(cfg)
        {
            _exchangeAsync = exchangeAsync;
        }

        /// <summary>
        /// 根据条件查询所有交易所，Demo不实现分页及排序
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "交易所" })]
        [ProducesResponseType(typeof(PageGetSuccessResponse<List<ExchangeEntity>>), 200)]
        [ProducesResponseType(typeof(BaseResponse), 202)]
        [ProducesResponseType(typeof(BaseResponse), 400)]
        [ProducesResponseType(typeof(BaseResponse), 401)]
        [ProducesResponseType(typeof(BaseResponse), 403)]
        public async Task<IActionResult> QueryExchanges([FromQuery]ExchangeSearchEntity query, PageParams page, string sortBy)
        {
            return Ok(MessageTransform(await _exchangeAsync.GetByQueryAsync(query, page, SortTransform(sortBy))));
        }

        /// <summary>
        /// 不同路由获取交易所示例，获取已上线的交易所集合
        /// </summary>
        /// <param name="name"></param>
        /// <param name="isOnline"></param>
        /// <returns></returns>
        [HttpGet("OnlineExchanges")]
        [SwaggerOperation(Tags = new[] { "交易所" })]
        [ProducesResponseType(typeof(PageGetSuccessResponse<List<ExchangeEntity>>), 200)]
        [ProducesResponseType(typeof(BaseResponse), 202)]
        [ProducesResponseType(typeof(BaseResponse), 400)]
        [ProducesResponseType(typeof(BaseResponse), 401)]
        [ProducesResponseType(typeof(BaseResponse), 403)]
        public async Task<IActionResult> GetOnlineExchanges([FromQuery]string name)
        {
            return Ok(MessageTransform(await _exchangeAsync.GetByQueryAsync(new ExchangeSearchEntity() { Name = name, IsOnline = true})));
        }
        /// <summary>
        /// 根据ID查询交易所
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [SwaggerOperation(Tags = new[] { "交易所" })]
        [ProducesResponseType(typeof(GetSuccessResponse<ExchangeEntity>), 200)]
        [ProducesResponseType(typeof(BaseResponse), 202)]
        [ProducesResponseType(typeof(BaseResponse), 400)]
        [ProducesResponseType(typeof(BaseResponse), 401)]
        [ProducesResponseType(typeof(BaseResponse), 403)]
        public async Task<IActionResult> GetByID(int id)
        {
            return Ok(MessageTransform(new GetSuccessResponse<ExchangeEntity>(await _exchangeAsync.GetByIDAsync(id))));
        }

        /// <summary>
        /// 创建交易所
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "交易所" })]
        [ProducesResponseType(typeof(PostSuccessResponse<ExchangeEntity>), 201)]
        [ProducesResponseType(typeof(BaseResponse), 202)]
        [ProducesResponseType(typeof(BaseResponse), 400)]
        [ProducesResponseType(typeof(BaseResponse), 401)]
        [ProducesResponseType(typeof(BaseResponse), 403)]
        [ProducesResponseType(typeof(BaseResponse), 404)]
        public async Task<IActionResult> Post([FromBody]PostParams<ExchangeCreateEntity> entity)
        {
            return Created("", MessageTransform(new PostSuccessResponse<ExchangeEntity>(await _exchangeAsync.CreateExchangeAsync(entity.Body))));
        }

        /// <summary>
        /// 更新指定交易所，参数中故意把是否上线移除，用于演示
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [SwaggerOperation(Tags = new[] { "交易所" })]
        [ProducesResponseType(typeof(PutSuccessResponse), 201)]
        [ProducesResponseType(typeof(BaseResponse), 202)]
        [ProducesResponseType(typeof(BaseResponse), 400)]
        [ProducesResponseType(typeof(BaseResponse), 401)]
        [ProducesResponseType(typeof(BaseResponse), 403)]
        [ProducesResponseType(typeof(BaseResponse), 404)]
        public async Task<IActionResult> Put(int id,[FromBody]PostParams<ExchangeUpdateEntity> entity)
        {
            await _exchangeAsync.UpdateExchangeAsync(id, entity.Body);
            return Created("", MessageTransform(new PutSuccessResponse()));
        }

        /// <summary>
        /// 设置指定交易所是否上线
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        [SwaggerOperation(Tags = new[] { "交易所" })]
        [ProducesResponseType(typeof(PutSuccessResponse), 201)]
        [ProducesResponseType(typeof(BaseResponse), 202)]
        [ProducesResponseType(typeof(BaseResponse), 400)]
        [ProducesResponseType(typeof(BaseResponse), 401)]
        [ProducesResponseType(typeof(BaseResponse), 403)]
        [ProducesResponseType(typeof(BaseResponse), 404)]
        public async Task<IActionResult> Patch(int id, [FromBody]PostParams<ExchangePatchEntity> entity)
        {
            await _exchangeAsync.OnOffLineExchangeAsync(id, entity.Body.IsOnLine);
            return Created("", MessageTransform(new PutSuccessResponse()));
        }

        /// <summary>
        /// 删除指定ID的交易所
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [SwaggerOperation(Tags = new[] { "交易所" })]
        [ProducesResponseType(typeof(string), 204)]
        [ProducesResponseType(typeof(BaseResponse), 202)]
        [ProducesResponseType(typeof(BaseResponse), 400)]
        [ProducesResponseType(typeof(BaseResponse), 401)]
        [ProducesResponseType(typeof(BaseResponse), 403)]
        [ProducesResponseType(typeof(BaseResponse), 404)]
        public async Task<IActionResult> DeleteByID(int id)
        {
            await _exchangeAsync.DeleteExchangeAsync(id);
            return NoContent();
        }

        /// <summary>
        /// 批量删除指定ID集合交易所
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpDelete]
        [SwaggerOperation(Tags = new[] { "交易所" })]
        [ProducesResponseType(typeof(string), 204)]
        [ProducesResponseType(typeof(BaseResponse), 202)]
        [ProducesResponseType(typeof(BaseResponse), 400)]
        [ProducesResponseType(typeof(BaseResponse), 401)]
        [ProducesResponseType(typeof(BaseResponse), 403)]
        [ProducesResponseType(typeof(BaseResponse), 404)]
        public async Task<IActionResult> Delete([Required]List<int> ids)
        {
            await _exchangeAsync.DeleteExchangeAsync(ids);
            return NoContent();
        }
    }
}
