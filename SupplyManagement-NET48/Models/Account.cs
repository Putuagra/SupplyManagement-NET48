using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupplyManagement_NET48.Models
{
    [Table("tb_m_accounts")]
    public class Account : BaseEntity
    {
        [Column("password", TypeName = "nvarchar(max)")]
        public string Password { get; set; }

        // Cardinality
        public Employee Employee { get; set; }
        public ICollection<AccountRole> AccountRoles { get; set; }
    }
}