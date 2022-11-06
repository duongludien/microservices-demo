using Autofac;

namespace WalletService.Api.Client;

public class WalletServiceApiClientModule : Module
{
    protected override void Load(ContainerBuilder builder)
    { 
        builder.RegisterType<GrpcWalletService>()
            .As<IGrpcWalletService>()
            .InstancePerLifetimeScope();
    }
}
