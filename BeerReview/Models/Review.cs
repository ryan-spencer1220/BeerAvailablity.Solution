using System;
using System.Collections.Generic;

namespace BeerReview.Models
{
  public class Review
  {
    public Review()
    {
      this.JoinEntities = new HashSet<BeerRating>();
    }
    public int ReviewId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public virtual ApplicationUser User { get; set; }
    public virtual ICollection<BeerRating> JoinEntities { get; set; }
  }
}