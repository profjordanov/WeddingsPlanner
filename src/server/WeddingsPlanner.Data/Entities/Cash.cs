using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingsPlanner.Data.Entities
{
    [Table(nameof(Cash))]
    public class Cash : Present
    {
        [Required]
        public decimal Amount { get; set; }
    }
}