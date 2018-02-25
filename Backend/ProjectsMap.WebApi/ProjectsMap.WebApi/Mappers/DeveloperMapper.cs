using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectsMap.WebApi.DTOs;
using ProjectsMap.WebApi.Models;

namespace ProjectsMap.WebApi.Mapper
{
    public class DeveloperMapper
    {
        public static DeveloperDto GetDeveloperDto(Developer developer)
        {
            var dto = new DeveloperDto()
            {
                FirstName = developer.FirstName,
                Surname = developer.Surname,
                Technologies = developer.Technologies.Select(x => x.Name).ToList(),
            };

            var seatDto = new SeatDto()
            {
                Developer = dto,
                Vertex = developer.Seat == null ? null : VertexMapper.GetVertexDto(developer.Seat.ToList()[0].Vertex),
                Rooom = developer.Seat == null ? null: RoomMapper.GetRoomDto(developer.Seat.ToList()[0].Room),
            };

            dto.Seat = seatDto;

            return dto;
        }
    }
}