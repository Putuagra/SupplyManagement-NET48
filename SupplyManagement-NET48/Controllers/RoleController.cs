using SupplyManagement_NET48.Models;
using SupplyManagement_NET48.Services;
using System.Web.Mvc;

namespace SupplyManagement_NET48.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleService _roleService;
        /*private readonly RoleRepository _roleRepository;*/

        /*public RoleController(RoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }*/

        public RoleController(RoleService roleService)
        {
            _roleService = roleService;
        }

        // GET: Role
        public ActionResult Index()
        {
            var roles = _roleService.Get();
            return View(roles);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Role role)
        {
            _roleService.Create(role);
            return RedirectToAction("Index");
        }
    }
}