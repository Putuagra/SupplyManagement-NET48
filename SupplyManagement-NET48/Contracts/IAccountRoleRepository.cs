using SupplyManagement_NET48.Models;
using System;
using System.Collections.Generic;

namespace SupplyManagement_NET48.Contracts
{
    public interface IAccountRoleRepository : IGeneralRepository<AccountRole>
    {
        IEnumerable<AccountRole> GetAccountRolesByAccountGuid(Guid guid);
    }
}
