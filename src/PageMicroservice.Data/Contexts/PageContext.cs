using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using PageMicroservice.Data.Configurations;
using PageMicroservice.Models;

namespace PageMicroservice.Data.Contexts
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
                const string connectString =
                    @"Server=(localdb)\mssqllocaldb;Database=PageMicroservice;Trusted_Connection=True;";
                optionsBuilder.UseSqlServer(connectString);
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