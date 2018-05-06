using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectsMap.WebApi.Models;
using ProjectsMap.WebApi.Repositories.Abstract;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using ProjectsMap.WebApi.DTOs;
using ProjectsMap.WebApi.Infrastructure;

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
                        .Include(d => d.ProjectRoles.Select(pr => pr.Project))
                        .Include(d => d.Seat).ToList();
                }
            }
        }

        public Employee Get(int id)
        {
            using (var dbContext = new EfDbContext())
            {
                return dbContext.Employees.
                    Include(d => d.Technologies)
                    .Include(d => d.ProjectRoles.Select(pr => pr.Project))
                    .Include(d => d.Seat)
                    .FirstOrDefault(x => x.EmployeeId == id);
            }
        }

        public int Add(EmployeeDto dto, ApplicationUser user)
        {
            using (var dbContext = new EfDbContext())
            {
              //  dbContext.Entry(user).State = EntityState.Unchanged;
                
                var existingTechnologies = dbContext.Technologies.ToList();
                var existingTechnologiesNames = existingTechnologies.Select(x => x.Name).ToList();
                var newTechnologiesNames = dto.Technologies?.Except(existingTechnologiesNames).ToList();
                var dev = new Employee(dto.FirstName, dto.Surname)
                {
                    EmployeeId = dto.Id,
                    ManagerId = dto?.ManagerId,
                    JobTitle = dto?.JobTitle,
                    Email = dto?.Email,
                    Technologies = dto.Technologies == null ? null : existingTechnologies.Where(x => dto.Technologies.Contains(x.Name)).ToList(),
                    Seat = dto.Seat == null ? null : dbContext.Seats.FirstOrDefault(s => s.SeatId == dto.Seat.Id),
                    //ApplicationUser = user
                };
                
                //ADD NEW TECHNOLOGIES
                if (newTechnologiesNames != null)
                    foreach (var technology in newTechnologiesNames)
                    {
                        Technology tech = new Technology()
                        {
                            Name = technology,
                            Employees = new List<Employee>() {dev},
                        };
                        dev.Technologies.Add(tech);
                    }

                user.Employee = dev;
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

        public void Update(int employeeId, EmployeeDto employeeDto)
        {
            using (var dbContext = new EfDbContext())
            {
                var entity = dbContext.Employees.
                    Include(d => d.Technologies)
                    .FirstOrDefault(x => x.EmployeeId == employeeId);
                if (entity != null)
                {
                    var existingTechnologies = dbContext.Technologies.ToList();
                    var existingTechnologiesNames = existingTechnologies.Select(x => x.Name).ToList();
                    var newTechnologiesNames = employeeDto.Technologies.Except(existingTechnologiesNames).ToList();

                    entity.FirstName = employeeDto.FirstName;
                    entity.JobTitle = employeeDto.JobTitle;
                    entity.Surname = employeeDto.Surname;
                    entity.Email = employeeDto.Email;
                    entity.EmployeeId = employeeDto.Id;
                    entity.Manager = employeeDto.ManagerId == null
                        ? null : dbContext.Employees.FirstOrDefault(e => e.EmployeeId == employeeDto.ManagerId);
                    
                    entity.Technologies = employeeDto.Technologies == null
                        ? new List<Technology>()
                        : existingTechnologies.Where(x => employeeDto.Technologies.Contains(x.Name)).ToList();
                  
                    foreach (var technology in newTechnologiesNames)
                    {
                        Technology tech = new Technology()
                        {
                            Name = technology,
                            Employees = new List<Employee>() { entity },
                        };
                        entity.Technologies.Add(tech);
                    }

                    dbContext.SaveChanges();
                }
              
            }
        }
    }
}