using AutoMapper;
using Microsoft.AspNetCore.Http;
using Moq;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using WeddingsPlanner.Business.Services;
using WeddingsPlanner.Core.Generators;
using WeddingsPlanner.Core.Services;
using Xunit;

namespace WeddingsPlanner.Business.Tests.Services
{
    public class OnboardingServiceTests
    {
        private readonly OnboardingService _onboardingService;

        private readonly ICsvReportGenerator _csvReportGenerator;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IAgenciesService> _agenciesServiceMock;
        private readonly Mock<IVenuesService> _venuesServiceMock;

        public OnboardingServiceTests()
        {
            _mapperMock = new Mock<IMapper>();
            _agenciesServiceMock = new Mock<IAgenciesService>();
            _venuesServiceMock = new Mock<IVenuesService>();

            _onboardingService = new OnboardingService(
                _mapperMock.Object,
                DbContextProvider.GetSqlServerDbContext(),
                _agenciesServiceMock.Object,
                _csvReportGenerator,
                _venuesServiceMock.Object);
        }

        [Fact]
        public async Task xx()
        {
            // Arrange
            const string resourceName = "WeddingsPlanner.Business.Tests.EmbeddedResource.agencies.json";
            const string fileName = "agencies.json";
            //var iFormFile = MockIFormFileByEmbeddedResource(resourceName, fileName);

            var file = new Mock<IFormFile>();
            var resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(resourceStream);
            writer.Flush();
            ms.Position = 0;
            file.Setup(f => f.FileName).Returns(fileName).Verifiable();
            file.Setup(_ => _.CopyToAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>()))
                .Returns((Stream stream, CancellationToken token) => ms.CopyToAsync(stream))
                .Verifiable();
            file.Setup(formFile => formFile.OpenReadStream())
                .Returns(resourceStream)
                .Verifiable();

            // Act
            var result = await _onboardingService.AgenciesByJson(file.Object);

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