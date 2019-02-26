using AutoMapper;
using Microsoft.AspNetCore.Http;
using Moq;
using Shouldly;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using WeddingsPlanner.Business.Generators;
using WeddingsPlanner.Business.Services;
using WeddingsPlanner.Core.Generators;
using WeddingsPlanner.Core.Services;
using Xunit;

namespace WeddingsPlanner.Business.Tests.Services
{
    /// <summary>
    /// SQL Server Database Integration Tests
    /// for <see cref="OnboardingService"/>.
    /// </summary>
    /// <seealso cref="OnboardingController"/>
    public class OnboardingServiceTests
    {
        private readonly OnboardingService _onboardingService;

        private readonly Mock<IMapper> _mapperMock;

        private readonly ICsvReportGenerator _csvReportGenerator;
        private readonly IAgenciesService _agenciesService;
        private readonly IVenuesService _venuesService;

        public OnboardingServiceTests()
        {
            _mapperMock = new Mock<IMapper>();

            _csvReportGenerator = new CsvReportGenerator();

            _agenciesService = new AgenciesService(
                _mapperMock.Object,
                DbContextProvider.GetSqlServerDbContext());

            _venuesService = new VenuesService(
                _mapperMock.Object,
                DbContextProvider.GetSqlServerDbContext());

            _onboardingService = new OnboardingService(
                _mapperMock.Object,
                DbContextProvider.GetSqlServerDbContext(),
                _agenciesService,
                _csvReportGenerator,
                _venuesService);
        }

        [Fact]
        public async Task AgenciesByJson_Returns_Correct_Data()
        {
            // Arrange
            const string resourceName = "WeddingsPlanner.Business.Tests.EmbeddedResource.agencies.json";
            const string fileName = "agencies.json";
            var iFormFile = MockIFormFileByEmbeddedResource(resourceName, fileName);

            // Act
            var result = await _onboardingService.AgenciesByJson(iFormFile);

            // Asset
            result.HasValue.ShouldBe(true);
            result.MatchSome(report => report
                .AllRows
                .Skip(1)
                .ShouldAllBe(row => row.Contains("successfully added!")));
        }

        [Fact]
        public async Task AgenciesByJson_Returns_Error_For_Invalid_Json_File()
        {
            // Arrange
            const string fileName = "invalid_agencies.json";
            const string resourceName = "WeddingsPlanner.Business.Tests.EmbeddedResource." + fileName;
            var iFormFile = MockIFormFileByEmbeddedResource(resourceName, fileName);

            // Act
            var result = await _onboardingService.AgenciesByJson(iFormFile);

            // Asset
            result.HasValue.ShouldBe(false);
            result.MatchNone(error => error
                .Messages
                .ShouldAllBe(msg => msg == "Something went wrong while deserializing the file! " +
                                    "Please, check for any mistakes."));
        }

        private static IFormFile MockIFormFileByEmbeddedResource(string resourceName, string fileName)
        {
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

            return file.Object;
        }
    }
}