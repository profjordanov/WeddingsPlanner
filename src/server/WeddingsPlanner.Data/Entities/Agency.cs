using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WeddingsPlanner.Data.Entities
{
    public class Agency
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int EmployeesCount { get; set; }

        [Required]
        public string Town { get; set; }

        public virtual ICollection<Wedding> OrganizedWeddings { get; set; } = new HashSet<Wedding>();
    }
}