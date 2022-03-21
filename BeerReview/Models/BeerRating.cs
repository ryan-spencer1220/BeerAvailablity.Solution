namespace BeerReview.Models
{
  public class BeerRating
    {       
        public int BeerRatingId { get; set; }
        public int BeerId { get; set; }
        public int ReviewId { get; set; }
        public virtual Review Review { get; set; }
        public virtual Beer Beer { get; set; }
    }
}