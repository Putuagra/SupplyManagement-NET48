using SupplyManagement_NET48.DataTransferObjects.AccountVendors;
using SupplyManagement_NET48.Models;
using SupplyManagement_NET48.Services;
using System;
using System.Net;
using System.Web.Mvc;

namespace SupplyManagement_NET48.Controllers
{
    public class AccountVendorController : Controller
    {
        private readonly AccountVendorService _accountVendorService;
        private readonly VendorService _vendorService;

        public AccountVendorController(AccountVendorService accountVendorService, VendorService vendorService)
        {
            _accountVendorService = accountVendorService;
            _vendorService = vendorService;
        }

        // GET: AccountVendor
        public ActionResult Index()
        {
            var accountVendors = _accountVendorService.Get();
            var vendors = _vendorService.Get();
            ViewData["Vendors"] = vendors;
            return View(accountVendors);
        }

        // GET: AccountVendor/Details/5
        public ActionResult Details(Guid id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var accountVendor = _accountVendorService.Get(id);
            if (accountVendor == null) return HttpNotFound();
            return View(accountVendor);
        }

        // GET: AccountVendor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccountVendor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AccountVendor accountVendor)
        {
            if (ModelState.IsValid)
            {
                _accountVendorService.Create(accountVendor);
                return RedirectToAction("Index");
            }
            return View(accountVendor);
        }

        // GET: AccountVendor/Edit/5
        public ActionResult Edit(Guid id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var accountVendor = _accountVendorService.Get(id);
            if (accountVendor == null) return HttpNotFound();
            return View(accountVendor);
        }

        // POST: AccountVendor/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AccountVendor accountVendor)
        {
            if (ModelState.IsValid)
            {
                var result = _accountVendorService.Update(accountVendor);

                if (result == 1)
                {
                    return RedirectToAction("Index");
                }
                else if (result == 0)
                {
                    return HttpNotFound();
                }
            }
            return View(accountVendor);
        }

        // GET: AccountVendor/Delete/5
        public ActionResult Delete(Guid id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var accountVendor = _accountVendorService.Get(id);
            if (accountVendor == null) return HttpNotFound();
            return View(accountVendor);
        }

        // POST: AccountVendor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            var result = _accountVendorService.Delete(id);

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
        public ActionResult Register(AccountVendorDtoRegister registerDto)
        {
            if (ModelState.IsValid)
            {
                if (_accountVendorService.Register(registerDto))
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
