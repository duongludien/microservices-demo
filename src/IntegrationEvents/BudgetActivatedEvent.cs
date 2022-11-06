namespace IntegrationEvents;

public class BudgetActivatedEvent
{
    private int _budgetId;

    public int BudgetId => _budgetId;

    private BudgetActivatedEvent(int budgetId)
    {
        _budgetId = budgetId;
    }

    public static BudgetActivatedEvent CreateNew(int budgetId)
    {
        return new BudgetActivatedEvent(budgetId);
    }
}
