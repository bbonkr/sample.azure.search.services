using System;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Sample.Services.Models;
using System.Threading.Tasks;

namespace Sample.Services.Tests
{
    public class SearchServiceTests : TestBase
    {
        [Fact]
        public async Task CreateIndexTestAsync()
        {
            using (var server = CreateServer())
            {
                var service = server.Host.Services.GetService<ISearchService>();

                await service.DeleteIndexIfExists("idx-hospitals");
                await service.CreateIndex<HospitalModel>("idx-hospitals", x => x.HospitalId);

                await service.DeleteIndexIfExists("idx-doctors");
                await service.CreateIndex<DoctorModel>("idx-doctors", x => x.DoctorId);

                await service.DeleteIndexIfExists("idx-deals");
                await service.CreateIndex<DealModel>("idx-deals", x => x.DealId);

                await service.DeleteIndexIfExists("idx-specialties");
                await service.CreateIndex<SpecialtyModel>("idx-specialties", x => x.SpecialtyId);

                await service.DeleteIndexIfExists("idx-departments");
                await service.CreateIndex<DepartmentModel>("idx-departments", x => x.DepartmentId);
            }
        }
    }
}
