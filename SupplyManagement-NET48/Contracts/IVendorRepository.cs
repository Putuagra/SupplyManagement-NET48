using SupplyManagement_NET48.Models;

namespace SupplyManagement_NET48.Contracts
{
    public interface IVendorRepository : IGeneralRepository<Vendor>
    {
        Vendor GetByEmail(string email);
    }
}
