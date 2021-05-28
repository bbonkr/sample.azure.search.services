using System;
using System.Collections.Generic;
using System.Threading;

using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Indexes;

using Microsoft.Extensions.Options;

using Sample.Services.Models;

namespace Sample.Services
{
    public class SearchService : ISearchService
    {
        public SearchService(IOptionsMonitor<AzureSearchServicesOptions> optionsMonitor)
        {
            options = optionsMonitor.CurrentValue ?? throw new ArgumentException("Azure Search Services Options did not have configured.");

            searchClient = CreateSearchClient(options);
        }

        public  IEnumerable<HotelModel> SearchByHotelName(string keyword = "")
        {
            var options = new SearchOptions
            {
                IncludeTotalCount = true,
                Filter = "",
                OrderBy = { $"" },
            };

            options.Select.Add(nameof(HotelModel.HotelId));
            options.Select.Add(nameof(HotelModel.HotelName));
            options.Select.Add($"{nameof(HotelModel.Address)}/{nameof(HotelModel.Address.City)}");

            var response = searchClient.Search<HotelModel>($"{keyword}*", options);
            foreach (var result in response.Value.GetResults())
            {
                yield return result.Document;
            }
        }

        private SearchClient CreateSearchClient(AzureSearchServicesOptions options)
        {
            var endPoint = new Uri($"https://{options.ServiceName}.search.windows.net/");
            var credential = new AzureKeyCredential(options.ApiKey);
            var adminClient = new SearchIndexClient(endPoint, credential);

            var client = new SearchClient(endPoint, options.IndexName, credential);

            return client;
        }


        private readonly SearchClient searchClient;
        private readonly AzureSearchServicesOptions options;

    }
}
