using rogidev.Rotator.Api.Entities;
using rogidev.Rotator.Common.Extensions;

namespace rogidev.Rotator.Api.Features.User.Register;

public class RegisterEndpoint(IRegisterService registerService) : Endpoint<Request, Response, EndpointMapper>
{
    public override void Configure()
    {
        Post("register");
        Group<UserApiGroup>();
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var registerResult = await registerService.Register(req);
        await this.SendResponse(registerResult, r => Map.FromEntity(r));
    }
}

public class EndpointMapper : Mapper<Request, Response, UserEntity>
{
    public override Response FromEntity(UserEntity userEntity)
    {
        return new Response()
        {
            Email = userEntity.Email
        };
    }
}