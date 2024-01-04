using SupplyManagement_NET48.Contracts;
using SupplyManagement_NET48.Data;
using SupplyManagement_NET48.Models;

namespace SupplyManagement_NET48.Repositories
{
    public class AccountRepository : GeneralRepository<Account>, IAccountRepository
    {
        public AccountRepository(SupplyManagementDbContext context) : base(context)
        {
        }
    }
}