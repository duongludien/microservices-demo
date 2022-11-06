using BudgetService.Api.EventHandlers;
using BudgetService.Api.Infrastructure;
using IntegrationEvents;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace BudgetService.Api;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGrpcClients(this IServiceCollection services, GrpcServicesConfiguration configuration)
    {
        services.AddGrpcClient<GrpcBalance.BalanceService.BalanceServiceClient>((_, options) =>
        {
            options.Address = new Uri(configuration.BalanceGrpcServiceUrl);
        });

        return services;
    }

    public static IServiceCollection AddServiceBus(this IServiceCollection services, ServiceBusConfiguration configuration)
    {
        services.AddMassTransit(configurator =>
        {
            configurator.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(configuration.Host, configuration.VirtualHost, host =>
                {
                    host.Username(configuration.UserName);
                    host.Password(configuration.Password);
                });

                cfg.ConfigureEndpoints(context);
            });
            
            configurator.AddConsumer<BalanceItemsForBudgetCreatedEventHandler>();
        });

        return services;
    }

    public static IServiceCollection AddDb(this IServiceCollection services, string connectionString, string version = "8.0.31")
    {
        services.AddDbContext<BudgetDbContext>(options =>
            options.UseMySql(connectionString,
                ServerVersion.Parse(version)));

        return services;
    }
}
