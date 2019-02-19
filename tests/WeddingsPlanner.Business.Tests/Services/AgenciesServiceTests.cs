using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using Shouldly;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeddingsPlanner.Business.Services;
using WeddingsPlanner.Business.Tests.Attributes;
using WeddingsPlanner.Data.Entities;
using Xunit;

namespace WeddingsPlanner.Business.Tests.Services
{
    public class AgenciesServiceTests
    {
        private readonly AgenciesService _agenciesService;
        private readonly Mock<IMapper> _mapperMock;

        public AgenciesServiceTests()
        {
            _mapperMock = new Mock<IMapper>();
            _agenciesService = new AgenciesService(
                _mapperMock.Object,
                DbContextProvider.GetSqlServerDbContext());
        }

        [Theory]
        [CustomAutoData]
        public async Task AddAsync_Works_Correctly(Agency agency)
        {
            // Arrange
            agency.Id = 0;
            agency.OrganizedWeddings = new HashSet<Wedding>();

            // Act
            var result = await _agenciesService.AddAsync(agency);

            // Assert
            result.HasValue.ShouldBe(true);

            var lastAgency = await DbContextProvider.GetSqlServerDbContext().Agencies.LastAsync();
            agency.Name.ShouldBe(lastAgency.Name);
            agency.Town.ShouldBe(lastAgency.Town);
            agency.EmployeesCount.ShouldBe(lastAgency.EmployeesCount);
        }
    }
}