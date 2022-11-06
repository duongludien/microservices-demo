using System.Data;
using BalanceService.Domain.Interfaces;
using BalanceService.Domain.Models;
using Dapper;

namespace BalanceService.Infrastructure.Repositories;

public class BalanceRepository : IBalanceRepository
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public BalanceRepository(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Balance> CreateAsync(Balance balance)
    {
        var connection = _sqlConnectionFactory.GetOpenConnection();
        
        const string sql = @"
            INSERT INTO Balance (BudgetId, WalletId, Amount) 
            VALUES (@BudgetId, @WalletId, @Amount); 
            SELECT LAST_INSERT_ID();";

        var id = await connection.QuerySingleAsync<int>(
            sql,
            new
            {
                balance.BudgetId,
                balance.WalletId,
                balance.Amount
            }, 
            commandType: CommandType.Text);

        balance.Id = id;
        return balance;
    }
}
