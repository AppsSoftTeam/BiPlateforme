using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestApp.Models
{
    public class Direction
    {
        [Key]
        public int DirectionId { get; set; }
        [Display(Name = "Direction")]
        public string DirectionName { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
    }
    
}