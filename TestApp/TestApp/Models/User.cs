using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace TestApp.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [DisplayName("Nom utilisateur")]
        public string UserName { get; set; }
        [DisplayName("Nom")]
        public string DisplayName { get; set; }
        public virtual ICollection<Profile> Profiles { get; set; }
    }
}