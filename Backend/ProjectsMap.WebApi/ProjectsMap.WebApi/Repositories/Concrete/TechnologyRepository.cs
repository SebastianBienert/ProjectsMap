using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectsMap.WebApi.DTOs;
using ProjectsMap.WebApi.Models;
using ProjectsMap.WebApi.Repositories.Abstract;
using System.Data.Entity;

namespace ProjectsMap.WebApi.Repositories.Concrete
{
    public class TechnologyRepository : ITechnologyRepository
    {
        public IEnumerable<Technology> Technologies
        {
            get
            {
                using (var dbContext = new EfDbContext())
                {
                    return dbContext.Technologies
                        .Include(t => t.Developers)
                        .Include(t => t.Projects)
                        .ToList();
                }
            }
        }

        public Technology Get(int id)
        {
            using (var dbContext = new EfDbContext())
            {
                return dbContext.Technologies
                    .Include(t => t.Developers)
                    .Include(t => t.Projects).
                    FirstOrDefault(t => t.TechnologyId == id);
            }
        }

        public void Add(TechnologyDto technologyDto)
        {
            using (var dbContext = new EfDbContext())
            {
                var developersFromDataBase = dbContext.Developers.Where(x => technologyDto.DevelopersId.Contains(x.DeveloperId)).ToList();
                var projectsFromDatabase = dbContext.Projects.Where(x => technologyDto.ProjectsId.Contains(x.ProjectId)).ToList();
                var technology = new Technology()
                {
                    Name = technologyDto.Name,
                    Developers = technologyDto.DevelopersId.Count() == 0 ? null : developersFromDataBase, 
                    Projects = technologyDto.ProjectsId.Count() == 0 ? null : projectsFromDatabase
                };

                foreach (var dev in developersFromDataBase)
                {
                    dev.Technologies.Add(technology);
                }

                foreach (var project in projectsFromDatabase)
                {
                    project.Technologies.Add(technology);
                }

                dbContext.Technologies.Add(technology);
                dbContext.SaveChanges();
            }
        }

        public IEnumerable<Technology> GetTechnologiesByName(string name)
        {
            using (var dbContext = new EfDbContext())
            {
                return dbContext.Technologies
                    .Include(t => t.Developers)
                    .Include(t => t.Projects)
                    .Where(t => t.Name.StartsWith(name)).ToList();
            }
        }


        public void Delete(Technology technology)
        {
            throw new NotImplementedException();
        }

        public void Update(Technology technology)
        {
            throw new NotImplementedException();
        }
    }
}