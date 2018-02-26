using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectsMap.WebApi.DTOs;
using ProjectsMap.WebApi.Mapper;
using ProjectsMap.WebApi.Models;

namespace ProjectsMap.WebApi.Mappers
{
    public class SeatMapper
    {
        public static SeatDto GetSeatDto(Seat seat)
        {
            var result = new SeatDto()
            {
                Id = seat.SeatId,
                DeveloperId = seat.DeveloperId,
                RooomId = seat.RoomId,
                Vertex = VertexMapper.GetVertexDto(seat.Vertex)
            };
            return result;
        }
    }
}