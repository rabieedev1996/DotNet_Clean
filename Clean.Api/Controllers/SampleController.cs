using Clean.Application.Common;
using Clean.Application.Features.Sample.Commands.FirstService;
using Clean.Application.Features.Sample.Commands.GenerateSampleToken;
using Clean.Application.Features.Sample.Commands.ValidateToken;
using Clean.Application.Models;
using Clean.Domain;
using Clean.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using YasgapNew.Api.Filters;

namespace Clean.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        IMediator _mediator;
        ResponseGenerator _rsponseGenerator;
        public SampleController(IMediator mediator, ResponseGenerator rsponseGenerator)
        {
            _mediator = mediator;
            _rsponseGenerator = rsponseGenerator;
        }

        [HttpPost("/GenerateToken")]
        [ProducesResponseType(typeof(ApiResponseModel<string>),StatusCodes.Status200OK)]
        public async Task<IActionResult> GenerateToken()
        {
            var resultData = await _mediator.Send(new GenerateSampleTokenInput { });
            var resultObj = _rsponseGenerator.GetResponseModel(Domain.Enums.ResponseCodes.SUCCESS, resultData);
            return resultObj;
        }
        [HttpPost("/ValidateToken")]
        [ProducesResponseType(typeof(ApiResponseModel<ValidateTokenVm>), StatusCodes.Status200OK)]
        [CustomAuthorize(IdentityRoles.USER,IdentityReason.ON_REGISTER)]
        public async Task<IActionResult> ValidateToken()
        {
            var resultData = await _mediator.Send(new ValidateTokenInput { });
            var resultObj = _rsponseGenerator.GetResponseModel(Domain.Enums.ResponseCodes.SUCCESS, resultData);
            return resultObj;
        }


    }
}
