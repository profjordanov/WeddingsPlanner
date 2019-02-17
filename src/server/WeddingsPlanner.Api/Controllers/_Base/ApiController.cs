using WeddingsPlanner.Core;
using Microsoft.AspNetCore.Mvc;

namespace WeddingsPlanner.Api.Controllers
{
    [Route("api/[controller]")]
    public class ApiController : Controller
    {
        protected IActionResult Error(Error error) =>
            new BadRequestObjectResult(error);
    }
}
