using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TestApp.ViewModels
{
    public class ReportSelectionVM
    {
        public int ReportId { get; set; }
        [DisplayName("Report Name")]
        public string ReportName { get; set; }
        public string ReportNameDisplayed { get; set; }
        public string Path { get; set; }
        public bool Selected { get; set; }
    }
}