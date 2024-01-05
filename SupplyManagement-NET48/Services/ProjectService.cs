using SupplyManagement_NET48.Contracts;
using SupplyManagement_NET48.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SupplyManagement_NET48.Services
{
    public class ProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public IEnumerable<Project> Get()
        {
            var projects = _projectRepository.GetAll();
            if (!projects.Any()) return null;
            return projects;
        }

        public Project Get(Guid guid)
        {
            var project = _projectRepository.GetByGuid(guid);
            if (project is null) return null;
            return project;
        }

        public Project Create(Project projectCreate)
        {
            var project = new Project
            {
                Guid = Guid.NewGuid(),
                Name = projectCreate.Name,
                Description = projectCreate.Description,
                VendorGuid = projectCreate.VendorGuid,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };

            var createdProject = _projectRepository.Create(project);

            return createdProject;
        }

        public int Update(Project projectUpdate)
        {
            var getProject = _projectRepository.GetByGuid(projectUpdate.Guid);
            if (getProject == null) return 0;

            getProject.Name = projectUpdate.Name;
            getProject.Description = projectUpdate.Description;
            getProject.VendorGuid = projectUpdate.VendorGuid;
            getProject.ModifiedDate = DateTime.Now;

            var isUpdate = _projectRepository.Update(getProject);

            return isUpdate ? 1 : 0;
        }

        public int Delete(Guid guid)
        {
            var project = _projectRepository.GetByGuid(guid);
            if (project == null) return 0;
            var isDelete = _projectRepository.Delete(project);
            return isDelete ? 1 : 0;
        }
    }
}