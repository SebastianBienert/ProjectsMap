using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProjectsMap.WebApi.DTOs;
using ProjectsMap.WebApi.Services.Abstract;

namespace ProjectsMap.WebApi.Controllers
{
    [RoutePrefix("api/company")]
    public class CompanyController : ApiController
    {
        private ICompanyService _companyService;
		private IBuildingService _buildingService;

        public CompanyController(ICompanyService companyService, IBuildingService buildingService)
        {
            _companyService = companyService;
			_buildingService = buildingService;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {

			return Ok(_companyService.GetAllCompanies());
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetCompanyById")]
        public IHttpActionResult Get(int id)
        {
            var companyDto = _companyService.GetCompany(id);

            if (companyDto != null)
                return Ok(companyDto);
            else
                return NotFound();
        }

		[HttpGet]
		[Route("{id:int}/buildings")]
		public IHttpActionResult GetCompanyBuildingsList(int id)
		{
			var companyBuildingsList = _buildingService.GetBuildingsList(id);
			return Ok(companyBuildingsList);
			/*if (companyBuildingsList)
				return Ok(companyDto);
			else
				return NotFound();*/
		}


		[HttpPost]
        [Route("")]
        public IHttpActionResult Post([FromBody] CompanyDto company)
        {
            int createdId = _companyService.Post(company);

            return CreatedAtRoute("GetCompanyById", new { id = createdId }, company);
        }

    }
}
