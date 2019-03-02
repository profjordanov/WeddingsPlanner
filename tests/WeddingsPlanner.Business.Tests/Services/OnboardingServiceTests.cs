using AutoMapper;
using Microsoft.AspNetCore.Http;
using Moq;
using Shouldly;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WeddingsPlanner.Business.Generators;
using WeddingsPlanner.Business.Identity;
using WeddingsPlanner.Business.Services;
using WeddingsPlanner.Core.Configuration;
using WeddingsPlanner.Core.Generators;
using WeddingsPlanner.Core.Mappings;
using WeddingsPlanner.Core.Services;
using WeddingsPlanner.Data.Entities;
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

        private readonly Mock<UserManager<User>> _userManagerMock;
        private readonly IMapper _mapper;

        private readonly ICsvReportGenerator _csvReportGenerator;
        private readonly IAgenciesService _agenciesService;
        private readonly IVenuesService _venuesService;
        private readonly IPeopleService _peopleService;
        private readonly IWeddingsService _weddingsService;
        private readonly IUsersService _usersService;

        private JwtConfiguration _jwtConfiguration;
        private readonly JwtFactory _jwtFactory;

        public OnboardingServiceTests()
        {
            _csvReportGenerator = new CsvReportGenerator();

            _agenciesService = new AgenciesService(
                CreateMapper(),
                DbContextProvider.GetSqlServerDbContext());

            _venuesService = new VenuesService(
                CreateMapper(),
                DbContextProvider.GetSqlServerDbContext());

            _userManagerMock = IdentityMocksProvider.GetMockUserManager();

            var fixture = new Fixture();

            var signingKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(fixture.Create<string>()));

            _jwtConfiguration = fixture
                .Build<JwtConfiguration>()
                .With(config => config.SigningCredentials, new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256))
                .Create();

            var jwtFactory = new JwtFactory(CreateJwtFactoryConfiguration());

            _usersService = new UsersService(
                _userManagerMock.Object,
                jwtFactory,
                CreateMapper());

            _peopleService = new PeopleService(
                CreateMapper(),
                DbContextProvider.GetSqlServerDbContext(),
                _usersService);

            _weddingsService = new WeddingsService(
                CreateMapper(),
                DbContextProvider.GetSqlServerDbContext());

            _onboardingService = new OnboardingService(
                CreateMapper(),
                DbContextProvider.GetSqlServerDbContext(),
                _agenciesService,
                _csvReportGenerator,
                _venuesService,
                _peopleService,
                _weddingsService);
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

        [Fact]
        public async Task PeopleByJson_Returns_Correct_Data()
        {
            // Arrange
            const string fileName = "people.json";
            const string resourceName = "WeddingsPlanner.Business.Tests.EmbeddedResource." + fileName;
            var iFormFile = MockIFormFileByEmbeddedResource(resourceName, fileName);

            // Act
            var result = await _onboardingService.PeopleByJson(iFormFile);

            // Asset
            result.HasValue.ShouldBe(true);
            result.MatchSome(report => report
                .AllRows
                .Skip(1)
                .Take(5)
                .ShouldAllBe(row => row.Contains("successfully added!")));
        }

        private IMapper CreateMapper()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new UsersMapping());
            });
            return mockMapper.CreateMapper();
        }

        private IOptions<JwtConfiguration> CreateJwtFactoryConfiguration()
        {
            var fixture = new Fixture();

            var signingKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(fixture.Create<string>()));

            _jwtConfiguration = fixture
                .Build<JwtConfiguration>()
                .With(config => config.SigningCredentials, new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256))
                .Create();

            return Options.Create(_jwtConfiguration);
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