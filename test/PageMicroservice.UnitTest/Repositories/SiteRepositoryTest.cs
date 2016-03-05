using System.Collections.Generic;
using System.Linq;
using PageMicroservice.Api.Models;
using PageMicroservice.Api.Repositories;
using PageMicroservice.UnitTest.Fakes;
using Xunit;

namespace PageMicroservice.UnitTest.Repositories
{
    public class SiteRepositoryTest
    {
        private ISiteRepository siteRepository;

        public SiteRepositoryTest()
        {
            //IContextFactory contextFactory = new MemoryContextFactory();
            siteRepository = new SiteRepository(new MemoryContextFactory());
        }

        [Fact]
        public void Should_return_true_when_update_site()
        {
            //arrange
            var page = new Page()
            {
                Url = "http://lenta.ru"
            };

            var site = new Site()
            {
                SiteId = 1,
                Name = "lenta.ru"
            };

            var siteUpdate = new Site()
            {
                SiteId = 1,
                Name = "gazeta.ru"
            };

            //act
            siteRepository.Add(site);

            siteRepository.Update(siteUpdate);

            var result = siteRepository.GetById(1);

            //assert
            Assert.NotNull(result);
            Assert.Equal(site.SiteId, result.SiteId);
            Assert.Equal(siteUpdate.Name, result.Name);
            //Assert.NotNull(result.Pages);
            //Assert.Equal(page.Url, result.Pages.First().Url);
            
            
        }
    }
}