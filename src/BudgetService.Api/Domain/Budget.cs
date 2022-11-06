namespace BudgetService.Api.Domain;

public class Budget
{
    private int _id;

    public int Id
    {
        get => _id;
        // For EF Core 
        set => _id = value;
    }

    private string _name;
    
    public string Name => _name;

    private BudgetStatusEnum _status;
    
    public BudgetStatusEnum Status => _status;

    private Budget(string name)
    {
        _name = name;
    }

    public static Budget CreateNew(string name)
    {
        return new Budget(name)
        {
            _status = BudgetStatusEnum.Created
        };
    }

    public void Activate()
    {
        _status = BudgetStatusEnum.Active;
    }
}
