using System.ComponentModel.DataAnnotations;

namespace TodoProject.Models
{
    public class AddUser
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
       
    }
}
