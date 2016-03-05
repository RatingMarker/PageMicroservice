using Autofac;
using Mapster;
using Microsoft.Extensions.Configuration;
using NLog;
using PageMicroservice.Api.Infrastructure;
using PageMicroservice.Api.Repositories;
using PageMicroservice.Api.Services;

namespace PageMicroservice.Api.Configurations
{
    public class AutofacModule: Module
    {
        public AutofacModule()
        {
            var config = new ConfigurationBuilder();
            config.AddJsonFile("config.json");
            Configuration = config.Build();
        }

        public IConfiguration Configuration { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            RegistryMapper(builder);
            RegistryLogging(builder);
            RegistryRepositories(builder);
            RegistryServices(builder);
        }

        private void RegistryMapper(ContainerBuilder builder)
        {
            builder.RegisterType<Adapter>().As<IAdapter>();
        }

        private void RegistryRepositories(ContainerBuilder builder)
        {
            builder.Register(
                c =>
                {
                    var contextFactory = new ContextFactory(
                        Configuration["Data:DefaultConnection:ConnectionString"]);
                    return contextFactory;
                })
                   .As<IContextFactory>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<SiteRepository>().As<ISiteRepository>();
            builder.RegisterType<PageRepository>().As<IPageRepository>();
        }

        private void RegistryServices(ContainerBuilder builder)
        {
            builder.RegisterType<SiteService>().As<ISiteService>();
            builder.RegisterType<PageService>().As<IPageService>();
        }

        private void RegistryLogging(ContainerBuilder builder)
        {
            builder.Register(c => LogManager.GetLogger(GetType().Namespace)).As<ILogger>();
        }
    }
}