using System.Reflection;
using Autofac;
using PageMicroservice.Data.Infrastructure;
using Module = Autofac.Module;

namespace PageMicroservice.Data
{
    public class DataModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ContextFactory>().As<IContextFactory>()
                   .AsImplementedInterfaces().SingleInstance();

            builder.RegisterAssemblyTypes(Assembly.Load("PageMicroservice.Data"))
                   .Where(t => t.Name.EndsWith("Repository"))
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();
        }
    }
}