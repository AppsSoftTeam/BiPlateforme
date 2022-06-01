using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestApp.Models
{
    public class Gouvernorat
    {
        [Key]
        public int GouvId { get; set; }
        public string GouvName { get; set; }
        public virtual ICollection<ObjectifContrat> ObjectifContrats { get; set; }
    }
}