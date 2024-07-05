using FinanceApp.Dtos.Stock;
using FinanceApp.Helpers;

namespace FinanceApp.Interfaces;

public interface IStockRepository
{
    Task<List<Stock>> GetAllStockAsync(QueryObject query);
    Task<Stock?> GetStockByIdAsync(int id);
    Task<Stock?> GetStockBySymbolAsync(string symbol);
    Task<Stock> CreateStockAsync(Stock stock);
    Task<Stock?> DeleteStockAsync(int id);
    Task<Stock?> UpdateStockAsync(int id, UpdateStockRequestDto updatedStock);
    Task<bool> StockExists(int id);
}