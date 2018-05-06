using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectsMap.WebApi.DTOs;
using ProjectsMap.WebApi.Infrastructure;
using ProjectsMap.WebApi.Models;

namespace ProjectsMap.WebApi.Repositories.Abstract
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> Employees { get; }

        Employee Get(int id);

        int Add(EmployeeDto employee, ApplicationUser user);

        int Add(Employee employee);

        void Delete(Employee employee);

        void Update(Employee employee);

        void Update(int employeeId, EmployeeDto employee);

    }
}
