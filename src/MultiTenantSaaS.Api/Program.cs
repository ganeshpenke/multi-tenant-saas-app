using MultiTenantSaaS.Api.Middlewares;
using MultiTenantSaaS.Application.Extensions;
using MultiTenantSaaS.Infraastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddApplication();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddOpenApi();

var app = builder.Build();

app.MapOpenApi();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<TenantResolutionMiddleware>();

app.MapControllers();

app.Run();
