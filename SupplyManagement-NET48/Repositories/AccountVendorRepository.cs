using SupplyManagement_NET48.Contracts;
using SupplyManagement_NET48.Data;
using SupplyManagement_NET48.Models;

namespace SupplyManagement_NET48.Repositories
{
    public class AccountVendorRepository : GeneralRepository<AccountVendor>, IAccountVendorRepository
    {
        public AccountVendorRepository(SupplyManagementDbContext context) : base(context)
        {
        }
    }
}