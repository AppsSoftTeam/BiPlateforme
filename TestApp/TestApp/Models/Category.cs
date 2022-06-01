using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TestApp.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        [Display(Name = " Categorie")]
        public string CategoryName { get; set; }
        [ForeignKey("Direction")]
        public int DirectionId { get; set; }
        public virtual Direction Direction { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
        public virtual ICollection<Dashboard> Dashboards { get; set; }
        public virtual ICollection<Profile> Profiles { get; set; }
    }
}