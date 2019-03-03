using System.Collections.Generic;
using System.Threading;
using Optional;
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
    }
}