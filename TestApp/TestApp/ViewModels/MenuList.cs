using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestApp.Models;

namespace TestApp.ViewModels
{
    public class MenuList
    {
        public List<Direction> DirectionMenuModel { get; set; }
        public List<Category> CategoryMenuModel { get; set; }
        public List<ObjectifSection> ObjectifMenuModel { get; set; }
        public List<ObjectifType> TypeMenuModel { get; set; }

    }
}