using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectsMap.WebApi.DTOs
{
	public class FloorDto
	{
		public int Id { get; set; }

		public string Description { get; set; }
		public int FloorNumber { get; set; }

		public int BuildingId { get; set; }

		public IEnumerable<WallDto> Walls { get; set; }

		public IEnumerable<RoomDto> Rooms { get; set; }


	}
}