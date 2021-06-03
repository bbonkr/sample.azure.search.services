using Microsoft.Extensions.Configuration;
using Sample.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Priority;

namespace Sample.Services.Tests
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class AzureSearchServiceTests
    {
        private readonly AzureSearchConfiguration _configuration;

        private const string HospitalIndexName = "test-hospitals";
        private const string DoctorIndexName = "test-doctors";
        private const string DealIndexName = "test-deals";
        private const string DepartmentIndexName = "test-departments";
        private const string SpecialtyIndexName = "test-specialties";

        public AzureSearchServiceTests()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .AddUserSecrets<TestBase>()
                .Build();

            _configuration = config.GetSection(nameof(AzureSearchConfiguration)).Get<AzureSearchConfiguration>();
        }

        [Fact, Priority(1)]
        public async Task CreateIndexTest()
        {
            var service = new AzureSearchService(_configuration);

            await service.DeleteIndexIfExists(HospitalIndexName);
            await service.CreateIndex<HospitalModel>(HospitalIndexName);

            await service.DeleteIndexIfExists(DoctorIndexName);
            await service.CreateIndex<DoctorModel>(DoctorIndexName);

            await service.DeleteIndexIfExists(DealIndexName);
            await service.CreateIndex<DealModel>(DealIndexName);

            await service.DeleteIndexIfExists(DepartmentIndexName);
            await service.CreateIndex<DepartmentModel>(DepartmentIndexName);

            await service.DeleteIndexIfExists(SpecialtyIndexName);
            await service.CreateIndex<SpecialtyModel>(SpecialtyIndexName);
        }

        [Fact, Priority(2)]
        public async Task UploadDocumentsTest()
        {
            var service = new AzureSearchService(_configuration);
            var models = CreateFakeHospitalModels();
            var result = await service.UploadDocumentsAsync(HospitalIndexName, models);

            Assert.NotEmpty(result);
        }

        [Fact, Priority(3)]
        public async Task WaitingForUploadTest()
        {
            var service = new AzureSearchService(_configuration);
            
            while(true)
            {
                Thread.Sleep(1000);
                var models = await service.SearchDocumentsAsync<HospitalModel>(HospitalIndexName);
                if (models.Any())
                    break;
            }
        }

        [Fact, Priority(4)]
        public async Task MergeOrUpdateDocumentsTest()
        {
            var service = new AzureSearchService(_configuration);
            var models = await service.SearchDocumentsAsync<HospitalModel>(HospitalIndexName); // all
            
            // modify first item
            var firstModel = models.First();
            firstModel.Name = $"{firstModel.Name} updated";
            firstModel.Deals = firstModel.Deals.ToList().Append(new HospitalModel.DealModel
            {
                DealId = Guid.NewGuid().ToString(),
                Name = "new-deal-name",
                Description = "new-deal-description"
            });

            var mergeModels = new List<HospitalModel> { firstModel };
            var result = await service.MergeOrUploadDocumentsAsync(HospitalIndexName, mergeModels);

            Assert.NotEmpty(result);
        }


        [Fact, Priority(5)]
        public async Task SearchDocumentsTest()
        {
            var service = new AzureSearchService(_configuration);
            var result = await service.SearchDocumentsAsync<HospitalModel>(HospitalIndexName, "new-deal-name");

            Assert.NotEmpty(result);
            Assert.True(result.Count() == 1);
        }

        [Fact, Priority(6)]
        public async Task DeleteDocumentsTest()
        {
            var service = new AzureSearchService(_configuration);
            var models = await service.SearchDocumentsAsync<HospitalModel>(HospitalIndexName); // all
            var result = await service.DeleteDocumentsAsync(HospitalIndexName, models);

            Assert.NotEmpty(result);
        }

        [Fact, Priority(7)]
        public async Task CleanupTest()
        {
            var service = new AzureSearchService(_configuration);

            await service.DeleteIndexIfExists(HospitalIndexName);
            await service.DeleteIndexIfExists(DoctorIndexName);
            await service.DeleteIndexIfExists(DealIndexName);
            await service.DeleteIndexIfExists(DepartmentIndexName);
            await service.DeleteIndexIfExists(SpecialtyIndexName);
        }

        private IEnumerable<HospitalModel> CreateFakeHospitalModels()
        {
            var hospitals = Enumerable.Range(0, 10).Select(_ => new HospitalModel
            {
                HospitalId = Guid.NewGuid().ToString(),
                Name = Faker.Company.Name(),
                Overview = Faker.Lorem.Paragraph(),
                Doctors = Enumerable.Range(0, 5).Select(_ => new HospitalModel.DoctorModel
                {
                    DoctorId = Guid.NewGuid().ToString(),
                    FirstName = Faker.Name.First(),
                    LastName = Faker.Name.Last(),
                    Overview = Faker.Lorem.Paragraph(),
                    Specialties = Enumerable.Range(0, 5).Select(_ => new HospitalModel.SpecialtyModel
                    {
                        SpecialtyId = Guid.NewGuid().ToString(),
                        Name = Faker.Name.FullName(),
                        Content = Faker.Lorem.Paragraph(),
                        Locales = Enumerable.Range(0, 5).Select(_ => new SpecialtyLocalizedModel
                        {
                            Name = Faker.Name.FullName(),
                            Content = Faker.Lorem.Paragraph()
                        })
                    })
                }),
                Deals = Enumerable.Range(0, 5).Select(_ => new HospitalModel.DealModel
                {
                    DealId = Guid.NewGuid().ToString(),
                    Name = Faker.Name.FullName(),
                    Description = Faker.Lorem.Paragraph(),
                    Services = Enumerable.Range(0, 5).Select(_ => new HospitalModel.ServiceModel
                    {
                        ServiceId = Guid.NewGuid().ToString(),
                        Name = Faker.Name.FullName(),
                        Locales = Enumerable.Range(0, 5).Select(_ => new ServiceLocalizedModel
                        {
                            Name = Faker.Name.FullName(),
                            Content = Faker.Lorem.Paragraph()
                        })
                    })
                }),
                Departments = Enumerable.Range(0, 5).Select(_ => new HospitalModel.DepartmentModel
                {
                    DepartmentId = Guid.NewGuid().ToString(),
                    Name = Faker.Name.FullName(),
                    Locales = Enumerable.Range(0, 5).Select(_ => new DepartmenLocalizedModel
                    {
                        Name = Faker.Name.FullName(),
                        Content = Faker.Lorem.Paragraph()
                    })
                }),
                Specialties = Enumerable.Range(0, 10).Select(_ => new HospitalModel.SpecialtyModel
                {
                    SpecialtyId = Guid.NewGuid().ToString(),
                    Name = Faker.Name.FullName(),
                    Content = Faker.Lorem.Paragraph(),
                    Locales = Enumerable.Range(0, 5).Select(_ => new SpecialtyLocalizedModel
                    {
                        Name = Faker.Name.FullName(),
                        Content = Faker.Lorem.Paragraph()
                    })
                })
            });

            return hospitals;
        }
    }
}
