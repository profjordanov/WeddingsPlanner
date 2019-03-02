using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Threading.Tasks;
using WeddingsPlanner.Api.Controllers._Base;
using WeddingsPlanner.Api.OperationFilters;
using WeddingsPlanner.Core;
using WeddingsPlanner.Core.Models;
using WeddingsPlanner.Core.Reports;
using WeddingsPlanner.Core.Services;

namespace WeddingsPlanner.Api.Controllers
{
    /// <summary>
    /// Controller for uploading collection of entities from file.
    /// </summary>
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
        /// <param name="request">File request</param>
        /// <returns>Either CSV report file or an error.</returns>
        /// <response code="200">If the file has been uploaded successfully.</response>
        /// <response code="400">If the file don't match/don't meet the requirements.</response>
        [HttpPost("json-agencies")]
        [ProducesResponseType(typeof(CsvReport), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
        [SwaggerOperationFilter(typeof(UploadDocumentsOperationFilter))]
        public async Task<IActionResult> UploadAgenciesByJson([FromForm] UploadDocumentRequest request) =>
            (await _onboardingService.AgenciesByJson(request.File))
            .Match(CsvReport, Error);

        /// <summary>
        /// Uploads collection of people from json file.
        /// </summary>
        /// <param name="request">File request</param>
        /// <returns>Either CSV report file or an error.</returns>
        /// <response code="200">If the file has been uploaded successfully.</response>
        /// <response code="400">If the file don't match/don't meet the requirements.</response>
        [HttpPost("json-people")]
        [ProducesResponseType(typeof(CsvReport), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
        [SwaggerOperationFilter(typeof(UploadDocumentsOperationFilter))]
        public async Task<IActionResult> UploadPeopleByJson([FromForm] UploadDocumentRequest request) =>
            (await _onboardingService.PeopleByJson(request.File))
            .Match(CsvReport, Error);

        /// <summary>
        /// Uploads collection of weddings from json file.
        /// </summary>
        /// <param name="request">File request</param>
        /// <returns>Either CSV report file or an error.</returns>
        /// <response code="200">If the file has been uploaded successfully.</response>
        /// <response code="400">If the file don't match/don't meet the requirements.</response>
        [HttpPost("json-weddings")]
        [ProducesResponseType(typeof(CsvReport), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
        [SwaggerOperationFilter(typeof(UploadDocumentsOperationFilter))]
        public async Task<IActionResult> UploadWeddingsByJson([FromForm] UploadDocumentRequest request) =>
            (await _onboardingService.WeddingsByJson(request.File))
            .Match(CsvReport, Error);

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