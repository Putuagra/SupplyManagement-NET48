using SupplyManagement_NET48.Models;
using SupplyManagement_NET48.Services;
using System;
using System.Net;
using System.Web.Mvc;

namespace SupplyManagement_NET48.Controllers
{
    public class AccountRoleController : Controller
    {
        private readonly AccountRoleService _accountRoleService;
        private readonly EmployeeService _employeeService;
        private readonly RoleService _roleService;

        public AccountRoleController(AccountRoleService accountRoleService, RoleService roleService, EmployeeService employeeService)
        {
            _accountRoleService = accountRoleService;
            _roleService = roleService;
            _employeeService = employeeService;
        }

        // GET: AccountRole
        public ActionResult Index()
        {
            var accountRoles = _accountRoleService.Get();
            var employees = _employeeService.Get();
            var roles = _roleService.Get();
            ViewData["Employees"] = employees;
            ViewData["Roles"] = roles;
            return View(accountRoles);
        }

        // GET: AccountRole/Details/5
        public ActionResult Details(Guid id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var accountRole = _accountRoleService.Get(id);
            if (accountRole == null) return HttpNotFound();
            var employees = _employeeService.Get();
            var roles = _roleService.Get();
            ViewData["Employees"] = employees;
            ViewData["Roles"] = roles;
            return View(accountRole);
        }

        // GET: AccountRole/Create
        public ActionResult Create()
        {
            var employees = _employeeService.Get();
            var roles = _roleService.Get();
            ViewData["Employees"] = employees;
            ViewData["Roles"] = roles;
            return View();
        }

        // POST: AccountRole/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AccountRole accountRole)
        {
            if (ModelState.IsValid)
            {
                _accountRoleService.Create(accountRole);
                return RedirectToAction("Index");
            }
            return View(accountRole);
        }

        // GET: AccountRole/Edit/5
        public ActionResult Edit(Guid id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var accountRole = _accountRoleService.Get(id);
            if (accountRole == null) return HttpNotFound();
            var employees = _employeeService.Get();
            var roles = _roleService.Get();
            ViewData["Employees"] = employees;
            ViewData["Roles"] = roles;
            return View(accountRole);
        }

        // POST: AccountRole/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AccountRole accountRole)
        {
            if (ModelState.IsValid)
            {
                var result = _accountRoleService.Update(accountRole);

                if (result == 1)
                {
                    return RedirectToAction("Index");
                }
                else if (result == 0)
                {
                    return HttpNotFound();
                }
            }
            return View(accountRole);
        }

        // GET: AccountRole/Delete/5
        public ActionResult Delete(Guid id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var accountRole = _accountRoleService.Get(id);
            if (accountRole == null) return HttpNotFound();
            var employees = _employeeService.Get();
            var roles = _roleService.Get();
            ViewData["Employees"] = employees;
            ViewData["Roles"] = roles;
            return View(accountRole);
        }

        // POST: AccountRole/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            var result = _accountRoleService.Delete(id);

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
