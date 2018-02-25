using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectsMap.WebApi.DTOs;
using ProjectsMap.WebApi.Models;

namespace ProjectsMap.WebApi.Mapper
{
    public class RoomMapper
    {
        public static RoomDto GetRoomDto(Room room)
        {
            var dto = new RoomDto()
            {
                Vertexes = room.Vertexes.Select(v => VertexMapper.GetVertexDto(v)).ToList(),
                Projects = room.Projects.Select(p => p.Description).ToList(),
                Seats = room.Seats.Select(s => VertexMapper.GetVertexDto(s.Vertex)).ToList()
            };
            return dto;
        }
    }
}