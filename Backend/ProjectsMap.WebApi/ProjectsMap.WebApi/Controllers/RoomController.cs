using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProjectsMap.WebApi.Models;
using ProjectsMap.WebApi.Repositories.Abstract;
using ProjectsMap.WebApi.Services.Concrete;

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
        public IHttpActionResult Post(Room room)
        {
            _service.Post(room);
            return Ok();
        }

        [HttpDelete]
        [Route("")]
        public IHttpActionResult Delete(Room room)
        {
            _service.Delete(room);
            return Ok();
        }

        [HttpPut]
        [Route("")]
        public IHttpActionResult Update(Room room)
        {
            _service.Update(room);
            return Ok();
        }
    }
}
