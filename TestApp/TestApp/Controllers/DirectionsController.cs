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
    public class DirectionsController : Controller
    {
        private ProjectContext db = new ProjectContext();

        // GET: Directions
        public ActionResult Index()
        {
            var direction = db.Directions.ToList();
            List<Direction> DirectionList = new List<Direction>();
            foreach (var item in direction)
            {
                DirectionList.Add(new Direction
                {
                    DirectionId = item.DirectionId,
                    DirectionName = item.DirectionName,
                    Categories = GetCategories(item.DirectionId)
                });

            }
            return PartialView("GetDirections", DirectionList);
        }
        public List<Category> GetCategories(int? id)
        {
            Direction direction = db.Directions.Find(id);

            var CategoryList = from C in db.Categories
                               where C.DirectionId == direction.DirectionId
                               select new
                               {
                                   C.CategoryName,
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

        // Post : Directions
        public ActionResult Create()
        {
            return PartialView("CreateDirection");
        }

        [HttpPost]
        public ActionResult SaveDirection([Bind(Include = "DirectionId,DirectionName")] Direction direction)
        {
            if (ModelState.IsValid)
            {
                db.Directions.Add(direction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(direction);
        }


        // Edit : Direction
        public ActionResult EditDirection(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Direction direction = db.Directions.Find(id);
            if (direction == null)
            {
                return HttpNotFound();
            }
            return PartialView("EditDirectionPartial", direction);
        }
        
        [HttpPost]
        public ActionResult SaveEditDirection([Bind(Include = "DirectionId,DirectionName")] Direction direction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(direction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(direction);
        }


        // GET: Directions/Delete/5
        [HttpGet]
        public ActionResult DeleteDirection(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Direction direction = db.Directions.Find(id);
            if (direction == null)
            {
                return HttpNotFound();
            }
            return PartialView("DeleteDirectionPartial", direction);
        }

        // POST: Directions/Delete/5
        [HttpPost]
        public ActionResult SaveDeleteDirection(Direction D)
        {
            Direction direction = db.Directions.Where(x => x.DirectionId == D.DirectionId).FirstOrDefault();
            db.Directions.Remove(direction);
            db.SaveChanges();
            return View(direction);
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
