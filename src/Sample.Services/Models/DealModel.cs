using Azure.Search.Documents.Indexes;
using System;

namespace Sample.Services.Models
{
    public class DealModel
    {
        [SimpleField(IsKey = true)]
        public Guid Id { get; set; }

        [SearchableField]
        public string Name { get; set; }

        [SearchableField]
        public string Description { get; set; }

        public ServiceModel[] Services { get; set; }
    }
}
