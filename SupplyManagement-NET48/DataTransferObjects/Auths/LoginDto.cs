using System.ComponentModel.DataAnnotations;

namespace SupplyManagement_NET48.DataTransferObjects.Auths
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}