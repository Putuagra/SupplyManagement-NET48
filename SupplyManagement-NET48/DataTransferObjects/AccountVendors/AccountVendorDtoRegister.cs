using System.ComponentModel.DataAnnotations;
using System.Web;

namespace SupplyManagement_NET48.DataTransferObjects.AccountVendors
{
    public class AccountVendorDtoRegister
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string PhotoProfile { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public HttpPostedFileBase ImageFile { get; set; }
    }
}