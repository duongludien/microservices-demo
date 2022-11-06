using BalanceService.Domain.Interfaces;
using BalanceService.Domain.Models;
using Grpc.Core;
using GrpcBalance;
using WalletService.Api.Client;

namespace BalanceService.Api.Grpc;

public class BalanceService : GrpcBalance.BalanceService.BalanceServiceBase 
{
    private readonly IBalanceRepository _balanceRepository;
    private readonly IGrpcWalletService _walletService;

    public BalanceService(IBalanceRepository balanceRepository, IGrpcWalletService walletService)
    {
        _balanceRepository = balanceRepository;
        _walletService = walletService;
    }

    public override async Task<CreateBalanceForBudgetResponse> CreateForBudget(CreateBalanceForBudgetRequest request, ServerCallContext context)
    {
        var wallets = await _walletService.GetAllWalletsAsync();

        foreach (var wallet in wallets.Items)
        {
            await _balanceRepository.CreateAsync(new Balance(request.BudgetId, wallet.Id, 0));
        }

        return new CreateBalanceForBudgetResponse
        {
            Items =
            {
                Capacity = 0
            }
        };
    }
}
