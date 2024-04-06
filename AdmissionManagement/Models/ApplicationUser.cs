using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AdmissionsManagement.Models
{
    public class ApplicationUser : IdentityUser
    {

        [Required(ErrorMessage = "Enter full name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Enter birthdate")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Enter gender type")]
        public string Gender { get; set; }
    }
}
