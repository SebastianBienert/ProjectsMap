using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProjectsMap.WebApi.Models;
using ProjectsMap.WebApi.Repositories.Abstract;

namespace ProjectsMap.WebApi.Controllers
{
    [RoutePrefix("api/room")]
    public class RoomController : ApiController
    {
        private IRoomRepository _repository;

        public RoomController(IRoomRepository roomRepository)
        {
            _repository = roomRepository;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            return Ok(_repository.Rooms);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            var floor = _repository.Get(id);

            if (floor != null)
                return Ok(floor);
            else
                return NotFound();
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Post(Room room)
        {
            _repository.Add(room);
            return Ok();
        }

        [HttpDelete]
        [Route("")]
        public IHttpActionResult Delete(Room room)
        {
            _repository.Delete(room);
            return Ok();
        }

        [HttpPut]
        [Route("")]
        public IHttpActionResult Update(Room room)
        {
            _repository.Update(room);
            return Ok();
        }
    }
}
