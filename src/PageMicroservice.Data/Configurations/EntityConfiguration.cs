using Microsoft.Data.Entity;
using PageMicroservice.Models;

namespace PageMicroservice.Data.Configurations
{
    public class EntityConfiguration
    {
        //public EntityConfiguration(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Page>(
        //        map =>
        //        {
        //            map.Reference(s => s.Site)
        //               .InverseCollection(s => s.Pages)
        //               .ForeignKey(s => s.SiteId);
        //            //HasRequired(s => s.Site)
        //            //   .WithMany(s => s.Pages)
        //            //   .HasForeignKey(s => s.SiteId);

        //            //map.Property(p => p.FoundDate);
        //            //map.Property(p => p.LastScanDate);
        //        });
        //}

        public static void PageConfig(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Page>(
                map =>
                {
                    map.HasOne(s => s.Site)
                       .WithMany(s => s.Pages)
                       .HasForeignKey(s => s.SiteId);

                    map.Property(s => s.FoundDate).IsRequired(false);
                    map.Property(s => s.LastScanDate).IsRequired(false);
                });
        }
    }
}