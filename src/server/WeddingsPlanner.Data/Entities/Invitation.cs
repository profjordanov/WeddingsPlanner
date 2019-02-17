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

        /// <summary>
        /// Can be any person beside the bride or bridegroom.
        /// </summary>
        [Required]
        public int GuestId { get; set; }
        public virtual Person Guest { get; set; }

        /// <summary>
        /// Can be either <see cref="Cash"/> or <see cref="Gift"/>.
        /// </summary>
        public int PresentId { get; set; }
        public virtual Present Present { get; set; }

        /// <summary>
        /// Info about whether the guest accepted
        /// the invitation to attend at the wedding or not.
        /// </summary>
        public bool IsAttending { get; set; }

        /// <summary>
        /// Family from which the guest is coming from.
        /// Can be only from Bride’s family or Bridegroom’s family.
        /// </summary>
        [Required]
        public Family Family { get; set; }
    }
}