using Azure.Search.Documents.Indexes;
using System;

namespace Sample.Services.Models
{
    public class DepartmentModel
    {
        [SimpleField(IsKey = true)]
        public Guid DepartmentId { get; set; }

        [SearchableField]
        public string Name { get; set; }

        public DepartmenLocalizedModel[] Locales { get; set; }
    }
}
