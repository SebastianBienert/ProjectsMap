﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProjectsMap.WebApi.Models;
using ProjectsMap.WebApi.Repositories.Abstract;
using ProjectsMap.WebApi.Services.Concrete;
using ProjectsMap.WebApi.DTOs;

namespace ProjectsMap.WebApi.Controllers
{
    [RoutePrefix("api/room")]
    public class RoomController : ApiController
    {
        private IRoomService _service;

        public RoomController(IRoomService roomService)
        {
            _service = roomService;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            return Ok(_service.GetAllRooms());
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            var room = _service.GetRoom(id);

            if (room != null)
                return Ok(room);
            else
                return NotFound();
        }

		[HttpPost]
        [Route("")]
        public IHttpActionResult Post([FromBody] RoomDto roomDto)
        {
			//int createdId = _service.Post(roomDto);
            return Ok();//for now
        }

    }
}
