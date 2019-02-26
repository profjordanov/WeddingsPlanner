using Microsoft.AspNetCore.Mvc;
using WeddingsPlanner.Core;
using WeddingsPlanner.Core.Reports;

namespace WeddingsPlanner.Api.Controllers._Base
{
    [Route("api/[controller]")]
    public class ApiController : Controller
    {
        protected IActionResult Error(Error error) =>
            new BadRequestObjectResult(error);

        protected IActionResult CsvReport(CsvReport csvReport) =>
            File(csvReport.Data, "text/csv", csvReport.FileName);
    }
}
