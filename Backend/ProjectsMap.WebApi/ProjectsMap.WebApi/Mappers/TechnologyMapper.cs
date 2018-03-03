using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectsMap.WebApi.DTOs;
using ProjectsMap.WebApi.Models;

namespace ProjectsMap.WebApi.Mappers
{
    public class TechnologyMapper
    {
        public static TechnologyDto GeTechnologyDto(Technology technology)
        {
            var dto = new TechnologyDto()
            {
                Name = technology.Name,
                Id = technology.TechnologyId,
                DevelopersId = technology.Developers.Select(x => x.DeveloperId).ToList(),
                ProjectsId = technology.Projects.Select(p => p.ProjectId).ToList()
            };

            return dto;
        }
    }
}