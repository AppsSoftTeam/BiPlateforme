using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TestApp.ViewModels
{
    public class FoldersVM
    {
        public int Id { get; set; }
        [DisplayName("Folder Name")]
        public string FolderName { get; set; }
        public string Path { get; set; }

    }
}