using System.ComponentModel.DataAnnotations;
using WeddingsPlanner.Data.Enums;

namespace WeddingsPlanner.Data.Entities
{
    public class Gift : Present
    {
        /// <summary>
        /// Name of the gift.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Size of the gift can be only Small, Medium or Large or Not Specified.
        /// </summary>
        public PresentSize Size { get; set; }
    }
}