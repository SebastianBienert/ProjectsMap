using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectsMap.WebApi.DTOs;
using ProjectsMap.WebApi.Models;

namespace ProjectsMap.WebApi.Repositories.Abstract
{
    public interface ITechnologyRepository
    {
        IEnumerable<Technology> Technologies { get; }

        Technology Get(int id);

        IEnumerable<Technology> GetTechnologiesByName(string name);

        void Add(TechnologyDto technology);

        void Delete(Technology technology);

        void Update(Technology technology);
    }
}
