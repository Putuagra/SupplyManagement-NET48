using SupplyManagement_NET48.Contracts;
using SupplyManagement_NET48.Data;
using SupplyManagement_NET48.Models;

namespace SupplyManagement_NET48.Repositories
{
    public class VendorRepository : GeneralRepository<Vendor>, IVendorRepository
    {
        public VendorRepository(SupplyManagementDbContext context) : base(context)
        {
        }
    }
}