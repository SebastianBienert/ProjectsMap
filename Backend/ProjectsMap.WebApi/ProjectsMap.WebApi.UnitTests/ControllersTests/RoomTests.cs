using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using ProjectsMap.WebApi.Controllers;
using ProjectsMap.WebApi.DTOs;
using ProjectsMap.WebApi.Models;
using ProjectsMap.WebApi.Repositories.Abstract;
using ProjectsMap.WebApi.Services.Concrete;

namespace ProjectsMap.WebApi.UnitTests
{
    [TestFixture]
    public class RoomTests
    {
        private RoomController _controller;
        private Mock<IRoomRepository> _repoMock;

        [SetUp]
        public void Init()
        {
            var roomList = TestsData.RoomList.ToList();
            _repoMock = new Mock<IRoomRepository>();

            _repoMock.Setup(x => x.Rooms).Returns(roomList);
            _repoMock.Setup(x => x.Get(It.IsAny<int>()))
                .Returns((int id) =>
                {
                    return roomList.FirstOrDefault(y => y.RoomId == id);
                });



            var service = new RoomService(_repoMock.Object,null,null);
            
            _controller = new RoomController(service);
            _controller.Request = new HttpRequestMessage();
            _controller.Configuration = new HttpConfiguration();
        }

        [Test]
        public void Given_valid_id_web_api_should_return_room_otherwise_404()
        {
            var validResult = _controller.Get(2) as OkNegotiatedContentResult<RoomDto>;
            var badResult = _controller.Get(3);

            Assert.IsNotNull(validResult);
            Assert.IsNotNull(validResult.Content);
            Assert.AreEqual(2, validResult.Content.Id);
           
            Assert.IsInstanceOf(typeof(NotFoundResult), badResult);
        }

        [Test]
        public void Can_Add_New_Room_Using_Room_Controller()
        {
            var room = new Room()
            {
                RoomId = 13,
                Projects = new List<Project>(),
                Seats = new List<Seat>(),
                Vertexes = new List<Vertex>()
            };

            _controller.Post(room);

            //Assert
            _repoMock.Verify(v => v.Add(It.Is<Room>(r => r.RoomId == room.RoomId)));
        }



    }
}
