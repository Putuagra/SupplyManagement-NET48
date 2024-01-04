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
            if (!roles.Any())
            {
                return null;
            }

            return roles;
        }

        public Role Create(Role roleDtoCreate)
        {
            var role = new Role
            {
                Guid = new Guid(),
                Name = roleDtoCreate.Name,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };

            var createdRole = _roleRepository.Create(role);

            return createdRole;
        }
    }
}