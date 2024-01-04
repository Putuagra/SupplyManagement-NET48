using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupplyManagement_NET48.Models
{
    [Table("tb_m_projects")]
    public class Project : BaseEntity
    {
        [Column("name", TypeName = "nvarchar(max)")]
        public string Name { get; set; }
        [Column("description", TypeName = "nvarchar(max)")]
        public string Description { get; set; }
        [Column("vendor_guid")]
        public Guid VendorGuid { get; set; }

        // Cardinality
        public Vendor Vendor { get; set; }
    }
}