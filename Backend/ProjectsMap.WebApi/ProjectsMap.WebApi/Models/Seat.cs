using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            Coordinates = new Tuple<int, int>(xCoordinate, yCoordinate);
        }

        [Key]
        public int SeatId { get; set; }

        public int DeveloperId { get; set; }

        public virtual Developer Developer { get; set; }

        //Coordinate of the Seat on the given floor is described by (x,y) - left top corner of a square
        public Tuple<int, int> Coordinates { get; set; }

    }
}