using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Extensions.Configuration;
using PageMicroservice.Api.Infrastructure;
using PageMicroservice.Api.Models;

namespace PageMicroservice.Api.Contexts
{
    public class PageContext:DbContext
    {
        public PageContext()
        {
        }

        public PageContext(DbContextOptions options): base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configurationBuilder = new ConfigurationBuilder();
                configurationBuilder.AddJsonFile("config.json");
                var configuration = configurationBuilder.Build();
                optionsBuilder.UseNpgsql(configuration["Data:DefaultConnection:ConnectionString"]);
            }
        }

        public DbSet<Site> Sites { get; set; }
        public DbSet<Page> Pages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            EntityConfiguration.PageConfig(modelBuilder);
            EntityConfiguration.SiteConfig(modelBuilder);
        }
    }
}