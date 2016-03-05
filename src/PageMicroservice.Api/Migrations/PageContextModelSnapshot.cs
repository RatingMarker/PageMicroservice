using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using PageMicroservice.Api.Contexts;

namespace PageMicroservice.Api.Migrations
{
    [DbContext(typeof(PageContext))]
    partial class PageContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348");

            modelBuilder.Entity("PageMicroservice.Api.Models.Page", b =>
                {
                    b.Property<int>("PageId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("FoundDate");

                    b.Property<DateTime?>("LastScanDate");

                    b.Property<int>("SiteId");

                    b.Property<int?>("SiteSiteId");

                    b.Property<string>("Url");

                    b.HasKey("PageId");
                });

            modelBuilder.Entity("PageMicroservice.Api.Models.Site", b =>
                {
                    b.Property<int>("SiteId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("SiteId");
                });

            modelBuilder.Entity("PageMicroservice.Api.Models.Page", b =>
                {
                    b.HasOne("PageMicroservice.Api.Models.Site")
                        .WithMany()
                        .HasForeignKey("SiteId");

                    b.HasOne("PageMicroservice.Api.Models.Site")
                        .WithMany()
                        .HasForeignKey("SiteSiteId");
                });
        }
    }
}
