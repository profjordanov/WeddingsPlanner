using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using WeddingsPlanner.Core;

namespace WeddingsPlanner.Api.Filters
{
    public class ModelStateFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
            {
                return;
            }

            var errors = context
                .ModelState
                .Values
                .SelectMany(v => v.Errors.Select(e => e.ErrorMessage));

            context.Result = new BadRequestObjectResult(new Error(errors));
        }
    }
}
