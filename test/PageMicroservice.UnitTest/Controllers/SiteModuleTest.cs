using System;
using System.Collections.Generic;
using Moq;
using Nancy;
using Nancy.Testing;
using PageMicroservice.Api.Controllers;
using PageMicroservice.Api.Models;
using PageMicroservice.Api.Services;
using Xunit;

namespace PageMicroservice.UnitTest.Controllers
{
    public class SiteModuleTest
    {
        private readonly Browser browser;
        private readonly Mock<ISiteService> mockSiteService;

        public SiteModuleTest()
        {
            mockSiteService = new Mock<ISiteService>();
            browser = new Browser(
                with =>
                {
                    with.Module<SiteModule>();
                    with.Dependency<ISiteService>(mockSiteService.Object);
                },
                defaults: to => to.Accept("application/json"));
        }

        [Fact]
        public void Should_return_status_ok_when_route_exists()
        {
            //arrange
            var sites = new List<Site>()
            {
                new Site()
                {
                    SiteId = 1,
                    Name = "lenta.ru",
                    Pages = new List<Page>()
                    {
                        new Page()
                        {
                            Uri = "http://lenta.ru/",
                            FoundDate = DateTime.Now.AddDays(-1),
                            LastScanDate = DateTime.Now
                        },
                        new Page()
                        {
                            Uri = "http://lenta.ru/news/1",
                            FoundDate = DateTime.Now.AddDays(-1),
                            LastScanDate = DateTime.Now
                        }
                    }
                }
            };

            mockSiteService.Setup(x => x.GetAll()).Returns(sites);

            //act
            var result = browser.Get(
                "/sites",
                with => { with.HttpRequest(); });

            //assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public void Should_return_newsite_when_post_site()
        {
            //arrange

            //act
            var result = browser.Post(
                "/sites",
                with =>
                {
                    with.HttpRequest();
                    with.FormValue("name", "lenta.ru");
                });

            //assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            mockSiteService.Verify(m => m.Add(It.IsAny<Site>()), Times.Once);
        }

        [Fact]
        public void Should_return_status_ok_when_update_site()
        {
            //arrange
            var url = @"/sites/1";
            mockSiteService.Setup(x => x.Update(It.IsAny<Site>())).Returns(true);

            //act
            var result = browser.Put(
                url,
                with =>
                {
                    with.HttpRequest();
                    with.FormValue("name", "lenta.ru/news");
                });

            //assert
            mockSiteService.Verify(m => m.Update(It.IsAny<Site>()), Times.Once);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public void Should_return_status_ok_when_delete_site()
        {
            //arrange
            var url = @"/sites/1";
            mockSiteService.Setup(x => x.Remove(It.IsAny<Site>())).Returns(true);
            //act
            var result = browser.Delete(
                url,
                with =>
                {
                    with.HttpRequest();
                });

            //assert
            mockSiteService.Verify(m => m.Remove(It.IsAny<Site>()), Times.Once);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }
    }
}