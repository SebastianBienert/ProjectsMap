using ProjectsMap.WebApi.Repositories.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectsMap.WebApi.DTOs
{
    public class WallDto
    {
        public int Id { get; set; }

		public Vertex StartVertex { get; set; }

		public Vertex EndVertex { get; set; }

		public int MyProperty { get; set; }

		public string StateChanged { get; set; }
	}
}