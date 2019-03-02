using Optional;
using System.Threading.Tasks;
using WeddingsPlanner.Data.Entities;

namespace WeddingsPlanner.Core.Services
{
    public interface IWeddingsService
    {
        Task<Option<Wedding, Error>> AddAsync(Wedding wedding);
    }
}