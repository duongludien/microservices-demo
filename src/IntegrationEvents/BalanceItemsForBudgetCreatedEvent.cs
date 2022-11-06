namespace IntegrationEvents;

public class BalanceItemsForBudgetCreatedEvent
{
    public int BudgetId { get; set; }

    public ICollection<int> BalanceItemIds { get; set; }

    public BalanceItemsForBudgetCreatedEvent()
    {
    }

    private BalanceItemsForBudgetCreatedEvent(int budgetId)
    {
        BudgetId = budgetId;
        BalanceItemIds = new List<int>();
    }

    private BalanceItemsForBudgetCreatedEvent(int budgetId, ICollection<int>? balanceItemIds) : this(budgetId)
    {
        if (balanceItemIds != null && balanceItemIds.Any())
        {
            BalanceItemIds = balanceItemIds;
        }
        else
        {
            BalanceItemIds = new List<int>();
        }
    }

    public static BalanceItemsForBudgetCreatedEvent CreateNew(int budgetId, ICollection<int>? balanceItemIds)
    {
        return new BalanceItemsForBudgetCreatedEvent(budgetId, balanceItemIds);
    }
}
