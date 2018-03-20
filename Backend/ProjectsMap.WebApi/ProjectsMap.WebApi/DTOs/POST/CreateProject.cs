using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectsMap.WebApi.DTOs.POST
{
    public class CreateProject
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string RepositoryLink { get; set; }

        public string DocumentationLink { get; set; }

        public int? CompanyId { get; set; }

        public IEnumerable<EmployeeRole> EmployeesRoles { get; set; }

        public IEnumerable<string> Technologies { get; set; }
    }

    public class EmployeeRole
    {
        public int EmployeeId { get; set; }

        public int CompanyId { get; set; }

        public string Role { get; set; }
    }

}