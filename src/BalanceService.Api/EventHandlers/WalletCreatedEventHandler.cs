using BalanceService.Domain.Interfaces;
using IntegrationEvents;
using MassTransit;

namespace BalanceService.Api.EventHandlers;

public class WalletCreatedEventHandler : IConsumer<WalletCreatedEvent>
{
    private readonly IBalanceRepository _balanceRepository;

    public WalletCreatedEventHandler(IBalanceRepository balanceRepository)
    {
        _balanceRepository = balanceRepository;
    }

    public  Task Consume(ConsumeContext<WalletCreatedEvent> context)
    {
       return Task.CompletedTask;
    }
}
