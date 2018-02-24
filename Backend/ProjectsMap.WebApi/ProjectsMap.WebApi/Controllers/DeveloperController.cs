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
    [RoutePrefix("api/developers")]
    public class DeveloperController : ApiController
    {
        private IDeveloperRepository _repository;

        public DeveloperController(IDeveloperRepository developerRepository)
        {
            _repository = developerRepository;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            return Ok( _repository.Developers.ToList());
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            var developer = _repository.Get(id);

            if (developer != null)
                return Ok(developer);
            else
                return NotFound();
        }

        [HttpGet]
        [Route("technology/{technology}")]
        public IHttpActionResult Get(string technology)
        {
            var list = _repository.Developers.Where(x => x.Technologies.Select(t => t.Name).ToList().Contains(technology));
            if (list.Count() > 0)
            {
                return Ok(list);
            }
            else
            {
                return NotFound();
            }
        }


        [HttpPost]
        [Route("")]
        public IHttpActionResult Post(Developer developer)
        {
            _repository.Add(developer);
            return Ok();
        }

        [HttpDelete]
        [Route("")]
        public IHttpActionResult Delete(Developer developer)
        {
            _repository.Delete(developer);
            return Ok();
        }

        [HttpPut]
        [Route("")]
        public IHttpActionResult Update(Developer developer)
        {
            _repository.Update(developer);
            return Ok();
        }


    }
}
