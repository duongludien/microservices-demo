using BalanceService.Domain.Interfaces;
using BalanceService.Domain.Models;
using IntegrationEvents;
using MassTransit;
using WalletService.Api.Client;

namespace BalanceService.Api.EventHandlers;

public class BudgetCreatedEventHandler : IConsumer<BudgetCreatedEvent>
{
    private readonly IGrpcWalletService _walletService;
    private readonly IBalanceRepository _balanceRepository;
    private readonly IBus _bus;

    public BudgetCreatedEventHandler(IGrpcWalletService walletService, IBalanceRepository balanceRepository, IBus bus)
    {
        _walletService = walletService;
        _balanceRepository = balanceRepository;
        _bus = bus;
    }

    public async Task Consume(ConsumeContext<BudgetCreatedEvent> context)
    {
        var wallets = await _walletService.GetAllWalletsAsync();

        var balances = new List<Balance>();
        
        foreach (var wallet in wallets.Items)
        {
            var balance = await _balanceRepository.CreateAsync(new Balance(context.Message.BudgetId, wallet.Id, 0));
            balances.Add(balance);
        }

        await _bus.Publish(BalanceItemsForBudgetCreatedEvent.CreateNew(
            context.Message.BudgetId,
            balances.Select(b => b.Id).ToList()));
    }
}
