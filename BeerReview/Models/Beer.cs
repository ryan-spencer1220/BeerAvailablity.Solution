using System;
using System.Collections.Generic;

namespace BeerReview.Models
{
  public class Beer
  {
    public Beer()
    {
      this.JoinEntities = new HashSet<BeerRating>();
    }

    public int BeerId { get; set; }
    public string Name { get; set; }
    public double ABV { get; set; }
    public virtual ICollection<BeerRating> JoinEntities { get; set; }
  }
}