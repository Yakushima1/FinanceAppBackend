using Microsoft.AspNetCore.Identity;

namespace FinanceApp;

public class AppUser : IdentityUser 
{
    public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>();

}