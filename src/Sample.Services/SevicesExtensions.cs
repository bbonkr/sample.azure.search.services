using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Sample.Services
{
    public static class SevicesExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AzureSearchServicesOptions>(configuration.GetSection(AzureSearchServicesOptions.Name));
            services.AddTransient<ISearchService, SearchService>();


            return services;
        }
    }
}
