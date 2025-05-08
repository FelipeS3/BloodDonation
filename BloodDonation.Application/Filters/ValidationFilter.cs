using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BloodDonation.Application.Filters;

public class ValidationFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var messages = context.ModelState
                .SelectMany(x => x.Value.Errors)
                .Select(x => x.ErrorMessage)
                .ToList();

            context.Result = new BadRequestObjectResult(messages);
        }
    }
}