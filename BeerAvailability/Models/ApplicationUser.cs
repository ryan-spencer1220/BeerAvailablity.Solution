using Microsoft.AspNetCore.Identity;

namespace BeerAvailability.Models
{
    public class ApplicationUser : IdentityUser
    {
    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    }
}