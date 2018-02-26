using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectsMap.WebApi.DTOs
{
    public class ProjectDto
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public IEnumerable<DeveloperDto> Developers { get; set; }

        public IEnumerable<RoomDto> Rooms { get; set; }

        public IEnumerable<TechnologyDto> Technologies { get; set; }
    }
}