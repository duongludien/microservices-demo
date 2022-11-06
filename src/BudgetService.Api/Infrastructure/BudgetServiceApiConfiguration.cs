namespace BudgetService.Api.Infrastructure;

public class ServiceBusConfiguration
{
    public string Host { get; set; }
    public string VirtualHost { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
}

public class GrpcServicesConfiguration
{
    public string  BalanceGrpcServiceUrl { get; set; }
}
