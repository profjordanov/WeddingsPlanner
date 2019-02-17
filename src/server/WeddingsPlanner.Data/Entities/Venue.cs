using System.ComponentModel.DataAnnotations;

namespace WeddingsPlanner.Data.Entities
{
    public class Venue
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// Number of people who can celebrate in that venue.
        /// </summary>
        public int Capacity { get; set; }

        public string Town { get; set; }
    }
}