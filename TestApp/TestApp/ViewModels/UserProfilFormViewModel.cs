using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestApp.ViewModels
{
    public class UserProfilFormViewModel
    {
        public int UId { get; set; }
        public List<int> Pro { get; set; }

        public string UserDisplayName { get; set; }
        public List<CheckBoxListItem> Profiles { get; set; }
    }
}