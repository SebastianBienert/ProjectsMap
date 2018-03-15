using ProjectsMap.WebApi.Repositories.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectsMap.WebApi.Models
{
    public class Wall
    {
		public Wall(Vertex start, Vertex end)
		{
			this.StartVertexX = start.X;
			this.StartVertexY = start.Y;
			this.EndVertexX = end.X;
			this.EndVertexY = end.Y;
		}

		public Wall()
		{
		}

		[Key]
        public int WallId { get; set; }

        public int StartVertexX { get; set; }
        public int StartVertexY { get; set; }

        public int EndVertexX { get; set; }
        public int EndVertexY { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }

        public int? FloorId { get; set; }
        public virtual Floor Floor { get; set; }

    }
}