using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestApp.Models
{
    public class UserProfiles
    {
        [Key, Column(Order = 1)]
        public int UserId { get; set; }
        [Key, Column(Order = 2)]
        public int ProfilId { get; set; }
        public User User { get; set; }
        public Profile Profile { get; set; }
    }
}