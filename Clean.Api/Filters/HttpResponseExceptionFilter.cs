using Clean.Application.ExceptionHandler;
using Clean.Application.Models;
using Clean.Application.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Clean.Application.Contract.Services;
using Clean.Domain.Entities;
using System.IO;
using System;

namespace Clean.Api.Filters
{

    public class HttpResponseExceptionFilter : IExceptionFilter
    {
        Logging _loggingService;
        private ResponseGenerator _responseGenerator;
        public HttpResponseExceptionFilter(Logging loggingService, ResponseGenerator responseGenerator)
        {
            _loggingService = loggingService;
            _responseGenerator = responseGenerator;
        }
        public int Order => int.MaxValue - 10;



        public void OnException(ExceptionContext context)
        {
            var UserId = context.HttpContext.User.FindFirst("BrokerId") == null ? null : context.HttpContext.User.FindFirst("BrokerId").Value;
            var guid = context.HttpContext.Request.Headers["RequestId"];
            var rdcontroller = context.RouteData.Values["controller"] as string;
            var rdaction = context.RouteData.Values["action"] as string;
            var result = context.Result;
            var path = context.HttpContext.Request.Path.ToString();

            ApiResponseModel<object> resultBody;
            if (context.Exception is ApiResponseException ex)
            {
                if (!(rdaction.ToLower() == "Home" && rdcontroller.ToLower() == "Account"))
                {
                    _loggingService.InsertApiLog(rdcontroller, rdaction, path, guid, UserId, true, ex.ResponseObject);
                }

                context.Result = ex.ResponseObject;
            }
            else
            {
                if (!(rdaction.ToLower() == "Home" && rdcontroller.ToLower() == "Account"))
                {
                    _loggingService.InsertApiException(rdcontroller, rdaction, guid, UserId, context.Exception);
                }

                context.Result = _responseGenerator.GetResponseModel<object>(Domain.Enums.ResponseCodes.EXCEPTION);
            }

            context.ExceptionHandled = true;
        }
    }
}