using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TestApp.Models;
using TestApp.ViewModels;

namespace TestApp.Controllers
{
    public class ProfilesController : Controller
    {
        private ProjectContext db = new ProjectContext();

        // GET: Profiles
        public ActionResult Index()
        {
            var profile = db.Profiles.ToList();
            List<Profile> ProfileList = new List<Profile>();
            foreach (var item in profile)
            {
                ProfileList.Add(new Profile
                {
                    ProfilId = item.ProfilId,
                    ProfilName = item.ProfilName,
                    Categories = GetCategories(item.ProfilId)
                });

            }
            return PartialView("GetProfiles", ProfileList);
        }

        public List<Category> GetCategories(int? id)
        {
            Profile profile = db.Profiles.Find(id);

            var CategoryList = from C in db.Profil_Roles
                               where C.ProfilId == profile.ProfilId
                               select new
                               {
                                   C.Category.CategoryName,
                                   C.CategoryId
                               };
            List<Category> res = new List<Category>();

            CategoryList.ToList().ForEach(rec =>
            {
                res.Add(new Category
                {
                    CategoryId = rec.CategoryId,
                    CategoryName = rec.CategoryName,
                });
            });
            return res;
        }

        public ActionResult CreatePartialView()
        {

            ViewBag.Categories = db.Categories;

            return PartialView();
        }

        [HttpPost]
        public ActionResult SaveProfile([Bind(Include = "ProfilId,ProfilName")] Profile profile, int[] CategoryId)
        {
            if (ModelState.IsValid)
            {
                db.Profiles.Add(profile);
                foreach (int catId in CategoryId)
                {
                    ProfileCategories profileRole = new ProfileCategories();

                    profileRole.ProfilId = profile.ProfilId;
                    profileRole.CategoryId = catId;
                    db.Profil_Roles.Add(profileRole);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View(profile);
        }

        public ActionResult EditProfile(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profile profile = db.Profiles.SingleOrDefault(c => c.ProfilId == id);
            if (profile == null)
            {
                return HttpNotFound();
            }
            var categoryList = db.Categories.ToList();
            var objectifTypeList = db.ObjectifTypes.ToList();

            // Get the selected category ID's for the profile
            IEnumerable<int> selectedCategories = db.Profil_Roles
                .Where(x => x.ProfilId == id).Select(x => x.CategoryId);

            // Get the selected Types ID's for the profile
            IEnumerable<int> selectedTypes = db.ProfileTypeObjs
                .Where(x => x.ProfilId == id).Select(x => x.TypeId);
            
            // Build view model
            var model = new ProfilCategoryFormViewModel()
            {
                ProId = profile.ProfilId,
                ProDisplayName = profile.ProfilName,
                Categories = categoryList.Select(x => new CheckBoxListItem()
                {
                    ID = x.CategoryId,
                    Display = x.CategoryName,
                    IsChecked = selectedCategories.Contains(x.CategoryId)
                }).ToList(),
                ObjectifTypes = objectifTypeList.Select(x => new CheckBoxListItem()
                {
                    ID = x.TypeId,
                    Display = x.TypeName,
                    IsChecked = selectedTypes.Contains(x.TypeId),
                }).ToList()
            };
            return PartialView("EditProfilePartial", model);

        }
        public JsonResult SaveChange(ProfilCategoryFormViewModel vmEdit)
        {

            if (ModelState.IsValid)
            {
                Profile profile = db.Profiles.SingleOrDefault(c => c.ProfilId == vmEdit.ProId);
                db.Profil_Roles.RemoveRange(db.Profil_Roles.Where(x => x.ProfilId == vmEdit.ProId));
                db.ProfileTypeObjs.RemoveRange(db.ProfileTypeObjs.Where(x => x.ProfilId == vmEdit.ProId));

                // Add new selections
                foreach (int categoryId in vmEdit.Cats)
                {
                    ProfileCategories profileCategory = new ProfileCategories
                    {
                        ProfilId = vmEdit.ProId,
                        CategoryId = categoryId
                    };
                    db.Profil_Roles.Add(profileCategory);
                }
                foreach (int typeId in vmEdit.Types)
                {
                    ProfileTypeObj profileType = new ProfileTypeObj
                    {
                        ProfilId = vmEdit.ProId,
                        TypeId = typeId
                    };
                    db.ProfileTypeObjs.Add(profileType);
                }


                profile.ProfilName = vmEdit.ProDisplayName;
                db.Entry(profile).State = EntityState.Modified;
                // Save and redirect
                db.SaveChanges();

            }
            string data = "success";
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult DeleteProfile(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profile profile = db.Profiles.Find(id);
            if (profile == null)
            {
                return HttpNotFound();
            }
            return PartialView("DeleteProfilePartial", profile);
        }

        // POST: Profiles/Delete/5
        [HttpPost]
        public ActionResult SaveDeleteProfile(Profile pro)
        {
            Profile profile = db.Profiles.Where(x => x.ProfilId == pro.ProfilId).FirstOrDefault();
            db.Profiles.Remove(profile);
            db.SaveChanges();
            return PartialView("DeleteProfilePartial", profile);
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
