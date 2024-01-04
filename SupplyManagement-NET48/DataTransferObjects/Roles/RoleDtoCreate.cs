using SupplyManagement_NET48.Models;

namespace SupplyManagement_NET48.DataTransferObjects.Roles
{
    public class RoleDtoCreate
    {
        public string Name { get; set; }

        public static implicit operator Role(RoleDtoCreate roleDtoCreate)
        {
            return new Role()
            {
                Name = roleDtoCreate.Name
            };
        }

        public static explicit operator RoleDtoCreate(Role role)
        {
            return new RoleDtoCreate()
            {
                Name = role.Name,
            };
        }
    }
}