using System.ComponentModel.DataAnnotations.Schema;

namespace SupplyManagement_NET48.Models
{
    [Table("tb_m_employees")]
    public class Employee : BaseEntity
    {
        [Column("first_name", TypeName = "nvarchar")]
        public string FirstName { get; set; }
        [Column("last_name", TypeName = "nvarchar")]
        public string LastName { get; set; } = string.Empty;
        [Column("email", TypeName = "nvarchar")]
        public string Email { get; set; }

        // Cardinality
        public Account Account { get; set; }
    }
}