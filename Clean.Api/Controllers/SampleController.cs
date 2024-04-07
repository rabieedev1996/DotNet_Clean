using Clean.Application.Common;
using Clean.Application.Features.Sample.Commands.FirstService;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [Route("/FirstService")]
        [HttpPost]
        public async Task<IActionResult> FirstService(FirstServiceCommand command)
        {
            var resultData = await _mediator.Send(command);
            var resultObj = _rsponseGenerator.GetResponseModel(Domain.Enums.ResponseCodes.SUCCESS, resultData);
            return resultObj;
        }
    }
}
