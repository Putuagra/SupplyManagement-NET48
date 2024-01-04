using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupplyManagement_NET48.Models
{
    [Table("tb_m_roles")]
    public class Role : BaseEntity
    {
        [Column("name", TypeName = "nvarchar")]
        public string Name { get; set; }

        // Cardinality
        public ICollection<AccountRole> AccountRoles { get; set; }
    }
}