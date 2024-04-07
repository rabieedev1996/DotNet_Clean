using FluentValidation;
using Clean.Application.Common;
using Clean.Application.ExceptionHandler;
using MediatR;

namespace Clean.Application.Behaviours;

public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator> _validators;
    ApiResponseException _apiResponseException;
    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators, ApiResponseException apiResponseException)
    {
        _validators = validators;
        _apiResponseException = apiResponseException;
    }


    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);

            var validationResults =
                await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
            var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).Select(a => a.ErrorMessage)
                .ToList();

            if (failures.Any())
            {
                _apiResponseException.SetDetail(Domain.Enums.ResponseCodes.VALIDATION_ERROR);
                throw _apiResponseException;
            }
        }

        return await next();
    }
}