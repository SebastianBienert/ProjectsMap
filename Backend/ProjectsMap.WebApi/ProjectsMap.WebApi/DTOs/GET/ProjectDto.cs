using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectsMap.WebApi.DTOs.GET;

namespace ProjectsMap.WebApi.DTOs
{
    public class ProjectDto
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public string Description { get; set; }

        public string RepositoryLink { get; set; }

        public string DocumentationLink { get; set; }

        public IEnumerable<EmployeeShortDto> Employees { get; set; }

        public IEnumerable<string> Technologies { get; set; }
    }


}