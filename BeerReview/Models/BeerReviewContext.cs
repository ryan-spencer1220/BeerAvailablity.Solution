using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BeerReview.Models
{
  public class BeerReviewContext : IdentityDbContext<ApplicationUser>
  {
    public DbSet<Beer> Beers { get; set; }
    public DbSet<BeerRating> BeerRatings { get; set; }
    public DbSet<Review> Reviews { get; set; }

    public BeerReviewContext(DbContextOptions options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseLazyLoadingProxies();
    }
  }
}