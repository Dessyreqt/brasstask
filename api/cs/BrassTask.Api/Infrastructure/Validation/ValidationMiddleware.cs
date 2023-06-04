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
        try { await _next(context); }
        catch (ValidationException ex)
        {
            context.Response.StatusCode = 400;
            var response = new ValidationProblemDetail
            {
                Errors = ex.Errors.Select(
                    x => new ValidationError
                    {
                        Name = x.PropertyName,
                        Detail = x.ErrorMessage
                    })
            };

            await context.Response.WriteAsJsonAsync(
                response,
                new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
        }
    }
}
