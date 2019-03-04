using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using WeddingsPlanner.Api.Controllers._Base;
using WeddingsPlanner.Core.Models.Weddings;
using WeddingsPlanner.Core.Services;

namespace WeddingsPlanner.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeddingsController : ApiController
    {
        private readonly IWeddingsService _weddingsService;

        public WeddingsController(IWeddingsService weddingsService)
        {
            _weddingsService = weddingsService;
        }

        /// <summary>
        /// Get all weddings guest lists.
        /// </summary>
        /// <returns>
        /// •   bride and bridegrooms full names
        /// •   agency that organizes the wedding (its name and town)
        /// •   count of all invited guests
        /// •   count of guest from bride’s family
        /// •   count of guest from bridegroom’s family
        /// •   count of guest that confirmed they would attend at the wedding 
        /// •   the list of full names of guests who would attend at the wedding
        /// </returns>
        [HttpGet]
        [Route("all-guest-lists")]
        [ProducesResponseType(typeof(IEnumerable<WeddingsGuestListServiceModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AllGuestLists() =>
            Ok(await _weddingsService.GetAllWeddingsGuestListsAsync());
    }
}