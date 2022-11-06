using Autofac;

namespace BalanceService.Api.Client;

public class BalanceServiceApiClientModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<GrpcBalanceService>()
            .As<IGrpcBalanceService>()
            .InstancePerLifetimeScope();
    }
}
