using ErrorOr;
using FluentValidation;
using MediatR;

namespace PRN221.Project.Application.Common.Behaviours;

public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest where TResponse : IErrorOr
{
    private readonly AbstractValidator<TRequest>? _validator;

    public ValidationBehaviour(AbstractValidator<TRequest>? validator = null)
    {
        _validator = validator;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validator is null)
        {
            return await next();
        }

        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (validationResult.IsValid)
        {
            return await next();
        }

        var errors = validationResult.Errors
            .ConvertAll(failure => Error.Validation(
                failure.PropertyName,
                failure.ErrorMessage));

        return (dynamic)errors;
    }
}