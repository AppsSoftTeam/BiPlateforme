using Microsoft.Ajax.Utilities;
using Microsoft.Rest;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TestApp.Models;
using TestApp.Services;
using TestApp.ViewModels;

namespace TestApp.Controllers
{
    public class DashboardsController : Controller
    {
        private ProjectContext db = new ProjectContext();

        // GET: Dashboards
        public ActionResult Index()
        {
            var dashboards = db.Dashboards.Include(d => d.Category);
            return PartialView("GetDashboards", dashboards.ToList());
        }

        public ActionResult CreatePartialView()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            return PartialView();
        }
        [HttpPost]
        public ActionResult SaveDashboard([Bind(Include = "DashboardName,WorkSpaceid,Reportid,CategoryId")] Dashboard dashboard)
        {
            if (ModelState.IsValid)
            {
                db.Dashboards.Add(dashboard);
                db.SaveChanges();
                
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", dashboard.CategoryId);
            return View();
        }

        public ActionResult EditDashboard(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dashboard dashboard = db.Dashboards.Find(id);
            if (dashboard == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", dashboard.CategoryId);
            return PartialView("EditDashboardPartial", dashboard);
        }

        [HttpPost]
        public ActionResult SaveEditDashboard([Bind(Include = "DashboardId,DashboardName,WorkSpaceid,Reportid,CategoryId")] Dashboard dashboard)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dashboard).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", dashboard.CategoryId);
            return View();
        }

        [HttpGet]
        public ActionResult DeleteDashboard(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dashboard dashboard = db.Dashboards.Find(id);
            if (dashboard == null)
            {
                return HttpNotFound();
            }
            return PartialView("DeleteDashboardPartial", dashboard);
        }
        // POST: Dashboards/Delete/5
        [HttpPost]
        public ActionResult SaveDeleteDashboard(Dashboard dash)
        {
            Dashboard dashboard = db.Dashboards.Where(x => x.DashboardId == dash.DashboardId).FirstOrDefault();
            db.Dashboards.Remove(dashboard);
            db.SaveChanges();
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private string m_errorMessage;
       
        public DashboardsController()
        {
            m_errorMessage = ConfigValidatorService.GetWebConfigErrors();
        }
        public async Task<PartialViewResult> EmbedReport(int? id)
        {
            if (!m_errorMessage.IsNullOrWhiteSpace())
            {
                return PartialView("Error", BuildErrorModel(m_errorMessage));
            }

            try
            {
                /*if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }*/
                Dashboard dashboard = db.Dashboards.Find(id);
                /*if (dashboard == null)
                {
                    return HttpNotFound();
                }*/

                var embedResult = await EmbedService.GetEmbedParams(new Guid(dashboard.WorkSpaceid), new Guid(dashboard.Reportid));
                return PartialView("ViewThisDashboard", embedResult);
            }
            catch (HttpOperationException exc)
            {
                m_errorMessage = string.Format("Status: {0} ({1})\r\nResponse: {2}\r\nRequestId: {3}", exc.Response.StatusCode, (int)exc.Response.StatusCode, exc.Response.Content, exc.Response.Headers["RequestId"].FirstOrDefault());
                return PartialView("Error", m_errorMessage);
            }
            catch (Exception ex)
            {
                return PartialView("Error", ex.Message);
            }
        }
        private ErrorModel BuildErrorModel(string errorMessage)
        {
            return new ErrorModel
            {
                ErrorMessage = errorMessage
            };
        }
    }
}
