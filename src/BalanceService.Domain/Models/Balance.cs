namespace BalanceService.Domain.Models;

public class Balance
{
    public int Id { get; set; }
    
    public int BudgetId { get; set; }

    public int WalletId { get; set; }

    public long Amount { get; set; }

    public Balance(int budgetId, int walletId, long amount)
    {
        BudgetId = budgetId;
        WalletId = walletId;
        Amount = amount;
    }
}
