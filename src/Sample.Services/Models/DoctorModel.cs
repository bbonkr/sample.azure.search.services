using Azure.Search.Documents.Indexes;
using System;

namespace Sample.Services.Models
{
    public class DoctorModel
    {
        public string DoctorId { get; set; }

        [SearchableField]
        public string FirstName { get; set; }

        [SearchableField]
        public string LastName { get; set; }

        [SearchableField]
        public string Fullname { get; set; }

        [SearchableField]
        public string Overview { get; set; }

        public HospitalModel Hospital { get; set; }

        public SpecialtyModel[] Specialties { get; set; }

        public DoctorLocalizedModel[] Locales { get; set; }
    }
}
