namespace BrassTask.Api.Infrastructure.Validation;

using System.Text.Json;
using System.Text.Json.Serialization;
using FluentValidation;

public class ValidationMiddleware
{
    private readonly RequestDelegate _next;

    public ValidationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException ex)
        {
            context.Response.StatusCode = 400;
            var response = new ValidationProblemDetails
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Title = "One or more validation errors occurred.",
                Status = 400,
                Detail = "See the errors property for details.",
                Errors = ex.Errors.Select(x => new ValidationError { Name = x.PropertyName, Detail = x.ErrorMessage }),
            };

            await context.Response.WriteAsJsonAsync(
                response,
                new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }
    }
}
