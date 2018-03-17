using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectsMap.WebApi.Models
{
    public class Floor
    {
        [Key]
        public int FloorId { get; set; }

        public string Description { get; set; }

		public int FloorNumber { get; set; }

		public virtual Building Building { get; set; }

        public int BuildingId { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }

        public virtual ICollection<Wall> Walls { get; set; }

    }
}