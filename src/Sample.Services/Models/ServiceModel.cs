using Azure.Search.Documents.Indexes;
using System;

namespace Sample.Services.Models
{
    public class ServiceModel
    {
        public string ServiceId { get; set; }

        [SearchableField]
        public string Name { get; set; }

        public ServiceLocalizedModel[] Locales { get; set; }
    }
}
