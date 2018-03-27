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
            name = name.ToLower().TrimStart(); ;
            var list = _repository.Projects.Where(x => x.Description.ToLower().Contains(name)).ToList();

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

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public void Update(CreateProject project)
        {
            _repository.Update(project);
        }

        public IEnumerable<ProjectDto> GetAllProjects()
        {
            return _repository.Projects.Select(x => DTOMapper.GetProjectDto(x)).ToList();
        }

        public IEnumerable<ProjectDto> GetProjectsByTechnology(string technology)
        {
                technology = technology.ToLower().TrimStart();
                var list = _repository.Projects.Where(x => x.Technologies.Select(t => t.Name.ToLower()).ToList().Any(s => s.Contains(technology))).ToList();

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
    }
}