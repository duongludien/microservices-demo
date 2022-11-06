using MassTransit;
using Microsoft.EntityFrameworkCore;
using WalletService.Api.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<WalletDbContext>(options => 
    options.UseMySql("Server=localhost;Database=wallet;Uid=root;Pwd=NoPassword1;", ServerVersion.Parse("8.0.31")));

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", host =>
        {
            host.Username("guest");
            host.Password("guest");
        });
    });
});

builder.Services.AddGrpc();


var app = builder.Build();

using var scope = app.Services.CreateScope();
var service = scope.ServiceProvider.GetRequiredService<WalletDbContext>();
service.Database.Migrate();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapGrpcService<WalletService.Api.Grpc.WalletService>();

app.Run();
