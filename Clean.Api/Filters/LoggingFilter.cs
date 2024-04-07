using Clean.Application.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Clean.API.Filters
{
    public class LoggingFilter : ActionFilterAttribute
    {
        Logging _loggingService;
        public LoggingFilter(Logging loggingService)
        {
            _loggingService = loggingService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                var guid = Guid.NewGuid().ToString();
                var UserId = context.HttpContext.User.FindFirst("BrokerId") == null ? null : context.HttpContext.User.FindFirst("BrokerId").Value;
                var requestBody = context.ActionArguments.Any() ? context.ActionArguments.FirstOrDefault().Value : null;
                context.HttpContext.Request.Headers.Add("RequestId", guid);
                var rdcontroller = context.RouteData.Values["controller"] as string;
                var rdaction = context.RouteData.Values["action"] as string;

                if (rdaction == "Home" && rdcontroller == "Account")
                {
                    return;
                }

                var path = context.HttpContext.Request.Path.ToString();
                _loggingService.InsertApiLog(rdcontroller, rdaction, path, guid, UserId, false, requestBody);
            }
            catch (Exception ex) { }
        }

        public override async void OnActionExecuted(ActionExecutedContext context)
        {
            try
            {
                var UserId = context.HttpContext.User.FindFirst("BrokerId") == null ? null : context.HttpContext.User.FindFirst("BrokerId").Value;
                var guid = context.HttpContext.Request.Headers["RequestId"];
                var rdcontroller = context.RouteData.Values["controller"] as string;
                var rdaction = context.RouteData.Values["action"] as string;
                var result = context.Result;
                var path = context.HttpContext.Request.Path.ToString();

                if (rdaction == "Home" && rdcontroller == "Account")
                {
                    return;
                }

                if (result is OkObjectResult okObjectResult)
                {
                    var responseBody = okObjectResult.Value;
                    _loggingService.InsertApiLog(rdcontroller, rdaction, path, guid, UserId, true, responseBody);
                }
                else if (result is JsonResult jsonResult)
                {
                    var responseBody = jsonResult.Value;
                    _loggingService.InsertApiLog(rdcontroller, rdaction, path, guid, UserId, true, responseBody);
                }
            }
            catch (Exception ex) { }
        }
    }

}
