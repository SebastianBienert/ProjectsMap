using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectsMap.WebApi.DTOs
{
    public class SeatDto
    {
        public VertexDto Vertex { get; set; }

        public RoomDto Rooom { get; set; }

        public DeveloperDto Developer { get; set; }
    }
}