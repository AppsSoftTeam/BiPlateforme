using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestApp.Models
{
    public class ObjectifSection
    {
        [Key]
        public int SectionId { get; set; }
        [Display(Name = "Section")]
        public string SectionName { get; set; }
        public virtual ICollection<ObjectifType> ObjectifTypes { get; set; }


    }
}