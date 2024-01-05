using SupplyManagement_NET48.Contracts;
using SupplyManagement_NET48.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SupplyManagement_NET48.Services
{
    public class RoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public IEnumerable<Role> Get()
        {
            var roles = _roleRepository.GetAll();
            if (!roles.Any()) return null;
            return roles;
        }

        public Role Get(Guid guid)
        {
            var role = _roleRepository.GetByGuid(guid);
            if (role is null) return null;
            return role;
        }

        public Role Create(Role roleDtoCreate)
        {
            var role = new Role
            {
                Guid = Guid.NewGuid(),
                Name = roleDtoCreate.Name,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };

            var createdRole = _roleRepository.Create(role);

            return createdRole;
        }

        public int Update(Role roleUpdate)
        {
            var getRole = _roleRepository.GetByGuid(roleUpdate.Guid);
            if (getRole == null) return 0;

            getRole.Name = roleUpdate.Name;
            getRole.ModifiedDate = DateTime.Now;

            var isUpdate = _roleRepository.Update(getRole);

            return isUpdate ? 1 : 0;
        }

        public int Delete(Guid guid)
        {
            var role = _roleRepository.GetByGuid(guid);
            if (role == null) return 0;
            var isDelete = _roleRepository.Delete(role);
            return isDelete ? 1 : 0;
        }
    }
}