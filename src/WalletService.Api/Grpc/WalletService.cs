using Grpc.Core;
using GrpcWallet;
using Microsoft.EntityFrameworkCore;
using WalletService.Api.Infrastructure;

namespace WalletService.Api.Grpc;

public class WalletService : Wallet.WalletBase
{
    private readonly WalletDbContext _walletDbContext;

    public WalletService(WalletDbContext walletDbContext)
    {
        _walletDbContext = walletDbContext;
    }

    public override async Task<GetAllWalletsResponse> GetAllWallets(GetAllWalletsRequest request, ServerCallContext context)
    {
        var items = await _walletDbContext.Wallets
            .Select(item => new WalletItemResponse
            {
                Id = item.Id,
                Name = item.Name
            })
            .ToListAsync();
        
        return new GetAllWalletsResponse { Items = { items } };
    }
}
