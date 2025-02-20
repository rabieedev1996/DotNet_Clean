using System.Security.Claims;
using Clean.Domain.Enums;
using Clean.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace YasgapNew.Api.Filters;

public class CustomAuthorizeAttribute : ActionFilterAttribute, IOrderedFilter
{
    public int Order => 2;

    List<IdentityRoles> _roles;
    List<IdentityReason> _reasons;

    public CustomAuthorizeAttribute(params object[] role)
    {
        _roles = role.Where(a => a is IdentityRoles).Select(a => (IdentityRoles)a).ToList();
        _reasons = role.Where(a => a is IdentityReason).Select(a => (IdentityReason)a).ToList();
    }


    public override void OnActionExecuting(ActionExecutingContext context)
    {
        string path = context.HttpContext.Request.Path;
        var tokenObj = context.HttpContext.Request.Cookies["Token"];

        if (tokenObj == null)
        {
            tokenObj = context.HttpContext.Request.Headers["Authorization"];
        }

        if (tokenObj == null)
        {
            context.Result =
                           new ObjectResult(null)
                           {
                               StatusCode = StatusCodes.Status401Unauthorized
                           };
            return;
        }
        var token = tokenObj.ToString();

        var principals = (ClaimsPrincipal)context.HttpContext.Items["principals"];
        var result = IdentityUtility.ValidateToken(principals, _roles.Select(a => a.ToString()).ToList(),
            _reasons.Select(a => a.ToString()).ToList(), false);
        if (result.ValidateResult != TokenValidateResult.Valid)
        {
            context.Result =
                new ObjectResult(null)
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };
        }
        context.HttpContext.User = result.ClaimPrincipal;
    }
}