using System.ComponentModel.DataAnnotations;
using WeddingsPlanner.Data.Enums;

namespace WeddingsPlanner.Data.Entities
{
    public class Invitation
    {
        public int Id { get; set; }
        [Required]
        public virtual Wedding Wedding { get; set; }
        [Required]
        public virtual Person Guest { get; set; }
        public virtual Present Present { get; set; }
        public bool IsAttending { get; set; }
        [Required]
        public Family Family { get; set; }
    }
}