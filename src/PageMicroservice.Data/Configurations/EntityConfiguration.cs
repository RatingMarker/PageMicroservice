﻿using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using PageMicroservice.Models;

namespace PageMicroservice.Data.Configurations
{
    public class EntityConfiguration
    {
        public static void PageConfig(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Page>(
                map =>
                {
                    map.HasKey(s => s.PageId);

                    map.Property(s => s.PageId)
                       .ValueGeneratedNever();

                    map.HasOne(s => s.Site)
                       .WithMany(s => s.Pages)
                       .HasForeignKey(s => s.SiteId)
                       .OnDelete(DeleteBehavior.Restrict);

                    map.Property(s => s.FoundDate)
                       .IsRequired(false);

                    map.Property(s => s.LastScanDate)
                       .IsRequired(false);
                });
        }

        public static void SiteConfig(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Site>(
                map =>
                {
                    map.HasKey(s => s.SiteId);

                    map.Property(s => s.SiteId)
                       .ValueGeneratedNever();

                    map.HasMany(s => s.Pages)
                       .WithOne().OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}