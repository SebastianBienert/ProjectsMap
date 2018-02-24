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
        public IEnumerable<Developer> Developers
        {
            get
            {
                var dbContext = new EfDbContext();
                return dbContext.Developers.ToList();
            }
        }

        public Developer Get(int id)
        {
            var dbContext = new EfDbContext();
            return dbContext.Developers.FirstOrDefault(x => x.DeveloperId == id);
        }

        public void Add(Developer developer)
        {
            using (var dbContext = new EfDbContext())
            {
                dbContext.Developers.Add(developer);
                dbContext.SaveChanges();
            }
        }

        public void Delete(Developer developer)
        {
            using (var dbContext = new EfDbContext())
            {
                dbContext.Developers.Remove(developer);
                dbContext.SaveChanges();
            }
        }

        public void Update(Developer developer)
        {
            using (var dbContext = new EfDbContext())
            {
                var dev = dbContext.Developers.FirstOrDefault(x => x.DeveloperId == developer.DeveloperId);
                dev = developer;
                dbContext.SaveChanges();
            }
        }
    }
}