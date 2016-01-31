using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using PageMicroservice.Data.Contexts;

namespace PageMicroservice.Data.Migrations
{
    [DbContext(typeof(PageContext))]
    [Migration("20160126111410_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PageMicroservice.Models.Page", b =>
                {
                    b.Property<int>("PageId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("FoundDate");

                    b.Property<DateTime?>("LastScanDate");

                    b.Property<int>("SiteId");

                    b.Property<string>("Uri");

                    b.HasKey("PageId");
                });

            modelBuilder.Entity("PageMicroservice.Models.Site", b =>
                {
                    b.Property<int>("SiteId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("SiteId");
                });

            modelBuilder.Entity("PageMicroservice.Models.Page", b =>
                {
                    b.HasOne("PageMicroservice.Models.Site")
                        .WithMany()
                        .HasForeignKey("SiteId");
                });
        }
    }
}
