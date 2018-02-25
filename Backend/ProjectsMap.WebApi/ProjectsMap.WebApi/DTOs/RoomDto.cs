using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectsMap.WebApi.DTOs
{
    public class RoomDto
    {
        public IEnumerable<VertexDto> Vertexes { get; set; }

        public IEnumerable<VertexDto> Seats { get; set; }

        public IEnumerable<string> Projects { get; set; }

    }
}