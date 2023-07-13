namespace BrassTask.Api.Infrastructure.Validation;

using FluentValidation;
using MediatR.Pipeline;

public class ValidationPreProcessor<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationPreProcessor(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(request, cancellationToken)));
        var validationFailures = validationResults.SelectMany(r => r.Errors).Where(f => f is not null).ToList();

        if (validationFailures.Count != 0)
        {
            throw new ValidationException(validationFailures);
        }
    }
}
