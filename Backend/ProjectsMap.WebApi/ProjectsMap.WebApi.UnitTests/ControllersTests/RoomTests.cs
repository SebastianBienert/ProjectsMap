using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using ProjectsMap.WebApi.Controllers;
using ProjectsMap.WebApi.Models;
using ProjectsMap.WebApi.Repositories.Abstract;

namespace ProjectsMap.WebApi.UnitTests
{
    [TestFixture]
    public class RoomTests
    {
        private RoomController _controller;

        [SetUp]
        public void Init()
        {
            var roomList = TestsData.RoomList.ToList();
            var repositoryMock = new Mock<IRoomRepository>();

            repositoryMock.Setup(x => x.Rooms).Returns(roomList);
            repositoryMock.Setup(x => x.Get(It.IsAny<int>()))
                .Returns((int id) =>
                {
                    return roomList.FirstOrDefault(y => y.RoomId == id);
                });

            _controller = new RoomController(repositoryMock.Object);
            _controller.Request = new HttpRequestMessage();
            _controller.Configuration = new HttpConfiguration();
        }

        [Test]
        public void Given_valid_id_web_api_should_return_room_otherwise_404()
        {
            var validResult = _controller.Get(2) as OkNegotiatedContentResult<Room>;
            var badResult = _controller.Get(3);

            Assert.IsNotNull(validResult);
            Assert.IsNotNull(validResult.Content);
            Assert.AreEqual(2, validResult.Content.RoomId);
           
            Assert.IsInstanceOf(typeof(NotFoundResult), badResult);
        }
    }
}
