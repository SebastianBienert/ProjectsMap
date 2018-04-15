using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectsMap.WebApi.Models;
using ProjectsMap.WebApi.Repositories.Abstract;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using ProjectsMap.WebApi.DTOs;

namespace ProjectsMap.WebApi.Repositories.Concrete
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public IEnumerable<Employee> Employees
        {
            get
            {
                using (var dbContext = new EfDbContext())
                {
                    return dbContext.Employees
                        .Include(d => d.Technologies)
                        .Include(d => d.Seat)
                        .Include(d => d.Company).ToList();
                }
            }
        }

        public Employee Get(int id)
        {
            using (var dbContext = new EfDbContext())
            {
                return dbContext.Employees.
                    Include(d => d.Technologies)
                    .Include(d => d.Seat)
                    .Include(d => d.Company)
                    .FirstOrDefault(x => x.EmployeeId == id);
            }
        }

        public int Add(EmployeeDto dto)
        {
            using (var dbContext = new EfDbContext())
            {
                var existingTechnologies = dbContext.Technologies.ToList();
                var existingTechnologiesNames = existingTechnologies.Select(x => x.Name).ToList();
                var newTechnologiesNames = dto.Technologies.Except(existingTechnologiesNames).ToList();
                var dev = new Employee(dto.FirstName, dto.Surname)
                {
                    CompanyId = dto.CompanyId,
                    EmployeeId = dto.Id,
                    ManagerId = dto.ManagerId,
                    ManagerCompanyId = dto.ManagerCompanyId,
                    JobTitle = dto.JobTitle,
                    Company = dbContext.Companies.FirstOrDefault(c => c.CompanyId == dto.CompanyId),
                    Email = dto.Email,
                    Technologies = dto.Technologies == null ? null : existingTechnologies.Where(x => dto.Technologies.Contains(x.Name)).ToList(),
                    Seat = dto.Seat == null ? null:  dbContext.Seats.FirstOrDefault(s => s.SeatId == dto.Seat.Id)
                };
                
                //ADD NEW TECHNOLOGIES
                foreach (var technology in newTechnologiesNames)
                {
                    Technology tech = new Technology()
                    {
                        Name = technology,
                        Employees = new List<Employee>() {dev},
                    };
                    dev.Technologies.Add(tech);
                }

                dbContext.Employees.Add(dev);
                dbContext.SaveChanges();
                return dev.EmployeeId;
            }
        }

        public int Add(Employee employee)
        {
            using (var dbContext = new EfDbContext())
            {
                dbContext.Employees.Add(employee);
                dbContext.SaveChanges();

                return employee.EmployeeId;
            }
        }

        public void Delete(Employee employee)
        {
            using (var dbContext = new EfDbContext())
            {
                dbContext.Employees.Remove(employee);
                dbContext.SaveChanges();
            }
        }

        public void Update(Employee employee)
        {
            using (var dbContext = new EfDbContext())
            {
                dbContext.Entry(employee).State = EntityState.Modified;
                dbContext.SaveChanges();
            }
        }
    }
}