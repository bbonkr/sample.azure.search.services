using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;

using Microsoft.Extensions.Options;

using Sample.Services.Models;

namespace Sample.Services
{
    public class SearchIndexService : ISearchIndexService
    {
        public SearchIndexService(HttpClient client, IMapper mapper, IOptionsMonitor<AzureSearchServicesOptions> optionsMonitor)
        {
            this.mapper = mapper;
            options = optionsMonitor.CurrentValue ?? throw new ArgumentException("Azure Search Services Options did not have configured.");

            searchIndexClient = CreateSearchIndexClient(options);
            this.client = client;
        }


        public async Task<SearchIndex> CreateIndex()
        {
            var fieldBuilder = new FieldBuilder();
            var searchFields = fieldBuilder.Build(typeof(HospitalModel));

            var suggesters = new List<SearchSuggester>{
              
            };

            var index = new SearchIndex("hospitals")
            {
                Fields = searchFields,
            };

            foreach (var sg in suggesters)
            {
                index.Suggesters.Add(sg);
            }

            var response = await searchIndexClient.CreateIndexAsync(index);

            return response.Value;
        }

        private SearchIndexClient CreateSearchIndexClient(AzureSearchServicesOptions options)
        {
            var endPoint = new Uri($"https://{options.ServiceName}.search.windows.net/");
            var credential = new AzureKeyCredential(options.ApiKey);
            var client = new SearchIndexClient(endPoint, credential);

            return client;
        }




        private readonly SearchIndexClient searchIndexClient;
        private readonly IMapper mapper;
        private readonly AzureSearchServicesOptions options;
        private readonly HttpClient client;
    }
}
