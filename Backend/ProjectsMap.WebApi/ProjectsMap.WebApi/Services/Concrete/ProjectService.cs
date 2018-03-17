using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectsMap.WebApi.DTOs;
using ProjectsMap.WebApi.DTOs.POST;
using ProjectsMap.WebApi.Mappers;
using ProjectsMap.WebApi.Repositories.Abstract;
using ProjectsMap.WebApi.Services.Abstract;

namespace ProjectsMap.WebApi.Services.Concrete
{
    public class ProjectService : IProjectService
    {
        private IProjectRepository _repository;
        public ProjectService(IProjectRepository repo)
        {
            _repository = repo;
        }

        public IEnumerable<ProjectDto> GetAllProjects()
        {
            throw new NotImplementedException();
        }

        public ProjectDto GetProject(int id)
        {
            throw new NotImplementedException();
        }

        public int Post(CreateProject project)
        {
            return _repository.Add(project);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(CreateProject project)
        {
            throw new NotImplementedException();
        }
    }
}