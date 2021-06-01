using Azure.Search.Documents.Indexes;
using System;

namespace Sample.Services.Models
{
    public class SpecialtyModel
    {
        public string SpecialtyId { get; set; }

        [SearchableField]
        public string Name { get; set; }

        [SearchableField]
        public string Content { get; set; }

        public DepartmentModel Department { get; set; }

        public SpecialtyLocalizedModel[] Locales { get; set; }

        public HospitalModel[] Hospitals { get; set; }
    }
}
