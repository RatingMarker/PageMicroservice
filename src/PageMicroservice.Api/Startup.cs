using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNet.Builder;
using Microsoft.Extensions.DependencyInjection;
using Nancy.Owin;

namespace PageMicroservice.Api
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //            var connection = @"Server=(localdb)\mssqllocaldb;Database=PageMicroserviceDb;Trusted_Connection=True;";
            //            services.AddEntityFramework()
            //                    .AddSqlServer()
            //                    .AddDbContext<PageContext>(options => options.UseSqlServer(connection));

            var builder = new ContainerBuilder();

            builder.Populate(services);

            var container = builder.Build();

            return container.Resolve<IServiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseOwin(owin => owin.UseNancy(options => options.Bootstrapper = new Bootstrapper()));
        }
    }
}