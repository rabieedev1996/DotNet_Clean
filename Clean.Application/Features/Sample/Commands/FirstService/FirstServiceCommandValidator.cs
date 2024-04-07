using Clean.Application.Contract.Services;
using Clean.Domain.Enums;
using FluentValidation;

namespace Clean.Application.Features.Sample.Commands.FirstService;

public class FirstServiceCommandValidator : AbstractValidator<FirstServiceCommand>
{
    IMessageService _messageService;

    public FirstServiceCommandValidator(IMessageService messageService)
    {
        _messageService = messageService;
        RuleFor(p => p.RequiredParam)
           .NotNull().WithMessage(_messageService.GetMessage(MessageCodes.MESSAGE_REQUIRED_PARAM, Langs.FA));
    }
}