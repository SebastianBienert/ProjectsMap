using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectsMap.WebApi.DTOs
{
    public class TechnologyDto
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public string Name { get; set; }

        public IEnumerable<int> EmployeesId { get; set; }

        public IEnumerable<int> ProjectsId { get; set; }
    }
}