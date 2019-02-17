using System.ComponentModel.DataAnnotations;

namespace WeddingsPlanner.Core.Models
{
    public class LoginUserModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
