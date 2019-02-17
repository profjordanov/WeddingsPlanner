using System.ComponentModel.DataAnnotations;

namespace WeddingsPlanner.Data.Entities
{
    public class WeddingVenue
    {
        [Key]
        public int Id { get; set; }

        public int WeddingId { get; set; }
        public virtual Wedding Wedding { get; set; }

        public int VenueId { get; set; }
        public virtual Venue Venue { get; set; }
    }
}