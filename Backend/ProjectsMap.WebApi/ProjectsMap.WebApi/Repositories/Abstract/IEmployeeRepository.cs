using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectsMap.WebApi.DTOs;
using ProjectsMap.WebApi.Models;

namespace ProjectsMap.WebApi.Repositories.Abstract
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> Developers { get; }

        Employee Get(int id);

        int Add(EmployeeDto employee);

        int Add(Employee employee);

        void Delete(Employee employee);

        void Update(Employee employee);

    }
}
