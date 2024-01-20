using Ardalis.Result;
using Dapper;
using Microsoft.Data.SqlClient;
using rogidev.Rotator.Api.Entities;
using rogidev.Rotator.Common.Const;
using rogidev.Rotator.Common.Cryptography;

namespace rogidev.Rotator.Api.Features.User.Login;

public interface ILoginService
{
    Task<Result<UserEntity>> Login(Request req);
}

public class LoginService(IConfiguration configuration) : ILoginService
{
    public async Task<Result<UserEntity>> Login(Request req)
    {
        await using var connection = new SqlConnection(configuration.GetConnectionString(ConnectionStrings.DefaultConnection));
        await connection.OpenAsync();

        var user = await connection.QuerySingleOrDefaultAsync<UserEntity>($"SELECT {nameof(UserEntity.Id)}, {nameof(UserEntity.Email)}, {nameof(UserEntity.Password)} FROM [User] WHERE {nameof(UserEntity.Email)} = @Email", new { req.Email });
        if (user is null || !Hasher.Verify(req.Password, user.Password))
        {
            return Result.Invalid(new ValidationError() { Identifier = nameof(Request.Password), ErrorMessage = "User does not exists or the password is wrong." });
        }

        return user;
    }
}