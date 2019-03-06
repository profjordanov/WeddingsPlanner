using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using WeddingsPlanner.Api.Controllers._Base;
using WeddingsPlanner.Core;
using WeddingsPlanner.Core.Models.Agencies;
using WeddingsPlanner.Core.Services;
using WeddingsPlanner.Data.Entities;

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
        [Route("all")]
        [ProducesResponseType(typeof(IEnumerable<AgencyServiceModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll() =>
            Ok(await _agenciesService.GetAgenciesOrderedAsync(CancellationToken.None));

        /// <summary>
        /// Creates an agency.
        /// </summary>
        /// <param name="agency"><seealso cref="Agency"/></param>
        /// <returns><seealso cref="Agency"/></returns>
        /// <response code="201">An agency was created.</response>
        /// <response code="400">Invalid agency name or town.</response>
        [HttpPost]
        [ProducesResponseType(typeof(Agency), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post([FromBody] Agency agency) =>
            (await _agenciesService.AddAsync(agency))
            .Match(createdAgency => CreatedAtAction(nameof(Post), createdAgency), Error);

    }
}