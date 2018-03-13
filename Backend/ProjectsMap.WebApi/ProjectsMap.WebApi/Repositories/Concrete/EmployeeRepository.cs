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
                        .Include(d => d.Seat.Select(s => s.Vertex))
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
                    .Include(d => d.Seat.Select(s => s.Vertex))
                    .Include(d => d.Company)
                    .FirstOrDefault(x => x.EmployeeId == id);
            }
        }

        public int Add(EmployeeDto dto)
        {
            using (var dbContext = new EfDbContext())
            {
                var dev = new Employee(dto.FirstName, dto.Surname)
                {
                    CompanyId = dto.CompanyId,
                    EmployeeId = dto.Id,
                    Technologies = dbContext.Technologies.Where(x => dto.Technologies.Contains(x.Name)).ToList(),
                    Seat = dto.Seat == null ? null : new List<Seat>() { dbContext.Seats.FirstOrDefault(s => s.SeatId == dto.Seat.Id)}
                };

                    var user = new User
                    {                       
                        Created = DateTime.Now,
                        Employee = dev
                    };
                    dev.User = user;
           

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
                var dev = dbContext.Employees.FirstOrDefault(x => x.EmployeeId == employee.EmployeeId);
                dev = employee;
                dbContext.SaveChanges();
            }
        }
    }
}