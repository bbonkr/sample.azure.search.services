using Azure.Search.Documents.Indexes;

namespace Sample.Services.Models
{
    public class SpecialtyLocalizedModel
    {
        [SearchableField]
        public string Name { get; set; }

        [SearchableField]
        public string Description { get; set; }

        [SearchableField]
        public string Content { get; set; }
    }
}
