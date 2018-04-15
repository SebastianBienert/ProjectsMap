using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using ProjectsMap.WebApi.DTOs;
using ProjectsMap.WebApi.Infrastructure;
using ProjectsMap.WebApi.Models;
using ProjectsMap.WebApi.Repositories.Abstract;
using ProjectsMap.WebApi.Services;
using ProjectsMap.WebApi.Services.Abstract;

namespace ProjectsMap.WebApi.Controllers
{
    [RoutePrefix("api/developers")]
    public class EmployeeController : ApiController
    {
        private IEmployeeService _service;


        public EmployeeController(IEmployeeService service)
        {
            _service = service;
        }
        [ClaimsAuthorization(ClaimType = "canReadUsers", ClaimValue = "true")]
        [HttpGet]
        [Route("pagination")]
        public IHttpActionResult GetAllPaingate(int page = 0, int pageSize = 10)
        {
            var allEmployees = _service.GetAllEmployees().ToList();
            var totalCount = allEmployees.Count;
            var pageCount = Math.Ceiling((double)totalCount / pageSize);
            var prevPage = page > 0 ? Url.Link("Employees", new { page = page - 1, pageSize = pageSize }) : "";
            var nextPage = page < pageCount - 1 ? Url.Link("Employees", new { page = page + 1, pageSize = pageSize }) : "";

            var filtered = allEmployees.Skip(page * pageSize).Take(pageSize);

            return Ok(new
            {
                TotalEmployees = totalCount,
                TotalPages = pageCount,
                PreviousPage = prevPage,
                NextPage = nextPage,
                Result = filtered
            });
        }

        [ClaimsAuthorization(ClaimType = "canReadUsers", ClaimValue = "true")]
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            var allEmployees = _service.GetAllEmployees().ToList();
            return Ok(allEmployees);
        }

        [ClaimsAuthorization(ClaimType = "canReadUsers", ClaimValue = "true")]
        [HttpGet]
        [Route("{id:int}", Name = "GetEmployeeById")]
        public IHttpActionResult Get(int id)
        {
            var developerDto = _service.GetEmployee(id);
            if (developerDto != null)
            {
                return Ok(developerDto);
            }
            return NotFound();
        }

        [ClaimsAuthorization(ClaimType = "canReadUsers", ClaimValue = "true")]
        [HttpGet]
        [Route("like/{query}")]
        public IHttpActionResult GetSearchedEmployee(string query)
        {
            var allEmployees = _service.GetEmployeesByQuery(query);

            if (allEmployees != null)
                return Ok(allEmployees);

            return NotFound();
        }

        [ClaimsAuthorization(ClaimType = "canReadUsers", ClaimValue = "true")]
        [HttpGet]
        [Route("technology/{technology}")]
        public IHttpActionResult Get(string technology)
        {
            var allEmployees = _service.GetDevelopersByTechnology(technology);

            if (allEmployees != null)
                return Ok(allEmployees);

            return NotFound();
        }

        [ClaimsAuthorization(ClaimType = "canReadUsers", ClaimValue = "true")]
        [HttpGet]
        [Route("technology/pagination/{technology}", Name = "GetEmployeesByTechnology")]
        public IHttpActionResult Get(string technology, int page = 0, int pageSize = 10)
        {
            var allEmployees = _service.GetDevelopersByTechnology(technology);

            if (allEmployees == null)
                return NotFound();

            var dtos = allEmployees.ToList();
            var totalCount = dtos.ToList().Count;
            var pageCount = Math.Ceiling((double)totalCount / pageSize);
            var prevPage = page > 0 ? Url.Link("GetEmployeesByTechnology", new { technology = technology, page = page - 1, pageSize = pageSize }) : "";
            var nextPage = page < pageCount - 1 ? Url.Link("GetEmployeesByTechnology", new { technology = technology, page = page + 1, pageSize = pageSize }) : "";

            var filtered = dtos.Skip(page * pageSize).Take(pageSize);

            return Ok(new
            {
                TotalEmployees = totalCount,
                TotalPages = pageCount,
                PreviousPage = prevPage,
                NextPage = nextPage,
                Result = filtered
            });
        }

        [ClaimsAuthorization(ClaimType = "canReadUsers", ClaimValue = "true")]
        [HttpGet]
        [Route("{id:int}/floor")]
        public IHttpActionResult GetEmployeeFloor(int id)
        {
            var employeeFloor = _service.GetEmployeeFloor(id);
            if (employeeFloor == null)
                return NotFound();
            return Ok(employeeFloor);
        }

