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
    public class SearchIndexService
    {
        public SearchIndexService(HttpClient client, IMapper mapper, IOptionsMonitor<AzureSearchServicesOptions> optionsMonitor)
        {
            this.mapper = mapper;
            options = optionsMonitor.CurrentValue ?? throw new ArgumentException("Azure Search Services Options did not have configured.");

            searchIndexClient = CreateSearchIndexClient(options);
            this.client = client;
        }

       
        public async Task CreateIndex()
        {
            var fieldBuilder=new FieldBuilder();
            var searchFields = fieldBuilder.Build(typeof(HospitalModel));

            var suggesters = new List<SearchSuggester>{
                new SearchSuggester("sg", new[] {
                    nameof(HospitalModel.Name),
                    $"{nameof(HospitalModel.Specialties)}/{nameof(SpecialtyModel.Name)}",
                    $"{nameof(HospitalModel.Doctors)}/{nameof(DoctorModel.Fullname)}",
                }),
                new SearchSuggester("sg-hospital", new[]{ nameof(HospitalModel.Name)}),
                new SearchSuggester("sg-specialty", new[]{
                    $"{nameof(HospitalModel.Specialties)}/{nameof(SpecialtyModel.Name)}",
                }),
                new SearchSuggester("sg-doctor", new[]{
                    $"{nameof(HospitalModel.Doctors)}/{nameof(DoctorModel.Fullname)}",
                }),
            };

            var index = new SearchIndex("Hospitals")
            {
                Fields = searchFields,
            };

            foreach (var sg in suggesters)
            {
                index.Suggesters.Add(sg);
            }



            await searchIndexClient.CreateIndexAsync(index);
        }

        private SearchIndexClient CreateSearchIndexClient(AzureSearchServicesOptions options)
        {
            var endPoint = new Uri($"https://{options.ServiceName}.search.windows.net/");
            var credential = new AzureKeyCredential(options.ApiKey);
            var client = new SearchIndexClient(endPoint, credential);

            return client;
        }




        private readonly SearchClient searchClient;
        private readonly SearchIndexClient searchIndexClient;
        private readonly IMapper mapper;
        private readonly AzureSearchServicesOptions options;
        private readonly HttpClient client;
    }
}
