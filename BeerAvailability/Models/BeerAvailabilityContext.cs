using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BeerAvailability.Models
{
  public class BeerAvailabilityContext : IdentityDbContext<ApplicationUser>
  {
    public DbSet<Beer> Beers { get; set; }
    public DbSet<Inventory> Inventory { get; set; }
    public DbSet<Store> Stores { get; set; }

    public BeerAvailabilityContext(DbContextOptions options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseLazyLoadingProxies();
    }
  }
}