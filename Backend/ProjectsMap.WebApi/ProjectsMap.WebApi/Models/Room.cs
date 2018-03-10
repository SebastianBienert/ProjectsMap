using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectsMap.WebApi.Models
{
    public class Room
    {
		public Room() { }
		public Room(ICollection<Wall> walls)
		{
			Walls = walls;
		}
		[Key]
        public int RoomId { get; set; }

        //One to many relation [Floor - Room]
        public virtual Floor Floor { get; set; }

        public int? FloorId { get; set; }

        //Shape of the room is described by the list of edges
        public virtual ICollection<Wall> Walls { get; set; }

        //One to many relation (Room - Seats)
        public virtual ICollection<Seat> Seats { get; set; }
    }
}