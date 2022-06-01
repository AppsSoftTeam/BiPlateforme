using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestApp.Models
{
    public class ObjectifType
    {
        [Key]
        public int TypeId { get; set; }
        [Required]
        [Display(Name = " Type")]
        public string TypeName { get; set; }
        [ForeignKey("ObjectifSection")]
        public int SectionId { get; set; }
        public virtual ObjectifSection ObjectifSection { get; set; }
        public virtual ICollection<Profile> Profiles { get; set; }
    }
}