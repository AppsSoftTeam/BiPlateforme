using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TestApp.Models
{
    public class Report
    {
        [Key]
        public int ReportId { get; set; }
        [DisplayName("Rapport")]
        public string ReportName { get; set; }
        [DisplayName("Nom Rapport")]
        public string ReportNameDisplayed { get; set; }
        
        public string Path { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}