using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestApp.ViewModels
{
    public class ReportInfo
    {
        public int ReportId { get; set; }
        public string ReportName { get; set; }
        public string ReportPath { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Parametername { get; set; }
        public string Parametervalue { get; set; }
    }
}