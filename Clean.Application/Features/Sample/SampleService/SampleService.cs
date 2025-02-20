using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clean.Application.Contract.Services;
using Clean.Application.ExceptionHandler;
using Clean.Domain.Enums;
using FluentValidation;
using MediatR;

namespace Clean.Application.Features.Sample.SampleService
{
    #region Handler
    public class SampleServiceHandler : IRequestHandler<SampleServiceInput, SampleServiceVm>
    {
        ApiResponseException _apiResponseException;

        public SampleServiceHandler(ApiResponseException apiResponseException)
        {
            _apiResponseException = apiResponseException;
        }
        public async Task<SampleServiceVm> Handle(SampleServiceInput request, CancellationToken cancellationToken)
        {
            if (request.CreateError)
            {
                _apiResponseException.SetDetail(ResponseCodes.EXCEPTION);
                throw _apiResponseException;
            }
            return new SampleServiceVm
            {
                Param1 = "Hello"
            };
        }
    }
    #endregion

    #region Query
    public class SampleServiceInput : IRequest<SampleServiceVm>
    {
        public int Param1 { set; get; }
        public string RequiredParam { set; get; }
        public bool CreateError { set; get; }
    }
    public class SampleServiceValidator : AbstractValidator<SampleServiceInput>
    {
        IMessageService _messageService;

        public SampleServiceValidator(IMessageService messageService)
        {
            _messageService = messageService;
            RuleFor(p => p.RequiredParam)
               .NotNull().WithMessage(_messageService.GetMessage(MessageCodes.MESSAGE_REQUIRED_PARAM, Langs.FA));
        }
    }

    #endregion

    #region ViewModel
    public class SampleServiceVm
    {
        public string Param1 { set; get; }

    }
    #endregion
}