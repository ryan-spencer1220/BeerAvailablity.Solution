namespace BeerAvailability.Models
{
  public class Inventory
    {       
        public int InventoryId { get; set; }
        public int BeerId { get; set; }
        public int StoreId { get; set; }
        public virtual Store Store { get; set; }
        public virtual Beer Beer { get; set; }
    }
}