using SupplyManagement_NET48.Contracts;
using SupplyManagement_NET48.Repositories;
using SupplyManagement_NET48.Services;
using System.Web.Mvc;
using Unity;
using Unity.AspNet.Mvc;

namespace SupplyManagement_NET48.App_Start
{
    public class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // Registrasi layanan di sini
            container.RegisterType<IRoleRepository, RoleRepository>();
            container.RegisterType<RoleService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}