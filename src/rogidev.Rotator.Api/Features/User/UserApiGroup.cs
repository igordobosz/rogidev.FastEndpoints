namespace rogidev.Rotator.Api.Features.User;

public sealed class UserApiGroup : Group
{
    public UserApiGroup()
    {
        Configure("user", ep =>
        {
            ep.Description(x => x.Produces(401));
        });
    }
}