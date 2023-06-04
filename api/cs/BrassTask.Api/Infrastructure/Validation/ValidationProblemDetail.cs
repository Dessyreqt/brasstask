namespace BrassTask.Api.Infrastructure.Validation;

public class ValidationProblemDetail : ProblemDetail
{
    public override string? Type { get; set; } = "problem/validation-error";
    public override string Title { get; set; } = "Bad request";
    public override int Status { get; set; } = 400;
    public IEnumerable<ValidationError> Errors { get; set; } = Enumerable.Empty<ValidationError>();
}
