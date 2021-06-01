using Azure.Search.Documents.Indexes;
using System;

namespace Sample.Services.Models
{
    public class DealModel
    {
        public string DealId { get; set; }

        [SearchableField]
        public string Name { get; set; }

        [SearchableField]
        public string Description { get; set; }

        public HospitalModel Hospital { get; set; }

        public ServiceModel[] Services { get; set; }
    }
}
