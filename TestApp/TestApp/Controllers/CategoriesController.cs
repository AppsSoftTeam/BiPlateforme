using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TestApp.Models;

namespace TestApp.Controllers
{
    public class CategoriesController : Controller
    {
        private ProjectContext db = new ProjectContext();

        // GET: Categories        
        public ActionResult Index()
        {
            var categories = db.Categories.Include(c => c.Direction).ToList();
            ViewBag.Categories = categories;
            ViewBag.DirectionId = new SelectList(db.Directions, "DirectionId", "DirectionName");
            return PartialView("GetCategories", categories);
        }
        public ActionResult Create()
        {
            ViewBag.DirectionId = new SelectList(db.Directions, "DirectionId", "DirectionName");
            return PartialView("CreateCategory");
        }
        
        [HttpPost]
        public ActionResult SaveCategory([Bind(Include = "CategoryId,CategoryName,DirectionId")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DirectionId = new SelectList(db.Directions, "DirectionId", "DirectionName", category.DirectionId);
            return View(category);
        }

        public ActionResult EditCategory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            ViewBag.DirectionId = new SelectList(db.Directions, "DirectionId", "DirectionName", category.DirectionId);
            return PartialView("EditCategoryPartial", category);
        }
        
        [HttpPost]
        public ActionResult SaveEditCategory([Bind(Include = "CategoryId,CategoryName,DirectionId")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DirectionId = new SelectList(db.Directions, "DirectionId", "DirectionName", category.DirectionId);
            return View(category);
        }

        
        public ActionResult DeleteCategory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return PartialView("DeleteCategoryPartial", category);
        }
        
        // POST: Categories/Delete/5
        [HttpPost]
        public ActionResult DeleteCategory(Category cat)
        {
            Category category = db.Categories.Where(x => x.CategoryId == cat.CategoryId).FirstOrDefault();
            db.Categories.Remove(category);
            db.SaveChanges();
            return PartialView("DeleteCategoryPartial", category);
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
