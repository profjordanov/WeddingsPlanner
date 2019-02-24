using AutoMapper;
using Optional;
using Optional.Async;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using WeddingsPlanner.Business.Services._Base;
using WeddingsPlanner.Core;
using WeddingsPlanner.Core.Services;
using WeddingsPlanner.Data.Entities;
using WeddingsPlanner.Data.EntityFramework;

using static System.Environment;

namespace WeddingsPlanner.Business.Services
{
    public class VenuesService : BaseService, IVenuesService
    {
        public VenuesService(IMapper mapper, ApplicationDbContext dbContext) 
            : base(mapper, dbContext)
        {
        }

        public Task<Option<Venue, Error>> AddAsync(Venue venue) =>
            ValidateInputModel(venue).FlatMapAsync(async venueToMap =>
            {
                try
                {
                    var entry = await DbContext.Venues.AddAsync(venueToMap);
                    await DbContext.SaveChangesAsync();
                    return entry.Entity.Some<Venue, Error>();
                }
                catch (Exception e)
                {
                    var errMsg = $"An unhandled exception has occurred while creating the agency : {e.Message}!";
                    Debug.WriteLine($"{errMsg}{NewLine}" +
                                    $"{e.Data}{NewLine}" +
                                    $"{e.InnerException}{NewLine}" +
                                    $"{e.StackTrace}{NewLine}");
                    return Option.None<Venue, Error>(new Error(errMsg));
                }
            });

        private Option<Venue, Error> ValidateInputModel(Venue venue) =>
            venue.Some<Venue, Error>()
                .FlatMap(ValidateName);

        private Option<Venue, Error> ValidateName(Venue venue) =>
            string.IsNullOrEmpty(venue.Name) ||
            string.IsNullOrWhiteSpace(venue.Name)
                ? Option.None<Venue, Error>(new Error("Venue name cannot be empty!"))
                : venue.Some<Venue, Error>();
    }
}