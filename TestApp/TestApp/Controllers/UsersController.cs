using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TestApp.Models;
using TestApp.Utils;
using TestApp.ViewModels;

namespace TestApp.Controllers
{
    public class UsersController : Controller
    {
        private ProjectContext db = new ProjectContext();
        LDAPconnection _utils = new LDAPconnection();
        // GET: Users
        public ActionResult Index()
        {
            var user = db.Users.ToList();
            List<User> UserList = new List<User>();
            foreach (var item in user)
            {
                UserList.Add(new User
                {
                    UserId = item.UserId,
                    UserName = item.UserName,
                    DisplayName = item.DisplayName,
                    Profiles = GetProfiles(item.UserId)
                });

            }
            ViewBag.Profiles = db.Profiles;
            return PartialView("GetUsers", UserList);
        }
        public List<Profile> GetProfiles(int? id)
        {

            User user = db.Users.Find(id);
            var ProfilesList = from P in db.User_Profils
                               where P.UserId == user.UserId
                               select new
                               {
                                   P.Profile.ProfilName,
                                   P.ProfilId
                               };
            List<Profile> res = new List<Profile>();

            ProfilesList.ToList().ForEach(rec =>
            {
                res.Add(new Profile
                {
                    ProfilId = rec.ProfilId,
                    ProfilName = rec.ProfilName,
                });
            });
            if (res.Count == 0)
            {
                res.Add(new Profile
                {
                    ProfilName = "No assigned profile"
                });

            }
            return res;
        }
        [HttpGet]
        public ActionResult GetUsersFromAD()
        {
            List<UserSelectionVM> L = new List<UserSelectionVM>();
            L = _utils.GetListUsers();

            /*
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            if (!String.IsNullOrEmpty(searchString))
            {
                L = L.Where(x => x.UserName.Contains(searchString) || x.DisplayName.Contains(searchString)).ToList();

            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);*/
            return PartialView(L.ToList());
        }
        [HttpPost]
        public ActionResult SaveUsersFromAD(List<UserSelectionVM> items)
        {
            foreach (UserSelectionVM item in items)
            {
                if (item.Selected == true)
                {
                    var user = new User();
                    user.UserName = item.UserName;
                    user.DisplayName = item.DisplayName;
                    db.Users.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.MyMessage("select one user at least");
            
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AssignProfilePartial(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.SingleOrDefault(c => c.UserId == id);
            if (user == null)
            {
                return HttpNotFound();
            }
            var profileList = db.Profiles.ToList();
            // Get the selected profile ID's for the profile
            IEnumerable<int> selectedProfiles = db.User_Profils
                .Where(x => x.UserId == id).Select(x => x.ProfilId);
            // Build view model
            var model = new UserProfilFormViewModel()
            {
                UId = user.UserId,
                UserDisplayName = user.UserName,
                Profiles = profileList.Select(x => new CheckBoxListItem()
                {
                    ID = x.ProfilId,
                    Display = x.ProfilName,
                    IsChecked = selectedProfiles.Contains(x.ProfilId)
                }).ToList(),

            };

            return PartialView("AssignProfile", model);
        }
        [HttpPost]
        public JsonResult SaveChange(UserProfilFormViewModel vmEdit)
        {
            if (ModelState.IsValid)
            {
                User user = db.Users.SingleOrDefault(c => c.UserId == vmEdit.UId);
                db.User_Profils.RemoveRange(db.User_Profils.Where(x => x.UserId == vmEdit.UId));

                // Add new selections
                foreach (int profileId in vmEdit.Pro)
                {
                    UserProfiles userprofile = new UserProfiles
                    {
                        UserId = vmEdit.UId,
                        ProfilId = profileId
                    };
                    db.User_Profils.Add(userprofile);
                }

                user.UserName = vmEdit.UserDisplayName;
                db.Entry(user).State = EntityState.Modified;
                // Save and redirect
                db.SaveChanges();

            }
            string data = "success";
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteUser(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return PartialView("DeleteUserPartial", user);
        }

        // POST: Profiles/Delete/5
        [HttpPost]
        public ActionResult SaveDeleteUser(User U)
        {
            User user = db.Users.Where(x => x.UserId == U.UserId).FirstOrDefault();
            db.Users.Remove(user);
            db.SaveChanges();
            return PartialView("DeleteUserPartial", user);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
