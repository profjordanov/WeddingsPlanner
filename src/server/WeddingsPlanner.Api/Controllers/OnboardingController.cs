using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
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

        /// <summary>
        /// Uploads collection of agencies from json file.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>CSV report file.</returns>
        [HttpPost("json-agencies")]
        [SwaggerOperationFilter(typeof(UploadDocumentsOperationFilter))]
        public async Task<IActionResult> UploadAgenciesByJson([FromForm] UploadDocumentRequest request)
        {
            var result = await _onboardingService.AgenciesByJson(request.File);
            return File(result.Data, "text/csv", result.FileName);
        }

        /// <summary>
        /// Uploads collection of venues from xml file.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>CSV report file.</returns>
        [HttpPost("xml-venues")]
        [SwaggerOperationFilter(typeof(UploadDocumentsOperationFilter))]
        public async Task<IActionResult> UploadVenuesByXml([FromForm] UploadDocumentRequest request)
        {
            var result = await _onboardingService.VenuesByXml(request.File);
            return File(result.Data, "text/csv", result.FileName);
        }
    }
}