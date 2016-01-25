using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using PageMicroservice.Data.Infrastructure;
using PageMicroservice.Data.Repositories;
using PageMicroservice.Models;
using Xunit;

namespace PageMicroservice.Data.UnitTest
{
    public class PageRepositoryTest
    {
        private readonly Mock<IContextFactory> contextFactoryMock;
        private readonly IPageRepository pageRepository;

        public PageRepositoryTest()
        {
            contextFactoryMock = new Mock<IContextFactory>();
            pageRepository = new PageRepository(contextFactoryMock.Object);
        }

        [Fact]
        public void WhereGetAllThenPages()
        {
            //arrange
            var pages = new List<Page>()
            {
                new Page() {PageId = 1, Uri = "http://lenta.ru/",FoundDate = DateTime.Now.AddDays(-1),LastScanDate = DateTime.Now},
                new Page() {PageId = 2, Uri = "http://lenta.ru/robots.txt",FoundDate = DateTime.Now.AddDays(-1),LastScanDate = DateTime.Now}
            };


            //act
            var result = pageRepository.GetAll();

            //assert
            Assert.NotNull(result);
            Assert.True(result.Any(), "Empty list");
            
        }

        private int Add(int x, int y) => x + y;
    }
}