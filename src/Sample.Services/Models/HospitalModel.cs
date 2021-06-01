using Azure.Search.Documents.Indexes;
using System;

namespace Sample.Services.Models
{
    public class HospitalModel
    {
        [SimpleField(IsKey = true)]
        public Guid HospitalId { get; set; }

        [SearchableField]
        public string Name { get; set; }

        [SearchableField]
        public string Overview { get; set; }

        public DoctorModel[] Doctors { get; set; }

        public DealModel[] Deals { get; set; }

        public DepartmentModel[] Departments { get; set; }

        public SpecialtyModel[] Specialties { get; set; }
    }
}
