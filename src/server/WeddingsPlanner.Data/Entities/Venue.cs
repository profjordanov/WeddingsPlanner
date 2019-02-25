using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WeddingsPlanner.Data.Entities
{
    public class Venue
    {
        public Venue(string name, int capacity, string town)
        {
            Name = name;
            Capacity = capacity;
            Town = town;
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// Number of people who can celebrate in that venue.
        /// </summary>
        public int Capacity { get; set; }

        public string Town { get; set; }

        public virtual ICollection<WeddingVenue> WeddingVenues { get; set; } = new HashSet<WeddingVenue>();
    }
}