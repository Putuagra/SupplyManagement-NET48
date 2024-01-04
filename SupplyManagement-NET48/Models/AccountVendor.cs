using System.ComponentModel.DataAnnotations.Schema;

namespace SupplyManagement_NET48.Models
{
    [Table("tb_m_account_vendors")]
    public class AccountVendor : BaseEntity
    {
        [Column("password")]
        public string Password { get; set; }

        // Cardinality
        public Vendor Vendor { get; set; }
    }
}