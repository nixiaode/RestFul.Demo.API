using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using RestFull.Demo.Services;
using RestFullDemoAPI.Extensions;
using Swashbuckle.AspNetCore.Swagger;

namespace RestFullDemoAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddMvc(options =>
            {
                options.Filters.Add<ControllerActionExceptionFilter>(); //添加自定义异常捕获过滤器
                options.Filters.Add<ControllerActionFilter>();          //添加自定义接口方法过滤器
            }).AddJsonOptions(options => {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();    //指定返回数据为大驼峰命名法，即首字母保持大写
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDemoBussinessServices();//注入业务类

            services.AddCors(options =>
            {
                // this defines a CORS policy called "default"
                options.AddPolicy("default", policy =>
                {
                    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });
            services.AddMvcCore()
                .AddCors(optoins =>
                {
                    optoins.AddPolicy(optoins.DefaultPolicyName, policy =>
                    {
                        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials();
                    });
                })
                .AddJsonFormatters();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "MatrixOne RestFull规范接口示例 Swagger接口文档",
                    Description = "MatrixOne RestFull规范接口示例 Swagger接口文档"
                });
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetEntryAssembly().GetName().Name}.xml"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/v1/swagger.json", "MatrixOne RestFull规范接口示例 On Asp.net Core 2.2.0");
            });
            app.UseAuthentication();
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials());
            app.UseMvc();
        }
    }
}
