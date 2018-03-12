using ProjectsMap.WebApi.DTOs;
using ProjectsMap.WebApi.DTOs.POST;
using ProjectsMap.WebApi.Mappers;
using ProjectsMap.WebApi.Models;
using ProjectsMap.WebApi.Repositories.Abstract;
using ProjectsMap.WebApi.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectsMap.WebApi.Services.Concrete
{
    public class ProjectService : IProjectService
    {
        private IProjectRepository _repository;

        public ProjectService(IProjectRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<ProjectDto> GetProjectsByName(string name)
        {
            var list = _repository.Projects.ToList();

            if (list.Count() > 0)
            {
                var dtoS = list.Select(x => DTOMapper.GetProjectDto(x)).ToList();
                return dtoS;
            }
            else
            {
                return null;
            }
        }

        public ProjectDto GetProject(int id)
        {
            var project = _repository.Get(id);
            if (project == null)
                return null;

            return DTOMapper.GetProjectDto(project);
        }

        public int Post(CreateProject Project)
        {
            return _repository.Add(Project);
        }

        public void Delete(Project project)
        {
            _repository.Delete(project);
        }

        public void Update(Project project)
        {
            _repository.Update(project);
        }

        public IEnumerable<ProjectDto> GetAllProjects()
        {
            return _repository.Projects.Select(x => DTOMapper.GetProjectDto(x)).ToList();
        }
    }
}