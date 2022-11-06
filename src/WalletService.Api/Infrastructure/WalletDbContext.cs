using Microsoft.EntityFrameworkCore;
using WalletService.Api.Infrastructure.EntityConfigurations;
using WalletService.Api.Models;

namespace WalletService.Api.Infrastructure;

public class WalletDbContext : DbContext
{
    public DbSet<Wallet> Wallets { get; set; }
    
    public WalletDbContext(DbContextOptions<WalletDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new WalletConfig());
    }
}
