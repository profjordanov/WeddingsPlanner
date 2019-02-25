using Optional;
using System.Threading.Tasks;
using WeddingsPlanner.Data.Entities;

namespace WeddingsPlanner.Core.Services
{
    public interface IVenuesService
    {
        Task<Option<Venue, Error>> AddAsync(Venue venue);
    }
}