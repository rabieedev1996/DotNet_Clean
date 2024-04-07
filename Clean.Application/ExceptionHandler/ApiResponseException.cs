using Clean.Application.Models;
using Clean.Application.Common;
using Clean.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Clean.Application.ExceptionHandler;

public class ApiResponseException : Exception
{
    private ObjectResult _responseObject;
    private ResponseGenerator _responseGenerator;

    public ApiResponseException(ResponseGenerator responseGenerator)
    {
        _responseGenerator = responseGenerator;
    }

    public void SetDetail(ResponseCodes code)
    {
        _responseObject = _responseGenerator.GetResponseModel<object>(code, null);
    }
    public void SetDetail<TModel>(ResponseCodes code, TModel Data)
    {
        _responseObject = _responseGenerator.GetResponseModel<object>(code, Data);
    }

    public ObjectResult ResponseObject
    {
        get { return _responseObject; }
    }
}