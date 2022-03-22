using System;
using System.Collections.Generic;

namespace BeerAvailability.Models
{
  public class Store
  {
    public Store()
    {
      this.JoinEntities = new HashSet<Inventory>();
    }
    public int StoreId { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public virtual ApplicationUser User { get; set; }
    public virtual ICollection<Inventory> JoinEntities { get; set; }
  }
}