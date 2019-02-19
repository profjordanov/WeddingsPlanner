using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WeddingsPlanner.Api.OperationFilters;
using WeddingsPlanner.Core.Models;
using WeddingsPlanner.Core.Services;

namespace WeddingsPlanner.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OnboardingController : ApiController
    {
        private readonly IOnboardingService _onboardingService;

        public OnboardingController(IOnboardingService onboardingService)
        {
            _onboardingService = onboardingService;
        }
    }
}