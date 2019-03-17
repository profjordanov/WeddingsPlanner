using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using WeddingsPlanner.Api.Controllers._Base;
using WeddingsPlanner.Core;
using WeddingsPlanner.Core.Models.Agencies;
using WeddingsPlanner.Core.Services;
using WeddingsPlanner.Data.Entities;

namespace WeddingsPlanner.Api.Controllers
{
    [Authorize]
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
        /// Gets <see cref="Agency"/> by ID.
        /// </summary>
        /// <param name="agencyId"></param>
        /// <response code="200">Valid ID.</response>
        /// <response code="400">Invalid ID.</response>
        [HttpGet]
        [Route("single/{agencyId}")]
        [ProducesResponseType(typeof(Agency), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetSingle([FromRoute] int agencyId) =>
            (await _agenciesService.GetSingleAsync(agencyId))
            .Match(Ok, Error);

        /// <summary>
        /// Gets <see cref="Agency"/> by Name.
        /// </summary>
        /// <param name="agencyName"></param>
        /// <response code="200">Valid Agency Name.</response>
        /// <response code="400">Invalid Agency Name..</response>
        [HttpGet]
        [Route("by-name/{agencyName}")]
        [ProducesResponseType(typeof(Agency), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetByName([FromRoute] string agencyName) =>
            (await _agenciesService.GetFirstByNameAsync(agencyName))
            .Match(Ok, Error);

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

        /// <summary>
        /// Updates an agency.
        /// </summary>
        /// <param name="agency"><seealso cref="Agency"/></param>
        /// <returns>updated agency.</returns>
        /// <response code="200">An agency was update.</response>
        /// <response code="400">
        /// Cannot find agency with current ID or
        /// invalid agency name or town.
        /// </response>
        [HttpPut]
        [ProducesResponseType(typeof(Agency), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put([FromBody] Agency agency) =>
            (await _agenciesService.UpdateAsync(agency))
            .Match(updatedAgency => Ok(new { updatedAgency }), Error);

        /// <summary>
        /// Deletes an <see cref="Agency"/>.
        /// </summary>
        /// <param name="agency"></param>
        /// <returns>Deleted agancy.</returns>
        /// <response code="200">An agency was deleted.</response>
        /// <response code="400">
        /// Cannot find agency with current ID or name and town.
        /// </response>
        [HttpDelete]
        [ProducesResponseType(typeof(Agency), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Error), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete([FromBody] Agency agency) =>
            (await _agenciesService.DeleteAsync(agency))
            .Match(Ok, Error);
    }
}