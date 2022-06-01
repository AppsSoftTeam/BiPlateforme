using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TestApp.Models
{
    public class ObjectifContrat
    {
        [Key]
        public int ObjId { get; set; }
        public string Month { get; set; }
        public int Year { get; set; }
        public int NbreContrat { get; set; }
        [ForeignKey("Gouvernorat")]
        public int GouvId { get; set; }
        public virtual Gouvernorat Gouvernorat { get; set; }
    }
}