using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectsMap.WebApi.DTOs;
using ProjectsMap.WebApi.Models;

namespace ProjectsMap.WebApi.Services.Abstract
{
    public interface ITechnologyService
    {
        IEnumerable<TechnologyDto> GetAllTechnologies();

        TechnologyDto GetTechnology(int id);

        IEnumerable<TechnologyDto> GetTechnologiesByName(string technology);

        void Post(TechnologyDto technology);

        void Delete(TechnologyDto technology);

        void Update(TechnologyDto technology);
    }
}
