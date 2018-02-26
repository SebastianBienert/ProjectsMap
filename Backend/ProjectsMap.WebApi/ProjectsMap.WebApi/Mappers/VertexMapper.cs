using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectsMap.WebApi.DTOs;
using ProjectsMap.WebApi.Models;

namespace ProjectsMap.WebApi.Mapper
{
    public class VertexMapper
    {
        public static VertexDto GetVertexDto(Vertex vertex)
        {
            var dto = new VertexDto()
            {
                Id = vertex.VertexId,
                X = vertex.X,
                Y = vertex.Y
            };
            return dto;
        }
    }
}