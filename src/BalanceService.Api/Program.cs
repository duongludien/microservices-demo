using Autofac;
using Autofac.Extensions.DependencyInjection;
using BalanceService.Api.EventHandlers;
using BalanceService.Domain.Interfaces;
using BalanceService.Infrastructure;
using BalanceService.Infrastructure.Repositories;
using GrpcWallet;
using MassTransit;
using WalletService.Api.Client;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());







builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ISqlConnectionFactory, MySqlConnectionFactory>(_ 
    => new MySqlConnectionFactory("Server=localhost;Database=Balance;Uid=root;Pwd=NoPassword1;"));

builder.Services.AddScoped<IBalanceRepository, BalanceRepository>();

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", host =>
        {
            host.Username("guest");
            host.Password("guest");
        });

        cfg.ReceiveEndpoint("balance-service", e =>
        {
            e.ConfigureConsumer<BudgetCreatedEventHandler>(context);
            e.ConfigureConsumer<WalletCreatedEventHandler>(context);
        });
    });

    x.AddConsumer<BudgetCreatedEventHandler>();
    x.AddConsumer<WalletCreatedEventHandler>();
});

builder.Services.AddGrpc();

builder.Services.AddGrpcClient<Wallet.WalletClient>((_, options) =>
{
    options.Address = new Uri("https://localhost:7077");
});




builder.Host.ConfigureContainer<ContainerBuilder>(b =>
{
    b.RegisterModule(new WalletServiceApiClientModule());
});











var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapGrpcService<BalanceService.Api.Grpc.BalanceService>();

app.Run();
