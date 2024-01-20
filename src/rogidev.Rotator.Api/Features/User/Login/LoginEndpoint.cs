using System.Security.Claims;
using rogidev.Rotator.Common.Extensions;

namespace rogidev.Rotator.Api.Features.User.Login;

public class LoginEndpoint(IOptions<AuthOptions> authOptions, ILoginService loginService) : Endpoint<Request, Response>
{
    public override void Configure()
    {
        Post("login");
        Group<UserApiGroup>();
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var loginResult = await loginService.Login(req);
        if (loginResult.IsSuccess)
        {
            var jwtToken = JWTBearer.CreateToken(
                signingKey: authOptions.Value.JwtKey,
                expireAt: DateTime.UtcNow.AddDays(1),
                privileges: u =>
                {
                    u.Claims.Add(new(ClaimTypes.Email, req.Email));
                });

            await this.SendResponse(loginResult, _ => new Response()
            {
                Token = jwtToken
            });
        }
        await this.SendResponse(loginResult, _ => new Response());
    }
}