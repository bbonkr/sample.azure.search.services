using Azure.Search.Documents.Indexes;

namespace Sample.Services.Models
{
    public class ServiceLocalizedModel
    {
        [SearchableField]
        public string Name { get; set; }

        [SearchableField]
        public string Content { get; set; }
    }
}
