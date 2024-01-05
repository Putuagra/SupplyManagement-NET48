using SupplyManagement_NET48.DataTransferObjects.Accounts;
using SupplyManagement_NET48.Models;
using SupplyManagement_NET48.Services;
using System;
using System.Net;
using System.Web.Mvc;

namespace SupplyManagement_NET48.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountService _accountService;
        private readonly EmployeeService _employeeService;

        public AccountController(AccountService accountService, EmployeeService employeeService)
        {
            _accountService = accountService;
            _employeeService = employeeService;
        }

        // GET: Account
        public ActionResult Index()
        {
            var accounts = _accountService.Get();
            var employees = _employeeService.Get();
            ViewData["Employees"] = employees;
            return View(accounts);
        }

        // GET: Account/Details/5
        public ActionResult Details(Guid id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var account = _accountService.Get(id);
            if (account == null) return HttpNotFound();
            return View(account);
        }

        // GET: Account/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Account/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Account account)
        {
            if (ModelState.IsValid)
            {
                _accountService.Create(account);
                return RedirectToAction("Index");
            }
            return View(account);
        }

        // GET: Account/Edit/5
        public ActionResult Edit(Guid id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var account = _accountService.Get(id);
            if (account == null) return HttpNotFound();
            return View(account);
        }

        // POST: Account/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Account account)
        {
            if (ModelState.IsValid)
            {
                var result = _accountService.Update(account);

                if (result == 1)
                {
                    return RedirectToAction("Index");
                }
                else if (result == 0)
                {
                    return HttpNotFound();
                }
            }
            return View(account);
        }

        // GET: Account/Delete/5
        public ActionResult Delete(Guid id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var account = _accountService.Get(id);
            if (account == null) return HttpNotFound();
            return View(account);
        }

        // POST: Account/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            var result = _accountService.Delete(id);

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

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(AccountDtoRegister registerDto)
        {
            if (ModelState.IsValid)
            {
                if (_accountService.Register(registerDto))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Registration failed. Please try again.");
                }
            }

            return View(registerDto);
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
