﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeddingsPlanner.Business.Services;
using WeddingsPlanner.Business.Tests.Attributes;
using WeddingsPlanner.Data.Entities;
using Xunit;
using Xunit.Sdk;

namespace WeddingsPlanner.Business.Tests.Services
{
    /// <summary>
    /// SQL Server Database Integration Tests
    /// for <see cref="AgenciesService"/>. 
    /// </summary>
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
        public async Task AddAsync_Returns_None_When_Agency_Name_Is_Null(Agency agency)
        {
            // Arrange
            agency.Name = null;

            // Act
            var result = await _agenciesService.AddAsync(agency);

            // Assert
            result.HasValue.ShouldBe(false);
            result.MatchNone(error => error
                .Messages
                .ShouldAllBe(msg => msg == "Agency name cannot be empty!"));
        }

        [Theory]
        [CustomAutoData]
        public async Task AddAsync_Returns_None_When_Agency_Town_Is_Null(Agency agency)
        {
            // Arrange
            agency.Town = null;

            // Act
            var result = await _agenciesService.AddAsync(agency);

            // Assert
            result.HasValue.ShouldBe(false);
            result.MatchNone(error => error
                .Messages
                .ShouldAllBe(msg => msg == "Agency town cannot be empty!"));
        }

        [Theory]
        [CustomAutoData]
        public async Task AddAsync_Works_Correctly_For_Single_Record(Agency agency)
        {
            // Arrange
            agency.Id = 0;
            agency.OrganizedWeddings = new HashSet<Wedding>();

            // Act
            var result = await _agenciesService.AddAsync(agency);

            // Assert
            result.HasValue.ShouldBe(true);

            var lastAgency = await DbContextProvider
                .GetSqlServerDbContext()
                .Agencies
                .LastAsync();

            agency.Name.ShouldBe(lastAgency.Name);
            agency.Town.ShouldBe(lastAgency.Town);
            agency.EmployeesCount.ShouldBe(lastAgency.EmployeesCount);
        }

        [Theory]
        [CustomAutoData]
        public async Task AddAsync_Works_Correctly_For_Multiple_Records(Agency[] agencies)
        {
            foreach (var agency in agencies)
            {
                // Arrange
                agency.Id = 0;
                agency.OrganizedWeddings = new HashSet<Wedding>();

                // Act
                var result = await _agenciesService.AddAsync(agency);

                // Assert
                result.HasValue.ShouldBe(true);
            }

            var lastAgencies = DbContextProvider
                .GetSqlServerDbContext()
                .Agencies
                .OrderByDescending(agency => agency.Id)
                .Take(agencies.Length);

            lastAgencies.ShouldAllBe(agency => lastAgencies.Any(a => a.Name == agency.Name));
        }
    }
}