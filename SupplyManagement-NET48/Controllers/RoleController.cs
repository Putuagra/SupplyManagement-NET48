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
    public class RoleController : Controller
    {
        private readonly RoleService _roleService;

        public RoleController(RoleService roleService)
        {
            _roleService = roleService;
        }

        // GET: Role
        public ActionResult Index()
        {
            var roles = _roleService.Get();
            var token = Request.Cookies["AuthToken"]?.Value;
            var handler = new JwtSecurityTokenHandler();
            if (token != null)
            {
                var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

                var roleClaim = jsonToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Role || claim.Type == "Role")?.Value;

                ViewBag.UserRole = roleClaim;
            }
            return View(roles);
        }

        // GET: Role/Details/5
        public ActionResult Details(Guid id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var role = _roleService.Get(id);
            if (role == null) return HttpNotFound();
            return View(role);
        }

        // GET: Role/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Role/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Role role)
        {
            if (ModelState.IsValid)
            {
                _roleService.Create(role);
                return RedirectToAction("Index");
            }

            return View(role);
        }

        // GET: Role/Edit/5
        public ActionResult Edit(Guid id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var role = _roleService.Get(id);
            if (role == null) return HttpNotFound();
            return View(role);
        }

        // POST: Role/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Role role)
        {
            if (ModelState.IsValid)
            {
                var result = _roleService.Update(role);

                if (result == 1)
                {
                    return RedirectToAction("Index");
                }
                else if (result == 0)
                {
                    return HttpNotFound();
                }
            }

            return View(role);
        }

        // GET: Role/Delete/5
        public ActionResult Delete(Guid id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var role = _roleService.Get(id);
            if (role == null) return HttpNotFound();
            return View(role);
        }

        // POST: Role/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            var result = _roleService.Delete(id);

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
