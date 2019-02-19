using AutoMapper;
using Moq;
using WeddingsPlanner.Business.Services;
using WeddingsPlanner.Core.Generators;
using WeddingsPlanner.Core.Services;

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
            _onboardingService = new OnboardingService(
                _mapperMock.Object,
                DbContextProvider.GetSqlServerDbContext(),
                _agenciesServiceMock.Object,
                _csvReportGenerator);
        }


    }
}