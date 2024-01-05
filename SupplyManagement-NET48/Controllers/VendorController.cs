using SupplyManagement_NET48.Models;
using SupplyManagement_NET48.Services;
using System;
using System.Net;
using System.Web.Mvc;

namespace SupplyManagement_NET48.Controllers
{
    public class VendorController : Controller
    {
        private readonly VendorService _vendorService;
        public VendorController(VendorService vendorService)
        {
            _vendorService = vendorService;
        }

        // GET: Vendors
        public ActionResult Index()
        {
            var vendors = _vendorService.Get();
            return View(vendors);
        }

        // GET: Vendors/Details/5
        public ActionResult Details(Guid id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var vendor = _vendorService.Get(id);
            if (vendor == null) return HttpNotFound();
            return View(vendor);
        }

        // GET: Vendors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vendors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Vendor vendor)
        {
            if (ModelState.IsValid)
            {
                _vendorService.Create(vendor);
                return RedirectToAction("Index");
            }

            return View(vendor);
        }

        // GET: Vendors/Edit/5
        public ActionResult Edit(Guid id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var vendor = _vendorService.Get(id);
            if (vendor == null) return HttpNotFound();
            return View(vendor);
        }

        // POST: Vendors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Vendor vendor)
        {
            if (ModelState.IsValid)
            {
                var result = _vendorService.Update(vendor);

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
            return View(vendor);
        }

        // GET: Vendors/Delete/5
        public ActionResult Delete(Guid id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var vendor = _vendorService.Get(id);
            if (vendor == null) return HttpNotFound();
            return View(vendor);
        }

        // POST: Vendors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            var result = _vendorService.Delete(id);

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
