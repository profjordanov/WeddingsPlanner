using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingsPlanner.Data.Entities
{
    public class Present
    {
        public int InvitationId { get; set; }
        public virtual Invitation Invitation { get; set; }

        /// <summary>
        /// Person marked as guest in an invitation.
        /// </summary>
        [NotMapped]
        public Person Owner => Invitation.Guest;
    }
}