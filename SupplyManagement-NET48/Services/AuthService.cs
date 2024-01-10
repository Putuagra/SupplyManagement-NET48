using SupplyManagement_NET48.Contracts;
using SupplyManagement_NET48.DataTransferObjects.Auths;
using SupplyManagement_NET48.Utilities.Handlers;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace SupplyManagement_NET48.Services
{
    public class AuthService
    {
        private readonly IAccountVendorRepository _accountVendorRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountRoleRepository _accountRoleRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IVendorRepository _vendorRepository;
        private readonly ITokenHandler _tokenHandler;

        public AuthService(IAccountVendorRepository accountVendorRepository, IAccountRepository accountRepository, IEmployeeRepository employeeRepository, IVendorRepository vendorRepository, ITokenHandler tokenHandler, IAccountRoleRepository accountRoleRepository, IRoleRepository roleRepository)
        {
            _accountVendorRepository = accountVendorRepository;
            _accountRepository = accountRepository;
            _employeeRepository = employeeRepository;
            _vendorRepository = vendorRepository;
            _tokenHandler = tokenHandler;
            _accountRoleRepository = accountRoleRepository;
            _roleRepository = roleRepository;
        }

        public string Login(LoginDto loginDto)
        {
            var vendor = _vendorRepository.GetByEmail(loginDto.Email);
            var employee = _employeeRepository.GetByEmail(loginDto.Email);

            if (vendor != null && employee == null)
            {
                var accountVendor = _accountVendorRepository.GetByGuid(vendor.Guid);
                if (accountVendor is null || !HashingHandler.Validate(loginDto.Password, accountVendor.Password))
                {
                    return "-1";
                }

                if (vendor.IsAdminApprove && vendor.IsManagerApprove)
                {
                    var claims = new List<Claim>()
                    {
                        new Claim("Guid", vendor.Guid.ToString()),
                        new Claim("Name", $"{vendor.Name}"),
                        new Claim("Email", vendor.Email),
                        new Claim("Role", "Vendor"),
                    };
                    var token = _tokenHandler.GenerateToken(claims);
                    return token;
                }
                else
                {
                    return "-2";
                }
            }
            else if (employee != null && vendor == null)
            {
                var accountEmployee = _accountRepository.GetByGuid(employee.Guid);
                if (accountEmployee is null || !HashingHandler.Validate(loginDto.Password, accountEmployee.Password))
                {
                    return "-1";
                }

                var claims = new List<Claim>()
                {
                    new Claim("Guid", employee.Guid.ToString()),
                    new Claim("FullName", $"{employee.FirstName} {employee.LastName}"),
                    new Claim("Email", $"{employee.Email}"),
                };
                var accountRoles = _accountRoleRepository.GetAccountRolesByAccountGuid(accountEmployee.Guid);
                var getRolesNameByAccountRole = accountRoles
                .Join(
                    _roleRepository.GetAll(),
                    accountRole => accountRole.RoleGuid,
                    role => role.Guid,
                    (accountRole, role) => role.Name
                )
                .ToList();

                claims.AddRange(getRolesNameByAccountRole.Select(role => new Claim(ClaimTypes.Role, role)));
                var token = _tokenHandler.GenerateToken(claims);
                return token;
            }
            else
            {
                return "0";
            }
        }
    }
}