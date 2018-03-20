using ProjectsMap.WebApi.Repositories.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectsMap.WebApi.DTOs
{
    public class SeatDto
    {
        public int Id { get; set; }

		public Vertex Vertex { get; set; }

		public int RoomId { get; set; }

        public int? DeveloperId { get; set; }
    }
}