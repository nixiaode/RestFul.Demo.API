using Microsoft.Extensions.DependencyInjection;
using RestFullDemoAPI.Services.Exchange;
using RestFullDemoAPI.Services.Exchange.Interface;
using RestFullDemoAPI.Services.SymbolPair;
using RestFullDemoAPI.Services.SymbolPair.Interface;

namespace RestFull.Demo.Services
{
    public static class ServicesInjection
    {
        /// <summary>
        /// 注入实现方法
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDemoBussinessServices(this IServiceCollection services)
        {
            services.AddScoped<IExchangeManageServiceAsync, ExchangeManageServiceAsync>();
            services.AddScoped<ISymbolPairManageServiceAsync, SymbolPairManageServiceAsync>();
            return services;
        }
    }
}
