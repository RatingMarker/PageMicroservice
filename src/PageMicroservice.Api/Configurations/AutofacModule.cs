using System.Reflection;
using Autofac;
using Microsoft.Extensions.Configuration;
using PageMicroservice.Api.Infrastructure;
using Module = Autofac.Module;

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
            builder.RegisterModule<AutoMapperModule>();

            RegistryRepositories(builder);
            RegistryServices(builder);
        }

        private void RegistryRepositories(ContainerBuilder builder)
        {
            builder.Register(c => new ContextFactory(Configuration["Data:DefaultConnection:ConnectionString"]))
                   .As<IContextFactory>()
                   .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(Assembly.Load("PageMicroservice.Api.Repositories"))
                   .Where(t => t.Name.EndsWith("Repository"))
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();
        }

        private void RegistryServices(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.Load("PageMicroservice.Api.Services"))
                   .Where(t => t.Name.EndsWith("Service"))
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();
        }
    }
}