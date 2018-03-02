using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectsMap.WebApi.DTOs
{
    public class SeatDto
    {
        public int Id { get; set; }

        public VertexDto Vertex { get; set; }

        public int RooomId { get; set; }

        public int? DeveloperId { get; set; }
    }
}