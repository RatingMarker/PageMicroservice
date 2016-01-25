using Microsoft.Data.Entity;
using PageMicroservice.Data.Configurations;
using PageMicroservice.Models;

namespace PageMicroservice.Data.Contexts
{
    public class PageContext:DbContext
    {
        public PageContext()
        {
        }

        public DbSet<Site> Sites { get; set; }
        public DbSet<Page> Pages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            EntityConfiguration.PageConfig(modelBuilder);
        }
    }
}