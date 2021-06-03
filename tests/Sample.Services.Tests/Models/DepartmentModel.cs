using Azure.Search.Documents.Indexes;
using System.Collections.Generic;
using System.Linq;

namespace Sample.Services.Tests.Models
{
    public class DepartmentModel
    {
        [SimpleField(IsKey = true)]
        public string DepartmentId { get; set; }

        [SearchableField(IsFilterable = true, IsSortable = true)]
        public string Name { get; set; }

        public IEnumerable<DepartmenLocalizedModel> Locales { get; set; } = Enumerable.Empty<DepartmenLocalizedModel>();

        public IEnumerable<HospitalModel> Hospitals { get; set; } = Enumerable.Empty<HospitalModel>();

        public class HospitalModel
        {
            public string HospitalId { get; set; }

            [SearchableField]
            public string Name { get; set; }

            [SearchableField]
            public string Overview { get; set; }
        }
    }
}
