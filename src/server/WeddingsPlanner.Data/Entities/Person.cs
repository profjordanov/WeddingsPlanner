using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WeddingsPlanner.Data.Enums;

using static WeddingsPlanner.Data.ConstantData;

namespace WeddingsPlanner.Data.Entities
{
    public class Person
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 1)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(1, MinimumLength = 1)]
        public string MiddleNameInitial { get; set; }

        [Required]
        [MinLength(2)]
        public string LastName { get; set; }

        /// <summary>
        /// Full name is concatenation of
        /// first name, middle name initial and last name separated with single space.
        /// </summary>
        [NotMapped]
        public string FullName => 
            $"{FirstName} {MiddleNameInitial} {LastName}";

        public Gender Gender { get; set; }

        public DateTime? Birthdate { get; set; }

        [NotMapped]
        public int? Age
        {
            get
            {
                if (Birthdate == null)
                {
                    return null;
                }

                var now = DateTime.Now;
                var age = now.Year - ((DateTime)Birthdate).Year;

                if (now.Month < ((DateTime)Birthdate).Month ||
                    (now.Month == ((DateTime)Birthdate).Month && now.Day < ((DateTime)Birthdate).Day))
                {
                    age--;
                }

                return age;
            }
        }

        public string Phone { get; set; }

        /// <summary>
        /// Must be in format {user}@{host} where:
        /// - {user} - can contain only alphanumeric characters, ignoring the casing
        /// - {host} - can contain only lowercase latin letters and one dot.
        /// The dot cannot be at the beginning or at the end of the host.
        /// </summary>
        /// <example>
        /// - Valid Emails - john99JJ@abv.bg, john@abv.bg, 9john@abvbg.bg, JJ@abv.com
        /// - Invalid Emails - john.99@abv.bg, john-J@abv.bg, john99@.abv.bg, john99@abvbg., jj<3@a.b.bg
        /// </example>
        [RegularExpression(EmailRegularExpression)]
        public string Email { get; set; }

        public virtual ICollection<Invitation> Invitations { get; set; } = new HashSet<Invitation>();

        public virtual ICollection<Wedding> Brides { get; set; } = new HashSet<Wedding>();

        public virtual ICollection<Wedding> Bridegrooms { get; set; } = new HashSet<Wedding>();
    }
}