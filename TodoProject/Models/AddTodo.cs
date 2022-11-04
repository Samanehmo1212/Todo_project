using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TodoProject.Models
{
    public class AddTodo
    {
    

        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }

      //  public DateTime CreatedTime { get; set; }
      //  public DateTime UpdatedTime { get; set; }

        public EnumStatus Status { get; set; }


         //public User user { get; set; }
         //[ForeignKey("UserId")]
        public Guid UserId { get; set; }
      
    }
}
