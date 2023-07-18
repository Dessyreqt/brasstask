namespace BrassTask.Api.Infrastructure.Validation;

using System.Text.Json;
using System.Text.Json.Serialization;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next)
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
                Type = "https://datatracker.ietf.org/doc/html/rfc9110#name-400-bad-request",
                Title = "One or more validation errors occurred.",
                Status = 400,
                Detail = "See the errors property for details.",
                Errors = ex.Errors.Select(x => new ValidationError { Name = x.PropertyName, Detail = x.ErrorMessage }),
            };

            await context.Response.WriteAsJsonAsync(
                response,
                new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }
        catch (UnauthorizedAccessException ex)
        {
            context.Response.StatusCode = 401;
            var response = new ProblemDetails
            {
                Type = "https://datatracker.ietf.org/doc/html/rfc9110#name-401-unauthorized", Title = "Unauthorized", Status = 401, Detail = ex.Message,
            };

            await context.Response.WriteAsJsonAsync(
                response,
                new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }
    }
}
