﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectsMap.WebApi.DTOs
{
    public class BuildingDto
    {
        public int Id { get; set; }

        public string Address { get; set; }
		public IEnumerable<int> FloorsIds { get; set; }

		public int? CompanyId { get; set; }

	}
}