using Azure.Search.Documents.Indexes;
using System;

namespace Sample.Services.Models
{
    public class DepartmentModel
    {
        public string DepartmentId { get; set; }

        [SearchableField]
        public string Name { get; set; }

        public DepartmenLocalizedModel[] Locales { get; set; }

        public HospitalModel[] Hospitals { get; set; }
    }
}
