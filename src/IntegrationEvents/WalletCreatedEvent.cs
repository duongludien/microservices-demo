namespace IntegrationEvents;

public class WalletCreatedEvent
{
    public WalletCreatedEvent(int walletId)
    {
        WalletId = walletId;
    }

    public int WalletId { get; set; }
}
