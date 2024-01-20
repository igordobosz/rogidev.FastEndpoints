namespace rogidev.Rotator.Api.Features.User.Register;

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
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(8);
    }
}
