using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectsMap.WebApi.DTOs
{
    public class ProjectDto
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public string Description { get; set; }

        public string RepositoryLink { get; set; }

        public string DocumentationLink { get; set; }

        public int? CompanyId { get; set; }

        public IEnumerable<string> EmployeesNames { get; set; }

        public IEnumerable<RoomDto> Rooms { get; set; }

        public IEnumerable<TechnologyDto> Technologies { get; set; }
    }


}