using SupplyManagement_NET48.Models;
using SupplyManagement_NET48.Services;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web.Mvc;

namespace SupplyManagement_NET48.Controllers
{
    /*[Authorize]*/
    public class ProjectController : Controller
    {
        private readonly ProjectService _projectService;
        private readonly VendorService _vendorService;

        public ProjectController(ProjectService projectService, VendorService vendorService)
        {
            _projectService = projectService;
            _vendorService = vendorService;
        }

        // GET: Project
        public ActionResult Index()
        {
            var projects = _projectService.Get();
            var vendors = _vendorService.Get();
            ViewData["Vendors"] = vendors;
            var token = Request.Cookies["AuthToken"]?.Value;
            var handler = new JwtSecurityTokenHandler();
            if (token != null)
            {
                var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

                var roleClaim = jsonToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Role || claim.Type == "Role")?.Value;

                ViewBag.UserRole = roleClaim;
            }
            return View(projects);
        }

        // GET: Project/Details/5
        public ActionResult Details(Guid id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var project = _projectService.Get(id);
            if (project == null) return HttpNotFound();
            var vendors = _vendorService.Get();
            ViewData["Vendors"] = vendors;
            return View(project);
        }

        // GET: Project/Create
        public ActionResult Create()
        {
            var vendors = _vendorService.Get();
            ViewData["Vendors"] = vendors;
            return View();
        }

        // POST: Project/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Project project)
        {
            if (ModelState.IsValid)
            {
                _projectService.Create(project);
                return RedirectToAction("Index");
            }
            return View(project);
        }

        // GET: Project/Edit/5
        public ActionResult Edit(Guid id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var project = _projectService.Get(id);
            if (project == null) return HttpNotFound();
            var vendors = _vendorService.Get();
            ViewData["Vendors"] = vendors;
            return View(project);
        }

        // POST: Project/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Project project)
        {
            if (ModelState.IsValid)
            {
                var result = _projectService.Update(project);

                if (result == 1)
                {
                    return RedirectToAction("Index");
                }
                else if (result == 0)
                {
                    return HttpNotFound();
                }
            }
            return View(project);
        }

        // GET: Project/Delete/5
        public ActionResult Delete(Guid id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var project = _projectService.Get(id);
            if (project == null) return HttpNotFound();
            return View(project);
        }

        // POST: Project/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            var result = _projectService.Delete(id);

            if (result == 1)
            {
                return RedirectToAction("Index");
            }
            else if (result == 0)
            {
                return HttpNotFound();
            }
            else
            {
                return View("Error");
            }
        }

        /*protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }*/
    }
}
