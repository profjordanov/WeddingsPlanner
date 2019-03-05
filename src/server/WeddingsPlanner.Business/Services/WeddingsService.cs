using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Optional;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeddingsPlanner.Business.Services._Base;
using WeddingsPlanner.Core;
using WeddingsPlanner.Core.Models.Agencies;
using WeddingsPlanner.Core.Models.Weddings;
using WeddingsPlanner.Core.Services;
using WeddingsPlanner.Data.Entities;
using WeddingsPlanner.Data.EntityFramework;
using WeddingsPlanner.Data.Enums;

namespace WeddingsPlanner.Business.Services
{
    public class WeddingsService : BaseService, IWeddingsService
    {
        public WeddingsService(IMapper mapper, ApplicationDbContext dbContext) 
            : base(mapper, dbContext)
        {
        }

        public async Task<IEnumerable<WeddingsGuestListServiceModel>> GetAllWeddingsGuestListsAsync() =>
            await DbContext.Weddings
                .Include(wedding => wedding.Invitations)
                .ThenInclude(invitation => invitation.Guest)
                .Select(wedding => new WeddingsGuestListServiceModel
                {
                    Bride = wedding.Bride.FullName,
                    Bridegroom = wedding.Bridegroom.FullName,
                    Agency = new AgencyShortModel
                    {
                        Name = wedding.Agency.Name,
                        Town = wedding.Agency.Town
                    },
                    InvitedGuests = wedding.Invitations.Count,
                    BrideGuests = wedding.Invitations.Count(i => i.Family == Family.Bride),
                    AttendingGuests = wedding.Invitations.Count(i => i.IsAttending),
                    Guests = wedding.Invitations.Where(i => i.IsAttending).Select(i => i.Guest.FullName)
                })
                .OrderByDescending(model => model.InvitedGuests)
                .ThenBy(model => model.AttendingGuests)
                .ToListAsync();

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