using Azure.Search.Documents.Indexes;

namespace Sample.Services.Models
{
    public class DoctorLocalizedModel
    {
        [SearchableField]
        public string Overview { get; set; }
    }
}
