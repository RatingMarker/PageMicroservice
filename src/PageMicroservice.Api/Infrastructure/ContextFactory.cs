using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using PageMicroservice.Api.Contexts;

namespace PageMicroservice.Api.Infrastructure
{
    public interface IContextFactory
    {
        PageContext Get();
    }

    public class ContextFactory: IContextFactory
    {
        private readonly DbContextOptions<PageContext> options; 

        public ContextFactory(string connectString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PageContext>();
            optionsBuilder.UseNpgsql(connectString);
            options = optionsBuilder.Options;
        }

        public PageContext Get()
        {
            return new PageContext(options);
        }
    }
}