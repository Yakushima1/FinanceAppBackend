using FinanceApp.Data;
using FinanceApp.Dtos.Stock;
using FinanceApp.Helpers;
using FinanceApp.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Repository;

public class StockRepository : IStockRepository
{
    private readonly ApplicationDBContext _context;

    public StockRepository(ApplicationDBContext context)
    {
        _context = context;
    }

    public async Task<List<Stock>> GetAllStockAsync(QueryObject query)
    {
        var stocks = _context.Stocks.Include(c => c.Comments)
            .ThenInclude(a=> a.AppUser).AsQueryable();

        if (!string.IsNullOrWhiteSpace(query.CompanyName))
        {
            stocks = stocks.Where(s => s.CompanyName.Contains(query.CompanyName));
        }

        if (!string.IsNullOrWhiteSpace(query.Symbol))
        {
            stocks = stocks.Where(s => s.Symbol.Contains(query.Symbol));
        }
        if (!string.IsNullOrWhiteSpace(query.SortBy))
        {
            if (query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
            {
                stocks = query.IsDescending  ? stocks.OrderByDescending(s => s.Symbol) : stocks.OrderBy(s => s.Symbol);
            }
        }

        var skipNumber = (query.PageNumber - 1) * query.PageSize;
        
        return await stocks.Skip(skipNumber).Take(query.PageSize).ToListAsync();

    }


    public async Task<Stock?> GetStockByIdAsync(int id)
    {
        return await _context.Stocks.Include(c =>c.Comments).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Stock?> GetStockBySymbolAsync(string symbol)
    {
        return await _context.Stocks.FirstOrDefaultAsync(x => x.Symbol == symbol);
    }

    public async Task<Stock> CreateStockAsync(Stock stock)
    {
        await _context.Stocks.AddAsync(stock);
        await _context.SaveChangesAsync();
        return stock;
    }

    public async Task<Stock?> DeleteStockAsync(int id)
    {
        var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
        if (stock == null)
        {
            return null;
        }
        _context.Stocks.Remove(stock);
        await _context.SaveChangesAsync();
        return stock;
    }

    public async Task<Stock?> UpdateStockAsync(int id, UpdateStockRequestDto updatedStock)
    {
        var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
        if (stock == null)
        {
            return null;
        }
        stock.Symbol = updatedStock.Symbol;
        stock.CompanyName = updatedStock.CompanyName;
        stock.Purchase = updatedStock.Purchase;
        stock.LastDiv = updatedStock.LastDiv;
        stock.Industry = updatedStock.Industry;
        stock.MarketCap = updatedStock.MarketCap;

        await _context.SaveChangesAsync();
        return stock;

    }

    public Task<bool> StockExists(int id)
    {
        return _context.Stocks.AnyAsync(x => x.Id == id);
    }
}