using IntegrationEvents;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WalletService.Api.Infrastructure;
using WalletService.Api.Models;

namespace WalletService.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class WalletController : ControllerBase
{
    private readonly WalletDbContext _walletDbContext;
    private readonly IBus _bus;

    public WalletController(WalletDbContext walletDbContext, IBus bus)
    {
        _walletDbContext = walletDbContext;
        _bus = bus;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] Wallet input, CancellationToken cancellationToken)
    {
        var wallet = new Wallet(input.Name);
        await _walletDbContext.AddAsync(wallet, cancellationToken);
        await _walletDbContext.SaveChangesAsync(cancellationToken);
        
        await _bus.Publish(new WalletCreatedEvent(wallet.Id), cancellationToken);

        return Ok(wallet);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        return Ok(await _walletDbContext.Wallets.ToListAsync(cancellationToken));
    }
}
