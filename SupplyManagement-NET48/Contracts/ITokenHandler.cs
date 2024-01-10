using System.Collections.Generic;
using System.Security.Claims;

namespace SupplyManagement_NET48.Contracts
{
    public interface ITokenHandler
    {
        string GenerateToken(IEnumerable<Claim> claims);
    }
}
