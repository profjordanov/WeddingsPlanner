using System.ComponentModel.DataAnnotations;
using WeddingsPlanner.Data.Enums;

namespace WeddingsPlanner.Data.Entities
{
    public class Invitation
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Wedding where the guest were invited.
        /// </summary>
        [Required]
        public int WeddingId { get; set; }
        public virtual Wedding Wedding { get; set; }

        [Required]
        public int GuestId { get; set; }
        public virtual Person Guest { get; set; }

        public int PresentId { get; set; }
        public virtual Present Present { get; set; }

        public bool IsAttending { get; set; }

        [Required]
        public Family Family { get; set; }
    }
}