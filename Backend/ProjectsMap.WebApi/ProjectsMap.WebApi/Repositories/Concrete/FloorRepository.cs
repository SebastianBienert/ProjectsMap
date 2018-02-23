using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectsMap.WebApi.Models;
using ProjectsMap.WebApi.Repositories.Abstract;

namespace ProjectsMap.WebApi.Repositories.Concrete
{
    public class FloorRepository : IFloorRepository
    {
        public IEnumerable<Floor> Developers { get; }
        public Floor Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Add(Floor developer)
        {
            throw new NotImplementedException();
        }

        public void Delete(Floor developer)
        {
            throw new NotImplementedException();
        }

        public void Update(Floor developer)
        {
            throw new NotImplementedException();
        }
    }
}