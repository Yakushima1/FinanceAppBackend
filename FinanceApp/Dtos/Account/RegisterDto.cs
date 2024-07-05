using System.ComponentModel.DataAnnotations;

namespace FinanceApp.Dtos.Account;

public class RegisterDto
{
    [Required]
    public string? Username { get; set; }
    [Required]
    [EmailAddress]
    public string? EmailAdress { get; set; }
    [Required]
    public string? Password { get; set; }
}