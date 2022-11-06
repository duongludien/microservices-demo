using BudgetService.Api.Domain;
using BudgetService.Api.Dtos;
using BudgetService.Api.Infrastructure;
using IntegrationEvents;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace BudgetService.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class BudgetController : ControllerBase
{
    private readonly BudgetDbContext _budgetDbContext;
    private readonly IBus _bus;

    public BudgetController(BudgetDbContext budgetDbContext, IBus bus)
    {
        _budgetDbContext = budgetDbContext;
        _bus = bus;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateBudgetDto input, CancellationToken cancellationToken)
    {
        var budget = Budget.CreateNew(input.Name);
        
        await _budgetDbContext.Budgets.AddAsync(budget, cancellationToken);
        await _budgetDbContext.SaveChangesAsync(cancellationToken);

        await _bus.Publish(new BudgetCreatedEvent(budget.Id), cancellationToken);
        
        return Ok(budget);
    }
}
