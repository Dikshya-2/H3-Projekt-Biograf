using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biograf.Repo.DTOs
{
    public class RegistrationUser
    {
        
        [Required]
        [StringLength(128, ErrorMessage = "Email must be less than 128 chars")]
        public string Email { get; set; }

        [Required]
        [StringLength(62, ErrorMessage = "Password must be less than 62 chars")]
        public string Password { get; set; }

        [Required]
        [StringLength(32, ErrorMessage = "FirstName must be less than 32 chars")]
        public string FullName { get; set; }

        [Required]
        [StringLength(52, ErrorMessage = "Address must be less than 52 chars")]
        public string Address { get; set; }
        public int Phone { get; set; } 
    }
}
    

