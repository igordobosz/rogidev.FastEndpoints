using Microsoft.Extensions.Options;

public class AuthOptions
{
    public const string SectionName = nameof(AuthOptions);

    [Required]
    public required string JwtKey { get; set; }
}

[OptionsValidator]
public partial class ValidateAuthOptions : IValidateOptions<AuthOptions> { }