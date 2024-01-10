using SupplyManagement_NET48.Contracts;
using SupplyManagement_NET48.Data;
using SupplyManagement_NET48.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SupplyManagement_NET48.Repositories
{
    public class AccountRoleRepository : GeneralRepository<AccountRole>, IAccountRoleRepository
    {
        public AccountRoleRepository(SupplyManagementDbContext context) : base(context)
        {
        }
        public IEnumerable<AccountRole> GetAccountRolesByAccountGuid(Guid guid)
        {
            return Context.Set<AccountRole>().Where(accountRole => accountRole.AccountGuid == guid);
        }
    }
}