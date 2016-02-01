using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using PageMicroservice.Data.Contexts;

namespace PageMicroservice.Data.Infrastructure
{
    public interface IContextFactory
    {
        PageContext Get();
    }

    public class ContextFactory: IContextFactory
    {
        private readonly DbContextOptions<PageContext> options; 

        public ContextFactory()
        {
            const string connectString = @"Server=(localdb)\mssqllocaldb;Database=PageMicroserviceDb;Trusted_Connection=True;";

            var optionsBuilder = new DbContextOptionsBuilder<PageContext>();
            optionsBuilder.UseSqlServer(connectString);
            options = optionsBuilder.Options;
        }

        public ContextFactory(string connectString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PageContext>();
            optionsBuilder.UseSqlServer(connectString);
            options = optionsBuilder.Options;
        }

        public ContextFactory(DbContextOptions<PageContext> options)
        {
            this.options = options;
        }

        public PageContext Get()
        {
            return new PageContext(options);
        }
    }
}