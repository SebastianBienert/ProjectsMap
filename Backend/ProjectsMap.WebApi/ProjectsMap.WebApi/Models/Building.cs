using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectsMap.WebApi.Models
{
    public class Building
    {
        [Key]
        public int BuildingId { get; set; }

        public string Address { get; set; }

        public virtual Company Company { get; set; }

        public int? CompanyId { get; set; }

        public virtual ICollection<Floor> Floors { get; set; }
    }
}