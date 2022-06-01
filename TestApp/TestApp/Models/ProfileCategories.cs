using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestApp.Models
{
    public class ProfileCategories
    {
        [Key, Column(Order = 1)]
        public int ProfilId { get; set; }
        [Key, Column(Order = 2)]
        public int CategoryId { get; set; }
        public Profile Profile { get; set; }
        public Category Category { get; set; }
    }
}