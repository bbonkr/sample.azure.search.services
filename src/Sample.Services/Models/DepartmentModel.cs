using Azure.Search.Documents.Indexes;
using System;

namespace Sample.Services.Models
{
    public class DepartmentModel
    {
        [SimpleField(IsKey = true)]
        public Guid Id { get; set; }

        [SearchableField]
        public string Name { get; set; }
    }
}