        [HttpGet]
        [Route("{name}")]
        public IHttpActionResult GetEmployeeByName(string name)
        {
            var allEmployees = _service.GetEmployeesByName(name);

            if (allEmployees != null)
                return Ok(allEmployees);

            return NotFound();
        }

        [ClaimsAuthorization(ClaimType = "canReadUsers", ClaimValue = "true")]
        [HttpGet]
        [Route("pagination/{name}", Name = "GetEmployeesByName")]
        public IHttpActionResult GetEmployeeByName(string name, int page = 0, int pageSize = 10)
        {
            var allEmployees = _service.GetEmployeesByName(name);

            if (allEmployees == null)
                return NotFound();

            var dtos = allEmployees.ToList();
            var totalCount = dtos.ToList().Count;
            var pageCount = Math.Ceiling((double)totalCount / pageSize);
            var prevPage = page > 0 ? Url.Link("GetEmployeesByTechnology", new { name = name, page = page - 1, pageSize = pageSize }) : "";
            var nextPage = page < pageCount - 1 ? Url.Link("GetEmployeesByTechnology", new { name = name, page = page + 1, pageSize = pageSize }) : "";

            var filtered = dtos.Skip(page * pageSize).Take(pageSize);

            return Ok(new
            {
                TotalEmployees = totalCount,
                TotalPages = pageCount,
                PreviousPage = prevPage,
                NextPage = nextPage,
                Result = filtered
            });
        }

		[ClaimsAuthorization(ClaimType = "canReadUsers", ClaimValue = "true")]
        [HttpGet]
        [Route("photo/{id}", Name = "GetEmployeePhoto")]
        public IHttpActionResult GetPhoto(int id)
        {
            var path = _service.GetPhotoPath(id);
            if (path != null)
            {
                var result = new HttpResponseMessage(HttpStatusCode.OK);


                using (var filestream = File.Open(path, FileMode.Open))
                {
                    Image image = Image.FromStream(filestream);
                    MemoryStream memoryStream = new MemoryStream();
                    image.Save(memoryStream, ImageFormat.Jpeg);
                    result.Content = new ByteArrayContent(memoryStream.ToArray());
                    result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
                }

                return ResponseMessage(result);
            }

            return NotFound();
        }

		[ClaimsAuthorization(ClaimType = "canWriteUsers", ClaimValue = "true")]
        [HttpDelete]
        [Route("photo/{id}")]
        public IHttpActionResult Delete(int id)
        {
            if (_service.DeletePhoto(id))
            {
                return Ok();
            }

            return NotFound();
        }

		[ClaimsAuthorization(ClaimType = "canWriteUsers", ClaimValue = "true")]
        [HttpPost]
        [Route("")]
        public IHttpActionResult Post([FromBody] EmployeeDto employee)
        {
            int createdId = _service.Post(employee);
            return CreatedAtRoute("GetEmployeeById", new { id = createdId }, employee);
        }

		[ClaimsAuthorization(ClaimType = "canWriteUsers", ClaimValue = "true")]
        [HttpPost]
        [Route("photo/{id}")]
        public async Task<HttpResponseMessage> PostEmployeePhoto(int id)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            try
            {
                var httpRequest = HttpContext.Current.Request;
                foreach (string file in httpRequest.Files)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);

                    var postedFile = httpRequest.Files[file];
                    if (postedFile != null && postedFile.ContentLength > 0)
                    {

                        int MaxContentLength = 1024 * 1024 * 8; //Size = 8 MB  

                        IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
                        var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                        var extension = ext.ToLower();
                        if (!AllowedFileExtensions.Contains(extension))
                        {
                            dict.Add("error", "Please Upload image of type .jpg,.gif,.png.");
                            return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
    }
                        else if (postedFile.ContentLength > MaxContentLength)
                        {          
                            dict.Add("error", "Please Upload a file upto 8 mb.");
                            return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
}
                        else
                        {
                            var virtualPath = $"~/EmployeesPhoto/{id}{extension}";
                            var filePath = HttpContext.Current.Server.MapPath(virtualPath);

                            if(_service.AddPhotoToEmployee(id, virtualPath))
                                postedFile.SaveAs(filePath);
                            else
                            {
                                dict.Add("error", "Employee doesnt exist.");
                                return Request.CreateResponse(HttpStatusCode.NotFound, dict);
                            }
                            
                        }
                    }
                    return Request.CreateErrorResponse(HttpStatusCode.Created, "Image Updated Successfully."); ;
                }
                dict.Add("error", "Please Upload a image.");
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
            catch (Exception ex)
            {
                dict.Add("error", "Unexpected error ocurred.");
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
        }
    }

}

