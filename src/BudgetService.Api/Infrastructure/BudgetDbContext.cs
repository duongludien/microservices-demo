using BudgetService.Api.Domain;
using BudgetService.Api.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace BudgetService.Api.Infrastructure;

public class BudgetDbContext : DbContext
{
    public DbSet<Budget> Budgets { get; set; }

    public BudgetDbContext(DbContextOptions<BudgetDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new BudgetConfig());
    }
}
