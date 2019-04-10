using Microsoft.AspNetCore.Mvc.Filters;
using RestFull.Demo.Tools.Exceptions;
using System.Linq;
using System.Text;

namespace RestFullDemoAPI.Extensions
{
    public class ControllerActionFilter : IActionFilter
    {
        /// <summary>
        /// 接口方法执行前进入此函数
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            //1、判断当前请求是否带有Token，如有Token则需要在redis中检查是否重复登录
            //if (context.HttpContext.Request.Headers.TryGetValue("Authorization", out Microsoft.Extensions.Primitives.StringValues value))
            //{
            //    if (value.FirstOrDefault() != null && value.FirstOrDefault().Length > 8)
            //    {
            //        //检查Token有效性
            //        if (!_redisCacheHelper.CheckToken(RedisPrefix.InvalidationToken, context.HttpContext.Request.Headers["Authorization"].ToString().Substring(7)))
            //        {
            //          //此处应抛出401异常
            //
            //        }
            //    }
            //}
            //2、判断当前请求是否通过模型校验
            if (!context.ModelState.IsValid)
            {
                StringBuilder sb = new StringBuilder();
                //获取所有的Error
                var errors = context.ModelState.Values.Where(item => item.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid).Select(item => item.Errors.ToList());
                //将系统提示错误组成一个字符串
                foreach (var items in errors)
                {
                    foreach (var er in items)
                    {
                        sb.Append($" {er.ErrorMessage} ");
                    }
                }
                //手动抛出400错误
                throw new BadRequestException(sb.ToString());
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            //Do Nothing
        }
    }
}
