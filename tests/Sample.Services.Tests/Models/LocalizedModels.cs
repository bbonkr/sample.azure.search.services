using Azure.Search.Documents.Indexes;

namespace Sample.Services.Tests.Models
{
    public class DoctorLocalizedModel
    {
        [SearchableField]
        public string Overview { get; set; }
    }

    public class DepartmenLocalizedModel
    {
        [SearchableField]
        public string Name { get; set; }

        [SearchableField]
        public string Description { get; set; }

        [SearchableField]
        public string Content { get; set; }
    }

    public class SpecialtyLocalizedModel
    {
        [SearchableField]
        public string Name { get; set; }

        [SearchableField]
        public string Content { get; set; }
    }

    public class ServiceLocalizedModel
    {
        [SearchableField]
        public string Name { get; set; }

        [SearchableField]
        public string Content { get; set; }
    }
}
