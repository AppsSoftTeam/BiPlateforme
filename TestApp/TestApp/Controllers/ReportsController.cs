using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TestApp.Models;
using TestApp.Utils;
using TestApp.ViewModels;

namespace TestApp.Controllers
{
    public class ReportsController : Controller
    {
        private ProjectContext db = new ProjectContext();
        SSRSConnection _utils = new SSRSConnection();
        // GET: Test
        public ActionResult Index()
        {
            var reports = db.Reports.Include(r => r.Category);

            return PartialView("GetReports", reports);
        }
        public ActionResult GetFolders()
        {
            List<FoldersVM> L = new List<FoldersVM>();
            L = _utils.GetListFolders();
            return PartialView("GetFoldersFromSSRS", L);
        }
        public ActionResult GetReportsSSRS(string Foldername)
        {
            List<ReportSelectionVM> L = new List<ReportSelectionVM>();
            var reports = db.Reports.ToList();
            L = _utils.GetListReports(Foldername);

            var Result = L.Where(x => !reports.Any(z => z.ReportName == x.ReportName)).ToList();

            ViewBag.CategoryId = db.Categories;
            return View("GetReportsFromSSRS", Result);
        }
        public ActionResult test(string Foldername)
        {
            
            return PartialView("GetReportsFromSSRS");
        }
        /*[HttpPost]
        public ActionResult GetReports(List<ReportSelectionVM> items, int CategoryId)
        {
            //ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", r.CategoryId);
            foreach (ReportSelectionVM item in items)
            {
                if (item.Selected == true)
                {
                    var report = new Models.Report();
                    report.ReportName = item.ReportName;
                    report.ReportNameDisplayed = item.ReportNameDisplayed;
                    report.Path = item.Path;
                    report.CategoryId = CategoryId;
                    db.Reports.Add(report);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.MyMessage("select one user at least");
            return View();
        }
        */
        public ActionResult EditReport(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Report report = db.Reports.Find(id);
            if (report == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", report.CategoryId);
            return PartialView("EditReportPartial", report);
        }
        [HttpPost]
        public JsonResult SaveReportChanges(Models.Report report)
        {

            if (ModelState.IsValid)
            {
                Models.Report R = db.Reports.SingleOrDefault(r => r.ReportId == report.ReportId);
                R.ReportId = report.ReportId;
                R.ReportNameDisplayed = report.ReportNameDisplayed;
                R.CategoryId = report.CategoryId;
                db.Entry(R).State = EntityState.Modified;
                // Save and redirect
                db.SaveChanges();

            }
            string data = "success";
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult DeleteReport(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Report report = db.Reports.Find(id);
            if (report == null)
            {
                return HttpNotFound();
            }
            return PartialView("DeleteReportPartial", report);
        }
        // POST: Reports/Delete/5
        [HttpPost]
        public ActionResult DeleteReport(Models.Report R)
        {
            Models.Report report = db.Reports.Where(x => x.ReportId == R.ReportId).FirstOrDefault();
            db.Reports.Remove(report);
            db.SaveChanges();
            return PartialView("DeleteReportPartial", report);
        }
        public PartialViewResult ViewReport(int? id)
        {
            int Width = 100;
            int Height = 650;
            var report = db.Reports.Find(id);
            var rptInfo = new ReportInfo();
            rptInfo.ReportName = report.ReportName;
            rptInfo.ReportPath = String.Format("../../Reports/ReportTemplate.aspx?ReportName={0}&Height={1}", report.Path, Height);
            rptInfo.Width = Width;
            rptInfo.Height = Height;
            return PartialView("ViewThisReport", rptInfo);
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
