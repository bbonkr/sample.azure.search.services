using Azure.Search.Documents.Indexes;
using System;

namespace Sample.Services.Models
{
    public class ServiceModel
    {
        [SimpleField(IsKey = true)]
        public Guid Id { get; set; }

        [SearchableField]
        public string Name { get; set; }
    }
}
