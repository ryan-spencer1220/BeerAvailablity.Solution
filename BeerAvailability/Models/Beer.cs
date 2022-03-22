using System;
using System.Collections.Generic;

namespace BeerAvailability.Models
{
  public class Beer
  {
    public Beer()
    {
      this.JoinEntities = new HashSet<Inventory>();
    }

    public int BeerId { get; set; }
    public string Name { get; set; }
    public double ABV { get; set; }
    public string PackageType { get; set; }
    public virtual ICollection<Inventory> JoinEntities { get; set; }
  }
}