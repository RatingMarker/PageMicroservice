using System;
using System.Collections.Generic;
using System.Linq;
using PageMicroservice.Api.Models;
using PageMicroservice.Api.Repositories;
using PageMicroservice.UnitTest.Fakes;
using Xunit;
using Xunit.Runners;

namespace PageMicroservice.UnitTest.Repositories
{
    
    public class PageRepositoryTest
    {
        private readonly IPageRepository pageRepository;

        public PageRepositoryTest()
        {
            pageRepository = new PageRepository(new MemoryContextFactory());
        }

        [Fact]
        public void Should_return_updated_page_when_updating_page()
        {
            //arrange
            var page = new Page()
            {
                PageId = 1,
                FoundDate = DateTime.Now,
                LastScanDate = DateTime.Now,
                Url = "http://lenta.ru"
            };

            var pageUpdate = new Page()
            {
                PageId = 1,
                Url = "http://lenta.ru/news"
            };

            //act
            pageRepository.Add(page);

            page.Url = pageUpdate.Url;

            pageRepository.Update(page);

            var result = pageRepository.GetById(1);

            //assert
            Assert.NotNull(result);
            Assert.Equal(page.PageId, result.PageId);
            Assert.Equal(page.Url, result.Url);
            Assert.Equal(page.FoundDate, result.FoundDate);
            Assert.Equal(page.LastScanDate, result.LastScanDate);
        }
    }
}