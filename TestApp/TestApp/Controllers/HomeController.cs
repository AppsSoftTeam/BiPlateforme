using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TestApp.Models;
using TestApp.ViewModels;

namespace TestApp.Controllers
{
    public class HomeController : Controller
    {
        private ProjectContext db = new ProjectContext();
        [ChildActionOnly]
        public ActionResult SideBar()
        {
            MenuList menuList = new MenuList();
            menuList.DirectionMenuModel = new List<Direction>();
            menuList.DirectionMenuModel = GetMainMenu();
            menuList.CategoryMenuModel = new List<Category>();
            menuList.CategoryMenuModel = GetSubMenu();
            /*menuList.ObjectifMenuModel = new List<ObjectifSection>();
            menuList.ObjectifMenuModel = GetObjectifMenu();
            menuList.TypeMenuModel = new List<ObjectifType>();
            menuList.TypeMenuModel = GetTypeMenu();*/

            return PartialView("_SideBar", menuList);
        }
        public List<Direction> GetMainMenu()
        {
            var pmenus = (from p in db.Directions

                          select new
                          {
                              p.DirectionId,
                              p.DirectionName

                          }).ToList();

            List<Direction> MainMenuList = new List<Direction>();

            pmenus.ToList().ForEach(rec =>
            {
                MainMenuList.Add(new Direction
                {
                    DirectionId = rec.DirectionId,
                    DirectionName = rec.DirectionName,

                });
            });

            return MainMenuList;
        }
        public List<Category> GetSubMenu()
        {
            string username = System.Web.HttpContext.Current.User.Identity.Name;

            var smenus = from u in db.Users
                         from up in db.User_Profils
                         from p in db.Profiles
                         from pr in db.Profil_Roles
                         from s in db.Categories
                         where (u.UserName == username && up.UserId == u.UserId && p.ProfilId == up.ProfilId && pr.ProfilId == p.ProfilId && s.CategoryId == pr.CategoryId)
                         select new
                         {
                             s.DirectionId,
                             s.CategoryId,
                             s.CategoryName,
                             s.Direction.DirectionName
                         };
            List<Category> SubMenuList = new List<Category>();

            smenus.ToList().ForEach(rec =>
            {
                SubMenuList.Add(new Category
                {
                    DirectionId = rec.DirectionId,
                    CategoryId = rec.CategoryId,
                    CategoryName = rec.CategoryName
                });
            });
            return SubMenuList;
        }
        public PartialViewResult ReportByCategory(int? id, string reportname)
        {
           /* if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }*/
            Category category = db.Categories.Find(id);
           /* if (category == null)
            {
                return HttpNotFound();
            }*/
            var reports = db.Reports.Where(i => i.CategoryId == id);
            if (!String.IsNullOrEmpty(reportname))
            {
                reports = reports.Where(r => r.ReportName.Contains(reportname));
            }

            return PartialView("GetReportByCategory", reports.ToList());
        }
        public PartialViewResult DashboardByCategory(int? id, string dashboardname)
        {
           /* if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }*/
            Category category = db.Categories.Find(id);
            /*if (category == null)
            {
                return HttpNotFound();
            }*/
            var dashboards = db.Dashboards.Where(i => i.CategoryId == id);
            if (!String.IsNullOrEmpty(dashboardname))
            {
                dashboards = dashboards.Where(r => r.DashboardName.Contains(dashboardname));
            }
            return PartialView("GetDashboardByCategory", dashboards.ToList());
        }
        public ActionResult Index()
        {
            return View();
        }

    }
}