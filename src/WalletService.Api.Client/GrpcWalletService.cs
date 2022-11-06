using GrpcWallet;
using Microsoft.Extensions.Logging;

namespace WalletService.Api.Client;

public class GrpcWalletService : IGrpcWalletService
{
    private readonly Wallet.WalletClient _walletClient;
    private readonly ILogger<GrpcWalletService> _logger;

    public GrpcWalletService(Wallet.WalletClient walletClient, ILogger<GrpcWalletService> logger)
    {
        _walletClient = walletClient;
        _logger = logger;
    }

    public async Task<GetAllWalletsResponse> GetAllWalletsAsync()
    {
        _logger.LogDebug("gRPC: get all wallets");
        return await _walletClient.GetAllWalletsAsync(new GetAllWalletsRequest());
    }
}
