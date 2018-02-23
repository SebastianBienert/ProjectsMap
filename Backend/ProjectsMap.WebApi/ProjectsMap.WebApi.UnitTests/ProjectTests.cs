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
            var devList = new List<Developer>()
            {
                new Developer()
                {
                    Id = 1,
                    FirstName = "Jan",
                    Surname = "Kowalski",
                    Technologies = new List<string>() {"AngularJS", "VueJS"}
                },
                new Developer()
                {
                    Id = 2,
                    FirstName = "Karol",
                    Surname = "Nowak",
                    Technologies = new List<string>() {"AngularJS", ".NET Framework"}
                },
                new Developer()
                {
                    Id = 3,
                    FirstName = "Piotr",
                    Surname = "Nowak",
                    Technologies = new List<string>() {"JavaScript", "PHP"}
                }
            };
            var projectList = new List<Project>()
            {
                new Project()
                {
                    Id = 1,
                    Description = "Projects Map",
                    Developers = new List<Developer> {devList[0], devList[1]},
                    UsedTechnologies = new List<string> {"AngularJS"}
                },
                new Project()
                {
                    Id = 2,
                    Description = "Simple store",
                    Developers = new List<Developer> {devList[2], devList[1]},
                    UsedTechnologies = new List<string> {".NET Framework"}
                },
                new Project()
                {
                    Id = 3,
                    Description = "Web browser game",
                    Developers = new List<Developer> {devList[2], devList[1]},
                    UsedTechnologies = new List<string> {"Unity"}
                }
            };

            var repositoryMock = new Mock<IProjectRepository>();
            repositoryMock.Setup(x => x.Projects).Returns(projectList);
            repositoryMock.Setup(x => x.Get(It.IsAny<int>()))
                .Returns((int id) =>
                {
                    return projectList.FirstOrDefault(y => y.Id == id);
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
            Assert.AreEqual(1, validResult.Content.Id);
            Assert.AreEqual(2, validResult.Content.Developers.Count());


            Assert.IsInstanceOf(typeof(NotFoundResult), badResult);
        }
    }
}
