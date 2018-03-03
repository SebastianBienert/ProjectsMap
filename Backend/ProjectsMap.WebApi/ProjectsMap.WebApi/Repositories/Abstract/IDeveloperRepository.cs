using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectsMap.WebApi.DTOs;
using ProjectsMap.WebApi.Models;

namespace ProjectsMap.WebApi.Repositories.Abstract
{
    public interface IDeveloperRepository
    {
        IEnumerable<Developer> Developers { get; }

        Developer Get(int id);

        int Add(DeveloperDto developer);

        int Add(Developer developer);

        void Delete(Developer developer);

        void Update(Developer developer);

    }
}
