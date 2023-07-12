namespace BrassTask.Api.Infrastructure.Swagger;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

public static class ApiResponseConventions
{
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(ProblemDetails), 404)]
    [ProducesDefaultResponseType(typeof(ProblemDetails))]
    [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
    public static void Get([ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix), ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)] object id)
    {
    }

    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(ProblemDetails), 404)]
    [ProducesDefaultResponseType(typeof(ProblemDetails))]
    [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
    public static void Find([ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix), ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)] object id)
    {
    }

    [ProducesResponseType(201)]
    [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
    [ProducesDefaultResponseType(typeof(ProblemDetails))]
    [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
    public static void Post([ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any), ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)] object model)
    {
    }

    [ProducesResponseType(201)]
    [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
    [ProducesDefaultResponseType(typeof(ProblemDetails))]
    [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
    public static void Create([ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any), ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)] object model)
    {
    }

    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(ProblemDetails), 404)]
    [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
    [ProducesDefaultResponseType(typeof(ProblemDetails))]
    [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
    public static void Put(
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix), ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)] object id,
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any), ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)] object model)
    {
    }

    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(ProblemDetails), 404)]
    [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
    [ProducesDefaultResponseType(typeof(ProblemDetails))]
    [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
    public static void Edit(
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix), ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)] object id,
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any), ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)] object model)
    {
    }

    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(ProblemDetails), 404)]
    [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
    [ProducesDefaultResponseType(typeof(ProblemDetails))]
    [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
    public static void Update(
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix), ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)] object id,
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any), ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)] object model)
    {
    }

    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(ProblemDetails), 404)]
    [ProducesDefaultResponseType(typeof(ProblemDetails))]
    [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
    public static void Delete([ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix), ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)] object id)
    {
    }

    [ProducesResponseType(200)]
    [ProducesDefaultResponseType(typeof(ProblemDetails))]
    [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
    public static void Action([ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any), ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)] object model)
    {
    }
}
