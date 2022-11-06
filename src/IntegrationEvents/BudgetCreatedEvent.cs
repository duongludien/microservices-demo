namespace IntegrationEvents;

public class BudgetCreatedEvent
{
    public BudgetCreatedEvent(int budgetId)
    {
        BudgetId = budgetId;
    }

    public int BudgetId { get; set; }
}
