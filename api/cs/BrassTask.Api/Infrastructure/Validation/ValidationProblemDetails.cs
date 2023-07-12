namespace BrassTask.Api.Infrastructure.Validation;

using Microsoft.AspNetCore.Mvc;

public class ValidationProblemDetails : ProblemDetails
{
    public IEnumerable<ValidationError> Errors { get; set; } = Enumerable.Empty<ValidationError>();
}
