using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using PageMicroservice.Api.Contexts;
using PageMicroservice.Api.Infrastructure;

namespace PageMicroservice.UnitTest.Fakes
{
    internal class MemoryContextFactory: IContextFactory
    {
        private readonly DbContextOptions<PageContext> options;

        public MemoryContextFactory()
        {
            var optionsBuilder = new DbContextOptionsBuilder<PageContext>();
            optionsBuilder.UseInMemoryDatabase();
            options = optionsBuilder.Options;
        }

        public PageContext Get()
        {
            return new PageContext(options);
        }
    }
}