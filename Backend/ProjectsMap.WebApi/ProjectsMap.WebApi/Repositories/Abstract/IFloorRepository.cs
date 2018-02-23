using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectsMap.WebApi.Models;

namespace ProjectsMap.WebApi.Repositories.Abstract
{
    public interface IFloorRepository
    {
        IEnumerable<Floor> Developers { get; }

        Floor Get(int id);

        void Add(Floor developer);

        void Delete(Floor developer);

        void Update(Floor developer);
    }
}
