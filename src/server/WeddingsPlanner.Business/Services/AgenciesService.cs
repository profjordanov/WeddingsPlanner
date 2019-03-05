using AutoMapper;
using Optional;
using Optional.Async;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WeddingsPlanner.Business.Services._Base;
using WeddingsPlanner.Core;
using WeddingsPlanner.Core.Models.Agencies;
using WeddingsPlanner.Core.Services;
using WeddingsPlanner.Data.Entities;
using WeddingsPlanner.Data.EntityFramework;

using static System.Environment;

namespace WeddingsPlanner.Business.Services
{
    public class AgenciesService : BaseService, IAgenciesService
    {
        public AgenciesService(IMapper mapper, ApplicationDbContext dbContext) 
            : base(mapper, dbContext)
        {
        }

        public async Task<IEnumerable<AgencyServiceModel>> GetAgenciesOrderedAsync(
            CancellationToken cancellationToken) =>
            DbContext
                .Agencies
                .OrderByDescending(agency => agency.EmployeesCount)
                .ThenBy(agency => agency.Name)
                .Select(agency => new AgencyServiceModel
                {
                    Name = agency.Name,
                    Town = agency.Town,
                    EmployeesCount = agency.EmployeesCount
                })
                .AsParallel()
                .WithCancellation(cancellationToken);

        public Task<Option<Agency, Error>> AddAsync(Agency agency) =>
            ValidateInputModel(agency).FlatMapAsync(async agencyToMap =>
            {
                try
                {
                    var entry = await DbContext.Agencies.AddAsync(agencyToMap);
                    await DbContext.SaveChangesAsync();
                    return entry.Entity.Some<Agency, Error>();
                }
                catch (Exception e)
                {
                    var errMsg = $"An unhandled exception has occurred while creating the agency : {e.Message}!";
                    Debug.WriteLine($"{errMsg}{NewLine}" +
                                    $"{e.Data}{NewLine}" +
                                    $"{e.InnerException}{NewLine}" +
                                    $"{e.StackTrace}{NewLine}" );
                    return Option.None<Agency, Error>(new Error(errMsg));
                }
            });

        private Option<Agency, Error> ValidateInputModel(Agency agency) =>
            agency.Some<Agency, Error>()
                .FlatMap(ValidateName)
                .FlatMap(ValidateTown);

        private Option<Agency, Error> ValidateName(Agency agency) =>
            string.IsNullOrEmpty(agency.Name) ||
            string.IsNullOrWhiteSpace(agency.Name)
                ? Option.None<Agency, Error>(new Error("Agency name cannot be empty!")) 
                : agency.Some<Agency, Error>();

        private Option<Agency, Error> ValidateTown(Agency agency) =>
            string.IsNullOrEmpty(agency.Town) ||
            string.IsNullOrWhiteSpace(agency.Town)
                ? Option.None<Agency, Error>(new Error("Agency town cannot be empty!"))
                : agency.Some<Agency, Error>();
    }
}