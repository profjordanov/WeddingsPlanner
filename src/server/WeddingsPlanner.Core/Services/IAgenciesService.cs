using Optional;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WeddingsPlanner.Core.Models.Agencies;
using WeddingsPlanner.Data.Entities;

namespace WeddingsPlanner.Core.Services
{
    public interface IAgenciesService
    {
        Task<Option<Agency, Error>> AddAsync(Agency agency);

        Task<IEnumerable<AgencyServiceModel>> GetAgenciesOrderedAsync(
            CancellationToken cancellationToken);

        Task<Option<Agency, Error>> GetSingleAsync(int id);

        Task<Option<Agency, Error>> GetFirstByNameAsync(string name);

        Task<Option<Agency, Error>> UpdateAsync(Agency agencyToUpdate);

        Task<Option<Agency, Error>> DeleteAsync(Agency agencyToDelete);
    }
}