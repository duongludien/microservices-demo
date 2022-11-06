using GrpcBalance;

namespace BalanceService.Api.Client;

public interface IGrpcBalanceService
{
    Task<CreateBalanceForBudgetResponse> CreateForBudgetAsync(int budgetId);
}
