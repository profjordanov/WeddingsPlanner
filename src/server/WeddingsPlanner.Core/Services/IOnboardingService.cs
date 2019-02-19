using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using WeddingsPlanner.Core.Reports;

namespace WeddingsPlanner.Core.Services
{
    public interface IOnboardingService
    {
        Task<CsvReport> AgenciesByJson(IFormFile file);
    }
}