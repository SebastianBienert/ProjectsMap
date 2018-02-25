using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectsMap.WebApi.DTOs;
using ProjectsMap.WebApi.Models;

namespace ProjectsMap.WebApi.Services.Abstract
{
    public interface IDeveloperService
    {
        IEnumerable<DeveloperDto> GetAllDevelopers();

        DeveloperDto GetDeveloper(int id);

        IEnumerable<DeveloperDto> GetDevelopersByTechnology(string technology);

        void Post(Developer developer);

        void Delete(Developer developer);

        void Update(Developer developer);

    }
}
