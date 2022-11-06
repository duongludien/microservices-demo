using GrpcBalance;

namespace BalanceService.Api.Client;

public class GrpcBalanceService : IGrpcBalanceService
{
    private readonly GrpcBalance.BalanceService.BalanceServiceClient _balanceServiceClient;

    public GrpcBalanceService(GrpcBalance.BalanceService.BalanceServiceClient balanceServiceClient)
    {
        _balanceServiceClient = balanceServiceClient;
    }

    public async Task<CreateBalanceForBudgetResponse> CreateForBudgetAsync(int budgetId)
    { 
        return await _balanceServiceClient.CreateForBudgetAsync(new CreateBalanceForBudgetRequest { BudgetId = budgetId });
    }
}
