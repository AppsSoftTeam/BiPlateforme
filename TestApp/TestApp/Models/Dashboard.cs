using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestApp.Models
{
    public class Dashboard
    {
        [Key]
        public int DashboardId { get; set; }
        
        [DisplayName("Dashboard")]
        public string DashboardName { get; set; }
        [Required(ErrorMessage = "Work Space Required")]
        public string WorkSpaceid { get; set; }
        [Required(ErrorMessage = "Report id Required")]
        public string Reportid { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}