using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectsMap.WebApi.DTOs;
using ProjectsMap.WebApi.DTOs.POST;
using ProjectsMap.WebApi.Models;

namespace ProjectsMap.WebApi.Repositories.Abstract
{
    public interface IProjectRepository
    {
        IEnumerable<Project> Projects { get; }

        Project Get(int id);

        void Add(Project project);

        int Add(CreateProject project);

        void Delete(Project project);

        void Update(Project project);
    }
}
