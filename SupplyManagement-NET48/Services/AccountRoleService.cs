using SupplyManagement_NET48.Contracts;
using SupplyManagement_NET48.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SupplyManagement_NET48.Services
{
    public class AccountRoleService
    {
        private readonly IAccountRoleRepository _accountRoleRepository;

        public AccountRoleService(IAccountRoleRepository accountRoleRepository)
        {
            _accountRoleRepository = accountRoleRepository;
        }

        public IEnumerable<AccountRole> Get()
        {
            var accountRoles = _accountRoleRepository.GetAll();
            if (!accountRoles.Any()) return null;
            return accountRoles;
        }

        public AccountRole Get(Guid guid)
        {
            var accountRole = _accountRoleRepository.GetByGuid(guid);
            if (accountRole is null) return null;
            return accountRole;
        }

        public AccountRole Create(AccountRole accountRoleCreate)
        {
            var accountRole = new AccountRole
            {
                Guid = Guid.NewGuid(),
                AccountGuid = accountRoleCreate.AccountGuid,
                RoleGuid = accountRoleCreate.RoleGuid,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };

            var createdAccountRole = _accountRoleRepository.Create(accountRole);

            return createdAccountRole;
        }

        public int Update(AccountRole accountRoleUpdate)
        {
            var getAccountRole = _accountRoleRepository.GetByGuid(accountRoleUpdate.Guid);
            if (getAccountRole == null) return 0;

            getAccountRole.AccountGuid = accountRoleUpdate.AccountGuid;
            getAccountRole.RoleGuid = accountRoleUpdate.RoleGuid;
            getAccountRole.ModifiedDate = DateTime.Now;

            var isUpdate = _accountRoleRepository.Update(getAccountRole);

            return isUpdate ? 1 : 0;
        }

        public int Delete(Guid guid)
        {
            var accountRole = _accountRoleRepository.GetByGuid(guid);
            if (accountRole == null) return 0;
            var isDelete = _accountRoleRepository.Delete(accountRole);
            return isDelete ? 1 : 0;
        }
    }
}