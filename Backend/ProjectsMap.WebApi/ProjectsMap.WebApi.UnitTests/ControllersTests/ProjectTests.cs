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
    public class ProjectTests
    {
        private ProjectController _controller;

        [SetUp]
        public void Init()
        {
            var projectList = TestsData.ProjectsList.ToList();

            var repositoryMock = new Mock<IProjectRepository>();
            repositoryMock.Setup(x => x.Projects).Returns(projectList);
            repositoryMock.Setup(x => x.Get(It.IsAny<int>()))
                .Returns((int id) =>
                {
                    return projectList.FirstOrDefault(y => y.ProjectId == id);
                });

            _controller = new ProjectController(repositoryMock.Object);
            _controller.Request = new HttpRequestMessage();
            _controller.Configuration = new HttpConfiguration();

        }


        [Test]
        public void Given_valid_id_web_api_should_return_proper_project_otherwise_404()
        {
            var validResult = _controller.Get(1) as OkNegotiatedContentResult<Project>;
            var badResult = _controller.Get(25);

            Assert.IsNotNull(validResult);
            Assert.IsNotNull(validResult.Content);
            Assert.AreEqual(1, validResult.Content.ProjectId);
            Assert.AreEqual(2, validResult.Content.Developers.Count());


            Assert.IsInstanceOf(typeof(NotFoundResult), badResult);
        }
    }
}
