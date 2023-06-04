namespace BrassTask.Api.Infrastructure.Swagger;

using BrassTask.Api.Infrastructure.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

public static class ApiResponseConventions
{
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(ProblemDetail), 404)]
    [ProducesDefaultResponseType(typeof(ProblemDetail))]
    [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
    public static void Get(
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix), ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
        object id)
    {
    }

    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(ProblemDetail), 404)]
    [ProducesDefaultResponseType(typeof(ProblemDetail))]
    [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
    public static void Find(
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix), ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
        object id)
    {
    }

    [ProducesResponseType(201)]
    [ProducesResponseType(typeof(ProblemDetail), 400)]
    [ProducesDefaultResponseType(typeof(ProblemDetail))]
    [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
    public static void Post(
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any), ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
        object model)
    {
    }

    [ProducesResponseType(201)]
    [ProducesResponseType(typeof(ProblemDetail), 400)]
    [ProducesDefaultResponseType(typeof(ProblemDetail))]
    [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
    public static void Create(
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any), ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
        object model)
    {
    }

    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(ProblemDetail), 404)]
    [ProducesResponseType(typeof(ProblemDetail), 400)]
    [ProducesDefaultResponseType(typeof(ProblemDetail))]
    [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
    public static void Put(
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix), ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
        object id,
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any), ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
        object model)
    {
    }

    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(ProblemDetail), 404)]
    [ProducesResponseType(typeof(ProblemDetail), 400)]
    [ProducesDefaultResponseType(typeof(ProblemDetail))]
    [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
    public static void Edit(
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix), ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
        object id,
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any), ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
        object model)
    {
    }

    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(ProblemDetail), 404)]
    [ProducesResponseType(typeof(ProblemDetail), 400)]
    [ProducesDefaultResponseType(typeof(ProblemDetail))]
    [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
    public static void Update(
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix), ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
        object id,
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any), ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
        object model)
    {
    }

    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(ProblemDetail), 404)]
    [ProducesDefaultResponseType(typeof(ProblemDetail))]
    [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
    public static void Delete(
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix), ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
        object id)
    {
    }

    [ProducesResponseType(200)]
    [ProducesDefaultResponseType(typeof(ProblemDetail))]
    [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
    public static void Action(
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any), ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
        object model)
    {
    }
}
