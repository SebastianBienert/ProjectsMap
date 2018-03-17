using ProjectsMap.WebApi.Repositories.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectsMap.WebApi.DTOs
{
    public class RoomDto
    {
        public int Id { get; set; }

        public IEnumerable<WallDto> Walls { get; set; }

        public IEnumerable<Vertex> Seats { get; set; }
    }
}