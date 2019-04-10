using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace RestFullDemoAPI.Controllers
{
    public class BaseController : Controller
    {
        private readonly IConfiguration _configuration;
        public BaseController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        /// <summary>
        /// 排序字段转换，将URL中的排序字符串转换成业务类使用的排序字段字典对象，Key为字段属性名称，Value为升序（true）、降序（false）
        /// </summary>
        /// <param name="sortBy"></param>
        /// <returns></returns>
        protected Dictionary<string, bool> SortTransform(string sortBy)
        {
            //键：排序字段；值：true表示升序，false表示倒序
            Dictionary<string, bool> dic = null;
            if (!string.IsNullOrEmpty(sortBy))
            {
                dic = new Dictionary<string, bool>();
                //使用英文逗号分隔字符串
                var arr = sortBy.Split(',');
                string tempKey = "";
                foreach (var str in arr)
                {
                    //只有字符串以+、-符号开头的情况下才视为正确的排序字段参数
                    if (str.StartsWith('+') || str.StartsWith('-'))
                    {
                        //截断字符串，获取排序字段属性名称
                        tempKey = str.Substring(1, str.Length - 1);
                        //判断是否已添加过键值
                        if (!dic.ContainsKey(tempKey))
                        {
                            dic.Add(tempKey, str.StartsWith('+'));
                        }
                    }
                }
            }
            return dic;
        }
        /// <summary>
        /// 返回消息国际化处理，使用反射查询Message属性，并根据当前使用语言尝试翻译返回消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        protected object MessageTransform<T>(T data)
        {
            try
            {
                //获得返回对象的Message属性对象
                var property = data.GetType().GetProperty("Message");
                if (property != null)
                {
                    //记录原数据值
                    string source = (string)property.GetValue(data);
                    //判断是否有数据需要翻译
                    if (!string.IsNullOrEmpty(source))
                    {
                        //获取语言
                        string language = (HttpContext.Request.Headers["Accept-Language"]).ToString().IndexOf("en-US") >= 0 ? "en-US" : "zh-CN";
                        //根据配置文件内容，尝试翻译
                        string taget = _configuration.GetSection($"{language}:{source}").Value;
                        //判断是否翻译成功，如成功则赋值
                        if (!string.IsNullOrEmpty(taget))
                            property.SetValue(data, taget);
                    }
                }
                return data;
            }
            catch
            {
                //记录日志

                throw;
            }
            
        }
    }
}
