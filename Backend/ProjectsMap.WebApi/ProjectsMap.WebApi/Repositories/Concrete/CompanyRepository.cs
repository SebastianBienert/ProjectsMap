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
    public class CompanyRepository : ICompanyRepository
    {
        public IEnumerable<Company> Companies
        {
            get
            {
                using (var dbContext = new EfDbContext())
                {
                    return dbContext.Companies
                        .Include(c => c.Employees.Select(d => d.Technologies))
                        .Include(c => c.Employees.Select(d => d.Seat))
                        .Include(c => c.Buildings.Select(d => d.Floors))
                        .Include(c => c.Projects).ToList();
                }
            }
        }

        public Company Get(int id)
        {
            using (var dbContext = new EfDbContext())
            {
                return dbContext.Companies
                    .Where(c => c.CompanyId == id)
                    .Include(c => c.Employees.Select(d => d.Technologies))
                    .Include(c => c.Employees.Select(d => d.Seat))
                    .Include(c => c.Buildings.Select(d => d.Floors))
                    .Include(c => c.Buildings)
                    .Include(c => c.Projects).FirstOrDefault();
            }
        }

        public int Add(CompanyDto companyDto)
        {
            using (var dbContext = new EfDbContext())
            {
                var buildings = dbContext.Buildings.
                    Where(b => companyDto.Buildings.Select(dto => dto.Id).ToList().Contains(b.BuildingId))
                    .ToList();

                var devs = dbContext.Employees
                    .Where(d => companyDto.Developers.Select(dto => dto.Id).Contains(d.EmployeeId)).ToList();

                var projects = dbContext.Projects.Where(p => companyDto.ProjectsId.Select(dto => dto.Id)
                    .Contains(p.ProjectId)).ToList();


                var company = new Company()
                {
                    Buildings = buildings,
                    Name = companyDto.Name,
                    Employees = devs,
                    Projects = projects,
                };

                dbContext.Companies.Add(company);

                dbContext.SaveChanges();

                return company.CompanyId;
            }
        }

        public int Add(Company company)
        {
            throw new NotImplementedException();
        }

        public void Delete(CompanyDto company)
        {
            throw new NotImplementedException();
        }

        public void Update(CompanyDto company)
        {
            throw new NotImplementedException();
        }

	}
}