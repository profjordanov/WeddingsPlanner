using AutoMapper;
using Optional;
using System;
using System.Threading.Tasks;
using WeddingsPlanner.Business.Services._Base;
using WeddingsPlanner.Core;
using WeddingsPlanner.Core.Services;
using WeddingsPlanner.Data.Entities;
using WeddingsPlanner.Data.EntityFramework;

namespace WeddingsPlanner.Business.Services
{
    public class WeddingsService : BaseService, IWeddingsService
    {
        public WeddingsService(IMapper mapper, ApplicationDbContext dbContext) 
            : base(mapper, dbContext)
        {
        }

        public async Task<Option<Wedding, Error>> AddAsync(Wedding wedding)
        {
            try
            {
                var entry = await DbContext.Weddings.AddAsync(wedding);
                await DbContext.SaveChangesAsync();
                return entry.Entity.Some<Wedding, Error>();
            }
            catch (Exception ex)
            {
                return Option.None<Wedding, Error>(
                    new Error($"Database error has accrued: {ex.Message} ."));
            }
        }
    }
}