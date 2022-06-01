using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestApp.ViewModels
{
    public class ProfilCategoryFormViewModel
    {
        public int ProId { get; set; }
        public List<int> Cats { get; set; }
        public List<int> Types { get; set; }
        public string ProDisplayName { get; set; }
        public List<CheckBoxListItem> Categories { get; set; }
        public List<CheckBoxListItem> ObjectifTypes { get; set; }
    }
}