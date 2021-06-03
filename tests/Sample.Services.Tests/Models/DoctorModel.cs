using Azure.Search.Documents.Indexes;
using System.Collections.Generic;
using System.Linq;

namespace Sample.Services.Tests.Models
{
    public class DoctorModel
    {
        [SimpleField(IsKey = true)]
        public string DoctorId { get; set; }

        [SearchableField(IsFilterable = true, IsSortable = true)]
        public string FirstName { get; set; }

        [SearchableField(IsFilterable = true, IsSortable = true)]
        public string LastName { get; set; }

        [SearchableField]
        public string Overview { get; set; }

        public IEnumerable<HospitalModel> Hospitals { get; set; } = Enumerable.Empty<HospitalModel>();

        public IEnumerable<SpecialtyModel> Specialties { get; set; } = Enumerable.Empty<SpecialtyModel>();

        public IEnumerable<DoctorLocalizedModel> Locales { get; set; } = Enumerable.Empty<DoctorLocalizedModel>();

        public class HospitalModel
        {
            public string HospitalId { get; set; }

            [SearchableField]
            public string Name { get; set; }

            [SearchableField]
            public string Overview { get; set; }
        }

        public class SpecialtyModel
        {
            public string SpecialtyId { get; set; }

            [SearchableField]
            public string Name { get; set; }

            [SearchableField]
            public string Content { get; set; }

            public IEnumerable<SpecialtyLocalizedModel> Locales { get; set; } = Enumerable.Empty<SpecialtyLocalizedModel>();
        }
    }
}
