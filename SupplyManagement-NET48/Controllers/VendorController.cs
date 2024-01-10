using SupplyManagement_NET48.Models;
using SupplyManagement_NET48.Services;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web.Mvc;

namespace SupplyManagement_NET48.Controllers
{
    /*[Authorize]*/
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
            var token = Request.Cookies["AuthToken"]?.Value;
            var handler = new JwtSecurityTokenHandler();
            if (token != null)
            {
                var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

                var roleClaim = jsonToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Role || claim.Type == "Role")?.Value;
                /*var fullName = jsonToken?.Payload["FullName"]?.ToString();*/

                ViewBag.UserRole = roleClaim;
                /*ViewBag.Fullname = fullName;*/
            }
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
        public ActionResult Create(Vendor vendorDtoCreate)
        {
            if (ModelState.IsValid)
            {
                string fileName = Path.GetFileNameWithoutExtension(vendorDtoCreate.ImageFile.FileName);
                string extension = Path.GetExtension(vendorDtoCreate.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                vendorDtoCreate.PhotoProfile = "~/Photo/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Photo/"), fileName);
                vendorDtoCreate.ImageFile.SaveAs(fileName);
                _vendorService.Create(vendorDtoCreate);
                return RedirectToAction("Index");
            }

            return View(vendorDtoCreate);
        }

        // GET: Vendors/Edit/5
        public ActionResult Edit(Guid id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var vendor = _vendorService.Get(id);
            Session["imgPath"] = vendor.PhotoProfile;
            if (vendor == null) return HttpNotFound();
            return View(vendor);
        }

        // POST: Vendors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Vendor vendorDtoUpdate)
        {
            if (ModelState.IsValid)
            {
                if (vendorDtoUpdate.ImageFile != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(vendorDtoUpdate.ImageFile.FileName);
                    string extension = Path.GetExtension(vendorDtoUpdate.ImageFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    string physicalPath = Path.Combine(Server.MapPath("~/Photo/"), fileName);
                    vendorDtoUpdate.ImageFile.SaveAs(physicalPath);
                    vendorDtoUpdate.PhotoProfile = "~/Photo/" + fileName;

                    var result = _vendorService.Update(vendorDtoUpdate);

                    string oldPath = Request.MapPath(Session["imgPath"].ToString());

                    if (result == 1)
                    {
                        if (System.IO.File.Exists(oldPath))
                        {
                            System.IO.File.Delete(oldPath);
                        }
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
                else
                {
                    vendorDtoUpdate.PhotoProfile = Session["imgPath"].ToString();
                    var result = _vendorService.Update(vendorDtoUpdate);
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

            }
            return View(vendorDtoUpdate);
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
            var vendor = _vendorService.Get(id);
            string currentPath = Request.MapPath(vendor.PhotoProfile);
            var result = _vendorService.Delete(id);

            if (result == 1)
            {
                if (System.IO.File.Exists(currentPath))
                {
                    System.IO.File.Delete(currentPath);
                }
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

        [HttpPost]
        public ActionResult AdminApprove(Guid guid)
        {
            if (ModelState.IsValid)
            {
                var result = _vendorService.AdminApprove(guid);

                if (result == 1)
                {
                    return RedirectToAction("Index");
                }
                else if (result == 0)
                {
                    return HttpNotFound();
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult AdminReject(Guid guid)
        {
            if (ModelState.IsValid)
            {
                var result = _vendorService.AdminReject(guid);

                if (result == 1)
                {
                    return RedirectToAction("Index");
                }
                else if (result == 0)
                {
                    return HttpNotFound();
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult ManagerApprove(Guid guid)
        {
            if (ModelState.IsValid)
            {
                var result = _vendorService.ManagerApprove(guid);

                if (result == 1)
                {
                    return RedirectToAction("Index");
                }
                else if (result == 0)
                {
                    return HttpNotFound();
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult ManagerReject(Guid guid)
        {
            if (ModelState.IsValid)
            {
                var result = _vendorService.ManagerReject(guid);

                if (result == 1)
                {
                    return RedirectToAction("Index");
                }
                else if (result == 0)
                {
                    return HttpNotFound();
                }
            }
            return View();
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
