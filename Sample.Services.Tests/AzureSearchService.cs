using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using Azure.Search.Documents.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sample.Services.Tests
{
    public class AzureSearchService
    {
        private readonly AzureSearchConfiguration _configuration;

        public AzureSearchService(AzureSearchConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task CreateIndex<TModel>(string indexName, CancellationToken cancellationToken = default)
        {
            var builder = new FieldBuilder();
            var searchFields = builder.Build(typeof(TModel));
            var definition = new SearchIndex(indexName, searchFields);
            var searchIndexClient = CreateSearchIndexClient();

            await searchIndexClient.CreateOrUpdateIndexAsync(definition, cancellationToken: cancellationToken);
        }

        public async Task DeleteIndexIfExists(string indexName, CancellationToken cancellationToken = default)
        {
            try
            {
                var searchIndexClient = CreateSearchIndexClient();
                if (searchIndexClient.GetIndex(indexName) != null)
                {
                    await searchIndexClient.DeleteIndexAsync(indexName, cancellationToken);
                }
            }
            catch (RequestFailedException e) when (e.Status == 404)
            {
            }
        }

        public async Task<IEnumerable<IndexingResult>> UploadDocumentsAsync<TModel>(string indexName, IEnumerable<TModel> models, CancellationToken cancellationToken = default)
        {
            var options = new IndexDocumentsOptions { };
            var searchClient = CreateSearchClient(indexName);
            var response = await searchClient.UploadDocumentsAsync(models, options, cancellationToken);

            return response.Value.Results;
        }

        public async Task<IEnumerable<IndexingResult>> MergeOrUploadDocumentsAsync<TModel>(string indexName, IEnumerable<TModel> models, CancellationToken cancellationToken = default)
        {
            var options = new IndexDocumentsOptions { };
            var searchClient = CreateSearchClient(indexName);
            var response = await searchClient.MergeOrUploadDocumentsAsync(models, options, cancellationToken);

            return response.Value.Results;
        }

        public async Task<IEnumerable<IndexingResult>> DeleteDocumentsAsync<TModel>(string indexName, IEnumerable<TModel> models, CancellationToken cancellationToken = default)
        {
            var options = new IndexDocumentsOptions { };
            var searchClient = CreateSearchClient(indexName);
            var response = await searchClient.DeleteDocumentsAsync(models, options, cancellationToken);

            return response.Value.Results;
        }

        public async Task<IEnumerable<TModel>> SearchDocumentsAsync<TModel>(string indexName, string keyword = "", string filter = "", CancellationToken cancellationToken = default)
        {
            var options = new SearchOptions
            {
                IncludeTotalCount = true,
                Filter = filter,
                OrderBy = { $"" },
            };

            if (string.IsNullOrWhiteSpace(keyword))
                keyword = "*";

            var searchClient = CreateSearchClient(indexName);
            var response = await searchClient.SearchAsync<TModel>($"{keyword}", options, cancellationToken);

            return response.Value.GetResults().Select(x => x.Document);
        }

        private SearchClient CreateSearchClient(string indexName)
        {
            var endPoint = new Uri($"https://{_configuration.ServiceName}.search.windows.net/");
            var credential = new AzureKeyCredential(_configuration.ApiKey);
            var client = new SearchClient(endPoint, indexName, credential);

            return client;
        }

        private SearchIndexClient CreateSearchIndexClient()
        {
            var endPoint = new Uri($"https://{_configuration.ServiceName}.search.windows.net/");
            var credential = new AzureKeyCredential(_configuration.ApiKey);
            var client = new SearchIndexClient(endPoint, credential);

            return client;
        }
    }
}
