using Azure.Search.Documents.Indexes;
using System.Collections.Generic;
using System.Linq;

namespace Sample.Services.Models
{
    public class SpecialtyModel
    {
        [SimpleField(IsKey = true)]
        public string SpecialtyId { get; set; }

        [SearchableField(IsFilterable = true, IsSortable = true)]
        public string Name { get; set; }

        [SearchableField]
        public string Content { get; set; }

        public DepartmentModel Department { get; set; }

        public IEnumerable<SpecialtyLocalizedModel> Locales { get; set; } = Enumerable.Empty<SpecialtyLocalizedModel>();

        public IEnumerable<HospitalModel> Hospitals { get; set; } = Enumerable.Empty<HospitalModel>();

        public class DepartmentModel
        {
            public string DepartmentId { get; set; }

            [SearchableField(IsFilterable = true, IsSortable = true)]
            public string Name { get; set; }
        }

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
