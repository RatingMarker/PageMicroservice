using AutoMapper;
using Moq;
using Nancy;
using Nancy.Testing;
using PageMicroservice.Api.Controllers;
using PageMicroservice.Api.Mappers;
using PageMicroservice.Models;
using PageMicroservice.Services;
using Xunit;

namespace PageMicroservice.UnitTest.Controllers
{
    public class PageModuleTest
    {
        private readonly Browser browser;
        private readonly Mock<IPageService> mockPageService;

        public PageModuleTest()
        {
            mockPageService = new Mock<IPageService>();

            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.AddProfile<DomainToViewModelMappingProfile>();
                    cfg.AddProfile<ViewModelToDomainMappingProfile>();
                });

            browser = new Browser(
                with =>
                {
                    with.Module<PageModule>();
                    with.Dependency<IPageService>(mockPageService.Object);
                    with.Dependency<IMapper>(config.CreateMapper());
                },
                defaults: to => to.Accept("application/json"));
        }

        [Fact]
        public void Should_return_pages_when_get_all()
        {
            //arrange
            var url = @"/pages";

            //act
            var result = browser.Get(
                url,
                with => { with.HttpRequest(); });

            //assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public void Should_return_newpage_when_add_page()
        {
            //arrange
            var url = @"/pages";

            //act
            var result = browser.Post(
                url,
                with =>
                {
                    with.HttpRequest();
                    with.FormValue("uri", "http://lenta.ru/news");
                });

            //assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            mockPageService.Verify(m => m.Add(It.IsAny<Page>()), Times.Once);
        }

        [Fact]
        public void Should_return_status_ok_when_update_page()
        {
            //arrange
            var url = @"/pages/1";

            mockPageService.Setup(x => x.Update(It.IsAny<Page>())).Returns(true);
            //act
            var result = browser.Put(
                url,
                with => { with.HttpRequest(); });

            //assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public void Should_return_status_ok_when_remove_page()
        {
            //arrange
            var url = @"/pages/1";
            mockPageService.Setup(x => x.Remove(It.IsAny<Page>())).Returns(true);
            //act
            var result = browser.Delete(
                url,
                with => { with.HttpRequest(); });

            //assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }
    }
}