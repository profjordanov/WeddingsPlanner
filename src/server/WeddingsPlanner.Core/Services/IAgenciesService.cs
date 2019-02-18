using Optional;
using System.Threading.Tasks;
using WeddingsPlanner.Data.Entities;

namespace WeddingsPlanner.Core.Services
{
    public interface IAgenciesService
    {
        Task<Option<Agency, Error>> AddAsync(Agency agency);
    }
}