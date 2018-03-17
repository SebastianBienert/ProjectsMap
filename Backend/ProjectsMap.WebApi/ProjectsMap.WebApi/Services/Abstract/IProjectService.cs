using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectsMap.WebApi.DTOs;
using ProjectsMap.WebApi.DTOs.POST;

namespace ProjectsMap.WebApi.Services.Abstract
{
    public interface IProjectService
    {
        IEnumerable<ProjectDto> GetAllProjects();

        ProjectDto GetProject(int id);

        int Post(CreateProject project);

        void Delete(int id);

        void Update(CreateProject project);
    }
}
