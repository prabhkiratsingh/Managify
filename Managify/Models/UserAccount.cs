using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Managify.Models
{
    public class UserAccount
    {
        // Specifying enum for the Genders.
        public enum Genders
        {
            Male = 0,
            Female = 1,
            Other = 2
        }

        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please make one selection.")]
        public Genders Gender { get; set; }

        [Required]
        public string Course { get; set; }

        [Required]
        public string Branch { get; set; }

        [Required]
        [Display(Name = "College Name")]
        public string CollegeName { get; set; }

        // Creating a Foreign Key from ApplicationUser table.
        public virtual ApplicationUser User { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        // For displaying the summary of registered events by that user.
        public virtual ICollection<RegisteredEvent> RegisteredEvents { set; get; }
    }
}