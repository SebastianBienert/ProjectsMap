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

        public Seat(Developer developer, int xCoordinate, int yCoordinate)
        {
            Developer = developer;
            Vertex = new Vertex(xCoordinate, yCoordinate);
        }

        [Key]
        public int SeatId { get; set; }
        //Coordinate of the Seat on the given floor is described by (x,y) - left top corner of a square
        //One to one or zero relation (Vertex - Seat)
        public virtual Vertex Vertex { get; set; }

        //One to many relation(Room - Seats)
        public virtual Room Room { get; set; }
        public int RoomId { get; set; }

        //One-zero to many relation (Seat - Developer)
        public int? DeveloperId { get; set; }
        public virtual Developer Developer { get; set; }
    }
}