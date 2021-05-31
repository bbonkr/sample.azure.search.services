using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Models;

using Microsoft.Extensions.Options;

using Sample.Services.Models;

namespace Sample.Services
{
    public class SearchService : ISearchService
    {
        public SearchService(HttpClient client,IMapper mapper, IOptionsMonitor<AzureSearchServicesOptions> optionsMonitor)
        {
            this.mapper = mapper;
            options = optionsMonitor.CurrentValue ?? throw new ArgumentException("Azure Search Services Options did not have configured.");

            searchClient = CreateSearchClient(options);
            this.client = client;
        }

        public async Task<IEnumerable<HotelModel>> SearchByHotelNameAsync(string keyword = "" , CancellationToken cancellationToken = default)
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

            var response = await searchClient.SearchAsync<HotelModel>($"{keyword}*", options, cancellationToken);

            return response.Value.GetResults().Select(x => x.Document);
        }

        public async Task<IEnumerable<IndexingResult>> UploadAsync(IEnumerable<HotelModel> models, CancellationToken cancellationToken = default)
        {
            var options = new IndexDocumentsOptions { };
            var response = await searchClient.UploadDocumentsAsync(models, options, cancellationToken);

            return response.Value.Results;
        }

        public async Task MergeAsync(IEnumerable<HotelModel> models, CancellationToken cancellationToken = default)
        { 
            //var options = new IndexDocumentsOptions {  };
            //var response = await searchClient.MergeDocumentsAsync(models, options, cancellationToken);
            //return response.Value.Results;

            var body = models.Select(x => mapper.Map<HotelDocumentManagementModel>(x)).ToList();
            foreach (var item in body)
            {
                item.Action = DocumentAction.Merge.ToString();
            }

            var request = GetHttpRequestMessage(HttpMethod.Post, body);
            var response = await client.SendAsync(request, cancellationToken);            

            if(response.Content != null)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
            }

            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(IEnumerable<HotelModel> models, CancellationToken cancellationToken = default)
        {
            //var options = new IndexDocumentsOptions { };
            //var response = await searchClient.DeleteDocumentsAsync(models, options, cancellationToken);

            //return response.Value.Results;


            var body = models.Select(x => mapper.Map<HotelDocumentManagementModel>(x)).ToList();
            foreach (var item in body)
            {
                item.Action = DocumentAction.Delete.ToString();
            }

            var request = GetHttpRequestMessage(HttpMethod.Post, body);
            var response = await client.SendAsync(request, cancellationToken);

            if (response.Content != null)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
            }

            response.EnsureSuccessStatusCode();
        }

        private SearchClient CreateSearchClient(AzureSearchServicesOptions options)
        {
            var endPoint = new Uri($"https://{options.ServiceName}.search.windows.net/");
            var credential = new AzureKeyCredential(options.ApiKey);
            var adminClient = new SearchIndexClient(endPoint, credential);

            var client = new SearchClient(endPoint, options.IndexName, credential);

            return client;
        }

        private HttpRequestMessage GetHttpRequestMessage<T>(HttpMethod method, T body)
        {
            var uri = new Uri($"https://{options.ServiceName}.search.windows.net/indexes/{Uri.EscapeUriString(options.IndexName)}/docs/index?api-version=2020-06-30");
            var request = new HttpRequestMessage(method, uri);

            request.Headers.Add("api-key", options.ApiKey);

            var requestModel = new HotelDocumentManagementRequestModel<T>
            {
                Value = body,
            };

            var json = JsonSerializer.Serialize(requestModel, new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            });

            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            return request;
        }


        private readonly SearchClient searchClient;
        private readonly IMapper mapper;
        private readonly AzureSearchServicesOptions options;
        private readonly HttpClient client;

    }
}
