using ProjectsMap.WebApi.DTOs;
using ProjectsMap.WebApi.Services.Abstract;
using ProjectsMap.WebApi.Services.Concrete;
using System;
using System.Collections.Generic;
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

namespace ProjectsMap.WebApi.Controllers
{
	[RoutePrefix("api/floor")]
	public class FloorController : ApiController
	{
		private IFloorService _service;

		public FloorController(IFloorService floorService)
		{
			_service = floorService;
		}

		[HttpGet]
		[Route("")]
		public IHttpActionResult GetAll()
		{
			return Ok(_service.GetFloorsList().Select(x => new { x.Id, x.BuildingId, x.Description }));
		}

        [HttpGet]
        [Route("allInformation")]
        public IHttpActionResult GetAllInformation()
        {
            return Ok(_service.GetAllInformationFloors());
        }


        [HttpGet]
		[Route("{id:int}")]
		public IHttpActionResult Get(int id)
		{
			var floor = _service.GetFloor(id);

			if (floor != null)
				return Ok(floor);
			else
				return NotFound();
		}

		[HttpPut]
		[Route("{id:int}")]
		public IHttpActionResult Put(FloorDto floorDto)
		{
			_service.Update(floorDto);

			return Ok();
		}

		[HttpPost]
		[Route("")]
		public IHttpActionResult Post([FromBody] FloorDto floorDto)
		{
			int createdId = _service.Post(floorDto);
			return Ok(createdId);//for now
		}

		[HttpPost]
		[Route("photo/{id}")]
		public async Task<HttpResponseMessage> PostMapPhoto(int id)
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
							var virtualPath = $"~/EmployeesPhoto/map{id}{extension}";
							var filePath = HttpContext.Current.Server.MapPath(virtualPath);

							if (_service.AddPhotoToMap(id, virtualPath))
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

		[HttpGet]
        [Route("photo/{id}", Name = "GetBackgroundImage")]
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
	}
}
