using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectsMap.WebApi.DTOs
{
    public class WallDto
    {
        public int Id { get; set; }

        public VertexDto StartVertex { get; set; }

        public VertexDto EndVertex { get; set; }
    }
}