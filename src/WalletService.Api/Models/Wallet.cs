namespace WalletService.Api.Models;

public class Wallet
{
    public Wallet(string name)
    {
        Name = name;
    }
    
    public int Id { get; set; }

    public string Name { get; set; }
}
