using Azure.Search.Documents.Indexes;
using System;

namespace Sample.Services.Models
{
    public class DoctorModel
    {
        [SimpleField(IsKey = true)]
        public Guid Id { get; set; }

        [SearchableField]
        public string FirstName { get; set; }

        [SearchableField]
        public string LastName { get; set; }

        [SearchableField]
        public string Fullname { get; set; }

        [SearchableField]
        public string Overview { get; set; }

        public SpecialtyModel[] Specialties { get; set; }
    }
}
