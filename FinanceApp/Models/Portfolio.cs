namespace FinanceApp;

public class Portfolio
{
    public int Id { get; set; }
    public string AppUserId { get; set; }
    public int StockId { get; set; }
    public AppUser AppUser { get; set; }
    public Stock Stock { get; set; }
    
}