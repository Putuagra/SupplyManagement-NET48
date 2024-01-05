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

            container.RegisterType<IAccountRepository, AccountRepository>();
            container.RegisterType<AccountService>();
            container.RegisterType<IAccountRoleRepository, AccountRoleRepository>();
            container.RegisterType<AccountRoleService>();
            container.RegisterType<IAccountVendorRepository, AccountVendorRepository>();
            container.RegisterType<AccountVendorService>();
            container.RegisterType<IEmployeeRepository, EmployeeRepository>();
            container.RegisterType<EmployeeService>();
            container.RegisterType<IProjectRepository, ProjectRepository>();
            container.RegisterType<ProjectService>();
            container.RegisterType<IRoleRepository, RoleRepository>();
            container.RegisterType<RoleService>();
            container.RegisterType<IVendorRepository, VendorRepository>();
            container.RegisterType<VendorService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}