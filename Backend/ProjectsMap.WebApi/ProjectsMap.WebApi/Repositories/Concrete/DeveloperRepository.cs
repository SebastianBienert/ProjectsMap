using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectsMap.WebApi.Models;
using ProjectsMap.WebApi.Repositories.Abstract;

namespace ProjectsMap.WebApi.Repositories.Concrete
{
    public class DeveloperRepository : IDeveloperRepository
    {
        public IEnumerable<Developer> Developers { get; }
        public Developer Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Add(Developer developer)
        {
            throw new NotImplementedException();
        }

        public void Delete(Developer developer)
        {
            throw new NotImplementedException();
        }

        public void Update(Developer developer)
        {
            throw new NotImplementedException();
        }
    }
}