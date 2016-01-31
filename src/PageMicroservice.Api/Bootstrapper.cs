using Autofac;
using Nancy;
using Nancy.Bootstrappers.Autofac;
using PageMicroservice.Api.Configurations;

namespace PageMicroservice.Api
{
    internal class Bootstrapper: AutofacNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(ILifetimeScope existingContainer)
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<AutofacModule>();

            builder.Update(existingContainer.ComponentRegistry);
        }
    }
}