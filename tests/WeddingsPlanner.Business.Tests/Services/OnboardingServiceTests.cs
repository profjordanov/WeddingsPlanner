using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Moq;
using WeddingsPlanner.Business.Services;
using WeddingsPlanner.Core.Generators;
using WeddingsPlanner.Core.Services;
using Xunit;

namespace WeddingsPlanner.Business.Tests.Services
{
    public class OnboardingServiceTests
    {
        private readonly OnboardingService _onboardingService;

        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IAgenciesService> _agenciesServiceMock;
        private readonly ICsvReportGenerator _csvReportGenerator;

        public OnboardingServiceTests()
        {
            _mapperMock = new Mock<IMapper>();
            _agenciesServiceMock = new Mock<IAgenciesService>();

            _onboardingService = new OnboardingService(
                _mapperMock.Object,
                DbContextProvider.GetSqlServerDbContext(),
                _agenciesServiceMock.Object,
                _csvReportGenerator);
        }

        [Fact]
        public async Task xx()
        {
            // Arrange
            const string resourceName = "WeddingsPlanner.Business.Tests.EmbeddedResource.agencies.json";
            const string fileName = "agencies.json";
            var iFormFile = MockIFormFileByEmbeddedResource(resourceName, fileName);

            // Act
            var result = _onboardingService.AgenciesByJson(iFormFile);

        }

        private IFormFile MockIFormFileByEmbeddedResource(string resourceName, string fileName)
        {
            var fileMock = new Mock<IFormFile>();

            var assembly = Assembly.GetExecutingAssembly();

            string result;
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (var reader = new StreamReader(stream))
                {
                    result = reader.ReadToEnd();
                }
            }

            using (var ms = new MemoryStream())
            {
                using (var writer = new StreamWriter(ms))
                {
                    writer.Write(result);
                }

                fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
                fileMock.Setup(_ => _.FileName).Returns(fileName);
                fileMock.Setup(_ => _.Length).Returns(ms.Length);
                return fileMock.Object;
            }
        }
    }
}