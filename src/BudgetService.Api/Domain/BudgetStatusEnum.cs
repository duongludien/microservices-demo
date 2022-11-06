namespace BudgetService.Api.Domain;

public enum BudgetStatusEnum
{
    Undefined = 0,
    Created = 1,
    Active = 2,
    PendingDelete = 3,
    Deleted = 4,
}
