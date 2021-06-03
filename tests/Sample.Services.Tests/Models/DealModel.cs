using Azure.Search.Documents.Indexes;
using System.Collections.Generic;
using System.Linq;

namespace Sample.Services.Tests.Models
{
    public class DealModel
    {
        [SimpleField(IsKey = true)]
        public string DealId { get; set; }

        [SearchableField(IsFilterable = true, IsSortable = true)]
        public string Name { get; set; }

        [SearchableField]
        public string Description { get; set; }

        public HospitalModel Hospital { get; set; }

        public IEnumerable<ServiceModel> Services { get; set; } = Enumerable.Empty<ServiceModel>();

        public class HospitalModel
        {
            public string HospitalId { get; set; }

            [SearchableField(IsFilterable = true, IsSortable = true)]
            public string Name { get; set; }

            [SearchableField]
            public string Overview { get; set; }
        }

        public class ServiceModel
        {
            public string ServiceId { get; set; }

            [SearchableField]
            public string Name { get; set; }

            public IEnumerable<ServiceLocalizedModel> Locales { get; set; } = Enumerable.Empty<ServiceLocalizedModel>();
        }
    }
}
