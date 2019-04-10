using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestFull.Demo.Tools.Exceptions;

namespace RestFullDemoAPI.Extensions
{
    /// <summary>
    /// 自定义异常过滤器
    /// </summary>
    public class ControllerActionExceptionFilter : IExceptionFilter
    {
        private readonly IConfiguration _configuration;
        public ControllerActionExceptionFilter(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        /// <summary>
        /// 自定义提示信息转换
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private string TransformExceptionMessage(BaseException ex, string msg)
        {
            if (!string.IsNullOrEmpty(msg))
            {
                //判断是否有待替换的参数字典
                if (ex.Args != null && ex.Args.Count > 0)
                {
                    string tempKey = "";
                    foreach (var kv in ex.Args)
                    {
                        //组装替换内容Key
                        tempKey = "{" + kv.Key + "}";
                        //判断在字符串中是否包含替换内容Key
                        if (msg.IndexOf(tempKey) >= 0)
                            msg = msg.Replace(tempKey, kv.Value);
                    }
                    return msg;
                }
            }
            return msg;
        }
        /// <summary>
        /// 异常捕获方法
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            //获取当前指定的语言
            string language = (context.HttpContext.Request.Headers["Accept-Language"]).ToString().IndexOf("en-US") >= 0 ? "en-US" : "zh-CN";
            ContentResult result = new ContentResult() { ContentType = "application/json; charset=utf-8" };
            //判断是否系统内部异常，设定所有System.开头的异常均为系统内部异常，所有未捕获（未处理）异常均为系统级异常
            if (context.Exception.GetType().ToString().StartsWith("System."))
            {
                context.HttpContext.Response.StatusCode = 500;
                result.Content = JsonConvert.SerializeObject(new { context.Exception.Message });
                //==================此处应有日志========================

            }
            else if (context.Exception.GetType() == typeof(UnauthorizedException))      //判断是否人工抛出401异常，只有在系统检查重复登录时可能抛出此异常
            {
                context.HttpContext.Response.StatusCode = 401;
                result.Content = JsonConvert.SerializeObject(new { Message = "IsOutLogin" });
            }
            else    //业务处理时抛出的异常
            {
                string msg = _configuration.GetSection($"{language}:{context.Exception.Message}").Value;
                if (string.IsNullOrEmpty(msg))
                    msg = context.Exception.Message;
                if (context.Exception.GetType() == typeof(ForbiddenException))  //禁止访问异常，通常在没有权限操作数据时抛出此异常
                {
                    context.HttpContext.Response.StatusCode = 403;
                    result.Content = JsonConvert.SerializeObject(new { Message = msg });
                }
                else
                {
                    //参数错误、业务错误、数据未找到错误
                    context.HttpContext.Response.StatusCode = context.Exception.GetType() == typeof(BadRequestException) ? 400 : 404;
                    result.Content = JsonConvert.SerializeObject(new { Message = TransformExceptionMessage((BaseException)context.Exception, msg) });
                }
            }
            context.Result = result;            //Result赋值后，后续流程将中断，直接返回客户端
            context.ExceptionHandled = true;    //表示异常已被处理，如不设置则会返回前端500错误。
        }
    }
}
