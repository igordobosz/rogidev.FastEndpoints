using FastEndpoints.Swagger;
using rogidev.Rotator.Api.Extensions;
using rogidev.Rotator.Persistence.Migration;

var builder = WebApplication.CreateBuilder();

builder.Services.AddFastEndpoints();

builder.Services.AddCommonOptions(builder.Configuration);

builder.Services.AddAuth(builder.Configuration);

builder.Services.SwaggerDocument();

builder.Services.AddServices();

var connString = builder.Configuration.GetConnectionString("defaultConnection");

MigrateDatabase.Migrate(connString);

var app = builder.Build();
app
    .UseAuthentication()
    .UseAuthorization()
    .UseFastEndpoints()
    .UseSwaggerGen();

app.Run();