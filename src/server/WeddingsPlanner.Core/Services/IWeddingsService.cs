using Optional;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeddingsPlanner.Core.Models.Weddings;
using WeddingsPlanner.Data.Entities;

namespace WeddingsPlanner.Core.Services
{
    public interface IWeddingsService
    {
        Task<IEnumerable<WeddingsGuestListServiceModel>> GetAllWeddingsGuestListsAsync();

        Task<Option<Wedding, Error>> AddAsync(Wedding wedding);
    }
}