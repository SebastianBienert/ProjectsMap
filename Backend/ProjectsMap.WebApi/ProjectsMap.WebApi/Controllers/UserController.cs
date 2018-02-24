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
    [RoutePrefix("api/users")]
    public class UserController : ApiController
    {
        private IUserRepository _repository;

        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            return Ok(_repository.Users);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            var user = _repository.Get(id);

            if (user != null)
                return Ok(user);
            else
                return NotFound();
        }
   
        [HttpPost]
        [Route("")]
        public IHttpActionResult Post(User user)
        {
            _repository.Add(user);
            return Ok();
        }

        [HttpDelete]
        [Route("")]
        public IHttpActionResult Delete(User user)
        {
            _repository.Delete(user);
            return Ok();
        }

        [HttpPut]
        [Route("")]
        public IHttpActionResult Update(User user)
        {
            _repository.Update(user);
            return Ok();
        }


    }
}
