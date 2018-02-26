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
                Id = developer.DeveloperId,
                FirstName = developer.FirstName,
                Surname = developer.Surname,
                Technologies = developer.Technologies.Select(x => x.Name).ToList(),
            };

            var seatDto = new SeatDto()
            {
                Id = developer.Seat == null ? 0 : developer.Seat.ToList()[0].SeatId,
                DeveloperId = developer.DeveloperId,
                Vertex = developer.Seat == null ? null : VertexMapper.GetVertexDto(developer.Seat.ToList()[0].Vertex),
                RooomId = developer.Seat.ToList()[0].SeatId
            };

            dto.Seat = seatDto;

            return dto;
        }
    }
}