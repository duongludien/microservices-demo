using GrpcWallet;

namespace WalletService.Api.Client;

public interface IGrpcWalletService
{
    Task<GetAllWalletsResponse> GetAllWalletsAsync();
}
