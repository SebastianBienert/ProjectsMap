using ProjectsMap.WebApi.DTOs;
using ProjectsMap.WebApi.DTOs.POST;
using ProjectsMap.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsMap.WebApi.Services.Abstract
{
    public interface IProjectService
    {
        IEnumerable<ProjectDto> GetProjectsByName(string name);
        ProjectDto GetProject(int id);
        int Post(CreateProject Project);
        void Delete(int id);
        IEnumerable<ProjectDto> GetAllProjects();
        IEnumerable<ProjectDto> GetProjectsByTechnology(string technology);
        IEnumerable<EmployeeDto> GetEmployeesInProjectByProjectId(int id);
        ProjectDto EditProject(CreateProject dtoProject);
    }
}
