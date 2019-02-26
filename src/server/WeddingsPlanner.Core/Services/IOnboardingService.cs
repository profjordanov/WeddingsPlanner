using Microsoft.AspNetCore.Http;
using Optional;
using System.Threading.Tasks;
using WeddingsPlanner.Core.Reports;

namespace WeddingsPlanner.Core.Services
{
    public interface IOnboardingService
    {
        Task<Option<CsvReport, Error>> AgenciesByJson(IFormFile file);
        Task<CsvReport> VenuesByXml(IFormFile file);
    }
}