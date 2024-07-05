namespace FinanceApp.Interfaces;

public interface IFMPService
{
    Task<Stock> FindStockBySymbolAsync(string symbol);

}