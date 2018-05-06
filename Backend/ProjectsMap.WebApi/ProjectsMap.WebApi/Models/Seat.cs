using ProjectsMap.WebApi.Repositories.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjectsMap.WebApi.Models
{
    public class Seat
    {

		public Seat() { }

        public Seat(int xCoordinate, int yCoordinate)
        {
			X = xCoordinate;
            Y  = yCoordinate;
        }
		public Seat(Vertex v)
		{
			X = v.X;
			Y = v.Y;
		}
		/*public Seat(Vertex seatVertex)
		{
			this.Vertex = seatVertex;
		}*/

		[Key]
        public int SeatId { get; set; }

        //Coordinate of the Seat on the given floor is described by (x,y) - left top corner of a square
        public int X { get; set; }

        public int Y { get; set; }

        //One to many relation(Room - Seats)
        public virtual Room Room { get; set; }
        public int RoomId { get; set; }

        //One-zero to many relation (Seat - Employee)
        public int? EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
		public object Vertex { get; internal set; }
	}
}