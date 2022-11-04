using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TodoProject.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
       
        public string Password { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
    }
}
