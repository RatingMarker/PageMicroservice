using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using PageMicroservice.Api.Contexts;
using PageMicroservice.Api.Models;
using PageMicroservice.UnitTest.Fakes;
using Xunit;

namespace PageMicroservice.UnitTest.Services
{
    public class LogicWithRepositoryTest
    {
        //private readonly IPageRepository pageRepository;
        //private readonly ISiteRepository siteRepository;

        public LogicWithRepositoryTest()
        {
            //var contextFactory = new MemoryContextFactory();
            //siteRepository = new SiteRepository(contextFactory);
            //pageRepository = new PageRepository(contextFactory);
        }

        [Fact]
        public void Given_added_page_when_add_site_then_site_attached_exist_page()
        {
            //arrange
            var optionsBuilder = new DbContextOptionsBuilder<PageContext>();
            optionsBuilder.UseInMemoryDatabase();
            DbContextOptions<PageContext> options = optionsBuilder.Options;

            var page = new Page()
            {
                Uri = "http://lenta.ru"
            };

            var site = new Site()
            {
                Name = "lenta.ru",
            };

            var siteUpdate = new Site()
            {
                Name = "gazeta.ru"
            };

            //act
            using (var context = new PageContext(options))
            {
                page = context.Pages.Add(page).Entity;
            }

            var pages = new List<Page>();

            pages.Add(page);

            site.Pages = pages;

            using (var context = new PageContext(options))
            {
                site = context.Sites.Add(site).Entity;
            }

            int siteId = site.SiteId;

            siteUpdate.SiteId = siteId;

            Site result = null;

            using (var context = new PageContext(options))
            {
                result = context.Sites.Update(siteUpdate).Entity;
            }

            //assert
            Assert.NotNull(result);
            Assert.NotEqual(site.Name, result.Name);
            Assert.NotNull(result.Pages);
            Assert.Equal(page.Uri, result.Pages.FirstOrDefault().Uri);
        }
    }
}