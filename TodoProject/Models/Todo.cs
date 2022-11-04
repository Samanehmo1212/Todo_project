using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TodoProject.Models
{ 
      public enum EnumStatus
    {
        notstarted, ongoing, compleated

    }

public class Todo
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }

        public Guid UserId { get; set; } 
     // [ForeignKey("UserId")]
      //   public virtual User user { get; set; }


        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }

        public EnumStatus Status { get; set; }


        
}
}
