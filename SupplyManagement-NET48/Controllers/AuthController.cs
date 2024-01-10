using SupplyManagement_NET48.DataTransferObjects.AccountVendors;
using SupplyManagement_NET48.DataTransferObjects.Auths;
using SupplyManagement_NET48.Services;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SupplyManagement_NET48.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthService _authService;
        private readonly AccountVendorService _accountVendorService;

        public AuthController(AuthService authService, AccountVendorService accountVendorService)
        {
            _authService = authService;
            _accountVendorService = accountVendorService;
        }

        // GET: Auth
        public ActionResult Index()
        {
            return View();
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
                string fileName = Path.GetFileNameWithoutExtension(registerDto.ImageFile.FileName);
                string extension = Path.GetExtension(registerDto.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                registerDto.PhotoProfile = "~/Image/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
                registerDto.ImageFile.SaveAs(fileName);

                if (_accountVendorService.Register(registerDto))
                {
                    return RedirectToAction("Login", "Auth");
                }
                else
                {
                    ModelState.AddModelError("", "Registration failed. Please try again.");
                }
            }

            return View(registerDto);
        }

        [HttpGet]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Vendor");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Invalid login information";
                return View(loginDto);
            }

            var result = _authService.Login(loginDto);
            if (result == "0")
            {
                TempData["Error"] = "not found";
            }
            else if (result == "-1")
            {
                TempData["Error"] = "Wrong email or password";
            }
            else if (result == "-2")
            {
                TempData["Error"] = "Error retrieving when creating token";
            }
            else
            {
                var cookie = new HttpCookie("AuthToken", result)
                {
                    HttpOnly = true,
                };
                Response.Cookies.Add(cookie);
                FormsAuthentication.SetAuthCookie(loginDto.Email, false);

                return RedirectToAction("Index", "Vendor");
            }
            return View(loginDto);
        }

        [HttpPost]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            var cookie = new HttpCookie("AuthToken")
            {
                Expires = DateTime.Now.AddDays(-1),
            };
            Response.Cookies.Add(cookie);
            return RedirectToAction("Login", "Auth");
        }
    }
}