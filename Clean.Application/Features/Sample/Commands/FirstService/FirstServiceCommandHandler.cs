using Clean.Application.ExceptionHandler;
using Clean.Domain.Enums;
using MediatR;

namespace Clean.Application.Features.Sample.Commands.FirstService
{
    public class FirstServiceCommandHandler : IRequestHandler<FirstServiceCommand, FirstServiceVm>
    {
        ApiResponseException _apiResponseException;

        public FirstServiceCommandHandler(ApiResponseException apiResponseException)
        {
            _apiResponseException = apiResponseException;
        }

        public async Task<FirstServiceVm> Handle(FirstServiceCommand request, CancellationToken cancellationToken)
        {
            if(request.CreateError)
            {
                _apiResponseException.SetDetail(ResponseCodes.EXCEPTION);
                throw _apiResponseException;
            }
            return new FirstServiceVm
            {
                Param1 = "Hello"
            };
        }
    }
}