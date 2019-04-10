using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;

namespace RestFullDemoAPI.Swagger
{
    public class HttpHeaderOperation : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
            {
                operation.Parameters = new List<IParameter>();
            }

            var actionAttrs = context.ApiDescription.ActionAttributes();

            var isAuthorized = actionAttrs.Any(a => a.GetType() == typeof(AuthorizeAttribute));

            if (isAuthorized == false) //提供action都没有权限特性标记，检查控制器有没有
            {
                var controllerAttrs = context.ApiDescription.ControllerAttributes();

                isAuthorized = controllerAttrs.Any(a => a.GetType() == typeof(AuthorizeAttribute));
            }

            var isAllowAnonymous = actionAttrs.Any(a => a.GetType() == typeof(AllowAnonymousAttribute));

            if (isAuthorized && isAllowAnonymous == false)
            {
                operation.Parameters.Add(new NonBodyParameter()
                {
                    Name = "Authorization",  //添加Authorization头部参数
                    In = "header",
                    Type = "string",
                    Required = true
                });
            }

            var files = context.ApiDescription.ActionDescriptor.Parameters.Where(n => n.ParameterType == typeof(IFormFile)).ToList();
            if (files.Count > 0)
            {
                operation.Parameters.Remove(operation.Parameters.FirstOrDefault(item => item.Name == "ContentType"));
                operation.Parameters.Remove(operation.Parameters.FirstOrDefault(item => item.Name == "ContentDisposition"));
                operation.Parameters.Remove(operation.Parameters.FirstOrDefault(item => item.Name == "Headers"));
                operation.Parameters.Remove(operation.Parameters.FirstOrDefault(item => item.Name == "Length"));
                operation.Parameters.Remove(operation.Parameters.FirstOrDefault(item => item.Name == "Name"));
                operation.Parameters.Remove(operation.Parameters.FirstOrDefault(item => item.Name == "FileName"));

                for (int i = 0; i < files.Count; i++)
                {
                    operation.Parameters.Add(new NonBodyParameter
                    {
                        Name = files[i].Name,
                        In = "formData",
                        Description = "Upload File",
                        Required = true,
                        Type = "file"
                    });
                }

                operation.Consumes.Add("multipart/form-data");
            }
        }
    }
}
