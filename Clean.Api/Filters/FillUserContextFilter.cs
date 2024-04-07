using Clean.Application;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Clean.Api.Filters;

public class FillUserContextFilter : IActionFilter
{
    private UserContext _userContext;

    public FillUserContextFilter(UserContext userContext)
    {
        _userContext = userContext;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        string userId = context.HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "UserId")?.Value;
        _userContext.UserId = userId;
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}