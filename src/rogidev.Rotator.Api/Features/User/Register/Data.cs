using Ardalis.Result;
using Dapper;
using Microsoft.Data.SqlClient;
using rogidev.Rotator.Api.Entities;
using rogidev.Rotator.Common.Const;
using rogidev.Rotator.Common.Cryptography;

namespace rogidev.Rotator.Api.Features.User.Register;

public interface IRegisterService
{
    Task<Result<UserEntity>> Register(Request registerRequest);
}

public class RegisterService(IConfiguration configuration) : IRegisterService
{
    public async Task<Result<UserEntity>> Register(Request registerRequest)
    {
        await using var connection = new SqlConnection(configuration.GetConnectionString(ConnectionStrings.DefaultConnection));
        await connection.OpenAsync();
        var userExists = await connection.ExecuteScalarAsync<bool>($"SELECT 1 FROM [User] WHERE {nameof(UserEntity.Email)} = @Email", new { registerRequest.Email });
        if (userExists)
        {
            return Result.Invalid(new ValidationError(){ Identifier = nameof(UserEntity.Email), ErrorMessage = "User already exists"});
        }

        var password = Hasher.Hash(registerRequest.Password);
        var userId = await connection.QuerySingleAsync<long>($"INSERT INTO [User]({nameof(UserEntity.Email)}, {nameof(UserEntity.Password)}) OUTPUT INSERTED.Id VALUES (@Email, @Password)", new { registerRequest.Email, password});
        return await connection.QuerySingleAsync<UserEntity>($"SELECT {nameof(UserEntity.Id)}, {nameof(UserEntity.Email)} FROM [User] WHERE {nameof(UserEntity.Id)} = @Id", new { Id = userId });
    }
}

