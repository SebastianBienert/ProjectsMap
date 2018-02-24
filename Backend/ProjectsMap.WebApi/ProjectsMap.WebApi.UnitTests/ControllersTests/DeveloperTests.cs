using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using ProjectsMap.WebApi.Controllers;
using ProjectsMap.WebApi.Models;
using ProjectsMap.WebApi.Repositories.Abstract;
using Assert = NUnit.Framework.Assert;

namespace ProjectsMap.WebApi.UnitTests
{
    [TestFixture]
    public class DeveloperTests
    {
        private DeveloperController _controller;
        [SetUp]
        public void Init()
        {
            var devList = TestsData.DevList.ToList();
            var repositoryMock = new Mock<IDeveloperRepository>();

            repositoryMock.Setup(x => x.Developers).Returns(devList);
            repositoryMock.Setup(x => x.Get(It.IsAny<int>()))
                .Returns((int id) =>
                {
                    return devList.FirstOrDefault(y => y.DeveloperId == id);
                });

            _controller = new DeveloperController(repositoryMock.Object);
            _controller.Request = new HttpRequestMessage();
            _controller.Configuration = new HttpConfiguration();
        }

        [Test]
        public void Given_existing_id_web_api_should_return_proper_developer()
        {
            var response = _controller.Get(2);
            var result = response as OkNegotiatedContentResult<Developer>;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
            Assert.AreEqual(2, result.Content.DeveloperId);
        }

        [Test]
        public void Given_non_existing_id_web_api_should_return_404_not_found()
        {
            var result = _controller.Get(5);
            
            Assert.IsInstanceOf(typeof(NotFoundResult), result);

        }

        [Test]
        public void Web_api_should_return_all_developers_from_repository()
        {
            var result = _controller.GetAll() as OkNegotiatedContentResult<List<Developer>>;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
            Assert.AreEqual(3, result.Content.Count());
        }

        [Test]
        public void Given_Existing_Technology_Web_Api_Should_Return_All_Developers_Using_It()
        {
            var resultAngular = _controller.Get("#AngularJS") as OkNegotiatedContentResult<IEnumerable<Developer>>;
            var resultVue = _controller.Get("#VueJS") as OkNegotiatedContentResult<IEnumerable<Developer>>;


            Assert.IsNotNull(resultVue);
            Assert.IsNotNull(resultAngular);
            Assert.AreEqual(2, resultAngular.Content.Count());
            Assert.AreEqual(1, resultVue.Content.Count());
        }

        [Test]
        public void Given_Non_Existing_Techonolgy_Web_Api_Should_Return_Not_Found()
        {
            var resultCplus = _controller.Get("C++");

            Assert.IsInstanceOf(typeof(NotFoundResult), resultCplus);
        }
    }
}
