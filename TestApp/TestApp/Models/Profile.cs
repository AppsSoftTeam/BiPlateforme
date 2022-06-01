using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TestApp.Models
{
    public class Profile
    {

        [Key]
        public int ProfilId { get; set; }
        [Required]
        [DisplayName("Nom")]
        public string ProfilName { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<ObjectifType> ObjectifTypes { get; set; }
    }
}