using BalanceService.Domain.Models;

namespace BalanceService.Domain.Interfaces;

public interface IBalanceRepository
{
    Task<Balance> CreateAsync(Balance balance);
}
