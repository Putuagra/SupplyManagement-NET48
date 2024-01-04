using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupplyManagement_NET48.Models
{
    [Table("tb_m_vendors")]
    public class Vendor : BaseEntity
    {
        [Column("vendor_name", TypeName = "nvarchar(max)")]
        public string Name { get; set; }
        [Column("email", TypeName = "nvarchar")]
        public string Email { get; set; }
        [Column("phone_number", TypeName = "nvarchar")]
        public string PhoneNumber { get; set; }
        [Column("photo_profile", TypeName = "nvarchar(max)")]
        public string PhotoProfile { get; set; }
        [Column("sector", TypeName = "nvarchar")]
        public string Sector { get; set; }
        [Column("type", TypeName = "nvarchar")]
        public string Type { get; set; }
        [Column("is_admin_approve")]
        public bool IsAdminApprove { get; set; }
        [Column("is_manager_approve")]
        public bool IsManagerApprove { get; set; }

        // Cardinality
        public AccountVendor AccountVendor { get; set; }
        public ICollection<Project> Projects { get; set; }
    }
}