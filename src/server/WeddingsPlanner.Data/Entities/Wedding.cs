using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingsPlanner.Data.Entities
{
    public class Wedding
    {
        public Wedding()
        {
        }

        public Wedding(Person bride, Person bridegroom, DateTime date, Agency agency)
        {
            Bride = bride;
            Bridegroom = bridegroom;
            Date = date;
            Agency = agency;
        }

        [Key]
        public int Id { get; set; }

        public int? BrideId { get; set; }
        [ForeignKey("BrideId")]
        public virtual Person Bride { get; set; }

        public int? BridegroomId { get; set; }
        [ForeignKey("BridegroomId")]
        public virtual Person Bridegroom { get; set; }

        /// <summary>
        /// Agency that organizes the wedding.
        /// </summary>
        public int AgencyId { get; set; }
        [ForeignKey("AgencyId")]
        public virtual Agency Agency { get; set; }

        public DateTime Date { get; set; }

        public virtual ICollection<WeddingVenue> WeddingVenues { get; set; } = new HashSet<WeddingVenue>();

        public virtual ICollection<Invitation> Invitations { get; set; } = new HashSet<Invitation>();
    }
}