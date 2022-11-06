using Autofac;
using Autofac.Extensions.DependencyInjection;
using BalanceService.Api.Client;
using BudgetService.Api;
using BudgetService.Api.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder();
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// .NET Core built-in DI
builder.Services.AddOptions();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDb(builder.Configuration.GetConnectionString("Db"));
builder.Services.AddServiceBus(builder.Configuration.GetSection("ServiceBus").Get<ServiceBusConfiguration>());
builder.Services.AddGrpcClients(builder.Configuration.GetSection("GrpcServices").Get<GrpcServicesConfiguration>());

// Autofac DI
builder.Host.ConfigureContainer<ContainerBuilder>(b =>
{
    b.RegisterModule<BalanceServiceApiClientModule>();
});

var app = builder.Build();

// Apply EF Core migrations
using var scope = app.Services.CreateScope();
var service = scope.ServiceProvider.GetRequiredService<BudgetDbContext>();
service.Database.Migrate();

// Middlewares
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
