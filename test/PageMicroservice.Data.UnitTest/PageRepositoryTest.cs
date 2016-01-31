using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using PageMicroservice.Data.Repositories;
using PageMicroservice.Models;
using Xunit;
using Xunit.Abstractions;

namespace PageMicroservice.Data.UnitTest
{
    public class PageRepositoryTest
    {
        private readonly ISiteRepository siteRepository;
        private readonly IPageRepository pageRepository;

        public PageRepositoryTest()
        {
            var contextFactory = new MemoryContextFactory();
            siteRepository = new SiteRepository(contextFactory);
            pageRepository = new PageRepository(contextFactory);
        }

        [Fact]
        public void WhenInsertPages()
        {
            //arrange
            var site = new Site() {Name = "lenta.ru"};

            var pages = new List<Page>()
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
                },
                new Page()
                {
                    Uri = "http://lenta.ru/new/2",
                    FoundDate = DateTime.Now.AddDays(-1),
                    LastScanDate = DateTime.Now
                }
            };

            //act
            site = siteRepository.Add(site);

            foreach (Page page in pages)
            {
                page.SiteId = site.SiteId;
            }

            pageRepository.Insert(pages);

            var result = pageRepository.GetAll();

            //assert
            Assert.NotNull(result);
            Assert.True(result.Any(), "Empty list");
            Assert.Equal(result.Count(), pages.Count());
            Assert.NotEqual(result.First().PageId, 0);
            Assert.NotNull(result.First().Site);
            
            Assert.NotEqual(site.SiteId, 0);
        }
    }
}