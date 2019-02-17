using System.ComponentModel.DataAnnotations;

namespace WeddingsPlanner.Data.Entities
{
    public class Cash : Present
    {
        [Required]
        public decimal Amount { get; set; }
    }
}