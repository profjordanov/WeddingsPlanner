using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using WeddingsPlanner.Api.Controllers._Base;
using WeddingsPlanner.Core.Models.Agencies;
using WeddingsPlanner.Core.Services;

namespace WeddingsPlanner.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgenciesController : ApiController
    {
        private readonly IAgenciesService _agenciesService;

        public AgenciesController(IAgenciesService agenciesService)
        {
            _agenciesService = agenciesService;
        }

        /// <summary>
        /// Gets all agencies.
        /// </summary>
        /// <returns>collection of agencies/</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AgencyServiceModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll() =>
            Ok(await _agenciesService.GetAgenciesOrderedAsync(CancellationToken.None));
    }
}