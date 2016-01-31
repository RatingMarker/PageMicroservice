using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using PageMicroservice.Data.Contexts;
using PageMicroservice.Data.Infrastructure;

namespace PageMicroservice.Data.UnitTest
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