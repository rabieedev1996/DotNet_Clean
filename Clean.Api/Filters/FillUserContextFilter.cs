using Clean.Application;
using Clean.Domain;
using Clean.Identity;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Clean.Api.Filters
{
    public class FillUserContextFilter : IActionFilter
    {
        private UserContext _userContext;
        Configs _config;
        public FillUserContextFilter(UserContext userContext,Configs configs)
        {
            _userContext = userContext;
            _config = configs;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var tokenObj = context.HttpContext.Request.Cookies["Token"];
            if (tokenObj==null)
            {
                tokenObj = context.HttpContext.Request.Headers["Authorization"];
            }

            if (tokenObj == null)
            {
                return;
            }
            var token = tokenObj.ToString();
            var principals = IdentityUtility.GetPrincipals(_config.TokenConfigs.Key,_config.TokenConfigs.Audience,_config.TokenConfigs.Issuer,token);
            context.HttpContext.Items["principals"] = principals;
            string userId = principals.Claims.FirstOrDefault(claim => claim.Type == "UserId")?.Value;
            _userContext.UserId = userId;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}