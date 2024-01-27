using System.Security.Claims;

namespace rogidev.Rotator.Api.Features.User.Get;

public class GetUserEndpoint : EndpointWithoutRequest<Response>
{
    public override void Configure()
    {
        Get("");
        Group<UserApiGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await SendAsync(new Response()
        {
            Email = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value,
        }, cancellation: ct);
    }
}