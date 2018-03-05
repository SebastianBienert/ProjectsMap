using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectsMap.WebApi.DTOs
{
    public class CompanyDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<BuildingDto> Buildings { get; set; }

        public IEnumerable<DeveloperDto> Developers { get; set; }

        public IEnumerable<ProjectDtoShort> ProjectsId { get; set; }
    }

    public class ProjectDtoShort
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }


}