using System.Security.Claims;

namespace FinanceApp.Extensions;

public static class ClaimExtensions
{
    public static string GetUsername(this ClaimsPrincipal user)
    {
        return user.Claims.SingleOrDefault(x=> x.Type.Equals("").Equals("https://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname")).Value;
    }
    
}