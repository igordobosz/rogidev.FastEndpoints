namespace rogidev.Rotator.Api.Features.User.Login;

public sealed class Request
{
    public string Email { get; set; }

    public string Password { get; set; }
}

public class RequestValidator : Validator<Request>
{
    public RequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty();

        RuleFor(x => x.Password)
            .NotEmpty();
    }
}