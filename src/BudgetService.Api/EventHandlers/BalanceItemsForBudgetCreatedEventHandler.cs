using BudgetService.Api.Infrastructure;
using IntegrationEvents;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace BudgetService.Api.EventHandlers;

public class BalanceItemsForBudgetCreatedEventHandler : IConsumer<BalanceItemsForBudgetCreatedEvent>
{
    private readonly BudgetDbContext _budgetDbContext;
    private readonly IBus _bus;

    public BalanceItemsForBudgetCreatedEventHandler(BudgetDbContext budgetDbContext, IBus bus)
    {
        _budgetDbContext = budgetDbContext;
        _bus = bus;
    }

    public async Task Consume(ConsumeContext<BalanceItemsForBudgetCreatedEvent> context)
    {
        var budget = await _budgetDbContext.Budgets.SingleOrDefaultAsync(b => b.Id == context.Message.BudgetId);

        if (budget != null)
        {
            budget.Activate();
            await _budgetDbContext.SaveChangesAsync();
            await _bus.Publish(BudgetActivatedEvent.CreateNew(budget.Id));
        }
    }
}
