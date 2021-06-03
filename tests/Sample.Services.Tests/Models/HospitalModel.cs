using Azure.Search.Documents.Indexes;
using System.Collections.Generic;
using System.Linq;

namespace Sample.Services.Tests.Models
{
    public class HospitalModel
    {
        [SimpleField(IsKey = true)]
        public string HospitalId { get; set; }

        [SearchableField(IsFilterable = true, IsSortable = true)]
        public string Name { get; set; }

        [SearchableField]
        public string Overview { get; set; }

        public IEnumerable<DoctorModel> Doctors { get; set; } = Enumerable.Empty<DoctorModel>();

        public IEnumerable<DealModel> Deals { get; set; } = Enumerable.Empty<DealModel>();

        public IEnumerable<DepartmentModel> Departments { get; set; } = Enumerable.Empty<DepartmentModel>();

        public IEnumerable<SpecialtyModel> Specialties { get; set; } = Enumerable.Empty<SpecialtyModel>();


        public class DoctorModel
        {
            public string DoctorId { get; set; }

            [SearchableField]
            public string FirstName { get; set; }

            [SearchableField]
            public string LastName { get; set; }

            [SearchableField]
            public string Overview { get; set; }

            public IEnumerable<SpecialtyModel> Specialties { get; set; } = Enumerable.Empty<SpecialtyModel>();

            public IEnumerable<DoctorLocalizedModel> Locales { get; set; } = Enumerable.Empty<DoctorLocalizedModel>();
        }

        public class DealModel
        {
            public string DealId { get; set; }

            [SearchableField]
            public string Name { get; set; }

            [SearchableField]
            public string Description { get; set; }

            public IEnumerable<ServiceModel> Services { get; set; } = Enumerable.Empty<ServiceModel>();
        }

        public class DepartmentModel
        {
            public string DepartmentId { get; set; }

            [SearchableField]
            public string Name { get; set; }

            public IEnumerable<DepartmenLocalizedModel> Locales { get; set; } = Enumerable.Empty<DepartmenLocalizedModel>();
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

        public class ServiceModel
        {
            public string ServiceId { get; set; }

            [SearchableField]
            public string Name { get; set; }

            public IEnumerable<ServiceLocalizedModel> Locales { get; set; } = Enumerable.Empty<ServiceLocalizedModel>();
        }
    }
}
