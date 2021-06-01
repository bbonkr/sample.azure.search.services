using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Sample.Services
{
    public static class SevicesExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient();
            services.Configure<AzureSearchServicesOptions>(configuration.GetSection(AzureSearchServicesOptions.Name));
            services.AddTransient<ISearchService, SearchService>();
            services.AddTransient<ISearchIndexService, SearchIndexService>();
            
            services.AddAutoMapper(typeof(PlaceHolder).Assembly);

            return services;
        }
    }

    public class PlaceHolder { }
}
