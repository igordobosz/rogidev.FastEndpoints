using Ardalis.Result;
using FastEndpoints;

namespace rogidev.Rotator.Common.Extensions;

public static class ArdalisResultsExtensions
{
    public static async Task SendResponse<TResult, TResponse>(this IEndpoint ep, TResult result, Func<TResult, TResponse> mapper) where TResult : IResult
    {
        switch (result.Status)
        {
            case ResultStatus.Ok:
                await ep.HttpContext.Response.SendAsync(mapper(result));
                break;

            case ResultStatus.Invalid:
                result.ValidationErrors.ForEach(e =>
                    ep.ValidationFailures.Add(new(e.Identifier, e.ErrorMessage)));

                await ep.HttpContext.Response.SendErrorsAsync(ep.ValidationFailures);
                break;
        }
    }
}