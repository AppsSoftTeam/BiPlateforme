using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestApp.Models
{
    public class ProfileTypeObj
    {
        [Key, Column(Order = 1)]
        public int ProfilId { get; set; }
        [Key, Column(Order = 2)]
        public int TypeId { get; set; }
        public Profile Profile { get; set; }
        public ObjectifType ObjectifType { get; set; }
    }
}